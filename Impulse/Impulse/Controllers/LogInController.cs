using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Impulse.DAL;
using Impulse.BAL.Update;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace Impulse.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        [HttpGet]
        [OutputCache(NoStore = true,Duration = 0)]
        public ActionResult LogIn()
        {
            DAL.LogInModel LogInModelObj = new DAL.LogInModel();
            try
            {
                
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","LogIn", ex.Message.ToString(), 0);
            }
            return View("LogIn");
        }
        
        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Submit(DAL.LogInModel LogInModel)
        {
            Impulse.BAL.Access.AcqLogIn AcqLogInObj = new Impulse.BAL.Access.AcqLogIn();
            try
            {
                if (ModelState.IsValid)
                {
                    var USERDetail = AcqLogInObj.GetUserDetailForLogIn(LogInModel);
                    if (USERDetail == null)
                    {
                        ViewBag.Message = "Please Insert Correct UserName and Password!!!";
                        return View("LogIn",LogInModel);
                    }
                    else
                    {
                        UsersModel UserModelObj = new UsersModel();
                        UserModelObj.Email = USERDetail.Email;
                        UserModelObj.id = USERDetail.id;
                        UserModelObj.UserId = USERDetail.UserId;
                        UserModelObj.UserRole = USERDetail.UserRole;
                        UserModelObj.UserMobileNumber = USERDetail.UserMobileNumber;
                        UserModelObj.UserAddress = USERDetail.UserAddress;
                        UserModelObj.UserFullName = USERDetail.UserFirstName + " " + USERDetail.UserLastName;
                        UserSignIn(UserModelObj);
                        return RedirectToAction("HomePage","Home");
                    }
                } else if (!ModelState.IsValid)
                {
                    return View("LogIn",LogInModel);
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","Submit",ex.Message.ToString(), 0);
            }
            return RedirectToAction("LogIn","LogIn");
        }

        public void UserSignIn(UsersModel UserModelObj)
        {

            try
            {
                var claims = new List<Claim>();

                // create required claims
                claims.Add(new Claim(ClaimTypes.NameIdentifier, UserModelObj.Email.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, UserModelObj.UserRole.ToString()));

                // custom – my serialized AppUserState object
                claims.Add(new Claim("UserId",UserModelObj.UserId.ToString()));
                claims.Add(new Claim("UserFullName",UserModelObj.UserFullName.ToString()));
                claims.Add(new Claim("UserMobileNumber", UserModelObj.UserMobileNumber.ToString()));
                claims.Add(new Claim("UserAddress", UserModelObj.UserAddress.ToString()));
                
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, identity);

            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","UserSignIn",ex.Message.ToString(),0);
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);

            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController", "LogOut",ex.Message.ToString(),0);
            }
            return RedirectToAction("HomePage","Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { 
                return HttpContext.GetOwinContext().Authentication; 
            }
        }

        public ActionResult RegisterPage()
        {
            RegisterModel RegisterModel = new RegisterModel();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserId = identity.FindFirst("UserId").Value;
                }
                else
                {
                    
                }
                
                ViewBag.CountryList = new SelectList(AcqCommonObj.GetCountryList(),"Id","CountryName");
            }
            catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController", "RegisterPage",ex.Message.ToString(),0);
            }
            return View(RegisterModel);
        }

        public ActionResult SubmitRegisterPage(RegisterModel RegisterModel)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdLogIn UpdLogIn = new Impulse.BAL.Update.UpdLogIn();
            try
            {
                RegisterModel.OTP = GenerateRandomOTP(6);
                Response = UpdLogIn.SaveGeneratedOTPForRegisterUserDetail(RegisterModel);
                Response = SendEmail(RegisterModel,"OTP Mail For New User");
                if(Response.IsSuccess == true && Response.ResponseMessage == "Send Mail Successfully...")
                {
                    Response.RegisterOTP = "";
                    Response.RegisterOTP = RegisterModel.OTP;
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","SubmitRegisterPage", ex.Message.ToString(), 0);
            }
            return Json(Response);
        }

        public ActionResult UpdateRegisterPage(RegisterModel RegisterModel)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdLogIn UpdLogIn = new Impulse.BAL.Update.UpdLogIn();
            try
            {
                RegisterModel.OTP = GenerateRandomOTP(6);
                Response = UpdLogIn.SaveGeneratedOTPForRegisterUserDetail(RegisterModel);
                Response = SendEmail(RegisterModel,"OTP Mail For Update User Profile");
                if (Response.IsSuccess == true && Response.ResponseMessage == "Send Mail Successfully...")
                {
                    Response.RegisterOTP = "";
                    Response.RegisterOTP = RegisterModel.OTP;
                }
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController", "SubmitRegisterPage", ex.Message.ToString(),0);
            }
            return Json(Response);
        }

        public JsonResponse SendEmail(RegisterModel RegisterModel,string Subject)
        {
            JsonResponse Response = new JsonResponse();
            try
            {
                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress,fromEmailDisplayName),
                new MailAddress(RegisterModel.Email));
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("C:/Users/amarj/OneDrive/Desktop/JavaScriptPrintfunction.pdf");
                //message.Attachments.Add(attachment);
                message.Subject = Subject; // "OTP Mail For New User";
                message.IsBodyHtml = true;
                message.Body = "Your OTP Number :- " + RegisterModel.OTP;
                var client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromEmailAddress,fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = true;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
                Response.IsSuccess = true;
                Response.ResponseMessage = "Send Mail Successfully...";
            }
            catch(Exception ex)
            {
                Response.IsSuccess = false;
                Response.ResponseMessage = "Something went Wrong.";
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","SendEmail",ex.Message.ToString(),0);
            }
            return Response;
        }

        public ActionResult SubmitOTPForRegisterPage(RegisterModel RegisterModel)
        {
            JsonResponse Response = new JsonResponse();
            Impulse.BAL.Update.UpdRegister UpdRegister = new Impulse.BAL.Update.UpdRegister();
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                if (identity.IsAuthenticated == true)
                {
                    var Role = identity.FindFirst(ClaimTypes.Role).Value;
                    var UserEmail = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserFullName = identity.FindFirst("UserFullName").Value;
                    var UserId = identity.FindFirst("UserId").Value;
                    RegisterModel.UserId = Convert.ToInt64(UserId);
                }

                Response = UpdRegister.SaveRegisterUserInUser(RegisterModel);
            }
            catch(Exception ex)
            {
                Response.IsSuccess = false;
                Response.ResponseMessage = "Something went Wrong.";
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","SubmitOTPForRegisterPage",ex.Message.ToString(),0);
            }
            return Json(Response);
        }

        public string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string Result = ""; 
            try
            {
                string sOTP = String.Empty;
                string sTempChars = String.Empty;
                Random rand = new Random();
                for (int i = 0; i < iOTPLength; i++)
                {
                    int p = rand.Next(0, saAllowedCharacters.Length);
                    sTempChars = saAllowedCharacters[rand.Next(0,saAllowedCharacters.Length)];
                    sOTP += sTempChars;
                }

                return sOTP;
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"LogInController","GenerateRandomOTP",ex.Message.ToString(),0);
            }
            return Result;
        }
    }
}