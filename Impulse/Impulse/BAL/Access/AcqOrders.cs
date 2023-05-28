using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Access
{
    public class AcqOrders
    {
        public List<OrdersDetailsModel> GetOrderDetailsDataOnOrderPage(long? UserId,long? OrderStatusId)
        {
            List<OrdersDetailsModel> OrdersDetailsList = new List<OrdersDetailsModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@UserId",UserId),
                                                new SqlParameter("@OrderStatusId",OrderStatusId),
                                            };

                OrdersDetailsList = DbEngineObj.Database.SqlQuery<OrdersDetailsModel>("GetOrderDetailsDataOnOrderPage @UserId,@OrderStatusId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqOrders","GetOrderDetailsDataOnOrderPage",ex.Message.ToString(),0);
            }
            return OrdersDetailsList;
        }


        public List<OrdersDetailsModel> GetOrderDetailsDataOnOrderPageOnlyShowingProcess(long? UserId, long? OrderStatusId)
        {
            List<OrdersDetailsModel> OrdersDetailsList = new List<OrdersDetailsModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@UserId",UserId),
                                                new SqlParameter("@OrderStatusId",OrderStatusId),
                                            };

                OrdersDetailsList = DbEngineObj.Database.SqlQuery<OrdersDetailsModel>("GetOrderDetailsDataOnOrderPageOnlyShowingProcess @UserId,@OrderStatusId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqOrders","GetOrderDetailsDataOnOrderPageOnlyShowingProcess",ex.Message.ToString(),0);
            }
            return OrdersDetailsList;
        }
    }
}