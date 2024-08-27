
using MovieBooking_Library.Models;
using MovieBooking_Library.Repository;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library.Service
{
    public class Movieservise
    {
        MovieRepository movieRepository;
        public Movieservise()
        {
            movieRepository = new MovieRepository();
        }




        private bool Adminauthentication()
        {
            if (UserService.loggedInUser == null || UserService.loggedInUser.Username != "admin")
            {
                return false;
            }
            return true;
        }

        public void AddMovie(string title, string genre, int duration, DateTime screeningTime, double price)
        {
            if (!Adminauthentication())
            {
                throw new UserException("Only admin can add movies.");
            }

            Movie newMovie = new Movie(title, genre, duration, screeningTime, price);

            movieRepository.Add(newMovie);

            Console.WriteLine($"Movie '{title}' added successfully.");

    
        }


        public Dictionary<string, Movie> CurrentMovieList()
        {
            Dictionary<string, Movie> moviesAfterDate = new Dictionary<string, Movie>();

            foreach (var movie in movieRepository.GetAll().Values)
            {
                if (movie.ScreeningTime > DateTime.Now)
                {
                    moviesAfterDate.Add(movie.Title, movie);
                }
            }

            return moviesAfterDate;
        }

        public Dictionary<string, Movie> ListAllMovies()
        {
            return movieRepository.GetAll();
        }
    }


}