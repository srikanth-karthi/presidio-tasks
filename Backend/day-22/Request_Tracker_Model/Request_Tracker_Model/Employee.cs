﻿using Azure.Core;
using Request_Tracker_Model;
using System.ComponentModel.DataAnnotations;

namespace RequestTrackerModelLibrary
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public override string ToString()
        {
            return Id + " " + Name + " " + Role;
        }
        public virtual bool PasswordCheck(string password)
        {
            return this.Password == password;
        }
        public ICollection<Request> RequestsRaised { get; set; }//No effect on the table
        public ICollection<Request> RequestsClosed { get; set; }//No effect on the table

        public ICollection<RequestSolution> SolutionsProvided { get; set; }
        public ICollection<SolutionResposnse> ResponseGiven { get; set; }
        public ICollection<SolutionFeedback> FeedbacksGiven { get; set; }
    }
}