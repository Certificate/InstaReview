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
    public partial class ContextualQuestions : ContentPage
    {
        public ContextualQuestions()
        {
            InitializeComponent();

            titleLabel.FontSize = 30;
            Q1.FontSize = Q2.FontSize = Q3.FontSize = Q4.FontSize = Q5.FontSize = Q6.FontSize = Q7.FontSize = Q8.FontSize = Q9.FontSize = 20;
            Q7Switch.Toggled += q7SwitchToggled;
            Q8Switch.Toggled += q8SwitchToggled;

            void q7SwitchToggled(object sender, ToggledEventArgs e)
            {
                if (Q7Switch.IsToggled)
                {
                    Q8.IsVisible = false;
                    Q8Switch.IsVisible = false;

                    Q9.IsVisible = false;
                    Q9Switch.IsVisible = false;
                }
                else
                {
                    Q8.IsVisible = true;
                    Q8Switch.IsToggled = false;
                    Q8Switch.IsVisible = true;

                    Q9.IsVisible = true;
                    Q9Switch.IsToggled = false;
                    Q9Switch.IsVisible = true;
                }
            }


            void q8SwitchToggled(object sender, ToggledEventArgs e)
            {
                if (Q8Switch.IsToggled)
                {
                    Q9.IsVisible = false;
                    Q9Switch.IsVisible = false;
                }
                else
                {
                    Q9.IsVisible = true;
                    Q9Switch.IsVisible = true;
                    Q9Switch.IsToggled = false;
                }
            }
        }
    }
}