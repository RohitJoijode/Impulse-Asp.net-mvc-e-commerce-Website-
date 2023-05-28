using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("ORDERS")]
    public class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id {get;set;}
        public long? AddToCartUniqueId  {get;set;}
        public long? OrderId { get; set; }
        public long? OrderStatusId { get; set; }
        public long? PaymentMethodSelectionId { get; set; }
        public bool? IsPayment { get; set; }
        public bool? IsCancel { get; set; }
        public bool? IsActive { get; set; }
        public long? UserId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}