using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace AudioAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Volume control app");
            Console.WriteLine("Choose an option");
            Console.WriteLine("1: Increase Volume");
            Console.WriteLine("2: Decrease Volume");
            Console.WriteLine("3: Set Volume (1-100)");
            Console.WriteLine("4: Exit");
            
            var device = new MMDeviceEnumerator()
                .GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            while (true)
            {
                Console.WriteLine("Enter your choice:");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        device.AudioEndpointVolume.VolumeStepUp();
                        Console.WriteLine($"Volume decreased → Now: {(int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100)}%");
                        break;
                    case "2":
                        device.AudioEndpointVolume.VolumeStepDown();
                        Console.WriteLine($"Volume decreased → Now: {(int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100)}%");
                        break;
                    case "3":
                        try
                        {
                            device.AudioEndpointVolume.MasterVolumeLevelScalar = float.Parse(Console.ReadLine()) / 100f;
                            Console.WriteLine($"Volume decreased → Now: {(int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100)}%");
                        }
                        catch
                        {

                        }
                        
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Ivalid number");
                        choice = Console.ReadLine();
                        break;
                }
            }
        }
    }
}
