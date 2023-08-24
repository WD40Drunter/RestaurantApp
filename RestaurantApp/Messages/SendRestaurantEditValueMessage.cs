using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;

namespace RestaurantApp.Messages
{
    public class SendRestaurantEditValueMessage : ValueChangedMessage<Restaurant>
    {
        public SendRestaurantEditValueMessage(Restaurant value) : base(value)
        {

        }
    }
}
