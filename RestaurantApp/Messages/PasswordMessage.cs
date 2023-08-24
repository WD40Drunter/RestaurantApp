using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RestaurantApp.Messages
{
    public class PasswordMessage : ValueChangedMessage<string>
    {
        public PasswordMessage(string value) : base(value)
        {

        }
    }
}
