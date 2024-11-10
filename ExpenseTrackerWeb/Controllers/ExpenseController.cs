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
                return BadRequest(new { message = "User is not authenticated." });
            }
            return View(_userCategoryMgr.ListCategory(UserId));
        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category userCategory)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            userCategory.UserId = UserId;
            userCategory.CreatedDate = DateTime.Now;

            if (_userCategoryMgr.CreateCategory(userCategory, ref ErrorMessage) != ErrorCode.Success)
            {
                return BadRequest(new { message = "Category creation failed." });
            }

            return Ok(new { success = true, message = "Category added successfully." });
        }

        [HttpPut]
        public IActionResult EditCategory([FromBody] Category category)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            category.UserId = UserId;

            if (_userCategoryMgr.UpdateCategory(category, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update category.", errors = ModelState });
            }

            return Ok(new { message = "Category updated successfully." });
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            if (_userCategoryMgr.DeleteCategory(id, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to delete category.", errors = ModelState });
            }

            return Ok(new { message = "Expense deleted successfully." });
        }
        #endregion

        public IActionResult Reports()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExpenseSummary()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }



            var viewModel = new ExpenseSummaryViewModel
            {
                Expenses = _userExpenseMgr.ListUserExpense(UserId),
                MonthList = _monthYearMgr.ListMonths()
            };

            return View(viewModel);
        }


        public IActionResult GenerateReport()
        {
            return View();
        }

    }
}
