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

namespace Impulse.BAL.Access
{
    public class AcqRegister
    {
        public long GeRegisterUserOTPDetailIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.SaveGeneratedOTPForRegisterUserDetail.Max(s => (long?)s.ID) == null) ?  0 : obj.SaveGeneratedOTPForRegisterUserDetail.Max(s => s.ID);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }

        public long GeRegisterUserDetailIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Users.Max(s => (long?)s.id) == null ? 0 : obj.Users.Max(s => s.id));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }

        public long? GeRegisterUserDetailIncreamentUserId()
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                //GetMaxValue = obj.Users.Max(s => s.UserId) == null ? 0 : obj.Users.Max(v => v.UserId);
                GetMaxValue = (obj.Users.Max(s => (long?)s.UserId) == null) ? 0 : obj.Users.Max(s => s.UserId);
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
    }
}