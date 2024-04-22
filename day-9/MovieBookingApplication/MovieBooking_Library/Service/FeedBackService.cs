using MovieBooking_Library.Models;
using MovieBooking_Library.Repository;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library.Services
{
    public class FeedBackService : FeedBackRepository
    {
        private MovieRepository movieRepository;

        public FeedBackService()
        {
            movieRepository = new MovieRepository();

        }

        public void AddReview(string movieTitle, string username, string review, int rating)
        {
            if (string.IsNullOrWhiteSpace(review))
            {
                throw new UserValidationException("Review cannot be empty.");
            }


            Movie movie = movieRepository.Get(movieTitle);
            if (movie == null)
            {
                throw new ArgumentException($"Movie '{movieTitle}' not found.");
            }

            Add(new Feedback(GenerateFeedbackKey(), username, rating, review));
        }


    }
}
