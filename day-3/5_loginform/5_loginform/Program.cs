namespace _5_loginform
{
    internal class Program
    {
        static void Main()
        {
            string username = "ABC";
            string password = "123";
            int attempts = 3;

            while (attempts > 0)
            {
                Console.Write("Enter username: ");
                string inputUsername = Console.ReadLine();

                Console.Write("Enter password: ");
                string inputPassword = Console.ReadLine();

                if (inputUsername == username && inputPassword == password)
                {
                    Console.WriteLine("Login successful!");
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect username or password. Please try again!!.");
                    attempts=attempts-1;
                    Console.WriteLine($"You have {attempts} attempts remaining.\n");
                }
            }

            if (attempts == 0)
            {
                Console.WriteLine("You have exceeded the maximum number of attempts. Please try again later.");
            }
        }
    }
}
