using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class OrderLinesInfo
    {
        public int OrderLineID { get; set; }

        public int DispatchID { get; set; }

        public string OrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double VATRate { get; set; }

        public double Discount { get; set; }

        public int OrderLineStatus { get; set; }

        public string Description { get; set; }

        public int RefundStatus { get; set; }

        public int RefundAmount { get; set; }
        public int PartnerID { get; set; }

        
    }
}
