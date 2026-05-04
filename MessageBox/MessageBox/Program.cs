using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageBox
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode,SetLastError =true)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        static void Main(string[] args)
        {
            MessageBox(IntPtr.Zero, "Mohammed Hammouz", "My name's", 0);
        }
    }
}
