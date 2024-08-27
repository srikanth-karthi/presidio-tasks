using ClinicManagementSystem_Library;

[TestFixture]
public class Tests
{
    private ICrud<Doctor, int> DoctorRepository;
    private Doctor Doctor;
    private ICrud<Patient, int> PatientRepository;
    private Patient Patient;
    private ICrud<Appointment, int> AppointmentRepository;
    private Appointment Appointment;

    [SetUp]
    public void Setup()
    {
        DoctorRepository = new DoctorRepositoryPattern();
        Doctor = new Doctor() { Name = "demo", Specialization = "demo" };
        DoctorRepository.Add(Doctor);

        PatientRepository = new PatientRepositoryPattern();
        Patient = new Patient() { Name = "demo", PhoneNumber = "9345810993" };
        PatientRepository.Add(Patient);

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
        bool result = DoctorRepository.Delete(Doctor.DoctorId);
        Assert.IsTrue(result);
        Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Get(Doctor.DoctorId));
    }

    [Test]
    public void FailedDeleteDoctorTest()
    {
        Assert.Throws<KeyNotFoundException>(() => DoctorRepository.Delete(Doctor.DoctorId = 1000));
    }

    // PatientRepositoryPattern tests

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
        bool result = PatientRepository.Delete(Patient.PatientId);
        Assert.IsTrue(result);
        Assert.Throws<KeyNotFoundException>(() => PatientRepository.Get(Patient.PatientId));
    }

    [Test]
    public void FailedDeletePatientTest()
    {
        Assert.Throws<KeyNotFoundException>(() => PatientRepository.Delete(1000));
    }



    [Test]
    public void AddAppointment_InvalidDoctorId()
    {
        Appointment appointment = new Appointment() { DoctorId = 1000, PatientId = Patient.PatientId, Date = DateTime.Now };
        Assert.Throws<ArgumentException>(() => AppointmentRepository.Add(appointment));
    }

    [Test]
    public void AddAppointment_InvalidPatientId() { 
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
    public void UpdateAppointment_InvalidDoctorId ()
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
    public void DoctorToString()
    {
        
        string expected = "DoctorId: 1, Name: demo, Specialization: demo";

        string result = Doctor.ToString();

        Assert.AreEqual(expected, result);
    }



    [Test]
    public void PatientToString()
    {
    
        string expected = "PatientId: 1, Name: demo, PhoneNumber: 9345810993";

        string result = Patient.ToString();

        Assert.AreEqual(expected, result);
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


