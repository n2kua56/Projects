using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.TODO.Models
{
    public class ToDoList
    {
        /// <summary>
        /// ID for this List item
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name/Description of this List item
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// True when the List item is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// User ID from the  UserSecurity table that this List belongs to.
        /// </summary>
        public int UserID { get; set; }
    }
}
