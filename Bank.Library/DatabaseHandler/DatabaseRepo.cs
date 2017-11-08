﻿using Bank.Library.Models;
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

        public static void ShowStats()
        {
            Console.WriteLine("Customers: " + _customerList.Count());
            Console.WriteLine("Accounts: " + _accountList.Count());
            Console.WriteLine("Total: " + _accountList.Sum(x => x.Balance) + " kr");
        }

        public static void SearchCustomer()
        {

        }

        public static void ShowCustomerReport()
        {

        }

        public static void SaveCustomerToFile()
        {

        }

        public static void DeleteCustomer()
        {

        }

        public static void DeleteAccount()
        {

        }

        public static void Transactions(string input)
        {

        }
    }
}