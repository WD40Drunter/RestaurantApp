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
    public partial class MainWindowViewModel : SolutionViewModel
    {
        public MainWindowViewModel(IRestaurantsService restaurantsService, IUserService userService)
        {
            _restaurantsService = restaurantsService;
            _userService = userService;

            CollectionCreator = new();

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
            if (!UserValidator.IsLoggedIn(LoggedInUser))
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
            if (!UserValidator.IsAdmin(LoggedInUser))
            {
                return;
            }
            RestaurantAddittonWindow restaurantAddittonWindow = new();
            restaurantAddittonWindow.Show();
        }

        public void OpenAddtionToEdit()
        {
            if (SelectedRestaurant is null)
            {
                return;
            }
            if (!UserValidator.IsAdmin(LoggedInUser))
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
            _restaurantsService.EditRestaurant(SelectedRestaurant!, restaurant);

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
