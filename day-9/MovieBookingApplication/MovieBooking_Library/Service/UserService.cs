using System;
using System.Collections;
using System.Collections.Generic;
using MovieBooking_Library.Models;

namespace MovieBooking_Library.Service
{

    public class UserService
    {
        public static User loggedInUser = null;
        private UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public User CreateUser(User newUser)
        {
            return userRepository.Add(newUser);
        }

        public User LoginUser(string username, string password)
        {
            User user = userRepository.Get(username);
            if (user.Password == password)
            {
                Console.WriteLine($"Welcome, {username}!");
                loggedInUser=user;
                return user;
            }

            throw new UserException("Invalid username or password.");
        }

        public void Userlogout()
        {

            loggedInUser = null;
        }
    }
}





