using Serilog;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Projekt_PasswortManager
{
    public partial class VerificationWindow : Window
    {
        private readonly Config _cfg;
        private int fehlversuche = 0;
        private int cooldownMinuten = 0;
        private DateTime? cooldownEnde = null;

        public VerificationWindow()
        {
            InitializeComponent();
            _cfg = ConfigService.Load();
        }

        private async void OnVerifyClick(object sender, RoutedEventArgs e)
        {
            Log.Logger.Information("Master-Passwort-Verifizierung gestartet.");

            string entered = MasterPasswordBox.Password;

            if (cooldownEnde.HasValue && DateTime.Now < cooldownEnde.Value)
            {
                var verbleibend = cooldownEnde.Value - DateTime.Now;
                Log.Logger.Warning("Loginversuch während aktiver Sperre. Verbleibend: {Minutes}m {Seconds}s", verbleibend.Minutes, verbleibend.Seconds);
                MessageBox.Show($"Zugriff gesperrt. Bitte warte {verbleibend.Minutes} Minuten und {verbleibend.Seconds} Sekunden.");
                return;
            }

            if (string.IsNullOrEmpty(entered) || entered.Length < 8)
            {
                Log.Logger.Warning("Eingabe zu kurz oder leer bei Verifizierung.");
                MessageBox.Show("Bitte ein Passwort mit mindestens 8 Zeichen eingeben.");
                return;
            }

            try
            {
                string gespeichertesPasswort = CryptoService.Decrypt(_cfg.MasterPasswordHash);

                if (entered == gespeichertesPasswort)
                {
                    Log.Logger.Information("Erfolgreiche Master-Passwort-Verifizierung nach {Fehlversuche} Fehlversuch(en).", fehlversuche);
                    fehlversuche = 0;
                    cooldownMinuten = 0;
                    cooldownEnde = null;
                    OpenMainWindow(entered);
                }
                else
                {
                    fehlversuche++;
                    Log.Logger.Warning("Fehlgeschlagene Verifizierung. Fehlversuche: {Anzahl}", fehlversuche);
                    if (fehlversuche >= 3)
                    {
                        cooldownMinuten = (fehlversuche == 3) ? 0 : cooldownMinuten + 5;
                        int sekunden = (fehlversuche == 3) ? 10 : cooldownMinuten * 60;
                        cooldownEnde = DateTime.Now.AddSeconds(sekunden);
                        Log.Logger.Warning("Verifizierung gesperrt nach {Fehlversuche} Fehlversuchen. Sperrdauer: {Sekunden}s", fehlversuche, sekunden);
                        MessageBox.Show($"Falsches Passwort. Sperre für {sekunden / 60} Minuten und {sekunden % 60} Sekunden.");
                    }
                    else
                    {
                        int verbleibend = 3 - fehlversuche;
                        MessageBox.Show($"Falsches Passwort. Noch {verbleibend} Versuch(e).");
                    }

                    MasterPasswordBox.Clear();
                    MasterPasswordBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Fehler bei der Master-Passwort-Überprüfung.");
                MessageBox.Show("Fehler beim Überprüfen des Passworts:\n" + ex.Message);
            }
        }

        private void OpenMainWindow(string clearMasterPassword)
        {
            var main = new MainWindow(clearMasterPassword);
            main.Show();
            this.Close(); // wichtig, um das Loginfenster zu schließen
        }

        private void OnAddPwClick(object sender, RoutedEventArgs e)
        {
            // Optional: Kann verwendet werden, um z. B. VerifPwAdd zu öffnen
            VerifPwAdd addWindow = new VerifPwAdd();
            addWindow.ShowDialog(); // Modal öffnen
        }
    }
}