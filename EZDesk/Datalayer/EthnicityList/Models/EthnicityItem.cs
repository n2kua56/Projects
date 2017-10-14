using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.EthnicityList.Models
{
    public class EthnicityItem
    {
        public int ID { get; set; }
        public string Ethnicity { get; set; }
        public string HL7EthnicityCode { get; set; }
        public int DisplayOrder { get; set; }

        public EthnicityItem()
        {
        }
    }
}
