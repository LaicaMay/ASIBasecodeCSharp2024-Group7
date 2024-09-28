using ExpenseTracker.Data.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class BaseController : Controller
    {
        public String ErrorMessage;
        public ExpenseTrackerDbContext _db;
        public BaseRepository<User> _userRepo;
        public UserManager _userManager;
        public BaseRepository<Category> _categoryRepo;
        public BaseRepository<Expense> _expenseRepo;
        public BaseRepository<Report> _reportRepo;
     
        public BaseController()
        {
            ErrorMessage = String.Empty;
            _db = new ExpenseTrackerDbContext();
            _userRepo = new BaseRepository<User>();
            _userManager = new UserManager();  
            _categoryRepo = new BaseRepository<Category>();
            _expenseRepo = new BaseRepository<Expense>();
            _reportRepo = new BaseRepository<Report>();
        }
    }
}
