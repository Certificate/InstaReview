using System;
namespace InstantReview
{
    public interface IDialogService
    {
        void showAlert(string text);

        void showRegisteredDialog();

        void ShowLoginToast();

    }
}
