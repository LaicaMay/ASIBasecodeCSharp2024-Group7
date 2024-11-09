using ExpenseTracker.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.CustomModels
{
    public class ExpenseSummaryViewModel
    {
        public List<Expense> Expenses { get; set; }
        public List<Month> MonthList { get; set; }

    }

}
