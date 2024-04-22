using MovieBooking_Library;
using MovieBooking_Library.Models;
using MovieBooking_Library.Service;
using MovieBooking_Library.Services;
using System;
using System.Collections.Generic;

namespace MovieBookingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userRepo = new UserService();
            var movieRepo = new Movieservise();
            var movieBooking = new MovieBookingService();
            var feedbackService = new FeedBackService();
            User loggedInUser = null;

            Console.WriteLine("Welcome to the Movie Booking System!");

            while (true)
            {
                if (loggedInUser == null)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Exit");
                    Console.Write("Enter your choice: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter username: ");
                            string username = Console.ReadLine();
                            Console.Write("Enter password: ");
                            string password = Console.ReadLine();

                            try
                            {
                                loggedInUser = userRepo.LoginUser(username, password);
                                Console.WriteLine("Login successful!");
                            }
                            catch (UserException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case "2":
                            Console.Write("Enter username: ");
                            string newUsername = Console.ReadLine();
                            Console.Write("Enter password: ");
                            string newPassword = Console.ReadLine();

                            try
                            {
                                var newUser = new User(newUsername, newPassword);
                                userRepo.CreateUser(newUser);
                                Console.WriteLine("User registered successfully!");
                                Console.WriteLine("Please Login !");
                            }
                            catch (UserException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case "3":
                            Console.WriteLine("Exiting application...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else if (loggedInUser.Username == "admin")
                {
                    while (true)
                    {
                        Console.WriteLine("\nAdmin Menu:");
                        Console.WriteLine("1. Add Movie");
                        Console.WriteLine("2. List Movies");
                        Console.WriteLine("3. Logout");
                        Console.Write("Enter your choice: ");
                        string adminChoice = Console.ReadLine();

                        switch (adminChoice)
                        {
                            case "1":
                                Console.Write("Enter movie title: ");
                                string title = Console.ReadLine();
                                Console.Write("Enter genre: ");
                                string genre = Console.ReadLine();
                                Console.Write("Enter duration (in minutes): ");
                                int duration = int.Parse(Console.ReadLine());
                                Console.Write("Enter screening time (e.g., 'yyyy-MM-dd HH:mm'): ");
                                DateTime screeningTime = DateTime.Parse(Console.ReadLine());
                                Console.Write("Enter price Rs: ");
                                double price = Convert.ToDouble(Console.ReadLine());

                                try
                                {
                                    movieRepo.AddMovie(title, genre, duration, screeningTime, price);
                            
                                    Console.WriteLine("Movie added successfully!");
                                }
                                catch (UserException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            case "2":
                                Dictionary<string, Movie> allMovies = movieRepo.ListAllMovies();
                                Console.WriteLine("\nList of Movies:");
                                foreach (var movie in allMovies)
                                {
                                    Console.WriteLine(movie);
                                }
                                break;

                            case "3":
                                loggedInUser = null;
                                Console.WriteLine("Logged out successfully!");
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }

                        if (adminChoice == "3")
                        {
                            break;
                        }
                    }
                }
                else if (loggedInUser.Username != "admin")
                {
                    Console.WriteLine("\nUser Menu:");
                    Console.WriteLine("1. List Movies");
                    Console.WriteLine("2. Book Ticket");
                    Console.WriteLine("3. Ticket History");
                    Console.WriteLine("4. Review Movie");
             
                    Console.WriteLine("5. View Feedback");
                    Console.WriteLine("6. Logout");
                    Console.Write("Enter your choice: ");
                    string userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            Dictionary<string, Movie> allMovies = movieRepo.CurrentMovieList();
                            Console.WriteLine("\nList of Movies:");
                            foreach (var movie in allMovies)
                            {
                                Console.WriteLine(movie);
                            }
                            break;

                        case "2":
                            Console.Write("Enter the name of the movie you want to book a ticket for: ");
                            string movieName = Console.ReadLine();
                            try
                            {
                                movieBooking.BookTicket(movieName);
                                Console.WriteLine("Ticket booked successfully!");
                            }
                            catch (UserValidationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case "3":
                            Console.WriteLine("Ticket History:");
                            foreach (var booking in loggedInUser.TicketHistory)
                            {
                                Console.WriteLine($"Movie: {booking.SelectedMovie.Title}, Screening Time: {booking.ScreeningTime}, Number of Tickets: {booking.NumberOfTickets}, Total Cost: {booking.TotalCost}");
                            }
                            break;

                        case "4":
                            Console.WriteLine("Enter your feedback:");
                            Console.Write("Enter the name of the movie you want to Review: ");
                            string MovieName = Console.ReadLine();
                            Console.Write("Rating (1-5): ");
                            int rating;
                            while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                            {
                                Console.WriteLine("Invalid rating. Please enter a number between 1 and 5.");
                            }

                            Console.Write("Comments: ");
                            string comments = Console.ReadLine();

                            try
                            {
                                feedbackService.AddReview(MovieName, loggedInUser.Username, comments, rating);
                                Console.WriteLine("Feedback submitted successfully!");
                            }
                            catch (UserValidationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case "6":
                            loggedInUser = null;
                            Console.WriteLine("Logged out successfully!");
                            break;

                     
                        case "5":
                            try
                            {
                                Console.WriteLine("Feedback:");
                                foreach (var feedback in feedbackService.GetFeedbackByCustomerName(loggedInUser.Username))
                                {
                                    Console.WriteLine($"Movie: {feedback.Moviename}, User: {feedback.CustomerName}, Rating: {feedback.Rating}, Comments: {feedback.Comments}");
                                }
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;


                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (userChoice == "6")
                    {
                        break;
                    }
                }
            }
        }
    }
}
