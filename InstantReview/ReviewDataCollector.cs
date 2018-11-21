using Common.Logging.Factory;

namespace InstantReview
{
    public class ReviewDataCollector : IReviewDataCollector
    {

        public ReviewData Data;

        public ReviewData InitializeDataCollector()
        {
            ReviewData data = new ReviewData();
            return data;
        }




        public class ReviewData
        {
            public string ImagePath { get; set; }
            public string AdditionalInfo { get; set; }
            public string Question1 { get; set; }
            public string Question2 { get; set; }
            public string Question3 { get; set; }
            public string Question4 { get; set; }
        }
    }

    
}