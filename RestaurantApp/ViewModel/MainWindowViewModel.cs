using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Model;
using RestaurantApp.Services;
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

            GetAllRestaurants();
            CreateRestaurantsCollection();
        }
        private readonly IRestaurantsService _restaurantsService;

        [ObservableProperty]
        private ObservableCollection<Restaurant>? _restaurantsList;

        [ObservableProperty]
        private ICollectionView? _restaurantsCollection;

        [ObservableProperty]
        private string? _searchRestaurantValue;

        public void GetAllRestaurants()
        {
            RestaurantsList = new ObservableCollection<Restaurant>(_restaurantsService.GetAll());
        }

        public void CreateRestaurantsCollection()
        {
            RestaurantsCollection = CollectionViewSource.GetDefaultView(RestaurantsList);
            RestaurantsCollection.SortDescriptions.Add(new SortDescription(nameof(Restaurant.Name), ListSortDirection.Ascending));
            RestaurantsCollection.Filter = FilterRestaurants;
        }

        private bool FilterRestaurants(object obj)
        {
            if (obj is not Restaurant restaurant)
            {
                return false;
            }
            return restaurant.Name.Contains(SearchRestaurantValue ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }

        public void RefreshRestaurantsCollection()
        {
            RestaurantsCollection?.Refresh();
        }

        partial void OnSearchRestaurantValueChanged(string? value)
        {
            RefreshRestaurantsCollection();
        }
    }
}
