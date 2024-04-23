using ClinicManagementSystem_Library;


namespace ClinicManagementSystem
{
    public class ClinicService
    {
        private static DoctorRepositoryPattern doctorRepositoryPattern = new DoctorRepositoryPattern();
        private static PatientRepositoryPattern patientRepositoryPattern = new PatientRepositoryPattern();
        private static AppointmentRepositoryPattern appointmentRepositoryPattern = new AppointmentRepositoryPattern();


        public static Doctor AddDoctor(Doctor doctor)
        {
            return doctorRepositoryPattern.Add(doctor);
        }

        public static Doctor GetDoctor(int key)
        {
            return doctorRepositoryPattern.Get(key);
        }

        public static Doctor UpdateDoctor(Doctor doctor)
        {
            return doctorRepositoryPattern.Update(doctor);
        }

        public static bool DeleteDoctor(int key)
        {
            return doctorRepositoryPattern.Delete(key);
        }


        public static Patient AddPatient(Patient patient)
        {
            return patientRepositoryPattern.Add(patient);
        }

        public static Patient GetPatient(int key)
        {
            return patientRepositoryPattern.Get(key);
        }

        public static Patient UpdatePatient(Patient patient)
        {
            return patientRepositoryPattern.Update(patient);
        }

        public static bool DeletePatient(int key)
        {
            return patientRepositoryPattern.Delete(key);
        }

   
        public static Appointment AddAppointment(Appointment appointment)
        {
            return appointmentRepositoryPattern.Add(appointment);
        }

        public static Appointment GetAppointment(int key)
        {
            return appointmentRepositoryPattern.Get(key);
        }

        public static Appointment UpdateAppointment(Appointment appointment)
        {
            return appointmentRepositoryPattern.Update(appointment);
        }

        public static bool DeleteAppointment(int key)
        {
            return appointmentRepositoryPattern.Delete(key);
        }
    }
}
