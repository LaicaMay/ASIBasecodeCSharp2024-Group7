using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExpenseTracker.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Resources.Constants;

namespace ExpenseTrackerWeb.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult ExpenseTracker()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Overview", "Expense");
            }
            return View();
        }         

    }

}
