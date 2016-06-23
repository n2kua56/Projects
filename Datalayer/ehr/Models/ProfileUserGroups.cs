using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.ehr.Models
{
    /// <summary>
    /// This item represents a single Profile Group.  User may
    /// be added to a group and then a value set for the profile
    /// key for the group.  All users assigned to the gruop will
    /// then have the same value UNLESS they have a user profusers
    /// entry to override the value with a value for them.
    /// </summary>
    //public class ProfileUserGroups
    //{
    //    public int GroupID { get; set; }
    //    public DateTime Created { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime Modified { get; set; }
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// ProfileUserGroups empty Constructor
    //    /// </summary>
    //    public ProfileUserGroups()
    //    {
    //    }

    //    /// <summary>
    //    /// ProfileUserGroups Constructor
    //    /// </summary>
    //    /// <param name="desc">The Description of this new group</param>
    //    public ProfileUserGroups(string desc)
    //    {
    //        GroupID = -1;
    //        Created = DateTime.Now;
    //        IsActive = true;
    //        Modified = DateTime.Now;
    //        Description = desc;
    //    }
    //}
}
