using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class DHHPInfo
    {
        public string OrderID { get; set; }
        public string UUID { get; set; }        
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string ProductDescription { get; set; }
        public string Payment { get; set; }
        public string RecurringCode { get; set; }
        public string MerchantEmail { get; set; }
        public string EncryptedOrderID { get; set; }
        public string TrasactionSecrete { get; set; }
        public string Token { get; set; }  
        
    }
    
}
