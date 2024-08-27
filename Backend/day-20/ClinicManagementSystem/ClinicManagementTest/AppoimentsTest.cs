using ClinicManagementSystem_Library.Model;
using ClinicManagementSystem_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementTest
{
    public class AppoimentsTest
    {

        private ICrud<Doctor, int> DoctorRepository;
        private Doctor Doctor, Doctor2;
        private ICrud<Patient, int> PatientRepository;
        private Patient Patient, Patient2;
        private ICrud<Appointment, int> AppointmentRepository;
        private Appointment Appointment;

        [SetUp]
        public void Setup()
        {
            DoctorRepository = new DoctorRepositoryPattern();
            Doctor = new Doctor() { Name = "demo", Specialization = "demo" };
            DoctorRepository.Add(Doctor);
            Doctor2 = new Doctor() { Name = "demo1", Specialization = "demo1" };
            DoctorRepository.Add(Doctor2);


            PatientRepository = new PatientRepositoryPattern();
            Patient = new Patient() { Name = "demo", PhoneNumber = "9345810993" };
            PatientRepository.Add(Patient);
            Patient2 = new Patient() { Name = "demo2", PhoneNumber = "9345810993" };
            PatientRepository.Add(Patient2);

            AppointmentRepository = new AppointmentRepositoryPattern();
            Appointment = new Appointment() { DoctorId = Doctor.DoctorId, PatientId = Patient.PatientId, Date = DateTime.Now };
            AppointmentRepository.Add(Appointment);
        }


        [Test]
        public void AddAppointment_InvalidDoctorId()
        {
            Appointment appointment = new Appointment() { DoctorId = 1000, PatientId = Patient.PatientId, Date = DateTime.Now };
            Assert.Throws<ArgumentException>(() => AppointmentRepository.Add(appointment));
        }

        [Test]
        public void AddAppointment_InvalidPatientId()
        {
            Appointment appointment = new Appointment() { DoctorId = Doctor.DoctorId, PatientId = 1000, Date = DateTime.Now };
            Assert.Throws<ArgumentException>(() => AppointmentRepository.Add(appointment));
        }

        [Test]
        public void DeleteAppointment()
        {
            bool result = AppointmentRepository.Delete(Appointment.AppointmentId);
            Assert.IsTrue(result);
        }

        [Test]
        public void Failed_DeleteAppointment()
        {
            Assert.Throws<KeyNotFoundException>(() => AppointmentRepository.Delete(1000));
        }

        [Test]
        public void GetAppointment()
        {
            Appointment appointment = AppointmentRepository.Get(Appointment.AppointmentId);
            Assert.NotNull(appointment);
            Assert.AreEqual(Appointment.AppointmentId, appointment.AppointmentId);
        }

        [Test]
        public void Failed_GetAppointment()
        {
            Assert.Throws<KeyNotFoundException>(() => AppointmentRepository.Get(1000));
        }

        [Test]
        public void UpdateAppointment()
        {
            Appointment.Date = DateTime.Now.AddDays(1);
            Appointment updatedAppointment = AppointmentRepository.Update(Appointment);
            Assert.AreEqual(Appointment.Date, updatedAppointment.Date);
        }

        [Test]
        public void Failed_UpdateAppointment()
        {
            Appointment appointment = new Appointment() { AppointmentId = 1000, DoctorId = Doctor.DoctorId, PatientId = Patient.PatientId, Date = DateTime.Now };
            Assert.Throws<KeyNotFoundException>(() => AppointmentRepository.Update(appointment));
        }

        [Test]
        public void UpdateAppointment_InvalidDoctorId()
        {
            Appointment appointment = new Appointment() { AppointmentId = Appointment.AppointmentId, DoctorId = 1000, PatientId = Patient.PatientId, Date = DateTime.Now };
            Assert.Throws<ArgumentException>(() => AppointmentRepository.Update(appointment));
        }

        [Test]
        public void UpdateAppointment_InvalidPatientId()
        {
            Appointment appointment = new Appointment() { AppointmentId = Appointment.AppointmentId, DoctorId = Doctor.DoctorId, PatientId = 1000, Date = DateTime.Now };
            Assert.Throws<ArgumentException>(() => AppointmentRepository.Update(appointment));
        }










        [Test]
        public void AppointmentToString()
        {
            Appointment appointment = new() { AppointmentId = 1, Date = new DateTime(2024, 4, 25, 10, 0, 0), DoctorId = 1, PatientId = 1 };
            string expected = "AppointmentId: 1, Date: 25-04-2024 10:00:00, DoctorId: 1, PatientId: 1";

            string result = appointment.ToString();

            Assert.AreEqual(expected, result);
        }

    }
}
