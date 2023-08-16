using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Messages;

public class RestaurantIdMessage : ValueChangedMessage<int>
{
    public RestaurantIdMessage(int value) : base(value)
    {
    }
}
