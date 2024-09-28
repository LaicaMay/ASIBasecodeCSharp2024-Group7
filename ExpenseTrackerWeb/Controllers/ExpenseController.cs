using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ExpenseTrackerWeb.Controllers
{
    public class ExpenseController : BaseController
    {


        #region ExpenseManagement
        public IActionResult Expense()
        {
            return View();
        }

        public IActionResult AddExpense()
        {
            return View();
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
            return View();
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
