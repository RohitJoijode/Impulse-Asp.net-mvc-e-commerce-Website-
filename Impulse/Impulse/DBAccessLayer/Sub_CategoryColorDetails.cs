using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("Sub_CategoryColorDetails")]
    public class Sub_CategoryColorDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? SubCategoryDetailsId { get; set; }
        public bool? SubCategoryIsColor { get; set; }
        public long? SubCategoryQuantity { get; set; }
        public string SubCategoryColorWiseSize { get; set; }
        public long? SubCategoryColorId { get; set; }
        public long? SubCategoryAvailableOfNoColor { get; set; }
        public string SubCategoryColorImagePath { get; set; }
        public bool? IsActive { get; set; }
        public long? CreadedBy { get; set; }
        public DateTime? CreadedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}