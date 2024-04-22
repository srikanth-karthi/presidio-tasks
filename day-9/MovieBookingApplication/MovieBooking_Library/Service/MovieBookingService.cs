using MovieBooking_Library.Models;
using MovieBooking_Library.Repository;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library.Service
{
    public class MovieBookingService
    {
        private readonly MovieRepository movieRepository;

        public MovieBookingService()
        {
            movieRepository = new MovieRepository();
        }

        private bool UserAuthentication()
        {
            return UserService.loggedInUser != null;
        }

        public void BookTicket(string movieName)
        {
            if (!UserAuthentication())
            {
                throw new UserValidationException("User must be logged in to book tickets.");
            }
     
            if (!MovieRepository.movies.ContainsKey(movieName))
            {
                throw new ArgumentException($"Movie '{movieName}' not exists.");
            }

            Movie selectedMovie = MovieRepository.movies[movieName];

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
                    BookTicket(movieName);
                    return;
                }
                else
                {
                    selectedMovie.Seats[seatNumber - 1] = true;
                }
            }

            double totalCost = numberOfSeats * selectedMovie.Price;
            UserService.loggedInUser.AddToTicketHistory(new Booking(selectedMovie, selectedMovie.ScreeningTime, UserService.loggedInUser.Username, numberOfSeats, totalCost, GenerateBookingReference()));
        }

        private int GenerateBookingReference()
        {
            return DateTime.Now.GetHashCode();
        }
    }
}
