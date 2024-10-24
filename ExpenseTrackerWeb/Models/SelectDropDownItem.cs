using ExpenseTracker.Data.Repository;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Models
{
    public class SelectDropDownItem
    {
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

        public static List<SelectListItem> SelectListItemMonthYearByUser(int UserId)
        {
            BalanceManager _balanceMgr = new BalanceManager();
            var list = new List<SelectListItem>();

            foreach (var item in _balanceMgr.ListUserBalance(UserId))
            {
                var r = new SelectListItem
                {
                    Text = $"{item.Month?.MonthName} {item.Year?.YearCount}",
                    Value = $"{item.MonthId}-{item.YearId?.ToString()}" 
                };

                list.Add(r); 
            }

            return list; 
        }


        public static List<SelectListItem> SelectListsMonth()
        {
            MonthYearManager _monthYearMgr = new MonthYearManager();
            var list = new List<SelectListItem>();

            foreach (var item in _monthYearMgr.ListMonths())
            {
                var r = new SelectListItem
                {
                    Text = item.MonthName,
                    Value = item.MonthId.ToString()
                };
                list.Add(r);
            }
            return list;
        }

        public static List<SelectListItem> SelectListsYear()
        {
            MonthYearManager _monthYearMgr = new MonthYearManager();
            var list = new List<SelectListItem>();

            foreach (var item in _monthYearMgr.ListYears())
            {
                var r = new SelectListItem
                {
                    Text = item.YearCount,
                    Value = item.YearId.ToString()
                };
                list.Add(r);
            }
            return list;
        }
    }
}
