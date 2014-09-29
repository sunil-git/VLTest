using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class ConfirmDetailsInfo
    {
        #region Properties
            public string Message { get; set; }
            public string Payment { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime Deliverydate { get; set; }
            public string DeliveryInstructions { get; set; }
            public string CustomerId { get; set; }
        #endregion
    }
}
