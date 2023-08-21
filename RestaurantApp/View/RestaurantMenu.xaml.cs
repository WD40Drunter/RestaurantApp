using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
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

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
