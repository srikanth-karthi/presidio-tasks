using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opps
{
    internal class Employee
    {

       public string name {  get; set; }
        /// <summary>
        /// the work method is is used to define the work of the employee
        /// <param name="id">id as int</param>
        /// </summary>
        public void work()
        {
            Console.WriteLine("it works");
        }

    }
}
