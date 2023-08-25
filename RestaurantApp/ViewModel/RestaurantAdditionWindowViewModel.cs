using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System;

namespace RestaurantApp.ViewModel
{
    internal partial class RestaurantAdditionWindowViewModel : SolutionViewModel
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
        private string? _name;

        [ObservableProperty]
        private string? _rating;

        [ObservableProperty]
        private string? _openingHour;

        [ObservableProperty]
        private string? _closingHour;

        [ObservableProperty]
        private string? _adressCountry;

        [ObservableProperty]
        private string? _adressCity;

        [ObservableProperty]
        private string? _adressStreet;

        [ObservableProperty]
        private string? _adressHouseNumber;

        [ObservableProperty]
        private string? _adressPostalCode;

        public void AssignRestaurantValuesForEdit(Restaurant restaurant)
        {
            _oldEditRestaurant = restaurant;
            Name = restaurant.Name;
            Rating = restaurant.Rating.ToString();
            OpeningHour = restaurant.OpeningHour;
            ClosingHour = restaurant.ClosingHour;
            AdressCountry = restaurant.Adress!.Country;
            AdressCity = restaurant.Adress!.City;
            AdressStreet = restaurant.Adress!.Street;
            AdressHouseNumber = restaurant.Adress!.HouseNumber;
            AdressPostalCode = restaurant.Adress!.PostalCode;
        }

        public bool IsEverythingWellInputed()
        {
            if (!Validator.IsStringNotNull(Name)
                || !Validator.IsStringNotNull(Rating)
                || !Validator.IsStringNotNull(OpeningHour)
                || !Validator.IsStringNotNull(ClosingHour)
                || !Validator.IsStringNotNull(AdressCountry)
                || !Validator.IsStringNotNull(AdressCity)
                || !Validator.IsStringNotNull(AdressStreet)
                || !Validator.IsStringNotNull(AdressHouseNumber)
                || !Validator.IsStringNotNull(AdressPostalCode))
            {
                return false;
            }
            if (!Validator.IsHourValid(OpeningHour!) || !Validator.IsHourValid(ClosingHour!))
            {
                return false;
            }
            if (!Validator.IsHouseNumberValid(AdressHouseNumber!))
            {
                return false;
            }
            return true;
        }

        public void FinishAction()
        {
            Rating = Rating!.Replace('_', '0');
            OpeningHour = OpeningHour!.Replace('_', '0');
            ClosingHour = ClosingHour!.Replace('_', '0');
            AdressPostalCode = AdressPostalCode!.Replace('_', '0');

            if (!IsEverythingWellInputed())
            {
                return;
            }
            Adress adress = new(AdressCountry!, AdressCity!, AdressStreet!, AdressHouseNumber!, AdressPostalCode!);
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
            Restaurant restaurant = new(Name!, Convert.ToDecimal(Rating!), OpeningHour!, ClosingHour!, adressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantAddValueMessage(restaurant));
        }

        public void EditRestaurant(Adress adress)
        {
            _adressServices.EditAdress(_oldEditRestaurant!.Adress!, adress);
            Restaurant restaurant = new(Name!, Convert.ToDecimal(Rating!), OpeningHour!, ClosingHour!, _oldEditRestaurant.AdressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantEditValueMessage(restaurant));
        }

        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new RestaurantAdditionCloseWindowMessage());
        }
    }
}
