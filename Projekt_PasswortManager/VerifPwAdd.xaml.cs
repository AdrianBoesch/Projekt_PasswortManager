using System.Windows.Controls;
using System.Windows;
using Serilog;

namespace Projekt_PasswortManager
{
    public partial class VerifPwAdd : Window
    {
        public VerifPwAdd()
        {
            InitializeComponent();
        }

        private bool isPasswordVisible = false;

        private void ToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                PasswortTextBox.Text = NeuesPasswortBox.Password;
                NeuesPasswortBox.Visibility = Visibility.Collapsed;
                PasswortTextBox.Visibility = Visibility.Visible;
                ToggleBtn.Content = "🙈";
            }
            else
            {
                NeuesPasswortBox.Password = PasswortTextBox.Text;
                NeuesPasswortBox.Visibility = Visibility.Visible;
                PasswortTextBox.Visibility = Visibility.Collapsed;
                ToggleBtn.Content = "👁";
            }
        }

        private void PasswortBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
                PasswortTextBox.Text = NeuesPasswortBox.Password;
        }

        private void PasswortTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isPasswordVisible)
                NeuesPasswortBox.Password = PasswortTextBox.Text;
        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {
            Log.Logger.Information("Versuch, Master-Passwort zu ändern.");

            string altesPw = AltesPasswortBox.Password;
            string neuesPw = NeuesPasswortBox.Password;

            if (string.IsNullOrEmpty(altesPw) || string.IsNullOrEmpty(neuesPw))
            {
                Log.Logger.Warning("Passwortänderung abgebrochen: Eingabe unvollständig.");
                MessageBox.Show("Bitte beide Passwörter eingeben.");
                return;
            }

            if (neuesPw.Length < 8)
            {
                Log.Logger.Warning("Passwortänderung abgebrochen: Neues Passwort zu kurz.");
                MessageBox.Show("Neues Passwort muss mindestens 8 Zeichen lang sein.");
                return;
            }

            try
            {
                var cfg = ConfigService.Load();
                string gespeichertesPw = CryptoService.Decrypt(cfg.MasterPasswordHash);

               

                if (altesPw.Trim() != gespeichertesPw.Trim())
                {
                    Log.Logger.Warning("Passwortänderung fehlgeschlagen: Altes Passwort falsch.");
                    MessageBox.Show("Das alte Passwort ist falsch.");
                    return;
                }

                // Neues Passwort verschlüsseln und speichern
                string verschluesselt = CryptoService.Encrypt(neuesPw);
                cfg.MasterPasswordHash = verschluesselt;
                ConfigService.Save(cfg);
                Log.Logger.Information("Master-Passwort erfolgreich geändert.");

                MessageBox.Show("Neues Passwort erfolgreich gespeichert.");
                Close();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Fehler beim Speichern des neuen Master-Passworts.");
                MessageBox.Show("Fehler beim Speichern des neuen Passworts:\n" + ex.Message);
            }
        }

    }
}