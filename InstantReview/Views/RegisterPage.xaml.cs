using System;
using System.Collections.Generic;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterPageViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
