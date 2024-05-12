using Microsoft.EntityFrameworkCore;
using Request_Tracker_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class RequestTrackerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CUTTLDR\SQLDB;Integrated Security=true;TrustServerCertificate=true;Initial Catalog=dbEmployeeTrackerCF;");
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestSolution> RequestSolution { get; set; }
        public DbSet<SolutionResposnse> SolutionResposnse { get; set; }
        public DbSet<SolutionFeedback> SolutionFeedback { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 101, Name = "Ramu", Password = "ramu123", Role = "Admin" },
                new Employee { Id = 102, Name = "Somu", Password = "somu123", Role = "Admin" },
                new Employee { Id = 103, Name = "Bimu", Password = "bimu123", Role = "User" }
                );

            modelBuilder.Entity<Request>().HasKey(r => r.RequestNumber);

            modelBuilder.Entity<Request>()
               .HasOne(r => r.RaisedByEmployee)
               .WithMany(e => e.RequestsRaised)
               .HasForeignKey(r => r.RequestRaisedBy)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            modelBuilder.Entity<Request>()
               .HasOne(r => r.RequestClosedByEmployee)
               .WithMany(e => e.RequestsClosed)
               .HasForeignKey(r => r.RequestClosedBy)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RequestSolution>()
                .HasOne(rs=>rs.RequestRaised)
                .WithMany(r => r.SolutionsProvided)
                .HasForeignKey(rs => rs.RequestId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            modelBuilder.Entity<RequestSolution>()
                .HasOne(e => e.SolvedByEmployee)
                  .WithMany(r => r.SolutionsProvided)
                    .HasForeignKey(e => e.SolvedBy)
                   .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();




            modelBuilder.Entity<SolutionFeedback>()
                .HasOne(sf => sf.Request)
                .WithMany(s => s.FeedbackProvided)
                .HasForeignKey(sf => sf.RequestId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


                      modelBuilder.Entity<SolutionFeedback>()
                    .HasOne(sf => sf.FeedbackByEmployee)
                    .WithMany(e => e.FeedbacksGiven)
                    .HasForeignKey(sf => sf.FeedbackBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();





            modelBuilder.Entity<SolutionResposnse>()
                .HasOne(sr => sr.RequestSolution)
                .WithOne(rs => rs.Response)
                .HasForeignKey<SolutionResposnse>(sr => sr.SolutionId) 
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            modelBuilder.Entity<SolutionResposnse>()
                .HasOne(sr => sr.Employee)
                .WithMany(rs => rs.ResponseGiven)
                .HasForeignKey(sr => sr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();




        }



    }
}
