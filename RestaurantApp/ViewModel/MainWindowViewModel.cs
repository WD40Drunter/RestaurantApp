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

            AddRestaurantCommand = new RelayCommand(OpenAdditionToAdd);
            EditRestaurantCommand = new RelayCommand(OpenAddtionToEdit);

            WeakReferenceMessenger.Default.Register<PasswordMessage>(this, (r, m) =>
            {
                InputPassword = m.Value;
                Login();
            });

            WeakReferenceMessenger.Default.Register<SendRestaurantAddValueMessage>(this, (r, m) =>
            {
                AddRestaurant(m.Value);
            });

            WeakReferenceMessenger.Default.Register<SendRestaurantEditValueMessage>(this, (r, m) =>
            {
                EditRestaurant(m.Value);
            });
        }
        private readonly IRestaurantsService _restaurantsService;
        private readonly IUserService _userService;

        public IRelayCommand<object?> OpenMenuWindowCommand { get; }
        public IRelayCommand OpenRegisterWindowCommand { get; }
        public IRelayCommand LogoutCommand { get; }

        public IRelayCommand AddRestaurantCommand { get; }
        public IRelayCommand EditRestaurantCommand { get; }

        [ObservableProperty]
        private Restaurant? _selectedRestaurant;

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

        public bool IsLoggedIn()
        {
            if (LoggedInUser is null)
            {
                MessageBox.Show("Zaloguj się");
                return false;
            }
            return true;
        }

        public bool IsAdmin()
        {
            if (!IsLoggedIn())
            {
                return false;
            }
            if (LoggedInUser!.Access != "Admin")
            {
                MessageBox.Show("Brak uprawnień");
                return false;
            }
            return true;
        }

        public void OpenMenuWindow(object? obj)
        {
            if (!IsLoggedIn())
            {
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

        public void OpenAdditionToAdd()
        {
            if (!IsAdmin())
            {
                return;
            }
            RestaurantAddittonWindow restaurantAddittonWindow = new();
            restaurantAddittonWindow.Show();
        }

        public void OpenAddtionToEdit()
        {
            if(SelectedRestaurant is null)
            {
                return;
            }
            if (!IsAdmin())
            {
                return;
            }
            OpenAdditionToAdd();
            WeakReferenceMessenger.Default.Send(new SendRestaurantToEditMessage(SelectedRestaurant));
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

        public void EditRestaurant(Restaurant restaurant)
        {
            SelectedRestaurant = _restaurantsService.EditRestaurant(SelectedRestaurant!, restaurant);

            RefreshRestaurantsCollection();
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            RestaurantsList!.Add(_restaurantsService.AddRestaurant(restaurant));
            RefreshRestaurantsCollection();
        }

        partial void OnSearchRestaurantValueChanged(string? value)
        {
            CollectionCreator.SearchRestaurantValue = value;
            RefreshRestaurantsCollection();
        }

    }
}
