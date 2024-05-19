using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestTrackerApp.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public string Name { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] HasCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(50)]
        public string State { get; set; } = "Inactive";



        public ICollection<Requests> RequestsRaised { get; set; }
        public ICollection<Requests> RequestsClosed { get; set; }
        public ICollection<RequestSolution> SolutionsProvided { get; set; }
        public ICollection<SolutionResposnse> ResponseGiven { get; set; }
        public ICollection<SolutionFeedback> FeedbacksGiven { get; set; }
    }
}
