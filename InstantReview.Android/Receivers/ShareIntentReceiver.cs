using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;
using Common.Logging;
using InstantReview.Receivers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace InstantReview.Droid.Receivers
{
    public class ShareIntentReceiver : IShareIntentReceiver
    {
        public event EventHandler<EventArgs> ItemsReceivedEvent;
        private static readonly ILog Log = LogManager.GetLogger<ShareIntentReceiver>();
        private IList<Image> images { get; }


        public IList<Image> Images() => images;

        public void OnReceive(Context context, Intent intent)
        {
            Log.Debug($"Received an event: {intent}");
            if (intent.Action.Equals(Intent.ActionSend))
            {
                Log.Debug("Action directed to ACTION_SEND");
                var uriFromExtras = intent.GetParcelableExtra(Intent.ExtraStream) as Android.Net.Uri; 
                var subject = intent.GetStringExtra(Intent.ExtraSubject);
                var file = intent.ClipData.GetItemAt(0);


                // Open a stream from the URI 
                var dataStream = context.ContentResolver.OpenInputStream(file.Uri);

                // Save it over 
                var memOfPdf = new System.IO.MemoryStream();
                dataStream.CopyTo(memOfPdf);
                var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = System.IO.Path.Combine(docsPath, "temp.jpg");

                System.IO.File.WriteAllBytes(filePath, memOfPdf.ToArray());

                var image = new Image { Source = filePath };
                //images.Add(image);

                Toast.MakeText(context, "Image Received", ToastLength.Short).Show();
                ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
            }
            else if (intent.Action.Equals(Intent.ActionSendMultiple))
            {
                Log.Debug("Multiple pictures received!");
                ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
            }
        }


    }
}