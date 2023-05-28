using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class WishListModel
    {
        public long? Id { get; set; }
        public long? WishListId { get; set; }
        public long? UserId { get; set; }
        public long? CategoryId { get; set; }
        public long? Sub_CategoryId { get; set; }
        public bool? IsWishList { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}