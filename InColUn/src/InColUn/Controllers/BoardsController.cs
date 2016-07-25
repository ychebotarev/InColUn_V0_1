using Microsoft.AspNetCore.Mvc;

namespace InColUn.Controllers
{
    public class BoardsController : Controller
    {
        public IActionResult Index()
        {
            if(!HttpContext.Request.Cookies.ContainsKey("access_token"))
            {
                return RedirectToAction("/");
            }
            var token = HttpContext.Request.Cookies["access_token"];

            //TODO refresh token
            return View();
        }
    }
}
