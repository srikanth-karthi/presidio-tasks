using MovieBooking_Library.Models;
using System;
using System.Collections.Generic;

namespace MovieBooking_Library
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly Dictionary<string, User> userDictionary;

        public UserRepository()
        {
            userDictionary = new Dictionary<string, User>()
            {
                { "admin", new User("admin", "password") },
                  { "s", new User("s", "s") }
            };
        }

        public void Add(User item)
        {
            if (userDictionary.ContainsKey(item.Username))
            {
                throw new UserException($"{item.Username} already exists.");
            }
            userDictionary.Add(item.Username, item);
  
        }

        public User Delete(string key)
        {
            if (!userDictionary.ContainsKey(key))
            {
                throw new UserException($"User with username {key} not found.");
            }
            User user = userDictionary[key];
            userDictionary.Remove(key);
            return user;
        }

        public User Get(string key)
        {
            if (!userDictionary.ContainsKey(key))
            {
                throw new UserException($"User with username {key} not found.");
            }
            return userDictionary[key];
        }

        public Dictionary<string, User> GetAll()
        {
            return userDictionary;
        }

        public User Update(User item)
        {
            if (!userDictionary.ContainsKey(item.Username))
            {
                throw new UserException($"User with username {item.Username} not found.");
            }
            userDictionary[item.Username] = item;
            return item;
        }
    }
}
