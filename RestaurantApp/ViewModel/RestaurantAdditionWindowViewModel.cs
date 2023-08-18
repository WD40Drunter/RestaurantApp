using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    internal partial class RestaurantAdditionWindowViewModel : ObservableRecipient
    {
        public RestaurantAdditionWindowViewModel( IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;

            FinishActionCommand = new RelayCommand(FinishAction);
            CloseWindowCommand = new RelayCommand(CloseWindow);

        }
        private readonly IRestaurantsService _restaurantsService;

        public IRelayCommand FinishActionCommand { get; }
        public IRelayCommand CloseWindowCommand { get; }

        [ObservableProperty]
        private string? _restaurantName;

        [ObservableProperty]
        private string? _restaurantRating;

        [ObservableProperty]
        private string? _openingHour;

        [ObservableProperty]
        private string? _closingHour;

        [ObservableProperty]
        private string? _restaurantAdressCountry;

        [ObservableProperty]
        private string? _restaurantAdressCity;

        [ObservableProperty]
        private string? _restaurantAdressStreet;

        [ObservableProperty]
        private string? _restaurantAdressHouseNumber;

        [ObservableProperty]
        private string? _restaurantAdressPostalCode;

        public void FinishAction()
        {

        }

        public void CloseWindow()
        {

        }
    }
}
