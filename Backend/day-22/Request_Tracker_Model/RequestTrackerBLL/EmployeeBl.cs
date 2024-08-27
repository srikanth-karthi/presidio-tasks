using Request_Tracker_Model;
using RequestTrackerDAL;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class EmployeeLoginBL : IEmployeeLoginBL
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<int, Request> _requestRepository;
        private readonly ResponseRepository  _responseRepository;
        private readonly IRepository<int, SolutionFeedback> _feedbackRepository;
        private readonly IRepository<int, RequestSolution> _SolutionRepository;

        public EmployeeLoginBL()
        {
            _employeeRepository = new EmployeeRequestRepository(new RequestTrackerContext());
            _requestRepository = new RequestRepository(new RequestTrackerContext());
            _responseRepository = new ResponseRepository(new RequestTrackerContext());
            _feedbackRepository = new FeedbackRepository(new RequestTrackerContext());
            _SolutionRepository = new SolutionsRepository(new RequestTrackerContext());
        }

        public async Task<Employee> Login(Employee employee)
        {
            var emp = await _employeeRepository.Get(employee.Id);
            if (emp != null && emp.Password == employee.Password)
            {
                return emp;
            }
            return null;
        }

        public async Task<bool> RaiseRequest(Request request)
        {
            var emp = await _employeeRepository.Get(request.RequestRaisedBy);
            if (emp != null)
            {
                await _requestRepository.Add(request);
                return true;
            }
            return false;
        }

        public async Task<string> ViewStatus(int requestId)
        {
            var request = await _requestRepository.Get(requestId);
            return request.RequestStatus;
        }

        public async Task<List<Request>> GetAllRequest(int employeeId)
        {
            var requests = await _requestRepository.GetAll();
            return requests.Where(r => r.RequestRaisedBy == employeeId).ToList();
        }

        public async Task<List<RequestSolution>> GetSolutions(int requestId)
        {
            var solutions = await _SolutionRepository.GetAll();
           
            return solutions.Where(s => s.RequestId == requestId).ToList();
        }

        public async Task<bool> GiveFeedback(SolutionFeedback feedback)
        {
            await _feedbackRepository.Add(feedback);
            return true;
        }

        public async Task<bool> RespondToSolution(SolutionResposnse response)
        {
            var existingResponse = await _responseRepository.GetBySolution(response.SolutionId);

            if (existingResponse != null)
            {
                Console.WriteLine("This solution has already been responded to.");
                Console.WriteLine("Do you want to update the response? (Y/N)");
                var updateResponse = Console.ReadLine();

                if (updateResponse == "Y")
                {
                    existingResponse.Response = response.Response;
                    await _responseRepository.Update(existingResponse);
                    Console.WriteLine("Response updated successfully.");
                    return true;
                }
                else
                {
                    return false;
                }
            }

            await _responseRepository.Add(response);
            return true;
        }

        public async Task<Employee> Register(Employee employee)
        {
            var result = await _employeeRepository.Add(employee);
            return result;
        }

 
    }
}
