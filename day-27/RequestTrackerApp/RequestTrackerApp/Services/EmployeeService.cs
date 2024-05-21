using Azure;
using RequestTrackerApp.Context;
using RequestTrackerApp.Exceptions;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Interfaces;
using RequestTrackerApp.Model;
using RequestTrackerApp.Models.DTO;
using RequestTrackerApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerApp.Service
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<int, Requests> _requestRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<int, SolutionResposnse> _responseRepository;
        private readonly IRepository<int, SolutionFeedback> _feedbackRepository;
        private readonly IRepository<int, RequestSolution> _solutionRepository;

        public EmployeeService(IRepository<int, Requests> requestRepository,IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IRepository<int, Employee> employeeRepository, IRepository<int, RequestSolution> solutionRepository, IRepository<int, SolutionFeedback> feedbackRepository, IRepository<int, SolutionResposnse> responseRepository)
        {
            _employeeRepository = employeeRepository;
            _responseRepository = responseRepository;
            _feedbackRepository = feedbackRepository;
            _solutionRepository = solutionRepository;
            _requestRepository = requestRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponse> Login(LoginDTO employeeDto)
        {
            var employeeRequestRepository = (EmployeeRequestRepository)_employeeRepository;
            var employee = await employeeRequestRepository.GetByEmail(employeeDto.Email);


            if (employee == null)
            {
                throw new AuthenticationError($" {employeeDto.Email} Not Found.");
            }

            if (employee.State == "InActive")
            {
                throw new AuthenticationError($" {employeeDto.Email} Not Activated.");
            }

            using HMACSHA512 hmac = new(employee.HasCode);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(employeeDto.Password));

            if (ComparePassword(computedHash, employee.Password))
            {
                return new AuthResponse() { Email = employee.Email,State=employee.State, Name = employee.Name, Role = employee.Role, token= _tokenService.GenerateToken(employee) };
            }

            throw new AuthenticationError("Invalid email or password.");
        }

            private bool ComparePassword(byte[] computedHash, byte[] storedHash)
        {
            if (computedHash.Length != storedHash.Length) return false;

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<RegisterDTO> Register(RegisterDTO users)
        {
         

            var employeeRequestRepository = (EmployeeRequestRepository)_employeeRepository;
            var employee = await employeeRequestRepository.GetByEmail(users.Email);

            if (employee != null) throw new EmailAlreadyFoundException($" {employee.Email} Already  Registered.");
            
                HMACSHA512 hMACSHA = new HMACSHA512();
                await _employeeRepository.Add(new Employee
            {
                Name = users.Name,
                Email = users.Email,
                HasCode = hMACSHA.Key,
                Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(users.Password)),
                Role = users.Role,
                State = users.Role == "Admin" ? "Active" : "InActive"
                });
            users.Password = "";
            users.State = users.Role == "Admin" ? "Active" : "InActive";
            return users;

        }


        public async Task ActivateUser(int empId)
        {
            var employee = await _employeeRepository.Get(empId);

                employee.State = "Active";
                await _employeeRepository.Update(employee);
             

        }
        public async Task DeActivateUser(int empId)
        {
            var employee = await _employeeRepository.Get(empId);
   
                employee.State = "InActive";
                await _employeeRepository.Update(employee);

        }



        public async Task<bool> RaiseRequest(Requests request)
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

        public async Task<IList<RequestsDto>> GetAllRequest()
        {
            int employeeId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("eid")?.Value);
            string role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            await Console.Out.WriteLineAsync(role);
            IList<Requests> requests;

            if (role == "Admin")
            {
                requests = await _requestRepository.GetAll();
            }
            else
            {
                requests = await _requestRepository.GetAll();
                requests = requests.Where(r => r.RequestRaisedBy == employeeId).ToList();
            }

            return requests.Select(r => new RequestsDto
            {
                RequestNumber = r.RequestNumber,
                RequestMessage = r.RequestMessage,
                RequestDate = r.RequestDate,
                ClosedDate = r.ClosedDate,
                RequestStatus = r.RequestStatus,
                RequestRaisedBy = r.RequestRaisedBy,
                RaisedByEmployeeName = r.RaisedByEmployee?.Name, // Assuming Employee has a Name property
                RequestClosedBy = r.RequestClosedBy,
                RequestClosedByEmployeeName = r.RequestClosedByEmployee?.Name // Assuming Employee has a Name property
            }).ToList();
        }


        public async Task<List<RequestSolution>> GetSolutions(int requestId)
        {
            var solutions = await _solutionRepository.GetAll();
           
            return solutions.Where(s => s.RequestId == requestId).ToList();
        }

        public async Task<bool> GiveFeedback(SolutionFeedback feedback)
        {
            await _feedbackRepository.Add(feedback);
            return true;
        }
        public async Task<Employee> GetByEmail(string email)
        {
            var employeeRequestRepository = (EmployeeRequestRepository)_employeeRepository;
            return await employeeRequestRepository.GetByEmail(email);
        }

        public async Task<bool> RespondToSolution(SolutionResposnse response)
        {
            var existingResponse = await _responseRepository.GetAll();
            SolutionResposnse existResponse = existingResponse.FirstOrDefault(re => re.SolutionId == response.SolutionId);

            if (existResponse != null)
            {

                throw new ResponseAlreadyFoundException("Response already found.", existResponse);
            }

            await _responseRepository.Add(response);
            return true;
        }



 
    }
}
