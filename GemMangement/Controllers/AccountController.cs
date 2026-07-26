using GemMangement.DAL.Models;
using GemMangement_AL_.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GemMangement.Pl.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
          _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(AccountViewModel model,CancellationToken ct=default)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
             var user= await _userManager.FindByEmailAsync(model.Email);
            if (user is null) 
            {
                ModelState.AddModelError("invalidlogin", "invalid Email Or Password");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded) 
            {
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("invalidlogin", "invalid Email Or Password");
                return View(model);
            }
         

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        public IActionResult accessDenied()
        {
            return View();
        }
    }
}
