using ExpenseTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Data.Utils;
using ExpenseTracker.Resources.Constants;

namespace ExpenseTracker.Data.Repository
{
    public class BalanceManager
    {
        private UserExpenseManager _userExpenseManager;
        private BaseRepository<Balance> _balanceRepository;
        private UserManager _userManager;

        public BalanceManager()
        {
            _userExpenseManager = new UserExpenseManager();
            _balanceRepository = new BaseRepository<Balance>();
            _userManager = new UserManager();
        }

        public Balance GetBalanceById(int id)
        {
            return _balanceRepository.Get(id);
        }

        public List<Balance> GetUserBalance(int id)
        {
            var userId = _userManager.GetUserById(id);

            return _balanceRepository._table
                   .Where(m => m.UserId == userId.UserId)
                   .OrderByDescending(m => m.isActive == true)
                   .ToList();
        }

        public Balance GetActiveBalanceByUserId(int userId)
        {
            return _balanceRepository._table
                   .FirstOrDefault(b => b.UserId == userId && b.isActive == true);
        }

        public ErrorCode Create(Balance balance, ref String err) 
        {          
            return _balanceRepository.Create(balance, out err);     
        }

        public ErrorCode Update(Balance balance, ref String err)
        {
            return _balanceRepository.Update(balance.BalanceId, balance, out err);
        }

    }
}
