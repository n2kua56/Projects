using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLog2
{
    public class QSO
    {
        public int LogID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Call { get; set; }
        public String Frequency { get; set; }
        public String Mode { get; set; }
        public String TxRST { get; set; }
        public String RxRST { get; set; }
        public String Awards { get; set; }
        public bool QSLout { get; set; }
        public bool QSLin { get; set; }
        public String Power { get; set; }
        public String Name { get; set; }
        public String QTH { get; set; }
        public String Locator { get; set; }
        public String Unknown1 { get; set; }
        public String Unknown2 { get; set; }
        public String Remarks { get; set; }
        public String LogName { get; set; }
        public String Unknown1Label { get; set; }
        public String Unknown2Label { get; set; }
        public int CountryCode { get; set; }
        public int StateCode { get; set; }
        public int CountyCode { get; set; }
        public int BandID { get; set; }
        public int ModeID { get; set; }
        public QSO()
        {
            zClear();
        }

        /// <summary>
        /// For the ARRL 10meter contest.
        /// </summary>
        /// <param name="_ID">0 for an ADD, otherwise the id of the record being updated.</param>
        /// <param name="_Call">Called Station</param>
        /// <param name="_StartDate">DateTime the QSO started</param>
        /// <param name="_TxRST">RST Sent</param>
        /// <param name="_RxRST">RST Received</param>
        /// <param name="_StateCode">State Code of the other station</param>
        /// <param name="_Remarks">Remarks</param>
        /// <param name="_Mode">Mode of the contact (CW | Phone)</param>
        /// <param name="_LogName">LogName</param>
        public QSO(int _ID, string _Call, DateTime _StartDate, string _TxRST, string _RxRST, 
                    int _StateCode, string _Remarks, string _Mode, string _LogName)
        {
            zClear();

            LogID = _ID;
            Call = _Call;
            StartDate = _StartDate;
            TxRST = _TxRST;
            RxRST = _RxRST;
            StateCode = _StateCode;
            Remarks = _Remarks;
            Mode = _Mode;
            LogName = _LogName;
        }

        /// <summary>
        /// For the HamLog View
        /// </summary>
        /// <param name="_ID">0 for Add, otherwise ID of entry being updated.</param>
        /// <param name="_Call">Called Station</param>
        /// <param name="_StartDate">DateTime the QSO started</param>
        /// <param name="_BandId">ID of the band</param>
        /// <param name="_Frequency">Actual Frequency</param>
        /// <param name="_ModeId">ID of the Mode</param>
        /// <param name="_Power">Power</param>
        /// <param name="_CountryCode">CountryCode of the other station</param>
        /// <param name="_TxRST">RST Sent</param>
        /// <param name="_RxRST">RST Received</param>
        /// <param name="_Name">Other Operators name</param>
        /// <param name="_EndDate">DateTime the QSO ended</param>
        /// <param name="_StateCode">State Code of the other station</param>
        /// <param name="_CountyCode">County Code of the other station</param>
        /// <param name="_Other">Other Comments</param>
        /// <param name="_QSLRcvd">Received a QSL?</param>
        /// <param name="_QSLSent">Sent a QSL?</param>
        /// <param name="_Remarks">Remarks</param>
        /// <param name="_LogName">LogName</param>
        public QSO(int _ID, string _Call, DateTime _StartDate, int _BandId, string _Frequency, int _ModeId,
                    string _Power, int _CountryCode, string _TxRST, string _RxRST, string _Name,
                    DateTime _EndDate, int _StateCode, int _CountyCode, string _Other, bool _QSLRcvd,
                    bool _QSLSent, string _Remarks, string _LogName)
        {
            zClear();

            LogID = _ID;
            Call = _Call;
            StartDate = _StartDate;
            BandID = _BandId;
            Frequency = _Frequency;
            ModeID = _ModeId;
            Power = _Power;
            CountryCode = _CountryCode;
            TxRST = _TxRST;
            RxRST = _RxRST;
            Name = _Name;
            EndDate = _EndDate;
            StateCode = _StateCode;
            CountyCode = _CountyCode;
            Unknown1 = _Other;
            QSLin = _QSLRcvd;
            QSLout = _QSLSent;
            Remarks = _Remarks;
            LogName = _LogName;
        }
        private void zClear()
        {
            LogID = -1;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            Call = null;
            Frequency = null;
            Mode = null;
            TxRST = null;
            RxRST = null;
            Awards = null;
            QSLout = false;
            QSLin = false;
            Power = null;
            Name = null;
            QTH = null;
            Locator = null;
            Unknown1 = null;
            Unknown2 = null;
            Remarks = null;
            LogName = null;
            Unknown1Label = null;
            Unknown2Label = null;
            CountryCode = -1;
            StateCode = -1;
            CountyCode = -1;
            BandID = -1;
            ModeID = -1;
        }

    }
}
