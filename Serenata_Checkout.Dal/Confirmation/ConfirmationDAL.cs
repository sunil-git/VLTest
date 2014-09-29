using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
namespace Serenata_Checkout.Dal.Confirmation
{
    public class ConfirmationDAL
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

        public void UpdateOrderComplete(string strOrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@ByWhom", "Customer");
                dbManager.ExecuteScalar("SFsp_UpdateOrderComplete", CommandType.StoredProcedure);
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

        public void UpdateProductPaymentStatus(string strOrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.ExecuteScalar("SFsp_UpdateProductPaymentStatus", CommandType.StoredProcedure);
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

        public void UpdateOrderStatusForPaypal(string strOrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@ByWhom", "Customer");
                dbManager.ExecuteScalar("SFsp_UpdateOrderStatusForPaypal", CommandType.StoredProcedure);
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


        public ConfirmDetailsInfo GetConfirmationPageDetails(string strOrderId)
        {
            ConfirmDetailsInfo objConfirm = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetConfirmationPageDetails", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objConfirm = new ConfirmDetailsInfo();
                        if (!reader.IsDBNull(reader.GetOrdinal("Message")))
                        {
                            objConfirm.Message = Convert.ToString(reader["Message"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Payment")))
                        {
                            objConfirm.Payment = Convert.ToString(reader["Payment"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("OrderDate")))
                        {
                            objConfirm.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Deliverydate")))
                        {
                            objConfirm.Deliverydate = Convert.ToDateTime(reader["Deliverydate"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryInstructions")))
                        {
                            objConfirm.DeliveryInstructions = Convert.ToString(reader["DeliveryInstructions"]);
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
                dbManager.CloseConnection();
            }
            return objConfirm;
        }

        public DiscountInfo InsertReOrderVoucher(string strOrderId, string strVoucherId)
        {
            DiscountInfo objDisInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@IdOrder", strOrderId);
                dbManager.AddParameter("@IdVoucher", strVoucherId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_InsertReOrderVoucher", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objDisInfo = new DiscountInfo();
                        if (!reader.IsDBNull(reader.GetOrdinal("DiscountCode")))
                        {
                            objDisInfo.DiscountCode = Convert.ToString(reader["DiscountCode"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DiscountValue")))
                        {
                            objDisInfo.DiscountValue = Convert.ToInt32(reader["DiscountValue"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DiscountExpiryDate")))
                        {
                            objDisInfo.DiscountExpiryDate = Convert.ToDateTime(reader["DiscountExpiryDate"]);
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
                dbManager.CloseConnection();
            }
            return objDisInfo;
        }

        public ProductInfo GetProductDetails(int iProductId)
        {
            ProductInfo objProInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@ProductId", iProductId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductDetailsByID", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objProInfo = new ProductInfo();
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            objProInfo.productid = Convert.ToInt32(reader["ProductId"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
                        {
                            objProInfo.producttitle = Convert.ToString(reader["ProductTitle"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Descripton")))
                        {
                            objProInfo.info2 = Convert.ToString(reader["Descripton"]);
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
                dbManager.CloseConnection();
            }
            return objProInfo;
        }

        public OrderTotalInfo GetOrderAndPaymentTotals(string strOrderId)
        {
            OrderTotalInfo objOrdTot = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetOrderAndPaymentTotals", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objOrdTot = new OrderTotalInfo();
                        if (!reader.IsDBNull(reader.GetOrdinal("OrderTotal")))
                        {
                            objOrdTot.OrderTotal = Convert.ToDouble(reader["OrderTotal"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("PaymentTotal")))
                        {
                            objOrdTot.PaymentTotal = Convert.ToDouble(reader["PaymentTotal"]);
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
                dbManager.CloseConnection();
            }
            return objOrdTot;
        }

        public List<OccassionCard> GetExtraProducts(string orderID)
        {
            List<OccassionCard> lstExtras = new List<OccassionCard>();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderID);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetUpsellProductsForConfirmationPage", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        OccassionCard obj = new OccassionCard();
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
                        {
                            obj.ProductTitle = Convert.ToString(reader["ProductTitle"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            obj.ProductId = Convert.ToInt32(reader["ProductId"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                        {
                            obj.Price = Convert.ToDecimal(reader["Price"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Img1_big_high_checkout")))
                        {
                            obj.Img1BigHigh = Convert.ToString(reader["Img1_big_high_checkout"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Img1_small_low_checkout")))
                        {
                            obj.Img1SmallHigh = Convert.ToString(reader["Img1_small_low_checkout"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Info2")))
                        {
                            obj.Info2 = Convert.ToString(reader["Info2"]);
                        }

                        //For setting NoCard Initial Value
                        // obj.NoCard = 1;

                        lstExtras.Add(obj);
                    }

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
            return lstExtras;
        }

        public ConfirmDetailsInfo GetDataForMetaTagInConfirmation(string strOrderId)
        {
            ConfirmDetailsInfo objMetaInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDataForMetaTagInConfirmation", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objMetaInfo = new ConfirmDetailsInfo();
                        if (!reader.IsDBNull(reader.GetOrdinal("PaymentType")))
                        {
                            objMetaInfo.Payment = Convert.ToString(reader["PaymentType"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("CustomerID")))
                        {
                            objMetaInfo.CustomerId = Convert.ToString(reader["CustomerID"]);
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
                dbManager.CloseConnection();
            }
            return objMetaInfo;
        }
    }
}
