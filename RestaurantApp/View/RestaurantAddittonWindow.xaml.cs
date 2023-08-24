using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.ViewModel;
using System.Windows;

namespace RestaurantApp.View
{
    /// <summary>
    /// Interaction logic for RestaurantAddittonWindow.xaml
    /// </summary>
    public partial class RestaurantAddittonWindow : Window
    {
        public RestaurantAddittonWindow()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RestaurantAdditionWindowViewModel>();

            WeakReferenceMessenger.Default.Register<RestaurantAdditionCloseWindowMessage>(this, (r, m) =>
            {
                Close();
            });
        }
    }
}
