/// <summary>
/// Author:Valuelabs
/// Date: 07/22/2011 11:25:14 AM
/// Class Name:OrdersData
/// Description:This class contains the logic to execute the stored procedure to manipulate the
/// data for order related information by using below methods.
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
namespace SFMobile.DAL.Orders
{
    public partial class OrdersData
    {
        DBManager dbManager = new DBManager();


        /// <summary>
        /// Default Constructor for OrdersData 
        /// </summary>
        public OrdersData() { }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertAmendOrders()
        /// StoredProcedure Name:SFsp_InsertProductDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertProductDetails"
        /// to save/update the Order’s details in to database.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public string InsertAmendOrders(OrderDTO objOrder)
        {
            int orderId = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@productID", objOrder.ProductId);
                dbManager.AddParameter("@quantity", objOrder.Quantity);
                dbManager.AddParameter("@deliveryDate", objOrder.DeliveryDate);
                dbManager.AddParameter("@deliveryOptionID", objOrder.DeliveryOptionId);
                dbManager.AddParameter("@optionalName", objOrder.OptionalName);
                dbManager.AddParameter("@optionalCost", objOrder.OptionalCost);
                dbManager.AddParameter("@countryId", objOrder.CountryCode);
                dbManager.AddParameter("@UserIPAddress", objOrder.UserIpAddress);
                dbManager.AddParameter("@SiteID", objOrder.SiteId);
                dbManager.AddParameter("@BrowserCountry", objOrder.BrowserCountry);
                dbManager.AddParameter("@idOrder", (object)DBNull.Value);
                orderId = (int)dbManager.ExecuteScalar("SFsp_InsertProductDetails", CommandType.StoredProcedure);
                return orderId.ToString();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return orderId.ToString();
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : UpdateOrderDetails()
        /// StoredProcedure Name:SFsp_InsertIntoProductBasket
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertIntoProductBasket"
        /// to save/update the Order’s details in to database.
        /// </summary>
        /// <param name="objOrder"></param>
        public void UpdateOrderDetails(OrderDTO objOrder)
        {
            int response;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@orderID", objOrder.OrderId);
                dbManager.AddParameter("@productID", objOrder.ProductId);
                dbManager.AddParameter("@quantity", 1);
                dbManager.AddParameter("@deliveryDate", objOrder.DeliveryDate);
                dbManager.AddParameter("@deliveryOptionID", objOrder.DeliveryOptionId);
                dbManager.AddParameter("@optionalName", objOrder.OptionalName);
                dbManager.AddParameter("@optionalCost", objOrder.OptionalCost);
                response = dbManager.ExecuteNonQuery("SFsp_InsertIntoProductBasket", CommandType.StoredProcedure);

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
        /// Method Name : GetOrderDetailsById()
        /// StoredProcedure Name:SFsp_GetOrderDetailsByOrderId
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetOrderDetailsByOrderId"
        /// to get the Order details  based on productid from database.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataSet GetOrderDetailsById(string orderId)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(orderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", orderId);
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetOrderDetailsByOrderId", CommandType.StoredProcedure);
                    
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
            return dsOrders;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetConfirmationPriceByOrderID()
        /// StoredProcedure Name:SFsp_GetPriceDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetPriceDetails"
        /// to get the price details  based on orderid from database.
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet GetConfirmationPriceByOrderID(string orderId)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(orderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", orderId);
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetPriceDetails", CommandType.StoredProcedure);
                   
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
            return dsOrders;

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetConfirmationDetailsByOrderID()
        /// StoredProcedure Name:SFsp_GetPriceDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetAllOrderDetails"
        /// to get the all details for a order based on orderid from database.
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet GetConfirmationDetailsByOrderID(string orderId)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(orderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", orderId);
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetAllOrderDetails", CommandType.StoredProcedure);
                    
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
            return dsOrders;
        }
        public DataSet GetFloodtagDetailsByOrderID(string encryptedorderId)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(encryptedorderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@EncryptedOrderID", encryptedorderId);                   
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetAllOrderDetails", CommandType.StoredProcedure);

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
            return dsOrders;
        }
        public DataSet GetFloodtagDetailsByOrderIDTemp(string OrderId)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(OrderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", OrderId);
                    dbManager.AddParameter("@EncryptedOrderID", null);
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetAllOrderDetails", CommandType.StoredProcedure);

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
            return dsOrders;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetTransactionDetailsById()
        /// StoredProcedure Name:SFsp_GetTransactionDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetTransactionDetails"
        /// to get the Transaction details  based on orderid from database.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataSet GetTransactionDetailsById(string orderId)
        {
            DataSet dsTransaction = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(orderId))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@OrderId", orderId);
                    dsTransaction = dbManager.ExecuteDataSet("SFsp_GetTransactionDetails", CommandType.StoredProcedure);
                    
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
            return dsTransaction;
        }
        public int GetQuantityByOrderIdProductId(string orderId,int productId)
        {
            int qty = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                dbManager.AddParameter("@ProductID", productId);
                qty = (int)dbManager.ExecuteScalar("SFsp_GetProductQuantity", CommandType.StoredProcedure);
                return qty;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return qty;
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetContactUsDetailsByGuid()
        /// StoredProcedure Name:SFsp_GetContactUsDetailsByGUID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetContactUsDetailsByGUID"
        /// to get the Email communication details  based on guid from database.
        /// </summary>
        /// <param name="strGuid">strGuid</param>
        /// <returns>DataSet</returns>
        public DataSet GetContactUsDetailsByGuid(string strGuid)
        {
            DataSet dsOrders = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(strGuid))
                {
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@GUID", strGuid);
                    dsOrders = dbManager.ExecuteDataSet("SFsp_GetContactUsDetailsByGUID", CommandType.StoredProcedure);
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
            return dsOrders;
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertContactPageInfo()
        /// StoredProcedure Name:SFsp_InsertContactInfo
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertContactInfo"
        /// to add new message details in T_ContactUs table.
        /// </summary>
        /// <param name="objTicketInfo"></param>
        /// <returns>int</returns>
        public int InsertContactPageInfo(EmailTicketInfo objTicketInfo)
        {
            int IdMessage = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@MessageFromName", objTicketInfo.MessageFromName);
                dbManager.AddParameter("@MessageFromEmail", objTicketInfo.MessageFromEmail);
                dbManager.AddParameter("@MessageTo", objTicketInfo.MessageTo);
                dbManager.AddParameter("@OrderID", objTicketInfo.OrderID);
                dbManager.AddParameter("@SubjectStr", objTicketInfo.SubjectStr);
                dbManager.AddParameter("@EncryptedMessage", objTicketInfo.EncryptedMessage);
                dbManager.AddParameter("@ReasonID", objTicketInfo.ReasonID);
                dbManager.AddParameter("@SourceID", objTicketInfo.SourceID);
                dbManager.AddParameter("@IdTicket", objTicketInfo.IdTicket);
                dbManager.AddParameter("@EncryptionType", objTicketInfo.EncryptionType);
                IdMessage = (int)dbManager.ExecuteScalar("SFsp_InsertContactInfo", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return IdMessage;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertTicketInfo()
        /// StoredProcedure Name:SFsp_InsertContactTicket
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertContactTicket"
        /// to add new message details in T_ContactUs table.
        /// </summary>
        /// <param name="objTicketInfo"></param>
        /// <returns>int</returns>
        public int InsertTicketInfo(string orderid,int statusid)
        {
            int int_TicketId = 0;
            
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderid);
                dbManager.AddParameter("@StatusID", statusid);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_InsertContactTicket", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    int_TicketId = Convert.ToInt32(reader["IdTicket"]);
                   
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
            return int_TicketId;
        }
        public int GetQuantityByOrderId(string orderId)
        {
            int qty = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                qty = (int)dbManager.ExecuteScalar("SFsp_GetOrderQuantity", CommandType.StoredProcedure);
                return qty;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return qty;
        }

        public OrderDTO CheckMultiFPProducts(OrderDTO objOrder)
        {
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", objOrder.OrderId);
                dbManager.AddParameter("@NewProductID", objOrder.ProductId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_CheckMultiFPProducts", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("MultiFPTrue")))
                    {
                        objOrder.MultiFPTrue = Convert.ToInt32(reader["MultiFPTrue"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ErrorString")))
                    {
                        objOrder.ErrorString = Convert.ToString(reader["ErrorString"]);
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Productid")))
                    {
                        objOrder.ProductId = Convert.ToInt32(reader["Productid"]);
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

        public int GetCardMessageLength(string orderId)
        {
            int len = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                len = (int)dbManager.ExecuteScalar("SFsp_GetCardMessageLength", CommandType.StoredProcedure);
                return len;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return len;
        }

     

        public DataSet GetReOrderVoucherDetails(string orderId, int voucherId)
        {
            DataSet dsVoucher = new DataSet();
            try
            {
                
                    dbManager.OpenConnection();
                    dbManager.AddParameter("@IdOrder", orderId);
                    dbManager.AddParameter("@IdVoucher ", voucherId);
                    dsVoucher = dbManager.ExecuteDataSet("SFsp_InsertReOrderVoucher", CommandType.StoredProcedure);
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsVoucher;
        }

        public DataTable GetOrderDetails(string orderId)
        {
          
            DataTable dtOrder = new DataTable();
            try
            {
                DataSet dsOrder = new DataSet();
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                dsOrder = dbManager.ExecuteDataSet("sfsp_getordertotalbyorderid", CommandType.StoredProcedure);
                if (dsOrder.Tables.Count >= 1)
                {
                    dtOrder = dsOrder.Tables[0];
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
            return dtOrder;
        }

        public string GetGUIId()
        {

            string strGUID = string.Empty;
            try
            {
                DataSet dsOrder = new DataSet();
                dbManager.OpenConnection();

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetGUID", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("New_ID")))
                    {
                        strGUID = Convert.ToString(reader["New_ID"]);
                    }
                  
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
            return strGUID;
        }

        //public List<ProductInfo> GetBasketContents(string orderId)
        //{
        //    List<ProductInfo> lstProductItems = new List<ProductInfo>();
        //    try
        //    {
        //        dbManager.OpenConnection();
        //        dbManager.AddParameter("@OrderID", orderId);

        //        SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetBasketContents", CommandType.StoredProcedure);

        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                ProductInfo objProductInfo = new ProductInfo();

                       

        //                if (!reader.IsDBNull(reader.GetOrdinal("price")))
        //                {
        //                    objProductInfo.TotalPrice = Convert.ToDecimal(reader["price"]);
        //                }
                     
        //                if (!reader.IsDBNull(reader.GetOrdinal("Quantity")))
        //                {
        //                    objProductInfo.Quantity = Convert.ToInt16(reader["Quantity"]);
        //                }
        //                lstProductItems.Add(objProductInfo);
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }
        //    finally
        //    {
        //        dbManager.ClearParameters();
        //        dbManager.CloseConnection();
        //    }
        //    return lstProductItems;
        //}
    }
}
