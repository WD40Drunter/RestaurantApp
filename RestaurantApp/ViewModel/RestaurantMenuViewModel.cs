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
        public RestaurantMenuViewModel(IDishService dishService)
        {
            _dishService = dishService;
            CollectionCreator = new ();

            AddDishCommand = new RelayCommand(AddDish);

            

            WeakReferenceMessenger.Default.Register<RestaurantIdMessage>(this, (r, m) =>
            {
                RestaurantId = (int)(m.Value ?? 0);
                DishesList = new(_dishService.GetSelected(m.Value));
                DishesCollection = CollectionCreator.GetCollection(DishesList);
            });


        }
        private readonly IDishService _dishService;
        CollectionCreator CollectionCreator { get; set; }

        public int RestaurantId { get; set; }

        public IRelayCommand AddDishCommand { get; }

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
            Dish dish = new(DishName!, 0, RestaurantId);

            DishesList?.Add(_dishService.AddDish(dish));

            RefreshDishesCollection();
            ResetInputs();
        }

        partial void OnSearchDishValueChanged(string? value)
        {
            CollectionCreator.SearchDishValue = value;
            RefreshDishesCollection();
        }
    }
}
