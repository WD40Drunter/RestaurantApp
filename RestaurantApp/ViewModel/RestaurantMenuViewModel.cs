using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    public partial class RestaurantMenuViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        public RestaurantMenuViewModel(IDishService dishService, IStatusServices statusServices, ILoggedInUserServices loggedInUserServices)
        {
            _dishService = dishService;
            _statusServices = statusServices;
            _loggedInUserServices = loggedInUserServices;
            CollectionCreator = new();

            AddDishCommand = new RelayCommand(AddDish);
        }
        private readonly IDishService _dishService;
        private readonly IStatusServices _statusServices;
        private readonly ILoggedInUserServices _loggedInUserServices;
        CollectionCreator CollectionCreator { get; set; }

        public IRelayCommand AddDishCommand { get; }

        [ObservableProperty]
        private ObservableCollection<Dish>? _dishesList;

        [ObservableProperty]
        private ICollectionView? _dishesCollection;

        [ObservableProperty]
        private string? _searchDishValue;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _dishStatus;

        [ObservableProperty]
        private ObservableCollection<Status>? _statusList;

        [ObservableProperty]
        private ICollectionView? _statusCollection;

        [ObservableProperty]
        private bool _isAdmin = false;

        [ObservableProperty]
        private int? _restaurantId = null;

        public void GetStatusListAndCollection()
        {
            StatusList = new(_statusServices.GetAll());
            StatusCollection = CollectionCreator.GetCollection(StatusList);
        }

        public void GetDishesListAndCollection()
        {
            if (_loggedInUserServices.GetUserAccess() == "Admin")
            {
                DishesList = new(_dishService.GetSelected(RestaurantId));
                foreach (Dish dish in DishesList)
                {
                    dish.PropertyChanged += new PropertyChangedEventHandler(Dish_PropertyChanged);
                }
                DishesList.CollectionChanged += new NotifyCollectionChangedEventHandler(DishesList_PropertyChanged);
                DishesCollection = CollectionCreator.GetCollection(DishesList);
                IsAdmin = true;
            }
            else
            {
                DishesList = new(_dishService.GetSelectedForStandard(RestaurantId));
                DishesCollection = CollectionCreator.GetCollection(DishesList);
                IsAdmin = false;
            }
        }

        public void RefreshDishesCollection()
        {
            DishesCollection?.Refresh();
        }

        public void ResetInputs()
        {
            Name = string.Empty;
        }

        public void AddDish()
        {
            if (!UserValidator.IsAdmin(_loggedInUserServices.GetUser()))
            {
                return;
            }
            if (!Validator.IsStringNotNull(Name))
            {
                return;
            }
            Dish dish = new(Name!, 1, RestaurantId ?? 0);

            DishesList?.Add(_dishService.AddDish(dish));

            RefreshDishesCollection();
            ResetInputs();
        }

        partial void OnSearchDishValueChanged(string? value)
        {
            CollectionCreator.SearchDishValue = value;
            RefreshDishesCollection();
        }

        public void DishesList_PropertyChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (Dish dish in e.OldItems)
                {
                    dish.PropertyChanged -= new PropertyChangedEventHandler(Dish_PropertyChanged);
                }
            }
            if (e.NewItems is not null)
            {
                foreach (Dish dish in e.NewItems)
                {
                    dish.PropertyChanged += new PropertyChangedEventHandler(Dish_PropertyChanged);
                }
            }
        }

        public void Dish_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _dishService.ChangeStatus();
        }

        partial void OnRestaurantIdChanged(int? value)
        {
            GetDishesListAndCollection();
        }
    }
}
