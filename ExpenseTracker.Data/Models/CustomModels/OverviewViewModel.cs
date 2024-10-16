using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.CustomModels
{
    public class OverviewViewModel
    {
        public List<VwUsersExpensesView> ExpensesView { get; set; }
        public List<Expense> UserExpenses { get; set; }
    }
}
