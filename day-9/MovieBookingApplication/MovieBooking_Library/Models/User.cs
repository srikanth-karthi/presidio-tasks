using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking_Library.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Booking> TicketHistory { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            TicketHistory = new List<Booking>();
        }

        public void AddToTicketHistory(Booking booking)
        {
            TicketHistory.Add(booking);
        }
    }

}
