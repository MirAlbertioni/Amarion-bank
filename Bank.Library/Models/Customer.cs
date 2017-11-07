using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bank.Library.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string OrgNumber { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string AreaCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
