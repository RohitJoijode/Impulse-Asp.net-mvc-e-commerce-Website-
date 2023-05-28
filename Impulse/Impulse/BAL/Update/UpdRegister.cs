using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Impulse.DAL;

namespace Impulse.BAL.Update
{
    public class UpdRegister
    {

        public JsonResponse SaveRegisterUserInUser(RegisterModel RegisterModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.Users ent2Save = null;
            Impulse.BAL.Access.AcqRegister AcqRegisterObj = new Impulse.BAL.Access.AcqRegister();
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.Users.Where(x => x.UserEmail == RegisterModel.Email).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.Users();
                            ent2Save.id = AcqRegisterObj.GeRegisterUserDetailIncreamentId();
                            ent2Save.UserId = AcqRegisterObj.GeRegisterUserDetailIncreamentUserId();
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = RegisterModel.UserId;
                            ent2Save.CreatedDate = DateTime.Now;
                            ent2Save.UserPassword = RegisterModel.password;
                            ent2Save.UserConfirmPassword = RegisterModel.confirmPassword;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.UpdatedBy = RegisterModel.UserId;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isNew = false;
                        }
                        ent2Save.UserFirstName = RegisterModel.Firstname;
                        ent2Save.UserLastName = RegisterModel.LastName;
                        ent2Save.UserRole = 1;
                        ent2Save.UserAddress = RegisterModel.Address;
                        ent2Save.UserMobileNumber = RegisterModel.Phone;
                        ent2Save.UserEmail = RegisterModel.Email;
                        ent2Save.IsKeepMeUpToDateOnNews = RegisterModel.IsKeepMeUpToDateOnNews;
                        ent2Save.RegisterOTP = RegisterModel.OTP;
                        ent2Save.UserCoutryId = RegisterModel.UserCoutryId;
                        ent2Save.UserStateId = RegisterModel.UserStateId;
                        ent2Save.UserCityId = RegisterModel.UserCityId;
                        ent2Save.UserPincode = RegisterModel.PinCode;
                        if (isNew)
                        {
                            obj.Users.Add(ent2Save);
                            obj.SaveChanges();
                            transaction.Commit();
                            Response.IsSuccess = true;
                            Response.ResponseMessage = "Data Save Succefully...";
                        }
                        else
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
                        UpdCommonObj.Error_Log(0,"UpdRegister","SaveRegisterUserInUser", ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
    }
}