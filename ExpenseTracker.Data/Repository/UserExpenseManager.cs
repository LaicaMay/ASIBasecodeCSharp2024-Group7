using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Resources.Constants;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Repository
{
    public class UserExpenseManager
    {
        private readonly UserManager _userMgr;
        private readonly BaseRepository<Expense> _expense;
        private readonly BaseRepository<VwUsersExpensesView> _vwExpense;
        private readonly BaseRepository<UserExpense> _userExpense;


        public UserExpenseManager()
        {
            _userMgr = new UserManager();
            _expense = new BaseRepository<Expense>();
            _vwExpense = new BaseRepository<VwUsersExpensesView>();
            _userExpense = new BaseRepository<UserExpense>();
        }

        public List<Expense> ListUserExpense(int userId)
        {
            var user = _userMgr.GetUserById(userId);
            return _expense._table.Where(m => m.UserId == user.UserId).ToList();
        }

        public List<VwUsersExpensesView> ListExpense(int userId)
        {
            var user = _userMgr.GetUserById(userId);
            return _vwExpense._table.Where(m => m.UserId == user.UserId).ToList();
        }

        public UserExpense GetUserExpenseById(int? id)
        {
            return _userExpense.Get(id);
        }

        public Expense GetExpenseById(int? id)
        {
            return _expense.Get(id);
        }

        public ErrorCode AddExpense(Expense expn, ref String err)
        {
            
            expn.CreatedDate = DateTime.Now;

            if (_expense.Create(expn, out err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }

            var userExpn = new UserExpense()
            {
                UserId = expn.UserId,
                CategoryId = expn.CategoryId,
                ExpenseId = expn.ExpenseId

            };

            if (_userExpense.Create(userExpn, out err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }

            return ErrorCode.Success;
        }

        public ErrorCode CreateUserExpense(UserExpense expn, ref String err)
        {
            return _userExpense.Create(expn, out err);
        }

        public ErrorCode CreateExpense(Expense expn, ref String err)
        {
            return _expense.Create(expn, out err);
        }

        public ErrorCode UpdateExpense(Expense expn, ref String err)
        {
            return _expense.Update(expn.ExpenseId, expn, out err);
        }

        public ErrorCode DeleteExpense(int id, ref String err)
        {
            return _expense.Delete(id, out err);
        }
    }
}
