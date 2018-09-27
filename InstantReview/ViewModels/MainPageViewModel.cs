using System;
using System.Windows.Input;
using Common.Logging;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class MainPageViewModel
    {
        private readonly IDialogService dialogService;
        private static readonly ILog Log = LogManager.GetLogger<MainPageViewModel>();

        public MainPageViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            Console.WriteLine("Started ButtonHandler class");
        }


        public ICommand ButtonPressCommand => new Command(ClickButton);

        private void ClickButton(){
            Log.Debug("LogTest!");
            dialogService.showAlert("Encounter with a robot!");
        }
    }
}
