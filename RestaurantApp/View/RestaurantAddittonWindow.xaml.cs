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
