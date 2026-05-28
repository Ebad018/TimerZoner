using System.Windows;
using System.Windows.Input;
using TimeZoner.Interop;

namespace TimeZoner;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        DesktopInterop.PinToDesktop(this);
        
        // Setup AutoStart
        var settings = Services.SettingsManager.LoadSettings();
        StartupManager.SetAutoStart(settings.AutoStart);
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Allow dragging if user clicks on the background
        DragMove();
    }

    private void MenuItem_Settings_Click(object sender, RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow();
        settingsWindow.ShowDialog();
    }

    private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }
}