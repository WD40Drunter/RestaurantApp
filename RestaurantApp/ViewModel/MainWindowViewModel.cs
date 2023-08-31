using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using RestaurantApp.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace RestaurantApp.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel(IRestaurantsService restaurantsService, IUserService userService, 
            ILoggedInUserServices loggedInUserServices, IDishService dishService, IAdressServices adressServices)
        {
            _restaurantsService = restaurantsService;
            _userService = userService;
            _loggedInUserService = loggedInUserServices;
            _dishService = dishService;
            _adressService = adressServices;

            CollectionCreator = new();

            RestaurantsList = new ObservableCollection<Restaurant>(_restaurantsService.GetAll());
            RestaurantsCollection = CollectionCreator.GetCollection(RestaurantsList);

            OpenMenuWindowCommand = new RelayCommand<object?>(OpenMenuWindow);
            OpenRegisterWindowCommand = new RelayCommand(OpenRegisterWindow);
            OpenUserListWindowCommand = new RelayCommand(OpenUserListWindow);

            LogoutCommand = new RelayCommand(Logout);

            AddRestaurantCommand = new RelayCommand(OpenAdditionToAdd);
            EditRestaurantCommand = new RelayCommand(OpenAddtionToEdit);
            DeleteRestaurantCommand = new RelayCommand(DeleteRestaurant);

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
        private readonly ILoggedInUserServices _loggedInUserService;
        private readonly IAdressServices _adressService;
        private readonly IDishService _dishService;

        public IRelayCommand<object?> OpenMenuWindowCommand { get; }
        public IRelayCommand OpenRegisterWindowCommand { get; }
        public IRelayCommand OpenUserListWindowCommand { get; }

        public IRelayCommand LogoutCommand { get; }

        public IRelayCommand AddRestaurantCommand { get; }
        public IRelayCommand EditRestaurantCommand { get; }
        public IRelayCommand DeleteRestaurantCommand { get; }

        [ObservableProperty]
        private Restaurant? _selectedRestaurant;

        [ObservableProperty]
        private ObservableCollection<Restaurant>? _restaurantsList;

        [ObservableProperty]
        private ICollectionView? _restaurantsCollection;

        [ObservableProperty]
        private string? _searchRestaurantValue;

        [ObservableProperty]
        private string? _inputLogin;

        [ObservableProperty]
        private string? _inputPassword;

        [ObservableProperty]
        private bool _isLoggedIn = false;

        public CollectionCreator CollectionCreator { get; set; }

        public void RefreshRestaurantsCollection()
        {
            RestaurantsCollection?.Refresh();
        }
        
        public void OpenMenuWindow(object? obj)
        {
            if (!UserValidator.IsLoggedIn(_loggedInUserService.GetUser()))
            {
                return;
            }
            Restaurant? restaurant = obj as Restaurant;
            RestaurantMenu restaurantMenu = new(restaurant!.RestaurantId);
            restaurantMenu.Show();
        }

        public static void OpenRegisterWindow()
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
        }

        public void OpenAdditionToAdd()
        {
            if (!UserValidator.IsAdmin(_loggedInUserService.GetUser()))
            {
                return;
            }
            RestaurantAddittonWindow restaurantAddittonWindow = new(null);
            restaurantAddittonWindow.Show();
        }

        public void OpenUserListWindow()
        {
            if (!UserValidator.IsAdmin(_loggedInUserService.GetUser()))
            {
                return;
            }
            UserListWindow userListWindow = new();
            userListWindow.Show();
        }

        public void OpenAddtionToEdit()
        {
            if (!CanDeleteOrEdit())
            {
                return;
            }
            RestaurantAddittonWindow restaurantAddittonWindow = new(SelectedRestaurant!);
            restaurantAddittonWindow.Show();
        }

        public void Login()
        {
            User? user = _userService.GetUser(InputLogin!);
            if (user is null)
            {
                MessageBox.Show("Błędny login");
                return;
            }
            if (!SecretHasher.Verify(InputPassword!, user.Password))
            {
                MessageBox.Show("Błędne hasło");
                return;
            }
            _loggedInUserService.Login(user);
            IsLoggedIn = true;
        }

        public void Logout()
        {
            InputLogin = null;
            _loggedInUserService.Logout();
            IsLoggedIn = false;
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            _restaurantsService.EditRestaurant(SelectedRestaurant!, restaurant);
            RefreshRestaurantsCollection();
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            RestaurantsList!.Add(_restaurantsService.AddRestaurant(restaurant));
            RefreshRestaurantsCollection();
        }

        public void DeleteRestaurant()
        {
            if (!CanDeleteOrEdit())
            {
                return;
            }
            _dishService.DeleteDishes(SelectedRestaurant!.RestaurantId);
            _restaurantsService.DeleteRestaurant(SelectedRestaurant);
            _adressService.DeleteAdress(SelectedRestaurant.AdressId);
            RestaurantsList!.Remove(SelectedRestaurant);

            RefreshRestaurantsCollection();
        }

        public bool CanDeleteOrEdit()
        {
            if (SelectedRestaurant is null)
            {
                return false;
            }
            if (!UserValidator.IsAdmin(_loggedInUserService.GetUser()))
            {
                return false;
            }
            return true;
        }

        partial void OnSearchRestaurantValueChanged(string? value)
        {
            CollectionCreator.SearchRestaurantValue = value;
            RefreshRestaurantsCollection();
        }

    }
}
