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
            AppEintrag selected = this.AppListe.SelectedItem as AppEintrag;
            if (selected != null)
            {
                MessageBoxResult answer = MessageBox.Show(
                    $"Möchtest du \"{selected.AppName}\" wirklich löschen?",
                    "Löschen bestätigen",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (answer == MessageBoxResult.Yes)
                {
                    this.apps.Remove(selected);
                    this.AppListe.Items.Refresh();
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

        // Methode PrüfePasswort ist hier komplett entfernt
    }
}
