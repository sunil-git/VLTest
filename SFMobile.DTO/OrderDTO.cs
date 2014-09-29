/// <summary>
/// ProducInfoClass for Serenata Product
/// Date: 07/22/2011 11:25:14 AM
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
    public class OrderDTO
    {

        #region Private variables
        private int productId;
        private int quantity;
        private DateTime deliveryDate;
        private int deliveryOptionId;
        private string optionalName;
        private float optionalCost;
        private int idorderStatus;
        private int currencyId;
        private string deliveryTime;
        private int idShop;
        private int idBranch;
        private int idDeliveryRegion;
        private string countryCode;
        private string orgCostName;
        private string orgCost;
        private string userIpAddress;
        private string serverIpAddress;
        private string ipCountry;
        private string trialPay;
        private int sessionId;
        private int siteId;
        private string visitorId;
        private string browserCountry;
        private int idCustomer;
        private int consignmentNumber;
        private int? idChannel; // Set idChannel as nullable variable for New Order Schema
        private string orderId;
        #endregion

        #region Property OrderId
        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        #endregion

        #region Property ProductId
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        #endregion

        #region Property Quantity
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        #endregion

        #region Property   DeliveryDate
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }
        #endregion

        #region Property  DeliveryOptionId
        public int DeliveryOptionId
        {
            get { return deliveryOptionId; }
            set { deliveryOptionId = value; }
        }
        #endregion

        #region Property  OptionalName
        public string OptionalName
        {
            get { return optionalName; }
            set { optionalName = value; }
        }
        #endregion

        #region Property  OptionalCost
        public float OptionalCost
        {
            get { return optionalCost; }
            set { optionalCost = value; }
        }
        #endregion

        #region Property  IdorderStatus
        public int IdorderStatus
        {
            get { return idorderStatus; }
            set { idorderStatus = value; }
        }
        #endregion

        #region Property  CurrencyId
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }
        #endregion

        #region Property  DeliveryTime
        public string DeliveryTime
        {
            get { return deliveryTime; }
            set { deliveryTime = value; }
        }
        #endregion

        #region Property  IdShop
        public int IdShop
        {
            get { return idShop; }
            set { idShop = value; }
        }
        #endregion

        #region Property  IdBranch
        public int IdBranch
        {
            get { return idBranch; }
            set { idBranch = value; }
        }
        #endregion

        #region Property  IdDeliveryRegion
        public int IdDeliveryRegion
        {
            get { return idDeliveryRegion; }
            set { idDeliveryRegion = value; }
        }
        #endregion

        #region Property  CountryCode
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }
        #endregion

        #region Property  OrgCostName
        public string OrgCostName
        {
            get { return orgCostName; }
            set { orgCostName = value; }
        }
        #endregion

        #region Property  OrgCost
        public string OrgCost
        {
            get { return orgCost; }
            set { orgCost = value; }
        }
        #endregion

        #region Property  UserIpAddress
        public string UserIpAddress
        {
            get { return userIpAddress; }
            set { userIpAddress = value; }
        }
        #endregion

        #region Property  ServerIpAddress
        public string ServerIpAddress
        {
            get { return serverIpAddress; }
            set { serverIpAddress = value; }
        }
        #endregion

        #region Property  IpCountry
        public string IpCountry
        {
            get { return ipCountry; }
            set { ipCountry = value; }
        }
        #endregion

        #region Property  TrialPay
        public string TrialPay
        {
            get { return trialPay; }
            set { trialPay = value; }
        }
        #endregion

        #region Property  SessionId
        public int SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }
        #endregion

        #region Property  SiteId
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        #endregion

        #region Property  VisitorId
        public string VisitorId
        {
            get { return visitorId; }
            set { visitorId = value; }
        }
        #endregion

        #region Property  BrowserCountry
        public string BrowserCountry
        {
            get { return browserCountry; }
            set { browserCountry = value; }
        }
        #endregion

        #region Property  IdCustomer
        public int IdCustomer
        {
            get { return idCustomer; }
            set { idCustomer = value; }
        }
        #endregion

        #region Property  ConsignmentNumber
        public int ConsignmentNumber
        {
            get { return consignmentNumber; }
            set { consignmentNumber = value; }
        }
        #endregion

        // Set nullable to idChannel for New Order Schema
        #region Property  IdChannel
        public int? IdChannel
        {
            get { return idChannel; }
            set { idChannel = value; }
        }
        #endregion


        /// <summary>
        /// Default Consructor for OrderInfo
        /// </summary>
        public OrderDTO()
        { }



        #region Added additional properties for New Order Schema
            public int? CookieId { get; set; }
            public string Description { get; set; }
            public string Prefix { get; set; }
            public string EncryptedOrderID { get; set; }
            public string VoucherCode { get; set; }

            public double ProdVATRate { get; set; }
            public double Price { get; set; }
            public int PartnerID { get; set; }

            public int DeliveryPartnerID { get; set; }
            public int UpsaleCount { get; set; }
        
            public int VoucherID { get; set; }
            public int UpdateVoucherFlag { get; set; }
            public double Discount { get; set; }
            public double Total { get; set; }
        #endregion

            public int MultiFPTrue { get; set; }
            public string ErrorString { get; set; }

    }


}