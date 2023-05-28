using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Impulse.DAL;

namespace Impulse.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult GetCategoryList()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                SubCategoryObject.CategoryList = AcqAdminObj.GetCategoryList();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_CategoryAdmin",SubCategoryObject);
        }
        public ActionResult SellerPanel()
        {
            return View();
        }
        public ActionResult GetCategoryModal()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                ViewBag.CategoryTypeDropDown = new SelectList(AcqAdminObj.GetCategoryTypeList(),"CategoryTypeId","CategoryType");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_CategoryModal",SubCategoryObject);
        }
        public ActionResult SaveCategory(CategoryModel CategoryModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                Response = UpdAdminObj.SaveCategory(CategoryModel);
                SubCategoryObject.CategoryList = AcqAdminObj.GetCategoryList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
                return PartialView("_CategoryAdmin",SubCategoryObject);
        }
        public ActionResult GetEditCategoryModal(long? Id,long? CategoryId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                SubCategoryObject.CategoryModel = AcqAdminObj.GetEditCategoryModal(Id,CategoryId);
                ViewBag.CategoryTypeDropDown = new SelectList(AcqAdminObj.GetCategoryTypeList(),"CategoryTypeId","CategoryType");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_EditCategoryModal",SubCategoryObject);
        }
        public ActionResult AddSubCategory()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryList = AcqAdminObj.GetSubCategoryList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryAdmin",SubCategoryObject);
        }
        public ActionResult GetSubCategoryModal()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
                ViewBag.CategoryTypeDropDown = new SelectList(AcqAdminObj.GetCategoryTypeList(),"CategoryTypeId","CategoryType");
                //SubCategoryObject.SubCategoryModel = AcqAdminObj.GetEditSubCategoryModal(Id, SubCategoryId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryModalAdmin",SubCategoryObject);
        }
        public ActionResult GetEditSubCategoryModal(long? Id,long? SubCategoryId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryModel = AcqAdminObj.GetEditSubCategoryModal(Id,SubCategoryId);
                ViewBag.CategoryTypeDropDown = new SelectList(AcqAdminObj.GetCategoryTypeList(),"CategoryTypeId","CategoryType");
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(), "ColorId", "ColorName");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_EditSubCategoryModalAdmin",SubCategoryObject);
        }
        public ActionResult SaveSubCategory(SubCategoryModel SubCategoryModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                    if (SubCategoryModel.UploadImageId != null)
                    {
                        string _imgname = string.Empty;
                        var pic = System.Web.HttpContext.Current.Request.Files["UploadImageId"];
                        if (pic.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(pic.FileName);
                            var _ext = Path.GetExtension(pic.FileName);
                            _imgname = Guid.NewGuid().ToString();
                            var _comPath = Server.MapPath("~/assets2/images/Jewllery/") + _imgname + _ext;
                            var _ServercomPath = "/assets2/images/Jewllery/" + _imgname + _ext;
                            _imgname = "MVC_" + _imgname + DateTime.Now + _ext;
                            var path = _comPath;
                            pic.SaveAs(path);
                            SubCategoryModel.Sub_CategoryImagePath = _ServercomPath;
                        }
                    }
                
               Response = UpdAdminObj.SaveSubCategory(SubCategoryModel);
               SubCategoryObject.SubCategoryList = AcqAdminObj.GetSubCategoryList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryAdmin",SubCategoryObject);
        }
        public ActionResult AddSubCategoryImage()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.Sub_CategoryImageModelList = AcqAdminObj.GetSubCategoryImageModelList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryImageAdmin",SubCategoryObject);
        }
        public ActionResult GetSubCategoryImageModal()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {

                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(), "ColorId", "ColorName");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryImageModel",SubCategoryObject);
        }
        public JsonResult GetCaseCadingDropdownForSubCategoryByCategoryId(long? CategoryId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {

                SubCategoryObject.SubCategoryList = AcqAdminObj.GetCaseCadingDropdownForSubCategoryByCategoryId(CategoryId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Json(SubCategoryObject);
        }
        public ActionResult SaveSubCategoryImage(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                if (Sub_CategoryImageModel.UploadImageId != null)
                {
                    string _imgname = string.Empty;
                    var pic = System.Web.HttpContext.Current.Request.Files["UploadImageId"];
                    if (pic.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(pic.FileName);
                        var _ext = Path.GetExtension(pic.FileName);
                        _imgname = Guid.NewGuid().ToString();
                        var _comPath = Server.MapPath("~/assets2/images/Jewllery/") + _imgname + _ext;
                        var _ServercomPath = "/assets2/images/Jewllery/" + _imgname + _ext;
                        _imgname = "MVC_" + _imgname + DateTime.Now + _ext;
                        var path = _comPath;
                        pic.SaveAs(path);
                        Sub_CategoryImageModel.SubCategoryImagePath = _ServercomPath;
                    }
                }
                Response = UpdAdminObj.SaveSubCategoryImage(Sub_CategoryImageModel);
                SubCategoryObject.Sub_CategoryImageModelList = AcqAdminObj.GetSubCategoryImageModelList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryImageAdmin",SubCategoryObject);
        }
        public ActionResult EditSubCategoryImageModal(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.Sub_CategoryImageModel = AcqAdminObj.GetEditSubCategoryImageModal(Sub_CategoryImageModel.Id,Sub_CategoryImageModel.SubCategoryImageId);
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(), "CategoryId", "CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_EdiSubCategoryImageModal",SubCategoryObject);
        }
        public ActionResult AddSubCategorySideImageList(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryImageId = Sub_CategoryImageModel.SubCategoryImageId;
                SubCategoryObject.CategoryId = Sub_CategoryImageModel.CategoryId;
                SubCategoryObject.SubCategoryId = Sub_CategoryImageModel.SubCategoryId;
                SubCategoryObject.SubcategoryImageColorId = Sub_CategoryImageModel.SubcategoryImageColorId;
                SubCategoryObject.SubCategoryImageList = AcqAdminObj.GetSubCategorySideImageList(Sub_CategoryImageModel.SubCategoryImageId, Sub_CategoryImageModel.CategoryId,Sub_CategoryImageModel.SubCategoryId,Sub_CategoryImageModel.SubcategoryImageColorId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategoryImageListModal", SubCategoryObject);
        }
        public ActionResult CreateSideSubCategoryImageList(long? SubCategoryImageId,long? CategoryId,long? SubCategoryId,long? Sub_CategoryImageColorId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryImageId = SubCategoryImageId;
                SubCategoryObject.CategoryId = CategoryId;
                SubCategoryObject.SubCategoryId = SubCategoryId;
                SubCategoryObject.SubcategoryImageColorId = Sub_CategoryImageColorId;
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(), "ColorId", "ColorName");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_CreateSideSubCategoryImage",SubCategoryObject);
        }
        public ActionResult EditSideSubCategoryImageModalList(long? ImageSideId,long? SubCategoryImageId, long? CategoryId, long? SubCategoryId, long? Sub_CategoryImageColorId)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryImageId = SubCategoryImageId;
                SubCategoryObject.CategoryId = CategoryId;
                SubCategoryObject.SubCategoryId = SubCategoryId;
                SubCategoryObject.SubcategoryImageColorId = Sub_CategoryImageColorId;
                SubCategoryObject.SubCategorySideImageModel = AcqAdminObj.GetEditSubCategorySideImageModel(ImageSideId,SubCategoryImageId,CategoryId,SubCategoryId,Sub_CategoryImageColorId);
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(), "CategoryId", "CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_EditSideSubCategoryImageModal",SubCategoryObject);
        }
        public ActionResult SaveSideSubCategoryImage(SubCategoryImageList SubCategorySideImage)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {

                if (SubCategorySideImage.UploadImageId != null)
                {
                    string _imgname = string.Empty;
                    var pic = System.Web.HttpContext.Current.Request.Files["UploadImageId"];
                    if (pic.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(pic.FileName);
                        var _ext = Path.GetExtension(pic.FileName);
                        _imgname = Guid.NewGuid().ToString();
                        var _comPath = Server.MapPath("~/assets2/images/Jewllery/") + _imgname + _ext;
                        var _ServercomPath = "/assets2/images/Jewllery/" + _imgname + _ext;
                        _imgname = "MVC_" + _imgname + DateTime.Now + _ext;
                        var path = _comPath;
                        pic.SaveAs(path);
                        SubCategorySideImage.SubCategoryImagePath = _ServercomPath;
                    }
                }
                
                Response = UpdAdminObj.SaveSideSubCategoryImage(SubCategorySideImage);
                SubCategoryObject.SubCategoryImageList = AcqAdminObj.GetSubCategorySideImageList(SubCategorySideImage.Sub_CategoryImageId,SubCategorySideImage.CategoryId,SubCategorySideImage.SubCategoryId,SubCategorySideImage.SubCategoryImageColorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PartialView("_SubCategoryImageListModal",SubCategoryObject);
        }
        public ActionResult AddSubCategorySizeImage()
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.Sub_CategoryImageModelList = AcqAdminObj.GetSubCategoryImageModelList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategorySizeImage",SubCategoryObject);
        }
        public ActionResult SubCategorySizeDetailsModal(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryImageId = Sub_CategoryImageModel.SubCategoryImageId;
                SubCategoryObject.CategoryId = Sub_CategoryImageModel.CategoryId;
                SubCategoryObject.SubCategoryId = Sub_CategoryImageModel.SubCategoryId;
                SubCategoryObject.SubcategoryImageColorId = Sub_CategoryImageModel.SubcategoryImageColorId;
                SubCategoryObject.Sub_CategorySizeDetailsList = AcqAdminObj.GetSubCategorySizeDetailsList(Sub_CategoryImageModel.SubCategoryImageId,Sub_CategoryImageModel.CategoryId,Sub_CategoryImageModel.SubCategoryId);
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            return PartialView("_SubCategorySizeDetailsModal",SubCategoryObject);
        }
        public ActionResult CreateSubCategorySizeDetails(Sub_CategoryImageModel Sub_CategoryImageModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {

              SubCategoryObject.SubcategoryImageColorId = Sub_CategoryImageModel.SubcategoryImageColorId;
              SubCategoryObject.SubCategoryImageId = Sub_CategoryImageModel.SubCategoryImageId;
              SubCategoryObject.CategoryId = Sub_CategoryImageModel.CategoryId;
              SubCategoryObject.SubCategoryId = Sub_CategoryImageModel.SubCategoryId;
              ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
              ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
              ViewBag.GetSizeDropDownList = new SelectList(AcqAdminObj.GetSizeDropDownByCategoryId(SubCategoryObject.CategoryId),"SizeId","SizeName");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("CreateSubCategorySizeDetailsModal",SubCategoryObject);
        }
        public ActionResult SaveSubCategorySizeDetails(Sub_CategorySizeDetailsModel Sub_CategorySizeDetailsModal)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubcategoryImageColorId = Sub_CategorySizeDetailsModal.SubcategoryImageColorId;
                SubCategoryObject.SubCategoryImageId = Sub_CategorySizeDetailsModal.SubCategoryColorImageId;
                SubCategoryObject.CategoryId = Sub_CategorySizeDetailsModal.CategoryId;
                SubCategoryObject.SubCategoryId = Sub_CategorySizeDetailsModal.SubCategoryId;
                Response = UpdAdminObj.SaveSubCategorySizeDetails(Sub_CategorySizeDetailsModal);
                if(Response.IsSuccess == true)
                {
                    ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                    ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
                    ViewBag.GetSizeDropDownList = new SelectList(AcqAdminObj.GetSizeDropDownByCategoryId(SubCategoryObject.CategoryId),"SizeId","SizeName");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("CreateSubCategorySizeDetailsModal",SubCategoryObject);
        }
        public ActionResult EditSubCategorySizeDetailsModal(Sub_CategorySizeDetailsModel SubCategorySizeDetailsModel)
        {
            SubCategoryObject SubCategoryObject = new SubCategoryObject();
            Impulse.BAL.Access.AcqCategory AcqCategoryObj = new Impulse.BAL.Access.AcqCategory();
            Impulse.BAL.Update.UpdAdmin UpdAdminObj = new Impulse.BAL.Update.UpdAdmin();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            JsonResponse Response = new JsonResponse();
            try
            {
                SubCategoryObject.SubCategoryImageId = SubCategorySizeDetailsModel.SubCategoryImageId;
                SubCategoryObject.CategoryId = SubCategorySizeDetailsModel.CategoryId;
                SubCategoryObject.SubCategoryId = SubCategorySizeDetailsModel.SubCategoryId;
                SubCategoryObject.SubcategoryImageColorId = SubCategorySizeDetailsModel.SubcategoryImageColorId;
                SubCategoryObject.SubCategorySizeDetailsModel = AcqAdminObj.GetEditSubCategorySizeDetailsModal(SubCategorySizeDetailsModel.ID,SubCategorySizeDetailsModel.SubCategoryImageId,SubCategorySizeDetailsModel.CategoryId,SubCategorySizeDetailsModel.SubCategoryId);
                ViewBag.GetCategoryDropDownList = new SelectList(AcqAdminObj.GetCategoryDropDownList(),"CategoryId","CategoryName");
                ViewBag.GetColorDropDownList = new SelectList(AcqAdminObj.GetColorDropDownList(),"ColorId","ColorName");
                ViewBag.GetSizeDropDownList = new SelectList(AcqAdminObj.GetSizeDropDownByCategoryId(SubCategoryObject.CategoryId),"SizeId","SizeName");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return PartialView("EditSubCategorySizeDetailsModal",SubCategoryObject);
        }
    }
}