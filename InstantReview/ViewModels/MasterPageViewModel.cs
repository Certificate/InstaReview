using System.Windows.Input;
using Common.Logging;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MasterPageViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger<MasterPageViewModel>();

        public ICommand LogOutCommand => new Command(LogOut);

        private void LogOut()
        {
            Log.Debug("Log Out!");
        }
    }
}