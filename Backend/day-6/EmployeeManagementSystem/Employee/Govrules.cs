using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public interface IGovrules
    {
        double EmployeePF(double basicSalary);
        string LeaVeDetails();
        double GratuityAmount(float serviceCompleted, double basicSalary);
    }
}