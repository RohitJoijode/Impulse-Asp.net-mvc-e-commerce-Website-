using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Impulse.DBAccessLayer
{
    [Table("Sub_CategoryImage")]
    public class Sub_CategoryImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id  {get;set;}
        public long? SubCategoryImageId { get;set;}
        public long? SubcategoryImageColorId { get;set;}
        public string SubCategoryColorHexCode { get; set; }
        public decimal? Color_wise_prices { get; set; }
        public long? CategoryId     {get;set;}
        public long? SubCategoryId  {get;set;}
        public string SubCategoryImagePath { get;set;}
        public bool? IsActive    {get;set;}
        public long? CreatedBy   {get;set;}
        public DateTime? CreatedDate {get;set;}
        public long? UpdatedBy   {get;set;}
        public DateTime? UpdatedDate { get; set; }
    }
}