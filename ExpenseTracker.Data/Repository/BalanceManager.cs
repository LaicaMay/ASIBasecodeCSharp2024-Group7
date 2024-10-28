using ExpenseTracker.Data.Models;
using ExpenseTracker.Resources.Constants;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Repository
{
    public class BalanceManager
    {
        private BaseRepository<Balance> _balanceRepository;
        private UserManager _userManager;
        private BaseRepository<Expense> _expnRepo;

        public BalanceManager()
        {
            _balanceRepository = new BaseRepository<Balance>();
            _userManager = new UserManager();
            _expnRepo = new BaseRepository<Expense>();
        }

        public Balance GetBalanceById(int id)
        {
            return _balanceRepository.Get(id);
        }

        public List<Balance> ListUserBalance(int id)
        {
            var userId = _userManager.GetUserById(id);

            return _balanceRepository._table
                   .Include(e => e.Month)
                   .Include(e => e.Year)
                   .Where(m => m.UserId == userId.UserId)
                   .OrderByDescending(m => m.isActive == true)
                   .ToList();
        }

        public Balance GetUserBalanceByUserId(int userId)
        {
            return _balanceRepository._table.Where(m => m.UserId == userId).FirstOrDefault();
        }

        public Balance GetActiveBalanceByUserId(int? activUserId)
        {
            return _balanceRepository._table
                   .FirstOrDefault(b => b.UserId == activUserId && b.isActive == true);
        }

        public ErrorCode DefaultBalance (Balance balance, ref String err)
        {
            if (balance == null)
            {
                err = "Balance object cannot be null.";
                return ErrorCode.Error;
            }

            balance.TotalBalance = null;
            balance.MonthId = 13;
            balance.YearId = 13;
            balance.isActive = true;

            return _balanceRepository.Create(balance, out err);
        }

        public ErrorCode AddBalance(Balance balance, ref String err) 
        {          
            return _balanceRepository.Create(balance, out err);     
        }

        public ErrorCode UpdateBalance(Balance balance, ref String err)
        {
            return _balanceRepository.Update(balance.BalanceId, balance, out err);
        }

     
    }
}
