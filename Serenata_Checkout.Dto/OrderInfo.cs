using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class OrderInfo
    {
        public string OrderID { get; set; }
        public int? CustomerID { get; set; }
        public int FulfilmentPartnerId { get; set; }
        public int OrderStatusID { get; set; }
        public int SiteID { get; set; }
        public int ChannelID { get; set; }
        public int CurrencyID  { get; set; }
        public int VoucherID { get; set; }
        public int CountryID { get; set; }
        public string GoogleNumber { get; set; }
        public int CookiesID { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string BrowserIP { get; set; }
        public string BrowserCountry { get; set; }
        public string EncryptedOrderID { get; set; }
        public int EncryptionTypeID { get; set; }
        public string FuneralTime { get; set; }
        public int OrderComplete { get; set; }
        public int idSubOrderStatus { get; set; }
        public int FollowUp { get; set; }
        public string PartnerOrderRef { get; set; }
        public string Prefix { get; set; }

        public string BrowserID { get; set; }
        public string DeviceID { get; set; }
        public string AltVisitorID { get; set; }

        

    }
}
