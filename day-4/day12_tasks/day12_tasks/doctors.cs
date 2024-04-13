using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day12_tasks
{
    internal class doctors
    {
        public int Id { get;  set; }
        public String Name { get;  set; }
        public int Age { get;  set; }
        public int Experience { get;  set; }
        public String Qualification { get;  set; }
        public String Speciality { get;  set; }
        public doctors(int id ) {
            Id = id;
        }
        public void PrintdoctorsDetails(string? Spec = null)
        {
            if (Spec == null || Spec == Speciality.ToString())
            {
                Console.WriteLine($"doctor Id\t:\t{Id}");
                Console.WriteLine($"doctor name\t:\t{Name}");
                Console.WriteLine($"doctor Age \t:\t{Age} years");
                Console.WriteLine($"doctor Qualification\t:\t.{Qualification}");
                Console.WriteLine($"doctor Speciality\t:\t{Speciality}");
                Console.WriteLine($"doctor Experience\t:\t{Experience} years");
            }
        }


    }
}
