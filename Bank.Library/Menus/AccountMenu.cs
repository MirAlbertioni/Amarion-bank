using Bank.Library.DatabaseHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.Menus
{
    public class AccountMenu
    {
        public static void ShowAccountMenu()
        {
            Console.Clear();
            Console.WriteLine("Account overview\n\n " +
            "1) Withdraw \n " +
            "2) Insert \n " +
            "3) Transfer \n " +
            "9) Back to menu");

            var userInput = Console.ReadLine();
            if(userInput == "9")
            {
                MainMenu.ShowMenu();
            }
            else DatabaseRepo.Transactions(userInput);
        }
    }
}
