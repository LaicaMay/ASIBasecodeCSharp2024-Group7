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

    public int? ExpenseId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalBalance { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? RemainingBalance { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TodayExpense { get; set; }

    [ForeignKey("ExpenseId")]
    [InverseProperty("Balances")]
    public virtual Expense? Expense { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Balances")]
    public virtual User? User { get; set; }
}
