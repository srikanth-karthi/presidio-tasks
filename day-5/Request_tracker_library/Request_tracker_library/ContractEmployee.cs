using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request_tracker_library
{
    public class ContractEmployee:Employee
    {
        public double WagesPerDay { get; set; }
        public string type = "contract";
        public ContractEmployee()
        {
        
            WagesPerDay = 0;
           // Console.WriteLine("Contract employee constructor");
        }
        public ContractEmployee(int id, string name, DateTime dateOfBirth, double salary, double wagesPerDay) : base(id, name, dateOfBirth, salary)
        {
           // Console.WriteLine("Contract Employee class prameterized constructor");

            WagesPerDay = wagesPerDay;
        }
        public override void BuildEmployeeFromConsole()
        {

            base.BuildEmployeeFromConsole();
            Console.WriteLine("Enter per day wages");
            WagesPerDay = Convert.ToDouble(Console.ReadLine());
            Salary = Convertsalary(WagesPerDay);
        }


        public override void PrintEmployeeDetails()
        {
            Console.WriteLine("Employee type: :" + type);
            base.PrintEmployeeDetails();
            Console.WriteLine("Employee Salary : Rs." + WagesPerDay);
        }

        private double Convertsalary(double WagesPerDay)
        {
            return 30 * WagesPerDay;
        }
    }


}
