using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    public class SendRestaurantAddValueMessage : ValueChangedMessage<Restaurant>
    {
        public SendRestaurantAddValueMessage(Restaurant value) : base(value)
        {
            
        }
    }
}
