using DoctorClinicApi.Contexts;
using DoctorClinicApi.Interfaces;
using DoctorClinicApi.Model;
using Microsoft.EntityFrameworkCore;
using DoctorClinicApi.Exceptions;

namespace DoctorClinicApi.Repositories
{
    public class DoctorRepository : IRepository<Doctor, int>
    {
        private readonly DoctorClinicContext context;

        public DoctorRepository(DoctorClinicContext _context)
        {
            context=_context;
        }

        public async Task<Doctor> Add(Doctor item)
        {
          context.Add(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int key)
        {
            var doctor = await Get(key);

            context.Remove(key);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Doctor> Get(int key)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(e => e.Doctorid == key);
            if(doctor != null)
            return doctor;

            throw new DoctorNotFoundExceptions();
        }

        public async Task<IEnumerable<Doctor>> Get()
        {
            var doctorList = await context.Doctors.ToListAsync();
            if(doctorList.Count() <= 0) return doctorList;

            throw new DoctorNotFoundExceptions();

        }

        public async Task<Doctor> Update(Doctor item)
        {
            var doctor = await Get(item.Doctorid);

                context.Update(item);
                context.SaveChangesAsync(true);


                return doctor;

    
        }
    }
}
