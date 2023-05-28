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

namespace Impulse.BAL.Access
{
    public class AcqCategory
    {
        public static List<SubCategoryModel> GetTopDealsSubCategoryList()
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                
                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetTopDealsSubCategoryList").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetTopDealsSubCategoryList",ex.Message.ToString(), 0);
            }
            return SubCategoryList;
        }
        public List<SubCategoryModel> GetHomePageSubCategoryData()
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetHomePageSubCategoryData").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetHomePageSubCategoryData",ex.Message.ToString(),0);
            }
            return SubCategoryList;
        }
        public List<CategoryModel> GetHomePageCategoryData()
        {
            List<CategoryModel> SubCategoryList = new List<CategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SubCategoryList = DbEngineObj.Database.SqlQuery<CategoryModel>("GetHomePageCategoryData").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetHomePageCategoryData", ex.Message.ToString(), 0);
            }
            return SubCategoryList;
        }
        public List<SubCategoryModel> SearchFilterOnTopDealsPage(Decimal? Min,Decimal? Max,long? Stars)
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@min",Min),
                                                new SqlParameter("@max",Max),
                                                new SqlParameter("@Stars",Stars),
                                            };

                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("SearchFilterOnTopDealsPage @min,@max,@Stars", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","SearchFilterOnTopDealsPage",ex.Message.ToString(),0);
            }
            return SubCategoryList;
        }
        public List<SubCategoryModel> GetIsRecommendSubCategoryList()
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetIsRecommendSubCategoryList").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetIsRecommendSubCategoryList",ex.Message.ToString(),0);
            }
            return SubCategoryList;
        }
        public string GetTopDealsSubCategorystring(List<SubCategoryModel> SubCategoryList)
        {
            string Reponse = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                
                sb.AppendLine("<div class='container'>");
                sb.AppendLine("<header class='section-heading'>");
                sb.AppendLine("<h3 class='section-title'" + ">Top Deals</h3>");
                sb.AppendLine("</header>");
                sb.AppendLine("<div class='row'>");
                for(var index = 0;index < SubCategoryList.Count();index++)
                {
                    sb.AppendLine("<div class='col-lg-3 col-md-6 col-sm-6'>");
                    sb.AppendLine("<figure class='card card-product-grid'" + ">");
                    sb.AppendLine("<div class='img-wrap'>");
                    sb.AppendLine("<img src ='" + SubCategoryList[index].Sub_CategoryImagePath + "'>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<figcaption class='info-wrap border-top'>");
                    sb.AppendLine("<div class='price-wrap'>");
                    sb.AppendLine("<span class='price'>"+ "₹" + " " + SubCategoryList[index].Sub_Category_Price + "</span>");
                    sb.AppendLine("</div>");
                    #region Color Activation Code
                    if (SubCategoryList[index].Sub_Category_IsColor == true)
                    {
                        sb.Append("<div>");
                        sb.Append("<strong>Available Colors</strong>");
                        sb.Append("</div>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Yellow' name='colors' value='Yellow'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Green' name='colors' value='Green'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='auto' name='colors' value='auto'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Red' name='colors' value='Red'>");
                    }
                    #endregion

                    sb.AppendLine("<a href='/Home/SubcategoryPage?SubcategoryId= " + SubCategoryList[index].Sub_CategoryId + " ' class='title mb-2'>" + SubCategoryList[index].Sub_CategoryDescription + "</a>");
                    sb.AppendLine("<a  class='btn btn-primary' onClick='OnClickAddToCart("+ SubCategoryList[index].Sub_CategoryId + ","+ SubCategoryList[index].CategoryId + ");'>Add to cart</a>");
                    sb.AppendLine("<a  class='btn btn-light btn-icon' onClick='OnClickWishList(" + SubCategoryList[index].Sub_CategoryId + "," + SubCategoryList[index].CategoryId  + ");'> <i class='fa fa-heart'></i></a>");
                    sb.AppendLine("</figcaption>");
                    sb.AppendLine("</figure>");
                    sb.AppendLine("</div>");
                }
                sb.AppendLine("</div>");
                sb.AppendLine("<div class='row'>");
                sb.AppendLine("<div class='text-end'>");
                sb.AppendLine("<a href='/Home/categoryPage?categoryId=" + (int?)6 + " ' class='btn btn-primary'>See More</a>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                Reponse = sb.ToString();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetTopDealsSubCategorystring",ex.Message.ToString(),0);
            }
            return Reponse;
        }
        /// <summary>
        /// Here Method Replace With Partial View 
        /// </summary>
        /// <param name="SubCategoryList"></param>
        /// <returns></returns>
        public string GetTopDealsSubCategoryListOnTopDealPageString(List<SubCategoryModel> SubCategoryList)
        {
            string Reponse = "";
            StringBuilder sb = new StringBuilder();
            try
            {

                sb.AppendLine("<div class='container'>");
                sb.AppendLine(" <header class='d - sm - flex align - items - center border - bottom mb-4pb-3'>");
                sb.AppendLine("<strong class='d-block py-2'>" + SubCategoryList.Count + " Items found</strong>");
                sb.AppendLine("</header>");
                sb.AppendLine("<div class='row'>");
                for (var index = 0; index < SubCategoryList.Count(); index++)
                {
                    sb.AppendLine("<div class='col-lg-4 col-md-6 col-sm-6'>");
                    sb.AppendLine("<figure class='card card-product-grid'" + ">");
                    sb.AppendLine("<div class='img-wrap'>");
                    sb.AppendLine("<img src ='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "'>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<figcaption class='info-wrap border-top'>");
                    sb.AppendLine("<div class='price-wrap'>");
                    sb.AppendLine("<span class='price'>" + "₹" + " " + SubCategoryList[index].Sub_Category_Price + "</span>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<p class='title mb-2'>" + SubCategoryList[index].Sub_CategoryDescription + "</p>");
                    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryList[index].Sub_CategoryId + "," + SubCategoryList[index].CategoryId + ");'>Add to cart</a>");
                    sb.AppendLine("<a class='btn btn-light btn-icon' onClick='OnClickWishList(" + SubCategoryList[index].Sub_CategoryId + "," + SubCategoryList[index].CategoryId + ");'> <i class='fa fa-heart'></i></a>");
                    sb.AppendLine("</figcaption>");
                    sb.AppendLine("</figure>");
                    sb.AppendLine("</div>");
                }
                sb.AppendLine("</div>");
                Reponse = sb.ToString();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory", "GetTopDealsSubCategorystring", ex.Message.ToString(), 0);
            }
            return Reponse;
        }
        public string GetRecommendSubCategorystring(List<SubCategoryModel> SubCategoryList)
        {
            string Reponse = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine("<div class='container'>");
                sb.AppendLine("<header class='section-heading'>");
                sb.AppendLine("<h3 class='section-title'" + ">Recommend</h3>");
                sb.AppendLine("</header>");
                sb.AppendLine("<div class='row'>");
                for (var index = 0; index < SubCategoryList.Count(); index++)
                {
                    sb.AppendLine("<div class='col-lg-3 col-md-6 col-sm-6'>");
                    sb.AppendLine("<figure class='card card-product-grid'" + ">");
                    sb.AppendLine("<div class='img-wrap'>");
                    sb.AppendLine("<img src='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "'>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<figcaption class='info-wrap border-top'>");
                    sb.AppendLine("<div class='price-wrap'>");
                    sb.AppendLine("<span class='price'>" + "₹" + " " + SubCategoryList[index].Sub_Category_Price + "</span>");
                    sb.AppendLine("</div>");

                    #region Color Activation Code
                    if (SubCategoryList[index].Sub_Category_IsColor == true)
                    {
                        sb.Append("<div>");
                        sb.Append("<strong>Available Colors</strong>");
                        sb.Append("</div>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Yellow' name='colors' value='Yellow'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Green' name='colors' value='Green'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='auto' name='colors' value='auto'>");
                        sb.Append("<input type='radio' style='margin: 3px;' id='Red' name='colors' value='Red'>");
                    }
                    #endregion


                    sb.AppendLine("<p class='title mb-2'>" + SubCategoryList[index].Sub_CategoryDescription + "</p>");
                    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryList[index].Sub_CategoryId + "," + SubCategoryList[index].CategoryId + ");'>Add to cart</a>");
                    sb.AppendLine("<a class='btn btn-light btn-icon' onClick='OnClickWishList(" + SubCategoryList[index].Sub_CategoryId + "," + SubCategoryList[index].CategoryId + ");'> <i class='fa fa-heart'></i></a>");
                    sb.AppendLine("</figcaption>");
                    sb.AppendLine("</figure>");
                    sb.AppendLine("</div>");
                }
                sb.AppendLine("</div>");
                sb.AppendLine("<div class='row'>");
                sb.AppendLine("<div class='text-end'>");
                sb.AppendLine("<a href='/Home/categoryPage?categoryId=" + (int?)7 + " ' class='btn btn-primary'>See More</a>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                Reponse = sb.ToString();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetRecommendSubCategorystring",ex.Message.ToString(),0);
            }
            return Reponse;
        }
        public long GetAddToCartIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.AddToCart.Max(s => (long?)s.Id) == null) ? 0 : obj.AddToCart.Max(s => s.Id);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetReviewIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.SubCategoryReviewAndStars.Max(s => (long?)s.Id) == null) ? 0 : obj.SubCategoryReviewAndStars.Max(s => s.Id);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetReviewIncreamentIdByUserId(long? UserId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.SubCategoryReviewAndStars.Where(s => s.UserId == UserId).Max(s => s.ReviewId) == null) ? 0 : obj.SubCategoryReviewAndStars.Where(s => s.UserId == UserId).Max(s => s.ReviewId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetOrderIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Orders.Max(s => (long?)s.Id) == null) ? 0 : obj.Orders.Max(s => s.Id);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetOrderIncreamentIdByUserId(long? UserId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Orders.Where(s => s.UserId == UserId).Max(s => s.OrderId) == null) ? 0 : obj.Orders.Where(s => s.UserId == UserId).Max(s => s.OrderId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetOrderDetailsIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.OrdersDetails.Max(s => (long?)s.Id) == null) ? 0 : obj.OrdersDetails.Max(s => s.Id);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetOrderDetailsIncreamentId(long? UserId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.OrdersDetails.Where(s => s.UserId == UserId).Max(s => s.OrderDetailsId) == null) ? 0 : obj.OrdersDetails.Where(s => s.UserId == UserId).Max(s => s.OrderDetailsId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetAddToCartIdIncreamentId(long? UserId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.AddToCart.Where(s => s.UserId == UserId).Max(s => s.AddToCartId) == null) ? 0 : obj.AddToCart.Where(s => s.UserId == UserId).Max(s => s.AddToCartId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetWishListIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.WishList.Max(s => (long?)s.Id) == null) ? 0 : obj.WishList.Max(s => s.Id);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetWishListIdIncreamentId(long? UserId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.WishList.Where(s => s.UserId == UserId).Max(s => s.WishListId) == null) ? 0 : obj.WishList.Where(s => s.UserId == UserId).Max(s => s.WishListId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public JsonResponse GetCookiesWistListOnWistListPage(List<WistListModel> WistList)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            //SubCategoryModel WistListSubCategoryDetails = new SubCategoryModel();
            List<SubCategoryModel> WistListSubCategoryDetailsList = new List<SubCategoryModel>();
            try
            {
                for(var Index = 0; Index < WistList.Count();Index++)
                {
                    long? SubCategoryId = Convert.ToInt64(WistList[Index].Sub_CategoryId);
                    var filteredResult = DbEngineObj.Sub_Category.Where(s => s.Sub_CategoryId == SubCategoryId && s.IsActive == true).OrderBy(s => s.Sub_CategoryName).FirstOrDefault();
                    if(filteredResult != null)
                    {
                        var SubCategoryModel = new SubCategoryModel();
                        SubCategoryModel.WishListUniqueId = WistList[Index].WishListCookiesId;
                        //SubCategoryModel.Sub_CategoryId = filteredResult.Sub_CategoryId;
                        //SubCategoryModel.CategoryId = filteredResult.CategoryId;
                        //SubCategoryModel.Sub_CategoryName = filteredResult.Sub_CategoryName;
                        //SubCategoryModel.Sub_CategoryDescription = filteredResult.Sub_CategoryDescription;
                        //SubCategoryModel.Sub_CategoryImagePath = filteredResult.Sub_CategoryImagePath;
                        //SubCategoryModel.Sub_Category_Quantity = filteredResult.Sub_Category_Quantity;
                        //SubCategoryModel.Sub_Category_Stars = filteredResult.Sub_Category_Stars;
                        //SubCategoryModel.Sub_Category_Price = filteredResult.Sub_Category_Price;
                        //SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsStrickOutPrice;
                        //SubCategoryModel.Sub_Category_StrickOutPrice = filteredResult.Sub_Category_StrickOutPrice;
                        //SubCategoryModel.Sub_Category_IsSize = filteredResult.Sub_Category_IsSize;
                        //SubCategoryModel.Sub_Category_Size = GetAvaliableSizeBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                        //SubCategoryModel.Sub_Category_SizeId = GetAvaliableSizeIdBySizeName(SubCategoryModel.CategoryId, SubCategoryModel.Sub_Category_Size);
                        //SubCategoryModel.Sub_Category_IsColor = filteredResult.Sub_Category_IsColor;
                        //SubCategoryModel.Sub_Category_DefaultColorId = filteredResult.Sub_Category_DefaultColorId;
                        //SubCategoryModel.Sub_Category_DefaultColorName = filteredResult.Sub_Category_ColorName;
                        //SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsItemForm;
                        //SubCategoryModel.Sub_Category_IsDeliveryFree = filteredResult.Sub_Category_IsDeliveryFree;
                        //SubCategoryModel.AvgStar = GetSubcategoryRatingBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                        SubCategoryModel.Sub_CategoryId = filteredResult.Sub_CategoryId;
                        SubCategoryModel.CategoryId = filteredResult.CategoryId;
                        SubCategoryModel.Sub_CategoryName = filteredResult.Sub_CategoryName;
                        SubCategoryModel.Sub_CategoryDescription = filteredResult.Sub_CategoryDescription;

                        if (WistList[Index].Sub_CategoryImagePath == null)
                            SubCategoryModel.Sub_CategoryImagePath = GetImagePathByImageColorId(WistList[Index].Sub_CategoryId,filteredResult.Sub_Category_DefaultColorId);
                        else
                            if(WistList[Index].Sub_Category_ColorId != null)
                            SubCategoryModel.Sub_CategoryImagePath = GetImagePathByImageColorId(WistList[Index].Sub_CategoryId,WistList[Index].Sub_Category_ColorId);

                        SubCategoryModel.Sub_Category_Quantity = filteredResult.Sub_Category_Quantity;
                        if (WistList[Index].Quantity == null)
                            SubCategoryModel.SelectedQuantity = 1;
                        else
                            SubCategoryModel.SelectedQuantity = WistList[Index].Quantity;

                        SubCategoryModel.Sub_Category_Stars = filteredResult.Sub_Category_Stars;

                        if (WistList[Index].Sub_Category_Price == null)
                            SubCategoryModel.Sub_Category_Price = filteredResult.Sub_Category_Price;
                        else
                            SubCategoryModel.Sub_Category_Price = WistList[Index].Sub_Category_Price;


                        SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsStrickOutPrice;
                        SubCategoryModel.Sub_Category_StrickOutPrice = filteredResult.Sub_Category_StrickOutPrice;
                        SubCategoryModel.Sub_Category_IsSize = filteredResult.Sub_Category_IsSize;

                        if (WistList[Index].Sub_Category_SizeId != null)
                        {
                            SubCategoryModel.Sub_Category_SizeId = WistList[Index].Sub_Category_SizeId;
                            if(SubCategoryModel.Sub_Category_SizeId != 0)
                            {
                                SubCategoryModel.Sub_Category_Size = GetSizeNameBySizeId(SubCategoryModel.CategoryId,SubCategoryModel.Sub_Category_SizeId);
                            }
                        }
                        else
                            SubCategoryModel.Sub_Category_Size = GetAvaliableSizeBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                            if(SubCategoryModel.Sub_Category_Size != null)
                            SubCategoryModel.Sub_Category_SizeId = GetAvaliableSizeIdBySizeName(SubCategoryModel.CategoryId,SubCategoryModel.Sub_Category_Size);

                        SubCategoryModel.Sub_Category_IsColor = filteredResult.Sub_Category_IsColor;

                        if (WistList[Index].Sub_Category_ColorId == null)
                        {
                            SubCategoryModel.Sub_Category_DefaultColorId = filteredResult.Sub_Category_DefaultColorId;
                            SubCategoryModel.Sub_Category_DefaultColorName = filteredResult.Sub_Category_ColorName;
                        }
                        else
                            SubCategoryModel.Sub_Category_DefaultColorId = WistList[Index].Sub_Category_ColorId;
                            if (WistList[Index].Sub_Category_ColorId != null)
                            SubCategoryModel.Sub_CategoryImagePath = GetImagePathByImageColorId(WistList[Index].Sub_CategoryId,WistList[Index].Sub_Category_ColorId);
                            SubCategoryModel.Sub_Category_DefaultColorName = GetColorNameByColorId(SubCategoryModel.Sub_Category_DefaultColorId);

                        SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsItemForm;
                        SubCategoryModel.Sub_Category_IsDeliveryFree = filteredResult.Sub_Category_IsDeliveryFree;
                        SubCategoryModel.AvgStar = GetSubcategoryRatingBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                        WistListSubCategoryDetailsList.Add(SubCategoryModel);
                    }
                }
                Response = GetCookiesWistListOnWistListPagestring(WistListSubCategoryDetailsList);
                Response.WishListCount = Convert.ToString(WistListSubCategoryDetailsList.Count);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetCookiesWistListOnWistListPage",ex.Message.ToString(),0);
            }
            return Response;
        }
        public JsonResponse GetWistListDataOnWistListPage(long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            List<SubCategoryModel> WistListSubCategoryDetailsList = new List<SubCategoryModel>();
            try
            {
                    var SubCategoryIdsList = DbEngineObj.WishList.Where(s => s.IsWishList == true && s.IsActive == true && s.UserId == UserId).ToList();
                    if (SubCategoryIdsList != null)
                    {
                            for (var Index = 0; Index < SubCategoryIdsList.Count; Index++)
                            {

                                long? SubCategoryId = Convert.ToInt64(SubCategoryIdsList[Index].Sub_CategoryId);
                                var filteredResult = DbEngineObj.Sub_Category.Where(s => s.Sub_CategoryId == SubCategoryId && s.IsActive == true).OrderBy(s => s.Sub_CategoryName).FirstOrDefault();
                                if (filteredResult != null)
                                {
                                    var SubCategoryModel = new SubCategoryModel();
                                    SubCategoryModel.WishListUniqueId = SubCategoryIdsList[Index].Id;
                                    SubCategoryModel.Sub_CategoryId = filteredResult.Sub_CategoryId;
                                    SubCategoryModel.CategoryId = filteredResult.CategoryId;
                                    SubCategoryModel.Sub_CategoryName = filteredResult.Sub_CategoryName;
                                    SubCategoryModel.Sub_CategoryDescription = filteredResult.Sub_CategoryDescription;
                                    
                                    if(SubCategoryIdsList[Index].Sub_CategoryImagePath == null)
                                        SubCategoryModel.Sub_CategoryImagePath = filteredResult.Sub_CategoryImagePath;
                                     else
                                        if(SubCategoryIdsList[Index].Sub_Category_ColorId != null)
                                        SubCategoryModel.Sub_CategoryImagePath = GetImagePathByImageColorId(SubCategoryIdsList[Index].Sub_CategoryId,SubCategoryIdsList[Index].Sub_Category_ColorId);

                                    SubCategoryModel.Sub_Category_Quantity = filteredResult.Sub_Category_Quantity;
                                    if (SubCategoryIdsList[Index].Quantity == null)
                                        SubCategoryModel.SelectedQuantity = 1;
                                    else
                                        SubCategoryModel.SelectedQuantity = SubCategoryIdsList[Index].Quantity;

                                    SubCategoryModel.Sub_Category_Stars = filteredResult.Sub_Category_Stars;
                            
                                    if (SubCategoryIdsList[Index].Sub_Category_Price == null)
                                        SubCategoryModel.Sub_Category_Price = filteredResult.Sub_Category_Price;
                                    else
                                        SubCategoryModel.Sub_Category_Price = SubCategoryIdsList[Index].Sub_Category_Price;
                                    
                            
                                    SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsStrickOutPrice;
                                    SubCategoryModel.Sub_Category_StrickOutPrice = filteredResult.Sub_Category_StrickOutPrice;
                                    SubCategoryModel.Sub_Category_IsSize = filteredResult.Sub_Category_IsSize;


                                    if (SubCategoryIdsList[Index].Sub_Category_SizeId != null)
                                    {
                                        SubCategoryModel.Sub_Category_SizeId = SubCategoryIdsList[Index].Sub_Category_SizeId;
                                        if(SubCategoryModel.Sub_Category_SizeId != null && SubCategoryModel.Sub_Category_SizeId != 0)
                                        {
                                            SubCategoryModel.Sub_Category_Size = GetSizeNameBySizeId(SubCategoryModel.CategoryId,SubCategoryModel.Sub_Category_SizeId);
                                        }
                                    }
                                    else
                                        SubCategoryModel.Sub_Category_Size = GetAvaliableSizeBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                                        if (SubCategoryModel.Sub_Category_Size != null)
                                        SubCategoryModel.Sub_Category_SizeId = GetAvaliableSizeIdBySizeName(SubCategoryModel.CategoryId,SubCategoryModel.Sub_Category_Size);

                                    SubCategoryModel.Sub_Category_IsColor = filteredResult.Sub_Category_IsColor;

                                    if (SubCategoryIdsList[Index].Sub_Category_ColorId == null)
                                    {
                                        SubCategoryModel.Sub_Category_DefaultColorId = filteredResult.Sub_Category_DefaultColorId;
                                        SubCategoryModel.Sub_Category_DefaultColorName = filteredResult.Sub_Category_ColorName;
                                    }
                                    else
                                        SubCategoryModel.Sub_Category_DefaultColorId = SubCategoryIdsList[Index].Sub_Category_ColorId;
                                        SubCategoryModel.Sub_Category_DefaultColorName = GetColorNameByColorId(SubCategoryModel.Sub_Category_DefaultColorId);

                                    SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsItemForm;
                                    SubCategoryModel.Sub_Category_IsDeliveryFree = filteredResult.Sub_Category_IsDeliveryFree;
                                    SubCategoryModel.AvgStar = GetSubcategoryRatingBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                                    WistListSubCategoryDetailsList.Add(SubCategoryModel);
                                }
                            }
                    Response = GetWistListDataOnWistListPagestring(WistListSubCategoryDetailsList);
                    Response.WishListCount = Convert.ToString(SubCategoryIdsList.Count);
                    Response.AddToCartCount = Convert.ToString(DbEngineObj.AddToCart.Where(a => a.IsAddToCart == true && a.IsActive == true && a.UserId == UserId).Count());
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetWistListDataOnWistListPage",ex.Message.ToString(),0);
            }
            return Response;
        }
        public JsonResponse GetCookiesWistListOnWistListPagestring(List<SubCategoryModel> SubCategoryIdList)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            StringBuilder sb = new StringBuilder();
            try
            {



                sb.AppendLine("<div class='row'>");
                sb.AppendLine("<div class='col-lg-9'>");
                sb.AppendLine("<div class='card'>");
                sb.AppendLine("<div class='content-body'>");
                sb.AppendLine("<h4 class='card-title mb-4'>Your Wish List shopping cart (" + SubCategoryIdList.Count + ")</h4>");

                for (var Index = 0; Index < SubCategoryIdList.Count; Index++)
                {
                    sb.AppendLine("<article class='card card-product-list'>");
                    sb.AppendLine("<div class='row g-0'>");
                    sb.AppendLine("<aside class='col-xl-3 col-lg-4 col-md-12 col-12'>");
                    sb.AppendLine("<a class='img-wrap'><img src='" + SubCategoryIdList[Index].Sub_CategoryImagePath + "'></a>");
                    sb.AppendLine("</aside> ");
                    sb.AppendLine("<div class='col-xl-6 col-lg col-md-7 col-sm-7 border-start'>");
                    sb.AppendLine(" <div class='card-body'>");
                    sb.AppendLine("<h3><a href='#' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a></h3>");
                    sb.AppendLine(" <div class='rating-wrap mb-2'>");
                    sb.AppendLine("<button class='badge bg-warning btn-lg' type='button' data-bs-toggle='popover' data-bs-html='true' title='Rating' data-bs-content='<p><strong>" + SubCategoryIdList[Index].AvgStar + " Out Of 5</strong></p>' data-bs-trigger='hover' ><i class='fa fa-star'> " + SubCategoryIdList[Index].AvgStar + "</i></button>");
                    //sb.AppendLine(" <ul class='rating-stars'>");
                    //sb.AppendLine(" <li class='stars-active' style='width: 90%;'>");
                    //sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/stars-active.svg' alt=''>");
                    //sb.AppendLine("	</li>");
                    //sb.AppendLine("<li>");
                    //sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/starts-disable.svg' alt=''>");
                    //sb.AppendLine("</li>");
                    //sb.AppendLine("</ul>");
                    //sb.AppendLine("<span class='label-rating text-warning'>4.5</span>");
                    //sb.AppendLine("<i class='dot'></i>");
                    //sb.AppendLine("<span class='label-rating text-muted'>154 orders</span>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<p class='text-muted'>" + SubCategoryIdList[Index].Sub_CategoryDescription + "</p>");
                    sb.AppendLine("<p class='my-2'>");
                    if (SubCategoryIdList[Index].Sub_Category_IsItemForm == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_category_ItemForm + "</span>");
                    }

                    if (SubCategoryIdList[Index].Sub_Category_IsColor == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_DefaultColorName + "</span>");
                    }

                    if (SubCategoryIdList[Index].Sub_Category_IsSize == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_Size + "</span>");
                    }
                    sb.AppendLine("</p>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<aside class='col-xl-3 col-lg-auto col-md-5 col-sm-5'>");
                    sb.AppendLine("<div class='info-aside'>");
                    sb.AppendLine("<div class='price-wrap'>");
                    sb.AppendLine("<span class='price h5'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</span>");
                    if (SubCategoryIdList[Index].Sub_Category_IsStrickOutPrice == true)
                    {
                        sb.AppendLine("<del class='price-old'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_StrickOutPrice + "</del>");
                    }
                    sb.AppendLine("</div>");
                    if (SubCategoryIdList[Index].Sub_Category_IsDeliveryFree == true)
                    {
                        sb.AppendLine("<p class='text-success'>Free shipping</p>");
                    }
                    sb.AppendLine("<br>");
                    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].WishListUniqueId + ");'>Add to cart</a>");
                    //sb.AppendLine("<a href='#' class='btn btn-outline-danger btn-icon'>");
                    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteWishListItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].WishListUniqueId + ");'> <i class='fa fa-trash'></i> </a>");
                    //sb.AppendLine("<i class='fa fa-trash'></i>");
                    sb.AppendLine("</a>");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<div class='input-group'>");
                    sb.AppendLine("<select id='QuantityId" + SubCategoryIdList[Index].WishListUniqueId + "'class='form-select'>");
                    var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                    for (var I = 0; I <= Sub_Category_Quantity_Count; I++)
                    {
                        if (I == 1)
                        {
                            sb.AppendLine("<option selected>" + I + "</option>");
                        }
                        else
                        {
                            sb.AppendLine("<option>" + I + "</option>");
                        }
                    }
                    Sub_Category_Quantity_Count = 0;
                    sb.AppendLine("</select>");
                    sb.AppendLine("<button class='btn btn-light text Primary' type='button'>Quantity</button>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<p class='mb-0 mt-3'>");
                    sb.AppendLine("</p>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</aside>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</article>");

                //sb.AppendLine("<div class='row'>");
                //sb.AppendLine("<div class='col-lg-9'>");
                //sb.AppendLine("<div class='card'>");
                //sb.AppendLine("<div class='content-body'>");
                //sb.AppendLine("<h4 class='card-title mb-4'>Your Wish List shopping cart ("+ SubCategoryIdList.Count + ")</h4>");

                //for (var Index = 0;Index < SubCategoryIdList.Count;Index++)
                //{
                //    sb.AppendLine("<article class='card card-product-list'>");
                //    sb.AppendLine("<div class='row g-0'>");
                //    sb.AppendLine("<aside class='col-xl-3 col-lg-4 col-md-12 col-12'>");
                //    sb.AppendLine("		<a href='#' class='img-wrap'> <img src='" + SubCategoryIdList[Index].Sub_CategoryImagePath + "'></a>");
                //    sb.AppendLine("</aside> ");
                //    sb.AppendLine("<div class='col-xl-6 col-lg col-md-7 col-sm-7 border-start'>");
                //    sb.AppendLine(" <div class='card-body'>");
                //    sb.AppendLine("<h3><a href='#' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a></h3>");
                //    sb.AppendLine(" <div class='rating-wrap mb-2'>");
                //    sb.AppendLine(" <ul class='rating-stars'>");
                //    sb.AppendLine(" <li class='stars-active' style='width: 90%;'>");
                //    sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/stars-active.svg' alt=''>");
                //    sb.AppendLine("	</li>");
                //    sb.AppendLine("<li>");
                //    sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/starts-disable.svg' alt=''>");
                //    sb.AppendLine("</li>");
                //    sb.AppendLine("</ul>");
                //    sb.AppendLine("<span class='label-rating text-warning'>4.5</span>");
                //    //sb.AppendLine("<i class='dot'></i>");
                //    //sb.AppendLine("<span class='label-rating text-muted'>154 orders</span>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<p class='text-muted'>" + SubCategoryIdList[Index].Sub_CategoryDescription + "</p>");
                //    sb.AppendLine("<p class='my-2'>");
                //    if(SubCategoryIdList[Index].Sub_Category_IsItemForm == true)
                //    {
                //        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_category_ItemForm + "</span>");
                //    }
                    
                //    if (SubCategoryIdList[Index].Sub_Category_IsColor == true)
                //    {
                //        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_DefaultColorName + "</span>");
                //    }

                //    if (SubCategoryIdList[Index].Sub_Category_IsSize == true)
                //    {
                //        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_Size + "</span>");
                //    }
                //    sb.AppendLine("</p>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<aside class='col-xl-3 col-lg-auto col-md-5 col-sm-5'>");
                //    sb.AppendLine("<div class='info-aside'>");
                //    sb.AppendLine("<div class='price-wrap'>");
                //    sb.AppendLine("<span class='price h5'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</span>");
                //    if(SubCategoryIdList[Index].Sub_Category_IsStrickOutPrice == true)
                //    {
                //        sb.AppendLine("<del class='price-old'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_StrickOutPrice + "</del>");
                //    }
                //    sb.AppendLine("</div>");
                //    if (SubCategoryIdList[Index].Sub_Category_IsDeliveryFree == true)
                //    {
                //        sb.AppendLine("<p class='text-success'>Free shipping</p>");
                //    }
                //    sb.AppendLine("<br>");
                //    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryIdList[Index].Sub_CategoryId + "," + Index + ");'>Add to cart</a>");
                //    //sb.AppendLine("<a href='#' class='btn btn-outline-danger btn-icon'>");
                //    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteWishListItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + Index + ");'> <i class='fa fa-trash'></i> </a>");
                //    //sb.AppendLine("<i class='fa fa-trash'></i>");
                //    sb.AppendLine("</a>");
                //    sb.AppendLine("<p class='mb-0 mt-3'>");
                //    sb.AppendLine("</p>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</aside>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</article>");
                }

                //for(var Index = 0; Index < SubCategoryIdList.Count;Index++)
                //{
                //    sb.AppendLine("<article class='row gy-3 mb-4'>");
                //    sb.AppendLine("<div class='col-lg-5'>");
                //    sb.AppendLine("<figure class='itemside me-lg-5'>");
                //    sb.AppendLine("<div class='aside'><img src='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "' class='img-sm img-thumbnail'></div>");
                //    sb.AppendLine("<figcaption class='info'>");
                //    sb.AppendLine("<a href='' class='title'>"+ SubCategoryIdList[Index].Sub_CategoryName + "</a>");
                //    sb.AppendLine("<p class='text-muted'> "+ SubCategoryIdList[Index].Sub_CategoryDescription + " </p>");
                //    sb.AppendLine("</figcaption>");
                //    sb.AppendLine("</figure>");
                //    sb.AppendLine("</div>");
                //    //sb.AppendLine("<div class='col-auto'>");
                //    //sb.AppendLine("<select style='width: 100px' class='form-select'>");
                //    //sb.AppendLine("<option>"+ "0" + "</option>");
                //    //var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                //    //for (var I = 1;I <= Sub_Category_Quantity_Count; I++)
                //    //{
                //    //    sb.AppendLine("<option>"+ I + "</option>");
                //    //}
                //    //Sub_Category_Quantity_Count = 0;
                //    //sb.AppendLine("</select>");
                //    //sb.AppendLine("</div>");
                //    sb.AppendLine("<div class='col-lg-2 col-sm-4 col-6'>");
                //    sb.AppendLine("<div class='price-wrap lh-sm'>");
                //    sb.AppendLine("<var class='price h6'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</var><br>");
                //    //sb.AppendLine("<small class='text-muted'> $460.00 / per item </small>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<div class='col-lg col-sm-4'>");
                //    sb.AppendLine("<div class='float-lg-end'>");
                //    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryIdList[Index].Sub_CategoryId + "," + Index + ");'>Add to cart</a>");
                //    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteWishListItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + Index + ");'> <i class='fa fa-trash'></i> </a>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</article>");
                //}
                sb.AppendLine("</div> ");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                Response.StringReponse = sb.ToString();
                Response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetCookiesWistListOnWistListPagestring",ex.Message.ToString(),0);
            }
            return Response;
        }
        public JsonResponse GetWistListDataOnWistListPagestring(List<SubCategoryModel> SubCategoryIdList)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine("<div class='row'>");
                sb.AppendLine("<div class='col-lg-9'>");
                sb.AppendLine("<div class='card'>");
                sb.AppendLine("<div class='content-body'>");
                sb.AppendLine("<h4 class='card-title mb-4'>Your Wish List shopping cart (" + SubCategoryIdList.Count + ")</h4>");
                
                for (var Index = 0; Index < SubCategoryIdList.Count; Index++)
                {
                    sb.AppendLine("<article class='card card-product-list'>");
                    sb.AppendLine("<div class='row g-0'>");
                    sb.AppendLine("<aside class='col-xl-3 col-lg-4 col-md-12 col-12'>");
                    sb.AppendLine("		<a href='#' class='img-wrap'> <img src='" + SubCategoryIdList[Index].Sub_CategoryImagePath + "'></a>");
                    sb.AppendLine("</aside> ");
                    sb.AppendLine("<div class='col-xl-6 col-lg col-md-7 col-sm-7 border-start'>");
                    sb.AppendLine(" <div class='card-body'>");
                    sb.AppendLine("<h3><a href='#' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a></h3>");
                    sb.AppendLine(" <div class='rating-wrap mb-2'>");
                    sb.AppendLine("<button class='badge bg-warning btn-lg' type='button' data-bs-toggle='popover' data-bs-html='true' title='Rating' data-bs-content='<p><strong>"+ SubCategoryIdList[Index].AvgStar + " Out Of 5</strong></p>' data-bs-trigger='hover' ><i class='fa fa-star'> "+ SubCategoryIdList[Index].AvgStar + "</i></button>");
                    //sb.AppendLine(" <ul class='rating-stars'>");
                    //sb.AppendLine(" <li class='stars-active' style='width: 90%;'>");
                    //sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/stars-active.svg' alt=''>");
                    //sb.AppendLine("	</li>");
                    //sb.AppendLine("<li>");
                    //sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/starts-disable.svg' alt=''>");
                    //sb.AppendLine("</li>");
                    //sb.AppendLine("</ul>");
                    //sb.AppendLine("<span class='label-rating text-warning'>4.5</span>");
                    //sb.AppendLine("<i class='dot'></i>");
                    //sb.AppendLine("<span class='label-rating text-muted'>154 orders</span>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<p class='text-muted'>" + SubCategoryIdList[Index].Sub_CategoryDescription + "</p>");
                    sb.AppendLine("<p class='my-2'>");
                    if (SubCategoryIdList[Index].Sub_Category_IsItemForm == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_category_ItemForm + "</span>");
                    }

                    if (SubCategoryIdList[Index].Sub_Category_IsColor == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_DefaultColorName + "</span>");
                    }

                    if (SubCategoryIdList[Index].Sub_Category_IsSize == true)
                    {
                        sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_Size + "</span>");
                    }
                    sb.AppendLine("</p>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<aside class='col-xl-3 col-lg-auto col-md-5 col-sm-5'>");
                    sb.AppendLine("<div class='info-aside'>");
                    sb.AppendLine("<div class='price-wrap'>");
                    sb.AppendLine("<span class='price h5'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</span>");
                    if (SubCategoryIdList[Index].Sub_Category_IsStrickOutPrice == true)
                    {
                        sb.AppendLine("<del class='price-old'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_StrickOutPrice + "</del>");
                    }
                    sb.AppendLine("</div>");
                    if (SubCategoryIdList[Index].Sub_Category_IsDeliveryFree == true)
                    {
                        sb.AppendLine("<p class='text-success'>Free shipping</p>");
                    }
                    sb.AppendLine("<br>");
                    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].WishListUniqueId + "," + SubCategoryIdList[Index].Sub_Category_Price + "," + SubCategoryIdList[Index].Sub_Category_DefaultColorId + "," + SubCategoryIdList[Index].Sub_Category_SizeId + ");'>Add to cart</a>");
                    //sb.AppendLine("<a href='#' class='btn btn-outline-danger btn-icon'>");
                    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteWishListItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].WishListUniqueId + ");'> <i class='fa fa-trash'></i> </a>");
                    //sb.AppendLine("<i class='fa fa-trash'></i>");
                    sb.AppendLine("</a>");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<div class='input-group'>");
                    sb.AppendLine("<select id='QuantityId"+ SubCategoryIdList[Index].WishListUniqueId + "'class='form-select'>");
                    var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                    for (var I = 0; I <= Sub_Category_Quantity_Count; I++)
                    {
                        if (I == SubCategoryIdList[Index].SelectedQuantity)
                        {
                            sb.AppendLine("<option selected>" + I + "</option>");
                        }
                        else
                        {
                            sb.AppendLine("<option>" + I + "</option>");
                        }
                    }
                    Sub_Category_Quantity_Count = 0;
                    sb.AppendLine("</select>");
                    sb.AppendLine("<button class='btn btn-light text Primary' type='button'>Quantity</button>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<p class='mb-0 mt-3'>");
                    sb.AppendLine("</p>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</aside>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</article>");
                }

                //for (var Index = 0; Index < SubCategoryIdList.Count; Index++)
                //{
                //    sb.AppendLine("<article class='row gy-3 mb-4'>");
                //    sb.AppendLine("<div class='col-lg-5'>");
                //    sb.AppendLine("<figure class='itemside me-lg-5'>");
                //    sb.AppendLine("<div class='aside'><img src='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "' class='img-sm img-thumbnail'></div>");
                //    sb.AppendLine("<figcaption class='info'>");
                //    sb.AppendLine("<a href='' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a>");
                //    sb.AppendLine("<p class='text-muted'> " + SubCategoryIdList[Index].Sub_CategoryDescription + " </p>");
                //    sb.AppendLine("</figcaption>");
                //    sb.AppendLine("</figure>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<div class='col-auto'>");
                //    sb.AppendLine("<select style='width: 100px' class='form-select'>");
                //    sb.AppendLine("<option>" + "0" + "</option>");
                //    var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                //    for (var I = 1; I <= Sub_Category_Quantity_Count; I++)
                //    {
                //        sb.AppendLine("<option>" + I + "</option>");
                //    }
                //    Sub_Category_Quantity_Count = 0;
                //    sb.AppendLine("</select>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<div class='col-lg-2 col-sm-4 col-6'>");
                //    sb.AppendLine("<div class='price-wrap lh-sm'>");
                //    sb.AppendLine("<var class='price h6'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</var><br>");
                //    //sb.AppendLine("<small class='text-muted'> $460.00 / per item </small>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("<div class='col-lg col-sm-4'>");
                //    sb.AppendLine("<div class='float-lg-end'>");
                //    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickAddToCart(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'>Add to cart</a>");
                //    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteWishListItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'> <i class='fa fa-trash'></i> </a>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</div>");
                //    sb.AppendLine("</article>");
                //}
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                Response.StringReponse = sb.ToString();
                Response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetWistListDataOnWistListPagestring",ex.Message.ToString(),0);
            }
            return Response;
        }
        public JsonResponse GetAddToCartDataOnAddToCartPage(long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            List<SubCategoryModel> AddToCartListSubCategoryDetailsList = new List<SubCategoryModel>();
            var TotalAmount = (dynamic)null;
            try
            {

                var SubCategoryIdsList = DbEngineObj.AddToCart.Where(s => s.IsAddToCart == true && s.IsActive == true && s.UserId == UserId).ToList();

                
                    for (var Index = 0; Index < SubCategoryIdsList.Count; Index++)
                    {

                        long? SubCategoryId = Convert.ToInt64(SubCategoryIdsList[Index].Sub_CategoryId);
                        var filteredResult = DbEngineObj.Sub_Category.Where(s => s.Sub_CategoryId == SubCategoryId && s.IsActive == true).OrderBy(s => s.Sub_CategoryName).FirstOrDefault();
                        if (filteredResult != null)
                        {
                            var SubCategoryModel = new SubCategoryModel();
                            SubCategoryModel.AddToCartUniqueId = SubCategoryIdsList[Index].Id;
                            SubCategoryModel.Sub_CategoryId = filteredResult.Sub_CategoryId;
                            SubCategoryModel.CategoryId = filteredResult.CategoryId;
                            SubCategoryModel.Sub_CategoryName = filteredResult.Sub_CategoryName;
                            SubCategoryModel.Sub_CategoryDescription = filteredResult.Sub_CategoryDescription;
                            SubCategoryModel.Sub_CategoryImagePath = filteredResult.Sub_CategoryImagePath;
                            SubCategoryModel.Sub_Category_Quantity = filteredResult.Sub_Category_Quantity;
                             if(SubCategoryIdsList[Index].Quantity == null)
                                SubCategoryModel.SelectedQuantity = 1;
                             else 
                                SubCategoryModel.SelectedQuantity = SubCategoryIdsList[Index].Quantity;
                            
                            if (SubCategoryIdsList[Index].Sub_Category_Price == null)
                                SubCategoryModel.Sub_Category_Price = filteredResult.Sub_Category_Price;
                            else
                                SubCategoryModel.Sub_Category_Price = SubCategoryIdsList[Index].Sub_Category_Price;
                        
                            SubCategoryModel.Sub_Category_IsStrickOutPrice = filteredResult.Sub_Category_IsStrickOutPrice;
                            SubCategoryModel.Sub_Category_StrickOutPrice = filteredResult.Sub_Category_StrickOutPrice;
                            SubCategoryModel.Sub_Category_IsSize = filteredResult.Sub_Category_IsSize;
                            SubCategoryModel.Sub_Category_Size = GetAvaliableSizeBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                            //SubCategoryModel.Sub_Category_Size = GetSizeNameBySizeId(SubCategoryModel.CategoryId,SubCategoryIdsList[Index].Sub_Category_SizeId);
                            if(SubCategoryModel.Sub_Category_Size != null)
                            {
                                SubCategoryModel.Sub_Category_SizeId = GetAvaliableSizeIdBySizeName(SubCategoryModel.CategoryId,SubCategoryModel.Sub_Category_Size);
                            }
                            
                            SubCategoryModel.Sub_Category_IsColor = filteredResult.Sub_Category_IsColor;
                            
                            if(SubCategoryIdsList[Index].Sub_Category_ColorId == null)
                                SubCategoryModel.Sub_Category_DefaultColorId = filteredResult.Sub_Category_DefaultColorId;
                             else
                                SubCategoryModel.Sub_Category_DefaultColorId = SubCategoryIdsList[Index].Sub_Category_ColorId;
                            
                            
                            SubCategoryModel.Sub_Category_DefaultColorName = filteredResult.Sub_Category_ColorName;
                            SubCategoryModel.Sub_Category_IsDeliveryFree = filteredResult.Sub_Category_IsDeliveryFree;
                            SubCategoryModel.AvgStar = GetSubcategoryRatingBySubCategoryId(SubCategoryModel.Sub_CategoryId);
                            AddToCartListSubCategoryDetailsList.Add(SubCategoryModel);
                        }
                        TotalAmount = 0;
                        for (var Index1 = 0; Index1 < AddToCartListSubCategoryDetailsList.Count; Index1++)
                        {
                            
                            TotalAmount = TotalAmount + (AddToCartListSubCategoryDetailsList[Index1].SelectedQuantity * AddToCartListSubCategoryDetailsList[Index1].Sub_Category_Price);
                        }

                        for (var Index1 = 0; Index1 < AddToCartListSubCategoryDetailsList.Count; Index1++)
                        {
                            AddToCartListSubCategoryDetailsList[Index1].SubCategoryTotalPrice = TotalAmount;
                        }

                    }
                Response = GetAddToCartDataOnAddToCartPagestring(AddToCartListSubCategoryDetailsList);
                Response.AddToCartCount = Convert.ToString(SubCategoryIdsList.Count);
            
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetAddToCartDataOnAddToCartPage",ex.Message.ToString(),0);
            }
            return Response;
        }
        public JsonResponse GetOnchangeQuantityAddToCartDataOnAddToCartPagestring(List<SubCategoryModel> SubCategoryIdList,long? AddToCartuniqueId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            StringBuilder sb = new StringBuilder();
            try
            {

                if (SubCategoryIdList.Count != 0)
                {
                    decimal? TotalAmount = 0;
                    sb.AppendLine("<div class='row'>");
                    sb.AppendLine("<div class='col-lg-9'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='content-body'>");
                    sb.AppendLine("<h4 class='card-title mb-4'>Your shopping cart (" + SubCategoryIdList.Count + ")</h4>");

                    for (var Index = 0; Index < SubCategoryIdList.Count; Index++)
                    {
                        sb.AppendLine("<article class='card card-product-list'>");
                        sb.AppendLine("<div class='row g-0'>");
                        sb.AppendLine("<aside class='col-xl-3 col-lg-4 col-md-12 col-12'>");
                        sb.AppendLine("		<a href='#' class='img-wrap'> <img src='" + SubCategoryIdList[Index].Sub_CategoryImagePath + "'></a>");
                        sb.AppendLine("</aside> ");
                        sb.AppendLine("<div class='col-xl-6 col-lg col-md-7 col-sm-7 border-start'>");
                        sb.AppendLine(" <div class='card-body'>");
                        sb.AppendLine("<h3><a href='#' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a></h3>");
                        sb.AppendLine(" <div class='rating-wrap mb-2'>");
                        sb.AppendLine("<button class='badge bg-warning btn-lg' type='button' data-bs-toggle='popover' data-bs-html='true' title='Rating' data-bs-content='<p><strong>" + SubCategoryIdList[Index].AvgStar + " Out Of 5</strong></p>' data-bs-trigger='hover' ><i class='fa fa-star'> " + SubCategoryIdList[Index].AvgStar + "</i></button>");
                        //sb.AppendLine(" <ul class='rating-stars'>");
                        ////sb.AppendLine(" <li class='stars-active' style='width: 90%;'>");
                        ////sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/stars-active.svg' alt=''>");
                        ////sb.AppendLine("	</li>");
                        ////sb.AppendLine("<li>");
                        ////sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/starts-disable.svg' alt=''>");
                        //sb.AppendLine("</li>");
                        //sb.AppendLine("</ul>");
                        //sb.AppendLine("<span class='label-rating text-warning'>4.5</span>");
                        //sb.AppendLine("<i class='dot'></i>");
                        //sb.AppendLine("<span class='label-rating text-muted'>154 orders</span>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<p class='text-muted'>" + SubCategoryIdList[Index].Sub_CategoryDescription + "</p>");
                        sb.AppendLine("<p class='my-2'>");
                        if (SubCategoryIdList[Index].Sub_Category_IsItemForm == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_category_ItemForm + "</span>");
                        }

                        if (SubCategoryIdList[Index].Sub_Category_IsColor == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_DefaultColorName + "</span>");
                        }

                        if (SubCategoryIdList[Index].Sub_Category_IsSize == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_Size + "</span>");
                        }
                        sb.AppendLine("</p>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<aside class='col-xl-3 col-lg-auto col-md-5 col-sm-5'>");
                        sb.AppendLine("<div class='info-aside'>");
                        sb.AppendLine("<div class='price-wrap'>");
                        sb.AppendLine("<span class='price h5'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</span>");
                        if (SubCategoryIdList[Index].Sub_Category_IsStrickOutPrice == true)
                        {
                            sb.AppendLine("<del class='price-old'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_StrickOutPrice + "</del>");
                        }
                        sb.AppendLine("</div>");
                        if (SubCategoryIdList[Index].Sub_Category_IsDeliveryFree == true)
                        {
                            sb.AppendLine("<p class='text-success'>Free shipping</p>");
                        }
                        sb.AppendLine("<br>");
                        sb.AppendLine("<a class='btn btn-primary' onClick='OnClickBuyItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].AddToCartUniqueId + ");'>Buy</a>");
                        //sb.AppendLine("<a href='#' class='btn btn-outline-danger btn-icon'>");
                        sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteItemOnAddToCartPage(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].AddToCartUniqueId + ");'> <i class='fa fa-trash'></i> </a>");
                        //sb.AppendLine("<i class='fa fa-trash'></i>");
                        sb.AppendLine("</a>");
                        sb.AppendLine("<br/>");
                        sb.AppendLine("<br/>");
                        sb.AppendLine("<div class='input-group'>");
                        sb.AppendLine("<select onchange='OnchangeQuantityForAddToCart(" + SubCategoryIdList[Index].AddToCartUniqueId + ")' id='QuantityId" + SubCategoryIdList[Index].AddToCartUniqueId + "'class='form-select'>");
                        var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                        for (var I = 0; I <= Sub_Category_Quantity_Count; I++)
                        {
                            if (I == SubCategoryIdList[Index].SelectedQuantity && SubCategoryIdList[Index].AddToCartUniqueId == AddToCartuniqueId)
                            {
                                sb.AppendLine("<option selected>" + I + "</option>");
                            }
                            else if(I == SubCategoryIdList[Index].SelectedQuantity)
                            {
                                sb.AppendLine("<option selected>" + I + "</option>");
                                
                            } else
                            {
                                sb.AppendLine("<option>" + I + "</option>");
                            }
                        }
                        Sub_Category_Quantity_Count = 0;
                        sb.AppendLine("</select>");
                        sb.AppendLine("<button class='btn btn-light text Primary' type='button'>Quantity</button>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<p class='mb-0 mt-3'>");
                        sb.AppendLine("</p>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</aside>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</article>");
                    }

                    //for(var Index = 0; Index < SubCategoryIdList.Count; Index++)
                    //{
                    //    sb.AppendLine("<article class='row gy-3 mb-4'>");
                    //    sb.AppendLine("<div class='col-lg-5'>");
                    //    sb.AppendLine("<figure class='itemside me-lg-5'>");
                    //    sb.AppendLine("<div class='aside'><img src='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "' class='img-sm img-thumbnail'></div>");
                    //    sb.AppendLine("<figcaption class='info'>");
                    //    sb.AppendLine("<a class='title'>"+ SubCategoryIdList[Index].Sub_CategoryName + "</a>");
                    //    sb.AppendLine("<p class='text-muted'> "+ SubCategoryIdList[Index].Sub_CategoryDescription + " </p>");
                    //    sb.AppendLine("</figcaption>");
                    //    sb.AppendLine("</figure>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-auto'>");
                    //    sb.AppendLine("<select style='width: 100px' id='QuantityDropDownId"+ SubCategoryIdList[Index].Id + "' class='form-select'>");
                    //    sb.AppendLine("<option value='" + 0 + "'>" + "0" + "</option>");
                    //    var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                    //    for (var I = 1; I <= Sub_Category_Quantity_Count; I++)
                    //    {
                    //            sb.AppendLine("<option value='"+ I + "'>" + I + "</option>");
                    //    }
                    //    Sub_Category_Quantity_Count = 0;
                    //    sb.AppendLine("</select>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-lg-2 col-sm-4 col-6'>");
                    //    sb.AppendLine("<div class='price-wrap lh-sm'>");
                    //    sb.AppendLine("<var class='price h6' id='SubCategoryPriceId"+ SubCategoryIdList[Index].Id + "' >" + SubCategoryIdList[Index].Sub_Category_Price + "</var><br>");
                    //    TotalAmount = TotalAmount + SubCategoryIdList[Index].Sub_Category_Price;
                    //    sb.AppendLine("<small class='text-muted'> ₹"+ SubCategoryIdList[Index].Sub_Category_Price + "/per item</small>");
                    //    sb.AppendLine("</div> <!-- price-wrap .// -->");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-lg col-sm-4'>");
                    //    sb.AppendLine("<div class='float-lg-end'>");
                    //    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickBuyItem("+ SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'>Buy</a>");
                    //    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteItemOnAddToCartPage(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'> <i class='fa fa-trash'></i> </a>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("</article> <!-- row.// -->");
                    //}
                    sb.AppendLine("");
                    sb.AppendLine("</div><!-- card-body .// -->");
                    sb.AppendLine("</div> <!-- card.// -->");
                    sb.AppendLine("");
                    sb.AppendLine("</div> <!-- col.// -->");
                    sb.AppendLine("<aside class='col-lg-3'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='card-body'>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total price:</dt>");
                    sb.AppendLine("<dd class='text-end'> ₹" + SubCategoryIdList[0].SubCategoryTotalPrice + "</dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    //sb.AppendLine("<dt>Discount:</dt>");
                    //sb.AppendLine("<dd class='text-end text-success'> - ₹0 </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    //sb.AppendLine("<dt>TAX:</dt>");
                    //sb.AppendLine("<dd class='text-end'> ₹0 </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<hr>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total:</dt>");
                    sb.AppendLine("<dd class='text-end text-dark h5'> ₹" + SubCategoryIdList[0].SubCategoryTotalPrice + " </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("");
                    sb.AppendLine("<div class='d-grid gap-2 my-3'>");
                    //sb.AppendLine("<a class='btn btn-success w-100'> Make Purchase </a>");
                    sb.AppendLine("<a href='/Home/OrderPage' class='btn btn-light w-100'>Track Orders</a>");
                    sb.AppendLine("<a href='/Home/HomePage' class='btn btn-light w-100'> Back to Home </a>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div> <!-- card-body.// -->");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<!-- card.// -->");
                    sb.AppendLine("</aside> <!-- col.// -->");
                    sb.AppendLine("</div>");
                }
                else
                {
                    decimal? TotalAmount = 0;
                }
                Response.StringReponse = sb.ToString();
                Response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory", "GetAddToCartDataOnAddToCartPagestring", ex.Message.ToString(), 0);
            }
            return Response;
        }
        public JsonResponse GetAddToCartDataOnAddToCartPagestring(List<SubCategoryModel> SubCategoryIdList)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            StringBuilder sb = new StringBuilder();
            try
            {

                if(SubCategoryIdList.Count != 0)
                {
                    decimal? TotalAmount = 0;
                    sb.AppendLine("<div class='row'>");
                    sb.AppendLine("<div class='col-lg-9'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='content-body'>");
                    sb.AppendLine("<h4 class='card-title mb-4'>Your shopping cart (" + SubCategoryIdList.Count + ")</h4>");

                    for (var Index = 0; Index < SubCategoryIdList.Count; Index++)
                    {
                        sb.AppendLine("<article class='card card-product-list'>");
                        sb.AppendLine("<div class='row g-0'>");
                        sb.AppendLine("<aside class='col-xl-3 col-lg-4 col-md-12 col-12'>");
                        sb.AppendLine("		<a href='#' class='img-wrap'> <img src='" + SubCategoryIdList[Index].Sub_CategoryImagePath + "'></a>");
                        sb.AppendLine("</aside> ");
                        sb.AppendLine("<div class='col-xl-6 col-lg col-md-7 col-sm-7 border-start'>");
                        sb.AppendLine(" <div class='card-body'>");
                        sb.AppendLine("<h3><a href='#' class='title'>" + SubCategoryIdList[Index].Sub_CategoryName + "</a></h3>");
                        sb.AppendLine(" <div class='rating-wrap mb-2'>");
                        sb.AppendLine("<button class='badge bg-warning btn-lg' type='button' data-bs-toggle='popover' data-bs-html='true' title='Rating' data-bs-content='<p><strong>" + SubCategoryIdList[Index].AvgStar + " Out Of 5</strong></p>' data-bs-trigger='hover' ><i class='fa fa-star'> " + SubCategoryIdList[Index].AvgStar + "</i></button>");
                        //sb.AppendLine(" <ul class='rating-stars'>");
                        ////sb.AppendLine(" <li class='stars-active' style='width: 90%;'>");
                        ////sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/stars-active.svg' alt=''>");
                        ////sb.AppendLine("	</li>");
                        ////sb.AppendLine("<li>");
                        ////sb.AppendLine(" <img src='bootstrap5-ecommerce/images/misc/starts-disable.svg' alt=''>");
                        //sb.AppendLine("</li>");
                        //sb.AppendLine("</ul>");
                        //sb.AppendLine("<span class='label-rating text-warning'>4.5</span>");
                        //sb.AppendLine("<i class='dot'></i>");
                        //sb.AppendLine("<span class='label-rating text-muted'>154 orders</span>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<p class='text-muted'>" + SubCategoryIdList[Index].Sub_CategoryDescription + "</p>");
                        sb.AppendLine("<p class='my-2'>");
                        if (SubCategoryIdList[Index].Sub_Category_IsItemForm == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_category_ItemForm + "</span>");
                        }

                        if (SubCategoryIdList[Index].Sub_Category_IsColor == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_DefaultColorName + "</span>");
                        }

                        if (SubCategoryIdList[Index].Sub_Category_IsSize == true)
                        {
                            sb.AppendLine("<span class='btn btn-outline-primary'>" + SubCategoryIdList[Index].Sub_Category_Size + "</span>");
                        }
                        sb.AppendLine("</p>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<aside class='col-xl-3 col-lg-auto col-md-5 col-sm-5'>");
                        sb.AppendLine("<div class='info-aside'>");
                        sb.AppendLine("<div class='price-wrap'>");
                        sb.AppendLine("<span class='price h5'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_Price + "</span>");
                        if (SubCategoryIdList[Index].Sub_Category_IsStrickOutPrice == true)
                        {
                            sb.AppendLine("<del class='price-old'>" + "₹" + " " + SubCategoryIdList[Index].Sub_Category_StrickOutPrice + "</del>");
                        }
                        sb.AppendLine("</div>");
                        if (SubCategoryIdList[Index].Sub_Category_IsDeliveryFree == true)
                        {
                            sb.AppendLine("<p class='text-success'>Free shipping</p>");
                        }
                        sb.AppendLine("<br>");
                        sb.AppendLine("<a class='btn btn-primary' onClick='OnClickBuyItem(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].AddToCartUniqueId + ");'>Buy</a>");
                        //sb.AppendLine("<a href='#' class='btn btn-outline-danger btn-icon'>");
                        sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteItemOnAddToCartPage(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].AddToCartUniqueId + ");'> <i class='fa fa-trash'></i> </a>");
                        //sb.AppendLine("<i class='fa fa-trash'></i>");
                        sb.AppendLine("</a>");
                        sb.AppendLine("<br/>");
                        sb.AppendLine("<br/>");
                        sb.AppendLine("<div class='input-group'>");
                        sb.AppendLine("<select onchange='OnchangeQuantityForAddToCart("+ SubCategoryIdList[Index].AddToCartUniqueId + ")' id='QuantityId"+SubCategoryIdList[Index].AddToCartUniqueId+"'class='form-select'>");
                        var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                        for (var I = 0; I <= Sub_Category_Quantity_Count; I++)
                        {
                            if (I == SubCategoryIdList[Index].SelectedQuantity)
                            {
                                sb.AppendLine("<option selected>" + I + "</option>");
                            }
                            else
                            {
                                sb.AppendLine("<option>" + I + "</option>");
                            }
                        }
                        Sub_Category_Quantity_Count = 0;
                        sb.AppendLine("</select>");
                        sb.AppendLine("<button class='btn btn-light text Primary' type='button'>Quantity</button>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<p class='mb-0 mt-3'>");
                        sb.AppendLine("</p>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</aside>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</article>");
                    }

                    //for(var Index = 0; Index < SubCategoryIdList.Count; Index++)
                    //{
                    //    sb.AppendLine("<article class='row gy-3 mb-4'>");
                    //    sb.AppendLine("<div class='col-lg-5'>");
                    //    sb.AppendLine("<figure class='itemside me-lg-5'>");
                    //    sb.AppendLine("<div class='aside'><img src='" + "/assets2/images/DefualtImage/default-user-icon-8.jpg" + "' class='img-sm img-thumbnail'></div>");
                    //    sb.AppendLine("<figcaption class='info'>");
                    //    sb.AppendLine("<a class='title'>"+ SubCategoryIdList[Index].Sub_CategoryName + "</a>");
                    //    sb.AppendLine("<p class='text-muted'> "+ SubCategoryIdList[Index].Sub_CategoryDescription + " </p>");
                    //    sb.AppendLine("</figcaption>");
                    //    sb.AppendLine("</figure>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-auto'>");
                    //    sb.AppendLine("<select style='width: 100px' id='QuantityDropDownId"+ SubCategoryIdList[Index].Id + "' class='form-select'>");
                    //    sb.AppendLine("<option value='" + 0 + "'>" + "0" + "</option>");
                    //    var Sub_Category_Quantity_Count = SubCategoryIdList[Index].Sub_Category_Quantity;
                    //    for (var I = 1; I <= Sub_Category_Quantity_Count; I++)
                    //    {
                    //            sb.AppendLine("<option value='"+ I + "'>" + I + "</option>");
                    //    }
                    //    Sub_Category_Quantity_Count = 0;
                    //    sb.AppendLine("</select>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-lg-2 col-sm-4 col-6'>");
                    //    sb.AppendLine("<div class='price-wrap lh-sm'>");
                    //    sb.AppendLine("<var class='price h6' id='SubCategoryPriceId"+ SubCategoryIdList[Index].Id + "' >" + SubCategoryIdList[Index].Sub_Category_Price + "</var><br>");
                    //    TotalAmount = TotalAmount + SubCategoryIdList[Index].Sub_Category_Price;
                    //    sb.AppendLine("<small class='text-muted'> ₹"+ SubCategoryIdList[Index].Sub_Category_Price + "/per item</small>");
                    //    sb.AppendLine("</div> <!-- price-wrap .// -->");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("<div class='col-lg col-sm-4'>");
                    //    sb.AppendLine("<div class='float-lg-end'>");
                    //    sb.AppendLine("<a class='btn btn-primary' onClick='OnClickBuyItem("+ SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'>Buy</a>");
                    //    sb.AppendLine("<a class='btn btn-icon btn-light' onClick='OnClickDeleteItemOnAddToCartPage(" + SubCategoryIdList[Index].Sub_CategoryId + "," + SubCategoryIdList[Index].Id + ");'> <i class='fa fa-trash'></i> </a>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("</div>");
                    //    sb.AppendLine("</article> <!-- row.// -->");
                    //}
                    sb.AppendLine("");
                    sb.AppendLine("</div><!-- card-body .// -->");
                    sb.AppendLine("</div> <!-- card.// -->");
                    sb.AppendLine("");
                    sb.AppendLine("</div> <!-- col.// -->");
                    sb.AppendLine("<aside class='col-lg-3'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='card-body'>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total price:</dt>");
                    sb.AppendLine("<dd class='text-end'> ₹" + SubCategoryIdList[0].SubCategoryTotalPrice + "</dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    //sb.AppendLine("<dt>Discount:</dt>");
                    //sb.AppendLine("<dd class='text-end text-success'> - ₹0 </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    //sb.AppendLine("<dt>TAX:</dt>");
                    //sb.AppendLine("<dd class='text-end'> ₹0 </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<hr>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total:</dt>");
                    sb.AppendLine("<dd class='text-end text-dark h5'> ₹" + SubCategoryIdList[0].SubCategoryTotalPrice + " </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("");
                    sb.AppendLine("<div class='d-grid gap-2 my-3'>");
                    //sb.AppendLine("<a class='btn btn-success w-100'> Make Purchase </a>");
                    sb.AppendLine("<a href='/Home/OrderPage' class='btn btn-light w-100'>Track Orders</a>");
                    sb.AppendLine("<a href='/Home/HomePage' class='btn btn-light w-100'> Back to Home </a>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div> <!-- card-body.// -->");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<!-- card.// -->");
                    sb.AppendLine("</aside> <!-- col.// -->");
                    sb.AppendLine("</div>");
                } else
                {
                    decimal? TotalAmount = 0;
                    sb.AppendLine("<div class='row'>");
                    sb.AppendLine("<div class='col-lg-9'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='content-body'>");
                    sb.AppendLine("<h4 class='card-title mb-4'>Your shopping cart (" + 0 + ")</h4>");
                    sb.AppendLine("");
                    sb.AppendLine("</div><!-- card-body .// -->");
                    sb.AppendLine("</div> <!-- card.// -->");
                    sb.AppendLine("");
                    sb.AppendLine("</div> <!-- col.// -->");
                    sb.AppendLine("<aside class='col-lg-3'>");
                    sb.AppendLine("<div class='card'>");
                    sb.AppendLine("<div class='card-body'>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total price:</dt>");
                    sb.AppendLine("<dd class='text-end'> ₹" + 0 + "</dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<hr>");
                    sb.AppendLine("<dl class='dlist-align'>");
                    sb.AppendLine("<dt>Total:</dt>");
                    sb.AppendLine("<dd class='text-end text-dark h5'> ₹" + 0 + " </dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("");
                    sb.AppendLine("<div class='d-grid gap-2 my-3'>");
                    //sb.AppendLine("<a class='btn btn-success w-100'> Make Purchase </a>");
                    sb.AppendLine("<a href='/Home/OrderPage' class='btn btn-light w-100'>Track Orders</a>");
                    sb.AppendLine("<a href='/Home/HomePage' class='btn btn-light w-100'> Back to Home </a>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("</div> <!-- card-body.// -->");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<!-- card.// -->");
                    sb.AppendLine("</aside> <!-- col.// -->");
                    sb.AppendLine("</div>");
                }
                Response.StringReponse = sb.ToString();
                Response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetAddToCartDataOnAddToCartPagestring",ex.Message.ToString(),0);
            }
            return Response;
        }
        public List<SubCategoryModel> GetAllCategoryForSearchFilter(long? CategoryId)
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@CategoryId",CategoryId)
                                            };

                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetAllCategoryForSearchFilter @CategoryId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetAllCategoryForSearchFilter",ex.Message.ToString(),0);
            }
            return SubCategoryList;
        }
        public List<SubCategoryModel> GetSubCategoryForSearchFilter(long? CategoryId,long? SubCategoryId)
        {
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId)
                                                
                                            };

                SubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetSubCategoryForSearchFilter @CategoryId,@SubCategoryId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetSubCategoryForSearchFilter",ex.Message.ToString(),0);
            }
            return SubCategoryList;
        }
        public SubCategoryModel GetSubCategoryForSubCategoryPage(long? SubCategoryId)
        {
            SubCategoryModel SubCategoryModel = new SubCategoryModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId)
                                            };

                SubCategoryModel = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetSubCategoryForSubCategoryPage @SubCategoryId",parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetSubCategoryForSubCategoryPage",ex.Message.ToString(), 0);
            }
            return SubCategoryModel;
        }
        public List<Sub_CategoryImageModel> GetSubCategoryColorStyleForSubCategoryPage(long? SubCategoryId,long? SubCategoryColorId)
        {
            List<Sub_CategoryImageModel> Sub_CategoryColorDetailsList = new List<Sub_CategoryImageModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubCategoryColorId",SubCategoryColorId),
                                            };

                Sub_CategoryColorDetailsList = DbEngineObj.Database.SqlQuery<Sub_CategoryImageModel>("GetSubCategoryColorStyleForSubCategoryPage @SubCategoryId,@SubCategoryColorId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetSubCategoryColorStyleForSubCategoryPage",ex.Message.ToString(), 0);
            }
            return Sub_CategoryColorDetailsList;
        }
        public List<SubCategoryAboutItem> GetSubCategoryAboutItemList(long? SubCategoryId)
        {
            List<SubCategoryAboutItem> SubCategoryAboutItemList = new List<SubCategoryAboutItem>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                            };

                SubCategoryAboutItemList = DbEngineObj.Database.SqlQuery<SubCategoryAboutItem>("GetSub_CategoryAboutItemList @SubCategoryId",parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetSubCategoryAboutItem",ex.Message.ToString(),0);
            }
            return SubCategoryAboutItemList;
        }
        public Sub_CategoryImageModel GetSubCategorySideImage(long? SubCategoryId, long? SubCategoryColorId)
        {
            Sub_CategoryImageModel Sub_CategoryImageModel = new Sub_CategoryImageModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubCategoryColorId",SubCategoryColorId),
                                            };

                Sub_CategoryImageModel = DbEngineObj.Database.SqlQuery<Sub_CategoryImageModel>("GetSubCategorySideImage @SubCategoryId,@SubCategoryColorId", parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetSubCategorySideImage",ex.Message.ToString(),0);
            }
            return Sub_CategoryImageModel;
        }
        public List<SubCategoryImageList> GetSubCategorySideImageList(long? SubCategoryId, long? SubCategoryColorId)
        {
            List<SubCategoryImageList> SubCategoryImageList = new List<SubCategoryImageList>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubCategoryColorId",SubCategoryColorId),
                                            };

                SubCategoryImageList = DbEngineObj.Database.SqlQuery<SubCategoryImageList>("GetSubCategorySideImageList @SubCategoryId,@SubCategoryColorId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetSubCategorySideImageList",ex.Message.ToString(),0);
            }
            return SubCategoryImageList;
        }
        public List<SubCategoryReviewAndStarsModel> GetReviewList(long? SubCategoryId)
        {
            List<SubCategoryReviewAndStarsModel> SubCategoryReviewAndStarsList = new List<SubCategoryReviewAndStarsModel>();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId)
                                            };

                SubCategoryReviewAndStarsList = DbEngineObj.Database.SqlQuery<SubCategoryReviewAndStarsModel>("GetReviewList @SubCategoryId",parameters).ToList();
            }
            catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetReviewList",ex.Message.ToString(),0);
            }
            return SubCategoryReviewAndStarsList;
        }
        public List<Sub_CategorySizeDetailsModel> GetSubCategorySizeDetails(long? SubCategoryId,long? SubCategorySizeId,long? SubCategoryImageId)
        {
            List<Sub_CategorySizeDetailsModel> Sub_CategorySizeDetailsList = new List<Sub_CategorySizeDetailsModel>();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubCategorySizeId",SubCategorySizeId),
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId)
                                            };

                Sub_CategorySizeDetailsList = DbEngineObj.Database.SqlQuery<Sub_CategorySizeDetailsModel>("GetSubCategorySizeDetails @SubCategoryId,@SubCategorySizeId,@SubCategoryImageId",parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetSubCategorySizeDetails",ex.Message.ToString(), 0);
            }
            return Sub_CategorySizeDetailsList;
        }
        public List<Sub_CategorySizeDetailsModel> GetNoOfSizeDataByImageId(long? SubCategoryId, long? SubCategoryImageId)
        {
            List<Sub_CategorySizeDetailsModel> Sub_CategorySizeDetailsList = new List<Sub_CategorySizeDetailsModel>();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId)
                                            };

                Sub_CategorySizeDetailsList = DbEngineObj.Database.SqlQuery<Sub_CategorySizeDetailsModel>("GetNoOfSizeDataByImageId @SubCategoryId,@SubCategoryImageId",parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","GetNoOfSizeDataByImageId",ex.Message.ToString(),0);
            }
            return Sub_CategorySizeDetailsList;
        }
        public List<SubCategoryReviewAndStarsModel> StarsFilterRating(long? SubCategoryId,long? StarsFilterRating)
        {
            List<SubCategoryReviewAndStarsModel> SubCategoryReviewAndStarsList = new List<SubCategoryReviewAndStarsModel>();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@StarsFilterRating",StarsFilterRating)
                                            };

                SubCategoryReviewAndStarsList = DbEngineObj.Database.SqlQuery<SubCategoryReviewAndStarsModel>("GetDataByStarsFilterRating @SubCategoryId,@StarsFilterRating", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCategory","StarsFilterRating",ex.Message.ToString(), 0);
            }
            return SubCategoryReviewAndStarsList;
        }
        public List<GetSubCategoryReviewAndStarsChartModel> GetSubCategoryReviewAndStarsChart(long? SubCategoryId)
       {
            List<GetSubCategoryReviewAndStarsChartModel> GetSubCategoryReviewAndStarsChartModelList = new List<GetSubCategoryReviewAndStarsChartModel>();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                            };

                GetSubCategoryReviewAndStarsChartModelList = DbEngineObj.Database.SqlQuery<GetSubCategoryReviewAndStarsChartModel>("GetSubCategoryReviewAndStarsChart @SubCategoryId",parameters).ToList();
                GetSubCategoryReviewAndStarsChartModelList.OrderByDescending(s => s.GivenStars).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCategory","GetSubCategoryReviewAndStarsChart",ex.Message.ToString(),0);
            }
            return GetSubCategoryReviewAndStarsChartModelList;
        }
        public string GetStarsGridChart(List<GetSubCategoryReviewAndStarsChartModel> SubCategoryReviewAndStarsChartList)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            string Result = "";
            StringBuilder SB = new StringBuilder();
            //List<Impulse.DBAccessLayer.Stars> StarsList = new List<Impulse.DBAccessLayer.Stars>();
            dynamic StarsList = DbEngineObj.Stars.OrderByDescending(s => s.Id).Select(s => s.Id).ToList();
            var StarsList1 = new List<Impulse.DBAccessLayer.Stars>();
            Impulse.DBAccessLayer.Stars Stars = new Impulse.DBAccessLayer.Stars();
            for(var Sr = 0;Sr < StarsList.Count;Sr++)
            {
                var StarsModel = new Impulse.DBAccessLayer.Stars();
                StarsModel.Id = StarsList[Sr];
                StarsList1.Add(StarsModel);
            }

            if (SubCategoryReviewAndStarsChartList.Count == 0)
            {
                SB.Append("<div class='small-space text-center'>");
                SB.Append("<span>0 out of 5 stars</span>");
                SB.Append("</div>");

                SB.Append("<table class='small-space' id='histogramTable'>");

                SB.Append("<tr>");
                SB.Append("<td class='rating-number'>");
                SB.Append("<span><a title='0% of reviews have 5 stars'>5 star</a> </span>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("</td>");
                SB.Append("<td class='histoBar'>");
                SB.Append("<a title='0 % Of reviews have 5 stars'>");
                SB.Append("<div class='histo-meter' aria-label='0%'>");
                SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                SB.Append("</div>");
                SB.Append("</a>");
                SB.Append("</td>");
                SB.Append("<td class='rating-percent'>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("<span class='a-size-small'>0%</span>");
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td class='rating-number'>");
                SB.Append("<span><a title='0% of reviews have 5 stars'>4 star</a> </span>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("</td>");
                SB.Append("<td class='histoBar'>");
                SB.Append("<a title='0 % Of reviews have 4 stars'>");
                SB.Append("<div class='histo-meter' aria-label='0%'>");
                SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                SB.Append("</div>");
                SB.Append("</a>");
                SB.Append("</td>");
                SB.Append("<td class='rating-percent'>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("<span class='a-size-small'>0%</span>");
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td class='rating-number'>");
                SB.Append("<span><a title='0% of reviews have 3 stars'>3 star</a> </span>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("</td>");
                SB.Append("<td class='histoBar'>")    ;
                SB.Append("<a title='0 % Of reviews have 3 stars'>")   ;
                SB.Append("<div class='histo-meter' aria-label='0%'>")  ;
                SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                SB.Append("</div>");
                SB.Append("</a>") ;
                SB.Append("</td>");
                SB.Append("<td class='rating-percent'>")    ;
                SB.Append("<span class='letter-space'></span>") ;
                SB.Append("<span class='a-size-small'>0%</span>");
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td class='rating-number'>");
                SB.Append("<span><a title='0% of reviews have 2 stars'>3 star</a> </span>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("</td>");
                SB.Append("<td class='histoBar'>");
                SB.Append("<a title='0 % Of reviews have 2 stars'>");
                SB.Append("<div class='histo-meter' aria-label='0%'>");
                SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                SB.Append("</div>");
                SB.Append("</a>");
                SB.Append("</td>");
                SB.Append("<td class='rating-percent'>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("<span class='a-size-small'>0%</span>");
                SB.Append("</td>");
                SB.Append("</tr>");


                SB.Append("<tr>");
                SB.Append("<td class='rating-number'>");
                SB.Append("<span><a title='0% of reviews have 1 stars'>1 star</a> </span>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("</td>");
                SB.Append("<td class='histoBar'>");
                SB.Append("<a title='0 % Of reviews have 1 stars'>");
                SB.Append("<div class='histo-meter' aria-label='0%'>");
                SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                SB.Append("</div>");
                SB.Append("</a>");
                SB.Append("</td>");
                SB.Append("<td class='rating-percent'>");
                SB.Append("<span class='letter-space'></span>");
                SB.Append("<span class='a-size-small'>0%</span>");
                SB.Append("</td>");
                SB.Append("</tr>");
                SB.Append("</table> ");
            } else
            {
                SB.Append("<table class='small-space' id='histogramTable'>");
                SB.Append("<div class='small-space text-center'>");
                SB.Append("<span> " + SubCategoryReviewAndStarsChartList[0].averageStar + " out of 5 stars</span>");
                SB.Append("</div>");
                for (var Index1 = 0; Index1 < StarsList1.Count; Index1++)
                {
                   for (var Index = 0;Index < SubCategoryReviewAndStarsChartList.Count;Index++)
                    {
                        if (StarsList1[Index1].Id == 5 && SubCategoryReviewAndStarsChartList[Index].GivenStars == 5)
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "% of reviews have 5 stars'>5 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + " % Of reviews have 5 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //break;
                        } else if(StarsList1[Index1].Id == 5 && SubCategoryReviewAndStarsChartList[Index].GivenStars == null) /*&& SubCategoryReviewAndStarsChartList[Index].GivenStars != 5*/
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='0% of reviews have 5 stars'>5 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='0 % Of reviews have 5 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='0%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>0%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            // break;
                        }

                        if (StarsList1[Index1].Id == 4 && SubCategoryReviewAndStarsChartList[Index].GivenStars == 4)
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "% of reviews have  stars'>4 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + " % Of reviews have 4 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //   break;
                        }
                        else if (StarsList1[Index1].Id == 4 && SubCategoryReviewAndStarsChartList[Index].GivenStars == null) /*&& SubCategoryReviewAndStarsChartList[Index].GivenStars != 4*/
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='0% of reviews have 5 stars'>4 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='0 % Of reviews have 4 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='0%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>0%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //    break;
                        }

                        if (StarsList1[Index1].Id == 3 && SubCategoryReviewAndStarsChartList[Index].GivenStars == 3)
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "% of reviews have  stars'>3 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + " % Of reviews have 3 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //    break;
                        }
                        else if (StarsList1[Index1].Id == 3 && SubCategoryReviewAndStarsChartList[Index].GivenStars == null) /*&& SubCategoryReviewAndStarsChartList[Index].GivenStars != 3*/
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='0% of reviews have 3 stars'>3 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='0 % Of reviews have 3 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='0%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>0%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //   break;
                        }

                        if (StarsList1[Index1].Id == 2 && SubCategoryReviewAndStarsChartList[Index].GivenStars == 2)
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "% of reviews have  stars'>2 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + " % Of reviews have 2 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //  break;
                        }
                        else if (StarsList1[Index1].Id == 2 && SubCategoryReviewAndStarsChartList[Index].GivenStars == null) /*&& SubCategoryReviewAndStarsChartList[Index].GivenStars != 2*/
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='0% of reviews have 2 stars'>3 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='0 % Of reviews have 2 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='0%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>0%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //   break;
                        }

                        if (StarsList1[Index1].Id == 1 && SubCategoryReviewAndStarsChartList[Index].GivenStars == 1)
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "% of reviews have  stars'>1 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + " % Of reviews have 3 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>" + SubCategoryReviewAndStarsChartList[Index].Parcentage + "%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //   break;
                        }
                        else if (StarsList1[Index1].Id == 1 && SubCategoryReviewAndStarsChartList[Index].GivenStars == null) /*&& SubCategoryReviewAndStarsChartList[Index].GivenStars != 1*/
                        {
                            SB.Append("<tr>");
                            SB.Append("<td class='rating-number'>");
                            SB.Append("<span><a title='0% of reviews have 1 stars'>1 star</a> </span>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("</td>");
                            SB.Append("<td class='histoBar'>");
                            SB.Append("<a title='0 % Of reviews have 1 stars'>");
                            SB.Append("<div class='histo-meter' aria-label='0%'>");
                            SB.Append("<div class='histo-meter-filled' style='width:0%'></div>");
                            SB.Append("</div>");
                            SB.Append("</a>");
                            SB.Append("</td>");
                            SB.Append("<td class='rating-percent'>");
                            SB.Append("<span class='letter-space'></span>");
                            SB.Append("<span class='a-size-small'>0%</span>");
                            SB.Append("</td>");
                            SB.Append("</tr>");
                            //  break;
                        }
                    }
                }
                SB.Append("</table> ");
            }
            Result = SB.ToString();
            return Result;
        }
        public long? GetSubcategoryRatingBySubCategoryId(long? subCategoryId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryobj = new Impulse.BAL.Access.AcqCategory();
            long? FiveTotalRatingOfThatRate = 0;
            long? FourTotalRatingOfThatRate = 0;
            long? ThreeTotalRatingOfThatRate = 0;
            long? TwoTotalRatingOfThatRate = 0;
            long? OneTotalRatingOfThatRate = 0;
            long? SumOfTotalRatingOfThatRate = 0;
            long? AvgStar = 0;
            
            try
            {
                
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryobj.GetSubCategoryReviewAndStarsChart(subCategoryId);
                for (var Index = 0; Index < SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList.Count; Index++)
                {
                    if (SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].GivenStars == 5)
                    {
                        FiveTotalRatingOfThatRate = 5 * SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].FiveStarCount;
                    }
                    if (SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].GivenStars == 4)
                    {
                        FourTotalRatingOfThatRate = SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].FourStarCount;
                    }
                    if (SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].GivenStars == 3)
                    {
                        ThreeTotalRatingOfThatRate = SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].ThreeStarCount;
                    }
                    if (SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].GivenStars == 2)
                    {
                        TwoTotalRatingOfThatRate = SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].TwoStarCount;
                    }
                    if (SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].GivenStars == 1)
                    {
                        OneTotalRatingOfThatRate = SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[Index].OneStarCount;
                    }
                }
                SumOfTotalRatingOfThatRate = (FiveTotalRatingOfThatRate + FourTotalRatingOfThatRate + ThreeTotalRatingOfThatRate + TwoTotalRatingOfThatRate + OneTotalRatingOfThatRate);

                for (var I = 0; I < SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList.Count; I++)
                {
                    AvgStar = SumOfTotalRatingOfThatRate / SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[0].TotalStarCount;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return AvgStar;
        }
        public string GetAvaliableSizeBySubCategoryId(long? subCategoryId)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            string Result = "";
            try
            {
                var SizeIdBySubcategory = DbEngineObj.Sub_CategorySizeDetails.Where(
                                                                                    s => s.SubCategoryId == subCategoryId 
                                                                                        && s.IsAvaliable == true 
                                                                                        && s.IsActive == true
                                                                                    ).FirstOrDefault();

                var Size = DbEngineObj.Size.Where(s => s.CategoryId == SizeIdBySubcategory.CategoryId
                                                                      && s.SizeId == SizeIdBySubcategory.SubCategorySizeId
                                                                      && s.IsActive == true
                                                                      && s.IsActive == true).FirstOrDefault();


                Result = Size.SizeName;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Result;
        }
        public long? GetAvaliableSizeIdBySizeName(long? CategoryId,string SizeName)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            long? Result = 0;
            try
            {
                var Size = DbEngineObj.Size.Where(s => s.CategoryId == CategoryId
                                                                      && s.SizeName == SizeName
                                                                      && s.IsActive == true
                                                                      ).FirstOrDefault();


                Result = Size.SizeId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Result;
        }
        public string GetSizeNameBySizeId(long? CategoryId,long? SizeId)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            string Result = "";
            try
            {
                var Size = DbEngineObj.Size.Where(s => s.CategoryId == CategoryId
                                                                      && s.SizeId == SizeId
                                                                      && s.IsActive == true
                                                                      ).FirstOrDefault();


                Result = Size.SizeName;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Result;
        }
        public string GetColorNameByColorId(long? ColorId)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            string Result = "";
            try
            {
                var color = DbEngineObj.Color.Where(s =>               
                                                        s.ColorId == ColorId
                                                        && s.IsActive == true
                                                                      ).FirstOrDefault();


                Result = color.ColorName;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Result;
        }
        public string GetImagePathByImageColorId(long? SubCategoryId,long? SubCategoryImageColorId)
        {
            try
            {
                DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
                string Result = "";
                try
                {
                    var Sub_CategoryImage = DbEngineObj.Sub_CategoryImage.Where(s =>
                                                            s.SubCategoryId == SubCategoryId  && 
                                                            s.SubcategoryImageColorId == SubCategoryImageColorId && 
                                                            s.IsActive == true
                                                           ).FirstOrDefault();


                    Result = Sub_CategoryImage.SubCategoryImagePath;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                return Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public long? GetWishListCookiesIdentityId(List<WistListModel> WistList)
        {
            long? GetMaxId = 0;
            try
            {
                GetMaxId = (WistList.Max(s => (long?)s.WishListCookiesId) == null) ? 0 : WistList.Max(s => s.WishListCookiesId);
                GetMaxId = GetMaxId + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxId;
        }
    }
}