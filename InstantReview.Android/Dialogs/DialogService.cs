using System;
using Android.App;
using Android.Content;
using Android.Net.Wifi.Aware;
using Android.Widget;
using Xamarin.Forms;
using static Android.Widget.ToastLength;
using AlertDialog = Android.Support.V7.App.AlertDialog;


namespace InstantReview.Droid.Dialogs
{
    public class DialogService : IDialogService
    {

        private readonly Context context = MainActivity.Instance;


        public void showAlert(string title, string text, string buttonLabel)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(title);
            alert.SetMessage(text);
            alert.SetPositiveButton(buttonLabel, (senderAlert, args) => {
            });


            Dialog dialog = alert.Create();
            dialog.Show();
        }


        public void showRegisteredDialog()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle("Success!");
            alert.SetMessage("Registration completed");
            alert.SetPositiveButton("Ok!", (sender, args) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        public void ShowLoginToast()
        {
            Toast.MakeText(context, "Logged in", Short).Show();
        }
    }
}
