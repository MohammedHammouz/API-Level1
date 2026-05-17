using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProcessesList
{
    internal class Program
    {
        // Import EnumProcesses from psapi.dll
        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool EnumProcesses([Out] uint[] lpidProcess, uint cb, out uint lpcbNeeded);
        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, uint size);
        [DllImport("psapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_MEMORY_COUNTERS
        {
            public uint cb;
            public uint PageFaultCount;
            public uint PeakWorkingSetSize;
            public uint WorkingSetSize;
            public uint QuotaPeakPagedPoolUsage;
            public uint QuotaPagedPoolUsage;
            public uint QuotaPeakNonPagedPoolUsage;
            public uint QuotaNonPagedPoolUsage;
            public uint PagefileUsage;
            public uint PeakPagefileUsage;
        }
        const uint PROCESS_QUERY_INFORMATION = 0x0400;
        const uint PROCESS_VM_READ = 0x0010;

        static void Main(string[] args)
        {
            uint[] processIds = new uint[1024];
            uint bytesReturned;

            if (EnumProcesses(processIds, (uint)(processIds.Length * sizeof(uint)), out bytesReturned))
            {
                int count = (int)(bytesReturned / sizeof(uint));
                Console.WriteLine("Number of processes: {0}", bytesReturned / sizeof(uint));
                for (int i = 0; i < count; i++)
                {
                    uint pid = processIds[i];
                    if (pid == 0) continue;

                    IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, pid);
                    

                    if (hProcess != IntPtr.Zero)
                    {
                        PROCESS_MEMORY_COUNTERS memCounters;
                        if (GetProcessMemoryInfo(hProcess, out memCounters, (uint)Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS))))
                        {
                            string processName = "Unknown";
                            try
                            {
                                Process proc = Process.GetProcessById((int)pid);
                                processName = proc.ProcessName;
                            }
                            catch (Exception)
                            {
                                // Process might have exited or access denied
                            }

                            Console.WriteLine($"Process ID: {pid}, Name: {processName} - Memory Usage: {memCounters.WorkingSetSize / 1024} KB");
                        }

                        CloseHandle(hProcess);
                    }

                    Console.WriteLine($"{hProcess} (PID: {pid.GetType()})");
                }
            }
            else
            {
                Console.WriteLine("Failed to enumerate processes.");
            }
        }
    }
}
