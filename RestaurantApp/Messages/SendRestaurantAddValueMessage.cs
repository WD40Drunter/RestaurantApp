using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;

namespace RestaurantApp.Messages
{
    public class SendRestaurantAddValueMessage : ValueChangedMessage<Restaurant>
    {
        public SendRestaurantAddValueMessage(Restaurant value) : base(value)
        {

        }
    }
}
