using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.DTO;
using System.IO;
using SFMobile.Exceptions;
namespace Serenataflowers.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        public bool EnableUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (EnableUrl == true)
                {
                    if ((!string.IsNullOrEmpty(Request.QueryString["s"]) ) && (System.IO.Path.GetFileName(Request.Path).ToLower()!="m_confirmation.aspx") )
                    {
                        aDefaultURL.HRef = "~/Default.aspx?s=" + Request.QueryString["s"];

                    }                    
                    else {                      
                        aDefaultURL.HRef ="~/Default.aspx";
                    }
                }
                else
                {
                    aDefaultURL.HRef = "";
                }
                logo1.Attributes["class"] = GetLogoCss(); ;
            }
        }
        private string GetLogoCss()
        {

            string strDomain = "logo_flowers";
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
                            strDomain ="logo_"+ objdomain.ProductType.ToLower();
                           
                            break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            }
            return strDomain;
        }
    }
}