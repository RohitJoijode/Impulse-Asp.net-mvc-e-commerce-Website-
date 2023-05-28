using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class UsersModel
    {
         public long? id { get; set; }
         public long? UserId { get; set; }
         public string UserFirstName { get; set; }
         public string UserLastName { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public long? CountryId { get; set; }
        public long? UserPincode { get; set; } // here added New column on (26-01-2023)
        public int UserRole { get; set; }
         public string Email { get; set; }
         public string UserPassword { get; set; }
         public string UserConfirmPassword { get; set; }
         public string UserMobileNumber { get; set; }
         public int? UserStatus { get; set; }
         public bool? IsActive { get; set; }
         public long? CreatedBy { get; set; }
         public DateTime? CreatedDate { get; set; }
         public long? UpdatedBy { get; set; }
         public DateTime? UpdatedDate { get; set; }
         public bool? IsKeepMeUpToDateOnNews { get; set; }
         public string RegisterOTP { get; set; }
        public string AddToCartSubCategoryIdsForCookies { get; set; } //For testing Purpose
        public string WistListSubCategoryIdsForCookies { get; set; } //For testing Purpose
        public string WistListCount { get; set; } //For testing Purpose
        public string AddToCartCount { get; set; } //For testing Purpose
        public string OrderListCount { get; set; } //For testing Purpose
        public long? CategoryId { get; set; }
        public string UserName { get; set; }
        public string SearchText { get; set; }
        public long? CurrencyId { get; set; } //New Column Add On (20-01-2023)  
        public string CurrencyName { get; set; } //New Column Add On (20-01-2023)
        public string CurrencySymbol { get; set; } //New Column Add On (20-01-2023)  
        public long? SubCategoryColorId { get; set; } //New Column Add On (20-01-2023)  
        public long? SubCategoryId { get; set; } //New Column Add On (20-01-2023)  

        #region SP Missing Column Name
        public long? UserCode       {get;set;}
        public string UserEmail      {get;set;}
        public long? UserCoutryId   {get;set;}
        public long? UserStateId    {get;set;}
        public long? UserCityId     {get;set;}
        #endregion
        public List<WistListModel> WistList { get; set; }

    }

    public class OrdersPageModel
    {
        public UsersModel UsersModel { get; set; }
        public OrdersDetailsModel OrdersDetailsModel { get; set;}
        public List<OrdersDetailsModel> OrdersDetailsList { get; set; }
    }


}