using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RestaurantApp.Messages;

public class RestaurantIdMessage : ValueChangedMessage<int?>
{
    public RestaurantIdMessage(int? value) : base(value)
    {
    }
}
