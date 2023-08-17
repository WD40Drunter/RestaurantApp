using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using RestaurantApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RestaurantApp.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel(IRestaurantsService restaurantsService, IUserService userService)
        {
            _restaurantsService = restaurantsService;
            _userService = userService;

            CollectionCreator = new ();

            RestaurantsList = new ObservableCollection<Restaurant>(_restaurantsService.GetAll());
            RestaurantsCollection = CollectionCreator.GetCollection(RestaurantsList);

            OpenMenuWindowCommand = new RelayCommand<object?>(OpenMenuWindow);
            OpenRegisterWindowCommand = new RelayCommand(OpenRegisterWindow);
            LogoutCommand = new RelayCommand(Logout);

            WeakReferenceMessenger.Default.Register<PasswordMessage>(this, (r, m) =>
            {
                InputPassword = m.Value;
                Login();
            });
        }
        private readonly IRestaurantsService _restaurantsService;
        private readonly IUserService _userService;

        public IRelayCommand<object?> OpenMenuWindowCommand { get; }
        public IRelayCommand OpenRegisterWindowCommand { get; }
        public IRelayCommand LogoutCommand { get; }

        [ObservableProperty]
        private ObservableCollection<Restaurant>? _restaurantsList;

        [ObservableProperty]
        private ICollectionView? _restaurantsCollection;

        [ObservableProperty]
        private string? _searchRestaurantValue;

        [ObservableProperty]
        private User? _loggedInUser;

        [ObservableProperty]
        private string? _inputLogin;

        [ObservableProperty]
        private string? _inputPassword;

        public CollectionCreator CollectionCreator { get; set; }

        public void RefreshRestaurantsCollection()
        {
            RestaurantsCollection?.Refresh();
        }

        public void OpenMenuWindow(object? obj)
        {
            if (LoggedInUser is null)
            {
                MessageBox.Show("Zaloguj się");
                return;
            }
            Restaurant? restaurant = obj as Restaurant;
            RestaurantMenu restaurantMenu = new();
            restaurantMenu.Show();
            WeakReferenceMessenger.Default.Send(new RestaurantIdMessage(restaurant?.RestaurantId));
        }

        public static void OpenRegisterWindow()
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
        }

        public void Login()
        {
            User? user = _userService.GetUser(InputLogin!);
            if (user == null)
            {
                MessageBox.Show("Błędny login");
                return;
            }
            if (!SecretHasher.Verify(InputPassword!, user.Password))
            {
                MessageBox.Show("Błędne hasło");
                return;
            }
            LoggedInUser = user;
        }

        public void Logout()
        {
            InputLogin = null;
            LoggedInUser = null;
        }

        partial void OnSearchRestaurantValueChanged(string? value)
        {
            CollectionCreator.SearchRestaurantValue = value;
            RefreshRestaurantsCollection();
        }

    }
}
