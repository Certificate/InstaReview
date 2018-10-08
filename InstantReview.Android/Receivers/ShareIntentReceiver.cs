using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using Common.Logging;
using InstantReview.Receivers;
using Java.IO;
using Java.Nio.Channels;
using Xamarin.Forms;
using Console = System.Console;
using File = Java.IO.File;
using FileNotFoundException = Java.IO.FileNotFoundException;

namespace InstantReview.Droid.Receivers
{
    public class ShareIntentReceiver : IShareIntentReceiver
    {
        public event EventHandler<EventArgs> ItemsReceivedEvent;
        private static readonly ILog Log = LogManager.GetLogger<ShareIntentReceiver>();

        public void OnReceive(Context context, Intent intent)
        {
            Log.Debug($"Received an event: {intent}");
            if (intent.Action.Equals(Intent.ActionSend))
            {
                Log.Debug("Action directed to ACTION_SEND");
                
                var uri = (Android.Net.Uri) intent.GetParcelableExtra(Intent.ExtraStream);

                if (uri != null)
                {
                    try
                    {
                        ExportBitmapAsPNG(GetBitmap(uri));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                    
                    Toast.MakeText(context, "Image Received", ToastLength.Short).Show();
                    ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (intent.Action.Equals(Intent.ActionSendMultiple))
            {
                Log.Debug("Multiple pictures received!");
                ItemsReceivedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        
        private Android.Graphics.Bitmap GetBitmap(Android.Net.Uri uriImage)
        {
            Android.Graphics.Bitmap mBitmap = null;
            mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(MainActivity.Instance.ContentResolver, uriImage);
            return mBitmap;
        }
        
        void ExportBitmapAsPNG(Bitmap bitmap)
        {
            var path = MainActivity.Instance.GetExternalFilesDir(null).AbsolutePath;
            var filePath = System.IO.Path.Combine(path, "example.png");
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Close();
        }

    }
}