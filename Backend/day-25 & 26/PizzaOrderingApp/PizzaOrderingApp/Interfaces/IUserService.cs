using PizzaOrderingApp.Models;
using PizzaOrderingApp.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaOrderingApp.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponceDTO> Register(UserDTO users);
        Task<AuthResponceDTO> Login(UserDTO users);
        Task<IEnumerable<ListPizzaDTO>> GetFoodList();
        Task<string> PlaceOrder(PlaceOrderDto Items, int userId);
        Task <List<OrderDto>> GetOrderList(int UserId);
    }
}
