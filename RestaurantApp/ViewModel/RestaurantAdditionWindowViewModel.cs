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
using System.Windows;

namespace RestaurantApp.ViewModel
{
    internal partial class RestaurantAdditionWindowViewModel : ObservableRecipient
    {
        public RestaurantAdditionWindowViewModel(IAdressServices adressServices)
        {
            _adressServices = adressServices;

            _oldEditRestaurant = null;

            FinishActionCommand = new RelayCommand(FinishAction);
            CloseWindowCommand = new RelayCommand(CloseWindow);

            WeakReferenceMessenger.Default.Register<SendRestaurantToEditMessage>(this, (r, m) =>
            {
                AssignRestaurantValuesForEdit(m.Value);
            });
        }
        private readonly IAdressServices _adressServices;

        public IRelayCommand FinishActionCommand { get; }
        public IRelayCommand CloseWindowCommand { get; }

        private Restaurant? _oldEditRestaurant;

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
            _oldEditRestaurant = restaurant;
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

        public bool IsEverythingWellInputed()
        {
            if (!Validator.IsStringNotNull(RestaurantName)
                || !Validator.IsStringNotNull(RestaurantRating)
                || !Validator.IsStringNotNull(OpeningHour)
                || !Validator.IsStringNotNull(ClosingHour)
                || !Validator.IsStringNotNull(RestaurantAdressCountry)
                || !Validator.IsStringNotNull(RestaurantAdressCity)
                || !Validator.IsStringNotNull(RestaurantAdressStreet)
                || !Validator.IsStringNotNull(RestaurantAdressHouseNumber)
                || !Validator.IsStringNotNull(RestaurantAdressPostalCode))
            {
                return false;
            }
            if (!Validator.IsHourValid(OpeningHour!) || !Validator.IsHourValid(ClosingHour!))
            {
                return false;
            }
            if (!Validator.IsHouseNumberValid(RestaurantAdressHouseNumber!))
            {
                return false;
            }
            return true;
        }

        public void FinishAction()
        {
            RestaurantRating = RestaurantRating!.Replace('_', '0');
            OpeningHour = OpeningHour!.Replace('_', '0');
            ClosingHour = ClosingHour!.Replace('_', '0');
            RestaurantAdressPostalCode = RestaurantAdressPostalCode!.Replace('_', '0');

            if (!IsEverythingWellInputed())
            {
                return;
            }
            Adress adress = new(RestaurantAdressCountry!, RestaurantAdressCity!, RestaurantAdressStreet!, RestaurantAdressHouseNumber!, RestaurantAdressPostalCode!);
            if (_oldEditRestaurant is null)
            {
                AddRestaurant(adress);
            }
            else
            {
                EditRestaurant(adress);
            }
            CloseWindow();
        }

        public void AddRestaurant(Adress adress)
        {
            int adressId = _adressServices.AddAdress(adress);
            Restaurant restaurant = new(RestaurantName!, Convert.ToDecimal(RestaurantRating!), OpeningHour!, ClosingHour!, adressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantAddValueMessage(restaurant));
        }

        public void EditRestaurant(Adress adress)
        {
            _oldEditRestaurant!.Adress = _adressServices.EditAdress(_oldEditRestaurant!.Adress!, adress);
            Restaurant restaurant = new(RestaurantName!, Convert.ToDecimal(RestaurantRating!), OpeningHour!, ClosingHour!, _oldEditRestaurant.Adress.AdressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantEditValueMessage(restaurant));
        }

        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new RestaurantAdditionCloseWindowMessage());
        }
    }
}
