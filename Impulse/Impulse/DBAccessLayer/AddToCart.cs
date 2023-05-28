using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Impulse.DBAccessLayer
{
    [Table("AddToCart")]
    public class AddToCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id {get;set;}
        public long? WistListUniqueId { get; set; }
        public long? AddToCartId { get; set; }
        public long? UserId     {get;set;}
        public long? CategoryId { get; set; }
        public long? Sub_CategoryId { get; set; }
        #region Here Added On (22-02-2023)
        public string Sub_CategoryImagePath { get; set; }
        public long? Quantity { get; set; }
        public decimal? Sub_Category_ColorWisePrice { get; set; }
        public decimal? Sub_Category_Price { get; set; }
        public long? Sub_Category_ColorId { get; set; }
        public long? Sub_Category_SizeId { get; set; }
        #endregion
        public bool? IsAddToCart    { get; set; }
        public bool? IsActive    { get; set; }
        public long? CreatedBy   { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public long? UpdatedBy   { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}