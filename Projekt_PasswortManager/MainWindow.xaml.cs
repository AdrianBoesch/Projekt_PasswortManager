using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_PasswortManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<AppEingabe> apps = new List<AppEingabe>();
        public MainWindow()
        {
            InitializeComponent();
            AppListe.ItemsSource = apps;
        }

        private void AppListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ausgewählt = AppListe.SelectedItem as AppEingabe;
            if (ausgewählt != null)
            {
                var fenster = new PasswortFenster(ausgewählt.Passwort);
                fenster.Show();
            }
        }
    }
}