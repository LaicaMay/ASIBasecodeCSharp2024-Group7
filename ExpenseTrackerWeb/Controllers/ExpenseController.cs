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
        public IActionResult AddExpense(Expense expense)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }

            expense.UserId = UserId;

            if (_userExpenseMgr.AddExpense(expense, ref ErrorMessage) != ErrorCode.Success)
            {
                ModelState.AddModelError(String.Empty, ErrorMessage);
                return View(expense);
            }
            return RedirectToAction("Overview");
        }

        //[HttpPost]
        //public IActionResult AddExpense(Expense expense)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return BadRequest();
        //    }      

        //    expense.UserId = UserId;
        //    expense.CreatedDate = DateTime.Now;

        //    ViewBag.Category = Utilities.SelectListItemCategoryByUser(UserId);

        //    if (_userExpenseMgr.CreateExpense(expense, ref ErrorMessage) == ErrorCode.Error)
        //    {

        //        ModelState.AddModelError(String.Empty, ErrorMessage);
        //        return View(expense);
        //    }

        //    var userExpn = new UserExpense()
        //    {
        //        UserId = expense.UserId,
        //        CategoryId = expense.CategoryId,
        //        ExpenseId = expense.ExpenseId

        //    };

        //    if (_userExpenseMgr.CreateUserExpense(userExpn, ref ErrorMessage) == ErrorCode.Error)
        //    {
        //        ModelState.AddModelError(String.Empty, ErrorMessage);
        //        return View(expense);
        //    }

        //    return RedirectToAction("Overview");      
        //}

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

        //[HttpGet]
        //public JsonResult CategoryGetById(int? id)
        //{
        //    var category = _userCategoryMgr.GetCategoryById(id);
        //    return Json(category);
        //}

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
