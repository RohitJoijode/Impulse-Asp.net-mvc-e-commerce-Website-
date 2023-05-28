using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("OrdersDetails")]
    public class OrdersDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key,Column(Order = 1)]
        public long Id  {get;set;}
        public long? AddToCartUniqueId {get;set;}
        public long? OrderId { get; set; }
        public long? OrderDetailsId { get; set; }
        public long? UserId { get; set; }
        public long? SubCategoryId { get; set; }
        public Decimal? SubCategoryQauntity { get; set; }
        public Decimal? SubCategoryPrice { get; set; }
        public Decimal?  SubCategoryTotalPrice { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}