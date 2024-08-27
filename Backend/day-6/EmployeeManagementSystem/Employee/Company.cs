
namespace EmployeeManagementSystem
{
    public class Company : Employee, IGovrules
    {
        public Company(int id, string name, string dept, string desig, double basicSalary)
            : base(id, name, dept, desig, basicSalary)
        {
        }



        public double EmployeePF(double basicSalary)
        {
            double eps = 0.0833 * basicSalary;
            double pf = 0.0367 * basicSalary;
            double basicpf = 0.12 * basicSalary;
            return eps + pf + basicpf;
        }



        public double GratuityAmount(float serviceCompleted, double basicSalary)
        {
            if (serviceCompleted > 5.0 && serviceCompleted <= 10.0)
                return basicSalary;
            else if (serviceCompleted > 10.0 && serviceCompleted <= 20.0)
                return 2 * basicSalary;
            else if (serviceCompleted > 20.0)
                return 3 * basicSalary;
            else
                return 0;
        }

        public string LeaVeDetails()
        {
            return "1 day of Casual Leave per month\n12 days of Sick Leave per year\n10 days of Privilege Leave per year";
        }
    }
    public class Google : Employee, IGovrules
    {
        public Google(int id, string name, string dept, string desig, double basicSalary)
            : base(id, name, dept, desig, basicSalary)
        {
        }

        public double EmployeePF(double basicSalary)
        {
            double employeeContribution = 0.12 * basicSalary;
            double employerContribution = 0.12 * basicSalary;
            return employeeContribution + employerContribution;
        }

        public double GratuityAmount(float serviceCompleted, double basicSalary)
        {
        
            return 0;
        }

        public string LeaVeDetails()
        {
            return "2 days of Casual Leave per month\n5 days of Sick Leave per year\n5 days of Privilege Leave per year";
        }
    }

}