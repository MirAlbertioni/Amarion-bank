using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Bank.Library.Models;
using System.Globalization;

namespace Bank.Library.DatabaseHandler
{
    public class ReadFile
    {
        public static void ReadFromTxtFile()
        {
            var _customerList = DatabaseRepo.Customers;
            var _accountList = DatabaseRepo.Accounts;
            string path = Path.Combine(Environment.CurrentDirectory, @"text\");
            var dir = new DirectoryInfo(path);
            var textFile = (from d in dir.GetFiles()
                            orderby d.LastWriteTime descending
                            select d).First().ToString();
            string[] items;

            foreach (var line in File.ReadLines(dir + textFile))
            {
                items = line.Split(";");
                if (items.Length == 9)
                {
                    var customer = new Customer
                    {
                        Id = Convert.ToInt32(items[0]),
                        OrgNumber = items[1],
                        Name = items[2],
                        Adress = items[3],
                        City = items[4],
                        Region = items[5],
                        AreaCode = items[6],
                        Country = items[7],
                        Phone = items[8]
                    };
                    _customerList.Add(customer);
                }
                if (items.Length == 3)
                {
                    var account = new Account
                    {
                        AccountNumber = Convert.ToInt32(items[0]),
                        CustomerId = Convert.ToInt32(items[1]),
                        Balance = Decimal.Parse(items[2], CultureInfo.InvariantCulture)
                    };
                    _accountList.Add(account);
                }
            }
        }
    }
}
