using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class JsonResponse
    {
        public string ResponseMessage { get; set; }
        public bool? IsSuccess { get; set; }
        public string  RegisterOTP { get; set; }
        public string GetUniqueId { get; set; }
        public string StringReponse { get; set; }
        public string WishListCookiesSubCategoryId { get; set; }
        public string AddToCartCount { get; set; }
        public string WishListCount { get; set; }
        public string WishListCookiesCount { get; set; }
        public string OrderListCount { get; set; }

    }
}