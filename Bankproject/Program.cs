using System;
using Bank.Library.Menus;
using Bank.Library.DatabaseHandler;

namespace Bankproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainMenu.ShowMenu();
            ReadFile.ReadFromTxtFile();
            
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
        }
    }
}
