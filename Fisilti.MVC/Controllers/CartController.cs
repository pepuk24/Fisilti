using Microsoft.AspNetCore.Mvc;

namespace Fisilti.MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
