using Bank.Library.DatabaseHandler;
using System;
using Xunit;
using System.Linq;

namespace BankTesting
{
    public class UnitTests
    {
        public UnitTests()
        {
            ReadFile.ReadFromTxtFile();
        }

        [Fact]
        public void SearchCostumer()
        {
            var customerList =  DatabaseRepo.Customers;
            var custListIdLast = customerList.Select(x => x.Id).Last();
            Assert.Equal("1091", custListIdLast.ToString());            
        }

        [Fact]
        public void SearchAccount()
        {
            //Search Account
            var accountList = DatabaseRepo.Accounts;
            var AccountFirst = accountList.Select(x => x.AccountNumber).First();
            Assert.Equal("13001", AccountFirst.ToString());
        }

        [Fact]
        public void Insert()
        {
            var acc = DatabaseRepo.Accounts;
            var accId = acc.FirstOrDefault(x => x.AccountNumber == 13008);
            decimal insertAmoun = 250.30M;
            accId.Balance = accId.Balance + insertAmoun;

            Assert.Equal("764,70", accId.Balance.ToString());
        }

        [Fact]
        public void DeleteAccount()
        {
            // Removes Account and check if gone with acc.Count
            var accRepo = DatabaseRepo.Accounts;
            var accCount = accRepo.Count;
            var acc = accRepo.FirstOrDefault();
            accRepo.Remove(acc);
            var expectedCount = accCount - 1;
            Assert.Equal(expectedCount.ToString(), accRepo.Count.ToString());

        }
    }
}
