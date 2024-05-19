using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RequestTrackerApp.Model;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Context;
using RequestTrackerApp.Exceptions;

namespace RequestTrackerApp.Repository
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        public readonly RequestTrackercontext _context;

        public EmployeeRepository(RequestTrackercontext context)
        {
            _context = context;
        }
        public async Task<Employee> Add(Employee entity)
        {
            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee> Delete(int key)
        {
            var employee = await Get(key);

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            
            return employee;
        }

        public virtual async Task<Employee> Get(int key)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == key);
        if (employee != null)
        {
            return employee;
        }
            throw new EmployeeNotFound();
        }

        public virtual async Task<IList<Employee>> GetAll()
        {
            var employees= await _context.Employees.ToListAsync();
            if (employees.Count >=0)
            {
                return employees;
            }
            throw new EmployeeNotFound();



        }
        public async Task<Employee> GetByEmail(string email)
        {

            var employee =  _context.Employees.SingleOrDefault(e => e.Email == email);

  


            return employee;


        }

        public async Task<Employee> Update(Employee entity)
        {
            var employee = await Get(entity.Id);

                _context.Entry<Employee>(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            return employee;


        }
    }
}
