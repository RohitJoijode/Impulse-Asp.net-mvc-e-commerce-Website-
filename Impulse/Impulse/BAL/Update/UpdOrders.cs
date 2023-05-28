using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Update
{
    public class UpdOrders
    {
        public JsonResponse OrdersCancel(OrdersDetailsModel OrdersDetailsModel, long? UserId)
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
                            ent2Save.OrderStatusId = OrdersDetailsModel.OrderStatusId;
                            ent2Save.IsCancel = true;
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
                        UpdCommonObj.Error_Log(0, "UpdOrders","OrdersCancel",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }

    }
}