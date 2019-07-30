using System;

namespace DogGenetics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dogbreeds = { "St. Bernard", "Chihuahua", "Dramatic RedNosed Asian Pug", "Common Cur", "King Doberman" };
            int[] dogbreedperc = { 0, 0, 0, 0, 0 };

            Console.Write("What is your dog's name?");
            string dogname = Console.ReadLine();
            Console.WriteLine("Well then, I have this highly reliable report on " + dogname + "'s prestigious background right here.\n");
            Console.WriteLine(dogname + " is:\n");

            Random random = new Random();
            int maxperc = 100, randomperc = maxperc, accumulatedperc = 0, computer_chosen_perc = 0;
            for (int i = 0; i <= 4; i++)                                   //This loop to derive 5 random percentages!   
            {
                computer_chosen_perc = random.Next(1, randomperc);         //Computer Randomly chooses a number from 1 thru remaining perc!
                accumulatedperc += computer_chosen_perc;                   //Summation of chosen percentages!
                randomperc -= computer_chosen_perc;                        //Negation of chosen percentage from current value for a total of 100!
                if (computer_chosen_perc == 100 || accumulatedperc == 100)
                {
                    /*When first or total accumulatedperc so far is 100,
                      The chosen perc is allocated to the current breeed and then breaks the for loop! 
                      So,the dog will have 0% of remaining breeds!
                      */
                    dogbreedperc[i] = computer_chosen_perc;
                    break;
                }
                else
                {
                    dogbreedperc[i] = computer_chosen_perc;
                }
            }
            if (accumulatedperc != 100)                                    //When total accumulatedperc is not 100, allocate remaining perc to last breed!
            {
                accumulatedperc -= computer_chosen_perc;
                dogbreedperc[4] = 100 - accumulatedperc;
            }

            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine(dogbreedperc[i] + "% " + dogbreeds[i]);
            }

            Console.WriteLine("\nWow, that's QUITE the dog!\n");

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();                                         // Wait for the user's confirmation/entry before ending the program!  
        }
    }
}
