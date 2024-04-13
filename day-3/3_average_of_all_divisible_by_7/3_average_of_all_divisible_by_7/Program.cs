using System;

namespace _3_average_of_all_divisible_by_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FindAverageOfNumbersDivisibleBySeven();
        }

        private static void FindAverageOfNumbersDivisibleBySeven()
        {
            int sum = 0;
            int count = 0;
            int number = 0;
            Console.WriteLine("Enter a number");
            while (int.TryParse(Console.ReadLine(), out number) && number > 0)
            {
             
                if (number % 7 == 0)
                {
                    count++;
                    sum += number;
                }

                Console.WriteLine("Enter a number");
            }

            if (count > 0)
            {
                int result = sum / count;
                Console.WriteLine($"the Average of number that are divisible by 7 is :{result}");
            }
            else
            {
                Console.WriteLine("No numbers divisible by 7 were entered.");
            }
        }
    }
}
