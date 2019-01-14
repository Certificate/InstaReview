using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstantReview.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstantReview.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage(QuestionPageViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (this.BindingContext as QuestionPageViewModel)?.resetEverythingOnThisPage();
        }
    }
}