using Request_Tracker_Model;
using RequestTrackerBLLibrary;
using RequestTrackerModelLibrary;
using System;

namespace RequestTrackerFEAPP
{
    internal class Program
    {
        private readonly IEmployeeLoginBL _employeeLoginBL;
        private readonly AdminBL _adminBL;

        public Program()
        {
            _employeeLoginBL = new EmployeeLoginBL();
            _adminBL = new AdminBL();
        }

        async Task Login()
        {
            Console.WriteLine("Please enter Employee Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine() ;
       
            Employee employee = new Employee() { Password = password, Id = id };
                    employee = await _employeeLoginBL.Login(employee);
            if (employee!=null)
            {
                Console.WriteLine("Login Success");
                await DisplayMenu(employee);
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
        }

        async Task DisplayMenu(Employee employee)
        {
            await Console.Out.WriteLineAsync(employee.Role);
            if (employee.Role=="Admin")
            {
                await DisplayAdminMenu(employee.Id);
            }
            else
            {
                await DisplayEmployeeMenu(employee.Id);
            }
        }

        async Task DisplayEmployeeMenu(int employeeId)
        {
            while (true)
            {
                Console.WriteLine("1. Raise Request");
                Console.WriteLine("2. View My Requests");
                Console.WriteLine("3. View Request Status");
                Console.WriteLine("4. View Solutions");
                Console.WriteLine("5. Give Feedback");
                Console.WriteLine("6. Respond to Solution");
                Console.WriteLine("7. Logout");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await RaiseRequest(employeeId);
                        break;
                    case 2:
                        await ViewMyRequests(employeeId);
                        break;
                    case 3:
                        await ViewRequestStatus();
                        break;
                    case 4:
                        await ViewSolutions();
                        break;
                    case 5:
                        await GiveFeedback(employeeId);
                        break;
                    case 6:
                        await RespondToSolution(employeeId);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        async Task RaiseRequest(int employeeId)
        {
            Console.WriteLine("Enter your request message:");
            string message = Console.ReadLine();

            Request request = new Request() { RequestMessage = message, RequestRaisedBy = employeeId };
            bool result = await _employeeLoginBL.RaiseRequest(request);

            if (result)
            {
                Console.WriteLine("Request raised successfully.");
            }
            else
            {
                Console.WriteLine("Failed to raise request. Please try again.");
            }
        }

        async Task ViewMyRequests(int employeeId)
        {
            var requests = await _employeeLoginBL.GetAllRequest(employeeId);
            foreach (var request in requests)
            {
                Console.WriteLine($"Request ID: {request.RequestNumber}, Message: {request.RequestMessage}");
            }
        }

        async Task ViewRequestStatus()
        {
            Console.WriteLine("Enter the request ID:");
            int requestId;
            if (!int.TryParse(Console.ReadLine(), out requestId))
            {
                Console.WriteLine("Invalid request ID. Please try again.");
                return;
            }

            string status = await _employeeLoginBL.ViewStatus(requestId);
            Console.WriteLine($"Request Status: {status}");
        }

        async Task ViewSolutions()
        {
            Console.WriteLine("Enter the Request ID:");
            int reqId;
            if (!int.TryParse(Console.ReadLine(), out reqId))
            {
                Console.WriteLine("Invalid solution ID. Please try again.");
                return;
            }
            var solutions = await _employeeLoginBL.GetSolutions(reqId);
            foreach (var solution in solutions)
            {
                Console.WriteLine($"Solution ID: {solution.SolutionId}, Message: {solution.SolutionDescription}");
            }
        }

        async Task GiveFeedback(int employeeId)
        {
            Console.WriteLine("Enter your feedback message:");
            string message = Console.ReadLine();

            SolutionFeedback feedback = new SolutionFeedback() { Remarks = message, FeedbackBy = employeeId, Rating = 5, RequestId = 1 };
            bool result = await _employeeLoginBL.GiveFeedback(feedback);

            if (result)
            {
                Console.WriteLine("Feedback submitted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to submit feedback. Please try again.");
            }
        }

        async Task RespondToSolution(int employeeId)
        {
            Console.WriteLine("Enter the solution ID:");
            int solutionId;
            if (!int.TryParse(Console.ReadLine(), out solutionId))
            {
                Console.WriteLine("Invalid solution ID. Please try again.");
                return;
            }

            Console.WriteLine("Enter your response message:");
            string message = Console.ReadLine();
            SolutionResposnse response = new SolutionResposnse() { Response = message, SolutionId = solutionId, EmployeeId = employeeId };
            bool result = await _employeeLoginBL.RespondToSolution(response);

            if (result)
            {
                Console.WriteLine("Response submitted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to submit response. Please try again.");
            }
        }

        async Task DisplayAdminMenu(int adminId)
        {
            while (true)
            {
                Console.WriteLine("1. Raise Request");
                Console.WriteLine("2. View Request Status");
                Console.WriteLine("3. View Solutions");
                Console.WriteLine("4. Give Feedback");
                Console.WriteLine("5. Respond to Solution");
                Console.WriteLine("6. Provide Solution");
                Console.WriteLine("7. Mark Request as Closed");
                Console.WriteLine("8. View Feedbacks");
                Console.WriteLine("9. Logout");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await RaiseRequest(adminId);
                        break;
                    case 2:
                        await ViewRequestStatus();
                        break;
                    case 3:
                        await ViewSolutions();
                        break;
                    case 4:
                        await GiveFeedback(adminId);
                        break;
                    case 5:
                        await RespondToSolution(adminId);
                        break;
                    case 6:
                        await ProvideSolution(adminId);
                        break;
                    case 7:
                        await MarkRequestAsClosed();
                        break;
                    case 8:
                        await ViewFeedbacks(adminId);
                        break;
                    case 9:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        async Task ProvideSolution(int adminId)
        {
            Console.WriteLine("Enter the request ID:");
            int requestId;
            if (!int.TryParse(Console.ReadLine(), out requestId))
            {
                Console.WriteLine("Invalid request ID. Please try again.");
                return;
            }

            Console.WriteLine("Enter the solution message:");
            string solutionMessage = Console.ReadLine();

            bool result = await _adminBL.ProvideSolution(requestId, solutionMessage);

            if (result)
            {
                Console.WriteLine("Solution provided successfully.");
            }
            else
            {
                Console.WriteLine("Failed to provide solution. Please try again.");
            }
        }

        async Task MarkRequestAsClosed()
        {
            Console.WriteLine("Enter the request ID:");
            int requestId;
            if (!int.TryParse(Console.ReadLine(), out requestId))
            {
                Console.WriteLine("Invalid request ID. Please try again.");
                return;
            }

            bool result = await _adminBL.MarkRequestAsClosed(requestId);

            if (result)
            {
                Console.WriteLine("Request marked as closed.");
            }
            else
            {
                Console.WriteLine("Failed to mark request as closed. Please try again.");
            }
        }

        async Task ViewFeedbacks(int adminId)
        {
            var feedbacks = await _adminBL.ViewFeedbacks(adminId);
            foreach (var feedback in feedbacks)
            {
                Console.WriteLine($"Feedback ID: {feedback.FeedbackId}, Remarks: {feedback.Remarks}, Rating: {feedback.Rating}");
            }
        }

        static async Task Main(string[] args)
        {
            await new Program().Login();
        }
    }
}
