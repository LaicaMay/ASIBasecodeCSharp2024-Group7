using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models.CustomModels
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }

        [StringLength(250)]
        [Unicode(false)]
        public string? Token { get; set; }

        public string? NewPassword { get; set; }

        public string? NewConfirmPassword { get; set; }
    }
}
