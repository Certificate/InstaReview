using System;
using System.Collections.Generic;
using System.Text;

namespace InstantReview
{
    public class Review
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int appId { get; set; }
        public string categoryName { get; set; }
        public string temporalContext { get; set; }
        public string spatialContext { get; set; }
        public string socialContext { get; set; }
        public string textReview { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string imagePath { get; set; }
    }
}
