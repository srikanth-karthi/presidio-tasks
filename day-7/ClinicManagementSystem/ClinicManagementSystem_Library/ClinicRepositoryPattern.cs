﻿namespace ClinicManagementSystem_Library
{
    public class DoctorRepositoryPattern : ICrud<Doctor, int>
    {
        public static Dictionary<int, Doctor> Doctors { get; set; }

        public DoctorRepositoryPattern()
        {
            Doctors = new Dictionary<int, Doctor>();
        }

        private int GenerateId()
        {
            if (Doctors.Count == 0)
                return 1;
            int id = Doctors.Keys.Max();
            return ++id;
        }

        public Doctor Add(Doctor doctor)
        {
            doctor.DoctorId = GenerateId();
            Doctors.Add(doctor.DoctorId, doctor);

            return doctor;
        }

        public bool Delete(int k)
        {
            if (Doctors.ContainsKey(k))
            {
                Doctors.Remove(k);
                return true;
            }
            return false;
        }

        public Doctor Get(int k)
        {
            return Doctors.ContainsKey(k) ? Doctors[k] : null;
        }

        public Doctor Update(Doctor doctor)
        {
            if (Doctors.ContainsKey(doctor.DoctorId))
            {
                Doctors[doctor.DoctorId] = doctor;
                return doctor;
            }
            return null;
        }
    }

    public class PatientRepositoryPattern : ICrud<Patient, int>
    {
        public static Dictionary<int, Patient> Patients { get; set; }

        public PatientRepositoryPattern()
        {
            Patients = new Dictionary<int, Patient>();
        }

        private int GenerateId()
        {
            if (Patients.Count == 0)
                return 1;
            int id = Patients.Keys.Max();
            return ++id;
        }

        public Patient Add(Patient patient)
        {
            patient.PatientId = GenerateId();
            Patients.Add(patient.PatientId, patient);
            return patient;
        }

        public bool Delete(int k)
        {
            if (Patients.ContainsKey(k))
            {
                Patients.Remove(k);
                return true;
            }
            return false;
        }

        public Patient Get(int k)
        {
            return Patients.ContainsKey(k) ? Patients[k] : null;
        }

        public Patient Update(Patient patient)
        {
            if (Patients.ContainsKey(patient.PatientId))
            {
                Patients[patient.PatientId] = patient;
                return patient;
            }
            return null;
        }
    }

        public class AppointmentRepositoryPattern : ICrud<Appointment, int>
        {
            private Dictionary<int, Appointment> Appointments { get; set; }

            public AppointmentRepositoryPattern()
            {
                Appointments = new Dictionary<int, Appointment>();
            }

            private int GenerateId()
            {
                if (Appointments.Count == 0)
                    return 1;
                int id = Appointments.Keys.Max();
                return ++id;
            }

            public Appointment Add(Appointment appointment)
            {
                appointment.AppointmentId = GenerateId();
                if (!DoctorRepositoryPattern.Doctors.ContainsKey(appointment.DoctorId))
            {
                Console.WriteLine( "Invalid doctor id");
                return null;
            }
            if (!PatientRepositoryPattern.Patients.ContainsKey(appointment.PatientId))
            {
                Console.WriteLine("Invalid Patients id");
                return null;
            }
            Appointments.Add(appointment.AppointmentId, appointment);
                return appointment;
            }

            public bool Delete(int k)
            {
                if (Appointments.ContainsKey(k))
                {
                    Appointments.Remove(k);
                    return true;
                }
                return false;
            }

            public Appointment Get(int k)
            {
                return Appointments.ContainsKey(k) ? Appointments[k] : null;
            }

            public Appointment Update(Appointment appointment)
            {
                if (Appointments.ContainsKey(appointment.AppointmentId))
                {
                    Appointments[appointment.AppointmentId] = appointment;
                    return appointment;
                }
                return null;
            }
        }
    }


