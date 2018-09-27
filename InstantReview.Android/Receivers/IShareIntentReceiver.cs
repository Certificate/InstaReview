using System;

namespace InstantReview.Droid.Receivers
{
    public struct IShareIntentReceiver
    {
        event EventHandler<EventArgs> ItemSentEvent;
    }
}