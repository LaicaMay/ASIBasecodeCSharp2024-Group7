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

        public ErrorCode Add(Expense expn, ref String err)
        {
            var userBalance = _balanceMgr.GetActiveBalanceByUserId(expn.UserId);
            decimal? totalAmount = 0;
            expn.CreatedDate = DateTime.Now;

            if (_expense.Create(expn, out err) != ErrorCode.Success)
            {
                err = "Error creating Expense";
                return ErrorCode.Error;
            }

            if (expn.StartDate != null && expn.EndDate != null)
            {
                DateOnly startDate = expn.StartDate ?? DateOnly.MinValue;
                DateOnly endDate = expn.EndDate ?? DateOnly.MaxValue;

                var daysOfWeek = expn.DaysOfWeek?.Split(',')
                                    .Where(day => !string.IsNullOrEmpty(day))
                                    .Select(day => day.Trim())
                                    .ToList();

                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (daysOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        totalAmount += expn.Amount;
                    }
                }
                Console.WriteLine("Total Amount for Selected Days: " + totalAmount);
            }

            if (userBalance == null)
            {
                err = "userBalance is Null";
                return ErrorCode.Error;
            }

            userBalance.RemainingBalance ??= userBalance.TotalBalance;

            if (!userBalance.TotalBalance.HasValue || userBalance.RemainingBalance <= 0)
            {
                err = "Insufficient balance.";
                return ErrorCode.Error;
            }

            if (totalAmount > userBalance.RemainingBalance)
            {
                err = "Expense exceeds remaining balance.";
                return ErrorCode.Error;
            }

            userBalance.RemainingBalance -= totalAmount ?? expn.Amount;

            if (_balanceMgr.UpdateBalance(userBalance, ref err) != ErrorCode.Success)
            {
                err = "Error Updating Balance";
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
