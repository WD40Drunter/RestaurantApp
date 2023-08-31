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
        public RestaurantMenu(int restaurantId)
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<RestaurantMenuViewModel>();
            ((RestaurantMenuViewModel)DataContext).RestaurantId = restaurantId;
        }

    }
}
