using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RestaurantApp.Messages
{
    class ValuesOfAccessToChangeItInDatabaseMessage : ValueChangedMessage<string[]>
    {
        public ValuesOfAccessToChangeItInDatabaseMessage(string[] value) : base(value)
        {

        }
    }
}
