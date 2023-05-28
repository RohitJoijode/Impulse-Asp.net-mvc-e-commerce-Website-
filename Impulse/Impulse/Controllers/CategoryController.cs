using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Impulse.DAL;
using Impulse.BAL;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Impulse.BAL.Update;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Text;
using Newtonsoft.Json;

namespace Impulse.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public JsonResult GetTopDealsSubCategoryList()
        {
            JsonResponse Response = new JsonResponse();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                SubCategoryList = AcqCategoryObj.GetTopDealsSubCategoryList();
                if (SubCategoryList.Count != 0)
                {
                    Response.StringReponse = AcqCategoryObj.GetTopDealsSubCategorystring(SubCategoryList);
                    Response.IsSuccess = true;
                }
            }
            catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","GetTopDealsSubCategoryList()",ex.Message.ToString(),0);
            }
            if (SubCategoryList.Count != 0)
                return Json(Response);
            else
                return Json( new JsonResponse { IsSuccess = false });
        }
        public JsonResult GetTopDealsSubCategoryListOnTopDealPage()
        {
            JsonResponse Response = new JsonResponse();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                SubCategoryList = AcqCategoryObj.GetTopDealsSubCategoryList();
                if (SubCategoryList.Count != 0)
                {
                    Response.StringReponse = AcqCategoryObj.GetTopDealsSubCategoryListOnTopDealPageString(SubCategoryList);
                    Response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "CategoryController", "GetTopDealsSubCategoryList()", ex.Message.ToString(), 0);
            }
            return Json(Response);
        }
        public JsonResult GetRecommendSubCategoryList()
        {
            JsonResponse Response = new JsonResponse();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                SubCategoryList = AcqCategoryObj.GetIsRecommendSubCategoryList();
                if (SubCategoryList.Count != 0)
                {
                    Response.StringReponse = AcqCategoryObj.GetRecommendSubCategorystring(SubCategoryList);
                    Response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Response.IsSuccess = false;
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","GetTopDealsSubCategoryList()",ex.Message.ToString(),0);
            }
            return Json(Response);
        }
        public ActionResult SaveToAddToCartOnWistListPage(WistListModel WistListModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            SubCategoryObject.WistListModel = WistListModel;
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    long? CategoryId = 0;
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    #region WistList Disable Before AddToList
                    Response = UpdCategoryObj.UpdateFlagWistListOnWistListPage(WistListModel,WistListModel.Sub_CategoryId,WistListModel.Id,Convert.ToInt64(UserId));
                    #endregion
                    var LogInUserId = Convert.ToInt64(UserId);
                    if (Response.IsSuccess == true)
                    {
                        if(Response.StringReponse != null)
                        {
                           CategoryId = Convert.ToInt64(Response.StringReponse);
                        }
                        Response = UpdCategoryObj.SaveToAddToCartOnWistListPage(SubCategoryObject,WistListModel.Sub_CategoryId,CategoryId,WistListModel.Id,Convert.ToInt64(UserId));
                    }

                    var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == LogInUserId
                                            select a).Count();
                    
                    Response.WishListCount = WistListCount.ToString();
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","SaveToAddToCart()",ex.Message.ToString(),0);
            }
            return Json(Response);
        }
        public ActionResult SaveToWishList(long? SubCategoryId,long? CategoryId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    var LogInUserId = Convert.ToInt64(UserId);
                    Response = UpdCategoryObj.SaveToWishList(SubCategoryId,CategoryId,Convert.ToInt64(UserId));
                    if(Response.IsSuccess == true)
                    {
                        var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == LogInUserId
                                            select a).Count();
                        if(WistListCount != 0)
                        {
                            Response.StringReponse = WistListCount.ToString();
                        }
                    }
                }
                else
                {

                    WistListModel WistListModel = new WistListModel();
                    WistListModel.Sub_CategoryId = SubCategoryId;
                    WistListModel.CategoryId = CategoryId;
                    //#region Wish List Store Sub Category Ids in Cookies
                    List<WistListModel> WistList = new List<WistListModel>();
                    List<WistListModel> WistListCookies = new List<WistListModel>();
                    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                    if(cookie != null)
                    {
                        WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                    }
                    
                    if(WistListCookies.Count == 0)
                    {
                       WistListModel.WishListCookiesId = AcqCategoryObj.GetWishListCookiesIdentityId(WistListCookies);
                       WistList.Add(WistListModel);
                       //#region Store Wish List Data In Array Object
                       string WLCookies = JsonConvert.SerializeObject(WistList);
                       System.Web.HttpContext.Current.Response.Cookies.Add(
                               new HttpCookie("WishListCookies",WLCookies)
                               {
                                   Expires = DateTime.Now.AddDays(365)
                               }
                       );
                       Response.WishListCookiesCount = Convert.ToString(1);
                       Response.IsSuccess = true;
                       //#endregion
                   }
                   else
                   {
                            WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                            if (WistListCookies.Count > 0)
                            {
                                WistListModel.WishListCookiesId = AcqCategoryObj.GetWishListCookiesIdentityId(WistListCookies);
                                WistListCookies.Add(WistListModel);
                                string WLCookies = JsonConvert.SerializeObject(WistListCookies);
                                System.Web.HttpContext.Current.Response.Cookies.Add(
                                   new HttpCookie("WishListCookies",WLCookies)
                                   {
                                       Expires = DateTime.Now.AddDays(365)
                                   }
                                );
                            }

                            Response.WishListCookiesCount = Convert.ToString(WistListCookies.Count);
                            Response.IsSuccess = true;
                   }
                }
                //#endregion
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","SaveToAddToCart()",ex.Message.ToString(),0);
            }
            return Json(Response);
        }
        public JsonResult SearchFilterOnTopDealsPage(string Min,string Max,string Stars)
        {
            JsonResponse Response = new JsonResponse();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                SubCategoryList = AcqCategoryObj.SearchFilterOnTopDealsPage(Convert.ToDecimal(Min),Convert.ToDecimal(Max), Convert.ToInt64(Stars));
                if (SubCategoryList.Count != 0)
                {
                    Response.StringReponse = AcqCategoryObj.GetTopDealsSubCategorystring(SubCategoryList);
                    Response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "CategoryController", "GetTopDealsSubCategoryList()", ex.Message.ToString(), 0);
            }
            return Json(Response);
        }
        public JsonResult GetAutoCompleteSearchFilter()
        {
            JsonResponse Response = new JsonResponse();
            List<SubCategoryKeyWordsModel> SubCategoryKeyWordsList = new List<SubCategoryKeyWordsModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                SubCategoryKeyWordsList = AcqCommonObj.GetSubCategoryKeyWordDropDownForSearchFilter();
            }
            catch (Exception ex)
            {
                  Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                  UpdCommon.Error_Log(0,"CategoryController","GetAutoCompleteSearchFilterOnTopDealsPage()",ex.Message.ToString(),0);
            }
            return Json(SubCategoryKeyWordsList);
        }
        public JsonResult GetCookiesWistListOnWistListPage() 
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            List<WistListModel> WistList = new List<WistListModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                Impulse.BAL.Access.AcqCategory AcqCategory = new Impulse.BAL.Access.AcqCategory();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }
                else
                {
                    HttpCookie myCookie = Request.Cookies["WishListCookies"];
                    if (myCookie != null)
                    {
                            WistList = JsonConvert.DeserializeObject<List<WistListModel>>(myCookie.Value);
                            if(WistList.Count > 0)
                            {
                                Response = AcqCategory.GetCookiesWistListOnWistListPage(WistList);
                            } else
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine("<div class='row'>");
                                sb.AppendLine("<div class='col-lg-9'>");
                                sb.AppendLine("<div class='card'>");
                                sb.AppendLine("<div class='content-body'>");
                                sb.AppendLine("<h4 class='card-title mb-4'>Your Wish List shopping cart (0)</h4>");
                                sb.AppendLine("<article class='row gy-3 mb-4'>");
                                sb.AppendLine("<div class='col-lg-5'>");
                                sb.AppendLine("</div>");
                                sb.AppendLine("</article>");
                                sb.AppendLine("</div> ");
                                sb.AppendLine("</div>");
                                sb.AppendLine("</div>");
                                sb.AppendLine("</div>");
                                Response.StringReponse = sb.ToString();
                                Response.IsSuccess = true;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","GetCookiesWistListOnWistListPage()",ex.Message.ToString(), 0);
            }
            return Json(Response);
        }
        public JsonResult GetWistListDataOnWistListPage()
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                Impulse.BAL.Access.AcqCategory AcqCategory = new Impulse.BAL.Access.AcqCategory();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = AcqCategoryObj.GetWistListDataOnWistListPage(Convert.ToInt64(UserId));
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","GetCookiesWistListOnWistListPage()",ex.Message.ToString(),0);
            }
            return Json(Response);
        }
        public JsonResult UpdateWistListDataForWistListPage(SubCategoryModel SubCategoryModel)
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {

                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = UpdCategoryObj.UpdateDeleteWistListDataForWistListPage(SubCategoryModel,Convert.ToInt64(UserId));
                    if(Response.IsSuccess == true)
                    {
                        Response = AcqCategoryObj.GetWistListDataOnWistListPage(Convert.ToInt64(UserId));
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(Response);
        }
        public JsonResult DeleteWishListItemFromWishListPage(long? WishListCookiesId) //List<WistListModel> WistList
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            //string WistListCookiesSubCategoryIds = "";
            List<WistListModel> WistList = new List<WistListModel>();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }
                else
                {
                    HttpCookie myCookie = Request.Cookies["WishListCookies"];
                    if (myCookie != null)
                    {

                        HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                        if (cookie != null)
                        {
                            WistList = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                            var Item = WistList.Remove(WistList.Find(x => x.WishListCookiesId == WishListCookiesId));
                        }

                        var cookie1 = new HttpCookie("WishListCookies")
                         {
                                    Expires = DateTime.Now.AddDays(-1)
                         };
                         System.Web.HttpContext.Current.Response.SetCookie(cookie1);
                        
                        string WLCookies = JsonConvert.SerializeObject(WistList);
                        System.Web.HttpContext.Current.Response.Cookies.Add(
                           new HttpCookie("WishListCookies",WLCookies)
                           {
                               Expires = DateTime.Now.AddDays(365)
                           }
                       );
                    }

                    Response.IsSuccess = true;
                    Response.StringReponse = Convert.ToString(WistList.Count);

                    //HttpCookie myCookie = Request.Cookies["WishListCookies"];
                    //if (myCookie != null)
                    // {
                    //    if (myCookie.Value != null)
                    //    {
                    //        for(var Index = 0;Index < SubCategoryIdList.Count();Index++)
                    //        {
                    //            if (SubCategoryIdList[0].DeletedSubCategoryId == SubCategoryIdList[Index].SubCategoryId && Index == Convert.ToInt64(SubCategoryIdList[0].UniqueId))
                    //            {

                    //            } else
                    //            {
                    //                if (WistListCookiesSubCategoryIds == "")
                    //                {
                    //                    WistListCookiesSubCategoryIds = SubCategoryIdList[Index].SubCategoryId;
                    //                }
                    //                else if (WistListCookiesSubCategoryIds != "")
                    //                {
                    //                    WistListCookiesSubCategoryIds = WistListCookiesSubCategoryIds + "," + SubCategoryIdList[Index].SubCategoryId;
                    //                }
                    //            }
                    //        }

                    //        var cookie = new HttpCookie("WishListSubCategroyIds")
                    //        {
                    //            Expires = DateTime.Now.AddDays(-1)
                    //        };
                    //        System.Web.HttpContext.Current.Response.SetCookie(cookie);
                    //        if(WistListCookiesSubCategoryIds != "")
                    //        {
                    //            var NewcookieStore = new HttpCookie("WishListSubCategroyIds", WistListCookiesSubCategoryIds)
                    //            {
                    //                Expires = DateTime.Now.AddDays(365)
                    //            };
                    //            System.Web.HttpContext.Current.Response.SetCookie(NewcookieStore);
                    //            Response.StringReponse = WistListCookiesSubCategoryIds;
                    //        } else if (WistListCookiesSubCategoryIds == "")
                    //        {
                    //            var cookie1 = new HttpCookie("WishListSubCategroyIds")
                    //            {
                    //                Expires = DateTime.Now.AddDays(-1)
                    //            };
                    //            System.Web.HttpContext.Current.Response.SetCookie(cookie1);
                    //            Response.WishListCount = "0";
                    //        }
                    //   }
                    //}
                    //Response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","DeleteWishListItemFromWishListPage()",ex.Message.ToString(),0);
            }
            return Json(Response);
        }
        public JsonResult GetAddToCartDataOnAddToCartPage()
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {
                Impulse.BAL.Access.AcqCategory AcqCategory = new Impulse.BAL.Access.AcqCategory();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = AcqCategoryObj.GetAddToCartDataOnAddToCartPage(Convert.ToInt64(UserId));
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","GetAddToCartDataOnAddToCartPage()", ex.Message.ToString(), 0);
            }
            return Json(Response);
        }
        public JsonResult DeleteAddToCartItem(SubCategoryModel SubCategoryModel)
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {

                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = UpdCategoryObj.DeleteAddToCartItem(SubCategoryModel,Convert.ToInt64(UserId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(Response);
        }
        public JsonResult CreateOrders(SubCategoryModel SubCategoryModel)
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            try
            { 
            
            } catch(Exception ex)
            {
                throw ex;
            }
            return Json(Response);
        }
        /// <summary>
        /// Below Method For Testing Purpose 
        /// </summary>
        public void demo()
        {
            string cell = "ABCD4321";
            int row,a = getIndexofNumber(cell);
            string Numberpart = cell.Substring(a,cell.Length - a);
            row = Convert.ToInt32(Numberpart);
            string Stringpart = cell.Substring(0,a);
        }
        /// <summary>
        /// Below Method For Testing Purpose 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public int getIndexofNumber(string cell)
        {
            int indexofNum = -1;
            foreach (char c in cell)
            {
                indexofNum++;
                if (Char.IsDigit(c))
                {
                    return indexofNum;
                }
            }
            return indexofNum;
        }
        public JsonResult SaveToAddToCart(long? SubCategoryId,long? CategoryId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    var LogInUserId = Convert.ToInt64(UserId);

                    var WistListCount = (
                                          from a in DbEngineObj.WishList
                                          where a.IsWishList == true && a.IsActive == true && a.UserId == LogInUserId
                                          select a).Count();

                    Response = UpdCategoryObj.SaveToAddToCart(SubCategoryId,CategoryId,LogInUserId);
                    
                    if (WistListCount != 0)
                        Response.WishListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        Response.WishListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == LogInUserId select a).Count();
                    if (AddToCartCount != 0)
                        Response.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        Response.WishListCount = 0.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(Response);
        }
        public ActionResult HomePartialPage()
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    var LogInUserId = Convert.ToInt64(UserId);

                    var WistListCount = (
                                          from a in DbEngineObj.WishList
                                          where a.IsWishList == true && a.IsActive == true && a.UserId == LogInUserId
                                          select a).Count();

                    if (WistListCount != 0)
                        Response.WishListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        Response.WishListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == LogInUserId select a).Count();
                    if (AddToCartCount != 0)
                        Response.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        Response.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == LogInUserId select a).Count();

                    if (OrderListCount != 0)
                        Response.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        Response.OrderListCount = 0.ToString();
                }

                SubCategoryObject.CategoryList = AcqCategoryObj.GetHomePageCategoryData();
                SubCategoryObject.SubCategoryList = AcqCategoryObj.GetHomePageSubCategoryData();

                for (var Index = 0;Index < SubCategoryObject.SubCategoryList.Count;Index++)
                {
                    SubCategoryObject.SubCategoryList[Index].EncryptedSub_CategoryId = AcqCommonObj.encrypt(Convert.ToString(SubCategoryObject.SubCategoryList[Index].Sub_CategoryId),"");
                    SubCategoryObject.SubCategoryList[Index].EncryptedSub_Category_DefaultColorId = AcqCommonObj.encrypt(Convert.ToString(SubCategoryObject.SubCategoryList[Index].Sub_Category_DefaultColorId),"");
                    SubCategoryObject.SubCategoryList[Index].AvgStar = AcqCategoryObj.GetSubcategoryRatingBySubCategoryId(SubCategoryObject.SubCategoryList[Index].Sub_CategoryId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_HomePage",SubCategoryObject);
        }
        public ActionResult SubCategoryPartialPage(long? SubCategoryId,long? SubCategoryColorId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            SubCategoryObject.UsersModel = new UsersModel();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    var LogInUserId = Convert.ToInt64(UserId);
                    SubCategoryObject.userId = LogInUserId;
                    SubCategoryObject.UsersModel = new UsersModel();
                    var WistListCount = (
                                          from a in DbEngineObj.WishList
                                          where a.IsWishList == true && a.IsActive == true && a.UserId == LogInUserId
                                          select a).Count();

                    if (WistListCount != 0)
                        SubCategoryObject.UsersModel.WistListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        SubCategoryObject.UsersModel.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == LogInUserId select a).Count();
                    if (AddToCartCount != 0)
                        SubCategoryObject.UsersModel.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        SubCategoryObject.UsersModel.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == LogInUserId select a).Count();

                    if (OrderListCount != 0)
                        SubCategoryObject.UsersModel.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        SubCategoryObject.UsersModel.OrderListCount = 0.ToString();
                } else
                {
                        SubCategoryObject.UsersModel.UserFullName = null;
                        SubCategoryObject.UsersModel.UserId = null;
                        SubCategoryObject.UsersModel.SubCategoryId = SubCategoryId;
                        SubCategoryObject.UsersModel.SubCategoryColorId = SubCategoryColorId;

                        HttpCookie myWistListCookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                        if (myWistListCookie != null)
                        {
                             List<WistListModel> WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(myWistListCookie.Value);
                             SubCategoryObject.UsersModel.WistListSubCategoryIdsForCookies = Convert.ToString(WistListCookies.Count); 
                        }

                    //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                    //if (myWistListCookie != null)
                    //{
                    //    if (myWistListCookie.Value != "")
                    //    {
                    //        if (myWistListCookie.Value != null)
                    //        {
                    //            SubCategoryObject.UsersModel.WistListSubCategoryIdsForCookies = "";
                    //            SubCategoryObject.UsersModel.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                    //        }
                    //    }
                    //    else if (myWistListCookie.Value == "")
                    //    {
                    //        SubCategoryObject.UsersModel.WistListSubCategoryIdsForCookies = "";
                    //        SubCategoryObject.UsersModel.WistListSubCategoryIdsForCookies = "0";
                    //    }

                    //}
                }
                ViewBag.SizeDropDownList = new SelectList(AcqCommonObj.GetSizeListByCategoryId((int)Impulse.DbEnum.Category.LadiesT_Shirt),"SizeId","SizeName");
                SubCategoryObject.SubCategoryModel = AcqCategoryObj.GetSubCategoryForSubCategoryPage(SubCategoryId);
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
                SubCategoryObject.SubCategoryModel.AvgStar = AcqCategoryObj.GetSubcategoryRatingBySubCategoryId(SubCategoryId);
                SubCategoryObject.Sub_CategoryImageModelList = AcqCategoryObj.GetSubCategoryColorStyleForSubCategoryPage(SubCategoryId,SubCategoryColorId);
                for (var Index = 0; Index < SubCategoryObject.Sub_CategoryImageModelList.Count; Index++)
                {
                    if (SubCategoryObject.Sub_CategoryImageModelList[Index].SubcategoryImageColorId == SubCategoryColorId)
                    {
                        SubCategoryObject.SubCategoryModel.SubCategorySelectedImageId = SubCategoryObject.Sub_CategoryImageModelList[Index].SubCategoryImageId;
                    }
                }
                SubCategoryObject.SubCategoryModel.Sub_CategoryId = SubCategoryId;
                SubCategoryObject.SubCategoryModel.Sub_Category_DefaultColorId = SubCategoryColorId;
                SubCategoryObject.SubCategoryAboutItemList = AcqCategoryObj.GetSubCategoryAboutItemList(SubCategoryId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_SubCategoryPage",SubCategoryObject);
        }
        public ActionResult SubCategorySidePartialImageDisplayPage(long? SubCategoryId,long? Sub_Category_DefaultColorId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            try
            {

                SubCategoryObject.Sub_CategoryImageModel = AcqCategoryObj.GetSubCategorySideImage(SubCategoryId,Sub_Category_DefaultColorId);
                SubCategoryObject.SubCategoryImageList = AcqCategoryObj.GetSubCategorySideImageList(SubCategoryId,Sub_Category_DefaultColorId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_SidePartialImage",SubCategoryObject);
        }
        public ActionResult ReviewPartialView(long? SubCategoryId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            try
            {
                //var ReviewData = (dynamic)null;
                var IsReviewEdit = false;
                var identity = (ClaimsIdentity)User.Identity;
                
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    long? UserId1 = Convert.ToInt64(UserId);
                    SubCategoryObject.userId = UserId1;
                    var ReviewData = DbEngineObj.SubCategoryReviewAndStars.Where(S => S.IsActive == true && S.Sub_CategoryId == SubCategoryId && S.UserId == UserId1).FirstOrDefault();
                    if (ReviewData != null)
                    {
                        SubCategoryObject.IsReviewEdit = true;
                    }
                    else
                    {
                        SubCategoryObject.IsReviewEdit = IsReviewEdit;
                    }
                }
                
                SubCategoryObject.SubCategoryReviewAndStarsList = AcqCategoryObj.GetReviewList(SubCategoryId);
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_ReviewPartialView",SubCategoryObject);
        }
        public ActionResult SaveReview(SubCategoryReviewAndStarsModel SubCategoryReviewAndStarsModel)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            JsonResponse Response = new JsonResponse();
            var ReviewData = (dynamic)null;
            var IsReviewEdit = false;
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    long? UserId1 = Convert.ToInt64(UserId);
                    SubCategoryObject.userId = UserId1;
                    Response = UpdCategoryObj.SaveReview(Convert.ToInt64(UserId),SubCategoryReviewAndStarsModel);
                    ReviewData = DbEngineObj.SubCategoryReviewAndStars.Where(S => S.IsActive == true && S.Sub_CategoryId == SubCategoryReviewAndStarsModel.Sub_CategoryId && S.UserId == UserId1).FirstOrDefault();
                }
                if (ReviewData != null)
                {
                    SubCategoryObject.IsReviewEdit = true;
                }
                else
                {
                    SubCategoryObject.IsReviewEdit = IsReviewEdit;
                }

                SubCategoryObject.SubCategoryReviewAndStarsList = AcqCategoryObj.GetReviewList(SubCategoryReviewAndStarsModel.Sub_CategoryId);
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryReviewAndStarsModel.Sub_CategoryId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("_ReviewPartialView",SubCategoryObject);
        }
        public ActionResult SubCategoryColorImagePartialPage()
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_SubCategoryColorImagePartialPage",SubCategoryObject);
        }
        public ActionResult SubCategorySizePartialPage()
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_SubCategorySizePartialPage",SubCategoryObject);
        }
        public JsonResult GetNoOfSizeDataByImageId(long? SubCategoryId,long? SubCategoryImageId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            DbEngine.DbEngine DbEngine = new DbEngine.DbEngine();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }
                dynamic Sub_Category = DbEngine.Sub_Category.Where(SC => SC.Sub_CategoryId == SubCategoryId).FirstOrDefault();
                SubCategoryObject.SizeList = AcqCommonObj.GetSizeListByCategoryId(Sub_Category.CategoryId);
                SubCategoryObject.Sub_CategorySizeDetailsList = AcqCategoryObj.GetNoOfSizeDataByImageId(SubCategoryId,SubCategoryImageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(SubCategoryObject);
        }
        public JsonResult GetSubCategorySizeDetails(long? SubCategoryId,long? SubCategorySizeId,long? SubCategoryImageId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }

                SubCategoryObject.Sub_CategorySizeDetailsList = AcqCategoryObj.GetSubCategorySizeDetails(SubCategoryId,SubCategorySizeId,SubCategoryImageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(SubCategoryObject);
        }
        public ActionResult StarsFilterRating(long? SubCategoryId,long? StarsFilterRating)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            var ReviewData = (dynamic)null;
            var IsReviewEdit = false;
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    long? UserId1 = Convert.ToInt64(UserId);
                    SubCategoryObject.userId = UserId1;
                    ReviewData = DbEngineObj.SubCategoryReviewAndStars.Where(S => S.IsActive == true && S.Sub_CategoryId == SubCategoryId && S.UserId == UserId1).FirstOrDefault();
                }
                if (ReviewData != null)
                {
                    SubCategoryObject.IsReviewEdit = true;
                }
                else
                {
                    SubCategoryObject.IsReviewEdit = IsReviewEdit;
                }

                SubCategoryObject.SubCategoryReviewAndStarsList = AcqCategoryObj.StarsFilterRating(SubCategoryId,StarsFilterRating);
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
                SubCategoryObject.SelectedStarCount = StarsFilterRating;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_ReviewPartialView",SubCategoryObject);
        }
        public JsonResult DeliveryPinCheck(long? PinCode)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            DbEngine.DbEngine DbEngine = new DbEngine.DbEngine();
            //var Response = new List<JsonResponse>();
            var IsDeliveryApplicable = false;
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                }

                var PinCodeExistForDelivery = DbEngine.PinCode.Where(P => P.IsActive == true && (long?)P.Pincode == (long?)PinCode).FirstOrDefault();
                if(PinCodeExistForDelivery != null)
                {
                    IsDeliveryApplicable = true;
                    
                } else
                {
                    IsDeliveryApplicable = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(IsDeliveryApplicable);
        }
        public JsonResult OnMouseHoverStar(long? SubCategoryId)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            List<SubCategoryModel> SubCategoryList = new List<SubCategoryModel>();
            try
            {
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
                Response.StringReponse = AcqCategoryObj.GetStarsGridChart(SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(SubCategoryObject);
        }
        public JsonResult GetStarsGridChart(long? SubCategoryId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
                //Sum of(Rate* TotalRatingOfThatRate)/TotalNumberOfReviews
                //((5 * 252) + (4 * 124) + (3 * 40) + (2 * 29) + (1 * 33)) / (252 + 124 + 40 + 29 + 33);
                for(var I = 0;I < SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList.Count;I++)
                {
                    SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[I].averageStar = AcqCategoryObj.GetSubcategoryRatingBySubCategoryId(SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList[I].Sub_CategoryId);
                }
                Response.StringReponse = AcqCategoryObj.GetStarsGridChart(SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
              return Json(Response);
        }
        public ActionResult DeleteReview(long? SubCategoryId,long? ReviewId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            JsonResponse Response = new JsonResponse();
            var ReviewData = (dynamic)null;
            var IsReviewEdit = false;
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    long? UserId1 = Convert.ToInt64(UserId);
                    SubCategoryObject.userId = UserId1;
                    Response = UpdCategoryObj.DeleteReview(Convert.ToInt64(UserId),SubCategoryId,ReviewId);
                    ReviewData = DbEngineObj.SubCategoryReviewAndStars.Where(S => S.IsActive == true && S.Sub_CategoryId == SubCategoryId && S.UserId == UserId1).FirstOrDefault();
                }
                
                if (ReviewData != null)
                {
                    SubCategoryObject.IsReviewEdit = true;
                }
                else
                {
                    SubCategoryObject.IsReviewEdit = IsReviewEdit;
                }
                SubCategoryObject.SubCategoryReviewAndStarsList = AcqCategoryObj.GetReviewList(SubCategoryId);
                SubCategoryObject.GetSubCategoryReviewAndStarsChartModelList = AcqCategoryObj.GetSubCategoryReviewAndStarsChart(SubCategoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("_ReviewPartialView",SubCategoryObject);
        }
        public JsonResult OnchangeQuantityForAddToCart(long? quantity,long? AddToListUniqueid)
        {
            JsonResponse Response = new JsonResponse();
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            try
            {
                Impulse.BAL.Access.AcqCategory AcqCategory = new Impulse.BAL.Access.AcqCategory();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = UpdCategoryObj.UpdateOnchangeQuantityAddToCart(quantity,AddToListUniqueid,Convert.ToInt64(UserId));
                    if(Response.IsSuccess == true)
                    {
                        Response = AcqCategoryObj.GetAddToCartDataOnAddToCartPage(Convert.ToInt64(UserId));
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"CategoryController","OnchangeQuantityForAddToCart()",ex.Message.ToString(), 0);
            }
            return Json(Response);
        }
        public ActionResult OnClickOnSubCategoryPageForAddToList(AddToCartModel1 Model)
        {
            JsonResponse Response = new JsonResponse();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = UpdCategoryObj.OnClickOnSubCategoryPageForAddToList(Model,Convert.ToInt64(UserId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("AddToCart","Home");
        }
        public ActionResult OnClickOnSubCategoryPageForWishList(WistListModel Model)
        {
            JsonResponse Response = new JsonResponse();
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            List<WistListModel> WistList = new List<WistListModel>();
            List<WistListModel> WistListCookies = new List<WistListModel>();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    Response = UpdCategoryObj.OnClickOnSubCategoryPageForWishList(Model,Convert.ToInt64(UserId));
                } else {
                    //HttpCookie myCookie = Request.Cookies["WishListSubCategroyIds"];
                    //if (myCookie != null)
                    //{
                    //    if (myCookie.Value != null)
                    //    {
                    //        StringBuilder sb = new StringBuilder();
                    //        sb.Append(myCookie.Value.ToString() + "," + Convert.ToString(Model.Sub_CategoryId));
                    //        var cookie = new HttpCookie("WishListSubCategroyIds", Convert.ToString(sb))
                    //        {
                    //            Expires = DateTime.Now.AddDays(365)
                    //        };
                    //        System.Web.HttpContext.Current.Response.SetCookie(cookie);
                    //        Response.WishListCookiesSubCategoryId = cookie.Value;
                    //        Response.IsSuccess = true;
                    //    }
                    //}
                    //else
                    //{
                    //    var cookie = new HttpCookie("WishListSubCategroyIds", Convert.ToString(Model.Sub_CategoryId))
                    //    {
                    //        Expires = DateTime.Now.AddDays(365)
                    //    };
                    //    System.Web.HttpContext.Current.Response.SetCookie(cookie);
                    //    Response.WishListCookiesSubCategoryId = cookie.Value;
                    //    Response.IsSuccess = true;
                    //}
                    
                    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                    if(cookie != null)
                    {
                        WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                    }
                    if (WistListCookies.Count == 0)
                    {
                        Model.WishListCookiesId = AcqCategoryObj.GetWishListCookiesIdentityId(WistListCookies);
                        WistListCookies.Add(Model);
                        string WLCookies = JsonConvert.SerializeObject(WistListCookies);
                        System.Web.HttpContext.Current.Response.Cookies.Add(
                           new HttpCookie("WishListCookies",WLCookies)
                           {
                               Expires = DateTime.Now.AddDays(365)
                           }
                       );
                    } else
                    {
                        Model.WishListCookiesId = AcqCategoryObj.GetWishListCookiesIdentityId(WistListCookies);
                        WistListCookies.Add(Model);
                        string WLCookies = JsonConvert.SerializeObject(WistListCookies);
                        System.Web.HttpContext.Current.Response.Cookies.Add(
                           new HttpCookie("WishListCookies",WLCookies)
                           {
                               Expires = DateTime.Now.AddDays(365)
                           }
                       );
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("WishListCart","Home");
        }
    }
}
