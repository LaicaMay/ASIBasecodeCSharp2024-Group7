using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.CustomModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Resources.Constants;

namespace ExpenseTrackerWeb.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login(String ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)      
                return RedirectToAction("Overview", "Expense");

            ViewBag.Error = String.Empty;
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            if (_userManager.SignIn(username, password, ref ErrorMessage) == ErrorCode.Success)
            {
                var user = _userManager.GetUserByUsername(username);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(user.UserId))
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                return RedirectToAction("Overview", "Expense");
            }

            ViewBag.Error = ErrorMessage;
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
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
                return RedirectToAction("Overview", "Expense");

            return View();
                
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(User u)
        {
            if (_userManager.SignUp(u, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return View(u);
            }
            TempData["Username"] = u.Username;
            return RedirectToAction("Login");
        }

        [Authorize]
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
