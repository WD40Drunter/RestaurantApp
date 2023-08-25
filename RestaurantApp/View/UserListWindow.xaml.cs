using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.ViewModel;
using System.Windows;
using System.Windows.Controls;

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

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.Source is not ComboBox comboBox)
            {
                return;
            }
            string[] valuesForDatabase = new string[2];
            valuesForDatabase[0] = comboBox.Tag.ToString()!;
            valuesForDatabase[1] = ReturnAccessValueBasedOnInt(comboBox.SelectedIndex.ToString());
            WeakReferenceMessenger.Default.Send(new ValuesOfAccessToChangeItInDatabaseMessage(valuesForDatabase));
        }

        private static string ReturnAccessValueBasedOnInt(string value)
        {
            int zad = int.Parse(value);
            if (zad == 1)
            {
                return "Admin";
            }
            return "Standard";
        }
    }
}
