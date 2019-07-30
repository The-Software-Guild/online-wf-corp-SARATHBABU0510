using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            int user_entered_num_of_rounds = GetRoundsNumberFromUser(); //Determine how many rounds user wants to play!
            FindAndDeclareWinner(user_entered_num_of_rounds);           //Pass the number of rounds as input to method!

            while (true)                                                //Repeat this loop until user chooses No! Yes will continue the game!
            {
                Console.WriteLine("Do you want to play the game again?");
                string user_another_game_response = Console.ReadLine();

                if (user_another_game_response.ToUpper() == "NO")
                {
                    Console.WriteLine("Thanks for playing!");
                    break;                                              //Control goes to the end of the while loop!
                }
                else if (user_another_game_response.ToUpper() == "YES")
                {
                    user_entered_num_of_rounds = GetRoundsNumberFromUser();
                    FindAndDeclareWinner(user_entered_num_of_rounds);
                }
                else
                {
                    Console.WriteLine("Choose Yes or No!");             // Bad input, we have only 2 options here - Yes or No!
                }
            }

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();                                         // Wait for the user's confirmation/entry before ending the program!  
        }

        /*Method to ask the user how many rounds he/she wants to play and get a valid input!
            This method returns the number of rounds (as int) user wants to play! */
        public static int GetRoundsNumberFromUser()
        {
            int user_entered_num_of_rounds_int, max_num_of_rounds = 10, min_num_of_rounds = 1; //Declaring min and max number of rounds variables!
            while (true)
            {
                Console.Write("Select the number of rounds you want to play from 1 through 10!");
                string user_entered_rounds = Console.ReadLine();

                if (int.TryParse(user_entered_rounds, out user_entered_num_of_rounds_int))
                {
                    // user entry was an int, now is it between 1 and 10?
                    if (user_entered_num_of_rounds_int >= min_num_of_rounds && user_entered_num_of_rounds_int <= max_num_of_rounds)
                    {
                        break; // good input. Control goes to the end of the while loop!
                    }
                    else
                    {
                        /*User entered a number, which is out of accepted range.
                            This writes an error message to console and quits the program without further processing!*/
                        Console.WriteLine("That number was not between 1 and 10!");
                        Console.WriteLine("Press any key to quit...");
                        Console.ReadKey();
                        Environment.Exit(13);          //User entered number is invalid here!
                    }
                }
                else
                {
                    Console.WriteLine("That was not a number. Enter a number!");  //continue until a whole number is entered.
                }
            }
            return user_entered_num_of_rounds_int;
        }

        //This method accepts number of rounds as an int input and declares winner of the game!
        public static void FindAndDeclareWinner(int num_of_rounds)
        {
            int user_entered_choice_item_int = 0;
            int tie_count = 0, user_won_count = 0, computer_won_count = 0;
            for (int i = 1; i <= num_of_rounds; i++)                                      //Execute the loop # of rounds times entered by the user!
            {
                bool user_choice_not_done = true;
                while (user_choice_not_done)                                              //While loop to get User choice of item!
                {
                    Console.Write("Select your choice: Enter 1 for Rock, 2 for Paper, 3 for Scissors!");
                    string user_entered_choice_item = Console.ReadLine();

                    if (int.TryParse(user_entered_choice_item, out user_entered_choice_item_int))
                    {
                        // User entry was an int, now is it between 1 and 3?
                        switch (user_entered_choice_item_int)
                        {
                            case 1:
                            case 2:
                            case 3:
                                user_choice_not_done = false; // good input - choice completed! This assignment terminates while loop!
                                break;
                            default:
                                Console.WriteLine("Enter a number from 1 through 3!");  //continue until 1 thru 3 is entered!
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That was not a number. Enter a number!");  //continue until a whole number is entered.
                    }
                }

                Random random = new Random();
                int computer_chosen_num = random.Next(1, 3);         //Computer Randomly chooses a number from 1 thru 3!

                /* Round winner determination rules:
                    1. Each player (User and Computer) chooses Rock, Paper, or Scissors. Hint: 1 = Rock, 2 = Paper, 3 = Scissors
                    2. If both players choose the same thing, the round is a tie.
                    3. Otherwise:
                        a. Paper wraps Rock to win
                        b. Scissors cut Paper to win
                        c. Rock breaks Scissors to win
                */
                // Winner is declared for each and every round until the game is complete!
                if (user_entered_choice_item_int == 1 && computer_chosen_num == 3)
                {
                    Console.WriteLine("User won this round!");
                    user_won_count += 1;
                }
                else if (user_entered_choice_item_int == 3 && computer_chosen_num == 1)
                {
                    Console.WriteLine("Computer won this round!");
                    computer_won_count += 1;
                }
                else if (user_entered_choice_item_int > computer_chosen_num)
                {
                    Console.WriteLine("User won this round!");
                    user_won_count += 1;
                }
                else if (user_entered_choice_item_int == computer_chosen_num)
                {
                    Console.WriteLine("This round is tie!");
                    tie_count += 1;
                }
                else
                {
                    Console.WriteLine("Computer won this round!");
                    computer_won_count += 1;
                }
            }

            Console.WriteLine("Number of ties in this game: " + tie_count);
            Console.WriteLine("Number of rounds won by the user in this game: " + user_won_count);
            Console.WriteLine("Number of rounds won by the computer in this game: " + computer_won_count);

            /*Following code declares winner of the game based on the counter variables! 
            Game is made up of number of rounds entered by user! */
            if (user_won_count == computer_won_count)
            {
                Console.WriteLine("This game is a tie!");
            }
            else if (user_won_count > computer_won_count)
            {
                Console.WriteLine("User has won this game!");
            }
            else
            {
                Console.WriteLine("Computer has won this game!");
            }
        }
    }
}
