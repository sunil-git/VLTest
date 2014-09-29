#region About the class
/// <summary>
/// Author:Valuelabs
/// Class Name:SerenataCheckout
/// Description:It contains list of functions to execute stored procedures with respective parameters.
/// </summary>
#endregion

#region Import Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Serenata_Checkout.Log;
using Serenata_Checkout.Dto;
using System.Configuration;
#endregion


namespace Serenata_Checkout.Dal
{
    public  class CustomerDetailsDAL
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

        public int CustomerLogin(LoginInfo objLoginInfo, string OrderId)
        {
            int customerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@Email", objLoginInfo.EmailAddress);
                dbManager.AddParameter("@EncryptedPassword", objLoginInfo.EncryptedPassword);
                dbManager.AddParameter("@OrderID", OrderId);
                customerId = (int)dbManager.ExecuteScalar("SFsp_CheckCustomer_NewCheckout", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerId;
        }

        public int SocailMedaiLogIn(LoginInfo objLoginInfo, string OrderId)
        {
            int customerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@Email", objLoginInfo.EmailAddress);
                dbManager.AddParameter("@SocialUserID", objLoginInfo.SocialUserID);
                dbManager.AddParameter("@SocialMediaType", objLoginInfo.SocialMediaType);
                dbManager.AddParameter("@OrderID", OrderId);
                customerId = (int)dbManager.ExecuteScalar("SFsp_SocialMediaLogIn", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerId;
        }

        public int CheckCustomer(string Email)
        {
            int customerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@Email", Email);
                customerId = (int)dbManager.ExecuteScalar("SFsp_CheckExistingCustomer", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerId;
        }
        public int CheckCustomer(string mobile,bool ismobile)
        {
            int customerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@Mobile", mobile);
                customerId = (int)dbManager.ExecuteScalar("SFsp_CheckExistingCustomer", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerId;
        }
        public void UpdateCustomerPassword(LoginInfo objLoginInfo, int CustomerId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@CustomerID", CustomerId);
                dbManager.AddParameter("@EncryptedPassword", objLoginInfo.EncryptedPassword);
                dbManager.ExecuteScalar("SFsp_UpdateCustomerPassword", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        public string GetCustomerNameByCustomerID(int CustomerID)
        {
            string customerName = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@CustomerID", CustomerID);
                customerName = (string)dbManager.ExecuteScalar("SFsp_GetCustomerNameByCustomerID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerName;
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
                dbManager.OpenConnection(connectionString);
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
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsCountries;

        }


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
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsCountries;

        }
        #region . To update customer or billing details
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : EditCustomerDetails()
        /// StoredProcedure Name:SFsp_EditCustomer
        /// Description: This method Contains logic to  execute stored procedure "SFsp_EditCustomer" to save/update the billing details into [Customers].
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <remarks></remarks>
        public void EditCustomerDetails(CustomerInfo objCustomerInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
              
                dbManager.AddParameter("@OrderID", objCustomerInfo.OrderID);
                dbManager.AddParameter("@CustomerID", objCustomerInfo.CustomerId);
                dbManager.AddParameter("@EmailAddress", objCustomerInfo.Email);
                dbManager.AddParameter("@Mobile", objCustomerInfo.UKMobile);
                dbManager.AddParameter("@FirstName", objCustomerInfo.FirstName);
                dbManager.AddParameter("@LastName", objCustomerInfo.LastName);
                dbManager.AddParameter("@Organisation", objCustomerInfo.Organisation);
                dbManager.AddParameter("@Address1", objCustomerInfo.HouseNo);
                dbManager.AddParameter("@Address2", objCustomerInfo.Street);
                dbManager.AddParameter("@Address3", objCustomerInfo.District);
                dbManager.AddParameter("@Town", objCustomerInfo.Town);
                dbManager.AddParameter("@Postcode", objCustomerInfo.PostCode);
                dbManager.AddParameter("@CountryID", objCustomerInfo.CountryID);
                dbManager.AddParameter("@EmailOptIn", objCustomerInfo.EmailNewsletter);
                dbManager.AddParameter("@SMSOptIn", objCustomerInfo.SMSNotification);
                if(!string.IsNullOrEmpty(objCustomerInfo.EncryptedPassword))
                {
                 dbManager.AddParameter("@EncryptedPassword", objCustomerInfo.EncryptedPassword);
                }
                
                dbManager.ExecuteScalar("SFsp_EditCustomer", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region . Get customer id against an order id
        /// <summary>
        /// Get customer id against an order id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public int GetCustomerIdByOrderId(string OrderId)
        {
            int CustomerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                CustomerId = (int)dbManager.ExecuteScalar("SFsp_GetCustomerIDByOrderID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return CustomerId;
        }
        #endregion
        public string GetCountryDialingCodeByCuontyID(int countyId)
        {
            string customerName = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@CountryID", countyId);
                customerName = (string)dbManager.ExecuteScalar("SFsp_GetDailingCodeByCountryID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return customerName;
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBillingDetails()
        /// StoredProcedure Name:SFsp_GetCustomerDetailsByOrderID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetCustomerDetailsByOrderID"
        /// to get the billing information  from database.
        /// </summary>
        /// <param name="orderid">The orderid.</param>
        /// <returns>CustomerInfo</returns>
        /// <remarks></remarks>
        public CustomerInfo GetBillingDetails(string orderid)
        {
            CustomerInfo objBillingInfo = new CustomerInfo();

            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderid);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCustomerDetailsByOrderID", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("CustomerID")))
                    {
                        objBillingInfo.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                    {
                        objBillingInfo.Title = reader.GetString(reader.GetOrdinal("Title"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmailAddress")))
                    {
                        objBillingInfo.Email = reader.GetString(reader.GetOrdinal("EmailAddress"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                    {
                        objBillingInfo.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("LastName")))
                    {
                        objBillingInfo.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Organisation")))
                    {
                        objBillingInfo.Organisation = reader.GetString(reader.GetOrdinal("Organisation"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Address1")))
                    {
                        objBillingInfo.HouseNo = reader.GetString(reader.GetOrdinal("Address1"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Address2")))
                    {
                        objBillingInfo.Street = reader.GetString(reader.GetOrdinal("Address2"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Address3")))
                    {
                        objBillingInfo.District= reader.GetString(reader.GetOrdinal("Address3"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Town")))
                    {
                        objBillingInfo.Town = reader.GetString(reader.GetOrdinal("Town"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Postcode")))
                    {
                        objBillingInfo.PostCode = reader.GetString(reader.GetOrdinal("Postcode"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("County")))
                    {
                        objBillingInfo.County = reader.GetString(reader.GetOrdinal("County"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Mobile")))
                    {
                        objBillingInfo.UKMobile = reader.GetString(reader.GetOrdinal("Mobile"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Country")))
                    {
                        objBillingInfo.Country = reader.GetString(reader.GetOrdinal("Country"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ISOCountryCode")))
                    {
                        objBillingInfo.ISOCountry = reader.GetString(reader.GetOrdinal("ISOCountryCode"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EncryptedPassword")))
                    {
                        objBillingInfo.EncryptedPassword = reader.GetString(reader.GetOrdinal("EncryptedPassword"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("CountryID")))
                    {
                        objBillingInfo.CountryID = reader.GetInt32(reader.GetOrdinal("CountryID"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmailOptIn")))
                    {
                        objBillingInfo.EmailNewsletter = reader.GetInt32(reader.GetOrdinal("EmailOptIn"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmailOptIn")))
                    {
                        objBillingInfo.SMSNotification = reader.GetInt32(reader.GetOrdinal("SMSOptIn"));
                    }

                }

                return objBillingInfo;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return objBillingInfo;
        }

        public int GetCustomerCountry(string OrderID)
        {
            int countryID = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderID);
                countryID = (int)dbManager.ExecuteScalar("SFsp_GetCustomerCountry", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return countryID;
        }


        public PasswordReminderInfo GetCustomerDetailsByCustomerID(int customerId)
        {
            PasswordReminderInfo objPasswordReminderInfo = new PasswordReminderInfo();

            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@CustomerID", customerId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCustomerDetailsByCustomerID", CommandType.StoredProcedure);
                while (reader.Read())
                {
                   
                    if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                    {
                        objPasswordReminderInfo.Name = reader.GetString(reader.GetOrdinal("Name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Mobile")))
                    {
                        objPasswordReminderInfo.Mobile = reader.GetString(reader.GetOrdinal("Mobile"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("SiteID")))
                    {
                        objPasswordReminderInfo.SiteId = reader.GetInt32(reader.GetOrdinal("SiteID"));
                    }                   
                                        

                }

                return objPasswordReminderInfo;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return objPasswordReminderInfo;
        }

        public void EditCustomerEmail(CustomerInfo objCustomerInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);

                //dbManager.AddParameter("@OrderID", objCustomerInfo.OrderID);
                dbManager.AddParameter("@CustomerID", objCustomerInfo.CustomerId);
                dbManager.AddParameter("@EmailAddress", objCustomerInfo.Email);                
                dbManager.ExecuteScalar("SFsp_EditCustomerEmail", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }

        public void UpdateCustomerProfileID(string OrderId, string  ProfileID)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@ProfileID", ProfileID);
                dbManager.ExecuteScalar("SFsp_UpdateCustomerProfileID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }


        public string GetLatestOrderIDByEmail(string Email)
        {
            string OID = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@CustomerEmail", Email);
                //SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetMainProductFromBasket", CommandType.StoredProcedure);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderIDByCustomerEmail", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                        {
                            OID = Convert.ToString(reader["OrderID"]);
                        }
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return OID;

        }

        public string CheckOrderExist(string OrderID)
        {
            string OID = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderID);
                //SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetMainProductFromBasket", CommandType.StoredProcedure);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_CheckOrderExists", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                        {
                            OID = Convert.ToString(reader["OrderID"]);
                        }
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return OID;

        }

     

       



    }
}
