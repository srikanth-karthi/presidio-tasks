using ClinicManagementSystem_Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagementSystem_Library
{
    public class DoctorRepositoryPattern : ICrud<Doctor, int>
    {

        ClinicManagementSystemContext context = new ClinicManagementSystemContext();


        private int GenerateId()
        {
            int maxId = context.Doctors.Any() ? context.Doctors.Max(d => d.DoctorId) : 0;
            return maxId + 1;
        }


        public Doctor Add(Doctor doctor)
        {

                doctor.DoctorId = GenerateId();
            context.Doctors.Add(doctor);
            context.SaveChanges();
            return doctor;

        }

        public bool Delete(int k)
        {
           var doctor= context.Doctors.Find(k);
            if (doctor!=null)
            {
                context.Doctors.Remove(doctor);
                context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("Doctor not found");
        }

        public Doctor Get(int k)
        {
            var doctor = context.Doctors.Find(k);
            if (doctor != null)
            {
                return doctor;
            }
            throw new KeyNotFoundException("Doctor not found");
        }

        public Doctor Update(Doctor doctor)
        {
            var doc = context.Doctors.Find(doctor.DoctorId);
            if (doc != null)
            {
                context.Doctors.Update(doc);
                return doctor;
            }
            throw new KeyNotFoundException("Doctor not found");
        }
    }

    public class PatientRepositoryPattern : ICrud<Patient, int>
    {
        ClinicManagementSystemContext _context = new ClinicManagementSystemContext();

        private int GenerateId()
        {
            int maxId = _context.Patients.Any() ? _context.Patients.Max(p => p.PatientId) : 0;
            return maxId + 1;
        }

        public Patient Add(Patient patient)
        {
            patient.PatientId = GenerateId();
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient;
        }

        public bool Delete(int k)
        {
            var patient = _context.Patients.Find(k);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("Patient not found");
        }

        public Patient Get(int k)
        {
            var patient = _context.Patients.Find(k);
            if (patient != null)
            {
                return patient;
            }
            throw new KeyNotFoundException("Patient not found");
        }

        public Patient Update(Patient patient)
        {
            var pat = _context.Patients.Find(patient.PatientId);
            if (pat != null)
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();
                return patient;
            }
            throw new KeyNotFoundException("Patient not found");
        }
    }

    public class AppointmentRepositoryPattern : ICrud<Appointment, int>
    {


        ClinicManagementSystemContext _context = new ClinicManagementSystemContext();

        private int GenerateId()
        {
            int maxId = _context.Appointments.Any() ? _context.Appointments.Max(a => a.AppointmentId) : 0;
            return maxId + 1;
        }

        public Appointment Add(Appointment appointment)
        {
            appointment.AppointmentId = GenerateId();
            if (!_context.Doctors.Any(d => d.DoctorId == appointment.DoctorId))
            {
                throw new ArgumentException("Invalid doctor id");
            }
            if (!_context.Patients.Any(p => p.PatientId == appointment.PatientId))
            {
                throw new ArgumentException("Invalid patient id");
            }
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return appointment;
        }

        public bool Delete(int k)
        {
            var appointment = _context.Appointments.Find(k);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("Appointment not found");
        }

        public Appointment Get(int k)
        {
            var appointment = _context.Appointments.Find(k);
            if (appointment != null)
            {
                return appointment;
            }
            throw new KeyNotFoundException("Appointment not found");
        }

        public Appointment Update(Appointment appointment)
        {
            var app = _context.Appointments.Find(appointment.AppointmentId);
            if (app != null)
            {
                if (!_context.Doctors.Any(d => d.DoctorId == appointment.DoctorId))
                {
                    throw new ArgumentException("Invalid doctor id");
                }
                if (!_context.Patients.Any(p => p.PatientId == appointment.PatientId))
                {
                    throw new ArgumentException("Invalid patient id");
                }

                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return appointment;
            }
            throw new KeyNotFoundException("Appointment not found");
        }
    }

}
