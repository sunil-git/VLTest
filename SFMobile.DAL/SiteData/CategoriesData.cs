
/// <summary>
/// Author:Valuelabs
/// Date: 04/07/2011 1:25:14 PM
/// Class Name:ProductsData
/// Description:This class contains the logic to execute the stored procedure to manipulate the
/// data for Caregories related information by using below methods.
/// This class associates with DBManager class to use execute 
/// stored procedure using ADO.Net technology.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SFMobile.Exceptions;
namespace SFMobile.DAL.SiteData
{
    public partial class CategoriesData
    {
        /// <summary>
        /// Default constructor for CaregoriesData
        /// <summary>
        public CategoriesData()
        {
        }

        DBManager dbManager = new DBManager();

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCategoriesByCountryId()
        /// StoredProcedure Name:SFsp_GetCategoryListByCountryId
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetCategoryListByCountryId"
        /// to get the Categories  based on countryId from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetCategoriesByCountryId(int countryId)
        {
            DataSet GetCategories = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CountryId", countryId);
                GetCategories = dbManager.ExecuteDataSet("SFsp_GetCategoryListByCountryId", CommandType.StoredProcedure);
               
            }
            catch(Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return GetCategories;

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBannerDetailsByCategoryId()
        /// StoredProcedure Name:SFsp_GetPageAndBannerTitle
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetPageAndBannerTitle"
        /// to get the banner details  based on categoryId and CountryId from database.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet GetBannerDetailsByCategoryId(int categoryId, int countryId)
        {

            DataSet GetCategoriesBanner = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CategoryID", categoryId);
                dbManager.AddParameter("@CountryID", countryId);              
                GetCategoriesBanner = dbManager.ExecuteDataSet("SFsp_GetPageAndBannerTitle", CommandType.StoredProcedure);
               
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return GetCategoriesBanner;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBannerDetailsByCategoryId()
        /// StoredProcedure Name:SFsp_GetPageAndBannerTitle
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetPageAndBannerTitle"
        /// to get the banner details  based on categoryId and CountryId from database.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet GetBannerDetailsByCategoryId(int categoryId)
        {

            DataSet GetCategoriesBanner = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CategoryID", categoryId);
                GetCategoriesBanner = dbManager.ExecuteDataSet("SFsp_GetPageAndBannerTitleByCategoryID", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return GetCategoriesBanner;
        }
        public DataSet GetBannerDetailsByCategorySiteId(int categoryId,int siteId)
        {

            DataSet GetCategoriesBanner = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CategoryID", categoryId);
                dbManager.AddParameter("@SiteID", siteId);
                GetCategoriesBanner = dbManager.ExecuteDataSet("SFsp_GetPageAndBannerTitle", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return GetCategoriesBanner;
        }
    }
}
