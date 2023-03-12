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

        public IActionResult Index()
        {
            IEnumerable<Toy>? toys = _db.toys;
            foreach(var obj in toys)
            {
                obj.Category = _db.categories.FirstOrDefault(u => u.Id == obj.CategoryId);
            }
            return View(toys);
        }

        public IActionResult Upsert(int? id)
        {
            ToyVM toyVM = new ToyVM()
            {
                toy =new Toy(),
                categoryList=_db.categories.Select(i=>new SelectListItem
                {
                    Text=i.Name,
                    Value=i.Id.ToString()
                })
            };
            if (id == null)  
            {
             
                return View(toyVM);
            }
            else
            {
                toyVM.toy = _db.toys.Find(id);
                if(toyVM.toy== null)
                {
                    return NotFound();
                }
                return View(toyVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ToyVM obj)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnviroment.WebRootPath;

            if (obj.toy.Id == 0)
            {
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extnsion = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extnsion), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                obj.toy.Image = fileName + extnsion;
                _db.toys.Add(obj.toy);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                var objDB = _db.toys.AsNoTracking().FirstOrDefault(u => u.Id == obj.toy.Id);
                if (files.Count > 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extnsion = Path.GetExtension(files[0].FileName);
                    var oldImage = Path.Combine(upload, objDB.Image);
                    if(System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extnsion), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    obj.toy.Image= fileName + extnsion;

                }
                else
                {
                    obj.toy.Image = objDB.Image;
                }

                _db.Update(obj.toy);
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
            Toy toy = _db.toys.Find(id);
            if(toy == null) {
                return NotFound();
            }
            string upload = _webHostEnviroment.WebRootPath + WC.ImagePath;
            var oldImage = Path.Combine(upload, toy.Image);
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }
            _db.toys.Remove(toy);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
