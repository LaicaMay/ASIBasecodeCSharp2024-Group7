using System;
using System.Collections.Generic;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Data;

public partial class ExpenseTrackerDbContext : DbContext
{
    public ExpenseTrackerDbContext()
    {
    }

    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localDB)\\MSSQLLocalDB; Initial Catalog=ExpenseTrackerDB; Trusted_Connection=Yes; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Categories).HasConstraintName("FK_Category_User");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Expenses).HasConstraintName("FK_Expense_Category");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses).HasConstraintName("FK_Expense_User");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Reports).HasConstraintName("FK_Report_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).IsFixedLength();
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserInformations).HasConstraintName("FK_UserInformation_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
