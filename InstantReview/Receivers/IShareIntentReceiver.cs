using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InstantReview.Receivers
{
    public interface IShareIntentReceiver
    {
        event EventHandler<EventArgs> ItemsReceivedEvent;

        Image UserImage { get; set; }
        
        string ImagePath { get; set; }
    }
}