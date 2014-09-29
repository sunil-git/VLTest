using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class OrderTracking
    {
        public string OrderStatusName { get; set; }
        public string Description { get; set; }
        public string DeliveryDate { get; set; }
        public string ConsignmentNumber { get; set; }
        public int OrderStatusID { get; set; }
    }
}
