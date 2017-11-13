using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Library.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
