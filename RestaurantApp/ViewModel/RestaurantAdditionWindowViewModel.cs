using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
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
        public RestaurantAdditionWindowViewModel(IAdressServices adressServices)
        {
            _adressServices = adressServices;

            FinishActionCommand = new RelayCommand(FinishAction);
            CloseWindowCommand = new RelayCommand(CloseWindow);

            WeakReferenceMessenger.Default.Register<SendRestaurantToEditMessage>(this, (r, m) =>
            {
                _editWasCalled = true;
                AssignRestaurantValuesForEdit(m.Value);
            });
        }
        private readonly IAdressServices _adressServices;

        public IRelayCommand FinishActionCommand { get; }
        public IRelayCommand CloseWindowCommand { get; }

        private bool _editWasCalled = false;

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

        public void AssignRestaurantValuesForEdit(Restaurant restaurant)
        {
            RestaurantName = restaurant.Name;
            RestaurantRating = restaurant.Rating.ToString();
            OpeningHour = restaurant.OpeningHour;
            ClosingHour = restaurant.ClosingHour;
            RestaurantAdressCountry = restaurant.Adress!.Country;
            RestaurantAdressCity = restaurant.Adress!.City;
            RestaurantAdressStreet = restaurant.Adress!.Street;
            RestaurantAdressHouseNumber = restaurant.Adress!.HouseNumber;
            RestaurantAdressPostalCode = restaurant.Adress!.PostalCode;
        }

        public void FinishAction()
        {
            // Validation
            decimal restaurantRating = Convert.ToDecimal(RestaurantRating!);
            Adress adress = new(RestaurantAdressCountry!, RestaurantAdressCity!, RestaurantAdressStreet!, RestaurantAdressHouseNumber!, RestaurantAdressPostalCode!);
            int adressId = _adressServices.AddAdress(adress);
            Restaurant restaurant = new(RestaurantName!, restaurantRating, OpeningHour!, ClosingHour!, adressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantAddValueMessage(restaurant));
            CloseWindow();
        }

        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new RestaurantAdditionCloseWindowMessage());
        }
    }
}
