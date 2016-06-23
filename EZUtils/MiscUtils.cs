using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Configuration;
using System.Reflection;

namespace EZUtils
{
    public class MiscUtils
    {
        public static string MethodName(int nestingLevel)
        {
            try
            {
                String s;
                // Get the name of the method that threw the exception
                // ASSUMPTION: this method will only give the correct name if called from the expected next level.
                // It is intended to be called from the constructors.
                System.Reflection.MethodBase b = new System.Diagnostics.StackTrace().GetFrame(nestingLevel).GetMethod();
                s = b.DeclaringType.FullName + '.' + b.Name;
                return s;
            }
            catch (Exception e)
            {
                EZUtils.EventLog.WriteErrorEntry(e);
            }
            return "unknown";
        }

        // Assumes that in AssemblyInfo.cs,
        // the version is specified as 1.0.* or the like,
        // with only 2 numbers specified;
        // the next two are generated from the date.
        // This routine decodes them.
        public static DateTime DateCompiled()
        {
            Trace.Enter("MiscUtils.DateCompiled");

            System.Version v =
                System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
            DateTime t = new DateTime(2000, 1, 1).AddDays(v.Build); ;

            Trace.Exit("MiscUtils.DateCompiled");

            return t;
        }

        // Assumes that in AssemblyInfo.cs,
        // the version is specified as 1.0.* or the like,
        // with only 2 numbers specified;
        // the next two are generated from the date.
        // This routine decodes them.
        public static string Version()
        {
            Trace.Enter("MiscUtils.Version");

            System.Version v =
                System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
            string rtn = v.Major.ToString() + "." +
                v.Minor.ToString();

            Trace.Exit("MiscUtils.Version");

            return rtn;
        }

        public static string Config(
            string key, 
            System.Configuration.SettingsPropertyCollection props)
        {
            Trace.Enter("MiscUtils.Config",key);
            
            string rtn = "";

            try
            {
                foreach (System.Configuration.SettingsProperty sp in props)
                {
                    if (sp.Name == key)
                    {
                        rtn = sp.DefaultValue.ToString();
                        break;
                    }
                }
                return rtn;
            }

            catch (Exception ex)
            {
                Trace.WriteLine(Trace.TraceLevels.None, "** ERROR: " + ex.Message);
                return "";
            }

            finally
            {
                Trace.Exit("MiscUtils.Config", rtn);
            }
        }

    }
}
