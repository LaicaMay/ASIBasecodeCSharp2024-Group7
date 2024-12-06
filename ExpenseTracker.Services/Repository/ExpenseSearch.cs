using ExpenseTracker.Data.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services.Repository
{
    public class ExpenseSearch
    {
        private readonly ExpenseTrackerDbContext _db;

        public ExpenseSearch()
        {
            _db = new ExpenseTrackerDbContext();
        }

        public List<Expense> SearchExpenses(string Search)
        {
            DateTime searchDate;
            bool isDate = DateTime.TryParse(Search, out searchDate);

            var expenses = _db.Expenses
                .Include(e => e.Category)
                .Where(e => e.ExpenseName.Contains(Search) ||
                             e.Description.Contains(Search) ||
                             e.Amount.ToString().Contains(Search) ||
                             (e.Category != null && e.Category.CategoryName.Contains(Search)) ||
                             (isDate && e.Date.HasValue && e.Date.Value == DateOnly.FromDateTime(searchDate)))
                .ToList();

            return expenses;
        }

        public List<Category> SearchCategory(string Search)
        {
            DateTime searchDate;
            bool isDate = DateTime.TryParse(Search, out searchDate);

            var category = _db.Categories
                .Where(e => e.CategoryName.Contains(Search) ||
                             e.Description.Contains(Search) ||
                             e.TotalAmount.ToString().Contains(Search))
                .ToList();

            return category;
        }
    }
}
