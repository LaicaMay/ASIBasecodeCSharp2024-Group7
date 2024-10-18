using ExpenseTracker.Data.Data;
using ExpenseTracker.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Tokenizer.Symbols;

namespace ExpenseTracker.Data.Repository
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







    }
}
