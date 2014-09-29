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
    public class OrderDetailsDAL
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

        #region 1.To create an order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CreateOrder
        /// Description: This method contains logic to execute stored procedure "SFsp_CreateOrder" to save/update the order details into [Orders] table.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public string CreateOrder(OrderInfo objOrder)
        {
            string strOrderId = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objOrder.OrderID);
                dbManager.AddParameter("@Prefix", objOrder.Prefix);
                dbManager.AddParameter("@IPAddress", objOrder.IPAddress);
                dbManager.AddParameter("@CustomerID", objOrder.CustomerID);
                dbManager.AddParameter("@CookieID", objOrder.CookiesID);
                dbManager.AddParameter("@SessionID", objOrder.SessionID);
                dbManager.AddParameter("@OrderStatusID", objOrder.OrderStatusID);
                dbManager.AddParameter("@SiteID", objOrder.SiteID);
                dbManager.AddParameter("@ChannelID", objOrder.ChannelID);
                dbManager.AddParameter("@CurrencyID", objOrder.CurrencyID);
                dbManager.AddParameter("@BrowserIP", objOrder.BrowserIP);
                dbManager.AddParameter("@BrowserCountry", objOrder.BrowserCountry);
                dbManager.AddParameter("@CountryID", objOrder.CountryID);
                dbManager.AddParameter("@BrowserID", objOrder.BrowserID);
                dbManager.AddParameter("@DeviceID", objOrder.DeviceID);
                dbManager.AddParameter("@AltVisitorID", objOrder.AltVisitorID);
                strOrderId = (string)dbManager.ExecuteScalar("SFsp_CreateOrder_NewCheckout", CommandType.StoredProcedure);
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
            return strOrderId;
        }
        #endregion
        #region 2.To StoreAffiliateTracking.
        /// <summary>
        /// StoreAffiliateTracking
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="awaid"></param>
        /// <param name="awgid"></param>
        /// <param name="awbid"></param>
        /// <param name="awpid"></param>
        /// <param name="awcr"></param>
        /// <param name="awcd"></param>
        public void StoreAffiliateTracking(string OrderID, string awaid, string awgid, string awbid, string awpid, string awcr, string awcd)
        {

            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@awaid", awaid);
                dbManager.AddParameter("@awgid", awgid);
                dbManager.AddParameter("@awbid", awbid);
                dbManager.AddParameter("@awpid", awpid);
                dbManager.AddParameter("@awcr", awcr);
                dbManager.AddParameter("@awcd", awcd);
                dbManager.ExecuteScalar("SFsp_StoreAffiliateTracking", CommandType.StoredProcedure);
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
        #region 3.To schedule the dispatch.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ScheduleDispatch()
        /// StoredProcedure Name: Sfsp_RescheduleDispatch
        /// Description: This method contains logic to execute stored procedure "Sfsp_RescheduleDispatch" to save/update the dispatch details into [Dispatches] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void ScheduleDispatch(string OrderID,DispatchesInfo objDispatchInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@DeliveryDate", objDispatchInfo.DeliveryDate);
                dbManager.AddParameter("@DelOptionID", objDispatchInfo.DelOptionID);
                dbManager.AddParameter("@CarrierID", objDispatchInfo.CarrierID);
                dbManager.AddParameter("@DeliveryTime", objDispatchInfo.DeliveryTime);
                dbManager.AddParameter("@DeliveryPrice", objDispatchInfo.DeliveryPrice);
                dbManager.ExecuteScalar("Sfsp_RescheduleDispatch", CommandType.StoredProcedure);
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

        #region 4. To update the encrypted order id
        /// <summary>
        /// Author:Valuelabs
        /// Method Name: UpdateEncryptedOrderId()
        /// StoredProcedure Name: SFsp_UpdateOrderWithEncryptedOrderID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_UpdateOrderWithEncryptedOrderID" to update the encrypted order id into [Orders].
        /// </summary>
        /// <param name="objCustomerInfo">The obj order info.</param>
        /// <remarks></remarks>
        public void UpdateEncryptedOrderId(OrderInfo objOrderInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objOrderInfo.OrderID);
                dbManager.AddParameter("@EncryptedOrderID", objOrderInfo.EncryptedOrderID);
                dbManager.ExecuteScalar("SFsp_UpdateOrderWithEncryptedOrderID", CommandType.StoredProcedure);
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

        public int AddProductToBasket(DispatchesInfo objDispatchesInfo,OrderLinesInfo objOrderLineInfo, bool addupsell)
        {
            int response=0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objDispatchesInfo.OrderID);
                dbManager.AddParameter("@ProductID", objOrderLineInfo.ProductID);
                dbManager.AddParameter("@Quantity", objOrderLineInfo.Quantity);         
                dbManager.AddParameter("@DelOptionID", objDispatchesInfo.DelOptionID);
                dbManager.AddParameter("@DeliveryDate", objDispatchesInfo.DeliveryDate);        
                dbManager.AddParameter("@DeliveryPrice", objDispatchesInfo.DeliveryPrice);
                dbManager.AddParameter("@UpsellAdded", addupsell);
                response = (int)dbManager.ExecuteScalar("SFsp_AddProductToBasketNew", CommandType.StoredProcedure);
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
            return response;
        }
        /// <summary>
        /// Author:Valuelabs
        /// Method Name: GetBasketContents()
        /// StoredProcedure Name: SFsp_GetBasketContents
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetBasketContents" to get basketdetails by orderid.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<ProductInfo> GetBasketContents(string orderId)
        {
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetBasketContents", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        ProductInfo objProductInfo = new ProductInfo();

                        if (!reader.IsDBNull(reader.GetOrdinal("producttitle")))
                        {
                            objProductInfo.producttitle = Convert.ToString(reader["producttitle"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("productid")))
                        {
                            objProductInfo.productid = Convert.ToInt32(reader["productid"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("price")))
                        {
                            objProductInfo.price = Convert.ToDouble(reader["price"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Img1_small_low_checkout")))
                        {
                            objProductInfo.img1_small_low = Convert.ToString(reader["Img1_small_low_checkout"]);
                        }
                        
                        if (!reader.IsDBNull(reader.GetOrdinal("Img1_small_high")))
                        {
                            objProductInfo.img1_big_high = Convert.ToString(reader["Img1_small_high"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("info2")))
                        {
                            objProductInfo.info2 = Convert.ToString(reader["info2"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Quantity")))
                        {
                            objProductInfo.quantity = Convert.ToInt16(reader["Quantity"]);
                        }
                        lstProductItems.Add(objProductInfo);
                    }
                }


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
            return lstProductItems;
        }

        /// <summary>
        /// Author:Valuelabs
        /// Method Name: GetDeliveryDetails()
        /// StoredProcedure Name: SFsp_GetOrderAndDiscountAmt
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetOrderAndDiscountAmt" to get deliverydetails by orderID.
      
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DeliveryTimeInfo GetDeliveryDetails(string orderId)
        {
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                //SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderAndDiscountAmt", CommandType.StoredProcedure);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderAndDiscountAmtWithOldVouchers", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                       

                        if (!reader.IsDBNull(reader.GetOrdinal("OrderTotal")))
                        {
                            objDeliveryTimeInfo.OrderTotal = Convert.ToDouble(reader["OrderTotal"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("VoucherTitle")))
                        {
                            objDeliveryTimeInfo.voucherTitle = Convert.ToString(reader["VoucherTitle"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Discount")))
                        {
                            objDeliveryTimeInfo.discount = Convert.ToDouble(reader["Discount"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Deliverydate")))
                        {
                            objDeliveryTimeInfo.Deliverydate = Convert.ToString(reader["Deliverydate"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryPrice")))
                        {
                            objDeliveryTimeInfo.deliveryPrice = Convert.ToDouble(reader["DeliveryPrice"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryTime")))
                        {
                            objDeliveryTimeInfo.OptionName = Convert.ToString(reader["DeliveryTime"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DateValue")))
                        {
                            objDeliveryTimeInfo.DateValue = Convert.ToString(reader["DateValue"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DelOptionID")))
                        {
                            objDeliveryTimeInfo.id = Convert.ToInt32(reader["DelOptionID"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("SubTotal")))
                        {
                            objDeliveryTimeInfo.SubTotal = Convert.ToDouble(reader["SubTotal"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("TotalExVat")))
                        {
                            objDeliveryTimeInfo.TotalExVat = Convert.ToDouble(reader["TotalExVat"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("SiteID")))
                        {
                            objDeliveryTimeInfo.SiteID = Convert.ToInt32(reader["SiteID"]);
                        }
                    }
                }


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
            return objDeliveryTimeInfo;
        }

        #region Delete selected item from the basket.
        public void DeleteBasketItem(string strOrderId, int intProductId)
        {
            int response;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@ProductID", intProductId);
                response = dbManager.ExecuteNonQuery("SFsp_DeleteProductFromBasket", CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }
        #endregion

        #region Delete selected item from the basket.
        public void UpdateBasketItem(string strOrderId, int intProductId, int inQty)
        {
            int response;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@ProductID", intProductId);
                dbManager.AddParameter("@Quantity", inQty);
                
                response = dbManager.ExecuteNonQuery("SFsp_UpdateProductQuantity", CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }
        #endregion

        public DataTable GetMainProducts(string orderId)
        {
            DataTable dtProducts = new DataTable();
            DataSet dsproducts = new DataSet();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                dsproducts = dbManager.ExecuteDataSet("SFsp_GetMainProductsInBasket", CommandType.StoredProcedure);

                if (dsproducts != null)
                {
                    if (dsproducts.Tables.Count > 0)
                    {
                        dtProducts = dsproducts.Tables[0];
                    }
                }

            
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
            return dtProducts;
        }

        public int GetTotalQty(string orderId)
        {
            int Qty = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetMainProductQuantityInBasket", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Qty  = Convert.ToInt16(reader["Qty"]);
                    }
                }

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
            return Qty;
        }
        public DataTable GetOrderDetails(string orderId)
        {

            DataTable dtOrder = new DataTable();
            try
            {
                DataSet dsOrder = new DataSet();
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                dsOrder = dbManager.ExecuteDataSet("SFsp_GetOrderTotalByOrderID", CommandType.StoredProcedure);
                if (dsOrder.Tables.Count >= 1)
                {
                    dtOrder = dsOrder.Tables[0];
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dtOrder;
        }
        public int GetTotalProductQty(string orderId)
        {
            int Qty = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFSP_GetTotalProdQuantityInBasket", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Qty = Convert.ToInt16(reader["Quantity"]);
                    }
                }

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
            return Qty;
        }

        public double GetOrderTotalByOrderID(string orderId)
        {
            double dblTotal = 0.0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderTotalByOrderID", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    dblTotal = Convert.ToDouble(reader["Total"]);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return dblTotal;
        }
        public OrderTracking GetOrderTrackingInfo(string orderId)
        {
            OrderTracking objOrderTracking = new OrderTracking();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);

                //SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderAndDiscountAmt", CommandType.StoredProcedure);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderTrackingInfo", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {


                        if (!reader.IsDBNull(reader.GetOrdinal("OrderStatusID")))
                        {
                            objOrderTracking.OrderStatusID = Convert.ToInt32(reader["OrderStatusID"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("description")))
                        {
                            objOrderTracking.Description = Convert.ToString(reader["description"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("orderstatusname")))
                        {
                            objOrderTracking.OrderStatusName = Convert.ToString(reader["orderstatusname"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("deliverydate")))
                        {
                            objOrderTracking.DeliveryDate = Convert.ToString(reader["deliverydate"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("consignmentNumber")))
                        {
                            objOrderTracking.ConsignmentNumber = Convert.ToString(reader["consignmentNumber"]);
                        }
                        
                    }
                }


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
            return objOrderTracking;
        }

        public string CheckDeliveryCutoffByOrderID(string orderId)
        {
            string strMsg = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_CheckDeliveryCutoffByOrderID", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    strMsg = Convert.ToString(reader["Message"]);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strMsg;
        }
    }
}
