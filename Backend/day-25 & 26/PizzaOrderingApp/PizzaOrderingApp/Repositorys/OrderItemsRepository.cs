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
    public class OrderItemsRepository : IRepository<int, OrderItems>
    {
        private readonly PizzaOrderingAppContext _context;
        public OrderItemsRepository(PizzaOrderingAppContext context)
        {
            _context = context;
        }

        public async Task<OrderItems> Add(OrderItems item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<OrderItems> Delete(int key)
        {
            var orderItem = await Get(key);
            _context.Remove(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItems> Get(int key)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(e => e.OrderItemsId == key);
            if (orderItem != null)
                return orderItem;

            throw new OrderItemNotFoundException();
        }

        public async Task<IEnumerable<OrderItems>> Get()
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return orderItems;
        }

        public async Task<OrderItems> Update(OrderItems item)
        {
            var orderItem = await Get(item.OrderItemsId);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return orderItem;
        }
    }
}
