namespace ClinicManagementSystem_Library
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public override string ToString()
        {
            return $"DoctorId: {DoctorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"PatientId: {PatientId}, Name: {Name}, PhoneNumber: {PhoneNumber}";
        }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public override string ToString()
        {
            return $"AppointmentId: {AppointmentId}, Date: {Date}, DoctorId: {DoctorId}, PatientId: {PatientId}";
        }
    }
}
