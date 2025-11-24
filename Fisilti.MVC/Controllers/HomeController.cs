using Fisilti.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

namespace Fisilti.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var CookieOptions = new CookieOptions
            //{
            //    Expires = DateTime.Now.AddDays(7),
            //    HttpOnly = true, //jsde okunmasını engeller
            //    Secure = true, //https
            //    SameSite = SameSiteMode.Strict //csrf saldırıları 
            //};


            //Response.Cookies.Append("AdSoyad", "cookies hazretleri", CookieOptions);
            //----------------------------------------------------------------
            //coookie içerisindeki bilgiye erişme
            //String AdiSoyad = Request.Cookies["AdSoyad"];
            //cookiyi temizleme
            //Response.Cookies.Delete("AdSoyad");
            var a = User.Claims;

             return View();
        }
    }
}
