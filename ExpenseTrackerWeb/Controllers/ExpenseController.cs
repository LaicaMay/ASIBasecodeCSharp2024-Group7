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

        public IActionResult ExpenseSummary()//(int userId)
        {
            int userId = 1;
            var userExpenseManager = new UserExpenseManager();
            var monthYearManager = new MonthYearManager();

            var months = monthYearManager.ListMonths()
                                         .OrderBy(m => m.MonthId)
                                         .Select(m => m.MonthName)
                                         .ToArray();

            var userExpenses = userExpenseManager.ListUserExpense(userId);

            var groupedExpenses = userExpenses
                .GroupBy(e => e.Category?.ExpenseCategoryName)
                .Select(group => new
                {
                    Category = group.Key,
                    MonthlyTotals = months.Select(month =>
                        group.Where(e => e.Date.HasValue &&
                                         e.Date.Value.ToString("MMMM") == month)
                             .Sum(e => e.Amount ?? 0)
                    ).ToArray()
                }).ToList();

            var barChartDatasets = groupedExpenses.Select(expense => new
            {
                label = expense.Category ?? "Uncategorized",
                data = expense.MonthlyTotals,
                borderWidth = 1
            }).ToList();

            var userBalance = userExpenseManager._balanceMgr.GetActiveBalanceByUserId(userId);
            var totalBalance = userBalance?.TotalBalance ?? 0;
            var totalExpenses = userExpenses.Sum(e => e.Amount ?? 0);

            var pieChartLabels = new[] { "Remaining Balance", "Total Expenses" };
            var pieChartData = new[] { (double)(totalBalance - totalExpenses), (double)totalExpenses };

            //ViewData["Months"] = months;
            //ViewData["BarChartDatasets"] = barChartDatasets;
            //ViewData["PieChartLabels"] = pieChartLabels;
            //ViewData["PieChartData"] = pieChartData;

            // Example 
            ViewData["Months"] = new[] { "January", "February", "March", "April", "May", "June", "July" };
            ViewData["BarChartDatasets"] = new List<object>
            {
                new { label = "Food Expense", data = new[] { 200.0, 300.0, 400.0, 500.0, 600.0, 700.0, 800.0 } },
                new { label = "School Expense", data = new[] { 150.0, 250.0, 350.0, 450.0, 550.0, 650.0, 750.0 } }
            };
            ViewData["PieChartLabels"] = new[] { "Remaining Balance", "Total Expenses" };
            ViewData["PieChartData"] = new[] { 5000.0, 2000.0 }; 


            return View();
        }



        public IActionResult GenerateReport()
        {
            return View();
        }

    }
}
