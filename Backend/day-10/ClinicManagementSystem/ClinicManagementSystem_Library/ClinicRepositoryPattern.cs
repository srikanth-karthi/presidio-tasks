using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagementSystem_Library
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
            throw new KeyNotFoundException("Doctor not found");
        }

        public Doctor Get(int k)
        {
            if (Doctors.ContainsKey(k))
            {
                return Doctors[k];
            }
            throw new KeyNotFoundException("Doctor not found");
        }

        public Doctor Update(Doctor doctor)
        {
            if (Doctors.ContainsKey(doctor.DoctorId))
            {
                Doctors[doctor.DoctorId] = doctor;
                return doctor;
            }
            throw new KeyNotFoundException("Doctor not found");
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
            throw new KeyNotFoundException("Patient not found");
        }

        public Patient Get(int k)
        {
            if (Patients.ContainsKey(k))
            {
                return Patients[k];
            }
            throw new KeyNotFoundException("Patient not found");
        }

        public Patient Update(Patient patient)
        {
            if (Patients.ContainsKey(patient.PatientId))
            {
                Patients[patient.PatientId] = patient;
                return patient;
            }
            throw new KeyNotFoundException("Patient not found");
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
                throw new ArgumentException("Invalid doctor id");
            }
            if (!PatientRepositoryPattern.Patients.ContainsKey(appointment.PatientId))
            {
                throw new ArgumentException("Invalid patient id");
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
            throw new KeyNotFoundException("Appointment not found");
        }

        public Appointment Get(int k)
        {
            if (Appointments.ContainsKey(k))
            {
                return Appointments[k];
            }
            throw new KeyNotFoundException("Appointment not found");
        }

        public Appointment Update(Appointment appointment)
        {
            if (Appointments.ContainsKey(appointment.AppointmentId))
            {
                if (!DoctorRepositoryPattern.Doctors.ContainsKey(appointment.DoctorId))
                {
                    throw new ArgumentException("Invalid doctor id");
                }
                if (!PatientRepositoryPattern.Patients.ContainsKey(appointment.PatientId))
                {
                    throw new ArgumentException("Invalid patient id");
                }

                Appointments[appointment.AppointmentId] = appointment;
                return appointment;
            }
            throw new KeyNotFoundException("Appointment not found");
        }

    }
}
