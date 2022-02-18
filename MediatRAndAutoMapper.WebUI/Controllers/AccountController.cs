using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities.Membership;
using MediatRAndAutoMapper.WebUI.Models.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MediatRAndAutoMapper.WebUI.Controllers
{
    public class AccountController : Controller
    {
        readonly TransportDbContext db;
        readonly UserManager<AppUser> userManager;
        readonly SignInManager<AppUser> signInManager;

        public AccountController(TransportDbContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> SignIn(SignInFormModel formModel)
        {
            Regex pattern = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            AppUser userResult = null;

            if (pattern.IsMatch(formModel.Username))
            {
                userResult = await userManager.FindByEmailAsync(formModel.Username);
            }
            else
            {
                userResult = await userManager.FindByNameAsync(formModel.Username);
            }

            if (userResult != null)
            {
                SignInResult signInResult = await signInManager.PasswordSignInAsync(userResult, formModel.Password, false, false);

                if (signInResult.Succeeded)
                {
                    string returlUrl = HttpContext.Request.Query["returnUrl"];

                    if (!string.IsNullOrWhiteSpace(returlUrl))
                    {
                        return Redirect(returlUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), "Passengers");
                    }
                }
                else
                {
                    ModelState.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                    TempData["SingInError"] = "İstifadəçi adı və ya şifrə səhvdir.";
                }
            }
            else
            {
                ModelState.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                TempData["SingInError"] = "İstifadəçi adı və ya şifrə səhvdir.";
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> Register(RegisterFormModel formModel)
        {
            var appUser = new AppUser
            {
                UserName = formModel.Username,
                Email = formModel.Email
            };

            var appResponse = await userManager.CreateAsync(appUser, formModel.Password);

            if (appResponse.Succeeded)
            {
                return Redirect(@"\signin.html");
            }
            else
            {
                foreach (IdentityError error in appResponse.Errors)
                {
                    ModelState.AddModelError("RegisterError", error.Description);
                }
            }

            return View();
        }

        public IActionResult AccessDenied()
        {

            return View();
        }

        async public Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return Redirect(@"/signin.html");
        }
    }
}
