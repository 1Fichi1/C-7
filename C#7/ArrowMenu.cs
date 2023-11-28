using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public static class ArrowMenu
{
    public static int ShowMenu(string[] items)
    {
        int currentChoice = 0;

        while (true)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (i == currentChoice)
                {
                    Console.Write("-> ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.WriteLine(items[i]);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.Clear();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                currentChoice = (currentChoice - 1 + items.Length) % items.Length;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                currentChoice = (currentChoice + 1) % items.Length;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                return currentChoice;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return -1;
            }
        }
    }
}