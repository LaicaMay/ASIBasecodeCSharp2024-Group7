using ExpenseTracker.Services.Repository;
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

        public static List<CustomSelectListItem> SelectListItemMonthYearByUser(int UserId)
        {
            BalanceManager _balanceMgr = new BalanceManager();
            var list = new List<CustomSelectListItem>();

            foreach (var item in _balanceMgr.ListUserBalance(UserId))
            {
                var r = new CustomSelectListItem
                {
                    Text = $"{item.Month?.MonthName} {item.Year?.YearCount}",
                    Value = $"{item.MonthId}-{item.YearId?.ToString()}",
                    RemainingBalance = item.RemainingBalance,
                    TotalBalance = item.TotalBalance,
                    BalanceId = item.BalanceId
                };

                list.Add(r);
            }

            return list;
        }
        //public static List<SelectListItem> SelectListItemMonthYearByUser(int UserId)
        //{
        //    BalanceManager _balanceMgr = new BalanceManager();
        //    var list = new List<SelectListItem>();

        //    foreach (var item in _balanceMgr.ListUserBalance(UserId))
        //    {
        //        var r = new SelectListItem
        //        {
        //            Text = $"{item.Month?.MonthName} {item.Year?.YearCount}",
        //            Value = $"{item.MonthId}-{item.YearId?.ToString()}" ,
        //        };

        //        list.Add(r); 
        //    }

        //    return list; 
        //}
        public static List<SelectListItem> SelectListsMonth()
        {
            MonthYearManager _monthYearMgr = new MonthYearManager();
            var list = new List<SelectListItem>();

            // Fetch months and filter out the unwanted combination
            foreach (var item in _monthYearMgr.ListMonths())
            {
                // Assuming MonthId is the identifier and you want to exclude the combination with YearId == 13
                if (!(item.MonthId == 13 && _monthYearMgr.ListYears().Any(y => y.YearId == 13)))
                {
                    var r = new SelectListItem
                    {
                        Text = item.MonthName,
                        Value = item.MonthId.ToString()
                    };
                    list.Add(r);
                }
            }
            return list;
        }

        public static List<SelectListItem> SelectListsYear()
        {
            MonthYearManager _monthYearMgr = new MonthYearManager();
            var list = new List<SelectListItem>();

            // Fetch years and filter out the unwanted combination
            foreach (var item in _monthYearMgr.ListYears())
            {
                // Assuming YearId is the identifier and you want to exclude the combination with MonthId == 13
                if (!(item.YearId == 13 && _monthYearMgr.ListMonths().Any(m => m.MonthId == 13)))
                {
                    var r = new SelectListItem
                    {
                        Text = item.YearCount,
                        Value = item.YearId.ToString()
                    };
                    list.Add(r);
                }
            }
            return list;
        }

    }
}
