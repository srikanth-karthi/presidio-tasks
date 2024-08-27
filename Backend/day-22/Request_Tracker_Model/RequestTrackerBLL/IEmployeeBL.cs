using Request_Tracker_Model;
using RequestTrackerModelLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface IEmployeeLoginBL
    {
        Task<Employee> Login(Employee employee);
        Task<Employee> Register(Employee employee);
        Task<bool> RaiseRequest(Request request);
        Task<string> ViewStatus(int requestId);
        Task<List<Request>> GetAllRequest(int employeeId);
        Task<List<RequestSolution>> GetSolutions(int requestId);
        Task<bool> GiveFeedback(SolutionFeedback feedback);
        Task<bool> RespondToSolution(SolutionResposnse response);
    }
}
