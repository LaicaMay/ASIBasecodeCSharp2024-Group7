using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("UserInformation")]
public partial class UserInformation
{
    [Key]
    public int UserInfoId { get; set; }

    public int? UserId { get; set; }

    [StringLength(255)]
    public string FirstName { get; set; } = null!;

    [StringLength(255)]
    public string? LastName { get; set; }

    [StringLength(255)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Street { get; set; }

    [StringLength(255)]
    public string? City { get; set; }

    [StringLength(255)]
    public string? ZipCode { get; set; }

    public int? Active { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserInformations")]
    public virtual User? User { get; set; }
}
