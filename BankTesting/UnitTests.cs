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
    }
}
