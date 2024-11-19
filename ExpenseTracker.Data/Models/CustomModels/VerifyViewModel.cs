using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.CustomModels
{
    public class VerifyViewModel
    {
        [Required]
        [StringLength(255)]
        public string? ConfirmCode { get; set; }
    }
}
