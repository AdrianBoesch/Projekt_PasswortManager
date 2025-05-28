using System;
using System.IO;
using System.Text.Json;

namespace Projekt_PasswortManager
{
    // 1a) Container für die Einstellungen
    public class Config
    {
        public string MasterPasswordHash { get; set; } = string.Empty;
    }

    // 1b) Service zum Laden/Speichern der Datei
    public static class ConfigService
    {
        private static readonly string _folder =
            Path.Combine(Environment.GetFolderPath(
                             Environment.SpecialFolder.ApplicationData),
                         "Projekt_PasswortManager");
        private static readonly string _path =
            Path.Combine(_folder, "config.json");

        public static Config Load()
        {
            if (!File.Exists(_path))
                return new Config();

            string json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<Config>(json)
                   ?? new Config();
        }

        public static void Save(Config cfg)
        {
            Directory.CreateDirectory(_folder);
            string json = JsonSerializer.Serialize(cfg,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }
    }
}

