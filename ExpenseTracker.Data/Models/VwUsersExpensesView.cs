using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Keyless]
public partial class VwUsersExpensesView
{
    public int CategoryId { get; set; }

    public int UserId { get; set; }

    public int ExpenseId { get; set; }

    [StringLength(252)]
    public string? CategoryName { get; set; }

    [StringLength(250)]
    public string? ExpenseName { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [StringLength(252)]
    public string? Description { get; set; }
}
