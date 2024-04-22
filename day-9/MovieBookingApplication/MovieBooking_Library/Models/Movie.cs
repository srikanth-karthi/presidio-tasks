using System;

namespace MovieBooking_Library.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime ScreeningTime { get; set; }
        public bool[] Seats { get; set; }
        public double Price { get; set; }

        // Parameterized constructor
        public Movie(string title, string genre, int duration, DateTime screeningTime, double price)
        {
            Title = title;
            Genre = genre;
            Duration = duration;
            ScreeningTime = screeningTime;
            Seats = new bool[50]; // 50 seats in the theater
            Price = price;
        }

        public void PrintSeats()
        {
            Console.WriteLine("\nSeats in the theater:");
            for (int i = 0; i < Seats.Length; i++)
            {
                if (Seats[i])
                {
                    Console.Write($"X{i + 1} "); 
                }
                else
                {
                    Console.Write($"_{i + 1} "); 
                }

                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }


        public override string ToString()
        {
            return $"Title: {Title}, Genre: {Genre}, Duration: {Duration} minutes, Screening Time: {ScreeningTime}, Price: {Price}";
        }
    }
}
