
namespace _2_greatestofallnumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Findgeatestofallnumber();
        }

        private static void Findgeatestofallnumber()
        {
            int greatestnumber=0;
            int number;
            Console.WriteLine("Enter a number: ");
            while (int.TryParse(Console.ReadLine(), out number)  && number > 0)
            {
                Console.WriteLine("Enter a valid number: ");
                if(number > greatestnumber) greatestnumber=number;
            }
            Console.WriteLine($"The greatest number you Enter is:{greatestnumber} ");

        }
    }
}
