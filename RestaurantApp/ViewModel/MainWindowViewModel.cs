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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantApp.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;

            CollectionCreator = new ();

            RestaurantsList = new ObservableCollection<Restaurant>(_restaurantsService.GetAll());
            RestaurantsCollection = CollectionCreator.GetCollection(RestaurantsList);

            OpenMenuWindowCommand = new RelayCommand<object?>(OpenMenuWindow);
        }
        private readonly IRestaurantsService _restaurantsService;

        public IRelayCommand<object?> OpenMenuWindowCommand { get; }

        [ObservableProperty]
        private ObservableCollection<Restaurant>? _restaurantsList;

        [ObservableProperty]
        private ICollectionView? _restaurantsCollection;

        [ObservableProperty]
        private string? _searchRestaurantValue;

        public CollectionCreator CollectionCreator { get; set; }

        public void RefreshRestaurantsCollection()
        {
            RestaurantsCollection?.Refresh();
        }

        public static void OpenMenuWindow(object? obj)
        {
            Restaurant? restaurant = obj as Restaurant;
            RestaurantMenu restaurantMenu = new();
            restaurantMenu.Show();
            WeakReferenceMessenger.Default.Send(new RestaurantIdMessage(restaurant?.RestaurantId));
        }

        partial void OnSearchRestaurantValueChanged(string? value)
        {
            CollectionCreator.SearchRestaurantValue = value;
            RefreshRestaurantsCollection();
        }

    }
}
