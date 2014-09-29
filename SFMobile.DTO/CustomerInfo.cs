/// <summary>
/// CustomerInfo for Serena Product
/// Date: 07/26/2011 12:00:14 PM
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
    public class CustomerInfo
    {
        #region  Private Variables
        private string title;
        private string name;
        private string firstName;
        private string lastName;
        private string email;
        private string postCode;
        private string organisation;
        private string houseNo;
        private string street;
        private string district;
        private string town;
        private string county;
        private string countryCode;
        private string receipentTelPhNo;
        private string deliveryIns;
        private int noCardMessage;
        private string giftMessage;
        private string orderID;
        private int emailNewsletter;
        private string  uKMobile;
        private int smsNotification;
        private string voucherCode;
        private string country;
        private int sameAsDelivery;
        private int addr_verified;
        private int countryid;
        #endregion

        #region Property  Title
    
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        #region Property  Name

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region Property FirstName
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        #endregion

        #region Property LastName
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        #endregion

        #region Property Email
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        #endregion

        #region Property PostCode
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }
        #endregion

        #region Property Organisation
        public string Organisation
        {
            get { return organisation; }
            set { organisation = value; }
        }
        #endregion

        #region Property HouseNo
        public string HouseNo
        {
            get { return houseNo; }
            set { houseNo = value; }
        }
        #endregion

        #region Property Street
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        #endregion

        #region Property District
        public string District
        {
            get { return district; }
            set { district = value; }
        }
        #endregion

        #region Property Town
        public string Town
        {
            get { return town; }
            set { town = value; }
        }
        #endregion

        #region Property County
        public string County
        {
            get { return county; }
            set { county = value; }
        }
        #endregion

        #region Property CountryCode
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }
        #endregion

        #region Property ReceipentTelPhNo
        public string ReceipentTelPhNo
        {
            get { return receipentTelPhNo; }
            set { receipentTelPhNo = value; }
        }
        #endregion

        #region Property DeliveryIns
        public string DeliveryIns
        {
            get { return deliveryIns; }
            set { deliveryIns = value; }
        }
        #endregion

        #region Property NoCardMessage
        public int NoCardMessage
        {
            get { return noCardMessage; }
            set { noCardMessage = value; }
        }
        #endregion

        #region Property GiftMessage
        public string GiftMessage
        {
            get { return giftMessage; }
            set { giftMessage = value; }
        }
        #endregion

        #region Property OrderID
        public string OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        #endregion

        #region Property EmailNewsletter
        public int EmailNewsletter
        {
            get { return emailNewsletter; }
            set { emailNewsletter = value; }
        }
        #endregion

        #region Property UKMobile
        public string UKMobile
        {
            get { return uKMobile; }
            set { uKMobile = value; }
        }
        #endregion

        #region Property SMSNotification
        public int SMSNotification
        {
            get { return smsNotification; }
            set { smsNotification = value; }
        }
        #endregion

        #region Property VoucherCode
        public string VoucherCode
        {
            get { return voucherCode; }
            set { voucherCode = value; }
        }
        #endregion

        #region Property Country
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        #endregion
        #region Property CountryId
        public int CountryID
        {
            get { return countryid; }
            set { countryid = value; }
        }
        #endregion

        #region Property SameAsDelivery

        public int SameAsDelivery
        {
            get { return sameAsDelivery; }
            set { sameAsDelivery = value; }
        }
        public int Addr_Verified
        {
            get { return addr_verified; }
            set { addr_verified = value; }
        }

        #endregion

        #region Added additional properties for New Order Schema
        public int CustomerId { get; set; }

        #endregion
    }
}
