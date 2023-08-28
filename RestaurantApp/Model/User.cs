using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RestaurantApp.Model
{
    public class User : INotifyPropertyChanged
    {
        [Key]
        public int UserId { get; set; }

        public User(string login, string password, string? access)
        {
            Login = login;
            Password = password;
            Access = access ?? "Standard";

        }

        public string Login { get; set; }
        public string Password { get; set; }

        private string? _access;
        public string Access 
        { 
            get 
            { 
                return _access ?? string.Empty;
            } 
            set 
            {
                _access = value;
                NotifyPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
