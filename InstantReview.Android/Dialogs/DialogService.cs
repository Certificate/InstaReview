using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using static Android.Widget.ToastLength;
using AlertDialog = Android.Support.V7.App.AlertDialog;


namespace InstantReview.Droid.Dialogs
{
    public class DialogService : IDialogService
    {

        private readonly Context context = MainActivity.Instance;


        public void showAlert(string text)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(text);
            alert.SetMessage("General Kenobi!");
            alert.SetPositiveButton("Hello There!", (senderAlert, args) => {
                Toast.MakeText(context, "Fight!!", Short).Show();
            });

            alert.SetNegativeButton("<do nothing>", (senderAlert, args) => {
                Toast.MakeText(context, "You escaped!", Short).Show();
            });

            Dialog dialog = alert.Create();
            //TODO Show dialog. Need to implement AppCompat theme before that.
            Console.WriteLine("YEAAHH BOII");
            dialog.Show();
        }
    }
}
