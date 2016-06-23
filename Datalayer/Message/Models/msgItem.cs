using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.Message.Models
{
    public class msgItem
    {
        public int ID { get; set; }
        public DateTime msgDateTime { get; set; }
        public char msgDirection { get; set; }
        public int msgSentBy { get; set; }
        public int msgReceivedBy { get; set; }
        public int msgPersonID { get; set; }
        public int msgTabID { get; set; }
        public string msgBody { get; set; }

        public msgItem()
        {
        }


    }
}
