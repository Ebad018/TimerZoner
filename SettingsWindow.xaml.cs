using System.Windows;
using TimeZoner.Services;

namespace TimeZoner;

public partial class SettingsWindow : Window
{
    private AppSettings currentSettings;

    public SettingsWindow()
    {
        InitializeComponent();
        currentSettings = SettingsManager.LoadSettings();
        DataContext = currentSettings;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        SettingsManager.SaveSettings(currentSettings);
        Interop.StartupManager.SetAutoStart(currentSettings.AutoStart);
        MessageBox.Show("Settings saved! Please restart the app to apply changes.", "TimeZoner", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AddCity_Click(object sender, RoutedEventArgs e)
    {
        if (currentSettings.Cities.Count >= 5)
        {
            MessageBox.Show("Maximum of 5 cities allowed.");
            return;
        }
        
        currentSettings.Cities.Add(new CityConfig { Name = "New City (UTC)", TimeZoneId = "UTC", Latitude = 0, Longitude = 0 });
    }

    private void RemoveCity_Click(object sender, RoutedEventArgs e)
    {
        if (currentSettings.Cities.Count > 0)
        {
            currentSettings.Cities.RemoveAt(currentSettings.Cities.Count - 1);
        }
    }
}
