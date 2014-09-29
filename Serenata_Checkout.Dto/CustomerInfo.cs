using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class CustomerInfo
    {


        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostCode { get; set; }
        public string Organisation { get; set; }
        public string HouseNo { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
      
        public string CountryCode { get; set; }
        public string EncryptedPassword { get; set; }

        public string ReceipentTelPhNo { get; set; }

        public string DeliveryIns { get; set; }
        public int NoCardMessage { get; set; }

        public int GiftMessage { get; set; }

        public string OrderID { get; set; }

        public string UKMobile { get; set; }
        public int? SMSNotification { get; set; }
        public string VoucherCode { get; set; }
        public string Country { get; set; }
        public string ISOCountry { get; set; }
        public int CountryID { get; set; }
        public int Addr_Verified { get; set; }
        public int SameAsDelivery { get; set; }
        public int?EmailNewsletter { get; set; }
        
       
               
    }
}
