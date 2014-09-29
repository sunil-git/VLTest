using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
namespace Serenataflowers.Controls
{
    public partial class BasketCount : System.Web.UI.UserControl
    {
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.UpBasketCount.UpdateMode; }
            set { this.UpBasketCount.UpdateMode = value; }
        }

        public void Update()
        {
            if (ViewState["OrderID"] != null)
            {
                BindBasketCount(Convert.ToString(ViewState["OrderID"]));
            }
            this.UpBasketCount.Update();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    if (!IsPostBack)
                    {
                        string orderID = Common.GetOrderIdFromQueryString();
                        ViewState["OrderID"] = orderID;
                        BindBasketCount(orderID);
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }
        public void BindBasketCount(string orderId)
        {
            try
            {
                OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();
               // basketCount.HRef = "~/Checkout/m_basket.aspx?s=" + Request.QueryString["s"];
                int quantity = objOrderDetails.GetTotalProductQty(orderId);
                basketCount.Text = Convert.ToString(quantity);
            }
            catch (Exception ex)
            {

            }
        }
    }
}