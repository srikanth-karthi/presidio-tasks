namespace _4_lengthof_username
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            int length = name.Length;
            Console.WriteLine($"Your name has {length} characters.");
        }
    }
}
