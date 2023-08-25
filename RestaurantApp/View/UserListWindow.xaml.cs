using CommunityToolkit.Mvvm.DependencyInjection;
using RestaurantApp.ViewModel;
using System.Windows;

namespace RestaurantApp.View
{
    /// <summary>
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        public UserListWindow()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<UserListWindowViewModel>();
        }
    }
}
