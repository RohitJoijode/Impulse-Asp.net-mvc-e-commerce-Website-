using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class Sub_CategoryImageModel
    {
        public long? Id { get; set; }
        public long? SubCategoryImageId { get; set; }
        public string SubCategoryColorHexCode { get; set; }
        public long? SubcategoryImageColorId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public decimal? Color_wise_prices { get; set; }
        public string SubCategoryImagePath { get; set; }
        public string SubcategoryImageColorName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SubCategoryName { get; set; } //Here added On (11-02-2023)
        public string CategoryName { get; set; } //Here added On (11-02-2023)
        public HttpPostedFileWrapper UploadImageId { get; set; }
    }
}