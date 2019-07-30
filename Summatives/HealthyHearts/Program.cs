using System;

namespace HealthyHearts
{
    class Program
    {
        static void Main(string[] args)
        {
            const int max_heart_rate = 220;
            const decimal heart_rate_zone_lower_end_perc = 0.5M, heart_rate_zone_high_end_perc = 0.85M;
            int user_age = GetUserAge();
            int user_max_heart_rate = max_heart_rate - user_age;

            Console.WriteLine("Your maximum heart rate should be " + user_max_heart_rate + " beats per minute");
            Console.WriteLine("Your target HR Zone is " + Math.Round(heart_rate_zone_lower_end_perc * user_max_heart_rate, 0, MidpointRounding.AwayFromZero)
                + " - " + Math.Round(heart_rate_zone_high_end_perc * user_max_heart_rate, 0, MidpointRounding.AwayFromZero) + " beats per minute");

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey();                                         // Wait for the user's confirmation/entry before ending the program!  
        }
        public static int GetUserAge()                                 // This method returns age of the user!
        {
            int user_entered_age_int;
            while (true)
            {
                Console.Write("What is your age?");
                string user_entered_age = Console.ReadLine();

                if (int.TryParse(user_entered_age, out user_entered_age_int))
                {
                    break; // good input. Control goes to the end of the while loop!
                }
                else
                {
                    Console.WriteLine("That was not a number. Enter a number!");  //continue until a whole number is entered.
                }
            }
            return user_entered_age_int;
        }
    }
}
