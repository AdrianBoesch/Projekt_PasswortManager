using System.Windows.Controls;
using System.Windows;

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
            string altesPw = AltesPasswortBox.Password;
            string neuesPw = NeuesPasswortBox.Password;

            if (string.IsNullOrEmpty(altesPw) || string.IsNullOrEmpty(neuesPw))
            {
                MessageBox.Show("Bitte beide Passwörter eingeben.");
                return;
            }

            if (neuesPw.Length < 8)
            {
                MessageBox.Show("Neues Passwort muss mindestens 8 Zeichen lang sein.");
                return;
            }

            try
            {
                var cfg = ConfigService.Load();
                string gespeichertesPw = CryptoService.Decrypt(cfg.MasterPasswordHash);

                MessageBox.Show($"Gespeichertes Passwort (entschlüsselt): '{gespeichertesPw}'");
                MessageBox.Show($"Eingegebenes altes Passwort: '{altesPw}'");

                if (altesPw.Trim() != gespeichertesPw.Trim())
                {
                    MessageBox.Show("Das alte Passwort ist falsch.");
                    return;
                }

                // Neues Passwort verschlüsseln und speichern
                string verschluesselt = CryptoService.Encrypt(neuesPw);
                cfg.MasterPasswordHash = verschluesselt;
                ConfigService.Save(cfg);

                MessageBox.Show("Neues Passwort erfolgreich gespeichert.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern des neuen Passworts:\n" + ex.Message);
            }
        }

    }
}