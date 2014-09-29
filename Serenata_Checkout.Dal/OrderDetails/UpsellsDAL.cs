using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;

namespace Serenata_Checkout.Dal
{
   public class UpsellsDAL
    {

        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

       /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetUpsells()
        /// StoredProcedure Name: SFsp_GetProductsForUpsellPage
        /// Description: To get Upsells
       /// </summary>
       /// <param name="orderId"></param>
       /// <param name="productId"></param>
       /// <param name="itemId1"></param>
       /// <param name="itemId2"></param>
       /// <param name="itemId3"></param>
       /// <returns></returns>
        public List<ProductInfo> GetUpsells(int partnerId,int productId, int itemId1, int itemId2, int itemId3)
        {
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@CarrierID", partnerId);
                dbManager.AddParameter("@ProductID", productId);
                dbManager.AddParameter("@ItemID1", itemId1);
                dbManager.AddParameter("@ItemID2", itemId2);
                dbManager.AddParameter("@ItemID3", itemId3);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductsForUpsellPage", CommandType.StoredProcedure);

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
                        if (!reader.IsDBNull(reader.GetOrdinal("img1_small_low")))
                        {
                            objProductInfo.img1_small_low = Convert.ToString(reader["img1_small_low"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("offer")))
                        {
                            objProductInfo.offer = Convert.ToDouble(reader["offer"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("offerstartdate")))
                        {
                            objProductInfo.offerstartdate = Convert.ToDateTime(reader["offerstartdate"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("offerenddate")))
                        {
                            objProductInfo.offerenddate = Convert.ToDateTime(reader["offerenddate"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Img1_big_high")))
                        {
                            objProductInfo.img1_big_high = Convert.ToString(reader["Img1_big_high"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("info2")))
                        {
                            objProductInfo.info2 = Convert.ToString(reader["info2"]);
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
        /// Author: Valuelabs
        /// Method Name: AddUpsellsProduct()
        /// StoredProcedure Name: SFsp_AddProductLine
        /// Description: To add Upsells
       /// </summary>
       /// <param name="objOrderLines"></param>
        public void AddUpsellsProduct(OrderLinesInfo objOrderLines)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objOrderLines.OrderID);
                dbManager.AddParameter("@ProductID", objOrderLines.ProductID);
                dbManager.AddParameter("@ProdVATRate", objOrderLines.VATRate);
                dbManager.AddParameter("@Price", objOrderLines.Price);
                dbManager.AddParameter("@PartnerID", objOrderLines.PartnerID);
                dbManager.AddParameter("@Description", objOrderLines.Description);
                dbManager.ExecuteScalar("SFsp_AddProductLine", CommandType.StoredProcedure);
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

    

        #region  Get upsale count by using OrderID.
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
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                count = (int)dbManager.ExecuteScalar("SFsp_GetUpsaleCountInBasketNew", CommandType.StoredProcedure);
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
            return count;
        }

        #endregion

        #region  Get delpartnerid by using OrderID.

        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetDeliveryPartnerid()
        /// StoredProcedure Name: SFsp_CheckDefaultDeliveryProductInBasket
        /// Description: This method contains logic to execute stored procedure "SFsp_CheckDefaultDeliveryProductInBasket" to get DeliveryPartnerid

        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetDeliveryPartnerid(string orderId)
        {
            int count = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                count = (int)dbManager.ExecuteScalar("SFsp_CheckDefaultDeliveryProductInBasket", CommandType.StoredProcedure);
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
            return count;
        }
        #endregion


       /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetPIONSByProductID()
        /// StoredProcedure Name: SFsp_GetDataForPionTags
        /// Description: This method contains logic to execute stored procedure "SFsp_GetDataForPionTags" to get product PION Values
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
        public ProductInfo GetSpotLightUpsell(string strOrderId)
        {
            ProductInfo objProductInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderId", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetSpotlightUpsellProduct", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objProductInfo = new ProductInfo();

                        if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
                        {
                            objProductInfo.producttitle = Convert.ToString(reader["ProductTitle"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Info2")))
                        {
                            objProductInfo.info2 = Convert.ToString(reader["Info2"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                        {
                            objProductInfo.price= Convert.ToDouble(reader["Price"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            objProductInfo.productid = Convert.ToInt32(reader["ProductId"]);
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
            return objProductInfo;
        }

        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetPIONSByProductID()
        /// StoredProcedure Name: SFsp_GetDataForPionTags
        /// Description: This method contains logic to execute stored procedure "SFsp_GetDataForPionTags" to get product PION Values
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductPION GetPIONSByProductID(int productId)
        {
            ProductPION objProductPION = new ProductPION();
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@ProductID", productId);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDataForPionTags", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            objProductPION.productid = Convert.ToInt32(reader["ProductId"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                        {
                            objProductPION.price = Convert.ToDouble(reader["Price"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("Discount")))
                        {
                            objProductPION.discount = Convert.ToDouble(reader["Discount"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Rating")))
                        {
                            objProductPION.raiting = Convert.ToDouble(reader["Rating"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("NumberOfReviews")))
                        {
                            objProductPION.noOfreviews = Convert.ToInt32(reader["NumberOfReviews"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("HasVideo")))
                        {
                            objProductPION.hasVideo = Convert.ToString(reader["HasVideo"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("HasDiscountRibbon")))
                        {
                            objProductPION.hasDiscountRibbon = Convert.ToString(reader["HasDiscountRibbon"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("SortOrder")))
                        {
                            objProductPION.SortOrder = Convert.ToString(reader["SortOrder"]);
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
            return objProductPION;
        }
    }
}
