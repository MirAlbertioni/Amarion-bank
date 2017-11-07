using Bank.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.DatabaseHandler
{
    public class DatabaseRepo
    {
        public static HashSet<Customer> Customers { get => _customerList; set => _customerList = value; }
        public static HashSet<Account> Accounts { get => _accountList; set => _accountList = value; }

        static HashSet<Customer> _customerList = new HashSet<Customer>();
        static HashSet<Account> _accountList = new HashSet<Account>();
    }
}
