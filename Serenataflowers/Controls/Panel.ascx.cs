using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serenataflowers.Controls
{
    public partial class Panel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             //if (!string.IsNullOrEmpty(Request.QueryString["s"]))
             //{
             //    EditBasket.HRef = "m_basket.aspx?s=" + Request.QueryString["s"];
             //}
            string strPagePath = Page.AppRelativeVirtualPath.Replace("~/", string.Empty).Replace(".aspx", string.Empty).ToLower();
            string strPage = strPagePath.Replace("checkout/",string.Empty);

            if (strPage == "m_confirmation" || strPage=="m_checkoutreview")
            {
                liEditBasket.Visible = false;
                liEditDelDate.Visible = false;
                liVoucher.Visible = false;
                liContact.Visible = true;
            }
            else
            {
                liEditBasket.Visible = true;
                liEditDelDate.Visible = true;
                liVoucher.Visible = true;
                liContact.Visible = true;
            }
        }
    }
}