using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.CustomModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTracker.Resources.Utils;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Data.Repository;

namespace ExpenseTrackerWeb.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var userObj = _db.Users.Where(model => (model.Username == user.Username || model.Username == user.Username)).FirstOrDefault();

            if (userObj == null)
            {
                ViewData["ErrorMessage"] = "Incorrect Password or User does not exist.";
                return View(user);
            }

            if (user.Password != userObj.Password)
            {
                ViewData["ErrorMessage"] = "Incorrect Password or User does not exist.";
                return View(user);
            }

            ViewData["ErrorMessage"] = null;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(userObj.UserId))
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            return RedirectToAction("Dashboard", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard", "Home");

            return View();
                
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(User u, String ConfirmPass)
        {
            if (_userManager.SignUp(u, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return View(u);
            }
            TempData["Username"] = u.Username;
            return RedirectToAction("Login");
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult UpdateUserInfo()
        {
            return View();
        }

    }
}
