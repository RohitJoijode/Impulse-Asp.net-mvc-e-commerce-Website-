using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Impulse.DBAccessLayer
{
    [Table("SubCategorySizeDetails")]
    public class Sub_CategorySizeDetails
    {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Key, Column(Order = 1)]
            public  long  ID  {get;set;}
            public long? SubCategorySizeId   {get;set;}
            public long? SubCategorySizeStatusId  {get;set;}
            public long? SubCategoryColorImageId  {get;set;}
            public long? SubCategoryId {get;set;}
            public long? CategoryId   {get;set;}
            public long? SubCategoryTotalNoOfQUantity {get;set;}
            public  bool?   SubCategoryIsSize  {get;set;}
            public long? SubCategory_M_Quantity {get;set;}
            public long? SubCategory_L_Quantity {get;set;}
            public long? SubCategory_XL_Quantity {get;set;}
            public long? SubCategory_XXL_Quantity {get;set;}
            public long? SubCategory_XXXL_Quantity {get;set;}
            public long? SubCategroy_4XL_Quantity {get;set;}
            public long? SubCategory_6UK_Quantity {get;set;}
            public long? SubCategory_7UK_Quantity {get;set;}
            public long? SubCategory_8UK_Quantity {get;set;}
            public long? SubCategory_9UK_Quantity {get;set;}
            public long? SubCategory_10UK_Quantity {get;set;}
            public long? SubCategory_11UK_Quantity {get;set;}
            public long? SubCategory_12UK_Quantity {get;set;}
            public bool? SubCategoryIsColor   {get;set;}
            public string SubCategoryColorHexCode {get;set;}
            public long? SubCategoryColorId {get;set;}
            public bool? IsAvaliable   {get;set;}
            public bool? IsActive      {get;set;}
            public  long?   CreatedBy     {get;set;}
            public DateTime? CreatedDate   {get;set;}
            public long? UpdatedBy     {get;set;}
            public DateTime? UpdatedDate   { get; set; }
	}
}