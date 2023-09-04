using Mark.Dao;
using Mark.Models;
using Mark.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace Mark.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit_User() {
            if(WC.userd is null)
            {
                return View("Login", "User");
            }
            else
            {
                return View(WC.userd);
            }
        }

        [HttpPost]
        public IActionResult Edit_User(User user)
        {
            _db.users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Registery (User obj)
        {
            _db.users.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Registery_form()
        {
            return View();
        }

        public IActionResult Sing_In (UserVM user_vm)
        {
            WC.userd = _db.users.FirstOrDefault(u => u.Email == user_vm.Email && u.Password == user_vm.Password);
            if (WC.userd != null) 
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}
