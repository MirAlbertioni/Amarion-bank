using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.Menus
{
    public class MainMenu
    {
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Main menu\n " +
                "0) Save and exit \n " +
                "1) Search for customer \n " +
                "2) Show Customer Report \n " +
                "3) Create new customer \n" +
                "4) Delete customer \n" +
                "5) Create new account \n" +
                "6) Delete account \n" +
                "9) Account overview \n");
        }
    }
}
