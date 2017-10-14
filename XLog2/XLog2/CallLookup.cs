using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLog2
{
    public class CallLookup
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Grid { get; set; }
        public string DXCC { get; set; }
        public string CQZone { get; set; }
        public string ITUZone { get; set; }
        public CallLookup()
        {
            FirstName = "";
            LastName = "";
            Addr1 = "";
            Addr2 = "";
            State = "";
            Zip = "";
            Country = "";
            Latitude = "";
            Longitude = "";
            Grid = "";
            DXCC = "";
            CQZone = "";
            ITUZone = "";
        }

        public CallLookup(string _firstname, string _lastname, string _addr1, string _addr2, 
                            string _state, string _zip, string _country, string _latitude, 
                            string _longitude, string _grid, string _dxcc, string _cqzone, 
                            string _ituzone)
        {
            FirstName = _firstname;
            LastName = _lastname;
            Addr1 = _addr1;
            Addr2 = _addr2;
            State = _state;
            Zip = _zip;
            Country = _country;
            Latitude = _latitude;
            Longitude = _longitude;
            Grid = _grid;
            DXCC = _dxcc;
            CQZone = _cqzone;
            ITUZone = _ituzone;
        }
    }
}
