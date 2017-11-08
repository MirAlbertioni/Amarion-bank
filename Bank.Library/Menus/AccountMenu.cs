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
            "0) Save and exit \n " +
            "1) Withdraw \n " +
            "2) Insert \n " +
            "3) Transformer \n");
            var userInput = Console.ReadLine();

            DatabaseRepo.Transactions(userInput);
        }
    }
}
