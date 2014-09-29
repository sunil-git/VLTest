/// <summary>
/// Date: 04/07/2011 1:25:14 PM
/// Author:Valuelabs
/// Date: 04/07/2011 1:25:14 PM
/// Class Name:CountriesData
/// Description:This class contains the logic to manipulate the
/// data for Countries related information by using below methods.
/// </summary>

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
    public partial class CountriesLogic
    {
        SFMobile.DAL.SiteData.CountriesData objCountries = new DAL.SiteData.CountriesData();

       /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCountries()
        /// StoredProcedure Name:SFsp_GetCountryNames
        /// Description: This method Contains logic  to get the countries   from database.
       /// </summary>
       /// <returns></returns>
        public DataSet GetCountries()
        {
            return objCountries.GetCountries();
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCountryById()
        /// StoredProcedure Name:SFsp_GetCountryNames
        /// Description: This method Contains logic to get the countries  based CountryId from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public DataSet GetCountryById(int CountryId)
        {
            return objCountries.GetCountryById(CountryId);
        } 
    }
}
