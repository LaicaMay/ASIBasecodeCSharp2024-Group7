using System.Web.Mvc;

namespace ExpenseTrackerWeb.Models
{
    public class CustomSelectListItem : SelectListItem
    {
        public int? BalanceId { get; set; }
        public decimal? RemainingBalance { get; set; }
        public decimal? TotalBalance { get; set; }
    }
}
    