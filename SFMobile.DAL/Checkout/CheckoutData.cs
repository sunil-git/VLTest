/// <summary>
/// Author:Valuelabs
/// Date: 07/26/2011 12:34:14 PM
/// Class Name:CheckoutData
/// Description:This class contains the logic to execute the stored procedure to manipulate the
/// data for checkout related information by using below methods.
/// This class associates with DBManager class to use execute 
/// stored procedure using ADO.Net technology.
/// <summary>
using System;
using System.Linq;
using System.Text;
using System.Data;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace SFMobile.DAL.Checkout
{

    public partial class CheckoutData
    {
      
        DBManager dbManager = new DBManager();
        /// <summary>
        /// Default CheckoutData Constructor
        /// </summary>
        /// <remarks></remarks>
        public CheckoutData()
        {
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertUpdateRecipientDetails()
        /// StoredProcedure Name:SFsp_InsertRecipientDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertRecipientDetails"
        /// to save/update the recipient’s details in to database.
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <returns>int</returns>
        /// <remarks></remarks>
        public int InsertUpdateRecipientDetails(CustomerInfo objCustomerInfo)
        {
            int result = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@Name", objCustomerInfo.Name);
                dbManager.AddParameter("@Lastname", objCustomerInfo.LastName);
                dbManager.AddParameter("@Postcode", objCustomerInfo.PostCode);
                dbManager.AddParameter("@Organisation", objCustomerInfo.Organisation);
                dbManager.AddParameter("@Houseno", objCustomerInfo.HouseNo);
                dbManager.AddParameter("@Street", objCustomerInfo.Street);
                dbManager.AddParameter("@District", objCustomerInfo.District);
                dbManager.AddParameter("@Town", objCustomerInfo.Town);
                dbManager.AddParameter("@County", objCustomerInfo.County);
                dbManager.AddParameter("@Countrycode", objCustomerInfo.CountryCode);
                dbManager.AddParameter("@Phone", objCustomerInfo.ReceipentTelPhNo);
                dbManager.AddParameter("@DeliveryIns", objCustomerInfo.DeliveryIns);
                dbManager.AddParameter("@Nocardmessage", objCustomerInfo.NoCardMessage);
                dbManager.AddParameter("@Message", objCustomerInfo.GiftMessage);
                dbManager.AddParameter("@idOrder", objCustomerInfo.OrderID);
                dbManager.AddParameter("@Addr_Verified", objCustomerInfo.Addr_Verified);
                result = dbManager.ExecuteNonQuery("SFsp_InsertRecipientDetails", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return result;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertUpdateBillingDetails()
        /// StoredProcedure Name:SFsp_InsertBillingDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertBillingDetails"
        /// to save/update the billing details in to database.
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <returns>int</returns>
        /// <remarks></remarks>
        public int InsertUpdateBillingDetails(CustomerInfo objCustomerInfo)
        {
            int result = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@Name", objCustomerInfo.Name);
                dbManager.AddParameter("@Lastname", objCustomerInfo.LastName);
                dbManager.AddParameter("@email", objCustomerInfo.Email);
                dbManager.AddParameter("@organisation", objCustomerInfo.Organisation);
                dbManager.AddParameter("@Houseno", objCustomerInfo.HouseNo);
                dbManager.AddParameter("@address", objCustomerInfo.Street);
                dbManager.AddParameter("@District", objCustomerInfo.District);
                dbManager.AddParameter("@city", objCustomerInfo.Town);
                dbManager.AddParameter("@postcode", objCustomerInfo.PostCode);
                dbManager.AddParameter("@Countrycode", objCustomerInfo.Country);
                dbManager.AddParameter("@Phone", objCustomerInfo.ReceipentTelPhNo);
                dbManager.AddParameter("@EmailOptIn", objCustomerInfo.EmailNewsletter);
                dbManager.AddParameter("@Mobile", objCustomerInfo.UKMobile);
                dbManager.AddParameter("@SMSOptIn", objCustomerInfo.SMSNotification);
                dbManager.AddParameter("@SameAsDelivery", objCustomerInfo.SameAsDelivery);
                dbManager.AddParameter("@voucherCode", objCustomerInfo.VoucherCode);
                dbManager.AddParameter("@OrderID", objCustomerInfo.OrderID);
                dbManager.AddParameter("@Voucher_CountryId", objCustomerInfo.CountryCode);
               
                result = (int)dbManager.ExecuteScalar("SFsp_InsertBillingDetails", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return result;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertCartItemDetails()
        /// StoredProcedure Name:SFsp_InsertIntoProductBasket
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertIntoProductBasket"
        /// to insert  the cart items details  in to database.
        /// </summary>
        /// <param name="objCartInfo">The obj cart info.</param>
        /// <returns>int</returns>
        /// <remarks></remarks>
        public int InsertCartItemDetails(CartInfo objCartInfo)
        {
            int result = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@Action", objCartInfo.ActionType);
                dbManager.AddParameter("@ProductID", objCartInfo.ProductId);
                dbManager.AddParameter("@OrderID", objCartInfo.OrderId);
                dbManager.AddParameter("@Quantity", objCartInfo.Quantity);
                dbManager.AddParameter("@DeliveryDate", objCartInfo.DeliveryDate);
                dbManager.AddParameter("@DeliveryOptionID", objCartInfo.DeliveryOptionId);
                dbManager.AddParameter("@OptionalName", objCartInfo.DeliveryOptionName);
                result = dbManager.ExecuteNonQuery("SFsp_InsertIntoProductBasket", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return result;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetBillingDetails()
        /// StoredProcedure Name:SFsp_GetSameAsDeliveryDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetSameAsDeliveryDetails"
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
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderid);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetSameAsDeliveryDetails", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("organisation")))
                    {
                        objBillingInfo.Organisation = reader.GetString(reader.GetOrdinal("organisation"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("houseno")))
                    {
                        objBillingInfo.HouseNo = reader.GetString(reader.GetOrdinal("houseno"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("street")))
                    {
                        objBillingInfo.Street = reader.GetString(reader.GetOrdinal("street"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("district")))
                    {
                        objBillingInfo.District = reader.GetString(reader.GetOrdinal("district"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("town")))
                    {
                        objBillingInfo.Town = reader.GetString(reader.GetOrdinal("town"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("postcode")))
                    {
                        objBillingInfo.PostCode = reader.GetString(reader.GetOrdinal("postcode"));
                    }                    
                    if (!reader.IsDBNull(reader.GetOrdinal("country")))
                    {
                        objBillingInfo.Country = reader.GetString(reader.GetOrdinal("country"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("County")))
                    {
                        objBillingInfo.County = reader.GetString(reader.GetOrdinal("County"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("PhoneNumber")))
                    {
                        objBillingInfo.ReceipentTelPhNo = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DeliveryInstructions")))
                    {
                        objBillingInfo.DeliveryIns= reader.GetString(reader.GetOrdinal("DeliveryInstructions"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("NoCardMessageChecked")))
                    {
                        objBillingInfo.NoCardMessage = reader.GetInt32(reader.GetOrdinal("NoCardMessageChecked"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Message")))
                    {
                        objBillingInfo.GiftMessage = reader.GetString(reader.GetOrdinal("Message"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                    {
                        objBillingInfo.Name = reader.GetString(reader.GetOrdinal("Name"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Lastname")))
                    {
                        objBillingInfo.LastName = reader.GetString(reader.GetOrdinal("Lastname"));
                    }

                    
                }
               
                return objBillingInfo;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return objBillingInfo;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetExtrasByProdId()
        /// StoredProcedure Name:Sfsp_GetGiftProducts
        /// Description: This method Contains logic to  execute stored procedure "Sfsp_GetGiftProducts"
        /// to get the gift products from database.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public DataSet GetExtrasByProdId( string orderId, int productId)
        {
            DataSet dsExtras = new DataSet();
            try
            {
                if (productId > 0)
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", orderId);
                    dbManager.AddParameter("@ProductId", productId);
                    dsExtras = dbManager.ExecuteDataSet("Sfsp_GetGiftProducts", CommandType.StoredProcedure);
                    
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsExtras;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : RetainedBillingDetails()
        /// StoredProcedure Name:SFsp_GetBillingDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetBillingDetails"
        /// to get the billing information  from database.
        /// </summary>
        /// <param name="orderid">The orderid.</param>
        /// <returns>CustomerInfo</returns>
        /// <remarks></remarks>
        public CustomerInfo RetainedBillingDetails(string orderid)
        {
            CustomerInfo objBillingInfo = new CustomerInfo();

            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderid);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetBillingDetails", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                    {
                        objBillingInfo.Name = reader.GetString(reader.GetOrdinal("FirstName"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Lastname")))
                    {
                        objBillingInfo.LastName = reader.GetString(reader.GetOrdinal("Lastname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmailAddress")))
                    {
                        objBillingInfo.Email = reader.GetString(reader.GetOrdinal("EmailAddress"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("organisation")))
                    {
                        objBillingInfo.Organisation = reader.GetString(reader.GetOrdinal("organisation"));
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
                        objBillingInfo.District = reader.GetString(reader.GetOrdinal("Address3"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("City")))
                    {
                        objBillingInfo.Town = reader.GetString(reader.GetOrdinal("City"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("postcode")))
                    {
                        objBillingInfo.PostCode = reader.GetString(reader.GetOrdinal("postcode"));
                    }                    
                    if (!reader.IsDBNull(reader.GetOrdinal("country")))
                    {
                        objBillingInfo.Country = reader.GetString(reader.GetOrdinal("country"));
                    }                    
                    if (!reader.IsDBNull(reader.GetOrdinal("ContactTel")))
                    {
                        objBillingInfo.ReceipentTelPhNo = reader.GetString(reader.GetOrdinal("ContactTel"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("SameAsDelivery")))
                    {
                        objBillingInfo.SameAsDelivery = reader.GetInt32(reader.GetOrdinal("SameAsDelivery"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmailNewsletter")))
                    {
                        objBillingInfo.EmailNewsletter = reader.GetInt32(reader.GetOrdinal("EmailNewsletter"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("UKMobileNumber")))
                    {
                        objBillingInfo.UKMobile = reader.GetString(reader.GetOrdinal("UKMobileNumber"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("SMSNotifications")))
                    {
                        objBillingInfo.SMSNotification = reader.GetInt32(reader.GetOrdinal("SMSNotifications"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("VoucherCode")))
                    {
                        objBillingInfo.VoucherCode = reader.GetString(reader.GetOrdinal("VoucherCode"));
                    }

                }

                return objBillingInfo;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return objBillingInfo;
        }
        public void UpdateEncryptedOrderId(string orderId,string encryptedOrderId)
        {
            int response;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@IdOrder", orderId);
                dbManager.AddParameter("@EncryptedOrderID", encryptedOrderId);
                response = dbManager.ExecuteNonQuery("SFsp_UpdateEncryptedOrderID", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : PostcodeValid()
        /// StoredProcedure Name:SFsp_CheckValidPostCode
        /// Description: This method Contains logic to  execute stored procedure "SFsp_CheckValidPostCode"
        /// to get the Valid information  from database.
        /// </summary>
        /// <param name="orderid">The orderid.</param>
        /// <returns>dataSet</returns>
        /// <remarks></remarks>
        public DataSet PostcodeValid(string postCode, string orderId)
        {

            DataSet dsPostcode = new DataSet();
            try
            {
                if (postCode !="")
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@PostCode", postCode);
                    dbManager.AddParameter("@OrderID", orderId);
                    dsPostcode = dbManager.ExecuteDataSet("SFsp_CheckValidPostCode", CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsPostcode;
        }
    }
}
