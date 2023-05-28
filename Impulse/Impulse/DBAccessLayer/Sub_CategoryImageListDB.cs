using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{

    [Table("Sub_CategoryImageList")]
    public class Sub_CategoryImageListDB
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id { get; set; }
        public long? Sub_CategoryImageId { get; set; }
        public long? Sub_CategoryImageColorId { get; set; } //New Column Added On (31-01-2023)
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string Sub_CategoryImagePath { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}