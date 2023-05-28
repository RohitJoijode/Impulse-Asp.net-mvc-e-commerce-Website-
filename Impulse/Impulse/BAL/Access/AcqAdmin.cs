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
using Impulse.DAL;
using System.Collections;

namespace Impulse.BAL.Access
{
    public class AcqAdmin
    {
        public List<CategoryModel> GetCategoryList()
        {
            List<CategoryModel> GetCategoryList = new List<CategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                GetCategoryList = DbEngineObj.Database.SqlQuery<CategoryModel>("GetCategoryList").ToList();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetCategoryList;
        }
        public List<SubCategoryModel> GetSubCategoryList()
        {
            List<SubCategoryModel> GetSubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                GetSubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetSubCategoryList").ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetSubCategoryList;
        }
        public List<CategoryModel> GetCategoryTypeList()
        {
            List<CategoryModel> GetCategoryTypeList = new List<CategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                GetCategoryTypeList = DbEngineObj.Database.SqlQuery<CategoryModel>("GetCategoryTypeList").ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetCategoryTypeList;
        }
        public List<Sub_CategoryImageModel> GetSubCategoryImageModelList()
        {
            List<Sub_CategoryImageModel> GetSubCategoryImageModelList = new List<Sub_CategoryImageModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                GetSubCategoryImageModelList = DbEngineObj.Database.SqlQuery<Sub_CategoryImageModel>("GetSubCategoryImageModelList").ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetSubCategoryImageModelList;
        }
        public CategoryModel GetEditCategoryModal(long? Id, long? CategoryId)
        {
            CategoryModel GetCategoryModal = new CategoryModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@Id",Id),
                                                new SqlParameter("@CategoryId",CategoryId)
                                            };

                GetCategoryModal = DbEngineObj.Database.SqlQuery<CategoryModel>("GetEditCategoryModal @Id,@CategoryId", parameters).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetCategoryModal;
        }
        public Sub_CategoryImageModel GetEditSubCategoryImageModal(long? Id, long? SubCategoryCategoryImageId)
        {
            Sub_CategoryImageModel GetSub_CategoryImageModel = new Sub_CategoryImageModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@Id",Id),
                                                new SqlParameter("@SubCategoryCategoryImageId",SubCategoryCategoryImageId)
                                            };

                GetSub_CategoryImageModel = DbEngineObj.Database.SqlQuery<Sub_CategoryImageModel>("GetEditSubCategoryImageModal @Id,@SubCategoryCategoryImageId", parameters).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetSub_CategoryImageModel;
        }
        public long GetCategoryIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Category.Max(s => (long?)s.Id) == null ? 0 : obj.Category.Max(s => s.Id));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetSubCategoryIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_Category.Max(s => (long?)s.Id) == null ? 0 : obj.Sub_Category.Max(s => s.Id));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long GetSubCategoryImageIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_CategoryImage.Max(s => (long?)s.Id) == null ? 0 : obj.Sub_CategoryImage.Max(s => s.Id));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetCategoryIdIncreamentId()
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Category.Max(s => (long?)s.CategoryId) == null ? 0 : obj.Category.Max(s => s.CategoryId));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetSubCategoryIdIncreamentId()
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_Category.Max(s => (long?)s.Sub_CategoryId) == null ? 0 : obj.Sub_Category.Max(s => s.Sub_CategoryId));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetSubCategoryImageIdIncreamentId(long? SubcategoryId)
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_CategoryImage.Where(x => x.SubCategoryId == SubcategoryId).Max(s => (long?)s.SubCategoryImageId) == null ? 0 : obj.Sub_CategoryImage.Where(x => x.SubCategoryId == SubcategoryId).Max(s => s.SubCategoryImageId));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public long? GetCategoryTypeIncreamentId()
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Category.Max(s => (long?)s.CategoryTypeId) == null ? 0 : obj.Category.Max(s => s.CategoryTypeId));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }
        public SubCategoryModel GetEditSubCategoryModal(long? Id, long? SubCategoryId)
        {
            SubCategoryModel GetSubCategoryModel = new SubCategoryModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@Id",Id),
                                                new SqlParameter("@SubCategoryId",SubCategoryId)
                                            };

                GetSubCategoryModel = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetEditSubCategoryModal @Id,@SubCategoryId", parameters).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetSubCategoryModel;
        }
        public List<SubCategoryModel> GetCaseCadingDropdownForSubCategoryByCategoryId(long? CategoryId)
        {
            List<SubCategoryModel> GetSubCategoryList = new List<SubCategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@CategoryId",CategoryId)
                                            };

                GetSubCategoryList = DbEngineObj.Database.SqlQuery<SubCategoryModel>("GetCaseCadingDropdownForSubCategoryByCategoryId @CategoryId", parameters).ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return GetSubCategoryList;
        }
        public List<CategoryModel> GetCategoryDropDownList()
        {
            List<CategoryModel> CategoryList = new List<CategoryModel>();
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                CategoryList = obj.Database.SqlQuery<CategoryModel>("GetCategoryDropDownList").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CategoryList;
        }
        public List<ColorModal> GetColorDropDownList()
        {
            List<ColorModal> ColorList = new List<ColorModal>();
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                ColorList = obj.Database.SqlQuery<ColorModal>("GetColorDropDownList").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ColorList;
        }
        public List<SubCategoryImageList> GetSubCategorySideImageList(long? SubCategoryImageId, long? CategoryId, long? SubCategoryId, long? Sub_CategoryImageColorId)
        {
            List<SubCategoryImageList> GetSubCategorySideImageList = new List<SubCategoryImageList>();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@Sub_CategoryImageColorId",Sub_CategoryImageColorId)
                                            };

                GetSubCategorySideImageList = DbEngineObj.Database.SqlQuery<SubCategoryImageList>("GetSub_CategoryImageList @SubCategoryImageId,@CategoryId,@SubCategoryId,@Sub_CategoryImageColorId", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetSubCategorySideImageList;
        }
        public List<Sub_CategorySizeDetailsModel> GetSubCategorySizeDetailsList(long? SubCategoryImageId,long? CategoryId, long? SubCategoryId)
        {
            List<Sub_CategorySizeDetailsModel> GetSubCategorySizeDetailsList = new List<Sub_CategorySizeDetailsModel>();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                            };

                GetSubCategorySizeDetailsList = DbEngineObj.Database.SqlQuery<Sub_CategorySizeDetailsModel>("GetSubCategorySizeDetailsList @SubCategoryImageId,@CategoryId,@SubCategoryId", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetSubCategorySizeDetailsList;
        }
        public SubCategoryImageList GetSubCategorySideImageModel(long? SubCategoryImageId,long? CategoryId, long? SubCategoryId, long? Sub_CategoryImageColorId)
        {
            SubCategoryImageList GetSubCategorySideImageModel = new SubCategoryImageList();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@Sub_CategoryImageColorId",Sub_CategoryImageColorId)
                                            };

                GetSubCategorySideImageModel = DbEngineObj.Database.SqlQuery<SubCategoryImageList>("GetSubCategorySideImageModel @SubCategoryImageId,@CategoryId,@SubCategoryId,@Sub_CategoryImageColorId", parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetSubCategorySideImageModel;
        }
        public SubCategoryImageList GetEditSubCategorySideImageModel(long? ImageSideId,long? SubCategoryImageId,long? CategoryId,long? SubCategoryId,long? Sub_CategoryImageColorId)
        {
            SubCategoryImageList GetSubCategorySideImageModel = new SubCategoryImageList();
            Impulse.BAL.Access.AcqAdmin AcqAdminObj = new Impulse.BAL.Access.AcqAdmin();
            try
            {
                Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@ImageSideId",ImageSideId),
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@Sub_CategoryImageColorId",Sub_CategoryImageColorId)
                                            };

                GetSubCategorySideImageModel = DbEngineObj.Database.SqlQuery<SubCategoryImageList>("GetEditSubCategorySideImageModel @ImageSideId,@SubCategoryImageId,@CategoryId,@SubCategoryId,@Sub_CategoryImageColorId", parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetSubCategorySideImageModel;
        }
        public long GetSubCategorySideImageIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_CategoryImageList.Max(s => (long?)s.Id) == null ? 0 : obj.Sub_CategoryImageList.Max(s => s.Id));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }

        public Sub_CategorySizeDetailsModel GetSubCategorySizeDetailsModel(long? SubCategoryImageId,long? CategoryId,long? SubCategoryId,long? SubcategoryImageColorId)
        {
            Sub_CategorySizeDetailsModel SubCategorySizeDetailsModel = new Sub_CategorySizeDetailsModel();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SqlParameter[] parameters =
                                    {
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId),
                                                new SqlParameter("@SubcategoryImageColorId",SubcategoryImageColorId)
                                            };

                SubCategorySizeDetailsModel = DbEngineObj.Database.SqlQuery<Sub_CategorySizeDetailsModel>("GetSubCategorySizeDetailsModel @Id,@SubCategoryId", parameters).FirstOrDefault();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return SubCategorySizeDetailsModel;
        }

        public List<SizeModel> GetSizeDropDownByCategoryId(long? CategoryId)
        {
            List<SizeModel> SizeList = new List<SizeModel>();
            Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@CategoryId",CategoryId)
                                            };

                SizeList = obj.Database.SqlQuery<SizeModel>("GetSizeDropDownByCategoryId @CategoryId",parameters).ToList();

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return SizeList;
        }

        public Sub_CategorySizeDetailsModel GetEditSubCategorySizeDetailsModal(long? ID,long? SubCategoryImageId,long? CategoryId,long? SubCategoryId)
        {
            Sub_CategorySizeDetailsModel getSubCategorySizeDetailsModel = new Sub_CategorySizeDetailsModel();
            Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                           {
                                                new SqlParameter("@ID",ID),
                                                new SqlParameter("@SubCategoryImageId",SubCategoryImageId),
                                                new SqlParameter("@CategoryId",CategoryId),
                                                new SqlParameter("@SubCategoryId",SubCategoryId)
                                            };

                getSubCategorySizeDetailsModel = obj.Database.SqlQuery<Sub_CategorySizeDetailsModel>("GetEditSubCategorySizeDetailsModal @ID,@SubCategoryImageId,@CategoryId,@SubCategoryId", parameters).FirstOrDefault();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return getSubCategorySizeDetailsModel;
        }


        public long GetSubCategorySizeDetailsIdentityIncreamentId()
        {
            long GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_CategorySizeDetails.Max(s => (long?)s.ID) == null ? 0 : obj.Sub_CategorySizeDetails.Max(s => s.ID));
                GetMaxValue = GetMaxValue + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMaxValue;
        }

        public long? GetSubCategorySizeId()
        {
            long? GetMaxValue = 0;
            try
            {
                Impulse.DbEngine.DbEngine obj = new DbEngine.DbEngine();
                GetMaxValue = (obj.Sub_CategorySizeDetails.Max(s => (long?)s.SubCategorySizeId) == null ? 0 : obj.Sub_CategorySizeDetails.Max(s => s.SubCategorySizeId));
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