// 代码生成时间: 2025-10-14 02:33:22
using System;
using System.IO;
using System.Collections.Generic;

namespace DiskSpaceManagerApp
{
    public class DiskSpaceManager
    {
        // Method to get the free space of the specified drive in GB
        public static long GetFreeSpace(string drive)
        {
            try
            {
                DriveInfo driveInfo = new DriveInfo(drive);
                return driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024); // Convert to GB
            }
            catch (Exception ex)
            {
                // Log error message
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        // Method to list all drives and their free space
        public static List<string> ListDrives()
        {
            var drives = new List<string>();
            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in allDrives)
                {
                    if (drive.IsReady)
                    {
                        drives.Add($"Drive {drive.Name} - Free Space: {GetFreeSpace(drive.Name)} GB");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error message
                Console.WriteLine($"Error: {ex.Message}");
            }
            return drives;
        }

        // Main method to run the program
        public static void Main(string[] args)
        {
            Console.WriteLine("Disk Space Manager");
            Console.WriteLine("---------------------");

            // List all drives and their free space
            List<string> drivesInfo = ListDrives();
            foreach (string info in drivesInfo)
            {
                Console.WriteLine(info);
            }

            // Get free space of a specific drive
            string driveToCheck = "C";
            long freeSpace = GetFreeSpace(driveToCheck);
            if (freeSpace != -1)
            {
                Console.WriteLine($"Free space on drive {driveToCheck}: {freeSpace} GB");
            }
            else
            {
                Console.WriteLine($"Failed to retrieve free space for drive {driveToCheck}");
            }
        }
    }
}