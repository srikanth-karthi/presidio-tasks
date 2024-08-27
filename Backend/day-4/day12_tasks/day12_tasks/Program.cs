namespace day12_tasks
{
    internal class Program
    {
   
        static void Main(string[] args)
        {

            doctors createdoctor(int id)
            {
                doctors doc = new doctors(id);
                Console.WriteLine($"Please enter the Doctor{id} name ");
                doc.Name = Console.ReadLine();
                int Age;
                Console.WriteLine($"Please enter the Doctor_{id} Age ");
                while (!int.TryParse(Console.ReadLine(), out Age))
                {
                    Console.WriteLine("Please enter valid Age:");
                }
                doc.Age = Age;
                Console.WriteLine($"Please enter the Doctor{id} Qualification ");
                doc.Qualification = Console.ReadLine();
                Console.WriteLine($"Please enter the Doctor{id} Experience ");
                int experience;
                while (!int.TryParse(Console.ReadLine(), out experience))
                {
                    Console.WriteLine("Please enter valid Experience:");
                }
               
                doc.Experience = experience;
                Console.WriteLine($"Please enter the Doctor{id} Speciality ");
                doc.Speciality = Console.ReadLine();

                return doc;
            }

            Console.WriteLine("Enter How many doctors do you want to create:");
            int no_doctor=int.Parse(Console.ReadLine());
            doctors[] doctors = new doctors[no_doctor];
            for (int i = 0; i < no_doctor; i++)


            {
                doctors[i]=createdoctor(i+1);
            }
            Console.WriteLine("__________________________________________");
            Console.WriteLine("Here is the doctors List :");
            for (int i = 0; i < no_doctor; i++)
            {
                doctors[i].PrintdoctorsDetails();
            }

            Console.WriteLine("__________________________________________");
            Console.WriteLine("Enter Speciality :");
            string spec = Console.ReadLine();

            for (int i = 0; i < no_doctor; i++)
            {
                doctors[i].PrintdoctorsDetails(spec);
            }

        }
    }
}
