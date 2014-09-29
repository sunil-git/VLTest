/// <summary>
/// ProducInfoClass for Serena Product
/// Date: 06/05/2011 11:25:14 PM
/// Author: Auto Generated.
/// <summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMobile.DTO
{
    /// <summary>
    /// This class is used to transfer the data from UI layer to BAl layer
    /// </summary>
    public class ProductDTO
    {

        #region local variables
        private int productId = 0;
        private string productname = string.Empty;
        private string productdesc = string.Empty;
        private decimal productoldprice;
        private decimal productofferprice;
        private string imagepath;
        private string producturl = string.Empty;
        private string deloptionname = string.Empty;
        private string deloptionprice;
        private string checkoutimagepath;
        private decimal checoutkproductprice;
        private decimal totalprice;
        private int quantity = 1;
        private int totalrecords;
        private string productInfo1;
        #endregion

        #region Property ProductID
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        #endregion

        #region Property ProductName
        public string ProductName
        {
            get { return productname; }
            set { productname = value; }
        }
        #endregion

        #region Property Product Description
        public string ProductDesc
        {
            get { return productdesc; }
            set { productdesc = value; }
        }
        #endregion

        #region Property ProductOldPrice
        public decimal ProductOldPrice
        {
            get { return productoldprice; }
            set { productoldprice = value; }
        }
        #endregion

        #region Property ProductOfferPrice
        public decimal ProductOfferPrice
        {
            get { return productofferprice; }
            set { productofferprice = value; }
        }
        #endregion

        #region Property ImagePath
        public string ImagePath
        {
            get { return imagepath; }
            set { imagepath = value; }
        }
        #endregion

        #region Property ProductURL
        public string ProductURL
        {
            get { return producturl; }
            set { producturl = value; }
        }
        #endregion

        #region Property DeliveryOptionName
        public string DelOptionName
        {
            get { return deloptionname; }
            set { deloptionname = value; }
        }
        #endregion

        #region Property DeliveryOptionPrice
        public string DelOptionPrice
        {
            get { return deloptionprice; }
            set { deloptionprice = value; }
        }
        #endregion
        #region Property CheckOutImgagePath
        public string CheckOutImgagePath
        {
            get { return checkoutimagepath; }
            set { checkoutimagepath = value; }
        }
        #endregion
        #region Property CheckProductPrice
        public Decimal CheckProductPrice
        {
            get { return checoutkproductprice; }
            set { checoutkproductprice = value; }
        }
        #endregion

        #region Property TotalPrice
        public Decimal TotalPrice
        {
            get { return totalprice; }
            set { totalprice = value; }
        }
        #endregion
        #region Property Quantity
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        #endregion
        #region Property TotalRecords
        public int TotalRecords
        {
            get { return totalrecords; }
            set { totalrecords = value; }
        }
        #endregion


        #region Property ProductInfo1
        public string ProductInfo1
        {
            get { return productInfo1; }
            set { productInfo1 = value; }
        }
        #endregion
        #region Property For Ratings
        public string TotalReviews { get; set; }
        public string StarPercentage { get; set; }
        public string StarRating { get; set; }
        public string StarRatingRounded { get; set; }
        public string StarRatingImageURL { get; set; }
        #endregion

        /// <summary>
        /// Default constructor for Products
        /// <summary>
        public ProductDTO()
        {
        }
        /// <summary>
        /// Overloaded constructor for Products
        /// <summary>
        public ProductDTO(int ProductId, string ProductName, string ProductDesc, decimal ProductOldPrice, decimal ProductOfferPrice, string ImagePath)
        {
            productId = ProductId;
            productname = ProductName;
            productdesc = ProductDesc;
            productoldprice = ProductOldPrice;
            productofferprice = ProductOfferPrice;
            imagepath = ImagePath;

        }
    }
}
