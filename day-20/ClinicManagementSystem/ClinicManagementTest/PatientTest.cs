using ClinicManagementSystem_Library.Model;
using ClinicManagementSystem_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementTest
{
    internal class PatientTest
    {
        private ICrud<Patient, int> PatientRepository;
        private Patient Patient, Patient2;

        [SetUp]
        public void Setup()
        {
            PatientRepository = new PatientRepositoryPattern();
        Patient = new Patient() { Name = "demo", PhoneNumber = "9345810993" };
        PatientRepository.Add(Patient);
        Patient2 = new Patient() { Name = "demo2", PhoneNumber = "9345810993" };
        PatientRepository.Add(Patient2);
        }

        [TestCase("john", "9345810993")]
        public void AddPatientTest(string name, string phoneNumber)
        {
            Patient patient = new Patient() { Name = name, PhoneNumber = phoneNumber };
            Patient pat = PatientRepository.Add(patient);

            Assert.AreEqual(pat.PatientId, patient.PatientId);
        }

        [Test]
        public void GetPatientTest()
        {
            Patient patient = PatientRepository.Get(Patient.PatientId);
            Assert.NotNull(patient);
            Assert.AreEqual(Patient.PatientId, patient.PatientId);
        }

        [Test]
        public void FailedGetPatientTest()
        {
            Assert.Throws<KeyNotFoundException>(() => PatientRepository.Get(1000));
        }

        [Test]
        public void UpdatePatientTest()
        {
            Patient.Name = "updatedName";
            Patient updatedPatient = PatientRepository.Update(Patient);
            Assert.AreEqual(Patient.Name, updatedPatient.Name);
        }

        [Test]
        public void FailedUpdatePatientTest()
        {
            Patient.Name = "updatedName";
            Patient.PatientId = 1000;

            Assert.Throws<KeyNotFoundException>(() => PatientRepository.Update(Patient));
        }

        [Test]
        public void DeletePatientTest()
        {
            bool result = PatientRepository.Delete(Patient2.PatientId);
            Assert.IsTrue(result);
            Assert.Throws<KeyNotFoundException>(() => PatientRepository.Get(Patient2.PatientId));
        }

        [Test]
        public void FailedDeletePatientTest()
        {
            Assert.Throws<KeyNotFoundException>(() => PatientRepository.Delete(1000));
        }


    }
}
