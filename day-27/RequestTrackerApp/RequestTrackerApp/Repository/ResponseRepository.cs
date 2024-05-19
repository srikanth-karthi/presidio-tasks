using Microsoft.EntityFrameworkCore;

using RequestTrackerApp.Repository;
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
    public class ResponseRepository : IRepository<int, SolutionResposnse>
    {
        private readonly RequestTrackercontext _context;

        public ResponseRepository(RequestTrackercontext context)
        {
            _context = context;
        }

        public async Task<SolutionResposnse> Add(SolutionResposnse entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SolutionResposnse> Delete(int key)
        {
            var response = await Get(key);
     
            
                _context.SolutionResposnse.Remove(response);
                await _context.SaveChangesAsync();
            
            return response;
        }
        public async Task<SolutionResposnse> Get(int key)
        {
            var response = _context.SolutionResposnse.SingleOrDefault(e => e.ResponseId == key);
            if (response != null)
            {
                return response;
            }
            throw new ResponseNotFound();

     
        }
        public async Task<SolutionResposnse> GetBySolution(int key)
        {
            
            
            var response = _context.SolutionResposnse.SingleOrDefault(e => e.SolutionId == key);

            if (response != null)
            {
                return response;
            }
            throw new EmployeeNotFound();

        }

        public async Task<IList<SolutionResposnse>> GetAll()
        {
            var response= await _context.SolutionResposnse.ToListAsync();
            if (response.Count >= 0)
            {
                return response;
            }
            throw new EmployeeNotFound();
        }

        public async Task<SolutionResposnse> Update(SolutionResposnse entity)
        {
            var response = await Get(entity.ResponseId);
      _context.Entry<SolutionResposnse>(response).State = EntityState.Modified;
                await _context.SaveChangesAsync();
           
            return response;
        }
    }
}
