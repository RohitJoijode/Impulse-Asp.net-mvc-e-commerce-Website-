using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Impulse.DBAccessLayer
{
    [Table("Sub_Category")]
    public class Sub_Category
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public long Id  {get;set;}
        public long? CategoryId {get;set;}
        public long? CategoryTypeId {get;set;}
        public long? UserId    {get;set;}
        public long? Sub_CategoryId  {get;set;}
        public string Sub_CategoryName {get;set;}
        public string Sub_CategoryImagePath   {get;set;}
        public string Sub_Category_Size   {get;set;}
        public long? Sub_Category_Stars   {get;set;}
        public long? Sub_Category_Quantity {get;set;}
        public Decimal? Sub_Category_Price {get;set;}
        public Decimal? Sub_Category_StrickOutPrice { get; set; }
        public string Sub_CategoryDescription {get;set;}
        public bool? Sub_Category_IsTopDeal   {get;set;}
        public bool? Sub_Category_IsRecommended { get; set; }
        public bool? IsActive   {get;set;}
        public long? CreatedBy  {get;set;}
        public DateTime? CreatedDate {get;set;}
        public long? UpdatedBy    {get;set;}
        public DateTime? UpdatedDate  { get; set; }
        public long? BrandId { get; set; } //New Column Add On (20-01-2023)
        public long? StockStatusId { get; set; } //New Column Add On (20-01-2023)
        public string StockStatusName { get; set; } //New Column Add On (20-01-2023)  
        public long? CurrencyId { get; set; } //New Column Add On (20-01-2023)  
        public string CurrencyName { get; set; } //New Column Add On (20-01-2023)  
        public string CurrencySymbol { get; set; } //New Column Add On (20-01-2023)  
        public bool? Sub_Category_IsSize { get; set; } //New Column Add On (24-01-2023)  
        public bool? Sub_Category_IsColor { get; set; } //New Column Add On (24-01-2023)  
        public long? Sub_Category_DefaultColorId { get; set; } //New Column Add On (28-01-2023)
        public long? Sub_Category_AvailableNoOfColor { get; set; } //New Column Add On (28-01-2023)
        public string Sub_Category_ColorName { get; set; } //New Column Add On (24-01-2023)  
        public bool? Sub_Category_IsStrickOutPrice { get; set; } //New Column Add On (22-02-2023)  
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
        public bool? IsDisplayHomePage { get; set; }
        
        #endregion
    }
}