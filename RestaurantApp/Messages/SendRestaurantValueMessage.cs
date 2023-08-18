using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    public class SendRestaurantValueMessage : ValueChangedMessage<Restaurant>
    {
        public SendRestaurantValueMessage(Restaurant value) : base(value)
        {
            
        }
    }
}
