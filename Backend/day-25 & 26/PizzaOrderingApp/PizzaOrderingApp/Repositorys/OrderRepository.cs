using Microsoft.EntityFrameworkCore;
using PizzaOrderingApp.contexts;
using PizzaOrderingApp.Interfaces;
using PizzaOrderingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaOrderingApp.Exceptions;

namespace PizzaOrderingApp.Repositorys
{
    public class OrderRepository : IRepository<int, Orders>
    {
        private readonly PizzaOrderingAppContext _context;
        public OrderRepository(PizzaOrderingAppContext context)
        {
            _context = context;
        }

        public async Task<Orders> Add(Orders item)
        {
          _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Orders> Delete(int key)
        {
            var order = await Get(key);
            _context.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> Get(int key)
        {
            var order = await _context.Orders
                                    .Include(o => o.OrderItems) 
                                    .FirstOrDefaultAsync(e => e.OrderId == key);
            if (order != null)
                return order;

            throw new OrderNotFoundException();
        }





        public async Task<IEnumerable<Orders>> Get()
        {

            var orders = await _context.Orders
       .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Pizza)
       .ToListAsync();

         
            if (orders.Count() <= 0)
            {
                throw new NoOrderException("No Order Till Now....");
            }

            return orders;
        }

        public async Task<Orders> Update(Orders item)
        {
            var order = await Get(item.OrderId);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
