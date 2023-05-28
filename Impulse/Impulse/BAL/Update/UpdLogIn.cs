using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Impulse.DAL;


namespace Impulse.BAL.Update
{
    public class UpdLogIn
    {
        public JsonResponse SaveGeneratedOTPForRegisterUserDetail(RegisterModel RegisterModel)
        {
            JsonResponse Response = new JsonResponse();
            DBAccessLayer.SaveGeneratedOTPForRegisterUserDetail ent2Save = null;
            bool isNew = false;
            using (Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine())
            {
                using (var transaction = obj.Database.BeginTransaction())
                {
                    try
                    {
                        ent2Save = obj.SaveGeneratedOTPForRegisterUserDetail.Where(x => x.UserEmailId == RegisterModel.Email).FirstOrDefault();
                        if (ent2Save == null)
                        {
                            ent2Save = new DBAccessLayer.SaveGeneratedOTPForRegisterUserDetail();
                            Impulse.BAL.Access.AcqRegister AcqRegisterObj = new Impulse.BAL.Access.AcqRegister();
                            ent2Save.ID = AcqRegisterObj.GeRegisterUserOTPDetailIncreamentId();
                            ent2Save.IsActive = true;
                            ent2Save.CreatedBy = 1;
                            ent2Save.CreatedDate = DateTime.Now;
                            isNew = true;
                        }
                        else
                        {
                            ent2Save.UpdatedBy = 1;
                            ent2Save.UpdatedDate = DateTime.Now;
                            isNew = false;
                        }
                        ent2Save.OTP = RegisterModel.OTP;
                        ent2Save.UserFirstName = RegisterModel.Firstname;
                        ent2Save.UserLastName = RegisterModel.LastName;
                        ent2Save.UserRole = (long?)DbEnum.UserRole.Admin;
                        ent2Save.UserAddress = RegisterModel.Address;
                        ent2Save.UserMobileNumber = RegisterModel.Phone;
                        ent2Save.UserEmailId = RegisterModel.Email;
                        ent2Save.UserPassword = RegisterModel.password;
                        ent2Save.UserConfirmPassword = RegisterModel.confirmPassword;
                        ent2Save.UserCoutryId = RegisterModel.UserCoutryId;
                        ent2Save.UserStateId = RegisterModel.UserStateId;
                        ent2Save.UserCityId = RegisterModel.UserCityId;
                        ent2Save.IsKeepMeUpToDateOnNews = RegisterModel.IsKeepMeUpToDateOnNews;
                        if (isNew)
                        {
                            obj.SaveGeneratedOTPForRegisterUserDetail.Add(ent2Save);
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
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        Response.IsSuccess = false;
                        Response.ResponseMessage = "Something went Wrong";
                        Impulse.BAL.Update.UpdCommon UpdCommonObj = new Impulse.BAL.Update.UpdCommon();
                        UpdCommonObj.Error_Log(0,"UpdLogIn","SaveGeneratedOTPForRegisterUserDetail",ex.Message.ToString(),1);
                    }
                }
            }
            return Response;
        }
    }
}