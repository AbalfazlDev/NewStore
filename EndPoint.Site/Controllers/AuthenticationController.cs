using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var result = "sdd";
            return Json(result);
        }


    }
}
