using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseTracker.Data.Contracts;
using Microsoft.AspNetCore.Authorization;

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

    }


}
