using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.BAL.Checkout;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using SFMobile.BAL.Orders;
using SerenataOrderSchemaBAL;
using Serenata_Checkout.Logic;
using System.Web.UI.HtmlControls;

namespace Serenataflowers
{

    public partial class m_checkoutCancel : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                string OrderID = Common.GetOrderIdFromQueryString();
                Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "PaypalCancel");
                Response.Redirect("m_paymentdetails.aspx?s=" + Request.QueryString["s"]);

            }
            else
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }

       
    }
}