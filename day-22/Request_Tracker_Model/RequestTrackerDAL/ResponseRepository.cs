using Microsoft.EntityFrameworkCore;
using Request_Tracker_Model;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDAL
{
    public class ResponseRepository : IRepository<int, SolutionResposnse>
    {
        private readonly RequestTrackerContext _context;

        public ResponseRepository(RequestTrackerContext context)
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
            if (response != null)
            {
                _context.SolutionResposnse.Remove(response);
                await _context.SaveChangesAsync();
            }
            return response;
        }
        public async Task<SolutionResposnse> Get(int key)
        {
            var response = _context.SolutionResposnse.SingleOrDefault(e => e.ResponseId == key);
            return response;
        }
        public async Task<SolutionResposnse> GetBySolution(int key)
        {
            var response = _context.SolutionResposnse.SingleOrDefault(e => e.SolutionId == key);
            return response;
        }

        public async Task<IList<SolutionResposnse>> GetAll()
        {
            return await _context.SolutionResposnse.ToListAsync();
        }

        public async Task<SolutionResposnse> Update(SolutionResposnse entity)
        {
            var response = await Get(entity.ResponseId);
            if (response != null)
            {
                _context.Entry<SolutionResposnse>(response).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return response;
        }
    }
}
