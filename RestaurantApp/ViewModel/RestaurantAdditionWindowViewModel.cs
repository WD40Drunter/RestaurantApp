﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System;

namespace RestaurantApp.ViewModel
{
    internal partial class RestaurantAdditionWindowViewModel : ObservableRecipient
    {
        public RestaurantAdditionWindowViewModel(IAdressServices adressServices)
        {
            _adressServices = adressServices;

            FinishActionCommand = new RelayCommand(FinishAction);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }
        private readonly IAdressServices _adressServices;

        public IRelayCommand FinishActionCommand { get; }
        public IRelayCommand CloseWindowCommand { get; }

        [ObservableProperty]
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

        public void AssignRestaurantValuesForEdit()
        {
            if( OldEditRestaurant is null)
            {
                return;
            }
            Name = OldEditRestaurant.Name;
            Rating = OldEditRestaurant.Rating.ToString();
            OpeningHour = OldEditRestaurant.OpeningHour;
            ClosingHour = OldEditRestaurant.ClosingHour;
            AdressCountry = OldEditRestaurant.Adress!.Country;
            AdressCity = OldEditRestaurant.Adress!.City;
            AdressStreet = OldEditRestaurant.Adress!.Street;
            AdressHouseNumber = OldEditRestaurant.Adress!.HouseNumber;
            AdressPostalCode = OldEditRestaurant.Adress!.PostalCode;
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
            if (OldEditRestaurant is null)
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
            _adressServices.EditAdress(OldEditRestaurant!.Adress!, adress);
            Restaurant restaurant = new(Name!, Convert.ToDecimal(Rating!), OpeningHour!, ClosingHour!, OldEditRestaurant.AdressId);
            WeakReferenceMessenger.Default.Send(new SendRestaurantEditValueMessage(restaurant));
        }

        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new RestaurantAdditionCloseWindowMessage());
        }

        partial void OnOldEditRestaurantChanged(Restaurant? value)
        {
            AssignRestaurantValuesForEdit();
        }
    }
}
