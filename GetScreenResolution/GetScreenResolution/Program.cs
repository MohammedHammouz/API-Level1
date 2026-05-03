using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GetScreenResolution
{
    internal class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        const int SM_CXSCREEN = 0; // screen width
        const int SM_CYSCREEN = 1; // screen height
        static void Main(string[] args)
        {
            int width = GetSystemMetrics(SM_CXSCREEN);
            int height = GetSystemMetrics(SM_CYSCREEN);
            Console.WriteLine($"Resolution: {width} x {height}");
        }
    }
}
