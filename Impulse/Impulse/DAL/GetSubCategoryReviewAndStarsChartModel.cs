using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class GetSubCategoryReviewAndStarsChartModel
    {
        public long? GivenStars {get;set;}
        public long? SubCategoryParticulerTotalStarsCount { get;set;}
        public decimal? averageStar { get; set; }
        public long? Sub_CategoryId { get; set; }
        public long? TotalStarCount { get; set; }
        public long? OneStarCount { get; set; }
        public long? TwoStarCount { get; set; }
        public long? ThreeStarCount { get; set; }
        public long? FourStarCount { get; set; }
        public long? FiveStarCount { get; set; }
        public Decimal? Parcentage { get; set; }
    }
}