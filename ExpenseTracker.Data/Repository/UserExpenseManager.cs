using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.CustomModels;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Resources.Constants;
using Microsoft.EntityFrameworkCore;
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
        private readonly BalanceManager _balanceMgr;
        private readonly BaseRepository<Expense> _expense;
        private readonly BaseRepository<VwUsersExpensesView> _vwExpense;
        private readonly BaseRepository<UserExpense> _userExpense;

        public UserExpenseManager()
        {
            _userMgr = new UserManager();
            _expense = new BaseRepository<Expense>();
            _vwExpense = new BaseRepository<VwUsersExpensesView>();
            _userExpense = new BaseRepository<UserExpense>();
            _balanceMgr = new BalanceManager();
        }

        public List<Expense> ListUserExpense(int userId)
        {
            var user = _userMgr.GetUserById(userId);

            return _expense._table
                .Include(e => e.Category) 
                .Where(m => m.UserId == user.UserId)
                .ToList();
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

        public ErrorCode Create(Expense expn, ref String err)
        {

            expn.CreatedDate = DateTime.Now;

            if (_expense.Create(expn, out err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }   

            var userBalance = _balanceMgr.GetActiveBalanceByUserId(expn.UserId);

            if (userBalance == null)
            {
                return ErrorCode.Error;
            }

            userBalance.UpdatedBalance = userBalance.TotalBalance;

            userBalance.UpdatedBalance -= expn.Amount;

            userBalance.RemainingBalance = userBalance.UpdatedBalance;

            if (_balanceMgr.Update(userBalance, ref err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }

            return ErrorCode.Success;
        }

        public ErrorCode Update(Expense expn, ref String err)
        {
            var existingExpense = GetExpenseById(expn.ExpenseId);

            if (existingExpense == null)
            {
                return ErrorCode.Error;
            }

            existingExpense.ExpenseName = expn.ExpenseName;
            existingExpense.Amount = expn.Amount;
            existingExpense.Date = expn.Date;
            existingExpense.CategoryId = expn.CategoryId;
            existingExpense.Description = expn.Description;
            existingExpense.UserId = expn.UserId;
            existingExpense.CreatedDate = expn.CreatedDate;
            existingExpense.DateModified = DateTime.Now;

            if (_expense.Update(expn.ExpenseId, expn, out err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }     

            return ErrorCode.Success;
        }

        public ErrorCode Delete(int id, ref String err)
        {
            return _expense.Delete(id, out err);
        }
    }
}
