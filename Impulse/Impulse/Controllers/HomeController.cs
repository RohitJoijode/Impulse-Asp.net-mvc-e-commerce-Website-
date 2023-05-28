using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Impulse.DAL;
using Impulse.BAL.Update;
using Newtonsoft.Json;
using System.Text;

namespace Impulse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated == true)
            {
                var Role = identity.FindFirst(ClaimTypes.Role).Value;
                var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var UserId = identity.FindFirst("UserId").Value;
            }
            return View();
        }
        public ActionResult HomePage()
        {
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                List<WistListModel> WistListCookies = new List<WistListModel>();
                #region start WishListCookiesClearHere 
                    //var cookie3 = new HttpCookie("WishListCookies")
                    //{
                    //    Expires = DateTime.Now.AddDays(-1)
                    //};
                    //System.Web.HttpContext.Current.Response.SetCookie(cookie3);
                #endregion end WishListCookiesClearHere 
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                var identity = (ClaimsIdentity)User.Identity;
                if(identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId
                                            select a).Count();

                    if (WistListCount != 0)
                        UsersModelObj.WistListCount = WistListCount.ToString();
                    else if (WistListCount != 0)
                        UsersModelObj.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (OrderListCount != 0)
                        UsersModelObj.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        UsersModelObj.OrderListCount = 0.ToString();
                }
                else
                {
                    UsersModelObj.UserFullName = null;
                    UsersModelObj.UserId = null;
                    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                    if(cookie != null)
                    {
                        WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                        if (WistListCookies.Count > 0)
                        {
                            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(WistListCookies.Count);
                        } 
                        else
                        {
                            UsersModelObj.WistListSubCategoryIdsForCookies = 0.ToString();
                        }
                    } 
                    else
                    {
                        UsersModelObj.WistListSubCategoryIdsForCookies = 0.ToString();
                    }
                    
                        //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                        //if (myWistListCookie != null)
                        //{
                        //    if(myWistListCookie.Value != "")
                        //    {
                        //        if (myWistListCookie.Value != null)
                        //        {
                        //            UsersModelObj.WistListSubCategoryIdsForCookies = "";
                        //            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                        //        }
                        //    } else if (myWistListCookie.Value == "")
                        //    {
                        //        UsersModelObj.WistListSubCategoryIdsForCookies = "";
                        //        UsersModelObj.WistListSubCategoryIdsForCookies = "0";
                        //    }
                        //}
                    }
                     ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                
            } catch(Exception  ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","HomePage",ex.Message.ToString(), 0);
            }
            return View(UsersModelObj);
        }
        public ActionResult Products(string category)
        {
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                } else
                {
                    UsersModelObj.UserFullName = null;
                    UsersModelObj.UserId = null;
                }
                return View("CottonNighty",UsersModelObj);
            }
            catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "HomeController", "Products",ex.Message.ToString(),0);
                return View("ErrorPage");
            }
        }
        public ViewResult categoryPage(long? categoryId)
            {
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                }
                else
                {
                    UsersModelObj.UserFullName = null;
                    UsersModelObj.UserId = null;
                }

                if(categoryId == (int)DbEnum.Category.LadiesJewellery)
                {
                    return View("LadiesJewellery");
                } else if (categoryId == (int)DbEnum.Category.LadiesBags)
                {
                    return View("LadiesBags");
                } else if (categoryId == (int)DbEnum.Category.LadiesShoes)
                {
                    return View("LadiesShoes");
                } else if (categoryId == (int)DbEnum.Category.LadiesT_Shirt)
                {
                    return View("LadiesT_Shirt");
                } else if (categoryId == (int)DbEnum.Category.LadiesNighty)
                {
                    return View("CottonNighty",UsersModelObj);
                } else if (categoryId == (int)DbEnum.Category.TopDeals)
                {
                    return View("TopDeals",UsersModelObj);
                } 
                else if (categoryId == (int)DbEnum.Category.Recommend)
                {
                    return View("Recommend",UsersModelObj);
                } else
                {
                    return View("ErrorPage");
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","categoryPage",ex.Message.ToString(),0);
                return View("ErrorPage");
            }
        }
        public ActionResult SubcategoryPage(string SubcategoryId1,string SubCategoryColorId1)
        {
            UsersModel UsersModelObj = new UsersModel();
            Impulse.BAL.Access.AcqCommon AcqCommon = new Impulse.BAL.Access.AcqCommon();
            try
            {
                long? SubcategoryId = Convert.ToInt64(AcqCommon.Decrypt(SubcategoryId1,""));
                long? SubCategoryColorId = Convert.ToInt64(AcqCommon.Decrypt(SubCategoryColorId1,""));

                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    UsersModelObj.SubCategoryId = SubcategoryId;
                    UsersModelObj.SubCategoryColorId = SubCategoryColorId;
                } else
                {
                    UsersModelObj.UserFullName = null;
                    UsersModelObj.UserId = null;
                    UsersModelObj.SubCategoryId = SubcategoryId;
                    UsersModelObj.SubCategoryColorId = SubCategoryColorId;
                    //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                    //if (myWistListCookie != null)
                    //{
                    //    if (myWistListCookie.Value != "")
                    //    {
                    //        if (myWistListCookie.Value != null)
                    //        {
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                    //        }
                    //    }
                    //    else if (myWistListCookie.Value == "")
                    //    {
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "0";
                    //    }
                    //}
                }
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                return View("ProductDetails",UsersModelObj);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","SubcategoryPage",ex.Message.ToString(),0);
                return View("ErrorPage");
            }
        }
        public ActionResult WishListCart()
        {
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            UsersModel UsersModelObj = new UsersModel();
            List<WistListModel> WistListCookies = new List<WistListModel>();
            try
            {
                #region start WishListCookiesClearHere 
                //var cookie = new HttpCookie("WishListSubCategroyIds")
                //{
                //    Expires = DateTime.Now.AddDays(-1)
                //};
                //System.Web.HttpContext.Current.Response.SetCookie(cookie);
                #endregion end WishListCookiesClearHere 

                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);

                    var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId
                                            select a).Count();
                    if (WistListCount != 0)
                        UsersModelObj.WistListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        UsersModelObj.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        UsersModelObj.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();

                    if (OrderListCount != 0)
                        UsersModelObj.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        UsersModelObj.OrderListCount = 0.ToString();

                } else
                {
                    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["WishListCookies"];
                    if (cookie != null)
                    {
                        WistListCookies = JsonConvert.DeserializeObject<List<WistListModel>>(cookie.Value);
                        if (WistListCookies.Count > 0)
                        {
                            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(WistListCookies.Count);
                            UsersModelObj.WistList = WistListCookies;
                        }
                        else
                        {
                            UsersModelObj.WistListSubCategoryIdsForCookies = 0.ToString();
                        }
                    }


                    //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                    //if (myWistListCookie != null)
                    //{
                    //    if (myWistListCookie.Value != "")
                    //    {
                    //        if (myWistListCookie.Value != null)
                    //        {
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                    //        }
                    //    }
                    //    else if (myWistListCookie.Value == "")
                    //    {
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "0";
                    //    }
                    //}
                }
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                return View("WishListCart",UsersModelObj);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","WishListCart",ex.Message.ToString(), 0);
                return View("ErrorPage");
            }
        }
        public ActionResult AddToCart()
        {
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    var WistListCount = (from a in DbEngineObj.WishList where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (WistListCount != 0)
                        UsersModelObj.WistListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        UsersModelObj.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        UsersModelObj.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (OrderListCount != 0)
                        UsersModelObj.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        UsersModelObj.OrderListCount = 0.ToString();

                } else 
                {
                    //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                    //if (myWistListCookie != null)
                    //{
                    //    if (myWistListCookie.Value != "")
                    //    {
                    //        if (myWistListCookie.Value != null)
                    //        {
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                    //        }
                    //    }
                    //    else if (myWistListCookie.Value == "")
                    //    {
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "";
                    //        UsersModelObj.WistListSubCategoryIdsForCookies = "0";
                    //    }
                    //}
                }
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                return View("AddToCart",UsersModelObj);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","AddToCart",ex.Message.ToString(), 0);
                return View("ErrorPage");
            }
        }
        public ActionResult OrderPage()
        {
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserMobileNumber = identity.FindFirst("UserMobileNumber").Value;
                    var UserAddress = identity.FindFirst("UserAddress").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    UsersModelObj.Email = UserEmail;
                    UsersModelObj.UserMobileNumber = UserMobileNumber;
                    UsersModelObj.UserAddress = UserAddress;
                    var WistListCount = (from a in DbEngineObj.WishList where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();

                    if (WistListCount !=  0)
                        UsersModelObj.WistListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        UsersModelObj.WistListCount = 0.ToString();

                    
                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                     if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        UsersModelObj.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();

                    if (OrderListCount != 0)
                        UsersModelObj.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        UsersModelObj.OrderListCount = 0.ToString();


                } else
                {
                    UsersModelObj.UserId = null;
                }
                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                return View("OrderPage",UsersModelObj);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","OrderPage",ex.Message.ToString(),0);
                return View("ErrorPage");
            }
        }
        public ActionResult PayMentMethodPage(OrdersDetailsModel OrdersDetailsModelobj)
            {
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            Impulse.BAL.Update.UpdCategory UpdCategoryObj = new Impulse.BAL.Update.UpdCategory();
            JsonResponse response = new JsonResponse();
            UsersModel UsersModelObj = new UsersModel();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    OrdersDetailsModelobj.UsersModel = new UsersModel();
                    var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId
                                            select a).Count();
                    
                    if (WistListCount != 0)
                        OrdersDetailsModelobj.UsersModel.WistListCount = WistListCount.ToString();
                    else if (WistListCount == 0)
                        OrdersDetailsModelobj.UsersModel.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    
                    if (AddToCartCount != 0)
                        OrdersDetailsModelobj.UsersModel.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount == 0)
                        OrdersDetailsModelobj.UsersModel.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false  && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (OrderListCount != 0)
                        OrdersDetailsModelobj.UsersModel.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        OrdersDetailsModelobj.UsersModel.OrderListCount = 0.ToString();


                    response =  UpdCategoryObj.CreateOrders(OrdersDetailsModelobj,Convert.ToInt64(UserId));
                    if(response.IsSuccess == true && response.GetUniqueId != null && response.GetUniqueId != "")
                    {

                        OrdersDetailsModelobj.OrderId = Convert.ToInt64(response.GetUniqueId);
                        response  = UpdCategoryObj.SaveOrdersDetails(OrdersDetailsModelobj,Convert.ToInt64(UserId));

                        if(response.IsSuccess == true && response.GetUniqueId != null && response.GetUniqueId != "")
                        {
                            OrdersDetailsModelobj.OrderDetailsId = Convert.ToInt64(response.GetUniqueId);
                            OrdersDetailsModelobj.UserId = Convert.ToInt64(UsersModelObj.UserId);
                            response = UpdCategoryObj.UpdateAddToCartStatus(OrdersDetailsModelobj,Convert.ToInt64(UserId));
                        }
                    }
                }

                Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View("PayMentMethodPage",OrdersDetailsModelobj);
        }
        public ActionResult SearchPage(string AutoCompleteSearch,long? CategoryId)
        {
            DbEngine.DbEngine DbEngineObj = new DbEngine.DbEngine();
            UsersModel UsersModelObj = new UsersModel();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
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
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    UsersModelObj.Email = UserEmail;
                    UsersModelObj.UserMobileNumber = UserMobileNumber;
                    UsersModelObj.UserAddress = UserAddress;
                }
                else
                {
                    UsersModelObj.UserId = null;
                }
                
                //HttpCookie myWistListCookie = Request.Cookies["WishListSubCategroyIds"];
                //if (myWistListCookie != null)
                //{
                //    if (myWistListCookie.Value != "")
                //    {
                //        if (myWistListCookie.Value != null)
                //        {
                //            UsersModelObj.WistListSubCategoryIdsForCookies = "";
                //            UsersModelObj.WistListSubCategoryIdsForCookies = Convert.ToString(myWistListCookie.Value);
                //        }
                //    }
                //    else if (myWistListCookie.Value == "")
                //    {
                //        UsersModelObj.WistListSubCategoryIdsForCookies = "";
                //        UsersModelObj.WistListSubCategoryIdsForCookies = "0";
                //    }
                //}

                if (AutoCompleteSearch == "" && CategoryId == 0)
                {
                    AutoCompleteSearch = "Result Not Found";
                } else if (CategoryId != 0)
                {
                    var CategroyName =  DbEngineObj.Category.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
                    if(CategroyName != null)
                    {
                        AutoCompleteSearch = CategroyName.CategoryName.ToString();
                    }
                }

                UsersModelObj.SearchText = AutoCompleteSearch;
                UsersModelObj.CategoryId = CategoryId;
                //var result = DbEngineObj.SubCategoryKeyWords.Where(x => x.SubCategoryKeyWordsName.Contains(AutoCompleteSearch)).ToList();
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                return View("SearchPage",UsersModelObj);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "HomeController","SearchPage",ex.Message.ToString(), 0);
                return View("ErrorPage");
            }
        }
        public ActionResult EditRegisterProfile()
        {
            UsersModel UsersModelObj = new UsersModel();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            Impulse.BAL.Access.AcqUsers AcqUsersObj = new Impulse.BAL.Access.AcqUsers();
            RegisterModel RegisterModel = new RegisterModel();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    UsersModelObj = AcqUsersObj.GetUsersDetails(UsersModelObj.UserId);
                    UsersModelObj.UserFullName = UserFullName;

                    var WistListCount = (
                                            from a in DbEngineObj.WishList
                                            where a.IsWishList == true && a.IsActive == true && a.UserId == UsersModelObj.UserId
                                            select a).Count();
                    if (WistListCount != 0)
                        UsersModelObj.WistListCount = WistListCount.ToString();
                    else if (WistListCount != 0)
                        UsersModelObj.WistListCount = 0.ToString();

                    var AddToCartCount = (from a in DbEngineObj.AddToCart where a.IsAddToCart == true && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = AddToCartCount.ToString();
                    else if (AddToCartCount != 0)
                        UsersModelObj.AddToCartCount = 0.ToString();

                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();
                    if (OrderListCount != 0)
                        UsersModelObj.OrderListCount = OrderListCount.ToString();
                    else if (OrderListCount == 0)
                        UsersModelObj.OrderListCount = 0.ToString();


                }
                ViewBag.CategoryListForDropDown = new SelectList(AcqCommonObj.GetCategoryDropDownForSearchFilter(),"CategoryId","CategoryName");
                ViewBag.CountryList = new SelectList(AcqCommonObj.GetCountryList(),"Id","CountryName");
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"HomeController","EditRegisterProfile",ex.Message.ToString(),0);
                return View("ErrorPage");
            }
            return View("EditRegisterProfile",UsersModelObj);
        }
        public ActionResult OnClickPaymentMethodContinue(OrdersDetailsModel OrdersDetailsModelobj)
        {
            JsonResponse Response = new JsonResponse();
            UsersModel UsersModelObj = new UsersModel();
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
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    Response = UpdCategoryObj.UpdateOrdersIsPaymentStatus(OrdersDetailsModelobj,UsersModelObj.UserId);
                 }

                 return RedirectToAction("OrderPage","Home");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult OrdersDetailsView(long? OrderViewId)
        {
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                Impulse.BAL.Access.AcqOrders AcqOrdersObj = new Impulse.BAL.Access.AcqOrders();
                Impulse.BAL.Access.AcqUsers AcqUsersObj = new Impulse.BAL.Access.AcqUsers();
                OrdersPageModel OrdersPageModel = new OrdersPageModel();
                UsersModel UsersModelObj = new UsersModel();
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    UsersModelObj.UserFullName = UserFullName;
                    UsersModelObj.UserId = Convert.ToInt64(UserId);
                    OrdersPageModel.UsersModel = new UsersModel();
                    var OrderListCount = (from a in DbEngineObj.Orders where a.IsCancel == false && a.IsActive == true && a.UserId == UsersModelObj.UserId select a).Count();

                    OrdersPageModel.UsersModel = AcqUsersObj.GetUsersDetails(UsersModelObj.UserId);

                    if ((int)Impulse.DbEnum.OrderStatus.OnProcess == OrderViewId)
                    {
                        OrdersPageModel.OrdersDetailsList = AcqOrdersObj.GetOrderDetailsDataOnOrderPage(UsersModelObj.UserId, (int)Impulse.DbEnum.OrderStatus.OnProcess);
                        
                        if (OrderListCount != 0)
                            OrdersPageModel.UsersModel.OrderListCount = OrderListCount.ToString();
                        else if (OrderListCount == 0)
                            OrdersPageModel.UsersModel.OrderListCount = 0.ToString();

                        return PartialView("_Orders", OrdersPageModel);
                    }
                    else if ((int)Impulse.DbEnum.OrderStatus.Pending == OrderViewId)
                    {
                        OrdersPageModel.OrdersDetailsList = AcqOrdersObj.GetOrderDetailsDataOnOrderPageOnlyShowingProcess(UsersModelObj.UserId, (int)Impulse.DbEnum.OrderStatus.Pending);

                        if (OrderListCount != 0)
                            OrdersPageModel.UsersModel.OrderListCount = OrderListCount.ToString();
                        else if (OrderListCount == 0)
                            OrdersPageModel.UsersModel.OrderListCount = 0.ToString();


                        return PartialView("_PendingOrders", OrdersPageModel);
                    }
                    else if ((int)Impulse.DbEnum.OrderStatus.Delivered == OrderViewId)
                    {
                        OrdersPageModel.OrdersDetailsList = AcqOrdersObj.GetOrderDetailsDataOnOrderPage(UsersModelObj.UserId,(int)Impulse.DbEnum.OrderStatus.Delivered);
                        
                        if (OrderListCount != 0)
                            OrdersPageModel.UsersModel.OrderListCount = OrderListCount.ToString();
                        else if (OrderListCount == 0)
                            OrdersPageModel.UsersModel.OrderListCount = 0.ToString();

                        return PartialView("_OrdersHistory", OrdersPageModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("ErrorPage");
        }
        public ActionResult GetSearchPageResult(string AutoCompleteSearch,long? CategoryId)
        {
            List<SubCategoryModel> SubCategoryModelList = new List<SubCategoryModel>();
            try
            {
                List<Impulse.DBAccessLayer.Sub_Category> SubCategoryList = new List<Impulse.DBAccessLayer.Sub_Category>();
                List<Impulse.DBAccessLayer.SubCategoryKeyWords> SubCategoryKeyWordsList = new List<Impulse.DBAccessLayer.SubCategoryKeyWords>();
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
                if (AutoCompleteSearch != "" && AutoCompleteSearch != "Result Not Found" && CategoryId == 0)
                {
                    dynamic result = DbEngineObj.SubCategoryKeyWords.Where(x => x.SubCategoryKeyWordsName.Contains(AutoCompleteSearch)).ToList();
                    if (result != null)
                    {
                        SubCategoryKeyWordsList = result;
                        for (var Index = 0; Index < SubCategoryKeyWordsList.Count;Index++)
                        {
                            if (SubCategoryKeyWordsList[Index].CategoryId != 0 && SubCategoryKeyWordsList[Index].SubCategoryId == 0)
                            {
                                var CategoryId1 = SubCategoryKeyWordsList[Index].CategoryId;
                                SubCategoryModelList = AcqCategoryObj.GetAllCategoryForSearchFilter(CategoryId1);
                                break;
                            } else if (SubCategoryKeyWordsList[Index].CategoryId != 0 && SubCategoryKeyWordsList[Index].SubCategoryId != 0)
                            {
                                var CategoryId1 = SubCategoryKeyWordsList[Index].CategoryId;
                                var SubCategoryId = SubCategoryKeyWordsList[Index].SubCategoryId;
                                SubCategoryModelList = AcqCategoryObj.GetSubCategoryForSearchFilter(CategoryId1,SubCategoryId);
                                break;
                            }
                        }
                    }
                }
                else if (CategoryId != 0 || AutoCompleteSearch == "Result Not Found" && AutoCompleteSearch != "")
                {
                    SubCategoryModelList = AcqCategoryObj.GetAllCategoryForSearchFilter(CategoryId);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           return PartialView("_SearchResult",SubCategoryModelList);
        }
     }
}