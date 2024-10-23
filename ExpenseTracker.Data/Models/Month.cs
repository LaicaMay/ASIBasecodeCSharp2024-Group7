using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Month")]
public partial class Month
{
    [Key]
    public int MonthId { get; set; }

    [StringLength(50)]
    public string? MonthName { get; set; }

    [InverseProperty("Month")]
    public virtual ICollection<Balance> Balances { get; set; } = new List<Balance>();
}
