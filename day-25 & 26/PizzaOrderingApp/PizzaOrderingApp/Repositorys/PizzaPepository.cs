using Microsoft.EntityFrameworkCore;
using PizzaOrderingApp.contexts;
using PizzaOrderingApp.Interfaces;
using PizzaOrderingApp.Models;
using PizzaOrderingApp.Exceptions;

namespace PizzaOrderingApp.Repositorys
{
    public class PizzaPepository : IRepository<int, Pizza>
    {

        private readonly PizzaOrderingAppContext _context;
        public PizzaPepository(PizzaOrderingAppContext context)
        {
            _context = context;
        }
        public async Task<Pizza> Add(Pizza item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }



        public async Task<Pizza> Delete(int key)
        {
            var pizza = await Get(key);

            _context.Remove(pizza);
          await  _context.SaveChangesAsync(true);
            return pizza;

        }

        public Task<Pizza> Get(int key)
        {
            var pizza = _context.Pizzas.FirstOrDefaultAsync(e => e.PizzaId == key);
            if (pizza != null)
                return pizza;

            throw new PizzaNotFound();
        }

        public async Task<IEnumerable<Pizza>> Get()
        {
            var pizza = await _context.Pizzas.ToListAsync();

            if(pizza.Count <= 0) throw new PizzaNotFound();
            return pizza;

        }

        public async Task<Pizza> Update(Pizza item)
        {
            var pizza = await Get(item.PizzaId);

            _context.Update(item);
            await _context.SaveChangesAsync(true);
            return pizza;

        }


    }
}
