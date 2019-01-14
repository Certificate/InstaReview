using System;
namespace InstantReview
{
    public interface IDialogService
    {
        void showAlert(string title, string text, string buttonLabel);

        void showRegisteredDialog();

        void ShowLoginToast();

    }
}
