using Bank.Library.Menus;
using Bank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Library.DatabaseHandler
{
    public class DatabaseRepo
    {
        public static HashSet<Customer> Customers { get => _customerList; set => _customerList = value; }
        public static HashSet<Account> Accounts { get => _accountList; set => _accountList = value; }

        static HashSet<Customer> _customerList = new HashSet<Customer>();
        static HashSet<Account> _accountList = new HashSet<Account>();

        public static void GoBackToMenu()
        {
            Console.WriteLine("Press 9 to go back to menu");
            var menu = Console.ReadLine();
            if (menu == "9")
            {
                MainMenu.ShowMenu();
            }
        }


        public static void ShowStats()
        {
            Console.WriteLine("Customers: " + _customerList.Count());
            Console.WriteLine("Accounts: " + _accountList.Count());
            Console.WriteLine("Total: " + _accountList.Sum(x => x.Balance) + " kr");
        }

        public static void SearchCustomer()

        {
            Console.Clear();
            Console.WriteLine("Search for customer\n" + "You can use name or city.");
            var input = Console.ReadLine();
            bool noCustomersFound = true;

            foreach (var item in Customers)
            {
                if (item.Name.ToUpper().Contains(input.ToUpper()) || item.City.ToUpper().Contains(input.ToUpper()))
                {
                    Console.WriteLine("ID: " + item.Id + "\nName: " + item.Name + "\nCity " + item.City);
                    noCustomersFound = false;
                }
            }

            if (noCustomersFound == true)
            {
                Console.WriteLine("Can't find any customer with your input. Press enter to try again or press 9 back to menu");
                var inp = Console.ReadLine();

                if (inp == "9") MainMenu.ShowMenu();

                else SearchCustomer();
            }
            GoBackToMenu();
            Console.ReadLine();
        }

        public static void ShowCustomerReport()
        {
            Console.Clear();
            Console.WriteLine("Search customer by ID");
            var input = Console.ReadLine();
            bool noCustomersFound = true;

            foreach (var item in Customers)
            {
                if (item.Id.ToString().Contains(input))
                {
                    Console.WriteLine("ID: " + item.Id +
                        "\nOrganisation number: " + item.OrgNumber +
                        "\nName: " + item.Name +
                        "\nAdress " + item.Adress + " " + item.AreaCode + "\n\n");
                    noCustomersFound = false;
                }
            }

            HashSet<decimal> balanceList = new HashSet<decimal>();
            foreach (var account in Accounts)
            {
                if (account.CustomerId.ToString().Contains(input))
                {
                    Console.WriteLine(account.CustomerId +
                        ": " + account.Balance + " kr");
                    noCustomersFound = false;
                    balanceList.Add(account.Balance);

                    Console.WriteLine(balanceList.Sum());
                }
            }


            if (noCustomersFound == true)
            {
                Console.WriteLine("Can't find any customer with your input. Press enter to try again or press 9 to go back to Menu");

                var inp = Console.ReadLine();

                if (inp == "9") MainMenu.ShowMenu();

                else ShowCustomerReport();

            }
            GoBackToMenu();
            Console.ReadLine();
        }

        public static void SaveCustomerToFile()
        {
                
        }

        public static void DeleteCustomer()
        {
            Console.Clear();
            Console.Write("Input customer Id for the account you want to delete: ");
            var userInput = Console.ReadLine();

            foreach (var item in _accountList.ToList())
            {
                foreach (var c in _customerList.ToList())
                {
                    if (Convert.ToInt32(userInput) == c.Id && item.CustomerId == c.Id)
                    {
                        if (item.Balance > 0)
                        {
                            Console.WriteLine("Account has balance, cannot remove");
                        }
                        else
                        {
                            item.CustomerId = c.Id;
                            var acc = item;
                            _accountList.Remove(acc);
                            _customerList.Remove(c);
                            SaveNewFile.WhenChangesCreateNewFile();
                        }
                    }
                }
            }
        }

        public static void DeleteAccount()
        {
            Console.Clear();
            Console.Write("Inpute customer Id for the account you want to delete: ");
            var userInput = Console.ReadLine();

            var account = _accountList.Where(x => x.AccountNumber == Convert.ToInt32(userInput)).FirstOrDefault();
            if (account == null)
            {
                Console.WriteLine("Input is wrong or The account doesn't exist, Press enter to continue.");
                Console.ReadLine();
                DeleteAccount();
            }
            if (account.Balance == 0)
            {
                _accountList.Remove(account);
                SaveNewFile.WhenChangesCreateNewFile();
            }
            else
            {
                Console.WriteLine("Account has balance, cannot remove, Press enter to continue.");
                Console.ReadLine();
                DeleteAccount();
            }

        }

        public static void Transactions(string input)
        {
            Console.Clear();
            Console.Write("Type in account number to login:");
            var userInput = Console.ReadLine();
            Console.Clear();

            var acc = _accountList.SingleOrDefault(x => x.AccountNumber == Convert.ToInt32(userInput));

            Console.WriteLine("Accounts: " + acc.AccountNumber + "\n" + "Balance: " + acc.Balance + "kr");

            switch (input)
            {
                case "0":
                    SaveNewFile.WhenChangesCreateNewFile();
                    break;
                case "1":
                    Console.WriteLine("Enter amount for withdrawal");
                    var withdraw = Convert.ToDecimal(Console.ReadLine());
                    acc.Balance = acc.Balance - withdraw;
                    Console.Clear();
                    break;
                case "2":
                    Console.WriteLine("Enter amount you wish to insert");
                    var insert = Convert.ToDecimal(Console.ReadLine());
                    acc.Balance = acc.Balance + insert;
                    Console.Clear();
                    break;
                case "3":
                    var user = _accountList.SingleOrDefault(x => x.AccountNumber == Convert.ToInt32(userInput));
                    Console.WriteLine("Transfer to account number?");
                    var transferInput = Console.ReadLine();
                    var transferAccount = _accountList.SingleOrDefault(x => x.AccountNumber == Convert.ToInt32(transferInput));
                    decimal amount = 0m;
                    Console.WriteLine("From what account do you want to transfer?");
                    var yourAccInput = Convert.ToInt32(Console.ReadLine());
                    if (yourAccInput == acc.AccountNumber)
                    {
                        Console.WriteLine("Enter amount you wish to transfer?");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        if (acc.Balance > amount)
                        {
                            acc.Balance = acc.Balance - amount;
                            transferAccount.Balance = transferAccount.Balance + amount;
                        }
                        else
                        {
                            Console.WriteLine("This account have not enough money to do this Transaction, press any key to try agine");
                        }
                    }
                    Console.WriteLine("Transfer completed, " + amount + "kr was sent from " + acc.AccountNumber
                        + " to account number " + transferAccount.AccountNumber);
                    break;

                    Console.WriteLine("Transfer completed, " + amount + "kr was sent from " + acc.AccountNumber
                        + " to account number " + transferAccount.AccountNumber);
                    _accountList.Add(acc);
                    _accountList.Add(transferAccount);
            }
            Console.WriteLine("Account: " + acc.AccountNumber + "Current balance is: " + acc.Balance + " kr");
            _accountList.Add(acc);
            SaveNewFile.WhenChangesCreateNewFile();

        }

        //public static void BackToMenu()
        //{
        //    Console.Clear();
        //    MainMenu.ShowMenu();
        //    Console.ReadLine();
        //}
    }
}



