using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.ehr.Models
{
    /// <summary>
    /// Indicates the various "areas" in the software that we want
    /// to collect audit data on.
    /// NOTE: Changes here must be reflected in the `ehr_auditarea` table.
    /// </summary>
    public enum AuditAreas 
    { 
        Person = 1, 
        PersonDemographics = 2, 
        User = 3, 
        TODO = 4, 
        Company = 5, 
        System = 6, 
        Documents = 7 
    };

    /// <summary>
    /// Indicates the various activities that can be audited.
    /// NOTE: Changes here must be reflected in the `ehr_auditactivity` table.
    /// </summary>
    public enum AuditActivities 
    { 
        Add = 1, 
        Edit = 2, 
        View = 3, 
        Login = 4, 
        Logout = 5,
        Print = 6,
        Fax = 7,
        Exported = 8

    }

    public class AuditItem
    {
        public int Id { get; set; }
        public DateTime AuditDateTime { get; set; }
        public int UserId { get; set; }
        public int? PersonId { get; set; }
        public AuditAreas AuditAreaId { get; set; }
        public AuditActivities AuditActivityId { get; set; }
        public string Description { get; set; }

        public AuditItem()
        {
        }

        public AuditItem(int userid, int? personid, AuditAreas auditareaid, AuditActivities auditactivityid, string description)
        {
            AuditDateTime = DateTime.Now;
            UserId = userid;
            PersonId = personid;
            AuditAreaId = auditareaid;
            AuditActivityId = auditactivityid;
            Description = description;
        }

    }
}
