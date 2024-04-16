



using System.Diagnostics;
using System.Xml.Linq;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public string Desg { get; set; }
        public double BasicSalary { get; set; }

        public Employee(int id,string name,string dept,string desig,double basicSalary)
        {
            EmpId = id;
            Name = name;
            Dept = dept;
            Desg = desig;
            BasicSalary= basicSalary;

        }


        public override string ToString()
        {
            return $"Employee Id: {EmpId}, Name: {Name}, Dept: {Dept}, Basic Salary: {BasicSalary}";
        }

    }
}
