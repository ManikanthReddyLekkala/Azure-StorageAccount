using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDB
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public PhoneNumber[] PhoneNumber { get; set; }
    }

    public class PhoneNumber
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string MainNumber { get; set; }
    }
}
