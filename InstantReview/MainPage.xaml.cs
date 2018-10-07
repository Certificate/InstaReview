using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPage)
        {
            InitializeComponent();
            BindingContext = mainPage;
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}