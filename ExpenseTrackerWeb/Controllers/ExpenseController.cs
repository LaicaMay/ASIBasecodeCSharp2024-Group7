using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Data.Models.CustomModels;

namespace ExpenseTrackerWeb.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        #region ExpenseManagement
        public IActionResult Overview()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            ViewBag.Category = Utilities.SelectListItemCategoryByUser(UserId);

            var expense = _userExpenseMgr.ListUserExpense(UserId);
            var expenseVw = _userExpenseMgr.ListExpense(UserId);

            var model = new OverviewViewModel
            {
                UserExpenses = expense,
                ExpensesView = expenseVw
            };
     
            return View(model);
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            expense.UserId = UserId;

            if (_userExpenseMgr.Create(expense, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to add expense.", errors = ModelState });
            }

            return Ok(new { message = "Expense added successfully." });
        }

        public IActionResult UpdateExpense()
        {
            return View();
        }

        public IActionResult DeleteExpense()
        {
            return View();
        }
        #endregion

        #region CategoryManagement
        public IActionResult Category()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            return View(_userCategoryMgr.ListCategory(UserId));
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        public IActionResult EditCategory()
        {
            return View();
        }

        public IActionResult DeleteCategory()
        {
            return View();
        }
        #endregion

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult ExpenseSummary()
        {
            return View();
        }

        public IActionResult GenerateReport()
        {
            return View();
        }

    }
}
