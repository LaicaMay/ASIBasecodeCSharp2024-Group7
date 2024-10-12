using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseTracker.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Resources.Utils;

namespace ExpenseTrackerWeb.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult ExpenseTracker()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            List<User> userList = _userRepo.GetAll();
            return View(userList);
        }

        public IActionResult CreateAccount(User u, String ConfirmPass)
        {
            var userObj = _db.Users.Where(model => (model.Username == u.Username || model.Username == u.Username)).FirstOrDefault();

            if (_userManager.SignUp(u, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return View(u);
            }
            TempData["Username"] = u.Username;
            return RedirectToAction("Dashboard");
        }

    }


}
