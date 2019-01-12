using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Common.Logging;
using InstantReview.Views;
using Xamarin.Forms;
using System.Linq;

namespace InstantReview.ViewModels
{
    public class QuestionPageViewModel : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger<QuestionPageViewModel>();

        private readonly IDialogService dialogService;
        private readonly ReviewDataCollector dataCollector;
        private readonly IPageFactory pageFactory;
        private readonly INavigation navigation;
        private readonly ThankYouPageViewModel thankYouPageViewModel;

        private double questionFontSize, nFontSize, emptySpace;
        private bool isDoneBEnable, q8Visible, q9Visible;
        private int[] qAnswers; 
        private Color donebColor, q1YbColor, q1NbColor, q2YbColor, q2NbColor, q3YbColor, q3NbColor, q4YbColor, q4NbColor, q5YbColor, q5NbColor, q6YbColor, q6NbColor, q7YbColor, q7NbColor, q8YbColor, q8NbColor, q9YbColor, q9NbColor;

        public event PropertyChangedEventHandler PropertyChanged;

        

        public QuestionPageViewModel(IDialogService dialogService, ReviewDataCollector dataCollector, IPageFactory pageFactory, INavigation navigation, ThankYouPageViewModel thankYouPageViewModel)
        {
            this.dialogService = dialogService;
            this.dataCollector = dataCollector;
            this.pageFactory = pageFactory;
            this.navigation = navigation;
            this.thankYouPageViewModel = thankYouPageViewModel;
            resetEverythingOnThisPage();
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public bool TemporalContextValue { get; set; }

        public bool SpatialContextValue { get; set; }

        public bool SocialContextValue { get; set; }


        public double noteFontSize
        {
            get { return nFontSize; }
            set
            {
                if (nFontSize != value)
                {
                    nFontSize = value;
                }
            }
        }


        public double qFontSize {
            get { return questionFontSize; }
            set
            {
                if(questionFontSize != value)
                {
                    questionFontSize = value;
                }
            }
        }

        public double gapBetweenQuestions
        {
            get { return emptySpace; }
            set
            {
                if (emptySpace != value)
                {
                    emptySpace = value;
                }
            }
        }


        public bool isQ8Visible
        {
            get { return q8Visible; }
            set
            {
                if (q8Visible != value)
                {
                    q8Visible = value;
                }
            }
        }


        public bool isQ9Visible
        {
            get { return q9Visible; }
            set
            {
                if (q9Visible != value)
                {
                    q9Visible = value;
                }
            }
        }



        public bool doneBEnable
        {
            get { return isDoneBEnable; }
            set
            {
                if (isDoneBEnable != value)
                {
                    isDoneBEnable = value;
                }
            }
        }

        public Color doneBColor
        {
            get { return donebColor; }
            set
            {
                if (donebColor != value)
                {
                    donebColor = value;
                }
            }
        }

        public Color q1YBcolor
        {
            get { return q1YbColor; }
            set
            {
                if(q1YbColor != value)
                {
                    q1YbColor = value;
                }
            }
        }

        public Color q1NBcolor
        {
            get { return q1NbColor; }
            set
            {
                if (q1NbColor != value)
                {
                    q1NbColor = value;
                }
            }
        }



        public Color q2YBcolor
        {
            get { return q2YbColor; }
            set
            {
                if (q2YbColor != value)
                {
                    q2YbColor = value;
                }
            }
        }

        public Color q2NBcolor
        {
            get { return q2NbColor; }
            set
            {
                if (q2NbColor != value)
                {
                    q2NbColor = value;
                }
            }
        }

        public Color q3YBcolor
        {
            get { return q3YbColor; }
            set
            {
                if (q3YbColor != value)
                {
                    q3YbColor = value;
                }
            }
        }

        public Color q3NBcolor
        {
            get { return q3NbColor; }
            set
            {
                if (q3NbColor != value)
                {
                    q3NbColor = value;
                }
            }
        }

        public Color q4YBcolor
        {
            get { return q4YbColor; }
            set
            {
                if (q4YbColor != value)
                {
                    q4YbColor = value;
                }
            }
        }

        public Color q4NBcolor
        {
            get { return q4NbColor; }
            set
            {
                if (q4NbColor != value)
                {
                    q4NbColor = value;
                }
            }
        }

        public Color q5YBcolor
        {
            get { return q5YbColor; }
            set
            {
                if (q5YbColor != value)
                {
                    q5YbColor = value;
                }
            }
        }

        public Color q5NBcolor
        {
            get { return q5NbColor; }
            set
            {
                if (q5NbColor != value)
                {
                    q5NbColor = value;
                }
            }
        }

        public Color q6YBcolor
        {
            get { return q6YbColor; }
            set
            {
                if (q6YbColor != value)
                {
                    q6YbColor = value;
                }
            }
        }

        public Color q6NBcolor
        {
            get { return q6NbColor; }
            set
            {
                if (q6NbColor != value)
                {
                    q6NbColor = value;
                }
            }
        }

        public Color q7YBcolor
        {
            get { return q7YbColor; }
            set
            {
                if (q7YbColor != value)
                {
                    q7YbColor = value;
                }
            }
        }

        public Color q7NBcolor
        {
            get { return q7NbColor; }
            set
            {
                if (q7NbColor != value)
                {
                    q7NbColor = value;
                }
            }
        }

        public Color q8YBcolor
        {
            get { return q8YbColor; }
            set
            {
                if (q8YbColor != value)
                {
                    q8YbColor = value;
                }
            }
        }

        public Color q8NBcolor
        {
            get { return q8NbColor; }
            set
            {
                if (q8NbColor != value)
                {
                    q8NbColor = value;
                }
            }
        }

        public Color q9YBcolor
        {
            get { return q9YbColor; }
            set
            {
                if (q9YbColor != value)
                {
                    q9YbColor = value;
                }
            }
        }

        public Color q9NBcolor
        {
            get { return q9NbColor; }
            set
            {
                if (q9NbColor != value)
                {
                    q9NbColor = value;
                }
            }
        }

        public ICommand CheckResultCommand => new Command(NavigateToThankYouPage);

        public ICommand q1YesCommand => new Command(q1YesAction);
        public ICommand q1NoCommand => new Command(q1NoAction);

        public ICommand q2YesCommand => new Command(q2YesAction);
        public ICommand q2NoCommand => new Command(q2NoAction);

        public ICommand q3YesCommand => new Command(q3YesAction);
        public ICommand q3NoCommand => new Command(q3NoAction);

        public ICommand q4YesCommand => new Command(q4YesAction);
        public ICommand q4NoCommand => new Command(q4NoAction);

        public ICommand q5YesCommand => new Command(q5YesAction);
        public ICommand q5NoCommand => new Command(q5NoAction);

        public ICommand q6YesCommand => new Command(q6YesAction);
        public ICommand q6NoCommand => new Command(q6NoAction);

        public ICommand q7YesCommand => new Command(q7YesAction);
        public ICommand q7NoCommand => new Command(q7NoAction);

        public ICommand q8YesCommand => new Command(q8YesAction);
        public ICommand q8NoCommand => new Command(q8NoAction);

        public ICommand q9YesCommand => new Command(q9YesAction);
        public ICommand q9NoCommand => new Command(q9NoAction);


        public void resetEverythingOnThisPage()
        {
            nFontSize = 15;
            questionFontSize = 25;
            emptySpace = 20;
            qAnswers = Enumerable.Repeat(-1, 9).ToArray();
            donebColor = q1YbColor = q1NbColor = q2YbColor = q2NbColor = q3YbColor = q3NbColor = q4YbColor = q4NbColor = q5YbColor = q5NbColor = q6YbColor = q6NbColor = q7YbColor = q7NbColor = q8YbColor = q8NbColor = q9YbColor = q9NbColor = Color.Default;
            isDoneBEnable = q8Visible = q9Visible = false;
            OnPropertyChanged("q1YbColor");
            OnPropertyChanged("q1NbColor");
            OnPropertyChanged("q2YbColor");
            OnPropertyChanged("q2NbColor");
            OnPropertyChanged("q3YbColor");
            OnPropertyChanged("q3NbColor");
            OnPropertyChanged("q4YbColor");
            OnPropertyChanged("q4NbColor");
            OnPropertyChanged("q5YbColor");
            OnPropertyChanged("q5NbColor");
            OnPropertyChanged("q6YbColor");
            OnPropertyChanged("q6NbColor");
            OnPropertyChanged("q7YbColor");
            OnPropertyChanged("q7NbColor");
            OnPropertyChanged("q8YbColor");
            OnPropertyChanged("q8NbColor");
            OnPropertyChanged("q9YbColor");
            OnPropertyChanged("q9NbColor");
            OnPropertyChanged("donebColor");

            OnPropertyChanged("isDoneBEnable");
            OnPropertyChanged("q8Visible");
            OnPropertyChanged("q9Visible");
        }


        private bool allQuestionsAnswered()
        {
            for(int i = 0; i<6; i++)
            {
                if (qAnswers[i] == -1) return false;
            }
            if (qAnswers[6] == 1 || qAnswers[7] == 1 || qAnswers[8] == 1) return true;
            return false;
        }

        private void updateDoneButton(Color color, bool isEnable)
        {
            donebColor = color;
            isDoneBEnable = isEnable;
            OnPropertyChanged("doneBColor");
            OnPropertyChanged("doneBEnable");
        }
        

        private void q1YesAction()
        {
            q1YbColor = Color.Salmon;
            q1NbColor = Color.Default;
            qAnswers[0] = 1;
            OnPropertyChanged("q1YBcolor");
            OnPropertyChanged("q1NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q1NoAction()
        {
            q1NbColor = Color.Salmon;
            q1YbColor = Color.Default;
            qAnswers[0] = 0;
            OnPropertyChanged("q1YBcolor");
            OnPropertyChanged("q1NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q2YesAction()
        {
            q2YbColor = Color.Salmon;
            q2NbColor = Color.Default;
            qAnswers[1] = 1;
            OnPropertyChanged("q2YBcolor");
            OnPropertyChanged("q2NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q2NoAction()
        {
            q2NbColor = Color.Salmon;
            q2YbColor = Color.Default;
            qAnswers[1] = 0;
            OnPropertyChanged("q2YBcolor");
            OnPropertyChanged("q2NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q3YesAction()
        {
            q3YbColor = Color.Salmon;
            q3NbColor = Color.Default;
            qAnswers[2] = 1;
            OnPropertyChanged("q3YBcolor");
            OnPropertyChanged("q3NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q3NoAction()
        {
            q3NbColor = Color.Salmon;
            q3YbColor = Color.Default;
            qAnswers[2] = 0;
            OnPropertyChanged("q3YBcolor");
            OnPropertyChanged("q3NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q4YesAction()
        {
            q4YbColor = Color.Salmon;
            q4NbColor = Color.Default;
            qAnswers[3] = 1;
            OnPropertyChanged("q4YBcolor");
            OnPropertyChanged("q4NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q4NoAction()
        {
            q4NbColor = Color.Salmon;
            q4YbColor = Color.Default;
            qAnswers[3] = 0;
            OnPropertyChanged("q4YBcolor");
            OnPropertyChanged("q4NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q5YesAction()
        {
            q5YbColor = Color.Salmon;
            q5NbColor = Color.Default;
            qAnswers[4] = 1;
            OnPropertyChanged("q5YBcolor");
            OnPropertyChanged("q5NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q5NoAction()
        {
            q5NbColor = Color.Salmon;
            q5YbColor = Color.Default;
            qAnswers[4] = 0;
            OnPropertyChanged("q5YBcolor");
            OnPropertyChanged("q5NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q6YesAction()
        {
            q6YbColor = Color.Salmon;
            q6NbColor = Color.Default;
            qAnswers[5] = 1;
            OnPropertyChanged("q6YBcolor");
            OnPropertyChanged("q6NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q6NoAction()
        {
            q6NbColor = Color.Salmon;
            q6YbColor = Color.Default;
            qAnswers[5] = 0;
            OnPropertyChanged("q6YBcolor");
            OnPropertyChanged("q6NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q7YesAction()
        {
            q7YbColor = Color.Salmon;
            q7NbColor = Color.Default;
            qAnswers[6] = 1;
            q8Visible = q9Visible = false;
            qAnswers[7] = qAnswers[8] = -1;
            q8YbColor = q9YbColor = q8NbColor = q9NbColor = Color.Default;
            OnPropertyChanged("q7YBcolor");
            OnPropertyChanged("q7NBcolor");
            OnPropertyChanged("q8YBcolor");
            OnPropertyChanged("q8NBcolor");
            OnPropertyChanged("q9YBcolor");
            OnPropertyChanged("q9NBcolor");
            OnPropertyChanged("isQ8Visible");
            OnPropertyChanged("isQ9Visible");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q7NoAction()
        {
            q7NbColor = Color.Salmon;
            q7YbColor = Color.Default;
            qAnswers[6] = 0;
            q8Visible = true;
            OnPropertyChanged("q7YBcolor");
            OnPropertyChanged("q7NBcolor");
            OnPropertyChanged("isQ8Visible");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q8YesAction()
        {
            q8YbColor = Color.Salmon;
            q8NbColor = Color.Default;
            qAnswers[7] = 1;
            q9Visible = false;
            qAnswers[8] = -1;
            q9YbColor = q9NbColor = Color.Default;
            OnPropertyChanged("q8YBcolor");
            OnPropertyChanged("q8NBcolor");
            OnPropertyChanged("q9YBcolor");
            OnPropertyChanged("q9NBcolor");
            OnPropertyChanged("isQ9Visible");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q8NoAction()
        {
            q8NbColor = Color.Salmon;
            q8YbColor = Color.Default;
            qAnswers[7] = 0;
            q9Visible = true;
            OnPropertyChanged("q8YBcolor");
            OnPropertyChanged("q8NBcolor");
            OnPropertyChanged("isQ9Visible");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q9YesAction()
        {
            q9YbColor = Color.Salmon;
            q9NbColor = Color.Default;
            qAnswers[8] = 1;
            OnPropertyChanged("q9YBcolor");
            OnPropertyChanged("q9NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        private void q9NoAction()
        {
            q9NbColor = Color.Salmon;
            q9YbColor = Color.Default;
            qAnswers[8] = 0;
            OnPropertyChanged("q9YBcolor");
            OnPropertyChanged("q9NBcolor");
            if (allQuestionsAnswered()) updateDoneButton(Color.Salmon, true);
            else updateDoneButton(Color.Default, false);
        }

        
        
        private bool AddQuestionsToDataCollector()
        {
            var success = false;
            Log.Debug("Adding questions to data collector");
            try
            {
                if (qAnswers[0] == 1 || qAnswers[1] == 1 || qAnswers[2] == 1)
                {
                    dataCollector.Data.temporalContext = "Intensive";
                }
                else
                {
                    dataCollector.Data.temporalContext = "Allocative";
                }

                if (qAnswers[3] == 1 || qAnswers[4] == 1 || qAnswers[5] == 1)
                {
                    dataCollector.Data.socialContext = "Constraining";
                }
                else
                {
                    dataCollector.Data.socialContext = "Encouraging";
                }

                if (qAnswers[6] == 1)
                {
                    dataCollector.Data.spatialContext = "Visiting";
                }
                else if (qAnswers[7] == 1)
                {
                    dataCollector.Data.spatialContext = "Wandering";
                }
                else
                {
                    dataCollector.Data.spatialContext = "Traveling";
                }

                success = true;
            }
            catch (Exception e)
            {
                Log.Error("Failed to add question data.", e);
            }
            return success;

        }

        

        public async void NavigateToThankYouPage()
        {
            if (allQuestionsAnswered() == false) return;
            if (AddQuestionsToDataCollector())
            {
                Log.Debug("Navigating to Reviews!");
                await navigation.PushAsyncSingle(CreateThankYouPage());
            }
            else
            {
                Log.Error("Could not continue forward.");
                dialogService.showAlert("Error", "Error while gathering data. Resuming to home page.", "Dismiss");
                await navigation.PopToRootAsync();

            }
            
        }

        private Page CreateThankYouPage()
        {
            return pageFactory.CreatePage<ThankYouPage, ThankYouPageViewModel>(thankYouPageViewModel);
        }
    }
}