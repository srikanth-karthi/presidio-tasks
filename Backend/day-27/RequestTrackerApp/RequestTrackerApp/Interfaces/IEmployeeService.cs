using RequestTrackerApp.Model;
using RequestTrackerApp.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestTrackerApp.Interface
{
    public interface IEmployeeService
    {
        Task<AuthResponse> Login(LoginDTO employeeDto);
        Task<RegisterDTO> Register(RegisterDTO users);
        Task ActivateUser(int empId);
        Task DeActivateUser(int empId);
        Task<bool> RaiseRequest(Requests request);
        Task<string> ViewStatus(int requestId);
        Task<IList<RequestsDto>> GetAllRequest();
        Task<List<RequestSolution>> GetSolutions(int requestId);
        Task<bool> GiveFeedback(SolutionFeedback feedback);
        Task<bool> RespondToSolution(SolutionResposnse response);
        Task<Employee> GetByEmail(string email);
    }
}
