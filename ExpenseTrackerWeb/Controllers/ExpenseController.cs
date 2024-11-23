using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Resources.Constants;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Data.Models.CustomModels;
using ExpenseTrackerWeb.Models;
using ExpenseTracker.Data.Repository;

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

            if (_userExpenseMgr.Delete(id, ref ErrorMessage) != ErrorCode.Success)
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
            var months = _monthYearMgr.ListMonths()
                                      .OrderBy(m => m.MonthId)
                                      .Select(m => m.MonthName)
                                      .ToArray();

            var userBalance = _balanceMgr.ListUserBalance(UserId);
            var userExpenses = _userExpenseMgr.ListUserExpense(UserId);
            var userCategories = _userCategoryMgr.ListCategory(UserId);

            var categoryMap = userCategories.ToDictionary(c => c.CategoryId, c => c.CategoryName);

            var categorizedExpenses = new Dictionary<string, decimal[]>();
            foreach (var categoryName in categoryMap.Values)
            {
                categorizedExpenses[categoryName] = new decimal[12];
            }

            foreach (var expense in userExpenses)
            {
                if (expense.CategoryId.HasValue && expense.StartDate.HasValue)
                {
                    var categoryId = expense.CategoryId.Value;
                    var categoryName = categoryMap.ContainsKey(categoryId) ? categoryMap[categoryId] : "Uncategorized";

                    var monthIndex = expense.StartDate.Value.Month;
                    if (monthIndex >= 0 && monthIndex < 12)
                    {
                        var amount = expense.Amount ?? 0m;
                        categorizedExpenses[categoryName][monthIndex] += amount;
                    }
                }
            }

            var barChartDatasets = categorizedExpenses.Select(entry => new
            {
                label = entry.Key, 
                data = entry.Value.Select(value => (double)value).ToArray(),
                borderWidth = 1
            }).ToList();

            var totalBalance = userBalance.Sum(e => e.TotalBalance ?? 0m);
            var totalExpenses = userExpenses.Sum(e => e.Amount ?? 0m);

            var pieChartLabels = new[] { "Total Balance", "Total Expenses" };
            var pieChartData = new[] { (double)totalBalance, (double)totalExpenses };

            //var sampleTopExpenses = new[]
            //        {
            //    new { CategoryName = "Food", ExpenseName = "Lunch", Amount = 300m },
            //    new { CategoryName = "Transportation", ExpenseName = "Gas", Amount = 200m },
            //    new { CategoryName = "Utilities", ExpenseName = "Electric Bill", Amount = 100m }
            //};

            ViewData["Months"] = months;
            ViewData["BarChartDatasets"] = barChartDatasets;
            ViewData["PieChartLabels"] = pieChartLabels;
            ViewData["PieChartData"] = pieChartData;
            //ViewData["Top3Expenses"] = top3Expenses;  
            //ViewData["Top3Expenses"] = sampleTopExpenses;

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
