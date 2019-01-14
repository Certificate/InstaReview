using System;
using System.Collections.Generic;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(ILoginPageViewModel loginPageViewModel)
        {
            InitializeComponent();
            BindingContext = loginPageViewModel;
        }
    }
}
