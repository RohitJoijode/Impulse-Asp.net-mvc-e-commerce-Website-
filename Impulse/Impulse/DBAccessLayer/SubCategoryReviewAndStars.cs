using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Impulse.DBAccessLayer
{
    [Table("SubCategoryReviewAndStars")]
    public class SubCategoryReviewAndStars
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id   {get;set;}
        public long? Sub_CategoryId {get;set;}
        public long? CategoryId { get; set; }
        public long? ReviewId { get; set; }
        public long? GivenStars { get; set; }
        public string ReviewMessage { get; set; }
        public bool? IsActive { get; set; } 
        public long? UserId { get; set; }
        public long? CreadedBy { get; set; }
        public DateTime? CreadedDate { get; set; }
        public long? Updatedby { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}