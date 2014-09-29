/// <summary>
/// Author:Valuelabs
/// Date: 06/05/2011 11:25:14 PM
/// Class Name:ProductsLogic
/// Description:This class contains the bisiness logic to manipulate the
/// data for products related information by using below methods.
/// <summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SFMobile.DAL;
using SFMobile.DTO;
using System.Data;

namespace SFMobile.BAL.Products
{
    public partial class ProductsLogic
    {
        SFMobile.DAL.Products.ProductsData objProducts = new SFMobile.DAL.Products.ProductsData();

        /// <summary>
        ///GetProductsByCountryId 
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public List<ProductDTO> GetProductsByCountryCagetory(int countryId, int caregoryId)
        {
            return objProducts.GetProductsByCountryCagetory(countryId, caregoryId);
        }
        /// <summary>
        /// Get Products by paging
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CaregoryId"></param>
        /// <param name="StartRow"></param>
        /// <param name="EndRow"></param>
        /// <returns></returns>
        public List<ProductDTO> GetProductsByCountryCagetory(int countryId, int categoryId, int startRow, int endRow)
        {
            return objProducts.GetProductsByCountryCagetory(countryId, categoryId, startRow, endRow);
        }
        /// <summary>
        /// Get the Products Details by - GetProductDetailsById method
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public ProductDTO GetProductDetailById(int productId)
        {
            return objProducts.GetProductDetailsById(productId);
        }
        /// <summary>
        /// Get delivery Dates based on Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetDeliveryDatesByProductId(int productId)
        {
            return objProducts.GetDeliveryDatesByProductId(productId);
        }
        /// <summary>
        /// Delivery Options based on ProductId and Day of week
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DataTable GetDeliveryOptionsByDeliveryDate(int productId, string day, string selectedDate)
        {
            return objProducts.GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);
        }
        /// <summary>
        /// Get the Product set XML id from database
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public int GetProductSetByCountryCategory(int countryId, int categoryId)
        {
            return objProducts.GetProductSetByCountryCategory(countryId, categoryId);
        
        }
        /// <summary>
        /// Get the Product set XML id from database
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public int GetProductSetByCategoryId(int categoryId)
        {
            return objProducts.GetProductSetByCategoryId( categoryId);

        }
        /// <summary>
        ///GetProductsByCagetoryId 
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public List<ProductDTO> GetProductsByCagetoryId(int caregoryId)
        {
            return objProducts.GetProductsByCagetoryId(caregoryId);
        }
        /// <summary>
        /// Get Products by paging
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="CaregoryId"></param>
        /// <param name="StartRow"></param>
        /// <param name="EndRow"></param>
        /// <returns></returns>
        public List<ProductDTO> GetProductsByCagetoryId( int categoryId, int startRow, int endRow)
        {
            return objProducts.GetProductsByCagetoryId(categoryId, startRow, endRow);
        }
        /// <summary>
        /// Get ReviewData  based on Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetReviewDataByProductId(int productId)
        {
            return objProducts.GetReviewDataByProductId(productId);
        }
        /// <summary>
        /// Get GetUpgradeProducts  based on Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetUpgradeProductsByProductId(int productId)
        {
            return objProducts.GetUpgradeProductsByProductId(productId);
        }
        public DataTable GetPartnerIdDelPartnerIdbyOptionId(int optionId)
        {
            return objProducts.GetPartnerIdDelPartnerIdbyOptionId(optionId);
        }
        public int GetProductIdByOrderId(string orderId)
        {
            return objProducts.GetProductIdByOrderId(orderId);
        }
        public string GetProductSetXMLByProductSet(int productSet)
        {
            return objProducts.GetProductSetXMLByProductSet(productSet);
        }
    }
}
