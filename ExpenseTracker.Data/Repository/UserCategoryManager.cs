using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Resources.Constants;

namespace ExpenseTracker.Data.Repository
{
    public class UserCategoryManager
    {
        private readonly BaseRepository<Category> _category;
        private readonly UserManager _userMgr;

        public UserCategoryManager()
        {
            _category = new BaseRepository<Category>();
            _userMgr = new UserManager();
        }

        public Category GetCategoryById(int? id)
        {
            return _category.Get(id);
        }

        public List<Category> ListCategory(int userId)
        {
            var user = _userMgr.GetUserById(userId);
            return _category._table.Where(m => m.UserId == user.UserId).ToList();
        }

        public ErrorCode CreateCategory(Category category, ref String err)
        {
            return _category.Create(category, out err);
        }

        public ErrorCode UpdateCategory(Category category, ref String err)
        {
            return _category.Update(category.CategoryId, category, out err);
        }

        public ErrorCode DeleteCategory(int? id, ref String err)
        {
            return _category.Delete(id, out err);
        }
    }
}
