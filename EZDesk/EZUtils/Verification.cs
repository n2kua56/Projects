using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZUtils
{
    public class Verification
    {
        public static bool VerifyZipInput(string s, KeyPressEventArgs e)
        {
            bool rtn = false;

            Trace.Enter("Verification.VerifyZipInput");

            try
            {
                if ((e.KeyChar == 13) ||   //Enter
                    (e.KeyChar == 39) ||   //Left Arrow
                    (e.KeyChar == 37) ||   //Right Arrow
                    (e.KeyChar == 8))      //BackSpace
                {
                    rtn = true;
                }
                else
                {
                    if ((s.Length == 5) &&
                        ((e.KeyChar == 32) ||     //Space
                         (e.KeyChar == 45)))     // -
                    {
                        rtn = true;
                    }

                    else
                    {
                        if ((e.KeyChar >= 48) &&
                             (e.KeyChar <= 57))
                        {
                            rtn = true;
                        }
                    }
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZe = new EZException("Failed", ex);
                throw EZe;
            }

            finally
            {
                Trace.Exit("Verification.VerifyZipInput");
            }
        }

        public static bool VerifyZipCode(string s)
        {
            bool rtn = true;
            string zip = "";
            string zip5 = "";
            string zipplus = "";
            string[] flds = null;

            Trace.Enter("Verification.VerifyZipCode");

            try
            {
                zip = s.Trim();
                if ((zip.Length > 5) && (zip.Substring(5, 1) == "-"))
                {
                    zip = zip.Substring(0, 5) + " " +
                        zip.Remove(0, 6);
                }
                flds = zip.Split(' ');
                switch (flds.Length)
                {
                    case 0:
                        rtn = false;
                        break;
                    case 1:
                        zip5 = flds[0];
                        zipplus = "";
                        break;
                    case 2:
                        zip5 = flds[0];
                        zipplus = flds[1];
                        break;
                    default:
                        rtn = false;
                        break;
                }
                
                if (rtn)
                {
                    if (zip5.Length != 5)
                    {
                        rtn = false;
                    }
                    if ((zipplus.Length != 0) &&
                        (zipplus.Length != 4)) {
                        rtn = false;
                    }
                }

                if (rtn)
                {
                    rtn = isAllDigits(zip5);
                    for (int idx = 0; idx < zipplus.Length; idx++)
                    {
                        rtn = isAllDigits(zipplus);
                    }
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZe = new EZException("Failed", ex);
                throw EZe;
            }

            finally
            {
                Trace.Exit("Verification.VerifyZipCode");
            }
        }

        private static bool isAllDigits(string s)
        {
            bool rtn = true;
            char c = ' ';
            int idx = -1;

            Trace.Enter("Verification.isAllDigits");

            try
            {
                for (idx = 0; idx < s.Length; idx++)
                {
                    c = Convert.ToChar(s.Substring(idx, 1));
                    if ((c < '0') || (c > '9'))
                    {
                        rtn = false;
                    }
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZe = new EZException("Failed", ex);
                throw EZe;
            }

            finally
            {
                Trace.Exit("Verification.isAllDigits");
            }
        }

    }
}
