using Mark.Dao;
using Mark.Models;
using Mark.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mark.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ToyController(ApplicationDbContext db, IWebHostEnvironment webHostEnviroment)
        {
            _db = db;
            _webHostEnviroment = webHostEnviroment; 
        }

        //Get 
        //Данная функция позволяет взять данные из базы данных
        public IActionResult Index()
        {
            //Берем из базы данных объекты Toy
            IEnumerable<Toy>? toys = _db.toys;
            foreach(var obj in toys)
            {
                //Каждому объекту подгружаем категорию ( так как в базе данных ссылается внешний ключ)
                obj.Category = _db.categories.FirstOrDefault(u => u.Id == obj.CategoryId);
            }
            //Передаем представлению, тоесть html странице данные что бы она их вывела
            return View(toys);
        }

        //Get
       
        public IActionResult Upsert(int? id)
        {
            ToyVM toyVM = new ToyVM()
            {
                //создаем обхект игрушки
                toy =new Toy(),
                //подгружаем категории
                categoryList = _db.categories.Select(i=>new SelectListItem
                {
                    Text=i.Name,
                    Value=i.Id.ToString()
                })
            };
            //Если такой игрушки нет, то выводим пользователю пустую формочку для ввода данных
            if (id == null)  
            {
             
                return View(toyVM);
            }
            else
            {
                //если такая игрушка есть, заполняем данные в форме
                toyVM.toy = _db.toys.Find(id);
                if(toyVM.toy== null)
                {
                    return NotFound();
                }
                //и выводим на страницу, для редактирования данных
                return View(toyVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ToyVM obj)
        {
            //Получаем фалй который передает пользователь в системк
            var files = HttpContext.Request.Form.Files;
            //получаем путь с помощью ссылки
            string webRootPath = _webHostEnviroment.WebRootPath;

            if (obj.toy.Id == 0)
            {
                //Получаем новый путь
                string upload = webRootPath + WC.ImagePath;
                //получаем иденфикатор файла
                string fileName = Guid.NewGuid().ToString();
                //берем имя
                string extnsion = Path.GetExtension(files[0].FileName);
                //создаем поток для файла
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extnsion), FileMode.Create))
                {
                    //копируем файл в этот поток 
                    files[0].CopyTo(fileStream);
                }
                //задаем файлу новый путь
                obj.toy.Image = fileName + extnsion;
                //добавляем изменения в базу данных
                _db.toys.Add(obj.toy);
                //сохраняем изменения
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            //если пользователь редактирует данные
            else
            {
                //получаем объект из базы данных
                var objDB = _db.toys.AsNoTracking().FirstOrDefault(u => u.Id == obj.toy.Id);
                if (files.Count > 0)
                {
                    //путь загруженного файла
                    string upload = webRootPath + WC.ImagePath;
                    //берем иденфикатор файла
                    string fileName = Guid.NewGuid().ToString();
                    //берем файл загруженный
                    string extnsion = Path.GetExtension(files[0].FileName);
                    //находим старый путь файла
                    var oldImage = Path.Combine(upload, objDB.Image);
                    if(System.IO.File.Exists(oldImage))
                    {
                        //удаляем старый файл
                        System.IO.File.Delete(oldImage);
                    }
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extnsion), FileMode.Create))
                    {
                        //добавляем вновь загруженный файл
                        files[0].CopyTo(fileStream);
                    }
                    obj.toy.Image= fileName + extnsion;

                }
                else
                {
                    //если пользователь не исправляет картинку у товара, то оставляем ее такой же
                    obj.toy.Image = objDB.Image;
                }
                //обновляем данные в базе данных
                _db.Update(obj.toy);
                //сохраняем данные
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Toy toy = _db.toys.Include(u=>u.CategoryId).FirstOrDefault(u=>u.Id==id);
        //    if(toy == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(toy);

        //}

        public IActionResult Delete(int? id)
        {
            //Находим объект в базе данных
            Toy toy = _db.toys.Find(id);
            if(toy == null) {
                return NotFound();
            }
            string upload = _webHostEnviroment.WebRootPath + WC.ImagePath;
            var oldImage = Path.Combine(upload, toy.Image);
            if (System.IO.File.Exists(oldImage))
            {
                //удаляем изображение
                System.IO.File.Delete(oldImage);
            }
            //удаляем обхект 
            _db.toys.Remove(toy);
            //сохраняем изменения
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
