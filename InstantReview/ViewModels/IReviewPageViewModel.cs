using System;

namespace InstantReview.ViewModels
{
    public interface IReviewPageViewModel
    {
        event EventHandler<EventArgs> ViewModelReadyEvent;
    }
}
