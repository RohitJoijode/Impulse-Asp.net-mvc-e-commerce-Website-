using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DAL
{
    public class SubCategoryModel
    {
        public long? Id { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; } //Add SP Column On (09-02-2023)
        public long? CategoryTypeId { get; set; }
        public string CategoryTypeName { get; set; } //Add SP Column On (09-02-2023)
        public long? UserId { get; set; }
        public string UserName { get; set; } //Add SP Column On (09-02-2023)
        public long? Sub_CategoryId { get; set; }
        public string EncryptedSub_CategoryId { get; set; }
        public string EncryptedSub_Category_DefaultColorId { get; set; }
        public string Sub_CategoryName { get; set; }
        public string Sub_CategoryImagePath { get; set; } //Get Image
        public string Sub_Category_Size { get; set; }
        public long? Sub_Category_SizeId { get; set; }
        public bool? Sub_Category_IsSize { get; set; }
        public bool? Sub_Category_IsColor { get; set; }
        public long? Sub_Category_DefaultColorId { get; set; }
        public string Sub_Category_DefaultColorName { get; set; } // Here added SP  New Column for (29-01-2023) // For Showing Default_ColorName 
        public long? Sub_Category_AvailableNoOfColor { get; set; }
        public bool? Sub_Category_IsInclusiveTax { get; set; } // Here Add SP column On (09-02-2023)
        public long? Sub_Category_Stars { get; set; }
        public long? SelectedQuantity { get; set; }
        public long? Sub_Category_Quantity { get; set; }
        public Decimal? Sub_Category_Price { get; set; }
        public Decimal? Sub_Category_StrickOutPrice { get; set; }
        public string Sub_CategoryDescription { get; set; }
        //public bool? Sub_Category_IsWistList { get; set; } //Remove From Table
        //public bool? Sub_Category_IsAddToCart { get; set; } //Remove From Table
        public bool? Sub_Category_IsTopDeal { get; set; }
        public bool? Sub_Category_IsRecommended { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SubCategoryId { get; set; }
        public string DeletedSubCategoryId { get; set; }
        public string UniqueId { get; set; }

        #region New Column Add On (20-01-2023)
        //public string BrandName { get; set; } //Remove From table
        public long? BrandId { get; set; }
        //public string ItemColorName { get; set; } //Remove From table
        public long? StockStatusId { get; set; }
        public string StockStatusName { get; set; }
        public string CurrencySymbol { get; set; }
        public long? SubCategoryQauntity { get; set; }
        public Decimal? SubCategoryPrice { get; set; }
        public Decimal? SubCategoryTotalPrice { get; set; }
        public string Sub_Category_ColorName { get; set; }
        #endregion

        #region New Column Add On (28-01-2023)  
        public bool? Sub_Category_IsDiscount { get; set; }
        public Decimal? Sub_Category_Discount_Percentage { get; set; }
        public bool? Sub_Category_IsDeliveryFree { get; set; }
        public string Sub_Category_DeliveryDescription { get; set; }
        #endregion

        #region New Column Added On (30-01-2023)
        public bool? Sub_Category_IsCare_Instructions { get; set; }
        public string Sub_Category_Care_Instructions { get; set; }
        public bool? Sub_Category_IsOccasionType { get; set; }
        public string Sub_Category_OccasionType { get; set; }
        public bool? Sub_Category_IsClosureType { get; set; }
        public string Sub_Category_ClosureType { get; set; }
        public bool? Sub_Category_IsItemWeight { get; set; }
        public string Sub_Category_ItemWeight { get; set; }
        public bool? Sub_Category_IsItemPackageQuantity { get; set; }
        public string Sub_Category_ItemPackageQuantity { get; set; }
        public bool? Sub_Category_IsNoOfItem { get; set; }
        public string Sub_Categor_NoOfItem { get; set; }
        public bool? Sub_Category_IsRiseStyle { get; set; }
        public string Sub_Category_RiseStyle { get; set; }
        public bool? Sub_Category_IsStrapType { get; set; }
        public string Sub_category_StrapType { get; set; }
        public bool? Sub_Category_IsItemForm { get; set; }
        public string Sub_category_ItemForm { get; set; }
        public bool? Sub_Category_IsPaperFinish { get; set; }
        public string Sub_Category_PaperFinish { get; set; }
        public bool? Sub_Category_IsNetQuantity { get; set; }
        public string Sub_Category_NetQuantity { get; set; }
        public bool? Sub_Category_IsNeckStyle { get; set; }
        public string Sub_Category_NeckStyle { get; set; }
        public bool? Sub_Category_IsStyle { get; set; }
        public string Sub_Category_Style { get; set; }
        public bool? Sub_Category_IsOrigin { get; set; }
        public string Sub_Category_Origin { get; set; }
        #endregion

        #region New Column Added On (04-02-2023)
        public long? ReivewCount { get; set; } //SubCategoryStarsCount
        public long? SubCategoryOrdersCount { get; set; }
        #endregion

        #region New Column Added On (06-02-2023)
        public long? SubCategorySelectedImageId { get; set; }
        public bool? Sub_Category_IsDeliveryPinCheck { get; set; }
        public long? Sub_Category_DeliveryPinCodeCheck { get; set; }
        #endregion
        public bool? Sub_Category_IsStrickOutPrice { get;set;}
        public bool? Sub_Category_IsBrand { get; set; }
        public string BrandName { get; set; }
        public bool? Sub_Category_IsStockStatus { get; set; }
        public bool? IsDisplayHomePage { get; set; }
        public long? AvgStar { get; set; }
        public long? WishListUniqueId { get; set; }
        public long? AddToCartUniqueId { get; set; }
        public HttpPostedFileWrapper UploadImageId { get; set; }
    }

    public class SubCategoryObject
    {
     public UsersModel UsersModel { get; set; }
      public List<SubCategoryModel> SubCategoryList { get; set; }
      public SubCategoryModel SubCategoryModel { get; set; }
      public List<CategoryModel> CategoryList { get; set; }
      public CategoryModel CategoryModel { get; set; }
      public Sub_CategoryColorDetailsModel Sub_CategoryColorDetailsModel { get; set; }
      public List<Sub_CategoryColorDetailsModel> SubCategoryColorDetailsList { get; set; }
      public List<SubCategoryKeyWordsModel> SubCategoryKeyWordsList { get; set; }
      public SubCategoryKeyWordsModel SubCategoryKeyWordsModel { get; set; }
      public List<SubCategoryAboutItem> SubCategoryAboutItemList { get; set; }
      public List<SubCategoryImageList> SubCategoryImageList { get; set; }
      public SubCategoryImageList SubCategorySideImageModel { get; set; }
      public Sub_CategoryImageModel Sub_CategoryImageModel { get; set; }
      public List<Sub_CategoryImageModel> Sub_CategoryImageModelList { get; set; }
      public List<SubCategoryReviewAndStarsModel> SubCategoryReviewAndStarsList { get; set; }
      public List<Sub_CategorySizeDetailsModel> Sub_CategorySizeDetailsList { get; set; }
      public Sub_CategorySizeDetailsModel SubCategorySizeDetailsModel { get; set; }
      public GetSubCategoryReviewAndStarsChartModel GetSubCategoryReviewAndStarsChartModel { get; set; }
     public List<GetSubCategoryReviewAndStarsChartModel> GetSubCategoryReviewAndStarsChartModelList { get; set; }
      public List<WistListModel> WistListList { get; set; }
      public WistListModel WistListModel { get; set; }
      public List<AddToCartModel1> AddToCartList { get; set; }
      public AddToCartModel1 AddToCartModel1 { get; set; }
      public List<SizeModel> SizeList { get; set; }
      public long? SizeId  { get; set; }
      public long? SelectedStarCount { get; set; }
      public long? SubCategoryImageId { get; set; }
     public long? CategoryId { get; set; }
     public long? SubCategoryId { get; set; }
     public long? SubcategoryImageColorId { get; set; }
     public bool? IsReviewEdit { get; set; }
    public long? userId { get; set; }
    public JsonResponse Response { get; set; }
    }

}