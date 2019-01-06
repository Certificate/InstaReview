using Common.Logging;
using Common.Logging.Factory;
using InstantReview.Login;
using InstantReview.ViewModels;

namespace InstantReview
{
    public class ReviewDataCollector
    {
        private static readonly ILog Log = LogManager.GetLogger<ReviewDataCollector>();

        public Review Data;

        public void InitializeDataCollector()
        {
            Data = new Review();
            Data.category = new Category();
        }

        public string ToSerializedFormat()
        {
            ReviewJson json = new ReviewJson {appId = 1, categoryName = Data.category.categoryName, temporalContext = Data.temporalContext, socialContext = Data.socialContext, spatialContext = Data.spatialContext, textReview = Data.textReview};
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            Log.Debug(serialized);
            return serialized;
        }

        private class ReviewJson
        {
            public int appId { get; set; }
            public string categoryName { get; set; }
            public string temporalContext { get; set; }
            public string spatialContext { get; set; }
            public string socialContext { get; set; }
            public string textReview { get; set; }
        }
    }


}

   