using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.ViewModel;
using System.Windows;

namespace RestaurantApp.View
{
    /// <summary>
    /// Interaction logic for RestaurantAddittonWindow.xaml
    /// </summary>
    public partial class RestaurantAddittonWindow : Window
    {
        public RestaurantAddittonWindow(Restaurant? restaurant)
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RestaurantAdditionWindowViewModel>();
            ((RestaurantAdditionWindowViewModel)DataContext).OldEditRestaurant = restaurant;

            WeakReferenceMessenger.Default.Register<RestaurantAdditionCloseWindowMessage>(this, (r, m) =>
            {
                Close();
            });
        }
    }
}
