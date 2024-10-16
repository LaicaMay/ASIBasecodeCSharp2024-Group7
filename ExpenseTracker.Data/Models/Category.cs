using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    public int? UserId { get; set; }

    [StringLength(252)]
    public string? CategoryName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModified { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? SetAmount { get; set; }

    [StringLength(252)]
    public string? Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [ForeignKey("UserId")]
    [InverseProperty("Categories")]
    public virtual User? User { get; set; }
}
