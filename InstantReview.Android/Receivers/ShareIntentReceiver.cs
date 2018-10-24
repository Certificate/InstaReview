using System;
using System.IO;
using Android.Content;
using Android.Graphics;
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

                if (uri != null)
                {
                    try
                    {
                        //UserImage = GenerateImage(GetBitmap(uri));
                        ImagePath = BitmapToFolder(GetBitmap(uri));
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
        
        private Bitmap GetBitmap(Android.Net.Uri uriImage)
        {
            var bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(MainActivity.Instance.ContentResolver, uriImage);
            return bitmap;
        }

        private Image GenerateImage(Bitmap bitmap)
        {
            Image image = new Image();

            var imgsrc = ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                ms.Seek(0L, SeekOrigin.Begin);
                return ms;
            });

            image.Source = imgsrc;
            return image;
        }

        private string BitmapToFolder(Bitmap bitmap)
        {

            var path = MainActivity.Instance.GetExternalFilesDir(null).AbsolutePath + "/temp";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            var filePath = System.IO.Path.Combine(path, "sharedPicture.png");
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Flush();
            stream.Close();
            
            
            Image ass = new Image();
            ass.Source = ImageSource.FromStream(() => new FileStream(filePath, FileMode.Open));
            Log.Debug(ass.Height);
            Log.Debug(ass.Width);
            Log.Debug("Siin o");
            UserImage = ass;

            return filePath;
            
            

        }
    }
}