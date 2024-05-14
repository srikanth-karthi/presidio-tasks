using DoctorClinicApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorClinicApi.Contexts
{
    public class DoctorClinicContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CUTTLDR\SQLDB;Integrated Security=true;TrustServerCertificate=true;Initial Catalog=dbclinic;");
        }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(

                new Doctor() { Doctorid = 1, Doctorname = "srikanth", Experience = 2,Specification="Mbbs" },
                                new Doctor() { Doctorid = 3, Doctorname = "Sugan", Experience = 2, Specification = "Mbbs" },
                 new Doctor() { Doctorid = 2, Doctorname = "Barath", Experience = 5, Specification = "Mbbs,md" }
                ); 


        }


    }
}
