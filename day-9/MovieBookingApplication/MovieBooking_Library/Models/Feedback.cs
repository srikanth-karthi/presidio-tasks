using System;

namespace MovieBooking_Library.Models
{
    public class Feedback
    {
        public string Key { get; set; }
        public string CustomerName { get; set; }
        public string Moviename { get; private set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

        public Feedback(string key, string customerName, int rating, string comments,string moviename)
        {
            Key = key;
            CustomerName = customerName;
            Moviename = moviename;
            Rating = rating;
            Comments = comments;
        }

        public override string ToString()
        {
            return $"Customer: {CustomerName}, Rating: {Rating}, Comments: {Comments}";
        }
    }
}
