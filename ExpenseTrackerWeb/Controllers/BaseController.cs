using ExpenseTracker.Data.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerWeb.Controllers
{
    public class BaseController : Controller
    {
        public String ErrorMessage;
        public ExpenseTrackerDbContext _db;
        public BaseRepository<User> _userRepo;
        public UserManager _userManager;
        public UserExpenseManager _userExpenseMgr;
        public UserCategoryManager _userCategoryMgr;
        public BaseRepository<Category> _categoryRepo;
        public BaseRepository<Expense> _expenseRepo;
        public BaseRepository<Report> _reportRepo;
        public BaseRepository<UserExpense> _userExp;
        public BaseRepository<VwUsersExpensesView> _userExpensesview;
        public int UserId { get { var userId = Convert.ToInt32(User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value); return userId; } }
        
        public BaseController()
        {
            ErrorMessage = String.Empty;
            _db = new ExpenseTrackerDbContext();
            _userRepo = new BaseRepository<User>();
            _userManager = new UserManager();
            _userExpenseMgr = new UserExpenseManager();
            _userCategoryMgr = new UserCategoryManager();
            _categoryRepo = new BaseRepository<Category>();
            _expenseRepo = new BaseRepository<Expense>();
            _reportRepo = new BaseRepository<Report>();
            _userExpensesview = new BaseRepository<VwUsersExpensesView>();
            _userExp = new BaseRepository<UserExpense>();
        }
    }
}
