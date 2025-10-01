using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
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
            Console.WriteLine("1. Skriv ny anteckning");
            Console.WriteLine("2. Lista alla anteckningar");
            Console.WriteLine("3. Sök anteckning på datum");
            Console.WriteLine("4. Spara till fil");
            Console.WriteLine("5. Läs från fil");
            Console.WriteLine("6. Ta bort en anteckning");
            Console.WriteLine("7. Avsluta");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddEntry(); break;
                case "2": ListEntries(); break;
                case "3": SearchEntry(); break;
                case "4": SaveToFile(); break;
                case "5": LoadFromFile(); break;
                case "6": RemoveEntry(); break;
                case "7": return;
                default: Console.WriteLine("Ogiltigt val, försök igen."); break;
            }
        }
    }
    static void AddEntry()
    {
        Console.Write("Ange datum (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            Console.WriteLine("Felaktigt datum");
            return;
        }
        Console.Write("Skriv din anteckling: ");
        string? text = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Texten kan inte vara tom.");
            return;
        }
        diaryEntries.Add(new DiaryEntry { Date = date, Text = text });
        Console.WriteLine("Anteckning sparad!");
    }

    static void ListEntries()
    {
        if (diaryEntries.Count == 0)
        {
            Console.WriteLine("Inga anteckningar finns.");
            return;
        }
        foreach (var e in diaryEntries)
        {
            Console.WriteLine($"{e.Date:yyyy-MM-dd}: {e.Text}");
        }
    }
    static void SearchEntry()
    {
        Console.Write("Ange datum att söka efter (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            var found = diaryEntries.Find(e => e.Date.Date == date.Date);
            if (found != null)
                Console.WriteLine($"{found.Date:yyyy-MM-dd}: {found.Text}");
            else
                Console.WriteLine("Ingen anteckning hittades för det datumet.");
        }
        else
        {
            Console.WriteLine("Felaktigt datum");
        }
    }
    static void SaveToFile()
    {
        try
        {
            string json = JsonSerializer.Serialize(diaryEntries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataFile, json);
            Console.WriteLine("Anteckningar sparade till fil.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid sparande av anteckningar: {ex.Message}");
        }
    }
    static void LoadFromFile()
    {
        try
        {
            if (File.Exists(dataFile))
            {
                string json = File.ReadAllText(dataFile);
                diaryEntries = JsonSerializer.Deserialize<List<DiaryEntry>>(json) ?? new List<DiaryEntry>();
                Console.WriteLine("Anteckningar inlästa.");
                ListEntries();
            }
            else
            {
                Console.WriteLine("Ingen sparad fil hittades.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid inläsning av anteckningar: {ex.Message}");
        }
    }
    static void RemoveEntry()
    {
        Console.Write("Ange datum för anteckningen att ta bort (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            var entry = diaryEntries.Find(e => e.Date.Date == date.Date);
            if (entry != null)
            {
                diaryEntries.Remove(entry);
                Console.WriteLine("Anteckning borttagen.");
            }
            else
            {
                Console.WriteLine("Ingen anteckning hittades för det datumet.");
            }
        }
        else
        {
            Console.WriteLine("Felaktigt datum");
        }
    }
}