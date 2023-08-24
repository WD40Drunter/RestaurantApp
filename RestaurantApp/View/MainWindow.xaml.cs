using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.ViewModel;
using System.Windows;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new PasswordMessage(PasswordInput.Password));
            PasswordInput.Password = null;
        }
    }
}
