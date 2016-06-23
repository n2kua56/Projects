using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///<
namespace EZDeskDataLayer.ehr.Models
{
    /// <remarks>
    ///                 PROFILE SUBSYSTEM DATA MODEL
    ///                 
    ///                                +------------------+
    ///                              +-+----------------+ |
    ///    Defines user personal     | ProfileUser      | |
    ///    overrides for a profile   +------------------+ |
    ///    entry. Any number of      |    ProfUserID    | |
    ///    user can override any     |    ProfDefaultID | |<<--+
    ///    profile entry. A user     |    UserID        | |    |
    ///    can override any number   |    val *1        +-+    |
    ///    of profile entries.       +------------------+      |
    ///                                                        |
    ///  +------------------+          +------------------+    |
    ///  | ProfileCategory  |        +-+----------------+ |    |
    ///  +------------------+        | ProfileDefault   | |    |       Defines the profile key
    ///  |   ProfCategoryID |<--+    +------------------+ |<---+       within a category and 
    ///  |   name           |   |    |   ProfDefaultID  | |<---------+ it's default value. A
    ///  +------------------+   +-->>|   ProfCategoryID | |          | profile entry must belong
    ///  Defines categories. The     |   key (name)     | |          | to a category. Users can
    ///  developer defines all of    |   val *3         +-+          | change these default values,
    ///  the Categories              +------------------+            | the developer creates the
    ///                                                              | profile default records!
    ///  +--------------------+            +--------------------+    |
    ///  | ProfileUserGroups  |          +-+------------------+ |    |
    ///  +--------------------+<----+    | ProfileGroups      | |    | Defines the override
    ///  |   ProfUserGroupsID |<--+ |    +--------------------+ |    | value for the members in
    ///  |   name             |   | |    |   ProfGroupId      | |    | the group. A group can
    ///  +--------------------+   | +-->>|   ProfUserGroupsID | |    | override any number of 
    ///  Defines Usesr Groups.    |      |   ProfDefaultID    | |<---+ profile entries. Each
    ///  There can be any number  |      |   Val *2           +-+      ProfileGroup must belong
    ///  of groups. Users define  |      +--------------------+        to a group and point to a
    ///  what groups there are.   |                                    profile entry. User set the
    ///                           |      +---------------------+       Group override values.
    ///                           |    +-+-------------------+ |
    ///  Defines the users that   |    | ProfileGroupMembers | |
    ///  are members of the       |    +---------------------+ |
    ///  group. Any number of     |    |   ProfGroupMemberID | |
    ///  users can be added to    +-->>|   ProfUserGroupsID  | |
    ///  a group. A user may           |   UserID            +-+
    ///  only be a member of           +---------------------+
    ///  one group. Users set  
    ///  who is in a group.
    /// </remarks>
    
    #region ProfileCategory

    /// <summary>
    /// A Profile entry must have a Category. The category
    /// records are added by the developer.
    /// Table: prof_profcategories
    /// </summary>
    public class ProfileCategory
    {
        /// <summary>
        /// ID of the Category record.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The Date/Time the Category record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// True the Categry record is active, False it is not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date/Time the Profile record was last updated.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// The Name of the Category. Len 50
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The Description of the Category. Len 128
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initialize a new empty Category object. 
        /// </summary>
        public ProfileCategory()
        {
        }

        /// <summary>
        /// Initialize a new Category object specifing the
        /// category name and description. The rest of the
        /// fields are set to new record values. The ID 
        /// defaults to -1 to indicate it has not yet been
        /// written to the database.
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="desc"></param>
        public ProfileCategory(string cat, string desc)
        {
            ID = -1;
            Created = DateTime.Now;
            IsActive = true;
            Modified = DateTime.Now;
            Category = cat;
            Description = desc;
        }
    }

    #endregion

    #region ProfileDefault

    /// <summary>
    /// A profile entry must also have default value. The
    /// Default object establishes the Profile "key" via 
    /// the ProfKey field and the default value everyone
    /// gets, unless overridden via the Val field. The
    /// Profile value MUST belong to a Category, specified
    /// by the CategoryID field. The Default record which
    /// creates the profile item is added by the developer.
    /// TABLE: prof_profdefault
    /// </summary>
    public class ProfileDefault
    {
        /// <summary>
        /// ID of the Default record.
        /// </summary>
        public int ProfID { get; set; }

        /// <summary>
        /// Name of the Profile entry this object creates. Len = 25.
        /// </summary>
        public string ProfKey { get; set; }

        /// <summary>
        /// Index of the Category object this Profile entry belongs to.
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// The Date/Time the Default record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// True the Default record is active, False it is not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date/Time the Profile record was last updated.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// The default value everyone gets unless the profile 
        /// entry is overridden. Len 255
        /// </summary>
        public string Val { get; set; }

        /// <summary>
        /// The Description of the Default. Len 200
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the default entry/value is available/shown
        /// in the Settings form.
        ///   0 = Not shown, can not be modified
        ///   1 = The entry/value is shown and can be modified by the user.
        /// </summary>
        public int Security { get; set; }

        /// <summary>
        /// Initialize a new empty Default object. 
        /// </summary>
        public ProfileDefault()
        {
        }

        /// <summary>
        /// Initialize a new Default with the specified key (name),
        /// for the specified Category with the given default value.
        /// The description of the Profile item is also set.  All 
        /// other fields default to the new item values (created 
        /// and modified NOW, IsActive and Visible in Setup.
        /// The ProfID defaults to -1 to indicate it is new and has
        /// not yet been written to the database.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="catID"></param>
        /// <param name="val"></param>
        /// <param name="desc"></param>
        public ProfileDefault(string key, int catID, string val, string desc)
        {
            ProfID = -1;
            ProfKey = key;
            CategoryID = catID;
            IsActive = true;
            Val = val;
            Description = desc;
            Security = 1;
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
    }

    #endregion

    #region ProfileUserGroups

    /// <summary>
    /// This item represents a single Profile Group.  Users may
    /// be added to a group and then a value set for the profile
    /// key for the group.  All users assigned to the gruop will
    /// then have the same value UNLESS they have a user profusers
    /// entry to override the value with a value for them.
    /// TABLE: prof_profusergroups
    /// </summary>
    public class ProfileUserGroups
    {
        /// <summary>
        /// ID of the UserGroup record.
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Date/Time the UserGroup was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// True the User Group record is active, False it is not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date/Time the User Group was last modified.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// The Description of this User Group. Len 50
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ProfileUserGroups empty Constructor
        /// </summary>
        public ProfileUserGroups()
        {
        }

        /// <summary>
        /// ProfileUserGroups Constructor
        /// </summary>
        /// <param name="desc">The Description of this new group</param>
        public ProfileUserGroups(string desc)
        {
            GroupID = -1;
            Created = DateTime.Now;
            IsActive = true;
            Modified = DateTime.Now;
            Description = desc;
        }
    }

    #endregion

    #region ProfGroups

    /// <summary>
    /// Once we have a Profile entry (Category and Default entries)
    /// and we have a Group entry we can specify an override value
    /// for the group. The ProfGroup entry will provide that override.
    /// It must have a CategoryID and ProfileID that will be overridden 
    /// and the GroupID this override is for.
    /// TABLE: prof_profgroups
    /// </summary>
    public class ProfGroups
    {
        public int ProfGroupID { get; set; }    // 'id for this row',
        public int CategoryID { get; set; }     // 'Category this profile group belongs to',
        public int GroupID { get; set; }        // 'Index into profGroups that this override belongs to',
        public int ProfDefID { get; set; }      // 'index in the ProfDefault that this record overrides for group members',
        public DateTime Created { get; set; }   // 'Date and time this record was created',
        public bool IsActive { get; set; }      // '1=Active  0=Deleted',
        public DateTime Modified { get; set; }  // 'Date and time this record was last updated',
        public string Val { get; set; }         // 'Value to use for members of this group',
    }

    #endregion

    #region ProfGroupMembers

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class ProfileUsers
    {
        public int ProfUserID { get; set; }
        public int CategoryID { get; set; }
        public int ProfID { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public DateTime Modified { get; set; }
        public string Val { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProfileUsers()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="profid"></param>
        /// <param name="val"></param>
        /// <param name="userid"></param>
        public ProfileUsers(int categoryid, int profid, string val, int userid)
        {
            ProfUserID = -1;
            CategoryID = categoryid;
            ProfID = profid;
            IsActive = true;
            Val = val;
            UserId = userid;
        }
    }
}
