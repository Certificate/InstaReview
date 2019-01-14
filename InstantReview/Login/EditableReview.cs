using System;
using System.Collections.Generic;

namespace InstantReview.Login
{

    public class ApplicationIdentifier
    {
        public int id { get; set; }
        public string name { get; set; }
        public string operatingSystem { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
        public int reviewId { get; set; }
        public string fileName { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class EditableReview
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int appId { get; set; }
        public string temporalContext { get; set; }
        public string spatialContext { get; set; }
        public string socialContext { get; set; }
        public string textReview { get; set; }
        public object thumbnailId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public ApplicationIdentifier application { get; set; }
        public Category category { get; set; }
        public List<Image> images { get; set; }
        public object thumbnail { get; set; }
    }
}