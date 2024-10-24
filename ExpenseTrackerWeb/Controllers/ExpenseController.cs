using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Data.Models.CustomModels;
using ExpenseTrackerWeb.Models;

namespace ExpenseTrackerWeb.Controllers
{
    [Authorize]
    public class ExpenseController : BaseController
    {
        #region ExpenseManagement
        public IActionResult Overview(string Search = "", string sortOrderCategory = "reset", string sortOrderDate = "reset")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            var expenses = _userExpenseMgr.ListUserExpense(UserId);

            var userBalance = _balanceMgr.ListUserBalance(UserId);

            if (!string.IsNullOrEmpty(Search))
            {
                var searchResults = _expenseSearch.SearchExpenses(Search)
                                    .Where(e => e.UserId == UserId)
                                    .ToList();

                expenses = searchResults.Any() ? searchResults : expenses;
            }

            if (sortOrderCategory != "reset" && sortOrderDate == "reset")
            {

                expenses = expenses.OrderBy(e => e.Category?.CategoryName).ToList();
            }
            else if (sortOrderDate != "reset" && sortOrderCategory == "reset")
            {

                expenses = expenses.OrderBy(e => e.Date).ToList();
            }

            var viewModel = new OverviewViewModel
            {
                UserExpense = expenses,
                UserBalance = userBalance,
            };

            ViewBag.Category = SelectDropDownItem.SelectListItemCategoryByUser(UserId);
            ViewBag.BalanceDate = SelectDropDownItem.SelectListItemMonthYearByUser(UserId);
            ViewBag.Month = SelectDropDownItem.SelectListsMonth();
            ViewBag.Year = SelectDropDownItem.SelectListsYear();
            ViewBag.CurrentSortOrderCategory = sortOrderCategory;
            ViewBag.CurrentSortOrderDate = sortOrderDate;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddBalance([FromBody] Balance balance)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            var currentActiveBalance = _balanceMgr.GetActiveBalanceByUserId(UserId);

            if (currentActiveBalance != null)
            {
                currentActiveBalance.isActive = false;
                _balanceMgr.UpdateBalance(currentActiveBalance, ref ErrorMessage);
            }

            balance.UserId = UserId;          
            balance.isActive = true;

            if (_balanceMgr.AddBalance(balance, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to add balance.", errors = ModelState });
            }

            return Ok(new { message = "Balance added successfully." });
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            expense.UserId = UserId;

            if (_userExpenseMgr.Add(expense, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to add expense.", errors = ModelState });
            }

            return Ok(new { message = "Expense added successfully and balance updated." });
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
