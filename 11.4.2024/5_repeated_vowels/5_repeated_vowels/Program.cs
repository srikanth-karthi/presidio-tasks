namespace _5_repeated_vowels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a String:");
            String str=Console.ReadLine();
            int count = 0;
            int finalcount_count = int.MaxValue;
            String result="";
            List<string> minVowelWords = new List<string>();
            str = str.ToLower().Trim();

            for (int i = 0;i<str.Length;i++)
            {
            if (str[i] =='a' || str[i] == 'e' || str[i] == 'i' || str[i] == 'o' || str[i] == 'u')
                {
                    count++;
                }
              if (str[i] == ',' || i==str.Length-1)
                {
  
                    if (i == str.Length - 1) result += str[i];
                    if (count < finalcount_count)
                    {
                        finalcount_count = count;
                        minVowelWords.Clear();
                        minVowelWords.Add(result);
                    }
                    else if(count == finalcount_count) minVowelWords.Add(result);
                    count = 0;
                    result = "";
                  

                    continue;
                }
                result += str[i];
           
          
            }
            Console.WriteLine($"Number of Words: {str.Split(',').Length}");
            Console.WriteLine($"Number of vowels: {finalcount_count}");
            Console.WriteLine($"Word(s) with the least vowels : {string.Join(" ,", minVowelWords)}");
                    }
    }
}
