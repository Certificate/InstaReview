using System;
using Xamarin.Forms;

namespace InstantReview.Droid
{
    public interface IImageOperations
    {
        event EventHandler<EventArgs> ImageReceivedEvent;

        Image UserImage { get; set; }
    }
}