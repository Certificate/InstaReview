using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstantReview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipingPageUI : ContentPage
    {

        int current = 1;
        int max = 10;

        string[] questionsArray = new string[] { "insert q1",
            "insert q2",
            "insert q3",
            "insert q4",
            "insert q5",
            "insert q6",
            "insert q7",
            "insert q8",
            "insert q9",
            "insert q10",};

        public SwipingPageUI()
        {
            InitializeComponent();
        }

        void OnSwiped(object sender, SwipedEventArgs e)
            {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    // Handle the swipe
                        BackgroundColor = Color.LightGreen;
                        Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
                        {
                            BackgroundColor = Color.FromHex("#FEFFFF");
                            return false;
                        });
                    
                    if(current < questionsArray.Length)
                    {
                        System.Diagnostics.Debug.WriteLine("Swiped to left");
                        current++;
                        Question_number.Text = "Question " + current + "/" + max;
                        Question_question.Text = questionsArray[current-1];
                    }
                    else
                    {
                        Question_number.Text = "Redirect to thankYouUI";
                        Question_question.Text = "";
                    }
                    break;
                    

                case SwipeDirection.Left:
                    // Handle the swipe

                    BackgroundColor = Color.Red;
                    Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
                    {
                        BackgroundColor = Color.FromHex("#FEFFFF");
                        return false;
                    });

                    if (current < questionsArray.Length)
                    {
                        System.Diagnostics.Debug.WriteLine("Swiped to right");
                        current++;
                        Question_number.Text = "Question " + current + "/" + max;
                        Question_question.Text = questionsArray[current - 1];
                    }
                    else
                    {
                        Question_number.Text = "Redirect to thankYouUI";
                        Question_question.Text = "";
                    }
                    break;

            }
        }
    }
}