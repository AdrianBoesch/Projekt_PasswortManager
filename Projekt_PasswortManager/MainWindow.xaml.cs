using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Projekt_PasswortManager
{
    public partial class MainWindow : Window
    {
        private List<AppEintrag> apps = new List<AppEintrag>();
        private readonly string _masterPasswordKey;

        public MainWindow(string masterPasswordKey)
        {
            InitializeComponent();
            _masterPasswordKey = masterPasswordKey;
            this.AppListe.ItemsSource = this.apps;

            // Optional: Automatisch laden beim Start
            this.apps.AddRange(AppEintragService.Laden());
            Log.Logger.Information("AppEinträge geladen: {Count}", this.apps.Count);
            this.AppListe.Items.Refresh();

            Log.Logger = new LoggerConfiguration().
                WriteTo.Console().
                WriteTo.File("stopwatch.log").
                CreateLogger();

            Log.Logger.Information("MainWindow started ...");
        }

        public MainWindow() : this(string.Empty) { }

        private void AppListe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppEintrag selected = this.AppListe.SelectedItem as AppEintrag;
            if (selected != null)
            {
                int index = this.apps.IndexOf(selected);
                Log.Logger.Information("Eintrag zur Anzeige geöffnet. Index: {Index}", index);
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
                Log.Logger.Information("Neuer Eintrag hinzugefügt. Neue Gesamtanzahl: {Count}", this.apps.Count);
                this.AppListe.Items.Refresh();
            }
        }

        private void LöschenButton_Click(object sender, RoutedEventArgs e)
        {
            var ausgewählt = AppListe.SelectedItem as AppEintrag;
            if (ausgewählt != null)
            {
                int index = this.apps.IndexOf(ausgewählt);
                this.apps.Remove(ausgewählt);
                Log.Logger.Information("Eintrag gelöscht. Index war: {Index}. Verbleibend: {Count}", index, this.apps.Count);
                this.AppListe.Items.Refresh();
            }
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            AppEintragService.Speichern(this.apps);
            Log.Logger.Information("Manuelles Speichern ausgelöst. Gespeicherte Einträge: {Count}", this.apps.Count);
            MessageBox.Show("Einträge wurden gespeichert.", "Gespeichert", MessageBoxButton.OK, MessageBoxImage.Information);
        }

       

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            AppEintragService.Speichern(this.apps); // Optional automatisches Speichern beim Schließen
            Log.Logger.Information("Anwendung geschlossen. Automatisches Speichern durchgeführt. Einträge: {Count}", this.apps.Count);
        }

    }
}