using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace APIFirstApp
{
    internal class Program
    {
        
        public class TextApi
        {
            public const uint GENERIC_WRITE = 0x40000000;
            private const uint FILE_SHARE_READ = 0x00000001;
            private const uint CREATE_ALWAYS = 4;
            private const uint FILE_ATTRIBUTE_NORMAL = 0x80;
            private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

            // Import CreateFile from kernel32.dll
            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr CreateFile(
                string lpFileName,
                uint dwDesiredAccess,
                uint dwShareMode,
                IntPtr lpSecurityAttributes,
                uint dwCreationDisposition,
                uint dwFlagsAndAttributes,
                IntPtr hTemplateFile);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint SetFilePointer(
                IntPtr hFile,
                int lDistanceToMove,
                IntPtr lpDistanceToMoveHigh,
                uint dwMoveMethod);
            // Import WriteFile from kernel32.dll
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteFile(
                IntPtr hFile,
                byte[] lpBuffer,
                uint nNumberOfBytesToWrite,
                out uint lpNumberOfBytesWritten,
                IntPtr lpOverlapped);

            // Import CloseHandle from kernel32.dll
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hObject);

        }
        static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        public static void AddWordsToTextFile(string filePath, string textToWrite)
        {
            const uint GENERIC_WRITE = 0x40000000;
            const uint FILE_SHARE_READ = 0x00000001;
            const uint CREATE_ALWAYS = 4;
            const uint FILE_ATTRIBUTE_NORMAL = 0x80;

            const uint FILE_END = 2;

            // move pointer to end of file
            

            // Create or overwrite the file
            IntPtr fileHandle = TextApi.CreateFile(
                filePath,
                GENERIC_WRITE,
                FILE_SHARE_READ,
                IntPtr.Zero,
                CREATE_ALWAYS,
                FILE_ATTRIBUTE_NORMAL,
                IntPtr.Zero);
            TextApi.SetFilePointer(fileHandle, 0, IntPtr.Zero, FILE_END);

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(textToWrite))
            {
                Console.WriteLine("Invalid file path or text.");
                return;
            }
            if (fileHandle == INVALID_HANDLE_VALUE)
            {
                Console.WriteLine($"Failed to create file. Error: {Marshal.GetLastWin32Error()}");
                return;
            }

            try
            {
                // Convert string to bytes (UTF-8)
                byte[] buffer = Encoding.UTF8.GetBytes(textToWrite);
                if (!TextApi.WriteFile(fileHandle, buffer, (uint)buffer.Length, out  uint bytesWritten, IntPtr.Zero))
                {
                    Console.WriteLine($"Failed to write to file. Error: {Marshal.GetLastWin32Error()}");
                }
                else
                {
                    Console.WriteLine($"Successfully wrote {bytesWritten} bytes to {filePath}");
                }
            }
            finally
            {
                // Always close the handle
                TextApi.CloseHandle(fileHandle);
            }
        }
        static void Main(string[] args)
        {
            AddWordsToTextFile(@"C:\Users\m7ama\OneDrive\Desktop\API-Level1\API-Level1\APIFirstApp.txt", "\nOrange,Banana");
            
        }
    }
}
