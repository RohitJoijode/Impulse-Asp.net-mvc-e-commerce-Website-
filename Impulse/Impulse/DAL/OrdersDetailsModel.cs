using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class OrdersDetailsModel
    {
        public long? Id { get; set; }
        public long? AddToCartUniqueId { get; set; }
        public long? OrderId { get; set; }
        public long? OrderDetailsId { get; set; }
        public long? PaymentMethodSelectionId { get; set; }
        public string PaymentMethodSelectionName { get; set; } // HERE ADDED ON (25-01-2022)
        public long? OrderStatusId { get; set; }
        public long? UserId { get; set; }
        public bool? IsPayment { get; set; }
        public long? SubCategoryId { get; set; }
        public string Sub_CategoryName { get; set; }
        public long? SubCategoryQauntity { get; set; }
        public Decimal? SubCategoryPrice { get; set; }
        public Decimal? SubCategoryTotalPrice { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public UsersModel UsersModel { get; set; }
    }
}