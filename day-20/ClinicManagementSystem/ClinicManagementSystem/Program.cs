using ClinicManagementSystem_Library;
using System;
using ClinicManagementSystem_Library.Model;
namespace ClinicManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
      
            Console.WriteLine("Adding doctors:");
            Console.WriteLine(ClinicService.AddDoctor(new Doctor { Name = "Dr. John Doe", Specialization = "General Physician" }));
            Console.WriteLine(ClinicService.AddDoctor(new Doctor { Name = "Dr. Jane Smith", Specialization = "Pediatrician" }));
            Console.WriteLine(ClinicService.AddDoctor(new Doctor { Name = "Dr. Jane Smith", Specialization = "Pediatrician" }));


            Console.WriteLine("\nGetting a doctor:");
            Console.WriteLine(ClinicService.GetDoctor(1));

          
            Console.WriteLine("\nUpdating a doctor:");
            Doctor updatedDoctor = ClinicService.GetDoctor(1);
            updatedDoctor.Specialization = "Cardiologist";
            Console.WriteLine(ClinicService.UpdateDoctor(updatedDoctor));
            Console.WriteLine(ClinicService.GetDoctor(1));

  
            Console.WriteLine("\nDeleting a doctor:");

            Console.WriteLine(ClinicService.DeleteDoctor(2));
            Console.WriteLine(ClinicService.GetDoctor(1));



            Console.WriteLine("\nAdding patients:");
            Console.WriteLine(ClinicService.AddPatient(new Patient { Name = "Alice", PhoneNumber = "1234567890" }));
            Console.WriteLine(ClinicService.AddPatient(new Patient { Name = "Bob", PhoneNumber = "9876543210" }));

            Console.WriteLine("\nGetting a patient:");
            Console.WriteLine(ClinicService.GetPatient(1));

   
            Console.WriteLine("\nUpdating a patient:");
            Patient updatedPatient = ClinicService.GetPatient(1);
            updatedPatient.PhoneNumber = "5555555555";
            Console.WriteLine(ClinicService.UpdatePatient(updatedPatient));



            Console.WriteLine("\nDeleting a patient:");
            Console.WriteLine(ClinicService.DeletePatient(2));



            Console.WriteLine("\nAdding appointments:");
            Console.WriteLine(ClinicService.AddAppointment(new Appointment { Date = DateTime.Now, DoctorId = 1, PatientId = 1 }));
            Console.WriteLine(ClinicService.AddAppointment(new Appointment { Date = DateTime.Now.AddDays(1), DoctorId = 1, PatientId = 1 }));


            Console.WriteLine("\nGetting an appointment:");
            Console.WriteLine(ClinicService.GetAppointment(1));



            Console.WriteLine("\nUpdating an appointment:");
            Appointment updatedAppointment = ClinicService.GetAppointment(1);
            updatedAppointment.DoctorId = 3;
            Console.WriteLine(ClinicService.UpdateAppointment(updatedAppointment));
            Console.WriteLine(ClinicService.GetAppointment(1));



            Console.WriteLine("\nDeleting an appointment:");
            Console.WriteLine(ClinicService.DeleteAppointment(1));
            Console.WriteLine(ClinicService.GetAppointment(2));
        }
    }
}
