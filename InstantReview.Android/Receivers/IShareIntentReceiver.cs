using System;

namespace InstantReview.Droid.Receivers
{
    public interface IShareIntentReceiver
    {
        event EventHandler<EventArgs> ItemSentEvent;
    }
}