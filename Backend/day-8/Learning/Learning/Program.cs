namespace Learning
{


    internal class Program
    {
        int Divide(int num1, int num2)
        {
            try
            {
                int result = num1 / num2;
                return result;
            }
            catch (Exception dbze)
            {
                Console.WriteLine(dbze);
            }
            finally
            {
                Console.WriteLine("Release the divide method resource");
            }
            Console.WriteLine("Your division did not go well");
            return 0;

        }

        static void Main(string[] args)
        {
            int num1, num2, result;
            try
            {
                num1 = Convert.ToInt32(Console.ReadLine());
                num2 = Convert.ToInt32(Console.ReadLine());
                result = new Program().Divide(num1, num2);
                Console.WriteLine(result);
            }
            catch (Exception fe)
            {
                Console.WriteLine(fe.Message);
                Console.WriteLine("The given data could not be converted to number.");
            }
            Console.WriteLine("Bye bye");
        }
    }
}

