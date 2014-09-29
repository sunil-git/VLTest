
/// <summary>
/// Date: 04/07/2011 1:25:14 PM
/// Author:Valuelabs
/// Date: 04/07/2011 1:25:14 PM
/// Class Name:CountriesData
/// Description:This class contains the logic to execute the stored procedure to manipulate the
/// data for Countries related information by using below methods.
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
    
    public partial class CountriesData
    {
        /// <summary>
        /// Default constructor for CountriesData
        /// <summary>
        public CountriesData()
        {
        }

        DBManager dbManager = new DBManager();

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCountries()
        /// StoredProcedure Name:SFsp_GetCountryNames
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetCountryNames"
        /// to get the countries  based  from database.
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetCountries()
        {
            DataSet dsCountries = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dsCountries = dbManager.ExecuteDataSet("SFsp_GetCountryNames", CommandType.StoredProcedure);
               
            }
            catch (Exception ex)
            { 
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsCountries;

        }
       
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCountryById()
        /// StoredProcedure Name:SFsp_GetCountryNames
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetCountryNames"
        /// to get the countries  based CountryId from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetCountryById(int countryId)
        {
            DataSet dsCountries = new DataSet();
            try
            {
                dbManager.OpenConnection();
                switch (countryId)
                {
                    case 0:
                    dbManager.AddParameter("@CountryId", countryId);
                    dsCountries = dbManager.ExecuteDataSet("SFsp_GetCountryNames", CommandType.StoredProcedure);
                    break;                    
                    case 1:
                    dbManager.AddParameter("@CountryId", countryId);
                    dsCountries = dbManager.ExecuteDataSet("SFsp_GetCountryNames", CommandType.StoredProcedure);
                    break;                            
                    default:
                    dbManager.AddParameter("@CountryId", countryId);
                    dsCountries = dbManager.ExecuteDataSet("SFsp_GetCountryNameByCountryId", CommandType.StoredProcedure);
                    break;
                   
                }
                return dsCountries;
               
            }
            catch (Exception ex)
            { 
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsCountries;

        }
    }
}
