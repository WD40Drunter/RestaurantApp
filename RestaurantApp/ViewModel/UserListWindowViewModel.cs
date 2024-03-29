﻿using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace RestaurantApp.ViewModel
{
    public partial class UserListWindowViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        public UserListWindowViewModel(IUserService userService)
        {
            _userService = userService;

            CollectionCreator = new();

            UserList = new ObservableCollection<User>(_userService.GetUsers());
            UserCollection = CollectionCreator.GetCollection(UserList);

            UserList.CollectionChanged += new NotifyCollectionChangedEventHandler(UserList_PropertyChanged);
            foreach (User user in UserList)
            {
                user.PropertyChanged += new PropertyChangedEventHandler(User_PropertyChanged);
            }
        }
        private readonly IUserService _userService;

        [ObservableProperty]
        private ObservableCollection<User>? _userList;

        [ObservableProperty]
        private ICollectionView? _userCollection;

        [ObservableProperty]
        private string? _searchUserValue;

        public CollectionCreator CollectionCreator { get; set; }

        public void RefreshUserCollection()
        {
            UserCollection?.Refresh();
        }

        partial void OnSearchUserValueChanged(string? value)
        {
            CollectionCreator.SearchUserValue = value;
            RefreshUserCollection();
        }

        public void UserList_PropertyChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (Dish dish in e.OldItems)
                {
                    dish.PropertyChanged -= new PropertyChangedEventHandler(User_PropertyChanged);
                }
            }
            if (e.NewItems is not null)
            {
                foreach (Dish dish in e.NewItems)
                {
                    dish.PropertyChanged += new PropertyChangedEventHandler(User_PropertyChanged);
                }
            }
        }

        public void User_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _userService.UpdateUserAccess();
        }
    }
}
