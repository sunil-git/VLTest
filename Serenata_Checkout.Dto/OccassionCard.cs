using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serenata_Checkout.Dto
{
    public class OccassionCard
    {
        public string ProductTitle { get; set; }

        public int ProductId { get; set; }

        public string Info2 { get; set; }

        public decimal Price { get; set; }

        //public int PartnerID { get; set; }

        //public int SoldOut { get; set; }

        //public int Deliverypartnerid { get; set; }

        public string Img1BigHigh { get; set; }

        public string Img3BigHigh { get; set; }

        public string Img1SmallHigh { get; set; }

        public string IsCheckedString { get; set; }

        public int DomainID { get; set; }

        public string CssClass { get; set; }

        public int NoCard { get; set; }

        public string CssClassWithCounter { get; set; }

        private bool visible=false ;
        public bool IsVisible
        {
            get { return visible; }
            set { visible = value; }
        }
        
    }
}
