using Azure;
using Microsoft.EntityFrameworkCore;
using RequestTrackerApp.Context;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Model;
using RequestTrackerApp.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerApp.Repository
{
    public class FeedbackRepository : IRepository<int, SolutionFeedback>
    {
        private readonly RequestTrackercontext _context;

        public FeedbackRepository(RequestTrackercontext context)
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
              _context.SolutionFeedback.Remove(feedback);
                await _context.SaveChangesAsync();
            return feedback;
            }

   
    

        public async Task<SolutionFeedback> Get(int key)
        {
            var feedback = _context.SolutionFeedback.SingleOrDefault(e => e.FeedbackId == key);

        if (feedback != null)
        {
            return feedback;
        }
        throw new FeedBackNotFound();
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
