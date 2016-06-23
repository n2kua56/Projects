using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.Person.Models
{
    public class PersonSelectedArguments : EventArgs
    {
        private int mPersonId;
        public int PersonId 
        {
            get { return mPersonId; }
            set { mPersonId = value; }
        }
    }
}
