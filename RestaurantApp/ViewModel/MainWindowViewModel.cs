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

        public void GetAllRestaurants()
        {
            RestaurantsList = new ObservableCollection<Restaurant>(_restaurantsService.GetAll());
        }

        public void CreateRestaurantsCollection()
        {
            RestaurantsCollection = CollectionViewSource.GetDefaultView(RestaurantsList);
        }
    }
}
