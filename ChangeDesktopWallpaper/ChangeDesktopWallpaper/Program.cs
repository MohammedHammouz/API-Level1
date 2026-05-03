using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDesktopWallpaper
{
    internal class Program
    {
        const int SPI_SETDESKWALLPAPER = 0x0014;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern int SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);
        public static void SetWallpaper(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                Console.WriteLine("Image path cannot be null or empty.", nameof(imagePath));
                return;
            }
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Wallpaper file not found.", imagePath);
                return;
            }
            int result=SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath,SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            if (result == 0)
            {
                Console.WriteLine("Failed to set wallpaper. Win32 Error:");
                return;
            }
            Console.WriteLine("Successfully");
        }
        static void Main(string[] args)
        {
            SetWallpaper("C:\\Users\\m7ama\\OneDrive\\Desktop\\NewPic.JPG");
        }
    }
}
