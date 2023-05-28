using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("WishList")]
    public class WishList
    {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Key,Column(Order = 1)]
            public long Id {get;set;}
            public long? WishListId { get; set; }
            public long? UserId {get;set;}
            public long? CategoryId { get; set; }
            public long? Sub_CategoryId { get; set; }
            #region Here added On (22-02-2023)
            public long? Quantity { get; set; }
            public string Sub_CategoryImagePath { get; set; }
            public decimal? Sub_Category_ColorWisePrice { get; set; }
            public decimal? Sub_Category_Price { get; set; }
            public long? Sub_Category_ColorId { get; set; }
            public long? Sub_Category_SizeId { get; set; }
            #endregion
            public bool? IsWishList { get; set; }
            public bool?  IsActive { get; set; }
            public long? CreatedBy { get; set; }
            public DateTime? CreatedDate { get; set; }
            public long? UpdatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
     }
}