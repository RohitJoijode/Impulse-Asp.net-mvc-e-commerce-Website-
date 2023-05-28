using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Access
{
    public class AcqLogIn
    {
        public UsersModel GetUserDetailForLogIn(LogInModel LogInModel)
        {
            UsersModel UsersModel = new UsersModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@UserEmail",LogInModel.UserName),
                                                new SqlParameter("@Password",LogInModel.UserPassword),
                                            };

                UsersModel = DbEngineObj.Database.SqlQuery<UsersModel>("GetUserDetailForLogIn @UserEmail,@Password",parameters).FirstOrDefault();
            }
            catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqLogIn","UsersModel",ex.Message.ToString(), 0);
            }
            
            return UsersModel;
        }

    }
}