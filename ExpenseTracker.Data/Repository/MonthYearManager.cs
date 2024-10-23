using ExpenseTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Repository
{
    public class MonthYearManager
    {
        private BaseRepository<Month> _month;
        private BaseRepository<Year> _year;

        public MonthYearManager() 
        {
            _month = new BaseRepository<Month>();
            _year = new BaseRepository<Year>();
        }

        public Month GetMonthById(int id)
        {
            return _month.Get(id);
        }

        public List<Month> ListMonths()
        {
            return _month.GetAll();
        }

        public Year GetYearById(int id)
        {
            return _year.Get(id);
        }

        public List<Year> ListYears()
        {
            return _year.GetAll();
        }
    }
}
