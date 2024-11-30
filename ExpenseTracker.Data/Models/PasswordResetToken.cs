using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("PasswordResetToken")]
public partial class PasswordResetToken
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Token { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PasswordResetTokens")]
    public virtual User? User { get; set; }
}
