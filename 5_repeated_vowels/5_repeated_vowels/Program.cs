namespace _5_repeated_vowels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a String:");
            String str=Console.ReadLine();
            int count = 0;
            Console.WriteLine(str.Length);
            int finalcount_count = int.MaxValue;
            String result="";
            String finalresult = "";
            str.ToLower().Trim();
            for(int i = 0;i<str.Length;i++)
            {
                if (str[i] =='a' || str[i] == 'e' || str[i] == 'i' || str[i] == 'o' || str[i] == 'u')
                {
                    count++;
             
                }
              
                if (str[i] == ',' || i==str.Length-1)
                {
                    Console.WriteLine(count);
                    Console.WriteLine(finalcount_count);
                    if (i == str.Length - 1) result += str[i];
                    if (count < finalcount_count)
                    {
                        finalcount_count = count;
                 
                        finalresult = result;
                       
                
                    }
                    count = 0;
                    result = "";
                  

                    continue;
                }
                result += str[i];
           
                Console.WriteLine(result);
            }
       
            Console.WriteLine(finalresult);


        }
    }
}
