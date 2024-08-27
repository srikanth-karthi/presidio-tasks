using System;

namespace _1_findthreedigit_repetedno
{
    internal class Program
    {
        void findrepetednums()
        {
            int count = 0;
            int[] nums = { 111, 222, 3325, 4135, 1355 };
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 100 && nums[i] < 1000)
                {
                    if (nums[i] % 111 == 0 && nums[i] != 0)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine($"The repeated three digits numbers are {count}");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.findrepetednums();
        }
    }
}
