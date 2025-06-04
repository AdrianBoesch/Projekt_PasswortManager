using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Projekt_PasswortManager
{
    public partial class MainWindow : Window
    {
        private readonly List<AppEintrag> apps = new List<AppEintrag>();
        private readonly string _masterPasswordKey;

        public MainWindow(string masterPasswordKey)
        {
            InitializeComponent();
            _masterPasswordKey = masterPasswordKey;
            this.AppListe.ItemsSource = this.apps;
        }

        public MainWindow() : this(string.Empty) { }

        private void AppListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppEintrag selected = this.AppListe.SelectedItem as AppEintrag;
            if (selected != null)
            {
                ViewWindow viewWindow = new ViewWindow(selected.Passwort);
                viewWindow.Show();
            }
        }

        private void HinzufügenButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            bool? result = editWindow.ShowDialog();
            if (result == true)
            {
                AppEintrag neuerEintrag = editWindow.NeuerEintrag;
                this.apps.Add(neuerEintrag);
                this.AppListe.Items.Refresh();
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
                MessageBox.Show("Bitte wähle zuerst einen Eintrag aus der Liste aus.",
                                "Hinweis",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        
    }
}
