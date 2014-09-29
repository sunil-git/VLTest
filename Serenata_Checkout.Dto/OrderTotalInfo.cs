using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class OrderTotalInfo
    {
        #region Properties
        public double OrderTotal { get; set; }
        public double PaymentTotal { get; set; }
        #endregion
    }
}
