using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class SubCategoryImageList
    {
        public long? Id { get; set; }
        public long? Sub_CategoryImageId { get; set; }
        public long? SubCategoryImageColorId { get; set; } //New Column Added On (31-01-2023)
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string SubCategoryImagePath { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public HttpPostedFileWrapper UploadImageId { get; set; }

    }
}