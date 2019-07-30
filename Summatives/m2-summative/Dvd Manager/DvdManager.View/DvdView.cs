using System;
using DvdManager.Models;

namespace DvdManager.View
{
    public class DvdView
    {
        private Dvd dvd;
        public int GetMenuChoice(string prompt)
        {
            int result;
            string userInput;
            ConsoleKeyInfo cki;

            // loop until we return an int
            while (true)
            {
                // 1 & 2: Prompt and Read
                Console.Write(prompt);
                cki = Console.ReadKey();
                Console.WriteLine();
                userInput = cki.KeyChar.ToString();

                // attempt to convert
                if (int.TryParse(userInput, out result))
                {
                    // good input
                    if (result <= 5)
                    {
                        return result;
                    }
                }
                Console.WriteLine("That is not a valid input.");
            }
        }

        public Dvd GetNewDvdInfo()
        {
            dvd = new Dvd();
            Console.WriteLine("Enter a new/updated DVD information!");            
            dvd.Id = UserInputInt("Enter DVD ID:");
            Console.Write("Enter DVD Title:");
            dvd.Title = Console.ReadLine();            
            dvd.ReleaseYear = UserInputInt("Enter DVD ReleaseYear:");
            Console.Write("Enter DVD Director:");
            dvd.Director = Console.ReadLine();            
            dvd.Rating = UserInputFloat("Enter DVD Rating:");
            return dvd;
        }        
        public void DisplayDvd(Dvd dvd)
        {
            Console.WriteLine("-------Dvd Information Starts here!-------------");
            Console.WriteLine("DVD ID:" + dvd.Id);
            Console.WriteLine("DVD Title:" + dvd.Title);
            Console.WriteLine("DVD ReleaseYear:" + dvd.ReleaseYear);
            Console.WriteLine("DVD Director:" + dvd.Director);
            Console.WriteLine("DVD Rating:" + dvd.Rating);
            Console.WriteLine("-------End of the Current Dvd Information!----------");
        }

        public Dvd EditDvdInfo(Dvd dvd)
        {
            DisplayDvd(dvd);
            dvd = GetNewDvdInfo();
            return dvd;
        }
        public int SearchDvd()
        {
            dvd = new Dvd();
            dvd.Id = UserInputInt("Enter the DVD ID you want to search by to view or edit/delete:");
            return dvd.Id;
        }
        public bool ConfirmRemoveDvd(Dvd dvd)
        {
            DisplayDvd(dvd);
            string userInput = UserchoiceYesorNo("Are you sure, you want to Remove? Enter Yes or No!");         
            if(userInput.ToUpper() == "YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int UserInputInt(string prompt)
        {
            int result;
            string userInput;

            // loop until we return an int
            while (true)
            {
                // 1 & 2: Prompt and Read
                Console.Write(prompt);
                userInput = Console.ReadLine();                

                // attempt to convert
                if (int.TryParse(userInput, out result))
                {
                    return result;
                }
                Console.WriteLine("Input is a not a valid number.");
            }
        }

        private float UserInputFloat(string prompt)
        {
            float result;
            string userInput;

            // loop until we return a float
            while (true)
            {
                // 1 & 2: Prompt and Read
                Console.Write(prompt);
                userInput = Console.ReadLine();                

                // attempt to convert
                if (float.TryParse(userInput, out result))
                {
                    return result;
                }
                Console.WriteLine("Input is a not a valid rating number.");
            }
        }

        public string UserchoiceYesorNo(string prompt)
        {
            string userInput;
            // loop until we return an Yes or No
            while (true)
            {
                Console.Write(prompt);                
                userInput = Console.ReadLine();
                if (userInput.ToUpper() == "YES" || userInput.ToUpper() == "NO")
                {
                    break;
                }
                Console.WriteLine("That is not a valid input.");
            }
            return userInput;
        }

    }
}

