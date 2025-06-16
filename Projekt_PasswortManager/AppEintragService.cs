using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Projekt_PasswortManager
{
    public static class AppEintragService
    {
        private static readonly string dateipfad = "app_eintraege.json";

        public static void Speichern(List<AppEintrag> eintraege)
        {
            try
            {
                var verschlüsselt = eintraege.ConvertAll(e => new AppEintrag
                {
                    AppName = e.AppName,
                    Passwort = CryptoService.Encrypt(e.Passwort)
                });

                string json = JsonSerializer.Serialize(verschlüsselt, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(dateipfad, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Speichern: " + ex.Message);
            }
        }


        public static List<AppEintrag> Laden()
        {
            try
            {
                if (!File.Exists(dateipfad))
                    return new List<AppEintrag>();

                string json = File.ReadAllText(dateipfad);
                var verschlüsselt = JsonSerializer.Deserialize<List<AppEintrag>>(json) ?? new List<AppEintrag>();

                return verschlüsselt.ConvertAll(e => new AppEintrag
                {
                    AppName = e.AppName,
                    Passwort = CryptoService.Decrypt(e.Passwort)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden: " + ex.Message);
                return new List<AppEintrag>();
            }
        }
    }
}