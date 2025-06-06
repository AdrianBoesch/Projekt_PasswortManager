using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Projekt_PasswortManager
{
    public partial class VerificationWindow : Window
    {
        private readonly Config _cfg;
        private string passwort;

        public VerificationWindow()
        {
            InitializeComponent();
            
            _cfg = ConfigService.Load();
        }

        public VerificationWindow(string passwort)
        {
            this.passwort = passwort;
        }

        private void OnVerifyClick(object sender, RoutedEventArgs e)
        {
            string entered = MasterPasswordBox.Password;
            if (string.IsNullOrEmpty(entered))
            {
                MessageBox.Show("Bitte ein Passwort eingeben.");
                return;
            }

            string hash = ComputeSha256Hash(entered);

            if (string.IsNullOrEmpty(_cfg.MasterPasswordHash))
            {
                // new master password anlegen
                _cfg.MasterPasswordHash = hash;
                ConfigService.Save(_cfg);
                OpenMainWindow(entered);
            }
            else if (hash == _cfg.MasterPasswordHash)
            {
                // Richtiges Passwort
                OpenMainWindow(entered);
            }
            else
            {
                // Falsch
                MessageBox.Show("Falsches Passwort.", "Fehler",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                MasterPasswordBox.Clear();
                MasterPasswordBox.Focus();


            }


        }

        private void OpenMainWindow(string clearMasterPassword)
        {
            var main = new MainWindow(clearMasterPassword);
            main.Show();
            Close();
        }

        private string ComputeSha256Hash(string raw)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return Convert.ToBase64String(bytes);
        }

        private void OnAddPwClick(object sender, RoutedEventArgs e)
        {
            VerifPwAdd verifPwAdd = new VerifPwAdd();
            verifPwAdd.Show();

            
            
        }
    }
}
