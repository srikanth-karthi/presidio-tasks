using MovieBooking_Library.Models;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library.Repository
{
    public class FeedBackRepository : IRepository<string, Feedback>
    {
        private readonly Dictionary<string, Feedback> feedbacks;

        public FeedBackRepository()
        {
            feedbacks = new Dictionary<string, Feedback>();
        }

        public void Add(Feedback feedback)
        {
            string key = GenerateFeedbackKey();
            feedbacks.Add(key, feedback);
       
        }

        public Feedback Delete(string key)
        {
            if (feedbacks.ContainsKey(key))
            {
                Feedback feedback = feedbacks[key];
                feedbacks.Remove(key);
                return feedback;
            }
            throw new KeyNotFoundException($"Feedback with key {key} not found.");
        }

        public Feedback Get(string key)
        {
            if (feedbacks.ContainsKey(key))
            {
                return feedbacks[key];
            }
            throw new KeyNotFoundException($"Feedback with key {key} not found.");
        }

        public Dictionary<string, Feedback> GetAll()
        {
            return feedbacks;
        }

        public Feedback Update(Feedback feedback)
        {
            if (feedbacks.ContainsKey(feedback.Key))
            {
                feedbacks[feedback.Key] = feedback;
                return feedback;
            }
            throw new KeyNotFoundException($"Feedback with key {feedback.Key} not found.");
        }

        protected string GenerateFeedbackKey()
        {
   
            return Guid.NewGuid().ToString();
        }
    }
}
