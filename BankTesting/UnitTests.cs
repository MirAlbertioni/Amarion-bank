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
            Assert.Equal("1093", custListIdLast.ToString());
            
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
    }
}
