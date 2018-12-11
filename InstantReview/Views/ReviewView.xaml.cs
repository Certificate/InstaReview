using Xamarin.Forms;

namespace InstantReview.Views
{
    public partial class ReviewView : StackLayout
    {
        public static BindableProperty SingleLineInfoProperty
            = BindableProperty.Create(nameof(SingleLineInfo), typeof(bool), typeof(ReviewView), false);

        public bool SingleLineInfo
        {
            get { return (bool)GetValue(SingleLineInfoProperty); }
            set { SetValue(SingleLineInfoProperty, value); }
        }

        public static BindableProperty SymbolVerticalAlignmentProperty
            = BindableProperty.Create(nameof(SymbolVerticalAlignment), typeof(LayoutOptions), typeof(ReviewView), LayoutOptions.Start);

        public LayoutOptions SymbolVerticalAlignment
        {
            get { return (LayoutOptions)GetValue(SymbolVerticalAlignmentProperty); }
            set { SetValue(SymbolVerticalAlignmentProperty, value); }
        }

        public ReviewView()
        {
            InitializeComponent();
        }
    }
}
