using Mark.Dao;
using Mark.Models;
using Mark.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mark.Controllers
{
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ToyController(ApplicationDbContext db)
        {
            _db = db;
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
            //IEnumerable<SelectListItem> categories = _db.categories.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value=i.Id.ToString()
            //}) ;
            //Toy toy = new Toy();
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
                //this create 
                return View(toyVM);
            }
            else
            {
                toyVM.toy = _db.toys.Find(id);
                if(toyVM == null)
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
            if(ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View(obj); 
        }
    }
}
