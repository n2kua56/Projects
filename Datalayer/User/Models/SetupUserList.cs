using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.User.Models
{
    public class SetupUserList
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SetupUserList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="active"></param>
        public SetupUserList(int id, string name, bool active)
        {
            UserID = id;
            Name = name;
            IsActive = active;
        }

    }
}
