using EmployeeTasksLib.App_Code.DomainModel;
using EmployeeTasksLib.App_Code.Infrastructure.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EmployeeTasksLib.App_Code.Infrastructure
{
    /// <summary>
    /// Summary description for EmployeeTasksDbContext
    /// </summary>
    public class EmployeeTasksDbContext : DbContext, IEmployeeTasksDbContext
    {
        public EmployeeTasksDbContext()
        : base("name=EmployeeTasksDbEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Employee
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.id_Employee); 

            modelBuilder.Entity<Employee>()
                .Property(e => e.id_Employee)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Deleted)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .Property(e => e.DateCreated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.id_Person);

            // Person
            modelBuilder.Entity<Person>()
                .HasKey(p => p.id_Person);

            modelBuilder.Entity<Person>()
                .Property(p => p.id_Person)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Person>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Person>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            // EmployeeTask
            modelBuilder.Entity<EmployeeTask>()
                .HasKey(t => t.id_EmployeeTask);

            modelBuilder.Entity<EmployeeTask>()
                .Property(t => t.id_EmployeeTask)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<EmployeeTask>()
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<EmployeeTask>()
                .Property(t => t.TimeSpent)
                .IsRequired();

            modelBuilder.Entity<EmployeeTask>()
                .Property(t => t.DateCompleted)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<EmployeeTask>()
                .HasRequired(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.id_Employee);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeTask> EmployeeTask { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}