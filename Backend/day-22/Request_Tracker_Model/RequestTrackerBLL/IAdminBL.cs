using Request_Tracker_Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface IAdminBL
    {
        Task<bool> RaiseRequest(Request request);
        Task<string> ViewRequestStatus(int adminId);
        Task<List<RequestSolution>> ViewSolutions(int adminId);
        Task<bool> GiveFeedback(SolutionFeedback feedback);
        Task<bool> RespondToSolution(SolutionResposnse response);
        Task<bool> ProvideSolution(int requestId, string solutionMessage);
        Task<bool> MarkRequestAsClosed(int requestId);
        Task<List<SolutionFeedback>> ViewFeedbacks(int adminId);
    }
}
