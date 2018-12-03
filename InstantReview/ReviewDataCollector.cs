using Common.Logging;
using Common.Logging.Factory;
using InstantReview.ViewModels;

namespace InstantReview
{
    public class ReviewDataCollector
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewDataCollector>();

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

        public string ToSerializedFormat()
        {
            ReviewJson json = new ReviewJson();
            json.textReview = GenerateReviewText();
            json.appId = 1;
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            Log.Debug(serialized);
            return serialized;
        }
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


    public class ReviewJson
    {
        public int appId { get; set; }
        public string temporalContext = "Intensive";
        public string spatialContext = "Visiting";
        public string socialContext = "Constraining";
        public string textReview { get; set; }
    }
}

   