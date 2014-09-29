#region About the class
/// <summary>
/// Author:Valuelabs
/// Date: 15/11/2012 3:20:14 PM
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
using SFMobile.Exceptions;
using SFMobile.DTO;
using System.Configuration;
#endregion

#region Class Definition
namespace SerenataOrderSchema.Checkout
{
    public class SerenataCheckout
    {

        DBManager dbManager = new DBManager();
        private string connectionString = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;  

        #region 1.To create an order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CreateOrder
        /// Description: This method contains logic to execute stored procedure "SFsp_CreateOrder" to save/update the order details into [Orders] table.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public string CreateOrder(OrderDTO objOrder)
        {
            string strOrderId = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrder.OrderId);
                dbManager.AddParameter("@Prefix", objOrder.Prefix);
                dbManager.AddParameter("@IPAddress", objOrder.ServerIpAddress);
                dbManager.AddParameter("@CustomerID", objOrder.IdCustomer);
                dbManager.AddParameter("@CookieID", objOrder.CookieId);
                dbManager.AddParameter("@SessionID", objOrder.SessionId);
                dbManager.AddParameter("@OrderStatusID", objOrder.IdorderStatus);
                dbManager.AddParameter("@SiteID", objOrder.SiteId);
                dbManager.AddParameter("@ChannelID", objOrder.IdChannel);
                dbManager.AddParameter("@CurrencyID", objOrder.CurrencyId);
                dbManager.AddParameter("@BrowserIP", objOrder.UserIpAddress);
                dbManager.AddParameter("@BrowserCountry", objOrder.BrowserCountry);
                dbManager.AddParameter("@CountryID", objOrder.CountryCode);
                strOrderId = (string)dbManager.ExecuteScalar("SFsp_CreateOrder_NewCheckout", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return strOrderId;
        }
        #endregion

        #region 2.To add product details into the order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: AddProductLine()
        /// StoredProcedure Name: Sfsp_AddProductLine
        /// Description: This method contains logic to  execute stored procedure "Sfsp_AddProductLine" to save/update the product details into [OrderLines] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void AddProductLine(OrderDTO objOrder)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrder.OrderId);
                dbManager.AddParameter("@ProductID", objOrder.ProductId);
                dbManager.AddParameter("@Description", objOrder.Description);
                dbManager.AddParameter("@ProdVATRate", objOrder.ProdVATRate);
                dbManager.AddParameter("@Price", objOrder.Price);
                dbManager.AddParameter("@PartnerID", objOrder.PartnerID);
                dbManager.ExecuteScalar("Sfsp_AddProductLine", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 3.To schedule the dispatch.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ScheduleDispatch()
        /// StoredProcedure Name: Sfsp_RescheduleDispatch
        /// Description: This method contains logic to execute stored procedure "Sfsp_RescheduleDispatch" to save/update the dispatch details into [Dispatches] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void ScheduleDispatch(OrderDTO objOrder)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrder.OrderId);
                dbManager.AddParameter("@DeliveryDate", objOrder.DeliveryDate);
                dbManager.AddParameter("@DelOptionID", objOrder.DeliveryOptionId);
                dbManager.AddParameter("@CarrierID", objOrder.DeliveryPartnerID);
                dbManager.AddParameter("@DeliveryTime", objOrder.OptionalName);
                dbManager.AddParameter("@DeliveryPrice", objOrder.OptionalCost);
                dbManager.ExecuteScalar("Sfsp_RescheduleDispatch", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 4. To Update Product Quantity
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateProductQuantity()
        /// StoredProcedure Name: SFsp_UpdateProductQuantity
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateProductQuantity" to save/update the quantity details into [orderline] table.
        /// </summary>
        /// <param name="objOrderInfoNew"></param>
        public void UpdateProductQuantity(OrderDTO objOrderInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrderInfoNew.OrderId);
                dbManager.AddParameter("@ProductID", objOrderInfoNew.ProductId);
                dbManager.AddParameter("@Quantity", objOrderInfoNew.Quantity);
                dbManager.AddParameter("@UpsaleCount", objOrderInfoNew.UpsaleCount);    
                dbManager.ExecuteScalar("SFsp_UpdateProductQuantity", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 5. To Delete Product from basket
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateProductQuantity()
        /// StoredProcedure Name: SFsp_UpdateProductQuantity
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateProductQuantity" to save/update the quantity details into [orderline] table.
        /// </summary>
        /// <param name="objOrderInfoNew"></param>
        public void DeleteProductFromBasket(OrderDTO objOrderInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrderInfoNew.OrderId);
                dbManager.AddParameter("@ProductID", objOrderInfoNew.ProductId);
                dbManager.AddParameter("@UpsaleCount", objOrderInfoNew.UpsaleCount);    
                dbManager.ExecuteScalar("SFsp_DeleteProductFromBasket", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 6. To edit the dispatch address
        /// <summary>
        /// To edit the dispatch address
        /// </summary>
        /// <param name="OrderID">Accepts Order ID as string type</param>
        /// <param name="Title">Accepts Title of customer as string type</param>
        /// <param name="FirstName">Accepts Customer's First name as string type</param>
        /// <param name="LastName">Accepts last name as string type</param>
        /// <param name="Organisation">Accepts name of organization</param>
        /// <param name="Address1">Accepts address name 1</param>
        /// <param name="Address2">Accepts address name 2</param>
        /// <param name="Address3">Accepts address name 3</param>
        /// <param name="Town">Accepts town name as string type</param>
        /// <param name="Mobile">Accepts mobile no.</param>
        /// <param name="Postcode">Accepts post code</param>
        /// <param name="County">Accepts name of county</param>
        /// <param name="CountryID">Accepts country id </param>
        /// <example><code>obj.Sfsp_EditDispatchAddress("M1244","Mr.","James","Saundar","Capita Total Document Solutions","Unit 331-333","Great Guildford Business Square","30 Great Guildford Street","London","9938371212","SE1 0HS","yorkshire",215,1);</code></example>
        public void EditDispatchAddress(CustomerInfo objcustomerInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objcustomerInfoNew.OrderID);
                dbManager.AddParameter("@Title", objcustomerInfoNew.Title);
                dbManager.AddParameter("@FirstName", objcustomerInfoNew.Name);
                dbManager.AddParameter("@LastName", objcustomerInfoNew.LastName);
                dbManager.AddParameter("@Organisation", objcustomerInfoNew.Organisation);
                dbManager.AddParameter("@Address1", objcustomerInfoNew.HouseNo);
                dbManager.AddParameter("@Address2", objcustomerInfoNew.Street);
                dbManager.AddParameter("@Address3", objcustomerInfoNew.District);
                dbManager.AddParameter("@Town", objcustomerInfoNew.Town);
                dbManager.AddParameter("@Mobile", objcustomerInfoNew.ReceipentTelPhNo);
                dbManager.AddParameter("@Postcode", objcustomerInfoNew.PostCode);
                dbManager.AddParameter("@County", objcustomerInfoNew.County);
                dbManager.AddParameter("@CountryID", objcustomerInfoNew.CountryID);
                dbManager.AddParameter("@Addr_Verified", objcustomerInfoNew.Addr_Verified);
                dbManager.ExecuteScalar("Sfsp_EditDispatchAddress", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 7. To edit delivery instructions
        /// <summary>
        /// To edit delivery instructions.
        /// </summary>
        public void EditDeliveryInstructions(CustomerInfo objCustomerInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objCustomerInfoNew.OrderID);
                dbManager.AddParameter("@DeliveryInstructions", objCustomerInfoNew.DeliveryIns);
                dbManager.ExecuteScalar("SFsp_EditDeliveryInstructions", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 8. To edit card message
        /// <summary>
        /// To edit card message
        /// </summary>
        public void EditCardMessage(CustomerInfo objCustomerInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objCustomerInfoNew.OrderID);         
                dbManager.AddParameter("@Message", objCustomerInfoNew.GiftMessage);
                dbManager.ExecuteScalar("SFsp_EditCardMessage", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 9. To update customer or billing details
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
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@CustomerID", objCustomerInfo.CustomerId);
                dbManager.AddParameter("@OrderID", objCustomerInfo.OrderID);
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
                dbManager.ExecuteScalar("SFsp_EditCustomer", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 10. To update VoucherCode
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : UpdateVoucherCode()
        /// StoredProcedure Name:SFsp_EditVoucherCode
        /// Description: This method Contains logic to  execute stored procedure "SFsp_EditVoucherCode" to save/update the valid voucher code into [Orders].
        /// </summary>
        /// <param name="objOrderInfo">The obj order info.</param>
        /// <remarks></remarks>
        public void EditVoucherCode(OrderDTO objOrderInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrderInfo.OrderId);
                dbManager.AddParameter("@VoucherID", objOrderInfo.VoucherID);
                dbManager.AddParameter("@Discount", objOrderInfo.Discount);
                dbManager.AddParameter("@Total", objOrderInfo.Total);
                dbManager.ExecuteScalar("SFsp_EditVoucherCode", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 11. To update the encrypted order id
        /// <summary>
        /// Author:Valuelabs
        /// Method Name: UpdateEncryptedOrderId()
        /// StoredProcedure Name: SFsp_UpdateOrderWithEncryptedOrderID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_UpdateOrderWithEncryptedOrderID" to update the encrypted order id into [Orders].
        /// </summary>
        /// <param name="objCustomerInfo">The obj order info.</param>
        /// <remarks></remarks>
        public void UpdateEncryptedOrderId(OrderDTO objOrderInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objOrderInfo.OrderId);
                dbManager.AddParameter("@EncryptedOrderID", objOrderInfo.EncryptedOrderID);
                dbManager.ExecuteScalar("SFsp_UpdateOrderWithEncryptedOrderID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 12. Get customer id against an order id
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
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", OrderId);
                CustomerId = (int)dbManager.ExecuteScalar("SFsp_GetCustomerIDByOrderID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return CustomerId;
        }
        #endregion

        #region 13. Get browser country
        /// <summary>
        /// Get browser country name against an user ip address.
        /// </summary>
        /// <param name="UserIp"></param>
        /// <returns>string</returns>
        public string GetBrowserCountry(string UserIp)
        {
            string BrowserCountry = string.Empty;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@IPAddress", UserIp);
                BrowserCountry = (string)dbManager.ExecuteScalar("SFsp_GetCountryFromIPAddress", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return BrowserCountry;
        }
        #endregion

        #region 14. To Accept the address for verified or not
        /// <summary>
        /// To edit card message
        /// </summary>
        public void AcceptAddress(CustomerInfo objCustomerInfoNew)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", objCustomerInfoNew.OrderID);               
                dbManager.AddParameter("@Addr_verified", objCustomerInfoNew.Addr_Verified);
                dbManager.ExecuteScalar("Sfsp_AcceptAddress", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 15. Validate voucher code
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ValidateVoucherCode()
        /// StoredProcedure Name: SFsp_ValidateVoucher
        /// Description: This method contains logic to execute stored procedure "SFsp_ValidateVoucher" to validate voucher code and update voucher id,discount and total  in [Orders] table.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns>OrderInfo</returns>
        public OrderDTO ValidateVoucherCode(OrderDTO objOrderInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderId", objOrderInfo.OrderId);
                dbManager.AddParameter("@VoucherCode", objOrderInfo.VoucherCode);
                dbManager.AddParameter("@SiteID", objOrderInfo.SiteId);
                dbManager.ExecuteScalar("SFsp_ValidateVoucher", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objOrderInfo;
        }
        #endregion

        #region 16. Update Order Complete
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOrderComplete()
        /// StoredProcedure Name: SFsp_UpdateOrderComplete
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOrderComplete" to update order complete status in [Orders] table.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>void</returns>
        public void UpdateOrderComplete(string OrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderId", OrderId);
                dbManager.AddParameter("@ByWhom", "Customer");
                dbManager.ExecuteDataSet("SFsp_UpdateOrderComplete", CommandType.StoredProcedure);
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
        #endregion

        #region 17. Update Order Status for Trial Pay
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOrderStatusForTrialPay()
        /// StoredProcedure Name: SFsp_UpdateOrderStatusForTrailPay
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOrderStatusForTrailPay" to update order status for trail pay voucher title in [Orders] table.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>void</returns>
        public void UpdateOrderStatusForTrialPay(string OrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderId", OrderId);
                dbManager.ExecuteDataSet("SFsp_UpdateOrderStatusForTrailPay", CommandType.StoredProcedure);
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
        #endregion

        #region 18. To get product price details.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetProductPriceDetails()
        /// StoredProcedure Name: SFsp_GetProductPriceDetails
        /// Description: This method contains logic to  execute stored procedure "SFsp_GetProductPriceDetails" to get product price details from [T_ProductData] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public OrderDTO GetProductPriceDetails(OrderDTO objOrder)
        {
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductID", objOrder.ProductId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductPriceDetails", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                    {
                        objOrder.Price = Convert.ToDouble(reader["Price"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("VATRate")))
                    {
                        objOrder.ProdVATRate = Convert.ToDouble(reader["VATRate"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("PartnerID")))
                    {
                        objOrder.PartnerID = Convert.ToInt32(reader["PartnerID"]);
                    }
                }
                return objOrder;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return objOrder;
        }
        #endregion

        #region 19. To get delivery details by using delivery option ID.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetDeliveryDetailsByDelOptionID()
        /// StoredProcedure Name: SFsp_GetDeliveryDetailsByDelOptionID
        /// Description: This method contains logic to execute stored procedure "SFsp_GetDeliveryDetailsByDelOptionID" to get delivery details from [T_Delivery_Option] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public OrderDTO GetDeliveryDetailsByDelOptionID(OrderDTO objOrder)
        {
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@DelOptionID", objOrder.DeliveryOptionId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryDetailsByDelOptionID", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("DeliveryPartnerID")))
                    {
                        objOrder.DeliveryPartnerID = Convert.ToInt32(reader["DeliveryPartnerID"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("OptionName")))
                    {
                        objOrder.OptionalName = Convert.ToString(reader["OptionName"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("OptionPrice")))
                    {
                        objOrder.OptionalCost = Convert.ToSingle(reader["OptionPrice"].ToString());
                    }
                }
                return objOrder;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return objOrder;
        }
        #endregion

        #region 20. Get upsale count by using OrderID.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetUpsaleCount()
        /// StoredProcedure Name: SFsp_GetUpsaleCountInBasket
        /// Description: This method contains logic to execute stored procedure "SFsp_GetUpsaleCountInBasket" to get upsalecount
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetUpsaleCount(string orderId)
        {
            int count = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                count = (int)dbManager.ExecuteScalar("SFsp_GetUpsaleCountInBasket", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return count;
        }

        #endregion

        #region 21. To get VoucherID by using VoucherCode.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetVoucherIDByVoucherCode()
        /// StoredProcedure Name: SFsp_GetVoucherIDByVoucherCode
        /// Description: This method contains logic to execute stored procedure "SFsp_GetVoucherIDByVoucherCode" to get VoucherID by using VoucherCode.
        /// </summary>
        /// <param name="voucherCode"></param>
        /// <returns>int</returns>
        public int GetVoucherIDByVoucherCode(string voucherCode)
        {
            int voucherID = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@VoucherCode", voucherCode);
                voucherID = (int)dbManager.ExecuteScalar("SFsp_GetVoucherIDByVoucherCode", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return voucherID;
        }
        #endregion

        #region 22. Update Occasion and funeral time for a given order
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOccasionAndFuneralTime()
        /// StoredProcedure Name: SFsp_UpdateOccasionAndFuneralTime
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOccasionAndFuneralTime" to update Occasion and funeral time for a given order in [Dispatches] and [Orders] table.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>void</returns>
        public void UpdateOccasionAndFuneralTime(string OrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.ExecuteScalar("SFsp_UpdateOccasionAndFuneralTime", CommandType.StoredProcedure);
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
        #endregion

        #region 23. Validate voucher code for existing order schema transactions
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ValidateVoucherCodeForExistingOrder()
        /// StoredProcedure Name: SFsp_ValidateVoucher
        /// Description: This method contains logic to execute stored procedure "SFsp_ValidateVoucher" to validate voucher code and update voucher id,discount and total  in [T_Orders] table.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns>int</returns>
        public int ValidateVoucherCodeForExistingOrder(OrderDTO objOrderInfo)
        {
            int result = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderId", objOrderInfo.OrderId);
                dbManager.AddParameter("@VoucherCode", objOrderInfo.VoucherCode);
                dbManager.AddParameter("@SiteID", objOrderInfo.SiteId);
                result = (int)dbManager.ExecuteScalar("SFsp_ValidateVoucherAndCalculateDiscount", CommandType.StoredProcedure);
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
        #endregion





        #region 6. To edit the dispatch service type
        /// <summary>
        /// To edit the dispatch service type
        /// </summary>
        public void EditDispatchServiceType(string OrderID, DateTime DeliveryDate, int DelOptionID)
        {
            try
            {
           
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@DeliveryDate", DeliveryDate);
                dbManager.AddParameter("@DelOptionID", DelOptionID);
                dbManager.ExecuteScalar("Sfsp_EditDispatchServiceType", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion
        
        #region 6. To resend the partial order
        /// <summary>
        /// To resend the partial order
        /// </summary>
        public void ResendPartialOrder(string OrderID, int DelOptionID, DateTime DeliveryDate)
        {
            try
            {
             
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@DelOptionID", DelOptionID);
                dbManager.AddParameter("@DeliveryDate", DeliveryDate);
                dbManager.ExecuteScalar("Sfsp_ResendPartialOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 7. To resend the Order
        /// <summary>
        /// To resend the Order
        /// </summary>
        public void ResendOrder(string OrderID, int DelOptionID, DateTime DeliveryDate)
        {
            try
            {
              
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@DelOptionID", DelOptionID);
                dbManager.AddParameter("@DeliveryDate", DeliveryDate);
                dbManager.ExecuteScalar("Sfsp_ResendOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 8. To cancel the order
        /// <summary>
        /// To cancel the order
        /// </summary>
        public void SetOrderToCancel(string OrderID)
        {
            try
            {
              
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.ExecuteScalar("Sfsp_SetOrderToCancel", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 9. To refund the order
        /// <summary>
        /// To refund the order 
        /// </summary>
        public void RefundOrder(string OrderID, int PaymentTypeID, int PaymentGatewayID,
            int PaymentStatus, DateTime PaymentDate, double Amount)
        {
            try
            {
            
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@PaymentTypeID", PaymentTypeID);
                dbManager.AddParameter("@PaymentGatewayID", PaymentGatewayID);
                dbManager.AddParameter("@PaymentStatus", PaymentStatus);
                dbManager.AddParameter("@PaymentDate", PaymentDate);
                dbManager.AddParameter("@Amount", Amount);
                dbManager.ExecuteScalar("Sfsp_RefundOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 10. To refund delivery charge
        /// <summary>
        /// To refund delivery charge
        /// </summary>
        public void RefundDeliveryCharge(string OrderID, int PaymentTypeID, int PaymentGatewayID,
            int PaymentStatus, DateTime PaymentDate, double Amount)
        {
            try
            {
             
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@PaymentTypeID", PaymentTypeID);
                dbManager.AddParameter("@PaymentGatewayID", PaymentGatewayID);
                dbManager.AddParameter("@PaymentStatus", PaymentStatus);
                dbManager.AddParameter("@PaymentDate", PaymentDate);
                dbManager.AddParameter("@Amount", Amount);
                dbManager.ExecuteScalar("Sfsp_RefundDeliveryCharge", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 11. To refund order line
        /// <summary>
        /// To refund order line
        /// </summary>
        public void RefundOrderLine(string OrderID, int ProductID, int PaymentTypeID, int PaymentGatewayID,
            int PaymentStatus, DateTime PaymentDate, double Amount)
        {
            try
            {
            
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@ProductID", ProductID);
                dbManager.AddParameter("@PaymentTypeID", PaymentTypeID);
                dbManager.AddParameter("@PaymentGatewayID", PaymentGatewayID);
                dbManager.AddParameter("@PaymentStatus", PaymentStatus);
                dbManager.AddParameter("@PaymentDate", PaymentDate);
                dbManager.AddParameter("@Amount", Amount);
                dbManager.ExecuteScalar("Sfsp_RefundOrderLine", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 12. To cancel product line
        /// <summary>
        /// To cancel product line
        /// </summary>
        public void CancelProductLine(string OrderID, int ProductID)
        {
            try
            {
               
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@ProductID", ProductID);
                dbManager.ExecuteScalar("Sfsp_CancelProductLine", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion       
     
        #region 14. To make a payment or refund
        /// <summary>
        /// To make a payment or refund
        /// </summary>
        public void MakePaymentOrRefund(string OrderID, int PaymentTypeID, int PaymentGatewayID,
            int PaymentStatus, DateTime PaymentDate, double Amount)
        {
            try
            {
                
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@PaymentTypeID", PaymentTypeID);
                dbManager.AddParameter("@PaymentGatewayID", PaymentGatewayID);
                dbManager.AddParameter("@PaymentStatus", PaymentStatus);
                dbManager.AddParameter("@PaymentDate", PaymentDate);
                dbManager.AddParameter("@Amount", Amount);
                dbManager.ExecuteScalar("Sfsp_MakePaymentOrRefund", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region 15. To edit card message
        /// <summary>
        /// To edit card message
        /// </summary>
        public void EditCardMessage(string OrderID, int MessageCardID, string Message)
        {
            try
            {
              
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@MessageCardID", MessageCardID);
                dbManager.AddParameter("@Message", Message);
                dbManager.ExecuteScalar("SFsp_EditCardMessage", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        #endregion        

        
        
    }
}
#endregion