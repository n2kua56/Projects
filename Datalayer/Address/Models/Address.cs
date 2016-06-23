using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.Address.Models
{
    public class Address
    {
        public enum AddressTypeEnum 
        { 
            Undefined = 1, 
            Home = 2, 
            Work = 3, 
            Business = 4 
        };

        public int AddressID { get; set; }
        public int PersonID { get; set; }
        public AddressTypeEnum AddressType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address()
        {
        }
    }
}
