/// <summary>
/// Author:Valuelabs
/// Date: 04/07/2011 1:25:14 PM
/// Class Name:ProductsData
/// Description:This class contains the logic to manipulate the
/// data for Caregories related information by using below methods.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SFMobile.DAL;
using SFMobile.DTO;
namespace SFMobile.BAL.SiteData
{
    public partial class CategoriesLogic
    {
        SFMobile.DAL.SiteData.CategoriesData objCategories = new DAL.SiteData.CategoriesData();
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCategoriesByCountryId()
        /// StoredProcedure Name:SFsp_GetCategoryListByCountryId
        /// Description: This method Contains logic to get the Categories  based on countryId from database.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetCategoriesByCountryId(int CountryId)
        {
            return objCategories.GetCategoriesByCountryId(CountryId);
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBannerDetailsByCategoryId()
        /// StoredProcedure Name:SFsp_GetPageAndBannerTitle
        /// Description: This method Contains logic to get the banner details  based on categoryId and countryId from database.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet GetBannerDetailsByCategoryId(int CategoryId, int CountryId)
        {
            return objCategories.GetBannerDetailsByCategoryId(CategoryId, CountryId);
        
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBannerDetailsByCategoryId()
        /// StoredProcedure Name:SFsp_GetPageAndBannerTitle
        /// Description: This method Contains logic to get the banner details  based on categoryId and countryId from database.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet GetBannerDetailsByCategoryId(int CategoryId)
        {
            return objCategories.GetBannerDetailsByCategoryId(CategoryId);

        }
        public DataSet GetBannerDetailsByCategorySiteId(int CategoryId, int SiteId)
        {
            return objCategories.GetBannerDetailsByCategorySiteId(CategoryId,SiteId);

        }
    }
}
