using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Impulse.DAL;

namespace Impulse.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public JsonResult GetStateListByCountryId(long? CountryId)
        {
            List<Impulse.DAL.StatesModel> StatesList = new List<Impulse.DAL.StatesModel>();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
             try
            {
                StatesList = AcqCommonObj.GetStateList(CountryId);

            } catch(Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"RegisterController","GetStateListByCountryId",ex.Message.ToString(), 0);
            }
            return Json(StatesList);
        }


        public JsonResult GetCityListByCountryIdAndStateId(long? CountryId,long? StateId)
        {
            List<Impulse.DAL.CityModel> CityList = new List<Impulse.DAL.CityModel>();
            Impulse.BAL.Access.AcqCommon AcqCommonObj = new Impulse.BAL.Access.AcqCommon();
            try
            {
                CityList = AcqCommonObj.GetCityList(CountryId,StateId);
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"RegisterController","GetCityListByCountryIdAndStateId",ex.Message.ToString(),0);
            }
            return Json(CityList);
        }
    }
}