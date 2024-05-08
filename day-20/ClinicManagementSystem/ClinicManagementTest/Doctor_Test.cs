using ClinicManagementSystem_Library;
using ClinicManagementSystem_Library.Model;

namespace ClinicManagementTest
{
    public class DoctorTest
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

        // DoctorRepositoryPattern tests

        [TestCase("srikanth", "mbbs")]
        public void AddDoctorTest(string name, string specialization)
        {
            Doctor doctor = new Doctor() { Name = name, Specialization = specialization };
            Doctor doc = DoctorRepository.Add(doctor);

            Assert.AreEqual(doc.DoctorId, doctor.DoctorId);
        }

        [Test]
        public void GetDoctorTest()
        {
            Doctor doctor = DoctorRepository.Get(Doctor.DoctorId);
            Assert.NotNull(doctor);
            Assert.AreEqual(Doctor.DoctorId, doctor.DoctorId);
        }

        [Test]
        public void FailedGetDoctorTest()
        {
            Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Get(1000));
        }

        [Test]
        public void UpdateDoctorTest()
        {
            Doctor.Name = "updatedName";
            Doctor updatedDoctor = DoctorRepository.Update(Doctor);
            Assert.AreEqual(Doctor.Name, updatedDoctor.Name);
        }

        [Test]
        public void FailedUpdateDoctorTest()
        {
            Doctor.Name = "updatedName";
            Doctor.DoctorId = 1000;

            Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Update(Doctor));
        }

        [Test]
        public void DeleteDoctorTest()
        {
            bool result = DoctorRepository.Delete(Doctor2.DoctorId);
            Assert.IsTrue(result);
            Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Get(Doctor2.DoctorId));
        }

        [Test]
        public void FailedDeleteDoctorTest()
        {
            Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Delete(Doctor.DoctorId = 1000));
        }




    }


}