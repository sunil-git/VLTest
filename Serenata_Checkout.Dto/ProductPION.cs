using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
   public class ProductPION
    {
        public int productid { get; set; }
        public Double price { get; set; }
        public Double discount { get; set; }
        public Double raiting { get; set; }

        public int noOfreviews { get; set; }

        public string hasVideo { get; set; }
        public string hasDiscountRibbon { get; set; }
        public string SortOrder { get; set; }
    }
}
