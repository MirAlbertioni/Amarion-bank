using Bank.Library.DatabaseHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.Menus
{
    public class MainMenu
    {
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\t\t Main menu\n " +
                "*********************************\n " +
                "0) Save and exit \n " +
                "1) Search for customer \n " +
                "2) Show Customer Report \n " +
                "3) Create new customer \n " +
                "4) Delete customer \n " +
                "5) Create new account \n " +
                "6) Delete account \n " +
                "7) Account overview \n " +
                "\n 9) Back to menu\n" );

            DatabaseRepo.ShowStats();

            var userInput = Console.ReadLine();
            if (userInput == "0")
            {
                SaveNewFile.WhenChangesCreateNewFile();
            }
            else if (userInput == "1")
            {
                DatabaseRepo.SearchCustomer();
            }
            else if (userInput == "2")
            {
                DatabaseRepo.ShowCustomerReport();
            }





            else if (userInput == "7")
            {
                AccountMenu.ShowAccountMenu();
            }
            Console.ReadLine();
        }
    }
}
