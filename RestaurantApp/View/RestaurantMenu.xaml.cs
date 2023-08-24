using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
