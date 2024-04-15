namespace Day5_learning
{
    internal class Program
    {


      void  understandingcodeflow(int age)
        {
     
            if (age > 18) Console.WriteLine("you are able vote !!");
            else if (age == 18) Console.WriteLine("apply voter id");
            else Console.WriteLine("you are not able t0 vote");


            
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            int age = int.Parse(Console.ReadLine());   
            program.understandingcodeflow(age);

        }
    }
}
