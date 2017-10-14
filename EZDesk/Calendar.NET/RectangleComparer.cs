using System.Collections.Generic;
using System.Drawing;

namespace Calendar.NET
{
    /// <summary>
    /// 
    /// </summary>
    public class RectangleComparer : IComparer<Rectangle>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Rectangle x, Rectangle y)
        {
            return x.Y.CompareTo(y.Y);
        }
    }
}
