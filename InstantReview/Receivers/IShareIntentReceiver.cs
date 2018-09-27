using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InstantReview.Receivers
{
    public interface IShareIntentReceiver
    {
        event EventHandler<EventArgs> ItemsReceivedEvent;
        IList<Image> Images();
    }
}