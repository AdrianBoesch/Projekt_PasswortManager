
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
    public partial class EditWindow : Window
    {
        public AppEintrag NeuerEintrag { get; private set; }

        public EditWindow()
        {
            InitializeComponent();
        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {
            
            string name = AppNameBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Bitte App-Name eingeben.");
                return;
            }

            
            string pw = PasswortBox.Password;
            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("Bitte Passwort eingeben.");
                return;
            }

            
            string pwHash = HashHelper.ComputeSha256Hash(pw);

            
            NeuerEintrag = new AppEintrag
            {
                AppName = name,
                Passwort = pwHash
            };

           
            DialogResult = true;
            Close();
        }
    }
}

