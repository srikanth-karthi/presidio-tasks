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
    public class AdminBL : IAdminBL
    {
        private readonly IRepository<int, Request> _requestRepository;
        private readonly IRepository<int, SolutionResposnse> _responseRepository;
        private readonly IRepository<int, SolutionFeedback> _feedbackRepository;
        private readonly IRepository<int, RequestSolution> _SolutionRepository;

        public AdminBL()
        {
            _requestRepository = new RequestRepository(new RequestTrackerContext());
            _SolutionRepository = new SolutionsRepository(new RequestTrackerContext());
            _responseRepository = new ResponseRepository(new RequestTrackerContext());
            _feedbackRepository = new FeedbackRepository(new RequestTrackerContext());
        }

        public async Task<bool> RaiseRequest(Request request)
        {
            await _requestRepository.Add(request);
            return true;
        }

        public async Task<string> ViewRequestStatus(int adminId)
        {
            var requests = await _requestRepository.GetAll();
            var adminRequests = requests.Where(r => r.RequestRaisedBy == adminId).ToList();
            return string.Join("\n", adminRequests.Select(r => $"Request ID: {r.RequestNumber}, Status: {r.RequestStatus}"));
        }

        public async Task<List<RequestSolution>> ViewSolutions(int adminId)
        {
            var solutions = await _SolutionRepository.GetAll();
           
            var adminSolutions = solutions.Where(s => s.SolvedBy == adminId).ToList();
            return adminSolutions;
        }

        public async Task<bool> GiveFeedback(SolutionFeedback feedback)
        {
            await _feedbackRepository.Add(feedback);
            return true;
        }

        public async Task<bool> RespondToSolution(SolutionResposnse response)
        {
       
            var solution = await _SolutionRepository.Get(response.SolutionId);

            if (solution == null || solution.SolvedBy != response.EmployeeId)
            {
    
                return false;
            }

            var existingResponse = await _responseRepository.Get(response.SolutionId);

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


        public async Task<bool> ProvideSolution(int requestId, string solutionMessage)
        {
            var response = new RequestSolution { RequestId = requestId, SolutionDescription = solutionMessage, SolvedBy = 101 };
            await _SolutionRepository.Add(response);
            return true;
        }

        public async Task<bool> MarkRequestAsClosed(int requestId)
        {
            var request = await _requestRepository.Get(requestId);
            if (request != null)
            {
                request.RequestStatus = "Closed";
                await _requestRepository.Update(request);
                return true;
            }
            return false;
        }

        public async Task<List<SolutionFeedback>> ViewFeedbacks(int adminId)
        {
            var feedbacks = await _feedbackRepository.GetAll();
            var adminFeedbacks = feedbacks.Where(f => f.FeedbackBy == adminId).ToList();
            return adminFeedbacks;
        }
    }
}
