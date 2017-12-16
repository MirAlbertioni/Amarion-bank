using System;
using Bank.Library.Menus;
using Bank.Library.DatabaseHandler;

namespace Bankproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReadFile.ReadFromTxtFile();
            MainMenu.ShowMenu();
        }
    }
}
