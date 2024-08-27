namespace RequestTrackerApp.Models.DTO
{ 
        public class RequestsDto
        {
            public int RequestNumber { get; set; }
            public string RequestMessage { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime? ClosedDate { get; set; }
            public string RequestStatus { get; set; }
            public int RequestRaisedBy { get; set; }
            public string RaisedByEmployeeName { get; set; } // Assuming you want to include employee name
            public int? RequestClosedBy { get; set; }
            public string RequestClosedByEmployeeName { get; set; } // Assuming you want to include employee name

        }
    }
