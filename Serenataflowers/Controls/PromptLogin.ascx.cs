using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Bal.Common;
using SFMobile.Exceptions;
namespace Serenataflowers.Controls
{
    public partial class PromptLogin : System.Web.UI.UserControl
    {
        string orderID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                //{
                //    if (!IsPostBack)
                //    {
                //        orderID = Common.GetOrderIdFromQueryString();
                //        Submit.Attributes.Add("data-theme", "a");
                //        Submit.Attributes.Add("data-role", "button");
                //        Submit.Attributes.Add("data-inline", "true");
                //        Submit.Attributes.Add("data-mini", "true");
                //        Submit.Attributes.Add("data-icon", "edit");
                //        Submit.Attributes.Add("data-iconpos", "left");
                //        ViewState["OrderID"] = orderID;
                        
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginInfo objLoginInfo = new LoginInfo();
            Common objCommon = new Common();
            try
            {
                if (LoginEmailAddress.Value.Trim() != "" && LoginPassword.Value.Trim() != "")
                {
                    string orderId = Common.GetOrderIdFromQueryString();
                    objLoginInfo.EmailAddress = LoginEmailAddress.Value.Trim();
                    objLoginInfo.EncryptedPassword = objCommon.CalculateMD5Hash(LoginPassword.Value.Trim());
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
                        ScriptManager.RegisterClientScriptBlock(btnLogin, btnLogin.GetType(), "expand", "displayInvalidLogin();", true);
                        //ErrorSignIn.InnerText = "Invalid Login";
                        //ErrorSignIn.Style.Add("display", "block");
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }           
        }
    }
}