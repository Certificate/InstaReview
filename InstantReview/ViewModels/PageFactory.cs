using System;
using Xamarin.Forms;

namespace InstantReview.ViewModels
{
    public class PageFactory : IPageFactory
    {
        public Page CreatePage<TPage, TViewModel>(TViewModel viewModel) where TPage : Page
        {
            if (viewModel != null)
            {
                return (TPage) Activator.CreateInstance(typeof (TPage), viewModel);
            }
            else
            {
                return (TPage) Activator.CreateInstance<TPage>();
            }
        }
    }
}