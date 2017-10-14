using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.User.Models
{
    public class UserDetails
    {
        public int UserSecurityID { get; set; }
        public string UserName { get; set; }
        public int PersonID { get; set; }
        public string UserPassWord { get; set; }
        public DateTime UserPassWordTime { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public bool CanSendMessages { get; set; }
        public bool CanRcvdSignMessages { get; set; }
        public DateTime Modified { get; set; }
        public String LastViewedRelNotes { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastLogin { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string UDF4 { get; set; }
        public string UDF5 { get; set; }
        public string UDF6 { get; set; }
        public string UDF7 { get; set; }
        public string UDF8 { get; set; }
        public string UDF9 { get; set; }
        public string UDF10 { get; set; }
        public string DirectEmail { get; set; }
    }
}
