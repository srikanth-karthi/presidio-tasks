using DoctorClinicApi.Model;

namespace DoctorClinicApi.Interfaces
{
    public interface IDoctorService
    {
        public Task<IEnumerable<Doctor>> GetDoctorBySpeciality(string speciality);
        public Task<Doctor> UpdateDoctorExperience(int doctorId, float experience);
        public Task<IEnumerable<Doctor>> GetDoctors();
    }
}
