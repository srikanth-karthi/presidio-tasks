using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestTrackerApp.Exceptions;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Interfaces;
using RequestTrackerApp.Models.DTO;
using RequestTrackerApp.Service;

namespace RequestTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            try
            {
                var registeredUser = await _EmployeeService.Register(user);
                return Ok(new { message = "Login successful", registeredUser });
            }
            catch (EmailAlreadyFoundException Emailfound)
            {
                return StatusCode(401, $"An error occurred: {Emailfound.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("GetRequests")]
        [Authorize]
        public async Task<IActionResult> GetAllRequest()
        {
            try
            {
                var AllRequest = await _EmployeeService.GetAllRequest();
                return Ok(new { message = "Request Data", AllRequest });
            }
            catch (RequestNotFound Rnf)
            {
                return StatusCode(401, $"No Request found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("login")]

     
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            try
            {
                var loggedInUser = await _EmployeeService.Login(user);
                return Ok(new { message = "Login successful", user = loggedInUser });
            }
            catch (AuthenticationError unauth)
            {
                return StatusCode(401, $"An error occurred: {unauth.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("ActivteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivteUser(int userId)
        {
            try
            {
                await _EmployeeService.ActivateUser(userId);
                return Ok(new { message = "User Activated Sucessfully" });
            }
            catch (EmployeeNotFound unauth)
            {
                return StatusCode(401, $"An error occurred: {unauth.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("DeActivteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeActivteUser(int userId)
        {
            try
            {
                await _EmployeeService.DeActivateUser(userId);
                return Ok(new { message = "User DeActivated Sucessfully" });
            }
            catch (EmployeeNotFound unauth)
            {
                return StatusCode(401, $"An error occurred: {unauth.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
