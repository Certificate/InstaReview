using Common.Logging.Factory;

namespace InstantReview
{
    public class ReviewDataCollector
    {

        public ReviewData Data;

        public void InitializeDataCollector()
        {
            Data = new ReviewData();
        }

        public string GenerateReviewText()
        {
            return $"IP: {Data.ImagePath}, " +
                       $"AddInfo: {Data.AdditionalInfo}, " +
                       $"Q1:{Data.Question1}, " +
                       $"Q2:{Data.Question2}, " +
                       $"Q3:{Data.Question3}, " +
                       $"Q4:{Data.Question4}";
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