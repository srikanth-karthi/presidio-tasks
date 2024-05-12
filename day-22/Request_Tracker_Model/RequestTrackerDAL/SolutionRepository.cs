
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
    public class SolutionsRepository : IRepository<int, RequestSolution>
    {

        private readonly RequestTrackerContext _context;

        public SolutionsRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<RequestSolution> Add(RequestSolution entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RequestSolution> Delete(int key)
        {
            var request = await Get(key);
            if (request != null)
            {

                _context.RequestSolution.Remove(request);
                await _context.SaveChangesAsync();
            }
            return request;
        }

        public async Task<RequestSolution> Get(int key)
        {
            var request = _context.RequestSolution.SingleOrDefault(e => e.SolutionId == key);
            return request;
        }

        public async Task<IList<RequestSolution>> GetAll()
        {
            return await _context.RequestSolution.ToListAsync();
        }

        public async Task<RequestSolution> Update(RequestSolution entity)
        {
            var request = await Get(entity.SolutionId);
            if (request != null)
            {
                _context.Entry<RequestSolution>(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return request;
        }

    }
}
