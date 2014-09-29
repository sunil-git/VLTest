#region About the Class
/// <summary>
/// Author: Valuelabs
/// Class Name: SerenataCheckoutLogic
/// Description: This class contains the business logic to manipulate the data for new order schema related information by using below methods.
/// <summary>
#endregion

#region Import Section
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
using System.Data;
#endregion

namespace Serenata_Checkout.Bal
{
    public class CustomerDetailsBAL
    {
        CustomerDetailsDAL objCustomerDAL = new CustomerDetailsDAL();

        #region 1. To Validate Customers.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CheckCustomer
        /// Description: This method contains logic to execute stored procedure "SFsp_CreateOrder" to save/update the order details into [Orders] table.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public int CustomerLogin(LoginInfo objLoginInfo, string OrderId)
        {
            return objCustomerDAL.CustomerLogin(objLoginInfo, OrderId);
        }
        #endregion

        #region 2. To check existing Customers.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CheckCustomer
        /// Description: This method contains logic to execute stored procedure "SFsp_CheckCustomer" 
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public int CheckCustomer(string Email)
        {
            return objCustomerDAL.CheckCustomer(Email);
        }
        public int CheckCustomer(string mobile,bool ismobile)
        {
            return objCustomerDAL.CheckCustomer(mobile, ismobile);
        }
        #endregion
        #region 3. To update Customers password.
        public void UpdateCustomerPassword(LoginInfo objLoginInfo, int CustomerId)
        {
            objCustomerDAL.UpdateCustomerPassword(objLoginInfo, CustomerId);
        }

        #endregion
        public string GetCustomerNameByCustomerID(int CustomerID)
        {
            return objCustomerDAL.GetCustomerNameByCustomerID(CustomerID);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetCountries()
        /// StoredProcedure Name:SFsp_GetCountryNames
        /// Description: This method Contains logic  to get the countries   from database.
        /// </summary>
        /// <returns></returns>
        public DataSet GetCountries()
        {
            return objCustomerDAL.GetCountries();
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
            return objCustomerDAL.GetCountryById(CountryId);
        }
        #region . To update customer or billing details
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : EditCustomerDetails()
        /// StoredProcedure Name:SFsp_EditCustomer
        /// Description: This method Contains logic to  execute stored procedure "SFsp_EditCustomer" to save/update the billing details into [Customers].
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <returns>int</returns>
        /// <remarks></remarks>
        public void EditCustomerDetails(CustomerInfo objCustomerInfo)
        {
            objCustomerDAL.EditCustomerDetails(objCustomerInfo);
        }
        #endregion

        #region  Get customer id against an order id
        /// <summary>
        /// Get customer id against an order id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public int GetCustomerIdByOrderId(string OrderId)
        {
            return objCustomerDAL.GetCustomerIdByOrderId(OrderId);
        }
        #endregion

        public string GetCountryDialingCodeByCuontyID(int countyId)
        {
            return objCustomerDAL.GetCountryDialingCodeByCuontyID(countyId);
        }
        public CustomerInfo GetBillingDetails(string orderId)
        {
            return objCustomerDAL.GetBillingDetails(orderId);

        }
        public PasswordReminderInfo GetCustomerDetailsByCustomerID(int customerId)
        {
            return objCustomerDAL.GetCustomerDetailsByCustomerID(customerId);
        }
        public int GetCustomerCountry(string OrderID)
        {
            return objCustomerDAL.GetCustomerCountry(OrderID);
        }

        public int SocailMedaiLogIn(LoginInfo objLoginInfo, string OrderId)
        {
            return objCustomerDAL.SocailMedaiLogIn(objLoginInfo, OrderId);
        }

        public void EditCustomerEmail(CustomerInfo objCustomerInfo)
        {
            objCustomerDAL.EditCustomerEmail(objCustomerInfo);
        }
        public void UpdateCustomerProfileID(string OrderId, string ProfileID)
        {
            objCustomerDAL.UpdateCustomerProfileID(OrderId, ProfileID);
        }

        public string CheckOrderExist(string orderid)
        {
           return objCustomerDAL.CheckOrderExist(orderid);
        }

        public string GetLatestOrderIDByEmail(string email)
        {
          return  objCustomerDAL.GetLatestOrderIDByEmail(email);
        }
    }
}
