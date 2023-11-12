using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using System.Web.Helpers;
using BCrypt;

namespace DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "Admin",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("12345", 13),
                    Created = DateTime.Now,
                    Role = "Admin"
                }
            );

            modelBuilder.Entity<Salary>(entity =>
                 {
                     entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");
                     entity.Property(e => e.Bonuses).HasColumnType("decimal(18, 2)");
                     entity.Property(e => e.Deductions).HasColumnType("decimal(18, 2)");
                     entity.Property(e => e.TotalSalary).HasColumnType("decimal(18, 2)");
                 });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


    }
}
