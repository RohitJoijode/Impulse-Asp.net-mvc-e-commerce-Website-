using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Access
{
    public class AcqUsers
    {
        public UsersModel GetUsersDetails(long? UserId)
        {
            UsersModel UsersModel = new UsersModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@UserId",UserId)
                                            };

                UsersModel = DbEngineObj.Database.SqlQuery<UsersModel>("GetUsersDetails @UserId", parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqUsers","GetUsersDetails",ex.Message.ToString(),0);
            }
            return UsersModel;
        }
    }
}