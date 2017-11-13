using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Bank.Library.DatabaseHandler
{
    public class SaveNewFile
    {
        public static void WhenChangesCreateNewFile()
        {
            var _customerList = DatabaseRepo.Customers;
            var _accountList = DatabaseRepo.Accounts;

            string path = Path.Combine(Environment.CurrentDirectory, @"text\");
            var dir = new DirectoryInfo(path);

            var saveNewDateTimeFile = DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";

            using (StreamWriter newTextFile = new StreamWriter(File.Create(dir + saveNewDateTimeFile)))
            {
                var customerCount = _customerList.Count();
                newTextFile.WriteLine(customerCount.ToString());

                foreach (var item in _customerList)
                {
                    newTextFile.WriteLine(item.Id.ToString() + ";" + item.OrgNumber + ";" + item.Name + ";" + item.Adress + ";" +
                        item.City + ";" + item.Region + ";" + item.AreaCode + ";" + item.Country + ";" + item.Phone);
                }

                var accountCount = _accountList.Count();
                newTextFile.WriteLine(accountCount.ToString());

                foreach (var item in _accountList)
                {
                    newTextFile.WriteLine(
                        item.AccountNumber + ";" +
                        item.CustomerId + ";" +
                        item.Balance.ToString(CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
