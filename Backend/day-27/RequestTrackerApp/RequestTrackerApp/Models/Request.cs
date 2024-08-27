using RequestTrackerApp.Model;
using RequestTrackerApp.Model;
using System.ComponentModel.DataAnnotations;
namespace RequestTrackerApp.Model
{
    public class Requests
    {
        [Key]
        public int RequestNumber { get; set; }
        public string RequestMessage { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ClosedDate { get; set; } = null;
        public string RequestStatus { get; set; } = "active";

        public int RequestRaisedBy { get; set; }
        public Employee RaisedByEmployee { get; set; }

        public int? RequestClosedBy { get; set; } // Nullable int
        public Employee RequestClosedByEmployee { get; set; }

        public ICollection<RequestSolution> SolutionsProvided { get; set; }
        public ICollection<SolutionFeedback> FeedbackProvided { get; set; }
    }
}