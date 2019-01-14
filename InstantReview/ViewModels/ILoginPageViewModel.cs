using System;

namespace InstantReview.ViewModels
{
    public interface ILoginPageViewModel
    {
        event EventHandler<EventArgs> LoginSuccessful;
    }
}