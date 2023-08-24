using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace RestaurantApp.View
{
    /// <summary>
    /// Interaction logic for RestaurantMenu.xaml
    /// </summary>
    public partial class RestaurantMenu : Window
    {
        public RestaurantMenu()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RestaurantMenuViewModel>();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? comboBox = e.Source as ComboBox;
            if (comboBox is null)
            {
                return;
            }
            string[] valuesForDatabase = new string[2];
            valuesForDatabase[0] = comboBox.Tag.ToString()!;
            valuesForDatabase[1] = comboBox.SelectedIndex.ToString();
            WeakReferenceMessenger.Default.Send(new ValuesOfStatusToChangeItInDatabaseMessage(valuesForDatabase));
        }
    }
}
