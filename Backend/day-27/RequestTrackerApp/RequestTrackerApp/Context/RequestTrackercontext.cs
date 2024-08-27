using Microsoft.EntityFrameworkCore;
using RequestTrackerApp.Model;

namespace RequestTrackerApp.Context
{
    public class RequestTrackercontext: DbContext
    {


        public RequestTrackercontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<RequestSolution> RequestSolution { get; set; }
        public DbSet<SolutionResposnse> SolutionResposnse { get; set; }
        public DbSet<SolutionFeedback> SolutionFeedback { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requests>().HasData(
         new Requests
         {
             RequestNumber = 1,
             RequestMessage = "Initial request by system",
             RequestDate = DateTime.Now,
             RequestRaisedBy = 6 // Admin/System ID
         },
         new Requests
         {
             RequestNumber = 2,
             RequestMessage = "Second request by system",
             RequestDate = DateTime.Now,
             RequestRaisedBy = 1 // Admin/System ID
         }
     );

            modelBuilder.Entity<Requests>().HasKey(r => r.RequestNumber);

            modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

            modelBuilder.Entity<Requests>()
               .HasOne(r => r.RaisedByEmployee)
               .WithMany(e => e.RequestsRaised)
               .HasForeignKey(r => r.RequestRaisedBy)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            modelBuilder.Entity<Requests>()
               .HasOne(r => r.RequestClosedByEmployee)
               .WithMany(e => e.RequestsClosed)
               .HasForeignKey(r => r.RequestClosedBy)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RequestSolution>()
                .HasOne(rs => rs.RequestRaised)
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
