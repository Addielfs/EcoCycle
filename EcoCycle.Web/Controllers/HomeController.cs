using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Auth");
        }
    }
}
