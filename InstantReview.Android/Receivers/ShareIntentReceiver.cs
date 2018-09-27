using System;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;
using Common.Logging;

namespace InstantReview.Droid.Receivers
{
    public class ShareIntentReceiver : BroadcastReceiver
    {
        public event EventHandler<EventArgs> ItemSentEvent;
        private static readonly ILog Log = LogManager.GetLogger<ShareIntentReceiver>();


        public override void OnReceive(Context context, Intent intent)
        {
            Log.Debug($"Received an event: {intent}");
            if (intent.Action.Equals(Intent.ActionSend))
            {
                Log.Debug("Event was caught successfully.");
                ItemSentEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}