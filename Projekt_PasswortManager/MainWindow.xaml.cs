using PasswortApp;
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
        private List<AppEintrag> apps = new List<AppEintrag>();

        public MainWindow()
        {
            InitializeComponent();
            AppListe.ItemsSource = apps;
        }

        private void AppListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ausgewählt = AppListe.SelectedItem as AppEintrag;
            if (ausgewählt != null)
            {
                var fenster = new EditWindow(ausgewählt.Passwort);
                fenster.Show();
            }
        }

        private void HinzufügenButton_Click(object sender, RoutedEventArgs e)
        {
            var eingabeFenster = new AppHinzufügenFenster();
            if (eingabeFenster.ShowDialog() == true)
            {
                apps.Add(eingabeFenster.NeuerEintrag);
                AppListe.Items.Refresh();
            }
        }
    }
}