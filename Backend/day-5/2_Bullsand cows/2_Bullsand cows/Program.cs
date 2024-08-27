
namespace _2_Bullsand_cows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cowsandbulls();
        }

        private static void Cowsandbulls()
        {
            int cow = 0,bull=0;
            Console.WriteLine("Enter a word for the game:");
            string word=Console.ReadLine();
            while(word.Length!=4)
            {
                Console.WriteLine("pls Enter 4 character:");
                word = Console.ReadLine();
            }


            Console.WriteLine("***********************");
            Console.WriteLine("START GAME");
            while (cow != 4)
            {
                cow = 0; bull = 0;
                Console.WriteLine("guess the 4 char word !!");
                string guess = Console.ReadLine();
                if (guess.Length != 4)
                {
                    Console.WriteLine("Please enter a 4-character word.");
                    continue;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (word[i] == guess[j] && i == j) cow++;
                        else if (word[i] == guess[j] ) bull++;

                    }
                }
                Console.WriteLine($"cow is {cow}, bull is {bull}");
            }
            Console.WriteLine($"You  won  !!!");



        }
    }
}
