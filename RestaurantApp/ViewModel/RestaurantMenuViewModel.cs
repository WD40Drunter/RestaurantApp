using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace RestaurantApp.ViewModel
{
    public partial class RestaurantMenuViewModel : ObservableObject
    {
        public RestaurantMenuViewModel(IDishService dishService, IStatusServices statusServices)
        {
            _dishService = dishService;
            _statusServices = statusServices;
            CollectionCreator = new();

            AddDishCommand = new RelayCommand(AddDish);

            StatusList = new(_statusServices.GetAll());
            StatusCollection = CollectionCreator.GetCollection(StatusList);

            WeakReferenceMessenger.Default.Register<RestaurantIdMessage>(this, (r, m) =>
            {
                RestaurantId = (int)(m.Value ?? 0);
            });

            WeakReferenceMessenger.Default.Register<SendUserMessage>(this, (r, m) =>
            {
                LoggedInUser = m.Value;
                if (LoggedInUser.Access == "Admin")
                {
                    DishesList = new(_dishService.GetSelected(RestaurantId));
                    DishesCollection = CollectionCreator.GetCollection(DishesList);
                    StatusColumnWidth = "100";
                }
                else
                {
                    DishesList = new(_dishService.GetSelectedForStandard(RestaurantId));
                    DishesCollection = CollectionCreator.GetCollection(DishesList);
                }
            });

            WeakReferenceMessenger.Default.Register<ValuesOfStatusToChangeItInDatabaseMessage>(this, (r, m) =>
            {
                _dishService.ChangeStatus(int.Parse(m.Value[0]), int.Parse(m.Value[1]));
            });
        }
        private readonly IDishService _dishService;
        private readonly IStatusServices _statusServices;
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
        private User? _loggedInUser;

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
            if (LoggedInUser!.Access == "Standard")
            {
                MessageBox.Show("Brak uprawnień");
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
            if (LoggedInUser!.Access == "Standard")
            {
                StatusColumnWidth = "0";
                return;
            }
            StatusColumnWidth = value;
        }
    }
}
