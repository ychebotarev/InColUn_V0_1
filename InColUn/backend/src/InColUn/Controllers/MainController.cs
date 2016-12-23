using Microsoft.AspNetCore.Mvc;

namespace InColUn.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            if(!HttpContext.Request.Cookies.ContainsKey("access_token"))
            {
                return RedirectToAction("Index", "Home");
            }

            var token = HttpContext.Request.Cookies["access_token"];

            //TODO refresh token
            return View();
        }
    }
}
