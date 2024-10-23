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

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Month> Months { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserExpense> UserExpenses { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<VwUsersExpensesView> VwUsersExpensesViews { get; set; }

    public virtual DbSet<Year> Years { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-FO9D3C2\\SQLEXPRESS;Initial Catalog=ExpenseTrackerDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>(entity =>
        {
            entity.HasOne(d => d.Month).WithMany(p => p.Balances).HasConstraintName("FK_Balance_Month");

            entity.HasOne(d => d.User).WithMany(p => p.Balances).HasConstraintName("FK_Balance_User");

            entity.HasOne(d => d.Year).WithMany(p => p.Balances).HasConstraintName("FK_Balance_Year");
        });

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

        modelBuilder.Entity<UserExpense>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserExpenses).HasConstraintName("FK_UserExpense_User");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserInformations).HasConstraintName("FK_UserInformation_User");
        });

        modelBuilder.Entity<VwUsersExpensesView>(entity =>
        {
            entity.ToView("vw_usersExpensesView");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
