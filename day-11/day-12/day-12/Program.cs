namespace day_12
{
    internal class Program
    {
        int GetResultFromDatabaseServer()
        {
            return new Random().Next();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();
            int number = program.GetResultFromDatabaseServer();
            Console.WriteLine("This is the random number from main" + new Random().Next());
            Console.WriteLine("This is the random number from server " + number);

        }
    }
}
