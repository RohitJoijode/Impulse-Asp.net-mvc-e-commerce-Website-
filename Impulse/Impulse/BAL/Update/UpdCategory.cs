using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Update
{
    public class UpdCategory
    {
        public JsonResponse SaveToAddToCartOnWistListPage(SubCategoryObject SubCategoryObject,long? SubCategoryId,long? Categoryid,long? WistListUniqueId,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                            ent2Save = new DBAccessLayer.AddToCart();
                            ent2Save.Id = AcqCategoryObj.GetAddToCartIncreamentId();
                            ent2Save.AddToCartId = AcqCategoryObj.GetAddToCartIdIncreamentId(UserId);
                            if(SubCategoryObject.WistListModel != null)
                            {
                                ent2Save.Quantity = SubCategoryObject.WistListModel.Quantity;
                                ent2Save.Sub_Category_ColorWisePrice = SubCategoryObject.WistListModel.Sub_Category_ColorWisePrice;
                                ent2Save.Sub_Category_Price = SubCategoryObject.WistListModel.Sub_Category_Price;
                                ent2Save.Sub_Category_ColorId = SubCategoryObject.WistListModel.Sub_Category_ColorId;
                                ent2Save.Sub_Category_SizeId = SubCategoryObject.WistListModel.Sub_Category_SizeId;
                            }
                            ent2Save.WistListUniqueId = WistListUniqueId;
                            ent2Save.UserId = UserId;
                            ent2Save.CategoryId = Categoryid;
                            ent2Save.Sub_CategoryId = SubCategoryId;
                            ent2Save.IsAddToCart = true;
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                        if (isNew)
                        {
                            obj.AddToCart.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdCategory","SaveToAddToCart",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse UpdateFlagWistListOnWistListPage(WistListModel WistListModel,long? SubCategoryId, long? WistListUniqueId,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.WishList ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isEdit = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.WishList.Where(s => s.Sub_CategoryId == SubCategoryId && s.Id == WistListUniqueId && s.UserId == UserId).FirstOrDefault();
                        if (ent2Save != null)
                        {
                            ent2Save.UpdatedBy = UserId;
                            ent2Save.IsWishList = false;
                            ent2Save.IsActive = false;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isEdit = true;
                        }
                        if (isEdit)
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.StringReponse = ent2Save.CategoryId.ToString();
                            Response.ResponseMessage = "Data Update Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdCategory","UpdateFlagWistListOnWistListPage",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse UpdateOnchangeQuantityAddToCart(long? quantity,long? AddToListUniqueid,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isEdit = false;
            try
            {
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {
                            ent2Save = obj.AddToCart.Where(s => s.Id == AddToListUniqueid && s.UserId == UserId).FirstOrDefault();
                            if (ent2Save != null)
                            {
                                ent2Save.Quantity = quantity;
                                ent2Save.UpdatedBy = UserId;
                                ent2Save.UpdatedDate = DateTime.Now;
                                isEdit = true;
                            }
                            if (isEdit)
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
                            UpdCommonObj.Error_Log(0,"UpdCategory","UpdateOnchangeQuantityAddToCart",ex.Message.ToString(),1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Response;
        }

        public JsonResponse SaveToWishList(long? SubCategoryId,long? CategoryId,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.WishList ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            try
            {
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {
                            ent2Save = new DBAccessLayer.WishList();
                            ent2Save.Id = AcqCategoryObj.GetWishListIncreamentId();
                            ent2Save.WishListId = AcqCategoryObj.GetWishListIdIncreamentId(UserId);
                            ent2Save.UserId = UserId;
                            ent2Save.CategoryId = CategoryId;
                            ent2Save.Sub_CategoryId = SubCategoryId;
                            ent2Save.IsWishList = true;
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                            if (isNew)
                            {
                                obj.WishList.Add(ent2Save);
                                obj.SaveChanges();
                                transaction.Commit();
                                Response.IsSuccess = true;
                                Response.ResponseMessage = "Data Save Succefully...";
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.IsSuccess = false;
                            Response.ResponseMessage = "Something went Wrong";
                            Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                            UpdCommonObj.Error_Log(0, "UpdCategory", "SaveToWishList", ex.Message.ToString(), 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return Response;
        }

        public JsonResponse UpdateDeleteWistListDataForWistListPage(SubCategoryModel SubCategoryModel,long? UserId)
        {
                JsonResponse Response = new JsonResponse();
                DBAccessLayer.WishList ent2Save = null;
                bool isEdit = false;
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {   var Id = Convert.ToInt64(SubCategoryModel.UniqueId);
                            var SubCategoryId = Convert.ToInt64(SubCategoryModel.SubCategoryId);
                            ent2Save = obj.WishList.Where(s => s.Sub_CategoryId == SubCategoryId && s.Id == Id && s.UserId == UserId).FirstOrDefault();
                            if (ent2Save != null)
                            {
                                ent2Save.UpdatedBy = UserId;
                                ent2Save.IsWishList = false;
                                ent2Save.IsActive = false;
                                ent2Save.UpdatedDate = DateTime.Now;
                                isEdit = true;
                            }
                             if (isEdit)
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
                            UpdCommonObj.Error_Log(0, "UpdCategory","UpdateDeleteWistListDataForWistListPage", ex.Message.ToString(), 1);
                        }
                    }
                }
            return Response;
        }

        public JsonResponse DeleteAddToCartItem(SubCategoryModel SubCategoryModel, long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            bool isEdit = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        var Id = Convert.ToInt64(SubCategoryModel.UniqueId);
                        var SubCategoryId = Convert.ToInt64(SubCategoryModel.SubCategoryId);
                        ent2Save = obj.AddToCart.Where(s => s.Sub_CategoryId == SubCategoryId && s.Id == Id && s.UserId == UserId).FirstOrDefault();
                        if (ent2Save != null)
                        {
                            ent2Save.UpdatedBy = UserId;
                            ent2Save.IsAddToCart = false;
                            ent2Save.IsActive = false;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isEdit = true;
                        }
                        if (isEdit)
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
                        UpdCommonObj.Error_Log(0, "UpdCategory","DeleteAddToCartItem",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse CreateOrders(OrdersDetailsModel OrdersDetailsModel,long? UserId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Orders ent2Save = null;
            bool IsNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = new DBAccessLayer.Orders();
                        ent2Save.Id = AcqCategoryObj.GetOrderIdentityIncreamentId();
                        ent2Save.OrderId = AcqCategoryObj.GetOrderIncreamentIdByUserId(UserId);
                        ent2Save.AddToCartUniqueId = OrdersDetailsModel.AddToCartUniqueId;
                        ent2Save.UserId = UserId;
                        ent2Save.OrderStatusId = (int)Impulse.DbEnum.OrderStatus.Pending;//Pending (Pending = 1,OnProcess = 2,Delivered = 3)
                        ent2Save.IsCancel = false;
                        ent2Save.PaymentMethodSelectionId = (int)Impulse.DbEnum.PaymentMethodSelection.None;
                        ent2Save.IsPayment = false;
                        ent2Save.IsActive = true;
                        ent2Save.CreatedBy = UserId;
                        ent2Save.CreatedDate = DateTime.Now;
                        IsNew = true;
                        if (IsNew)
                        {
                            obj.Orders.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.GetUniqueId = ent2Save.Id.ToString();
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdCategory","CreateOrders",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse SaveOrdersDetails(OrdersDetailsModel OrdersDetailsModel,long? UserId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.OrdersDetails ent2Save = null;
            bool IsNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = new DBAccessLayer.OrdersDetails();
                        ent2Save.Id = AcqCategoryObj.GetOrderDetailsIdentityIncreamentId();
                        ent2Save.OrderDetailsId = AcqCategoryObj.GetOrderDetailsIncreamentId(UserId);
                        ent2Save.OrderId = OrdersDetailsModel.OrderId;
                        ent2Save.AddToCartUniqueId = OrdersDetailsModel.AddToCartUniqueId;
                        ent2Save.UserId = UserId;
                        ent2Save.SubCategoryId = OrdersDetailsModel.SubCategoryId;
                        ent2Save.SubCategoryQauntity = OrdersDetailsModel.SubCategoryQauntity;
                        ent2Save.SubCategoryPrice = OrdersDetailsModel.SubCategoryPrice;
                        ent2Save.SubCategoryTotalPrice = OrdersDetailsModel.SubCategoryPrice * OrdersDetailsModel.SubCategoryQauntity;
                        ent2Save.IsActive = true;
                        ent2Save.CreatedBy = UserId;
                        ent2Save.CreatedDate = DateTime.Now;
                        IsNew = true;
                        if (IsNew)
                        {
                            obj.OrdersDetails.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.GetUniqueId = ent2Save.Id.ToString();
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdCategory","SaveOrdersDetails",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse UpdateOrdersIsPaymentStatus(OrdersDetailsModel OrdersDetailsModel,long? UserId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Orders ent2Save = null;
            bool IsEdit = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Orders.Where(s => s.Id == OrdersDetailsModel.OrderId && s.UserId == UserId).FirstOrDefault();
                        if (ent2Save != null)
                        {
                            ent2Save.UpdatedBy = UserId;
                            ent2Save.IsPayment = true;
                            ent2Save.PaymentMethodSelectionId = OrdersDetailsModel.PaymentMethodSelectionId;
                            if(OrdersDetailsModel.PaymentMethodSelectionId == (int)Impulse.DbEnum.PaymentMethodSelection.None)
                            {
                                ent2Save.OrderStatusId = (int)Impulse.DbEnum.OrderStatus.Pending;
                            } else if (OrdersDetailsModel.PaymentMethodSelectionId != (int)Impulse.DbEnum.PaymentMethodSelection.None)
                            {
                                ent2Save.OrderStatusId = (int)Impulse.DbEnum.OrderStatus.OnProcess;
                            }
                            ent2Save.UpdatedDate = DateTime.Now;
                            IsEdit = true;
                        }
                        if (IsEdit)
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
                        UpdCommonObj.Error_Log(0,"UpdCategory","UpdateOrdersIsPaymentStatus", ex.Message.ToString(), 1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse UpdateAddToCartStatus(OrdersDetailsModel OrdersDetailsModel, long? UserId)
        {
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            bool IsEdit = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.AddToCart.Where(s => s.Id == OrdersDetailsModel.AddToCartUniqueId && s.UserId == UserId).FirstOrDefault();
                        if (ent2Save != null)
                        {
                            ent2Save.IsAddToCart = false;
                            ent2Save.IsActive = false;
                            ent2Save.UpdatedBy = UserId;
                            ent2Save.UpdatedDate = DateTime.Now;
                            IsEdit = true;
                        }
                        if (IsEdit)
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
                        UpdCommonObj.Error_Log(0,"UpdCategory","UpdateAddToCartStatus",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
        public JsonResponse SaveReview(long? UserId,SubCategoryReviewAndStarsModel SubCategoryReviewAndStarsModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.SubCategoryReviewAndStars ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.SubCategoryReviewAndStars.Where(x => x.UserId == UserId && x.Sub_CategoryId == SubCategoryReviewAndStarsModel.Sub_CategoryId && x.IsActive == true).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.SubCategoryReviewAndStars();
                            ent2Save.Id = AcqCategoryObj.GetReviewIdentityIncreamentId();
                            ent2Save.ReviewId = AcqCategoryObj.GetReviewIncreamentIdByUserId(UserId);
                            ent2Save.IsActive = true;
                            ent2Save.CreadedBy = UserId;
                            ent2Save.CreadedDate = DateTime.Now;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.Updatedby = UserId;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isNew = false;
                        }
                        ent2Save.UserId = UserId;
                        ent2Save.Sub_CategoryId = SubCategoryReviewAndStarsModel.Sub_CategoryId;
                        ent2Save.ReviewId = SubCategoryReviewAndStarsModel.ReviewId;
                        ent2Save.GivenStars = SubCategoryReviewAndStarsModel.GivenStars;
                        ent2Save.ReviewMessage = SubCategoryReviewAndStarsModel.ReviewMessage;
                        if (isNew)
                        {
                            obj.SubCategoryReviewAndStars.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.GetUniqueId = SubCategoryReviewAndStarsModel.Sub_CategoryId.ToString();
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
                        {
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.GetUniqueId = SubCategoryReviewAndStarsModel.Sub_CategoryId.ToString();
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
                        UpdCommonObj.Error_Log(0,"UpdCategory","SaveReview",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse DeleteReview(long? UserId,long? SubCategoryId,long? ReviewId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.SubCategoryReviewAndStars ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.SubCategoryReviewAndStars.Where(x => x.UserId == UserId && x.Sub_CategoryId == SubCategoryId && x.Id == ReviewId && x.IsActive == true).FirstOrDefault();
                        if (ent2Save != null)
                        {
                            //ent2Save = new DBAccessLayer.SubCategoryReviewAndStars();
                            ent2Save.IsActive = false;
                            ent2Save.Updatedby = UserId;
                            ent2Save.UpdatedDate = DateTime.Now;
                        }
                        
                            obj.Entry(ent2Save).State = System.Data.Entity.EntityState.Modified;
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Update Succefully...";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0, "UpdCategory","DeleteReview",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

        public JsonResponse SaveToAddToCart(long? SubCategoryId,long? CategoryId,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            try
            {
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {
                            ent2Save = new DBAccessLayer.AddToCart();
                            ent2Save.Id = AcqCategoryObj.GetAddToCartIncreamentId();
                            ent2Save.AddToCartId = AcqCategoryObj.GetAddToCartIdIncreamentId(UserId);
                            ent2Save.UserId = UserId;
                            ent2Save.CategoryId = CategoryId;
                            ent2Save.Sub_CategoryId = SubCategoryId;
                            ent2Save.IsAddToCart = true;
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                            if (isNew)
                            {
                                obj.AddToCart.Add(ent2Save);
                                obj.SaveChanges();
                                transaction.Commit();
                                Response.IsSuccess = true;
                                Response.ResponseMessage = "Data Save Succefully...";
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.IsSuccess = false;
                            Response.ResponseMessage = "Something went Wrong";
                            Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                            UpdCommonObj.Error_Log(0,"UpdCategory","SaveToAddToCart",ex.Message.ToString(),1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Response;
        }
        public JsonResponse OnClickOnSubCategoryPageForAddToList(AddToCartModel1 AddToCartModel1,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.AddToCart ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            try
            {
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {
                            ent2Save = new DBAccessLayer.AddToCart();
                            ent2Save.Id = AcqCategoryObj.GetAddToCartIncreamentId();
                            ent2Save.AddToCartId = AcqCategoryObj.GetAddToCartIdIncreamentId(UserId);
                            ent2Save.UserId = UserId;
                            ent2Save.Quantity = AddToCartModel1.Quantity;
                            ent2Save.Sub_CategoryImagePath = AddToCartModel1.Sub_CategoryImagePath;
                            ent2Save.Sub_Category_ColorWisePrice = AddToCartModel1.Sub_Category_ColorWisePrice;
                            ent2Save.Sub_Category_Price = AddToCartModel1.Sub_Category_Price;
                            ent2Save.Sub_Category_ColorId = AddToCartModel1.Sub_Category_ColorId;
                            ent2Save.Sub_Category_SizeId = AddToCartModel1.Sub_Category_SizeId;
                            ent2Save.CategoryId = AddToCartModel1.CategoryId;
                            ent2Save.Sub_CategoryId = AddToCartModel1.Sub_CategoryId;
                            ent2Save.IsAddToCart = true;
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                            if (isNew)
                            {
                                obj.AddToCart.Add(ent2Save);
                                obj.SaveChanges();
                                transaction.Commit();
                                Response.IsSuccess = true;
                                Response.ResponseMessage = "Data Save Succefully...";
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.IsSuccess = false;
                            Response.ResponseMessage = "Something went Wrong";
                            Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                            UpdCommonObj.Error_Log(0,"UpdCategory","OnClickOnSubCategoryPageForAddToList",ex.Message.ToString(),1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Response;
        }

        public JsonResponse OnClickOnSubCategoryPageForWishList(WistListModel WistListModel,long? UserId)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.WishList ent2Save = null;
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            bool isNew = false;
            try
            {
                using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
                {
                    using (var transaction = obj.Database.BeginTransaction())
                    {
                        try
                        {
                            ent2Save = new DBAccessLayer.WishList();
                            ent2Save.Id = AcqCategoryObj.GetWishListIncreamentId();
                            ent2Save.WishListId = AcqCategoryObj.GetWishListIdIncreamentId(UserId);
                            ent2Save.UserId = UserId;
                            ent2Save.Quantity = WistListModel.Quantity;
                            ent2Save.Sub_CategoryImagePath = WistListModel.Sub_CategoryImagePath;
                            ent2Save.Sub_Category_ColorWisePrice = WistListModel.Sub_Category_ColorWisePrice;
                            ent2Save.Sub_Category_Price = WistListModel.Sub_Category_Price;
                            ent2Save.Sub_Category_ColorId = WistListModel.Sub_Category_ColorId;
                            ent2Save.Sub_Category_SizeId = WistListModel.Sub_Category_SizeId;
                            ent2Save.CategoryId = WistListModel.CategoryId;
                            ent2Save.Sub_CategoryId = WistListModel.Sub_CategoryId;
                            ent2Save.IsWishList = true;
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                            if (isNew)
                            {
                                obj.WishList.Add(ent2Save);
                                obj.SaveChanges();
                                transaction.Commit();
                                Response.IsSuccess = true;
                                Response.ResponseMessage = "Data Save Succefully...";
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.IsSuccess = false;
                            Response.ResponseMessage = "Something went Wrong";
                            Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                            UpdCommonObj.Error_Log(0,"UpdCategory","OnClickOnSubCategoryPageForWishList",ex.Message.ToString(),1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Response;
        }
    }
}
