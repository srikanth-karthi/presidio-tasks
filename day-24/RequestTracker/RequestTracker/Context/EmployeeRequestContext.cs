
using EmployeeRequestTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRequestTrackerAPI.Contexts
{
      public class RequestTrackerContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CUTTLDR\SQLDB;Integrated Security=true;TrustServerCertificate=true;Initial Catalog=dbEmployeeTrackertest;");
        }

        public DbSet<Employee>  Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 101, Name = "Ramu", DateOfBirth = new DateTime(2000,2,12), Phone = "9876543321", Image = "" },
                new Employee() { Id = 102, Name = "Somu", DateOfBirth = new DateTime(2002,3,24), Phone = "9988776655", Image = "" }
                );
        }
    }
}
