using Microsoft.EntityFrameworkCore;
using RequestTrackerApp.Model;

using RequestTrackerApp.Interface;
using RequestTrackerApp.Context;
using Azure.Core;
using RequestTrackerApp.Exceptions;

namespace RequestTrackerApp.Repository
{
    public class RequestRepository : IRepository<int, Requests>
    {
        private readonly RequestTrackercontext _context;

        public RequestRepository(RequestTrackercontext context)
        {
            _context = context;
        }
        public async Task<Requests> Add(Requests entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Requests> Delete(int key)
        {
            var request = await Get(key);
  
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            
            return request;
        }

        public async Task<Requests> Get(int key)
        {
            var request = _context.Requests.SingleOrDefault(e => e.RequestNumber == key);
            if (request != null)
            {
                return request;
            }
            throw new RequestNotFound();

        }

        public async Task<IList<Requests>> GetAll()
        {
            var requests = await _context.Requests.ToListAsync();

            if (requests != null && requests.Count > 0)
            {
                return requests;
            }

            throw new RequestNotFound("No requests found.");
        }


        public async Task<Requests> Update(Requests entity)
        {
            var request = await Get(entity.RequestNumber);
            if (request != null)
            {
                _context.Entry<Requests>(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return request;
        }


    }
}
