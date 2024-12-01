using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Services.Controllers;

namespace ExpenseTrackerWeb.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult ExpenseTracker()
        {
            return View();
        }

        public IActionResult WhyExpenseTracker()
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }
    }

}
