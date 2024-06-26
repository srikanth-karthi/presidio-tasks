﻿using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request_Tracker_Model
{
    public class SolutionFeedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public float Rating { get; set; }
        public string? Remarks { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public int FeedbackBy { get; set; }
        public Employee FeedbackByEmployee { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
