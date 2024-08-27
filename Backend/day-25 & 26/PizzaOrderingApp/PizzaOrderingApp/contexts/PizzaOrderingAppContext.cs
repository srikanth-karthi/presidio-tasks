using Microsoft.EntityFrameworkCore;
using PizzaOrderingApp.Models;

namespace PizzaOrderingApp.contexts
{
    public class PizzaOrderingAppContext : DbContext
    {


        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Users> Users { get; set; }
        public PizzaOrderingAppContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { PizzaId = 1, PizzaName = "Margherita", PizzaFlavour = "Classic tomato, basil, mozzarella", Price = 8.99, Description = "A classic pizza with simple and delicious flavors.", IsVegetarian = true, Stock = 100 },
                new Pizza { PizzaId = 2, PizzaName = "Pepperoni", PizzaFlavour = "Pepperoni, mozzarella", Price = 10.99, Description = "A classic favorite with spicy pepperoni slices.", IsVegetarian = false, Stock = 80 },
                new Pizza { PizzaId = 3, PizzaName = "Vegetarian", PizzaFlavour = "Assorted vegetables, mozzarella", Price = 9.99, Description = "A flavorful vegetarian option with a variety of vegetables.", IsVegetarian = true, Stock = 70 },
                new Pizza { PizzaId = 4, PizzaName = "Hawaiian", PizzaFlavour = "Ham, pineapple, mozzarella", Price = 11.99, Description = "A tropical delight with ham and pineapple toppings.", IsVegetarian = false, Stock = 0 },
                new Pizza { PizzaId = 5, PizzaName = "BBQ Chicken", PizzaFlavour = "BBQ sauce, chicken, onions, mozzarella", Price = 12.99, Description = "A tangy BBQ flavor with grilled chicken and onions.", IsVegetarian = false, Stock = 50 },
                new Pizza { PizzaId = 6, PizzaName = "Mushroom Lovers", PizzaFlavour = "Assorted mushrooms, mozzarella", Price = 10.99, Description = "A mushroom lover's dream with a variety of mushrooms.", IsVegetarian = true, Stock = 75 },
                new Pizza { PizzaId = 7, PizzaName = "Meat Feast", PizzaFlavour = "Pepperoni, sausage, ham, bacon, mozzarella", Price = 13.99, Description = "A meat lover's delight with a variety of meats.", IsVegetarian = false, Stock = 0 },
                new Pizza { PizzaId = 8, PizzaName = "Four Cheese", PizzaFlavour = "Mozzarella, cheddar, parmesan, provolone", Price = 11.99, Description = "A cheese lover's pizza with four different cheeses.", IsVegetarian = true, Stock = 65 },
                new Pizza { PizzaId = 9, PizzaName = "Supreme", PizzaFlavour = "Pepperoni, sausage, bell peppers, onions, olives, mozzarella", Price = 12.99, Description = "An ultimate combination of toppings for a satisfying pizza.", IsVegetarian = false, Stock = 45 },
                new Pizza { PizzaId = 10, PizzaName = "Pesto Chicken", PizzaFlavour = "Pesto sauce, chicken, tomatoes, mozzarella", Price = 12.99, Description = "A flavorful pesto sauce with grilled chicken and tomatoes.", IsVegetarian = false, Stock = 60 }
            );

            modelBuilder.Entity<Orders>().HasData(
              new Orders { OrderId = 1, UserId = 1, TotalPrice = 19.98 }, 
                 new Orders { OrderId = 2, UserId = 2, TotalPrice = 15.99 }, // Assuming UserId 2 exists
                new Orders { OrderId = 3, UserId = 3, TotalPrice = 25.49 } // Assuming UserId 3 exists
 );

            // Add seed data for OrderItems table
            modelBuilder.Entity<OrderItems>().HasData(
                new OrderItems { OrderItemsId = 1,  PizzaId = 1, Quantity = 2, Price = 8.99,OrderId=1 }, // Assuming OrderId 1 and PizzaId 1 exist
                new OrderItems { OrderItemsId = 2,  PizzaId = 2, Quantity = 1, Price = 10.99, OrderId = 1 }, // Assuming OrderId 2 and PizzaId 2 exist
                new OrderItems { OrderItemsId = 3, PizzaId = 3, Quantity = 3, Price = 9.99, OrderId = 2} // Assuming OrderId 3 and PizzaId 3 exist
            );
            modelBuilder.Entity<Users>().HasData(
                      new Users { UserId = 1, Name = "JohnDoe", Email = "john.doe@example.com", Password = [], HasCode = [] },
                      new Users { UserId = 2, Name = "JaneSmith", Email = "jane.smith@example.com", Password = [], HasCode = [] },
                         new Users { UserId = 3, Name = "AliceJohnson", Email = "alice.johnson@example.com", Password = [], HasCode = [] }
);
        }



    }
}

   
                

     



