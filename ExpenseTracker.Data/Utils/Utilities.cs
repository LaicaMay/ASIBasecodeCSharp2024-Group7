using ExpenseTracker.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTracker.Data.Utils
{
    public class Utilities
    {
        public static int code
        {
            get
            {
                Random r = new Random();
                return r.Next(100000, 999999);
            }
        }

        public static List<SelectListItem> SelectListItemCategoryByUser(int UserId)
        {
            UserCategoryManager _userCategoryMgr = new UserCategoryManager();
            var list = new List<SelectListItem>();

            foreach (var item in _userCategoryMgr.ListCategory(UserId))
            {
                var r = new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.CategoryId.ToString()
                };
                list.Add(r);
            }

            return list;
        }
    }
}
