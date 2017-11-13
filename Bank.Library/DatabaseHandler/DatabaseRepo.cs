using Bank.Library.Menus;
using Bank.Library.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public static void CreateNewCustomer()
        {
            CustomerHandler.CreateNewCustomer();
            GoBackToMenu();
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

        public static void CreateNewAccount()
        {
            //Skapa account för en kund som finns
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
            Console.Write("Type in id number to login:");
            var userInput = Console.ReadLine();
            Console.Clear();
            Account acc = null;
            var userAccount = _customerList.SingleOrDefault(user => user.Id == Int32.Parse(userInput));
            var accounts = _accountList.Where(x => x.CustomerId == userAccount.Id).ToList();
            Console.WriteLine("Customer Id: " + userAccount.Id + " has " + accounts.Capacity + " accounts: ");

            foreach (var item in accounts)
            {
                Console.WriteLine("Account: " + item.AccountNumber + " has Balance: " + item.Balance + "kr");
            }

            Console.WriteLine("Choose one of above accounts");
            var accountInput = Int32.Parse(Console.ReadLine());
            acc = accounts.SingleOrDefault(x => x.AccountNumber == accountInput);

            switch (input)
            {
                case "1":
                    Console.WriteLine("Enter amount for withdrawal");
                    var withdrawAmount = Console.ReadLine();
                    var amountReplace = withdrawAmount.Replace(".", ",");
                    decimal newWithdrawAmount;
                    var withdrawParsedSucced = decimal.TryParse(amountReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out newWithdrawAmount);
                    acc.Balance = acc.Balance - newWithdrawAmount;
                    Console.Clear();
                    break;
                case "2":
                    Console.WriteLine("Enter amount you wish to insert");
                    var insert = Console.ReadLine();
                    var insertReplace = insert.Replace(".", ",");
                    decimal newInsertAmount;
                    var insertParsedSucced = decimal.TryParse(insertReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out newInsertAmount);
                    acc.Balance = acc.Balance + newInsertAmount;
                    Console.Clear();
                    break;
                case "3":
                    bool checkLoop = true;
                    decimal amount = 0m;
                    var transferInput = 0;
                    Account transferAccount = null;
                    while (checkLoop)
                    {
                        Console.WriteLine("Transfer to account number?");
                        transferInput = Convert.ToInt32(Console.ReadLine());
                        transferAccount = accounts.SingleOrDefault(x => x.AccountNumber == transferInput);
                        if (transferAccount != acc)
                        {
                            Console.WriteLine("Enter amount you wish to transfer?");
                            var transferAmount = Console.ReadLine();
                            var transferReplace = transferAmount.Replace(".", ",");
                            var transferParsedSucced = decimal.TryParse(transferReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out amount);
                            checkLoop = false;
                            if (acc.Balance > amount)
                            {
                                acc.Balance = acc.Balance - amount;
                                transferAccount.Balance = transferAccount.Balance + amount;
                                _accountList.Add(transferAccount);
                                checkLoop = false;
                            }
                            else
                            {
                                Console.WriteLine("This account have not enough money to do this Transaction, press any key to try agine");
                                checkLoop = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The transaction should be between two different accounts");
                            checkLoop = true;
                        }
                    }
                    Console.WriteLine("Transfer completed, " + amount + "kr was sent from " + acc.AccountNumber
                        + " to account number " + transferAccount.AccountNumber);
                    break;
            }

            Console.Clear();
            Console.WriteLine("Customer Id: " + userAccount.Id + "Current balance is:");
            foreach (var item in accounts)
            {
                Console.WriteLine("Account: " + item.AccountNumber + " has Balance: " + item.Balance + "kr");
            }

            if (acc != null)
                _accountList.Add(acc);
            GoBackToMenu();
        }
    }
}



