using DoctorClinicApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorClinicApi.Contexts
{
    public class DoctorClinicContext:DbContext
    {

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
