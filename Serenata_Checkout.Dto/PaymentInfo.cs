using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class PaymentInfo
    {
        public string OrderID { get; set; }
        public int PaymentTypeID { get; set; }
        public int PaymentGatewayID { get; set; }
        public int PaymentStatus { get; set; }
        public double Amount { get; set; }
        public int IDCardType { get; set; }
        public string TransID { get; set; }
        public int IDTransType { get; set; }
        public double TotalAmount { get; set; }
        public string Comment { get; set; }
        public string CV2Result { get; set; }
        public string AddressResult { get; set; }
        public string PostcodeResult { get; set; }
        public string CardScheme { get; set; }
        public string CardCountry { get; set; }
        public string ChasePaymentType { get; set; }
    }
}
