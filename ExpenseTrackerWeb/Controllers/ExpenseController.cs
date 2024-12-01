using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Services.Controllers;
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
            var existUser = _userManager.GetUserById(UserId);

            if (User.Identity.IsAuthenticated)
            {
                if (existUser.isVerify == false)
                {
                    return RedirectToAction("Verify", "Account");
                }

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
            balance.RemainingBalance = balance.TotalBalance;

            if (_balanceMgr.AddBalance(balance, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to add balance.", errors = ModelState });
            }

            return Ok(new { message = "Balance added successfully." });
        }

        [HttpDelete]
        public IActionResult DeleteBalance(int id)
        {
            var userBal = _balanceMgr.GetBalanceById(id);

            if (userBal == null)
            {
                return BadRequest(new { success = false, message = "Balance is null" });
            }

            if(userBal.isActive == true)
            {
                return BadRequest(new { success = false, message = "You cant delete Active Balance" });
            }

            if (_balanceMgr.Delete(id, ref ErrorMessage) != ErrorCode.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to delete balance.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            return Ok(new { success = true, message = "Balance deleted successfully." });
        }


        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }

            //Set the UserId for the expense
            expense.UserId = UserId;

            //Add the expense
            if (_userExpenseMgr.Add(expense, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
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
            decimal? newRemainingBal = 0;
            decimal? newCategoryAmount = 0;

            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not aunthenticated" });
            }

            var existExpense = _userExpenseMgr.GetExpenseById(id);
            var existBalance = _balanceMgr.GetActiveBalanceByUserId(UserId);
            var existCategory = _userCategoryMgr.GetCategoryById(existExpense.CategoryId);

            if (existExpense == null) 
            {
                return BadRequest(new { message = "Expense is null." });
            }

            if (existBalance == null) 
            {
                return BadRequest(new { message = "Balance is null." });
            }

            if (existCategory == null)
            {
                return BadRequest(new { message = "Category is null." });
            }

            newRemainingBal = existBalance.RemainingBalance + existExpense.Amount;
            existBalance.RemainingBalance = newRemainingBal;

            if (_balanceMgr.UpdateBalance(existBalance, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update balance amount.", errors = ModelState });
            }

            newCategoryAmount = existCategory.TotalAmount - existExpense.Amount;
            existCategory.TotalAmount = newCategoryAmount;

            if (_userCategoryMgr.UpdateCategory(existCategory, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update category amount.", errors = ModelState });
            }

            if (_userExpenseMgr.Delete(id, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to delete expense.", errors = ModelState });
            }

            return Ok(new { message = "Expense deleted successfully." });
        }


        public IActionResult GetActiveUserBalance(int userBal)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not aunthenticated" });
            }

            if (userBal == null || userBal == 0)
            {
                userBal = UserId;
            }

            var userActiveBal = _balanceMgr.GetActiveBalanceByUserId(userBal);

            return Json(userActiveBal);
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
            decimal? updateBalance = 0;

            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { message = "User is not authenticated." });
            }
            var existCategory = _userCategoryMgr.GetCategoryById(id);
            var existActiveBal = _balanceMgr.GetActiveBalanceByUserId(UserId);

            if(existCategory == null)
            {
                return BadRequest(new { message = "existCategory is null." });
            }

            if(existActiveBal == null)
            {
                return BadRequest(new { message = "existActiveBal is null." });
            }

            updateBalance = existActiveBal.RemainingBalance + existCategory.TotalAmount;
            existActiveBal.RemainingBalance = updateBalance;

            if(_balanceMgr.UpdateBalance(existActiveBal, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return BadRequest(new { message = "Failed to update balance.", errors = ModelState });
            }

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
            var months = _monthYearMgr.ListMonths()
                                      .OrderBy(m => m.MonthId)
                                      .Select(m => m.MonthName)
                                      .ToArray();

            var userBalance = _balanceMgr.ListUserBalance(UserId);
            var userRemBal = _balanceMgr.GetActiveBalanceByUserId(UserId);

            var expensesByCategoryAndMonth = _userExpenseMgr.GroupExpensesByCategoryAndMonth(UserId);
            var currentMonth = DateTime.Now.ToString("yyyy-MM");
            var currentMonthExpenses = _userExpenseMgr.FilterExpensesByMonth(UserId, currentMonth);

            ViewData["ExpMonthAndCateg"] = expensesByCategoryAndMonth;
            ViewData["curMonthExp"] = currentMonthExpenses;
            ViewData["RemainingBal"] = userRemBal.RemainingBalance;


            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult GenerateReport()
        {
            var months = _monthYearMgr.ListMonths()
                                       .OrderBy(m => m.MonthId)
                                       .Select(m => m.MonthName)
                                       .ToArray();

            var userBalance = _balanceMgr.ListUserBalance(UserId);
            var userExpenses = _userExpenseMgr.ListUserExpense(UserId);
            var userCategories = _userCategoryMgr.ListCategory(UserId);

            var categoryMap = userCategories.ToDictionary(c => c.CategoryId, c => c.CategoryName);

            var userExpensesReport = userExpenses.Select(e => new
            {
                e.ExpenseName,
                e.Amount,
                Category = e.CategoryId.HasValue && categoryMap.ContainsKey(e.CategoryId.Value)
                    ? categoryMap[e.CategoryId.Value]
                    : "Uncategorized",
                Date = e.StartDate?.ToString("yyyy-MM-dd") ?? "N/A",
                e.Description
            }).ToList();

            return Ok(new
            {
                message = "Report Generated",
                expenses = userExpensesReport
            });
        }
    }
}
