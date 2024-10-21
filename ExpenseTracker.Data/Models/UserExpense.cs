using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("UserExpense")]
public partial class UserExpense
{
    [Key]
    public int UserExpenseId { get; set; }

    public int? ExpenseId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserExpenses")]
    public virtual User? User { get; set; }
}
