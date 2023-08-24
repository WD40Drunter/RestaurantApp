using CommunityToolkit.Mvvm.Messaging.Messages;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    public class SendUserMessage : ValueChangedMessage<User>
    {
        public SendUserMessage(User value) : base(value)
        {
            
        }
    }
}
