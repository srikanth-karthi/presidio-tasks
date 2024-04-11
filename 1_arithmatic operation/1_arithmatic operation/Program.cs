



namespace _1_arithmatic_operation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Calculate();
           
        }

        private static void Calculate()
        {
           int num1= Getvalue(1);
            int num2 = Getvalue(2);
           Performoperation(num1,num2);
  
        }

   

        private static void Performoperation(int a,int b)
        {
            
            checked
            {
          
                Console.WriteLine($"the sum of {a} and {b} is {a+b}");
                Console.WriteLine($"the difference of {a} and {b} is {b-a}");
                Console.WriteLine($"the mutiply of {a} and {b} is {a * b}");
                Console.WriteLine($"the remainder of {a} and {b} is {a%b}");

            }
        }

        private static int  Getvalue(int number)
        {
            int num;
            Console.WriteLine($"Enter num {number}:");
            while(int.TryParse(Console.ReadLine(),out num)==false)
            {
                Console.WriteLine("Enter a valid number");
            }

            return num;

        }
    }
}
