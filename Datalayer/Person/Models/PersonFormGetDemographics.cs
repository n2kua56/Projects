using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;

namespace EZDeskDataLayer.Person.Models
{
    public class PersonFormGetDemographics
    {
        public enum PersonTypeEnum
        {
            Patient = 1,
            Doctor = 2,
            Staff = 3,
            ClinicalStaff = 4,
            ReferringDoctor = 5,
            Company = 6,
            ExtCarePerson = 7, 
            LabDoctor = 8,
            LabFacility = 9,
            InsuranceGuarantor = 10,
            InsuredPerson = 11,
            Unknown = 12
        }

        public int PersonID { get; set; }
        public string SharedID { get; set; }
        public string SSNO { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public PersonTypeEnum PersonType { get; set; }
        public int RaceTypeID { get; set; }
        public int LanguageTypeID { get; set; }
        public int EthnicityTypeID { get; set; }
        public string PicturePath { get; set; }
        public string Note { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string UDF4 { get; set; }
        public string UDF5 { get; set; }
        public string UDF6 { get; set; }
        public string UDF7 { get; set; }
        public string UDF8 { get; set; }
        public string UDF9 { get; set; }
        public string UDF10 { get; set; }
        public List<cModels.Communication> comms { get; set; }
        public List<aModels.Address> addresses { get; set; }

        public PersonFormGetDemographics()
        {
        }
    }

}
