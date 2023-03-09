using Mark.Dao;
using Microsoft.AspNetCore.Mvc;

namespace Mark.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db) {
            _db = db;
        }

        [HttpPost]
        public ActionResult Login() {

            return View();
        }
    }
}
