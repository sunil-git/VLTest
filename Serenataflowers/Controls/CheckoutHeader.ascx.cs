using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.DTO;
using System.IO;
using SFMobile.Exceptions;
using Serenata_Checkout.Bal;
using Serenata_Checkout.ChilkatComponent;
namespace Serenataflowers.Controls
{
    public partial class CheckoutHeader : System.Web.UI.UserControl
    {
        public bool EnableUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (EnableUrl == true)
                {
                    //if ((!string.IsNullOrEmpty(Request.QueryString["s"])) && (System.IO.Path.GetFileName(Request.Path).ToLower() != "m_confirmation.aspx"))
                    //{
                    if ((!string.IsNullOrEmpty(Request.QueryString["s"])))
                    {
                        navigateURL.HRef= "~/Default.aspx?s=" + Request.QueryString["s"];
                        GetCheckoutLogo();
                    }
                    else
                    {
                        navigateURL.HRef = "~/Default.aspx";
                    }
                }
                else
                {
                    navigateURL.HRef = "";
                    GetCheckoutLogo();
                }
                Encryption objEncryption = new Encryption();
                string OrderId = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                string strPage = Page.AppRelativeVirtualPath.Replace("~/", string.Empty).Replace(".aspx", string.Empty).ToLower();
                if (IsPaymentExists(OrderId) == "1" && strPage != "checkout/m_confirmation")
                {
                    Response.Redirect("m_confirmation.aspx?s=" + Request.QueryString["s"], false);
                }

            }
            
        }
        private void GetCheckoutLogo()
        {

          
            try
            {
                List<ProductTypeInfo> lstProductTypeInfo = new List<ProductTypeInfo>();
                CommonFunctions objCommondetails = new CommonFunctions();
                string xPathProductType = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["ProductTypeCategoryXML"].ToString());
                lstProductTypeInfo = objCommondetails.GetProductType(xPathProductType);
                if (lstProductTypeInfo != null)
                {
                    string url = Request.Url.ToString();
                    Uri baseUri = new Uri(url);
                    string domain = baseUri.Host;
                    foreach (ProductTypeInfo objdomain in lstProductTypeInfo)
                    {
                        if (objdomain.domainName.ToLower() == domain.ToLower())
                        {
                            switch (objdomain.ProductTypeId.ToString())
                            {
                                case "1":
                                    //navigateURL.InnerHtml = "<div id='logo-checkout-flowers'></div>";
                                    break;
                                case "2":
                                    navigateURL.InnerHtml = "<div id='logo-checkout-plants'></div>";
                                    break;
                                case "3":
                                    navigateURL.InnerHtml = "<div id='logo-checkout-chocolates'></div>";
                                    break;
                                case "4":
                                    navigateURL.InnerHtml = "<div id='logo-checkout-hampers'></div>";
                                    break;
                                case "5":
                                    navigateURL.InnerHtml = "<div id='logo-checkout-wines'></div>";
                                    break;
                              
                            }
                           
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            }
           
        }
        private string IsPaymentExists(string OrderID)
        {
            PaymentDetailsBAL ObjOrder = new PaymentDetailsBAL();
            return ObjOrder.CheckOrderPaymentStatus(OrderID);
        }
    }
}