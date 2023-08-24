using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    public partial class RestaurantMenuViewModel : ObservableObject
    {
        public RestaurantMenuViewModel(IDishService dishService, IStatusServices statusServices)
        {
            _dishService = dishService;
            _statusServices = statusServices;
            CollectionCreator = new ();

            AddDishCommand = new RelayCommand(AddDish);

            StatusList = new(_statusServices.GetAll());
            StatusCollection = CollectionCreator.GetCollection(StatusList);

            WeakReferenceMessenger.Default.Register<RestaurantIdMessage>(this, (r, m) =>
            {
                RestaurantId = (int)(m.Value ?? 0);
                DishesList = new(_dishService.GetSelected(m.Value));
                DishesCollection = CollectionCreator.GetCollection(DishesList);
            });

            WeakReferenceMessenger.Default.Register<SendUserMessage>(this, (r, m) =>
            {
                LoggedInUser = m.Value;
                if(LoggedInUser.Access == "Admin")
                {
                    StatusColumnWidth = "100";
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
        private string? _dishName;

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
            DishName = string.Empty;
        }

        public void AddDish()
        {
            if(!Validator.IsStringNotNull(DishName))
            {
                return;
            }
            Dish dish = new(DishName!, 1, RestaurantId);

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
            if(LoggedInUser!.Access == "Standard")
            {
                StatusColumnWidth = "0";
                return;
            }
            StatusColumnWidth = value;
        }
    }
}
