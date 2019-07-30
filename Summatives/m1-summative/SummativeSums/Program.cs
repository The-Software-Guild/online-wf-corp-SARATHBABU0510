using System;

namespace SummativeSums
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = { 1, 90, -33, -55, 67, -16, 28, -55, 15 };
            int[] array2 = { 999, -60, -77, 14, 160, 301 };
            int[] array3 = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, -99 };

            Console.WriteLine("#1 Array Sum: " + GetArraySum(array1));
            Console.WriteLine("#2 Array Sum: " + GetArraySum(array2));
            Console.WriteLine("#3 Array Sum: " + GetArraySum(array3));

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey();                                         // Wait for the user's confirmation/entry before ending the program!  
        }
        public static int GetArraySum(int[] currentarray)             // This method accepts array as input and returns sum of array elements!
        {
            int currentarray_elements_sum = 0;
            for(int i = 0; i < currentarray.Length; i++)
            {
                currentarray_elements_sum += currentarray[i];
            }
            return currentarray_elements_sum;
        }
    }
}
