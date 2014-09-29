using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class DeliveryTimeInfo
    {
        public string Deliverydate { get; set; }

        public string DateValue { get; set; }

        public string OptionName { get; set; }

        public Int32 id { get; set; }
        public double OrderTotal { get; set; }
        public double discount { get; set; }
        public double deliveryPrice { get; set; }
        public string voucherTitle { get; set; }
        public double SubTotal { get; set; }
        public double TotalExVat { get; set; }
        public Int32 DeliveryPartnerID { get; set; }
        public Int32 SiteID { get; set; }
        
    }
}
