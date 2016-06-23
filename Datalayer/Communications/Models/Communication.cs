using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.Communications.Models
{
    public class Communication
    {
        public enum CommunicationsTypeEnum 
        { 
            Undefined = 1, 
            HomePhone = 2, 
            CellPhone = 3, 
            Fax = 4, 
            Beeper = 5, 
            WorkPhone = 6, 
            EMAIL = 7 
        }

        public int CommunicationID { get; set; }
        public int PersonID { get; set; }
        public CommunicationsTypeEnum CommunicationType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; }
        public string CommunicationCode { get; set; }

        public Communication()
        {
        }
    }
}
