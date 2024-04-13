using static System.Runtime.InteropServices.JavaScript.JSType;

namespace card_validation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a card number");
            string card=Console.ReadLine();
            int[] nums = new int[16];

            for (int i = 0; i < 16; i++)
            {
                while (!int.TryParse(card[i].ToString(), out nums[i]) || card.Length != 16)
                {
                    Console.WriteLine("Please enter a valid 16-digit card number:");
                    card = Console.ReadLine();
                }
            }
            int sum = 0;
            Array.Reverse(nums);

            for (int i = 0; i < 16; i++)
            {
                    nums[i] = i % 2 != 0 ?nums[i] * 2:nums[i];
                    nums[i] = nums[i] > 9 ? nums[i] - 9 : nums[i];
    
                sum += nums[i];
            }

                if (sum%10==0)  Console.WriteLine("Yess this a valid card number:");
            else Console.WriteLine("Sorry this a Invalid card number:");






        }
    }
}
