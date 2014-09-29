using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
  [Serializable()]
  public  class ProductInfo
    {
      public int productid { get; set; }

      public string producttitle { get; set; }

      public Double price { get; set; }

      public string img1_small_low { get; set; }

      public double offer { get; set; }
      public string img1_big_high { get; set; }
      public string info2 { get; set; }
      public DateTime offerstartdate { get; set; }
      public DateTime offerenddate { get; set; }
      public int quantity { get; set; }
      public double qtyPrice {get;set; }
      public Boolean isMainProduct { get; set; }


    
    }
}
