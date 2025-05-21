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

namespace Projekt_PasswortManager
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public AppEintrag NeuerEintrag { get; private set; }
        public EditWindow()
        {
            InitializeComponent();
        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AppNameBox.Text) && !string.IsNullOrWhiteSpace(PasswortBox.Password))
            {
                NeuerEintrag = new AppEintrag
                {
                    AppName = AppNameBox.Text,
                    Passwort = PasswortBox.Password
                };
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte App-Name und Passwort eingeben.");
            }

        }
    }
}

    

