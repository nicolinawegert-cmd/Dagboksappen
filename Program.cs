using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static List<DiaryEntry> diaryEntries = new List<DiaryEntry>();
    static string dataFile = Path.Combine("Data", "diary.json");

    static void Main()
    {
        Directory.CreateDirectory("Data");

        while (true)
        {
            Console.WriteLine("\n--- Dagboksappen ---");
            Console.WriteLine("1. Skriv ny annteckning");
            Console.WriteLine("2. Lista alla anteckningar");
            Console.WriteLine("3. Sök anteckning på datum");
            Console.WriteLine("4. Spara till fil");
            Console.WriteLine("5. Läs från fil");
            Console.WriteLine("6. Avsluta");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEntry();
                    break;
                case "2":
                    ListEntries();
                    break;
                case "3":
                    SearchEntry();
                    break;
                case "4":
                    SaveToFile();
                    break;
                case "5":
                    LoadFromFile();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }

}