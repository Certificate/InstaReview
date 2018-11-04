using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public interface IPageFactory
    {
        Page CreatePage<TPage, TViewModel>(TViewModel viewModel) where TPage : Page;
    }
}