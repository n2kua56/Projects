using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.RaceList.Models
{
    public class RaceItem
    {
        public int ID { get; set; }
        public string Race { get; set; }
        public string HL7RaceCode { get; set; }
        public int DisplayOrder { get; set; }

        public RaceItem()
        {
        }
    }
}
