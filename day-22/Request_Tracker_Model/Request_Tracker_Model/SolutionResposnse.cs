using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request_Tracker_Model
{
    public class SolutionResposnse
    {
        [Key]
        public int ResponseId { get; set; }
        public String Response { get; set; }

        public int SolutionId { get; set; }

        public  RequestSolution RequestSolution { get; set; }



        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }




    }
}
