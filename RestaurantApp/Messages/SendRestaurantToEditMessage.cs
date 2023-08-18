using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    public class SendRestaurantToEditMessage :ValueChangedMessage<Restaurant>
    {
        public SendRestaurantToEditMessage(Restaurant value) : base(value)
        {
            
        }
    }
}
