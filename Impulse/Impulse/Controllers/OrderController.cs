using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Impulse.DAL;
using Impulse.BAL.Update;

namespace Impulse.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        public ActionResult OrdersCancel(long? OrderId,long? OrderStatusId)
        {
            JsonResponse Response = new JsonResponse();
            OrdersDetailsModel OrdersDetailsModel = new OrdersDetailsModel();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdOrders UpdOrdersObj = new Impulse.BAL.Update.UpdOrders();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    OrdersDetailsModel.OrderId = OrderId;
                    OrdersDetailsModel.OrderStatusId = (int)Impulse.DbEnum.OrderStatus.Cancel;
                    Response = UpdOrdersObj.OrdersCancel(OrdersDetailsModel, Convert.ToInt64(UserId));
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"OrderController","OrdersCancel()",ex.Message.ToString(),0);
            }
            return RedirectToAction("OrdersDetailsView","Home",new { OrderViewId = OrderStatusId });
        }

    }
}