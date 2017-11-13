using Bank.Library.DatabaseHandler;
using System;
using Xunit;
using System.Linq;

namespace BankTesting
{
    public class UnitTests
    {
        [Fact]
        public void SearchCostumer()
        {
            ReadFile.ReadFromTxtFile();
            var customerList =  DatabaseRepo.Customers;
            var custListIdLast = customerList.Select(x => x.Id).Last();
            Assert.Equal("1091", custListIdLast.ToString());
            
        }
        [Fact]
        public void SearchAccount()
        {
            //Search Account
            ReadFile.ReadFromTxtFile();
            var accountList = DatabaseRepo.Accounts;
            var AccountFirst = accountList.Select(x => x.AccountNumber).First();
            Assert.Equal("13001", AccountFirst.ToString());

        }

        [Fact]
        public void Insert()
        {
            ReadFile.ReadFromTxtFile();
            var acc = DatabaseRepo.Accounts;
            var accId = acc.FirstOrDefault(x => x.AccountNumber == 13008);
            decimal insertAmoun = 250.30M;
            accId.Balance = accId.Balance + insertAmoun;

            Assert.Equal("764,70", accId.Balance.ToString());
        }
    }
}
