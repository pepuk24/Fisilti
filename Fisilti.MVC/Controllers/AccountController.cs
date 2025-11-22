using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Fisilti.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fisilti.MVC.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IMapper _mapper;
        IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        //Sadece Get İsteklerine Yanıt Versin
        [HttpGet] //Sadece Sayfayı Görüntülemek için. yani /Account/Register url açılmak istenirse alttaki action çalışacak ve kayıt olma sayfasını görüntülemek için
        public IActionResult Register()
        {
            return View();
        }

        //Sadece Post İsteklerine Yanıt Versin
        [HttpPost] // Kayıt ol butonuna basıldığında form içerisinde girilen verileri alıp kullanıcıyı kaydedecek
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);



            AppUser user = _mapper.Map<AppUser>(model);

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var dogrulamaLinki = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                await new EmailProcess(_config).SendEmail("Email Doğrulama", $"Hesabınızı Doğrulamak İçin <a href= '{dogrulamaLinki}'>Tıklayın</a>",emailAddresses:user.Email);

                return View("MailConfiguration");
            }


            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return View("EmailConfirmed");

            return BadRequest();
        }


        //HTTP(Hyper Text Transfer Protocol): Web üzerinde veri transferlerinde kullanılan kurallar.

        //HTTP Methodları;
        //----------------
        // GET   : Genelde veri çekmek için kullanılır.
        // POST  : Genelde veri göndermek için kullanılır.
        // PUT   : Veri güncellemek için kullanılır.
        // DELETE: Veri silmek için kullanılır.
        // PATCH : Bütün veriyi değil sadece verinin bir bölümünü güncelleyeceksek kullanılır.

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
