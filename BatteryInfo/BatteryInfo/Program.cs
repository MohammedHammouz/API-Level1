using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryInfo
{
    internal class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;      // 0 = Offline, 1 = Online, 255 = Unknown
            public byte BatteryFlag;       // Flags for battery status
            public byte BatteryLifePercent; // 0-100, 255 = Unknown
            public byte Reserved1;
            public int BatteryLifeTime;    // Seconds remaining, -1 = Unknown
            public int BatteryFullLifeTime; // Seconds at full charge, -1 = Unknown
        }
        [DllImport("kernel32.dll")]
        public static extern bool GetSystemPowerStatus(out SYSTEM_POWER_STATUS sps);
        static void Main(string[] args)
        {
            if (GetSystemPowerStatus(out SYSTEM_POWER_STATUS status))
            {
                Console.WriteLine("Battery Information:");
                Console.WriteLine("AC Line Status: " + (status.ACLineStatus == 0 ? "Offline" : "Online"));
                Console.WriteLine(status.BatteryFlag);
                Console.WriteLine("Battery Charge Status: " + GetBatteryStatus(status.BatteryFlag));
                Console.WriteLine("Battery Life Percent: " + (status.BatteryLifePercent == 255 ? "Unknown" : status.BatteryLifePercent + "%"));
                Console.WriteLine("Battery Life Remaining: " + (status.BatteryLifeTime == -1 ? "Unknown" : status.BatteryLifeTime + " seconds"));
                Console.WriteLine("Full Battery Lifetime: " + (status.BatteryFullLifeTime == -1 ? "Unknown" : status.BatteryFullLifeTime + " seconds"));
            }
            else
            {
                Console.WriteLine("Unable to get battery status.");
            }
        }
        static string GetBatteryStatus(byte flag)
        {
            switch (flag)
            {
                case 1:
                    return "High, more than 66% charged";
                case 2:
                    return "Low, less than 33% charged";
                case 4:
                    return "Critical, less than 5% charged";
                case 8:
                    return "Charging";
                case 128:
                    return "No battery";
                case 255:
                    return "Unknown status";
                default:
                    return "Battery status not detected";
            }
        }
    }
}
