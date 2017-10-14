using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZDesk
{
    public partial class frmExternalProgram : Form
    {
        public string ProgramNameFullPathName { get; set; }
        private string mPath = "";
        private string mFileName = "";

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        private static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);

        //assorted constants needed
        //assorted constants needed
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar
        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CLOSE = 0x10;
        private const int WS_CHILD = 0x40000000;

        public delegate bool EnumedWindow(IntPtr handleWindow, ArrayList handles);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumedWindow lpEnumFunc, ArrayList lParam);

        public static ArrayList GetWindows()
        {
            ArrayList windowHandles = new ArrayList();
            EnumedWindow callBackPtr = GetWindowHandle;
            EnumWindows(callBackPtr, windowHandles);

            return windowHandles;
        }

        private static bool GetWindowHandle(IntPtr windowHandle, ArrayList windowHandles)
        {
            windowHandles.Add(windowHandle);
            return true;
        }

        // This function sets the parent of the window with class
        // ClassClass to the form/control the method is in.
        public void Reparent(string programName)
        {
            ArrayList lst = GetWindows();
            for (int idx = 0; idx < lst.Count; idx++)
            {
                StringBuilder strBuff = new StringBuilder("                                                                    ", 80);
                //((object)lngResult) = 
                GetWindowText((IntPtr)lst[idx], strBuff, 40);
                string name = strBuff.ToString();
                if (name.Length > 0)
                {
                    name = idx.ToString() + ": " + lst[idx].ToString() + " - " + name;
                }
                //GetWindowText(lst[idx], out MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);
                //  55: 4852208 - Form1.ods - OpenOffice Calc
                //  57: 3216242 - Form1
                //  92: 987918 - Untitled 1 - OpenOffice Calc
                // 100: 3740412 - Untitled 2 - OpenOffice Writer
            }
            //get handle of parent form (.net property)
            IntPtr par = this.pnlHost.Handle;

            //get handle of child form (win32)
            IntPtr child = FindWindow(null, programName);

            //set parent of child form
            SetParent(child, par);

            //get current window style of child form
            long style = GetWindowLong(child, GWL_STYLE);

            //take current window style and remove WS_CAPTION from it
            //SetWindowLong(child, GWL_STYLE, (style & ~WS_CAPTION));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPathName"></param>
        public void StartExternal(string fullPathName, string parms)
        {
            string fullpath = (fullPathName.Length > 0) ? fullPathName : ProgramNameFullPathName;
            if (fullpath.Length > 0)
            {
                ProgramNameFullPathName = fullpath;
                FileInfo fi = new FileInfo(fullpath);
                if (fi.Exists)
                {
                    mPath = fi.DirectoryName;                       //Get the path, minus file name
                    mFileName = fi.Name;

                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = fullPathName;
                    p.StartInfo.Arguments = parms;

                    p.Start();
                    p.WaitForExit();
                    p.Close();

                    //TODO: Grab the program and put it in the form.
                    fi = new FileInfo(parms);
                    string filename = fi.Name;
                    filename = filename = " - ";
                    filename += "OpenOffice Calc";
                    Reparent(filename);
                }
                else
                {
                    //TODO: Problem, program file wasn't found.
                }
            }
        }

        public frmExternalProgram()
        {
            InitializeComponent();
        }

        private void frmExternalProgram_Load(object sender, EventArgs e)
        {

        }


    }
}
