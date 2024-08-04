using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TST.Core.Models;
using TST.Web.Models;

namespace TST.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserDa userDa;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserDa userDa)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userDa = userDa;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = userManager.FindByNameAsync(model.Username).Result;
                    if (!user.IsActive || user.IsDelete)
                    {
                        ModelState.AddModelError("Password", "Username or Password is incorrect.");

                        var roles = await userManager.GetRolesAsync(user);

                        await signInManager.SignOutAsync();

                        return View(model);
                    }

                    return RedirectToAction("Index", "BofHome");
                }
                else
                {
                    ModelState.AddModelError("Password", "Username or Password is incorrect.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

    }
}
