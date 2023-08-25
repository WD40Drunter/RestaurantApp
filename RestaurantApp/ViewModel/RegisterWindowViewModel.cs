using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantApp.Messages;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Windows;

namespace RestaurantApp.ViewModel
{
    public partial class RegisterWindowViewModel : SolutionViewModel
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

        public bool ValidateLogin()
        {
            if (string.IsNullOrEmpty(InputLogin))
            {
                MessageBox.Show("Login nie może być pusty");
                return false;
            }
            if (_userService.Exists(InputLogin))
            {
                MessageBox.Show("Login zajęty");
                return false;
            }
            if (InputLogin.Contains(' '))
            {
                MessageBox.Show("Login nie może zawierać spacji");
                return false;
            }
            if (InputLogin.Contains('@')
                || InputLogin.Contains('.')
                || InputLogin.Contains('?')
                || InputLogin.Contains(',')
                || InputLogin.Contains('!')
                || InputLogin.Contains('#')
                || InputLogin.Contains('$')
                || InputLogin.Contains('%')
                || InputLogin.Contains('^')
                || InputLogin.Contains('&')
                || InputLogin.Contains('*')
                || InputLogin.Contains('(')
                || InputLogin.Contains(')')
                || InputLogin.Contains('[')
                || InputLogin.Contains(']')
                || InputLogin.Contains('{')
                || InputLogin.Contains('}')
                || InputLogin.Contains('\\')
                || InputLogin.Contains('|')
                || InputLogin.Contains('/')
                || InputLogin.Contains('>')
                || InputLogin.Contains('<')
                || InputLogin.Contains(';')
                || InputLogin.Contains(':')
                || InputLogin.Contains('"')
                || InputLogin.Contains('\'')
                || InputLogin.Contains('=')
                || InputLogin.Contains('+')
                || InputLogin.Contains('-')
                || InputLogin.Contains('`')
                || InputLogin.Contains('~'))
            {
                MessageBox.Show("Login nie może zawierać znaków specjalnych innych niż \"_\"");
                return false;
            }
            return true;
        }

        public bool ValidatePassword()
        {
            if (string.IsNullOrEmpty(InputPassword))
            {
                MessageBox.Show("Hasło nie może być puste");
                return false;
            }
            return true;
        }

        public void AddUser()
        {
            if (!ValidateLogin())
            {
                return;
            }
            if (!ValidatePassword())
            {
                return;
            }

            User user = new(InputLogin!, SecretHasher.Hash(InputPassword!), PrivilageValue);
            _userService.AddUser(user);

            CloseWindow();
        }

        public static void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new RegisterWindowCloseMessage());
        }
    }
}
