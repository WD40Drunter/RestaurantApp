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
    public partial class UserListWindowViewModel : ObservableRecipient
    {
        public UserListWindowViewModel(IUserService userService)
        {
            _userService = userService;

            CollectionCreator = new();

            UserList = new ObservableCollection<User>(_userService.GetUsers());
            UserCollection = CollectionCreator.GetCollection(UserList);

            WeakReferenceMessenger.Default.Register<ValuesOfAccessToChangeItInDatabaseMessage>(this, (r, m) =>
            {
                int userId = int.Parse(m.Value[0]);
                string newAccess = m.Value[1];
                _userService.UpdateUserAccess(userId, newAccess);
            });

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

    }
}
