using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;

namespace RestaurantApp.Messages
{
    public class SendRestaurantToEditMessage : ValueChangedMessage<Restaurant>
    {
        public SendRestaurantToEditMessage(Restaurant value) : base(value)
        {

        }
    }
}
