using Microsoft.EntityFrameworkCore;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDAL
{
  
    public class EmployeeRequestRepository : EmployeeRepository
    {
        public EmployeeRequestRepository(RequestTrackerContext context) : base(context)
        {
        }

        public async override Task<Employee> Get(int key)
        {
            var employee = _context.Employees.Include(e => e.RequestsRaised).SingleOrDefault(e => e.Id == key);
            return employee;
        }
        public override async Task<IList<Employee>> GetAll()
        {
            return await _context.Employees.Include(e => e.RequestsRaised).ToListAsync();
        }
    }

}
