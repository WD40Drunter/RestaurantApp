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
    public partial class RegisterWindowViewModel : ObservableRecipient
    {
        public RegisterWindowViewModel(IUserService userService)
        {
            _userService = userService;

            AddUserCommand = new RelayCommand(AddUser);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }
        private readonly IUserService _userService;

        public IRelayCommand AddUserCommand { get; }
        public IRelayCommand CloseWindowCommand { get; }

        [ObservableProperty]
        private string? _inputLogin;

        [ObservableProperty]
        private string? _inputPassword;

        [ObservableProperty]
        private string _privilageValue = "Standard";


        public void AddUser()
        {

        }

        public void CloseWindow()
        {

        }
    }
}
