using MovieBooking_Library.Models;
using MovieBooking_Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking_Library.Service
{
    public class MovieBookingService : MovieRepository
    {

        readonly private Dictionary<string, Booking> Booking;

        public MovieBookingService()
        {
            new Dictionary<string, Booking>();
        }
        private bool Userauthentication()
        {
            if (UserService.loggedInUser == null)
            {
                return false;
            }
            return true;
        }


        public void BookTicket(string movieName)
        {
            if (!Userauthentication())
            {
                throw new UserValidationException("User must be logged in to book tickets.");
            }

            if (!movies.ContainsKey(movieName))
            {
                throw new ArgumentException($"Movie '{movieName}' not found.");
            }

            Movie selectedMovie = movies[movieName];
            selectedMovie.PrintSeats();

            Console.Write("Enter number of seats: ");
            int numberOfSeats;
            while (!int.TryParse(Console.ReadLine(), out numberOfSeats) || numberOfSeats < 1 || numberOfSeats > selectedMovie.Seats.Length)
            {
                Console.WriteLine("Invalid number of seats. Please enter a valid number.");
            }
            for (int i = 0; i < numberOfSeats; i++)
            {
                Console.Write($"Enter seat number {i + 1}: ");
                int seatNumber;
                while (!int.TryParse(Console.ReadLine(), out seatNumber) || seatNumber < 1 || seatNumber > selectedMovie.Seats.Length)
                {
                    Console.WriteLine("Invalid seat number or seat already selected. Please enter a valid seat number.");
                }

                if (selectedMovie.Seats[seatNumber - 1])
                {
                    Console.WriteLine($"Seat {seatNumber} is already booked. Please select another seat.");
                    BookTicket(movieName); // Recursive call to re-prompt for seat number
                }
                else
                {
                    selectedMovie.Seats[seatNumber - 1] = true;
                }
            }



            double totalCost = numberOfSeats * selectedMovie.Price; // Assuming ticket cost is $10 per ticket

            // Add the booking to the logged-in user's TicketHistory
            UserService.loggedInUser.AddToTicketHistory(new Booking(selectedMovie, selectedMovie.ScreeningTime, UserService.loggedInUser.Username, numberOfSeats, totalCost, GenerateBookingReference()));
        }

        private int GenerateBookingReference()
        {
            return DateTime.Now.GetHashCode();
        }
    }
}
