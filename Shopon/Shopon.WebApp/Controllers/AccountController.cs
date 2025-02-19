using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopon.WebApp.Models;

namespace Shopon.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVM register)
        {
            if(ModelState.IsValid) 
            {
                var identityUser = new IdentityUser
                {
                    UserName = register.UserName,
                    Email = register.EmailId,
                    PhoneNumber = register.MobileNumber,
                };
                var result = await userManager.CreateAsync(identityUser, register.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                // handling error

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(LoginVM login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(login.EmailId, login.Password, login.RememberMe, false);
        //        if(result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError("", "Invalid email id or password");
        //    }
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(login.EmailId);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid EmailId or Password!");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }

    }
}
