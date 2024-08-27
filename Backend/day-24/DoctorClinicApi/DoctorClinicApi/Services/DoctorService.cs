using DoctorClinicApi.Model;
using DoctorClinicApi.Interfaces;
using DoctorClinicApi.Exceptions;

namespace DoctorClinicApi.Services
{
    public class DoctorService: IDoctorService
    {
        public IRepository<Doctor, int> repository { get; }
        public DoctorService(IRepository<Doctor,int> _repository)
        {
            repository = _repository;
        }


        public async Task<Doctor> GetDoctorbyId(int doctorId)
        {
            try
            {
                return await repository.Get(doctorId);
            }
            catch (DoctorNotFoundExceptions)
            {
                throw;
            }
        }
        public async Task<Doctor> UpdateDoctorExperience(int doctorId, float experience)
        {
            try
            {
                var doc = await GetDoctorbyId(doctorId);
                doc.Experience = experience;
                    return await repository.Update(doc);

            }
            catch(DoctorNotFoundExceptions)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            try
            {
                return await repository.Get();
            }
            catch (DoctorNotFoundExceptions)
            {
                throw; 
            }
        }

        public async Task<IEnumerable<Doctor>> GetDoctorBySpeciality(string speciality)
        {
            try
            {
                var doctors = await GetDoctors();
                return doctors.Where(d => d.Specification == speciality);
            }
            catch (DoctorNotFoundExceptions)
            {
                throw;
            }
        }
    }
}
