using System;
using System.Windows.Input;
using Xamarin.Forms;
namespace InstantReview
{
    public class MainPageViewModel
    {
        private readonly IDialogService dialogService;

        public MainPageViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            Console.WriteLine("Started ButtonHandler class");
        }


        public ICommand ButtonPressCommand => new Command(ClickButton);

        private void ClickButton(){
            dialogService.showAlert("Encounter with a robot!");
        }
    }
}
