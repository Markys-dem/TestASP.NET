using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mark.Controllers
{
    public class BasketController : Controller
    {
        // GET: BasketController
        public ActionResult Index()
        {
            return View();
        }
    }
}
