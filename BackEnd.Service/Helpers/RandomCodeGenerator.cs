using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.Helpers
{
    public class RandomCodeGenerator
    {
        // Generate a random number between two numbers    
        public static int RandomNumber(int min = 10000, int max = 99999)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase  
        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generate a random password of a given length (optional)  
        public static string RandomPassword(int size = 0)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            var builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}
