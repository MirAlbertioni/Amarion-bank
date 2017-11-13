using Bank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Library.DatabaseHandler
{
    public class CustomerHandler
    {
        public static void CreateNewCustomer()
        {
            var _customerList = DatabaseRepo.Customers;
            var _accountList = DatabaseRepo.Accounts;

            Console.Clear();
            Console.WriteLine("Create new customer\n");

            string inputOrgnr;
            while (true)
            {
                Console.WriteLine("Organisation number: ");
                inputOrgnr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputOrgnr))
                {
                    break;
                }
            }

            string inputName;
            while (true)
            {
                Console.WriteLine("Name: ");
                inputName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputName))
                {
                    break;
                }
            }

            string inputAdress;
            while (true)
            {
                Console.WriteLine("Adress: ");
                inputAdress = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputAdress))
                {
                    break;
                }
            }

            string inputAreacode;
            while (true)
            {
                Console.WriteLine("AreaCode: ");
                inputAreacode = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputAreacode))
                {
                    break;
                }
            }

            string inputCity;
            while (true)
            {
                Console.WriteLine("City: ");
                inputCity = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputCity))
                {
                    break;
                }
            }

            string inputRegion;
            while (true)
            {
                Console.WriteLine("Region: ");
                inputRegion = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputRegion))
                {
                    break;
                }
            }

            string inputCountry;
            while (true)
            {
                Console.WriteLine("Country: ");
                inputCountry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputCountry))
                {
                    break;
                }
            }

            string inputPhone;
            while (true)
            {
                Console.WriteLine("Phone: ");
                inputPhone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputPhone))
                {
                    break;
                }
            }

            var newId = _customerList.Last().Id;
            newId++;
            var newCustomer = new Customer
            {
                Id = newId,
                OrgNumber = inputOrgnr,
                Name = inputName,
                Adress = inputAdress,
                AreaCode = inputAreacode,
                City = inputCity,
                Region = inputRegion,
                Country = inputCountry,
                Phone = inputPhone
            };
            _customerList.Add(newCustomer);

            var newAccId = _accountList.Last().AccountNumber;
            newAccId++;
            var account = new Account
            {
                CustomerId = newCustomer.Id,
                AccountNumber = newAccId,
                Balance = 0
            };
            _accountList.Add(account);
            Console.Clear();
            Console.WriteLine("Customer info\n" + newCustomer.Id + "\n" + newCustomer.OrgNumber + "\n" + newCustomer.Name + "\n" + newCustomer.Adress + "\n"
                + newCustomer.AreaCode + "\n" + newCustomer.City + "\n" + newCustomer.Region + "\n" + newCustomer.Country + "\n" + newCustomer.Phone + "\n\n\n" +
                "Account info" + "\n" + account.AccountNumber + "\n" + account.CustomerId + "\n" + account.Balance + "KR" + "\n\n");
        }
    }
}
