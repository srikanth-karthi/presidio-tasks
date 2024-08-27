using System.ComponentModel.DataAnnotations;

namespace RequestTrackerApp.Models.DTO
{
    public class RegisterDTO: LoginDTO
    {

        public string Name { get; set; }
        public string Role { get; set; }

        public  string State { get; set; }
    }
}
