using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages
{
    public class PasswordMessage : ValueChangedMessage<string>
    {
        public PasswordMessage(string value) : base(value)
        {
            
        }
    }
}
