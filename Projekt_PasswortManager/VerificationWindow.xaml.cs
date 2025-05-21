using System.Windows;
using System.Windows;

namespace Projekt_PasswortManager
{
    public partial class VerificationWindow : Window
    {
        
        private string expectedHash;

        public VerificationWindow(string storedHash)
        {
            InitializeComponent();
            
            expectedHash = storedHash;
        }

        private void Pruefen_Click(object sender, RoutedEventArgs e)
        {
           
            string input = PasswordCheckBox.Password;
           
            string inputHash = HashHelper.ComputeSha256Hash(input);

        
            if (inputHash == expectedHash)
            {
                MessageBox.Show("Passwort korrekt!", "OK",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Falsches Passwort!", "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }
    }
}
