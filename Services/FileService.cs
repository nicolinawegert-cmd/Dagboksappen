using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace Dagboksappen.Services
{
    public static class FileService
    {
        private static string dataFile = Path.Combine("Data", "diary.json");
        private static List<DiaryEntry> diaryEntries = new List<DiaryEntry>();

        public static void Save(List<DiaryEntry> entries)
        {
            try
            {
                string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dataFile, json);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        public static List<DiaryEntry> Load()
        {
            try
            {
                if (File.Exists(dataFile))
                {
                    string json = File.ReadAllText(dataFile);
                    return JsonSerializer.Deserialize<List<DiaryEntry>>(json) ?? new List<DiaryEntry>();
                }
                return new List<DiaryEntry>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<DiaryEntry>();
            }
        }
        private static void LogError(Exception ex)
        {
            File.AppendAllText("error.log", $"{DateTime.Now}: {ex.Message}{Environment.NewLine}");
        }
    }
}