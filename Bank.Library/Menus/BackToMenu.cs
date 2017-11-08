using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.Menus
{
    public class BackToMenu
    {
        public static void GoBackToMenu()
        {
            Console.WriteLine("\n\n\nPress 9 to go back to menu");
            var menu = Console.ReadLine();
            if (menu == "9")
            {
                MainMenu.Menu();
            }
        }
    }
}
