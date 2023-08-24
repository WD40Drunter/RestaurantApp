using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    class ValuesOfStatusToChangeItInDatabaseMessage : ValueChangedMessage<string[]>
    {
        public ValuesOfStatusToChangeItInDatabaseMessage(string[] value) : base(value)
        {
            
        }
    }
}
