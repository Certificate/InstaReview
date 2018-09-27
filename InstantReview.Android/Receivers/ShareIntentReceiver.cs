using System;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;
using Common.Logging;
using Xamarin.Forms;

namespace InstantReview.Droid.Receivers
{
    public class ShareIntentReceiver : IShareIntentReceiver
    {
        public event EventHandler<EventArgs> ItemSentEvent;
        private static readonly ILog Log = LogManager.GetLogger<ShareIntentReceiver>();


        public void OnReceive(Context context, Intent intent)
        {
            Log.Debug($"Received an event: {intent}");
            if (intent.Action.Equals(Intent.ActionSend))
            {

                var uriFromExtras = intent.GetParcelableExtra(Intent.ExtraStream) as Android.Net.Uri; 
                var subject = intent.GetStringExtra(Intent.ExtraSubject);
                
                Log.Debug(uriFromExtras);
                Log.Debug(subject);
                
                var image = new Image { Source = subject };
                
                
                ItemSentEvent?.Invoke(this, EventArgs.Empty);
            }
            else if (intent.Action.Equals(Intent.ActionSendMultiple))
            {
                Log.Debug("Multiple pictures received!");
                ItemSentEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}