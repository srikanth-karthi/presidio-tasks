
using Microsoft.EntityFrameworkCore;
using RequestTrackerApp.Model;
using RequestTrackerApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestTrackerApp.Context;
using Azure;
using RequestTrackerApp.Exceptions;



namespace RequestTrackerApp.Repository
{
    public class SolutionsRepository : IRepository<int, RequestSolution>
    {

        private readonly RequestTrackercontext _context;

        public SolutionsRepository(RequestTrackercontext context)
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
            var solution = await Get(key);
  

                _context.RequestSolution.Remove(solution);
                await _context.SaveChangesAsync();
            
            return solution;
        }

        public async Task<RequestSolution> Get(int key)
        {
            var solution = _context.RequestSolution.SingleOrDefault(e => e.SolutionId == key);

            if (solution != null)
            {
                return solution;
            }
            throw new SolutioNotFound();
        }

        public async Task<IList<RequestSolution>> GetAll()
        {
           var solution= await _context.RequestSolution.ToListAsync();
            if (solution.Count >= 0)
            {
                return solution;
            }
            throw new SolutioNotFound();
        }

        public async Task<RequestSolution> Update(RequestSolution entity)
        {
            var solution = await Get(entity.SolutionId);

                _context.Entry<RequestSolution>(solution).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            
            return solution;
        }

    }
}
