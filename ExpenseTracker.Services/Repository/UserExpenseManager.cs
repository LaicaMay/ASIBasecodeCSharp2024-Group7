using ExpenseTracker.Data.Models;
using ExpenseTracker.Resources.Constants;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services.Repository
{
    public class UserExpenseManager
    {
        private readonly UserManager _userMgr;
        private readonly BalanceManager _balanceMgr;
        private readonly BaseRepository<Expense> _expense;
        private readonly BaseRepository<VwUsersExpensesView> _vwExpense;
        private readonly BaseRepository<UserExpense> _userExpense;
        private readonly UserCategoryManager _userCategoryMgr;

        public UserExpenseManager()
        {
            _userMgr = new UserManager();
            _expense = new BaseRepository<Expense>();
            _vwExpense = new BaseRepository<VwUsersExpensesView>();
            _userExpense = new BaseRepository<UserExpense>();
            _balanceMgr = new BalanceManager();
            _userCategoryMgr = new UserCategoryManager();
        }

        public List<Expense> ListUserExpense(int userId)
        {
            var user = _userMgr.GetUserById(userId);

            return _expense._table
                .Include(e => e.Category)
                .Where(m => m.UserId == user.UserId)
                .OrderByDescending(m => m.ExpenseId)
                .ToList();
        }

        //public List<Expense> ListBarUserExpense(int userId)
        //{
        //    var user = _userMgr.GetUserById(userId);

        //    return _expense._table
        //        .Include(e => e.Category)
        //        .Where(m => m.UserId == user.UserId)
        //        .ToList();
        //}

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
            var existCategory = _userCategoryMgr.GetCategoryById(expn.CategoryId);
            decimal? totalAmount = 0;
            expn.CreatedDate = DateTime.Now;

            if (expn.Amount > userBalance.RemainingBalance)
            {
                err = "Insufficient Remaining Balance. Please try again..";
                return ErrorCode.Error;
            }

            if (userBalance.TotalBalance == 0 || userBalance.TotalBalance == null || userBalance.isActive == false)
            {
                err = "You do not have active balance.";
                return ErrorCode.Error;
            }

            if (_expense.Create(expn, out err) != ErrorCode.Success)
            {
                err = "Error creating Expense";
                return ErrorCode.Error;
            }

            if (expn.StartDate == null && expn.EndDate == null)
            {
                existCategory.TotalAmount = (existCategory.TotalAmount ?? 0) + expn.Amount;
            }

            if (expn.StartDate != null && expn.EndDate != null)
            {
                DateOnly startDate = expn.StartDate ?? DateOnly.MinValue;
                DateOnly endDate = expn.EndDate ?? DateOnly.MaxValue;

                var daysOfWeek = expn.DaysOfWeek.Split(',').Select(day => day.Trim()).ToList();

                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (daysOfWeek.Contains(date.DayOfWeek.ToString()))
                    {
                        totalAmount += expn.Amount;
           
                    }
                }

                existCategory.TotalAmount = (existCategory.TotalAmount ?? 0) + totalAmount;
                var existExpense = GetExpenseById(expn.ExpenseId);
                existExpense.Amount = totalAmount;
                if (_expense.Update(existExpense.ExpenseId, existExpense, out err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }
            }

            if (userBalance == null)
            {
                err = "userBalance is Null";
                return ErrorCode.Error;
            }

            if (userBalance.RemainingBalance == null)
            {
                userBalance.RemainingBalance = userBalance.TotalBalance;
            }

            if (!userBalance.TotalBalance.HasValue || userBalance.RemainingBalance <= 0)
            {
                err = "Insufficient balance.";
                return ErrorCode.Error;
            }

            if (totalAmount == 0 || totalAmount == null)
            {
                userBalance.RemainingBalance -= expn.Amount;
            }

            if (totalAmount != 0)
            {
                userBalance.RemainingBalance -= totalAmount;
            }


            if (userBalance.RemainingBalance < 0)
            {
                err = "Expense exceeds remaining balance.";
                return ErrorCode.Error;
            }

            if (_userCategoryMgr.UpdateCategory(existCategory, ref err) != ErrorCode.Success)
            {
                return ErrorCode.Error;
            }

            if (_balanceMgr.UpdateBalance(userBalance, ref err) != ErrorCode.Success)
            {
                err = "Error Updating Balance";
                return ErrorCode.Error;
            }

            return ErrorCode.Success;
        }

        public ErrorCode Update(Expense expn, ref String err)
        {
            decimal? newLessAmount = 0;
            decimal? updatedBalance = 0;
            decimal? updatedTotalAmount = 0;
            decimal? newTotalAmount = 0;
            decimal? newTotalLessAmount = 0;
            var existingExpense = GetExpenseById(expn.ExpenseId);
            var existBal = _balanceMgr.GetActiveBalanceByUserId(existingExpense.UserId);


            if (existingExpense == null)
            {
                return ErrorCode.Error;
            }

            if (expn.Amount < existingExpense.Amount)
            {
                var existCategory = _userCategoryMgr.GetCategoryById(existingExpense.CategoryId);
                newLessAmount = existingExpense.Amount - expn.Amount;
                updatedBalance = existBal.RemainingBalance + newLessAmount;
                existBal.RemainingBalance = updatedBalance;
                if (_balanceMgr.UpdateBalance(existBal, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }

                updatedTotalAmount = existCategory.TotalAmount - newLessAmount;
                existCategory.TotalAmount = updatedTotalAmount;
                if (_userCategoryMgr.UpdateCategory(existCategory, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }
            }

            if (expn.Amount > existingExpense.Amount)
            {
                var existCategory = _userCategoryMgr.GetCategoryById(existingExpense.CategoryId);
                newLessAmount = existingExpense.Amount - expn.Amount;
                updatedBalance = existBal.RemainingBalance + newLessAmount;
                existBal.RemainingBalance = updatedBalance;
                if (_balanceMgr.UpdateBalance(existBal, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }

                updatedTotalAmount = existCategory.TotalAmount - newLessAmount;
                existCategory.TotalAmount = updatedTotalAmount;
                if (_userCategoryMgr.UpdateCategory(existCategory, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }
            }


            existingExpense.ExpenseName = expn.ExpenseName;
            existingExpense.Date = expn.Date;
            existingExpense.Amount = expn.Amount;

            if (existingExpense.CategoryId != expn.CategoryId)
            {
                var updatedCategory = _userCategoryMgr.GetCategoryById(existingExpense.CategoryId);
                newTotalLessAmount = updatedCategory.TotalAmount - existingExpense.Amount;
                updatedCategory.TotalAmount = newTotalLessAmount;

                if (_userCategoryMgr.UpdateCategory(updatedCategory, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }


                var existNewCategory = _userCategoryMgr.GetCategoryById(expn.CategoryId);
                newTotalAmount = existNewCategory.TotalAmount + existingExpense.Amount;
                existNewCategory.TotalAmount = newTotalAmount;

                if (_userCategoryMgr.UpdateCategory(existNewCategory, ref err) != ErrorCode.Success)
                {
                    return ErrorCode.Error;
                }


            }

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
