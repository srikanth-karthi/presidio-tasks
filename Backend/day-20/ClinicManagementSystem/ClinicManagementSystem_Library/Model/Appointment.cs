using System;
using System.Collections.Generic;

namespace ClinicManagementSystem_Library.Model;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime Date { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
    public override string ToString()
    {
        return $"AppointmentId: {AppointmentId}, Date: {Date}, DoctorId: {DoctorId}, PatientId: {PatientId}";
    }
}
