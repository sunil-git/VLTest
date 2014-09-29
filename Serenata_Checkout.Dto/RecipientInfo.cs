using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class RecipientInfo
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
        public int CountryID { get; set; }
        public string RecipientMobile { get; set; }
        public int DeliveryAddressID { get; set; }
        public int AddressVerified { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderId { get; set; }
        public int CustomerID { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        public string ConsignmentNumber { get; set; }
        public string DeliveryInstructions { get; set; }
        public string DiscountName { get; set; }
        public int CurrencyId { get; set; }
        
        public DateTime DeliveryDate { get; set; }
        public string SiteId { get; set; }
        public string OptionalName { get; set; }
        public double OptionalCost { get; set; }
        public int DelOptionId { get; set; }
        
        public string Message { get; set; }
        public int FulfillmentPartnerId { get; set; }

        public string CountryName { get; set; }
        
    }
    public class AddressInfo
    {
        public int AddressID { get; set; }
        public string FullName { get; set; }
    }
}
