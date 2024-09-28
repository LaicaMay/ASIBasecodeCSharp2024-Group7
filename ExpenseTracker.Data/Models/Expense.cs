using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Expense")]
public partial class Expense
{
    [Key]
    public int ExpenseId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(252)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModified { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Expenses")]
    public virtual Category? Category { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Expenses")]
    public virtual User? User { get; set; }
}
