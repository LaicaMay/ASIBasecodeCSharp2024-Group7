using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Models;

[Table("Report")]
public partial class Report
{
    [Key]
    public int ReportId { get; set; }

    public int? UserId { get; set; }

    [StringLength(252)]
    public string? ReportType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? GeneratedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reports")]
    public virtual User? User { get; set; }
}
