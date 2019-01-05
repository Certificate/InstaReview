using System;
using System.Collections.Generic;
using System.Text;
using InstantReview.Login;
using Xamarin.Forms;

namespace InstantReview
{
    public class Review
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int appId { get; set; }
        public Category category { get; set; }
        public string temporalContext { get; set; }
        public string spatialContext { get; set; }
        public string socialContext { get; set; }
        public string textReview { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string imagePath { get; set; }
    }

    public class ReviewMenuItem
    {
        public ReviewMenuItem(int id, int userId, int appId, Category category, string temporalContext, string spatialContext, string socialContext, string textReview, DateTime createdAt, DateTime updatedAt, string imagePath, ImageSource imageSource)
        {
            this.id = id;
            this.userId = userId;
            this.appId = appId;
            this.category = category;
            this.temporalContext = temporalContext;
            this.spatialContext = spatialContext;
            this.socialContext = socialContext;
            this.textReview = textReview;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            this.imagePath = imagePath;
            ImageSource = imageSource;
        }

        public int id { get; set; }
        public int userId { get; set; }
        public int appId { get; set; }
        public Category category { get; set; }
        public string temporalContext { get; set; }
        public string spatialContext { get; set; }
        public string socialContext { get; set; }
        public string textReview { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string imagePath { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
