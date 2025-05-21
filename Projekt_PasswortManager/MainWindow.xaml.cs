using Projekt_PasswortManager;
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
                var fenster = new ViewWindow(ausgewählt.Passwort);
                fenster.Show();
            }
        }

        private void HinzufügenButton_Click(object sender, RoutedEventArgs e)
        {
            var eingabeFenster = new EditWindow();
            if (eingabeFenster.ShowDialog() == true)
            {
                apps.Add(eingabeFenster.NeuerEintrag);
                AppListe.Items.Refresh();
            }
        }

        private void LöschenButton_Click(object sender, RoutedEventArgs e)
        {
            var ausgewählt = AppListe.SelectedItem as AppEintrag;
            if (ausgewählt != null)
            {
                var result = MessageBox.Show($"Möchtest du \"{ausgewählt.AppName}\" wirklich löschen?",
                                             "Löschen bestätigen",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    apps.Remove(ausgewählt);
                    AppListe.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Bitte wähle zuerst einen Eintrag aus der Liste aus.",
                                "Hinweis",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        private void PrüfePasswort(AppEintrag ausgewählt)
        {
           
            var verif = new VerificationWindow(ausgewählt.Passwort);
            if (verif.ShowDialog() == true)
            {
               
                MessageBox.Show("Zugriff gewährt für " + ausgewählt.AppName);
            }
            else
            {
               
            }
        }
    }
}