using PizzaOrderingApp.Interfaces;
using PizzaOrderingApp.Models;
using PizzaOrderingApp.Models.Dto;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using PizzaOrderingApp.Exceptions;
using PizzaOrderingApp.Services;

namespace PizzaOrderingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, Pizza> pizza;
        private readonly ITokenService _tokenService;
        private readonly IRepository<int, Users> user;
        private readonly IRepository<int, Orders> order;
        private readonly IRepository<int, OrderItems> orderItems;

        public UserService(ITokenService tokenService,IRepository<int, Users> _user, IRepository<int, Orders> _orders, IRepository<int, OrderItems> _OrdersItems, IRepository<int, Pizza> _Pizza)
        {
            user = _user;
            order = _orders;
            orderItems = _OrdersItems;
            pizza = _Pizza;
            _tokenService = tokenService;
        }



        public async Task<AuthResponceDTO> Register(UserDTO users)
        {
            HMACSHA512 hMACSHA = new HMACSHA512();

            Users member = await user.Add(new Users
            {
                Name = users.Name,
                Email = users.Email,
                HasCode = hMACSHA.Key,
                Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(users.Password)),
            });

            return new AuthResponceDTO()
            {
                UserId = member.UserId,
                Name = users.Name,
                Email = member.Email,
                     Token = _tokenService.GenerateToken(member)


            };
        }


        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<AuthResponceDTO> Login(UserDTO users)
        {
            var members = await user.Get();
            Users member = members.FirstOrDefault(e => e.Email == users.Email);

            if (member != null)
            {
                HMACSHA512 hMACSHA = new HMACSHA512(member.HasCode);
                var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(users.Password));

                if (ComparePassword(encrypterPass, member.Password))
                {
                    return new AuthResponceDTO()
                    {
                        UserId = member.UserId,
                        Name = users.Name,
                        Email = member.Email,
                        Token= _tokenService.GenerateToken(member)


                    };
                }
            }
            throw new UnauthorizedUserException("Invalid email or password");
        }


        public async Task<IEnumerable<ListPizzaDTO>> GetFoodList()
        {
            var pizzas = await pizza.Get();

            pizzas = pizzas.Where(e => e.Stock > 0);

            if (pizzas.Count() <= 0) throw new NoStockException("No food available right now...");

            return pizzas.Select(pizza => new ListPizzaDTO
            {
                PizzaId = pizza.PizzaId,
                PizzaName = pizza.PizzaName,
                PizzaFlavour = pizza.PizzaFlavour,
                Price = pizza.Price,
                Description = pizza.Description,
                IsVegetarian = pizza.IsVegetarian,
                Stock = pizza.Stock
            }).ToList();
        }

        public async Task<String> PlaceOrder(PlaceOrderDto Items, int userId)
        {
            double totalprice = 0;
            var _Order = await order.Add(new Orders() { UserId = userId, TotalPrice = totalprice });


                foreach (var item in Items.OrderItems)
                {
                    var Pizza = await pizza.Get(item.PizzaId);

                    if (Pizza.Stock >= item.Quantity)
                    {
                        await orderItems.Add(new OrderItems() { OrderId = _Order.OrderId, PizzaId = Pizza.PizzaId, Price = Pizza.Price, Quantity = item.Quantity });
                        totalprice += Pizza.Price* item.Quantity;

                        Pizza.Stock -= item.Quantity;
                        await pizza.Update(Pizza);
                    }
                    else
                    {
                        throw new InsufficientQundityExecution($"{Pizza.PizzaName} is Out of stock");
                    }
                }

                _Order.TotalPrice = totalprice;
                await order.Update(_Order);

                return "Order placed SucessFully";
            

        }





        public async Task<List<OrderDto>> GetOrderList(int userId)
        {
            var orders = await order.Get();
            var userOrders = orders.Where(e => e.UserId == userId).ToList();

            if (userOrders.Count <= 0) throw new OrderNotFoundException("No Order Till Now...");

            return userOrders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    OrderItemId = item.OrderItemsId,
                    PizzaId = item.PizzaId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Pizza = new PizzaDto
                    {
                        PizzaName = item.Pizza.PizzaName,
                        PizzaFlavour = item.Pizza.PizzaFlavour,
                        IsVegetarian = item.Pizza.IsVegetarian
                    }
                }).ToList()
            }).ToList();
        }


    }
}


