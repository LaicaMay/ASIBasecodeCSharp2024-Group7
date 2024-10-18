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
        public IActionResult Overview(string searchTerm = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var expenses = _userExpenseMgr.ListUserExpense(UserId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchResults = _expenseSearch.SearchExpenses(searchTerm)
                                    .Where(e => e.UserId == UserId) 
                                    .ToList();

                expenses = searchResults.Any() ? searchResults : expenses;
            }

            ViewBag.Category = Utilities.SelectListItemCategoryByUser(UserId);

            return View(expenses);
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

        [HttpPut]
        public IActionResult UpdateExpense([FromBody] Expense expense)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            expense.UserId = UserId;

            if (_userExpenseMgr.Update(expense, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update expense.", errors = ModelState });
            }
          
            return Ok(new { message = "Expense updated successfully." });
        }

        [HttpDelete]
        public IActionResult DeleteExpense(int id)
        {

            if(_userExpenseMgr.Delete(id, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to delete expense.", errors = ModelState });
            }

            return Ok(new { message = "Expense deleted successfully." });
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
