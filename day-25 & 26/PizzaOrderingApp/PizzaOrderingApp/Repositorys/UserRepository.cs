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
    public class UserRepository : IRepository<int, Users>
    {
        private readonly PizzaOrderingAppContext _context;
        public UserRepository(PizzaOrderingAppContext context)
        {
            _context = context;
        }

        public async Task<Users> Add(Users item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Users> Delete(int key)
        {
            var user = await Get(key);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users> Get(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == key);
            if (user != null)
                return user;

            throw new UserNotFoundException();
        }

        public async Task<IEnumerable<Users>> Get()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<Users> Update(Users item)
        {
            var user = await Get(item.UserId);
            _context.Update(item);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
