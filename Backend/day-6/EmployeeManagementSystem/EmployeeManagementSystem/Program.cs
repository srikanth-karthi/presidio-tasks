namespace EmployeeManagementSystem
{
    internal class Program
    {
        void CreateEmployee()
        {

            Company amazonEmployee = new Company(101, "John Doe", "Development", "Manager", 50000);
            Console.WriteLine($"Employee: {amazonEmployee}");

            Console.WriteLine($"Gratuity Amount: {amazonEmployee.GratuityAmount(10, amazonEmployee.BasicSalary)}");
            Console.WriteLine($"Employee PF: {amazonEmployee.EmployeePF(amazonEmployee.BasicSalary)}");
            Console.WriteLine($"Leave Details:\n{amazonEmployee.LeaVeDetails()}\n");


            Google googleEmployee = new Google(102, "Jane Smith", "HR", "Associate", 60000);
            Console.WriteLine($"Employee: {googleEmployee}");
 
            Console.WriteLine($"Basic Salary: {googleEmployee.BasicSalary}");
            Console.WriteLine($"Gratuity Amount: {googleEmployee.GratuityAmount(18, googleEmployee.BasicSalary)}"); 
            Console.WriteLine($"Employee PF: {googleEmployee.EmployeePF(googleEmployee.BasicSalary)}");
            Console.WriteLine($"Leave Details:\n{googleEmployee.LeaVeDetails()}\n");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.CreateEmployee();
        }
    }
}
