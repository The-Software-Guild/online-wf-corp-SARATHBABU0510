using System;
using System.Collections.Generic;
using DvdManager.View;
using DvdManager.Data;
using DvdManager.Models;

namespace DvdManager.Controllers
{
    public class DvdController
    {
        private List<Dvd> _displaydvdlist;
        private DvdView view;
        private DvdRepository dvdRepository;
        
        public DvdController()
        {
            view = new DvdView();            
            _displaydvdlist = new List<Dvd>();
            dvdRepository = new DvdRepository();
        }
        public void Run()
        {            
            while (true)
            {
                int result = view.GetMenuChoice("Choose 1 of 5 options: 1 for Create, 2 for List All, 3 for Find By Id, 4 for Edit and 5 for Remove:");
                switch (result)
                {
                    case 1:
                        CreateDvd();
                        Success();                        
                        break;
                    case 2:
                        DisplayDvds();
                        Success();
                        break;
                    case 3:
                        SearchDvds();
                        Success();
                        break;
                    case 4:
                        EditDvd();
                        Success();
                        break;
                    case 5:
                        RemoveDvd();
                        Success();
                        break;                    
                    default:
                        break;
                }
                string user_continue_usage_response = view.UserchoiceYesorNo("Do you want to continue to use the application? Enter Yes or No!");               
                if (user_continue_usage_response.ToUpper() == "NO")
                {
                    Console.WriteLine("Thanks for using the application!");
                    Console.WriteLine("Press any key to exit the application.");
                    Console.ReadKey();
                    break;                                              //Control goes to the end of the while loop!
                }
                else
                {
                    Console.WriteLine("Good Choice! Enjoy using the application!");
                }
            }
        }

        private void CreateDvd()
        {
            view.DisplayDvd(dvdRepository.Create(view.GetNewDvdInfo()));
        }
        private void DisplayDvds()
        {
            _displaydvdlist = dvdRepository.ReadAll();
            if (_displaydvdlist.Count != 0)
            {
                foreach (Dvd dvd in _displaydvdlist)
                    view.DisplayDvd(dvd);
            }
            else
            {
                Console.WriteLine("No DVDs to display!");
            }            
        }

        private void SearchDvds()
        {
            int id = view.SearchDvd();
            bool exists = dvdRepository.exists(id);
            if (exists)
            {
                view.DisplayDvd(dvdRepository.ReadById(id));
            }
            else
            {
                Console.WriteLine("Entered Dvd Id doesn't exist!");
            }                
        }
        private void EditDvd()
        {
            int id = view.SearchDvd();
            bool exists = dvdRepository.exists(id);
            if (exists)
            {
                Dvd dvd = view.EditDvdInfo(dvdRepository.ReadById(id));
                dvdRepository.Update(id, dvd);
            }
            else
            {
                Console.WriteLine("Entered Dvd Id doesn't exist!");
            }
        }
        private void RemoveDvd()
        {
            int id = view.SearchDvd();
            bool exists = dvdRepository.exists(id);
            if (exists)
            {
                Dvd dvd = dvdRepository.ReadById(id);
                if (view.ConfirmRemoveDvd(dvd))
                {
                    dvdRepository.Delete(dvd.Id);
                }
            }
            else
            {
                Console.WriteLine("Entered Dvd Id doesn't exist!");
            }
        }
        private void Success()
        {
            Console.WriteLine("This transaction is successful and complete!");            
        }
    }
}
