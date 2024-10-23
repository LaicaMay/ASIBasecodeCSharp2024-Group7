using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Balance")]
public partial class Balance
{
    [Key]
    public int BalanceId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalBalance { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? RemainingBalance { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TodayExpense { get; set; }

    public bool? isActive { get; set; }
    public int? MonthId { get; set; }

    public int? YearId { get; set; }

    [ForeignKey("MonthId")]
    [InverseProperty("Balances")]
    public virtual Month? Month { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Balances")]
    public virtual User? User { get; set; }

    [ForeignKey("YearId")]
    [InverseProperty("Balances")]
    public virtual Year? Year { get; set; }
}
