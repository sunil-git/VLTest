/// <summary>
/// Author:Valuelabs
/// Date: 06/05/2011 11:25:14 PM
/// Class Name:ProductsData
/// Description:This class contains the logic to execute the stored procedure to manipulate the
/// data for products related information by using below methods.
/// This class associates with DBManager class to use execute 
/// stored procedure using ADO.Net technology.
/// <summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SFMobile.DTO;
using SFMobile.Exceptions;
namespace SFMobile.DAL.Products
{
    public partial class ProductsData
    {
        /// <summary>
        /// Default constructor for ProductDAL
        /// <summary>
        public ProductsData()
        { }
        DBManager dbManager = new DBManager();

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductDetailsById()
        /// StoredProcedure Name:SFsp_InsertProductDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetProductDetailsByID"
        /// to get the products details based on productid from database.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>ProductDTO</returns>
        public ProductDTO GetProductDetailsById(int productId)
        {
            ProductDTO objProduct = new ProductDTO();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductId", productId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductDetailsByID", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                    {
                        objProduct.ProductId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
                    {
                        objProduct.ProductName = reader.GetString(reader.GetOrdinal("ProductTitle"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Img1_big_high")))
                    {
                        objProduct.ImagePath = reader.GetString(reader.GetOrdinal("Img1_big_high"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                    {
                        objProduct.ProductOldPrice = reader.GetDecimal(reader.GetOrdinal("Price"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Offer")))
                    {
                        objProduct.ProductOfferPrice = reader.GetDecimal(reader.GetOrdinal("Offer"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Offer")))
                    {
                        objProduct.TotalPrice = reader.GetDecimal(reader.GetOrdinal("Offer"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("info1")))
                    {
                        objProduct.ProductInfo1 = reader.GetString(reader.GetOrdinal("info1"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("Descripton")))
                    {
                        objProduct.ProductDesc = reader.GetString(reader.GetOrdinal("Descripton"));
                    }

                }
                
                return objProduct;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objProduct;
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductsByCountryCagetory()
        /// StoredProcedure Name:SFsp_InsertProductDetails
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetProductDetailsByID"
        /// to get the products details based on countryId and categoryId from database.
       /// </summary>
       /// <param name="CountryId"></param>
       /// <param name="CategoryId"></param>
        /// <returns>List</returns>
        public List<ProductDTO> GetProductsByCountryCagetory(int countryId, int categoryId)
        {
            List<ProductDTO> objPoductList = new List<ProductDTO>();
            ProductDTO objProduct = new ProductDTO();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CountryID", countryId);
                dbManager.AddParameter("@CategoryId", categoryId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductListByCountryCategory_Paging", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    objPoductList.Add(FillProductsRecord(reader));
                }
               
                return objPoductList;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objPoductList;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductsByCountryCagetory()
        /// StoredProcedure Name:SFsp_GetProductListByCountryCategory_Paging
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetProductListByCountryCategory_Paging"
        /// to get the products details based on countryId and categoryId  for paging .
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <param name="StartRow"></param>
        /// <param name="EndRow"></param>
        /// <returns>List</returns>
        public List<ProductDTO> GetProductsByCountryCagetory(int countryId, int categoryId, int startRow, int endRow)
        {

            List<ProductDTO> objPoductList = new List<ProductDTO>();
            ProductDTO objProduct = new ProductDTO();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@CountryID", countryId);
                dbManager.AddParameter("@CategoryId", categoryId);
                dbManager.AddParameter("@StartRow", startRow);
                dbManager.AddParameter("@EndRow", endRow);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductListByCountryCategory_Paging", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    objPoductList.Add(FillProductsRecord(reader));
                }
               
                return objPoductList;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objPoductList;
        }      
     
       /// <summary>
        ///  Author :Valuelabs
        /// Method Name : FillProductsRecord()
        /// StoredProcedure Name:NA
        /// Description: This method Contains logic fill the ProductDTO from datareader        
       /// </summary>
       /// <param name="myDataRecord"></param>
        /// <returns>ProductDTO</returns>
        private ProductDTO FillProductsRecord(IDataRecord myDataRecord)
        {
            ProductDTO objProducts = new ProductDTO();
            try
            {

                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ProductId")))
                {
                    objProducts.ProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProductId"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ProductTitle")))
                {
                    objProducts.ProductName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProductTitle"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Img1_small_high")))
                {
                    objProducts.ImagePath = myDataRecord.GetString(myDataRecord.GetOrdinal("Img1_small_high"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Price")))
                {
                    objProducts.ProductOldPrice = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Price"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("offer")))
                {
                    objProducts.ProductOfferPrice = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("offer"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TotalRecord")))
                {
                    objProducts.TotalRecords = myDataRecord.GetInt32(myDataRecord.GetOrdinal("TotalRecord"));
                }
                return objProducts;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objProducts;

        }
       /// <summary>
        ///  Author :Valuelabs
        /// Method Name : FillSplProduct()
        /// StoredProcedure Name:NA
        /// Description: This method Contains logic fill the ProductDTO from datareader 
       /// </summary>
       /// <param name="myReader"></param>
       /// <returns></returns>
        private ProductDTO FillSplProduct(IDataReader myReader)
        {
            ProductDTO objProducts = new ProductDTO();
            try
            {
                if (!myReader.IsDBNull(myReader.GetOrdinal("ProductTitle")))
                {
                    objProducts.ProductName = myReader.GetString(myReader.GetOrdinal("ProductTitle"));
                }
                if (!myReader.IsDBNull(myReader.GetOrdinal("Img1_big_high_checkout")))
                {
                    objProducts.CheckOutImgagePath = myReader.GetString(myReader.GetOrdinal("Img1_big_high_checkout"));
                }
                if (!myReader.IsDBNull(myReader.GetOrdinal("Price")))
                {
                    objProducts.CheckProductPrice = myReader.GetDecimal(myReader.GetOrdinal("Price"));
                }
                return objProducts;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objProducts;

        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetDeliveryDatesByProductId()
        /// StoredProcedure Name:SFSP_GetDeliveryDate
        /// Description: This method Contains logic to execute the stored procedure "SFSP_GetDeliveryDate"
        /// to get the deliverydates based on productid  from database.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDeliveryDatesByProductId(int productId)
        {
            DataSet ds = new DataSet();

            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductId", productId);
                ds = dbManager.ExecuteDataSet("SFSP_GetDeliveryDate", CommandType.StoredProcedure);               
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetDeliveryOptionsByDeliveryDate()
        /// StoredProcedure Name:SFsp_GetDeliveryOptions
        /// Description: This method Contains logic to execute the stored procedure "SFsp_GetDeliveryOptions"
        /// to get delivery options based on productid and day  from database.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DataTable GetDeliveryOptionsByDeliveryDate(int productId, string day ,string selectedDate)
        {
            DataSet ds = new DataSet();
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductId", productId);
                dbManager.AddParameter("@Day", day);
                dbManager.AddParameter("@SelectedDeliveryDate", selectedDate);
                ds = dbManager.ExecuteDataSet("SFsp_GetDeliveryOptions", CommandType.StoredProcedure);
               
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductSetByCountryCategory()
        /// StoredProcedure Name:SFsp_GetProductSetForXML
        /// Description: This method Contains logic to execute the stored procedure "SFsp_GetProductSetForXML"
        /// to get product set Id based on countryId and categoryId  from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public int GetProductSetByCountryCategory(int countryId, int categoryId)
        {
            int productSetId = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@CountryId", countryId);
                dbManager.AddParameter("@CategoryId", categoryId);
                productSetId = Convert.ToInt16(dbManager.ExecuteScalar("SFsp_GetProductSetForXML", CommandType.StoredProcedure));
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }


            return productSetId;

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductSetByCategoryId()
        /// StoredProcedure Name:SFsp_CheckForCachedProductSetByCategoryID
        /// Description: This method Contains logic to execute the stored procedure "SFsp_CheckForCachedProductSetByCategoryID"
        /// to get product set Id based on countryId and categoryId  from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public int GetProductSetByCategoryId(int categoryId)
        {
            int productSetId = 0;

            try
            {

                dbManager.OpenConnection();                
                dbManager.AddParameter("@CategoryId", categoryId);
                productSetId = Convert.ToInt16(dbManager.ExecuteScalar("SFsp_CheckForCachedProductSetByCategoryID", CommandType.StoredProcedure));

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }


            return productSetId;

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductsByCagetoryId()
        /// StoredProcedure Name:SFsp_GetProductListByCountryCategory_Paging
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetProductListByCountryCategory_Paging"
        /// to get the products details based on countryId and categoryId from database.
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <returns>List</returns>
        public List<ProductDTO> GetProductsByCagetoryId(int categoryId)
        {
            List<ProductDTO> objPoductList = new List<ProductDTO>();
            ProductDTO objProduct = new ProductDTO();
            try
            {
                dbManager.OpenConnection();                
                dbManager.AddParameter("@CategoryId", categoryId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductListByCategory_Paging", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    objPoductList.Add(FillProductsRecord(reader));
                }

                return objPoductList;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objPoductList;
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetProductsByCagetory()
        /// StoredProcedure Name:SFsp_GetProductListByCountryCategory_Paging
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetProductListByCountryCategory_Paging"
        /// to get the products details based on countryId and categoryId  for paging .
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <param name="StartRow"></param>
        /// <param name="EndRow"></param>
        /// <returns>List</returns>
        public List<ProductDTO> GetProductsByCagetoryId(int categoryId, int startRow, int endRow)
        {

            List<ProductDTO> objPoductList = new List<ProductDTO>();
            ProductDTO objProduct = new ProductDTO();
            try
            {
                dbManager.OpenConnection();                
                dbManager.AddParameter("@CategoryId", categoryId);
                dbManager.AddParameter("@StartRow", startRow);
                dbManager.AddParameter("@EndRow", endRow);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetProductListByCategory_Paging", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    objPoductList.Add(FillProductsRecord(reader));
                }

                return objPoductList;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objPoductList;
        }
        /// <summary>
        /// Get ReviewData  based on Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetReviewDataByProductId(int productId)
        {

            DataSet ds = new DataSet();

            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductId", productId);
                ds = dbManager.ExecuteDataSet("SFsp_GetProductReviewData", CommandType.StoredProcedure);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// Get GetUpgradeProducts  based on Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetUpgradeProductsByProductId(int productId)
        {

            DataSet ds = new DataSet();

            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductId", productId);
                ds = dbManager.ExecuteDataSet("SFsp_GetUpgradeProductDetails", CommandType.StoredProcedure);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return ds.Tables[0];
        }
        public DataTable GetPartnerIdDelPartnerIdbyOptionId(int optionId)
        {

            DataSet ds = new DataSet();

            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@DelOptionID", optionId);
                ds = dbManager.ExecuteDataSet("SFsp_GetPartnerIDAndDelPartnerIDByOptionID", CommandType.StoredProcedure);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return ds.Tables[0];
        }
        public int GetProductIdByOrderId(string orderId)
        {
            int prodid = 0;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", orderId);
                prodid = (int)dbManager.ExecuteScalar("SFsp_GetRecentlyAddedMainProduct", CommandType.StoredProcedure);
                return prodid;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return prodid;
        }

        /// <summary>
        /// Added GetproductSETXML from DB
        /// </summary>
        /// <param name="productSet"></param>
        /// <returns></returns>
        public string GetProductSetXMLByProductSet(int  productSet)
        {
            string strXMl = string.Empty;

            try
            {

                dbManager.OpenConnection();
                dbManager.AddParameter("@ProductSetID", productSet);
                strXMl = (string)dbManager.ExecuteScalar("SFsp_GetProductSetXMLByProductSetID", CommandType.StoredProcedure);
               
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return strXMl;
        }
    }
}
