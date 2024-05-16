using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingApp.Exceptions;
using PizzaOrderingApp.Interfaces;
using PizzaOrderingApp.Models.Dto;
using PizzaOrderingApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PizzaOrderingApp.Models.Dto.PlaceOrderDto;

namespace PizzaOrderingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            Console.WriteLine("ediy");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            try
            {
                var registeredUser = await _userService.Register(user);
                return Ok(new { message = "Login successful", registeredUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            try
            {
                var loggedInUser = await _userService.Login(user);
                return Ok(new { message = "Login successful", user = loggedInUser });
            }
            catch (UnauthorizedUserException unauth)
            {
                return StatusCode(401, $"An error occurred: {unauth.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("foodlist")]
        [Authorize]
        public async Task<IActionResult> GetFoodList()
        {
            try
            {
                var foodList = await _userService.GetFoodList();
                return Ok(foodList);
            }
            catch (PizzaNotFound ex)
            {
                return StatusCode(404, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpGet("orderlist/{userId}")]
        public async Task<IActionResult> GetOrderList(int userId)
        {
            try
            {
                var orderList = await _userService.GetOrderList(userId);
                return Ok(orderList);
            }
            catch (OrderNotFoundException ex)
            {
                return StatusCode(404, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("OrderPizza")]
        public async Task<IActionResult> OrderPizza([FromBody] PlaceOrderDto orderItems, int userId)
        {

            try
            {
                var orderList = await _userService.PlaceOrder(orderItems, userId);
                return Ok(orderList);
            }
            catch (OrderNotFoundException ex)
            {
                return StatusCode(404, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }



    
        }


    }
}
