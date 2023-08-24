using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.ViewModel;
using System.Windows;

namespace RestaurantApp.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<RegisterWindowViewModel>();

            WeakReferenceMessenger.Default.Register<RegisterWindowCloseMessage>(this, (r, m) =>
            {
                Close();
            });
        }
    }
}
