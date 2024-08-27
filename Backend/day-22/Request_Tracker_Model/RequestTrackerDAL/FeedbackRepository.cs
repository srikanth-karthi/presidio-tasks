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
    public class FeedbackRepository : IRepository<int, SolutionFeedback>
    {
        private readonly RequestTrackerContext _context;

        public FeedbackRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        public async Task<SolutionFeedback> Add(SolutionFeedback entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SolutionFeedback> Delete(int key)
        {
            var feedback = await Get(key);
            if (feedback != null)
            {
                _context.SolutionFeedback.Remove(feedback);
                await _context.SaveChangesAsync();
            }
            return feedback;
        }

        public async Task<SolutionFeedback> Get(int key)
        {
            var feedback = _context.SolutionFeedback.SingleOrDefault(e => e.FeedbackId == key);
            return feedback;
        }

        public async Task<IList<SolutionFeedback>> GetAll()
        {
            return await _context.SolutionFeedback.ToListAsync();
        }

        public async Task<SolutionFeedback> Update(SolutionFeedback entity)
        {
            var feedback = await Get(entity.FeedbackId);
            if (feedback != null)
            {
                _context.Entry<SolutionFeedback>(feedback).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return feedback;
        }
    }
}
