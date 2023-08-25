using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.ViewModel
{
    public partial class SolutionViewModel : ObservableObject
    {
        public SolutionViewModel()
        {
            
        }

        [ObservableProperty]
        private static User? _loggedInUser;
    }
}
