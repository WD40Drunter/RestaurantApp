using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RestaurantApp.Messages
{
    class ValuesOfStatusToChangeItInDatabaseMessage : ValueChangedMessage<string[]>
    {
        public ValuesOfStatusToChangeItInDatabaseMessage(string[] value) : base(value)
        {

        }
    }
}
