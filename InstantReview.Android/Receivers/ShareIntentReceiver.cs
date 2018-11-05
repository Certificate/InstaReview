using System;
using System.IO;
using System.Threading.Tasks;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Widget;
using Common.Logging;
using InstantReview.Receivers;
using Xamarin.Forms;


namespace InstantReview.Droid.Receivers
{
    public class ShareIntentReceiver : IShareIntentReceiver
    {
        public event EventHandler<EventArgs> ItemsReceivedEvent;
        private static readonly ILog Log = LogManager.GetLogger<ShareIntentReceiver>();

        public Image UserImage { get; set; }
        public string ImagePath { get; set; }

        public void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == null) return;

            Log.Debug($"Received an event: {intent}");
            if (intent.Action.Equals(Intent.ActionSend))
            {
                Log.Debug("Action directed to ACTION_SEND");
                var uri = (Android.Net.Uri) intent.GetParcelableExtra(Intent.ExtraStream);
                var uriTool = new UriTool();
                ImagePath = uriTool.GetActualPathFromFile(uri);
                Toast.MakeText(context, "Image Received", ToastLength.Short).Show();

                while (ContextCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.ReadExternalStorage)
                       != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(MainActivity.Instance,
                        new String[] { Manifest.Permission.ReadExternalStorage }, 1);
                    Task.Delay(TimeSpan.FromSeconds(1.5)).Wait();
                }

                ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
            }
            else if (intent.Action.Equals(Intent.ActionSendMultiple))
            {
                Log.Debug("Multiple pictures received!");
                ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void RequestPermision()
        {
            if (ContextCompat.CheckSelfPermission(MainActivity.Instance, Manifest.Permission.ReadExternalStorage)
                != Permission.Granted)
            {
                Log.Debug("Permission not granted.");
                if (ActivityCompat.ShouldShowRequestPermissionRationale(MainActivity.Instance,
                    Manifest.Permission.ReadExternalStorage))
                {
                    Log.Debug("TODO: Explain permission.");
                }
                else
                {
                    // No explanation needed; request the permission
                    ActivityCompat.RequestPermissions(MainActivity.Instance,
                        new String[] { Manifest.Permission.ReadExternalStorage }, 1);
                }
            }
        }
    }
}