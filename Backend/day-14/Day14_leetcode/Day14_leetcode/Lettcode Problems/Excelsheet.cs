using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14_leetcode.Lettcode_Problems
{
    public class Excellsheet
    {
        public string ConvertToTitle(int columnNumber)
        {
            string result = "";

            while (columnNumber > 0)
            {
                int remainder = columnNumber % 26;
                if (remainder == 0)
                {
                    remainder = 26;
                    columnNumber -= 1;
                }
                result = (char)('A' + remainder - 1) + result;
                columnNumber /= 26;
            }

            return result;
        }
    }
}
