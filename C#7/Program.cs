using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;


public static class FileManager
{
    private static string currentDirectory;

    private static void DisplayDriveInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine($"Диск: {drive.Name}, Свободно: {drive.TotalFreeSpace / (1024 * 1024 * 1024):F2} GB");
        }
    }

    private static void DisplayDirectoryInfo(string path)
    {
        Console.WriteLine($"Текущий каталог: {path}");

        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        foreach (string directory in directories)
        {
            Console.WriteLine($"[дириктория] {Path.GetFileName(directory)}");
        }

        foreach (string file in files)
        {
            Console.WriteLine($"[Файл] {Path.GetFileName(file)}, Размер: {new FileInfo(file).Length / (1024 * 1024 * 1024.0):F2} GB");
        }
    }

    public static void RunFileManager()
    {
        int choice;
        do
        {
            DisplayDriveInfo();
            Console.WriteLine("Выберите дисковод:");

            string[] drives = Directory.GetLogicalDrives();
            choice = ArrowMenu.ShowMenu(drives);

            if (choice >= 0 && choice < drives.Length)
            {
                currentDirectory = drives[choice];
                int menuChoice;

                do
                {
                    DisplayDirectoryInfo(currentDirectory);
                    Console.WriteLine("Выберите элемент:");

                    string[] items = Directory.GetFileSystemEntries(currentDirectory);
                    menuChoice = ArrowMenu.ShowMenu(items);

                    if (menuChoice >= 0 && menuChoice < items.Length)
                    {
                        string selectedItem = items[menuChoice];

                        if (Directory.Exists(selectedItem))
                        {
                            currentDirectory = selectedItem;
                        }

                        else if (File.Exists(selectedItem))
                        {
                            OpenFile(selectedItem);
                        }
                    }
                } while (menuChoice != -1);
            }

        } while (choice != -1);
    }

    private static void OpenFile(string filePath)
    {
        try
        {
            string extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".txt":
                    Process.Start("notepad.exe", filePath);
                    break;
                case ".docx":
                    Process.Start("winword.exe", filePath);
                    break;
                case ".pdf":
                    Process.Start("AcroRd32.exe", filePath);
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    Process.Start("mspaint.exe", filePath);
                    break;
                case ".xlsx":
                    Process.Start("excel.exe", filePath);
                    break;
                default:
                    Console.WriteLine($"Иди в попу, нет такого");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
        }
    }


class Program
    {
        static void Main()
        {
            FileManager.RunFileManager();
        }
    }
}