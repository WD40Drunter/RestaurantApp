using CommunityToolkit.Mvvm.ComponentModel;
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

            WeakReferenceMessenger.Default.Register<RestaurantIdMessage>(this, (r, m) =>
            {
                DishesList = new(_dishService.GetSelected(m.Value));
                DishesCollection = CollectionCreator.GetCollection(DishesList);
            });

        }
        private readonly IDishService _dishService;
        CollectionCreator CollectionCreator { get; set; }

        [ObservableProperty]
        private ObservableCollection<Dish>? _dishesList;

        [ObservableProperty]
        private ICollectionView? _dishesCollection;

        [ObservableProperty]
        private string? _searchDishValue;
    }
}
