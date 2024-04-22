using MovieBooking_Library.Models;
using MovieBooking_Library.Repository;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library.Services
{
    public class FeedBackService: FeedBackRepository
    {
        private MovieRepository movieRepository;
      
        public FeedBackService()
        {
            movieRepository = new MovieRepository();
         
        }

        public void AddReview(string movieTitle, string username, string review,int rating )
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


        public Feedback GetFeedback(string key)
        {
            return Get(key);
        }

        public Dictionary<string, Feedback> GetAllFeedbacks()
        {
            return GetAll();
        }

        public Feedback UpdateFeedback(string key, string customerName, string email, int rating, string comments)
        {
            Feedback existingFeedback = Get(key);
            existingFeedback.CustomerName = customerName;
            existingFeedback.Rating = rating;
            existingFeedback.Comments = comments;

            return Update(existingFeedback);
        }
        public Feedback GetFeedbackByCustomerName(string customerName)
        {
 
            var feedbacks = GetAll().Values;
            foreach (var feedback in feedbacks)
            {
                if (feedback.CustomerName.Equals(customerName))
                {
                    return feedback;
                }
            }
            throw new KeyNotFoundException($"Feedback for customer {customerName} not found.");
        }

        public Feedback DeleteFeedback(string key)
        {
            return Delete(key);
        }
    }
}
