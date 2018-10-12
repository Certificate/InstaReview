using System.Windows.Input;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MasterPageViewModel
    {
        public ICommand LogOutCommand => new Command(LogOut);

        private void LogOut()
        {
            throw new System.NotImplementedException();
        }
    }
}