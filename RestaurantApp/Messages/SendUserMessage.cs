using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;

namespace RestaurantApp.Messages
{
    public class SendUserMessage : ValueChangedMessage<User>
    {
        public SendUserMessage(User value) : base(value)
        {

        }
    }
}
