namespace Opps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

  
            Employee employee = new Employee();
            employee.name = "Srikanth";
            Console.WriteLine(employee.name);
            employee.work();
        }
    }
}
