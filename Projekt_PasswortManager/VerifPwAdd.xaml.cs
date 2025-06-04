using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Projekt_PasswortManager
{
    /// <summary>
    /// Interaction logic for VerifPwAdd.xaml
    /// </summary>
    public partial class VerifPwAdd : Window
    {

        public VerifPw NeuerEintrag { get; private set; }
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
                PasswortTextBox.Text = PasswortBox.Password;
                PasswortBox.Visibility = Visibility.Collapsed;
                PasswortTextBox.Visibility = Visibility.Visible;
                ToggleBtn.Content = "🙈";
            }
            else
            {
                PasswortBox.Password = PasswortTextBox.Text;
                PasswortBox.Visibility = Visibility.Visible;
                PasswortTextBox.Visibility = Visibility.Collapsed;
                ToggleBtn.Content = "👁";
            }
        }

        private void PasswortBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
                PasswortTextBox.Text = PasswortBox.Password;
        }

        private void PasswortTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isPasswordVisible)
                PasswortBox.Password = PasswortTextBox.Text;
        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {

            string pw = PasswortBox.Password;
            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("Bitte Passwort eingeben.");
                return;
            }


            DialogResult = true;
            Close();
        }
    }
}
