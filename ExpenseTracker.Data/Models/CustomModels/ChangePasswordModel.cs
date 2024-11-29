using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.CustomModels
{
    public class ChangePasswordModel
    {
        public int? UserId { get; set; }

        [StringLength(252)]
        public string? Password { get; set; }

        [NotMapped]
        public string? NewPassword { get; set; }

        [NotMapped]
        public string? NewConfirmPassword { get; set; }
    }
}
