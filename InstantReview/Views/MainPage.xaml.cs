using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using InstantReview.ViewModels;
using Xamarin.Forms;

namespace InstantReview.Views
{
    public partial class MainPage : ContentPage
    {
        private static readonly ILog Log = LogManager.GetLogger<MainPage>();

        public static event EventHandler<EventArgs> ItemSelected;

        private readonly IPicturePicker picturePicker;
        MainPageViewModel mainPage;

        public MainPage(MainPageViewModel mainPage, IPicturePicker picturePicker)
        {
            InitializeComponent();
            BindingContext = mainPage;
            this.mainPage = mainPage;
            this.picturePicker = picturePicker;

            ReviewList.ItemSelected += ReviewListOnItemSelected;
        }

        private void ReviewListOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var type = (ReviewMenuItem)e.SelectedItem;
            ItemSelected?.Invoke(this, e);
        }

        public MainPage(IPicturePicker picturePicker)
        {
            this.picturePicker = picturePicker;
            InitializeComponent();
        }

        private void NewReview_Clicked(object sender, EventArgs e)
        {
            PickNewImage();
        }

        public async void PickNewImage()
        {
            Stream stream = await picturePicker.GetImageStreamAsync();

            if (stream != null)
            {
                Image image = new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    BackgroundColor = Color.Gray
                };

                var abc = 1;

            }
            else
            {
                Console.WriteLine("Stream was null");
            }
        }
    }
}