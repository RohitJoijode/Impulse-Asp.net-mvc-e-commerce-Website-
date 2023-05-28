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

namespace Impulse.BAL.Update
{
    public class UpdCommon
    {
        public void Error_Log(long? CompCode,string ControllerName,string ActionMethodName, string ErrorMessage, long? CreatedBy)
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["ImpulseEntities"].ConnectionString;
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("GET_ERRORLOG",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CompCode",CompCode);
                cmd.Parameters.AddWithValue("ControllerName",ControllerName);
                cmd.Parameters.AddWithValue("ActionMethodName",ActionMethodName);
                cmd.Parameters.AddWithValue("ErrorMessage",ErrorMessage);
                cmd.Parameters.AddWithValue("CreatedBy",CompCode);
                cmd.Parameters.AddWithValue("CreatedDate", DateTime.Now);
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}