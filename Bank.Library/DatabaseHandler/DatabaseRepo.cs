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
            string menu;
            while(true)
            {
                Console.WriteLine("Press 9 to go back to menu");
                menu = Console.ReadLine();
                if (menu == "9")
                {
                    MainMenu.ShowMenu();
                }
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
            Console.WriteLine("Search for customer or press 9 to go back to menu \nYou can use name or city.");
            var input = Console.ReadLine();
            bool noCustomersFound = true;
            
            if(input == "9")
            {
                MainMenu.ShowMenu();
            }
            else
            {
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
        }

        public static void ShowCustomerReport()
        {
            Console.Clear();
            Console.WriteLine("Search customer by ID or press 9 to go back to menu");
            var input = Console.ReadLine();
            bool noCustomersFound = true;
            if(input == "9")
            {
                MainMenu.ShowMenu();
            }
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
            Console.WriteLine("Input customer Id  you want to delete or go back to menu by pressing 9");
            var userInput = Console.ReadLine();
            if(userInput == "9")
            {
                MainMenu.ShowMenu();
            }
            var id = int.TryParse(userInput, out int ParseduserInput);
            if (id)
            {
                var CustemerToDelete = _customerList.FirstOrDefault(c => c.Id == ParseduserInput);
                if (CustemerToDelete == null)
                {
                    Console.WriteLine("Can't find any customer with your input. Press enter to try again or press 9 to go back to Menu");
                    var inp = Console.ReadLine();

                    if (inp == "9") MainMenu.ShowMenu();

                    else DeleteCustomer();
                }
                else
                {
                    var AccountToDelet = _accountList.Where(a => a.CustomerId == ParseduserInput).ToList();
                    foreach (var item in AccountToDelet)
                    {
                        if (item.Balance != 0)
                        {
                            Console.WriteLine("Custemer account  balance is not null. Press enter to try again or press 9 to go back to Menu");
                            var inp = Console.ReadLine();

                            if (inp == "9") MainMenu.ShowMenu();

                            else DeleteCustomer();
                        }
                        else _accountList.Remove(item);
                    }

                    var accountCheck = _accountList.Where(z => z.CustomerId == ParseduserInput).ToList().Count();

                    if (accountCheck == 0)
                    {
                        _customerList.Remove(CustemerToDelete);
                    }

                    else
                    {
                        Console.WriteLine("Customer account  balance is not null. Press enter to try again or press 9 to go back to Menu");
                        Console.ReadLine();
                    }
                }
            }

            else
            {
                Console.WriteLine("CustemerId muste be a number. Press enter to try again or press 9 to go back to Menu");
                var inp = Console.ReadLine();

                if (inp == "9") MainMenu.ShowMenu();

                else DeleteCustomer();
            }

            GoBackToMenu();
        }

        public static void CreateNewAccount()
        {
            Console.Clear();
            Console.WriteLine("*Create new Account*\n");
            Console.WriteLine("Enter Customer Id or press 9 to go back to menu");

            var userInput = Console.ReadLine().Trim();
            if(userInput == "9")
            {
                MainMenu.ShowMenu();
            }
            var control = int.TryParse(userInput, out int intCheck);

            if (control)
            {
                var customer = _customerList.FirstOrDefault(i => i.Id == intCheck);
                if (customer != null)
                {
                    var newAccId = _accountList.Last().AccountNumber;
                    newAccId++;
                    var account = new Account
                    {
                        CustomerId = customer.Id,
                        AccountNumber = newAccId,
                        Balance = 0
                    };
                    _accountList.Add(account);

                    Console.WriteLine(customer.Name + ", Your new account has been created! ");
                    Console.WriteLine("Account number: " + account.AccountNumber + " Balance: " + account.Balance);
                }

                else
                {
                    Console.WriteLine("No user found.. Please press key to try again");
                    Console.ReadKey();
                    CreateNewAccount();
                }
            }

            else
            {
                Console.WriteLine("Wrong format.. Use numbers.. Press enter to try again");
                Console.ReadLine();
                CreateNewAccount();
            }
            GoBackToMenu();
        }

        public static void DeleteAccount()
        {
            Console.Clear();
            Console.WriteLine("*Delete Account*");

            Console.Write("Input customer Id to view accounts: ");

            var userInput = Console.ReadLine().Trim();
            var control = int.TryParse(userInput, out int intCheck);
            var customer = _customerList.FirstOrDefault(x => x.Id == intCheck);
            var accounts = _accountList.Where(i => i.CustomerId == intCheck);

            if (control)
            {
                if (customer == null)
                {
                    Console.WriteLine("User not found..  Press key to try Again ");
                    Console.ReadKey();
                    DeleteAccount();
                }

                foreach (var item in accounts)
                {
                    Console.WriteLine(item.AccountNumber + " balance: " + item.Balance);
                }

                Console.Write("Enter account to delete: ");
                var accInput = Convert.ToInt32(Console.ReadLine());

                foreach (var item in accounts.ToList())
                {
                    if (accInput == item.AccountNumber)
                    {
                        if (item.Balance == 0)
                        {
                            _accountList.Remove(item);
                            Console.WriteLine(item.AccountNumber + " has been removed");
                        }
                        else
                        {
                            Console.WriteLine("Error: This account balance is not zero.. ");
                            Console.WriteLine("Press key to restart");
                            Console.ReadKey();
                            DeleteAccount();
                        }
                    }
                }
            }
            GoBackToMenu();
        }

        public static void Transactions(string input)
        {
            Console.Clear();
            var sb = new StringBuilder();
            switch (input)
            {
                case "1":
                    sb.Append("a withdrawal");
                    sb.AppendLine();
                    break;
                case "2":
                    sb.Append("your deposit");
                    sb.AppendLine();
                    break;
                case "3":
                    sb.Append("your transfer");
                    sb.AppendLine();
                    break;
            }

            Console.WriteLine("Type in id number to login:");
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

            Console.WriteLine("\n\nSelect one of the above accounts to make " + sb);
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



