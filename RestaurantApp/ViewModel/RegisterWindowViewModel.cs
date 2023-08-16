using CommunityToolkit.Mvvm.ComponentModel;
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


        }
        private readonly IUserService _userService;
    }
}
