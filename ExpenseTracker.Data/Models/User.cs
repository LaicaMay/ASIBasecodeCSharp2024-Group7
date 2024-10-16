using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(252)]
    public string? Username { get; set; }

    [StringLength(252)]
    public string? Password { get; set; }

    [StringLength(252)]
    public string? ConfirmPassword { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(255)]
    public string? Code { get; set; }

    public int? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateModified { get; set; }

    public bool Agree { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    [InverseProperty("User")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("User")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("User")]
    public virtual ICollection<UserExpense> UserExpenses { get; set; } = new List<UserExpense>();

    [InverseProperty("User")]
    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();
}
