using System.Windows;
using System.Windows.Controls;
using TimeZoner.Models;
using TimeZoner.Services;

namespace TimeZoner;

public partial class AddCityWindow : Window
{
    public CityConfig? SelectedCity { get; private set; }

    public AddCityWindow()
    {
        InitializeComponent();
        CityList.ItemsSource = CityDatabase.AllCities;
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        CityList.ItemsSource = CityDatabase.Search(SearchBox.Text);
    }

    private void CityList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (CityList.SelectedItem is CityConfig city)
        {
            SelectedCity = city;
            DialogResult = true;
            Close();
        }
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        if (CityList.SelectedItem is CityConfig city)
        {
            SelectedCity = city;
            DialogResult = true;
            Close();
        }
        else
        {
            System.Windows.MessageBox.Show("Please select a city first.");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
