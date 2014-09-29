using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class DiscountInfo
    {
        #region Properties
            public string DiscountCode { get; set; }
            public int DiscountValue { get; set; }
            public DateTime DiscountExpiryDate { get; set; }
        #endregion
    }
}
