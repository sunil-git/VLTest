using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serenataflowers.Controls
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var aboutus = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/about.aspx"));
                var contactus = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/contact.aspx"));
                var flowersdel = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/flower_delivery.aspx"));
                var condition = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/termsandcond.aspx"));

                hrefAbouts.HRef = aboutus;// "~/about.aspx";
                hrefContactus.HRef = contactus;// "~/contact.aspx";
                hrefFlowerDelivery.HRef = flowersdel;// "~/flower_delivery.aspx";
                hrefTermsConditions.HRef = condition;// "~/termsandcond.aspx";

                lblcopyright.Text = "2013"+ " - "+ DateTime.Now.Year.ToString();
            }

        }
    }
}