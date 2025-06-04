using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_PasswortManager
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        private string echtesPasswort;
        private bool istSichtbar = false;

        public ViewWindow(string passwort)
        {
            InitializeComponent();
            echtesPasswort = passwort;
            AktualisiereAnzeige();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            istSichtbar = !istSichtbar;
            AktualisiereAnzeige();
        }

        private void AktualisiereAnzeige()
        {
            if (istSichtbar)
            {
                PasswortText.Text = echtesPasswort;
                ToggleButton.Content = "Verbergen";
            }
            else
            {
                PasswortText.Text = new string('●', echtesPasswort.Length); 
                ToggleButton.Content = "Anzeigen";
            }
        }
    }
}

