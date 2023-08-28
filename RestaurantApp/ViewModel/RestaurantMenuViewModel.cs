using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RestaurantApp.ViewModel
{
    public partial class RestaurantMenuViewModel : ObservableRecipient
    {
        public RestaurantMenuViewModel(IDishService dishService, IStatusServices statusServices, ILoggedInUserServices loggedInUserServices)
        {
            _dishService = dishService;
            _statusServices = statusServices;
            _loggedInUserServices = loggedInUserServices;
            CollectionCreator = new();

            AddDishCommand = new RelayCommand(AddDish);

            StatusList = new(_statusServices.GetAll());
            StatusCollection = CollectionCreator.GetCollection(StatusList);

            WeakReferenceMessenger.Default.Register<RestaurantIdMessage>(this, (r, m) =>
            {
                RestaurantId = m.Value ?? 0;

                if (_loggedInUserServices.GetUserAccess() == "Admin")
                {
                    DishesList = new(_dishService.GetSelected(RestaurantId));
                    DishesCollection = CollectionCreator.GetCollection(DishesList);
                    StatusColumnWidth = "100";
                    IsAdmin = true;
                }
                else
                {
                    DishesList = new(_dishService.GetSelectedForStandard(RestaurantId));
                    DishesCollection = CollectionCreator.GetCollection(DishesList);
                    IsAdmin = false;
                }
            });

            WeakReferenceMessenger.Default.Register<ValuesOfStatusToChangeItInDatabaseMessage>(this, (r, m) =>
            {
                int dishId = int.Parse(m.Value[0]);
                int statusId = int.Parse(m.Value[1]);
                _dishService.ChangeStatus(dishId, statusId);
            });
        }
        private readonly IDishService _dishService;
        private readonly IStatusServices _statusServices;
        private readonly ILoggedInUserServices _loggedInUserServices;
        CollectionCreator CollectionCreator { get; set; }

        public int RestaurantId { get; set; }

        public IRelayCommand AddDishCommand { get; }

        [ObservableProperty]
        private string _statusColumnWidth = "0";

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
            Dish dish = new(Name!, 1, RestaurantId);

            DishesList?.Add(_dishService.AddDish(dish));

            RefreshDishesCollection();
            ResetInputs();
        }

        partial void OnSearchDishValueChanged(string? value)
        {
            CollectionCreator.SearchDishValue = value;
            RefreshDishesCollection();
        }

        partial void OnStatusColumnWidthChanged(string value)
        {
            if (!UserValidator.IsAdmin(_loggedInUserServices.GetUser()))
            {
                StatusColumnWidth = "0";
                return;
            }
            StatusColumnWidth = value;
        }
    }
}
