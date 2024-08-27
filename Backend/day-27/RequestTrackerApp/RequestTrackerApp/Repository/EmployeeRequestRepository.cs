using Microsoft.EntityFrameworkCore;
using RequestTrackerApp.Model;
using RequestTrackerApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestTrackerApp.Context;
using RequestTrackerApp.Exceptions;

namespace RequestTrackerApp.Repository
{
  
    public class EmployeeRequestRepository : EmployeeRepository
    {
        public EmployeeRequestRepository(RequestTrackercontext context) : base(context)
        {
        }

        public async override Task<Employee> Get(int key)
        {
            var employee = _context.Employees.Include(e => e.RequestsRaised).SingleOrDefault(e => e.Id == key);
            if (employee != null)
            {
                return employee;
            }
            throw new EmployeeNotFound();
        }
        public override async Task<IList<Employee>> GetAll()
        {
       var employees = await _context.Employees.Include(e => e.RequestsRaised).ToListAsync();
            if (employees.Count >= 0)
            {
                return employees;
            }
            throw new EmployeeNotFound();
        }
    }

}
