using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Impulse.DAL;
using System.Collections;

namespace Impulse.BAL.Update
{
    public class UpdAdmin
    {
        public JsonResponse SaveCategory(CategoryModel CategoryModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Category ent2Save = null;
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Category.Where(x => x.CategoryId == CategoryModel.CategoryId && x.CategoryName == CategoryModel.CategoryName).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Category();
                            ent2Save.Id = AcqAdminObj.GetCategoryIdentityIncreamentId();
                            ent2Save.CategoryId = AcqAdminObj.GetCategoryIdIncreamentId();
                            if(CategoryModel.CategoryTypeId == null)
                            {
                                ent2Save.CategoryTypeId = AcqAdminObj.GetCategoryTypeIncreamentId();
                                ent2Save.CategoryType = CategoryModel.CategoryType;
                            } else
                            {
                                ent2Save.CategoryTypeId = CategoryModel.CategoryTypeId;
                                ent2Save.CategoryType = CategoryModel.CategoryType;
                            }
                            ent2Save.IsActive = CategoryModel.IsActive;
                            ent2Save.CreatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.CreatedDate = DateTime.Now;
                            ent2Save.CategoryName = CategoryModel.CategoryName;
                            isNew = true;
                        }
                        else
                        {
                            if (CategoryModel.CategoryTypeId == null)
                            {
                                ent2Save.CategoryTypeId = AcqAdminObj.GetCategoryTypeIncreamentId();
                                ent2Save.CategoryType = CategoryModel.CategoryType;
                            }
                            else
                            {
                                ent2Save.CategoryTypeId = CategoryModel.CategoryTypeId;
                                ent2Save.CategoryType = CategoryModel.CategoryType;
                            }
                            ent2Save.IsActive = CategoryModel.IsActive;
                            ent2Save.CategoryName = CategoryModel.CategoryName;
                            ent2Save.UpdatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isNew = false;
                        }
                        
                        ent2Save.CategoryName = CategoryModel.CategoryName;

                        if (isNew)
                        {
                            obj.Category.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdAdmin","SaveCategory", ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse SaveSubCategory(SubCategoryModel SubCategoryModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Sub_Category ent2Save = null;
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Sub_Category.Where(x => x.Id == SubCategoryModel.Id && x.Sub_CategoryId == SubCategoryModel.Sub_CategoryId).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Sub_Category();
                            ent2Save.Id = AcqAdminObj.GetSubCategoryIdentityIncreamentId();
                            ent2Save.Sub_CategoryId = AcqAdminObj.GetSubCategoryIdIncreamentId();
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.UpdatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isNew = false;
                        }
                        ent2Save.CategoryId = SubCategoryModel.CategoryId;
                        ent2Save.CategoryTypeId = SubCategoryModel.CategoryTypeId;
                        ent2Save.Sub_CategoryName = SubCategoryModel.Sub_CategoryName;
                        ent2Save.Sub_CategoryImagePath = SubCategoryModel.Sub_CategoryImagePath;
                        ent2Save.Sub_Category_Size = SubCategoryModel.Sub_Category_Size;
                        ent2Save.Sub_Category_Quantity = SubCategoryModel.Sub_Category_Quantity;
                        ent2Save.Sub_Category_Price = SubCategoryModel.Sub_Category_Price;
                        ent2Save.Sub_Category_StrickOutPrice = SubCategoryModel.Sub_Category_StrickOutPrice;
                        ent2Save.Sub_CategoryDescription = SubCategoryModel.Sub_CategoryDescription;
                        ent2Save.Sub_Category_IsTopDeal = SubCategoryModel.Sub_Category_IsTopDeal;
                        ent2Save.Sub_Category_IsRecommended = SubCategoryModel.Sub_Category_IsRecommended;
                        ent2Save.IsActive = SubCategoryModel.IsActive;
                        ent2Save.BrandId = SubCategoryModel.BrandId;
                        ent2Save.StockStatusId = SubCategoryModel.StockStatusId;
                        ent2Save.StockStatusName = SubCategoryModel.StockStatusName;
                        ent2Save.Sub_Category_IsSize = SubCategoryModel.Sub_Category_IsSize;
                        ent2Save.Sub_Category_IsColor = SubCategoryModel.Sub_Category_IsColor;
                        ent2Save.Sub_Category_DefaultColorId = SubCategoryModel.Sub_Category_DefaultColorId;
                        ent2Save.Sub_Category_AvailableNoOfColor = SubCategoryModel.Sub_Category_AvailableNoOfColor;
                        ent2Save.Sub_Category_ColorName = SubCategoryModel.Sub_Category_ColorName;
                        ent2Save.Sub_Category_IsDiscount = SubCategoryModel.Sub_Category_IsDiscount;
                        //Discount = Marked Price - Selling price
                        //Discount Percentage = (Discount / Marked Price) *100
                        var Discount = SubCategoryModel.Sub_Category_StrickOutPrice - SubCategoryModel.Sub_Category_Price;
                        var DiscountPercentage = ((Discount / SubCategoryModel.Sub_Category_StrickOutPrice) * 100);
                        ent2Save.Sub_Category_Discount_Percentage = Math.Round(Convert.ToDecimal(DiscountPercentage));
                        ent2Save.Sub_Category_IsDeliveryFree = SubCategoryModel.Sub_Category_IsDeliveryFree;
                        ent2Save.Sub_Category_DeliveryDescription = SubCategoryModel.Sub_Category_DeliveryDescription;
                        ent2Save.Sub_Category_IsCare_Instructions = SubCategoryModel.Sub_Category_IsCare_Instructions;
                        ent2Save.Sub_Category_Care_Instructions = SubCategoryModel.Sub_Category_Care_Instructions;
                        ent2Save.Sub_Category_IsOccasionType = SubCategoryModel.Sub_Category_IsOccasionType;
                        ent2Save.Sub_Category_OccasionType = SubCategoryModel.Sub_Category_OccasionType;
                        ent2Save.Sub_Category_IsClosureType = SubCategoryModel.Sub_Category_IsClosureType;
                        ent2Save.Sub_Category_ClosureType = SubCategoryModel.Sub_Category_ClosureType;
                        ent2Save.Sub_Category_IsItemWeight = SubCategoryModel.Sub_Category_IsItemWeight;
                        ent2Save.Sub_Category_ItemWeight = SubCategoryModel.Sub_Category_ItemWeight;
                        ent2Save.Sub_Category_IsItemPackageQuantity = SubCategoryModel.Sub_Category_IsItemPackageQuantity;
                        ent2Save.Sub_Category_ItemPackageQuantity = SubCategoryModel.Sub_Category_ItemPackageQuantity;
                        ent2Save.Sub_Category_IsNoOfItem = SubCategoryModel.Sub_Category_IsNoOfItem;
                        ent2Save.Sub_Categor_NoOfItem = SubCategoryModel.Sub_Categor_NoOfItem;
                        ent2Save.Sub_Category_IsRiseStyle = SubCategoryModel.Sub_Category_IsRiseStyle;
                        ent2Save.Sub_Category_RiseStyle = SubCategoryModel.Sub_Category_RiseStyle;
                        ent2Save.Sub_Category_IsStrapType = SubCategoryModel.Sub_Category_IsStrapType;
                        ent2Save.Sub_category_StrapType = SubCategoryModel.Sub_category_StrapType;
                        ent2Save.Sub_Category_IsItemForm = SubCategoryModel.Sub_Category_IsItemForm;
                        ent2Save.Sub_category_ItemForm = SubCategoryModel.Sub_category_ItemForm;
                        ent2Save.Sub_Category_IsPaperFinish = SubCategoryModel.Sub_Category_IsPaperFinish;
                        ent2Save.Sub_Category_PaperFinish = SubCategoryModel.Sub_Category_PaperFinish;
                        ent2Save.Sub_Category_IsNetQuantity = SubCategoryModel.Sub_Category_IsNetQuantity;
                        ent2Save.Sub_Category_NetQuantity = SubCategoryModel.Sub_Category_NetQuantity;
                        ent2Save.Sub_Category_IsNeckStyle = SubCategoryModel.Sub_Category_IsNeckStyle;
                        ent2Save.Sub_Category_NeckStyle = SubCategoryModel.Sub_Category_NeckStyle;
                        ent2Save.Sub_Category_IsStyle = SubCategoryModel.Sub_Category_IsStyle;
                        ent2Save.Sub_Category_Style = SubCategoryModel.Sub_Category_Style;
                        ent2Save.Sub_Category_IsOrigin = SubCategoryModel.Sub_Category_IsOrigin;
                        ent2Save.Sub_Category_Origin = SubCategoryModel.Sub_Category_Origin;
                        ent2Save.IsDisplayHomePage = SubCategoryModel.IsDisplayHomePage;
                        if (isNew)
                        {
                            obj.Sub_Category.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdAdmin","SaveSubCategory",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse SaveSubCategoryImage(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Sub_CategoryImage ent2Save = null;
            DBAccessLayer.Color ent2Color = null;
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Sub_CategoryImage.Where(x => x.Id == Sub_CategoryImageModel.Id && x.SubCategoryId == Sub_CategoryImageModel.SubCategoryId && x.SubCategoryImageId == Sub_CategoryImageModel.SubCategoryImageId).FirstOrDefault();
                        ent2Color = obj.Color.Where(x => x.ColorId == Sub_CategoryImageModel.SubcategoryImageColorId && x.IsActive == true).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Sub_CategoryImage();
                            ent2Save.Id = AcqAdminObj.GetSubCategoryImageIdentityIncreamentId();
                            ent2Save.SubCategoryImageId = AcqAdminObj.GetSubCategoryImageIdIncreamentId(Sub_CategoryImageModel.SubCategoryId);
                            ent2Save.CreatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.IsActive = Sub_CategoryImageModel.IsActive;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.UpdatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.UpdatedDate = DateTime.Now;
                            ent2Save.IsActive = Sub_CategoryImageModel.IsActive;
                            isNew = false;
                        }
                        ent2Save.SubcategoryImageColorId = Sub_CategoryImageModel.SubcategoryImageColorId;
                        ent2Save.SubCategoryColorHexCode = ent2Color.ColorHexCode;
                        ent2Save.CategoryId = Sub_CategoryImageModel.CategoryId;
                        ent2Save.SubCategoryId = Sub_CategoryImageModel.SubCategoryId;
                        ent2Save.Color_wise_prices = Sub_CategoryImageModel.Color_wise_prices;
                        ent2Save.SubCategoryImagePath = Sub_CategoryImageModel.SubCategoryImagePath;
                        if (isNew)
                        {
                            obj.Sub_CategoryImage.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdAdmin","SaveSubCategoryImage",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse SaveSideSubCategoryImage(SubCategoryImageList SubCategorySideImageModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Sub_CategoryImageListDB ent2Save = null;
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Sub_CategoryImageList.Where(
                                                                        x => x.Id == SubCategorySideImageModel.Id 
                                                                        &&  x.Sub_CategoryImageId == SubCategorySideImageModel.Sub_CategoryImageId 
                                                                        && x.Sub_CategoryImageColorId == SubCategorySideImageModel.SubCategoryImageColorId
                                                                        && x.CategoryId == SubCategorySideImageModel.CategoryId
                                                                        && x.SubCategoryId == SubCategorySideImageModel.SubCategoryId
                                                                    ).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Sub_CategoryImageListDB();
                            ent2Save.Id = AcqAdminObj.GetSubCategorySideImageIdentityIncreamentId();
                            ent2Save.Sub_CategoryImageId = SubCategorySideImageModel.Sub_CategoryImageId;
                            ent2Save.CategoryId = SubCategorySideImageModel.CategoryId;
                            ent2Save.SubCategoryId = SubCategorySideImageModel.SubCategoryId;
                            ent2Save.CreatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.IsActive = SubCategorySideImageModel.IsActive;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.Sub_CategoryImageId = SubCategorySideImageModel.Sub_CategoryImageId;
                            ent2Save.CategoryId = SubCategorySideImageModel.CategoryId;
                            ent2Save.SubCategoryId = SubCategorySideImageModel.SubCategoryId;
                            ent2Save.UpdatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.UpdatedDate = DateTime.Now;
                            ent2Save.IsActive = SubCategorySideImageModel.IsActive;
                            isNew = false;
                        }
                        ent2Save.Sub_CategoryImagePath = SubCategorySideImageModel.SubCategoryImagePath;
                        ent2Save.Sub_CategoryImageColorId = SubCategorySideImageModel.SubCategoryImageColorId;
                        if (isNew)
                        {
                            obj.Sub_CategoryImageList.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdAdmin","SaveSideSubCategoryImage",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse SaveSubCategorySizeDetails(Sub_CategorySizeDetailsModel Sub_CategorySizeDetailsModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Sub_CategorySizeDetails ent2Save = null;
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Sub_CategorySizeDetails.Where(
                                                                        x => x.ID == Sub_CategorySizeDetailsModel.ID
                                                                        && x.SubCategoryColorImageId == Sub_CategorySizeDetailsModel.SubCategoryColorImageId
                                                                        && x.CategoryId == Sub_CategorySizeDetailsModel.CategoryId
                                                                        && x.SubCategoryId == Sub_CategorySizeDetailsModel.SubCategoryId
                                                                        && x.SubCategorySizeId == Sub_CategorySizeDetailsModel.SubCategorySizeId
                                                                    ).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Sub_CategorySizeDetails();
                            ent2Save.ID = AcqAdminObj.GetSubCategorySizeDetailsIdentityIncreamentId();
                            ent2Save.SubCategorySizeId = Sub_CategorySizeDetailsModel.SubCategorySizeId;//AcqAdminObj.GetSubCategorySizeId();
                            ent2Save.SubCategoryColorImageId = Sub_CategorySizeDetailsModel.SubCategoryColorImageId;
                            ent2Save.CategoryId = Sub_CategorySizeDetailsModel.CategoryId;
                            ent2Save.SubCategoryId = Sub_CategorySizeDetailsModel.SubCategoryId;
                            ent2Save.CreatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.IsActive = Sub_CategorySizeDetailsModel.IsActive;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.SubCategoryColorImageId = Sub_CategorySizeDetailsModel.SubCategoryColorImageId;
                            ent2Save.CategoryId = Sub_CategorySizeDetailsModel.CategoryId;
                            ent2Save.SubCategoryId = Sub_CategorySizeDetailsModel.SubCategoryId;
                            ent2Save.UpdatedBy = (long?)DbEnum.Role.Admin;
                            ent2Save.UpdatedDate = DateTime.Now;
                            ent2Save.IsActive = Sub_CategorySizeDetailsModel.IsActive;
                            isNew = false;
                        }

                        ent2Save.SubCategorySizeStatusId = ent2Save.SubCategorySizeStatusId;
                        ent2Save.SubCategoryIsSize = Sub_CategorySizeDetailsModel.SubCategoryIsSize;
                        ent2Save.SubCategory_M_Quantity = Sub_CategorySizeDetailsModel.SubCategory_M_Quantity;
                        ent2Save.SubCategory_L_Quantity = Sub_CategorySizeDetailsModel.SubCategory_L_Quantity;
                        ent2Save.SubCategory_XL_Quantity = Sub_CategorySizeDetailsModel.SubCategory_XL_Quantity;
                        ent2Save.SubCategory_XXL_Quantity = Sub_CategorySizeDetailsModel.SubCategory_XXL_Quantity;
                        ent2Save.SubCategory_XXXL_Quantity = Sub_CategorySizeDetailsModel.SubCategory_XXXL_Quantity;
                        ent2Save.SubCategroy_4XL_Quantity = Sub_CategorySizeDetailsModel.SubCategroy_4XL_Quantity;
                        ent2Save.SubCategory_6UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_6UK_Quantity;
                        ent2Save.SubCategory_7UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_7UK_Quantity;
                        ent2Save.SubCategory_8UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_8UK_Quantity;
                        ent2Save.SubCategory_9UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_9UK_Quantity;
                        ent2Save.SubCategory_10UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_10UK_Quantity;
                        ent2Save.SubCategory_11UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_11UK_Quantity;
                        ent2Save.SubCategory_12UK_Quantity = Sub_CategorySizeDetailsModel.SubCategory_12UK_Quantity;
                        ent2Save.SubCategoryIsColor = Sub_CategorySizeDetailsModel.SubCategoryIsColor;
                        ent2Save.SubCategoryColorId = Sub_CategorySizeDetailsModel.SubCategoryColorId;
                        ent2Save.IsAvaliable = Sub_CategorySizeDetailsModel.IsAvaliable;
                        if (isNew)
                        {
                            obj.Sub_CategorySizeDetails.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdAdmin","Sub_CategorySizeDetails", ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

    }
}