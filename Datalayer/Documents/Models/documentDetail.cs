using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.Documents.Models
{
    public class documentDetail
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TabId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string PathName { get; set; }
        public string Description { get; set; }
        public int GroupRestriction { get; set; }
        public string DocumentError { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        public documentDetail()
        {
        }

        /// <summary>
        /// Constructor - Creates a new object from the supplied
        /// items with a default Created/Id/IsActive.
        /// </summary>
        /// <param name="personid"></param>
        /// <param name="tabid"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="pathname"></param>
        /// <param name="description"></param>
        public documentDetail(int personid, int tabid, int userid,
                              string name, string pathname, string description)
        {
            Id = -1;
            PersonId = personid;
            TabId = tabid;
            Name = name;
            Description = description;
            PathName = pathname;
            Created = DateTime.Now;
            IsActive = true;
        }

    }
}
