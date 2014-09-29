using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
   public class DispatchesInfo
    {
       public int? DispatchID { get; set; }

       public string OrderID { get; set; }

       public int? CarrierID { get; set; }

       public DateTime DeliveryDate { get; set; }

       public string DeliveryTime { get; set; }
       public string DispatchType { get; set; }

       public int DelOptionID { get; set; }

       public DateTime DispatchDate { get; set; }
       public int? DeliveryServiceStatus { get; set; }
        public double DeliveryPrice { get; set; }

    }
}
