using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Year")]
public partial class Year
{
    [Key]
    public int YearId { get; set; }

    [StringLength(50)]
    public string? YearCount { get; set; }

    [InverseProperty("Year")]
    public virtual ICollection<Balance> Balances { get; set; } = new List<Balance>();
}
