using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class AddToCartModel
    {
            public long? Id { get; set; }
            public long? AddToCartId { get; set; }
            public long? UserId { get; set; }
            public long? CategoryId { get; set; }
            public long? Sub_CategoryId { get; set; }
            public bool? IsAddToCart { get; set; }
            public bool? IsActive { get; set; }
            public DateTime? CreatedBy { get; set; }
            public long? CreatedDate { get; set; }
            public long? UpdatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
    }
}