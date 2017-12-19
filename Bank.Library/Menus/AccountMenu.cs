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
            bool rightInput = false;
            string userInput;
            do
            {
                Console.WriteLine("Account overview\n\n " +
                                    "1) Withdraw \n " +
                                    "2) Insert \n " +
                                    "3) Transfer \n " +
                                    "9) Back to menu");

                userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4")
                {
                    rightInput = true;
                }
                else if (userInput == "9")
                {
                    MainMenu.ShowMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Fel input, försökt igen:");
                }
            } while (!rightInput);
            DatabaseRepo.Transactions(userInput);
        }
    }
}
