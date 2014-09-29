using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using Serenata_Checkout.ChilkatComponent;
namespace Serenataflowers.Checkout
{
    public partial class m_login : System.Web.UI.Page
    {
        string orderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            UpsellsBAL objUpsellsBAL = new UpsellsBAL();
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    if (!IsPostBack)
                    {
                        orderID = Common.GetOrderIdFromQueryString();
                        Common.AddMetaTags(orderID, (HtmlHead)Page.Header, "Login");
                        LoadSocialLoginIcons(new Common().GetSiteName().Replace("m.","").Replace("localhost","serenataflowers.com"));
                    }
                }
                else
                {
                    Response.Redirect("../ErrorPage.aspx");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private void LoadSocialLoginIcons(string siteName)
        {
            try
            {
                HtmlGenericControl jqueryInclude = new HtmlGenericControl("script");
                jqueryInclude.Attributes.Add("type", "text/javascript");
                jqueryInclude.Attributes.Add("src", "https://cdns.gigya.com/js/socialize.js?apiKey=" + Common.GetSocializeKey(siteName));
                jqueryInclude.InnerHtml = "{siteName: '" + siteName + "',enabledProviders: 'facebook,googleplus,yahoo,microsoft'}";
                Page.Header.Controls.Add(jqueryInclude);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            LoginInfo objLoginInfo = new LoginInfo();
            Common objCommon = new Common();
            try
            {
                if (LoginEmailAddress.Value.Trim() != "" && LoginPassword.Text.Trim() != "")
                {
                    string orderId = Common.GetOrderIdFromQueryString();
                    objLoginInfo.EmailAddress = LoginEmailAddress.Value.Trim();
                    objLoginInfo.EncryptedPassword = objCommon.CalculateMD5Hash(LoginPassword.Text.Trim());
                    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                    int CustomerId = objCustomerDetails.CustomerLogin(objLoginInfo, orderId);
                    if (CustomerId > 0)
                    {
                        HttpCookie cooki = new HttpCookie("CustomerID", CustomerId.ToString());
                        //cooki.Domain = objCommon.GetSiteName();
                        Response.Cookies.Add(cooki);
                        if (!string.IsNullOrEmpty(Request.QueryString["multiFP"]))
                        {
                            Response.Redirect("m_recipientdetails.aspx?s=" + Common.GetQueryStringValue("s") + "&multiFP=" + Common.GetQueryStringValue("multiFP"), false);
                        }
                        else
                        {
                            Response.Redirect("m_recipientdetails.aspx?s=" + Common.GetQueryStringValue("s"), false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(SignIn, SignIn.GetType(), "expand", "ExpnadExitCustomer();", true);
                        ErrorSignIn.InnerText = "Invalid Login";
                        ErrorSignIn.Style.Add("display", "block");
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
           
        }
        protected void ChechoutAsGuest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Common.GetQueryStringValue("multiFP")))
            {
                Response.Redirect("m_customerdetails.aspx?s=" + Common.GetQueryStringValue("s") + "&multiFP=" + Common.GetQueryStringValue("multiFP") + "&guest=yes", false);
            }
            else
            {
                Response.Redirect("m_customerdetails.aspx?s=" + Common.GetQueryStringValue("s") + "&guest=yes", false);
            }
           
        }
        protected void Register_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Common.GetQueryStringValue("multiFP")))
            {
                Response.Redirect("m_customerdetails.aspx?s=" + Common.GetQueryStringValue("s") + "&multiFP=" + Common.GetQueryStringValue("multiFP"), false);
            }
            else
            {
                Response.Redirect("m_customerdetails.aspx?s=" + Common.GetQueryStringValue("s"), false);
            }
           
        }
        protected void btnSocialLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string strsocialEmail = hdnSNEmail.Value;
                CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                Common objCommon = new Common();
                LoginInfo objLoginInfo = new LoginInfo();
                objLoginInfo.EmailAddress = hdnSNEmail.Value;
                string orderId = Common.GetOrderIdFromQueryString();
                objLoginInfo.SocialUserID = hdnSNusername.Value;
                objLoginInfo.SocialMediaType = hdnSNloginProvider.Value;
                int CustId = objCustomerDetails.SocailMedaiLogIn(objLoginInfo, orderId);
                if (CustId > 0)
                {
                    HttpCookie cooki = new HttpCookie("CustomerID", CustId.ToString());
                    //cooki.Domain = objCommon.GetSiteName();
                    Response.Cookies.Add(cooki);
                    if (!string.IsNullOrEmpty(Request.QueryString["multiFP"]))
                    {
                        Response.Redirect("m_recipientdetails.aspx?s=" + Request.QueryString["s"] + "&multiFP=" + Request.QueryString["multiFP"], false);
                    }
                    else
                    {
                        Response.Redirect("m_recipientdetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
                else
                {
                    System.Text.StringBuilder custData = new System.Text.StringBuilder();
                    custData.Append(Common.GetJSHiddenValue(hdnSNEmail.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNName.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNAddressLine1.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNTownCity.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNCountry.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNPhone.Value) + "|");
                    custData.Append(Common.GetJSHiddenValue(hdnSNLastname.Value) + "|");
                    Encryption objEncryption = new Encryption();
                    string encrptedStrSocialdata = objEncryption.GetAesEncryptionString(custData.ToString());
                    if (!string.IsNullOrEmpty(Request.QueryString["multiFP"]))
                    {
                        Response.Redirect("m_customerdetails.aspx?s=" + Request.QueryString["s"] + "&multiFP=" + Request.QueryString["multiFP"] + "&sn=" + encrptedStrSocialdata, false);
                    }
                    else
                    {
                        Response.Redirect("m_customerdetails.aspx?s=" + Request.QueryString["s"] + "&sn=" + encrptedStrSocialdata, false);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            DisplayBasketCount.Update();
            ModifyBasket.Update();
        }
        protected void SaveDates_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
        protected void Voucher_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
    }
}