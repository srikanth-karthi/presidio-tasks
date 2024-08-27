using System;

namespace MovieBooking_Library.Models
{
    public class Booking
    {
        public Movie SelectedMovie { get; set; }
        public DateTime ScreeningTime { get; set; }
        public string CustomerName { get; set; }
        public string ContactInfo { get; set; }
        public int NumberOfTickets { get; set; }
        public double TotalCost { get; set; }
        public int BookingReference { get; set; }

        public Booking(Movie selectedMovie, DateTime screeningTime, string customerName,  int numberOfTickets, double totalCost, int bookingReference)
        {
            SelectedMovie = selectedMovie;
            ScreeningTime = screeningTime;
            CustomerName = customerName;

            NumberOfTickets = numberOfTickets;
            TotalCost = totalCost;
            BookingReference = bookingReference;
        }

        public override string ToString()
        {
            return $"Movie: {SelectedMovie.Title}\nScreening Time: {ScreeningTime}\nCustomer Name: {CustomerName}\nNumber of Tickets: {NumberOfTickets}\nTotal Cost: {TotalCost}\nBooking Reference: {BookingReference}";
        }

    }
}
