using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.ExactTargetAPI;
namespace Serenataflowers.Controls
{
    public partial class ETEmailVerify : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Common.GetQueryStringValue("s") != null)
                {
                    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                    CustomerInfo objCustomerInfo = new CustomerInfo();
                    string OrderID = Common.GetOrderIdFromQueryString();
                    objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);
                    ValidateETEmail.Value = objCustomerInfo.Email;
                }
            }
        }
        protected void btnEmailVerify_Click(object sender, EventArgs e)
        {
            string strIsValid = ValidateEmail();
            if (strIsValid == "true")
            {
                ErrorSignIn.Style["display"] = "none";
                ValidateETEmail.Style["background"] = "";
                ValidateETEmail.Style["border"] = "";
                if (Common.GetQueryStringValue("s") != null)
                {
                    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                    CustomerInfo objCustomerInfo = new CustomerInfo();
                    string OrderID = Common.GetOrderIdFromQueryString();
                    int CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                   
                    objCustomerInfo.CustomerId = CustomerId;
                    objCustomerInfo.Email = ValidateETEmail.Value;
                    objCustomerDetails.EditCustomerEmail(objCustomerInfo);
                    ScriptManager.RegisterClientScriptBlock(btnEmailVerify, btnEmailVerify.GetType(), "ytyr", "closeETverifyPopup();", true);
                    if (Request.QueryString["guest"] != null)
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"] + "&guest=yes", false);
                    }
                    else
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
                
               
            }
            else {
                ErrorSignIn.Style["display"] = "block";
                ValidateETEmail.Style["background"] = "#f9c2c2";
                ValidateETEmail.Style["border"] = "1px solid red";
                ErrorSignIn.InnerHtml = "Email id is invalid! please enter valid email id.";
            }
        }
        public string ValidateEmail()
        {
            string isvalidate = String.Empty;
            try
            {
                FuelAPIValidateEmail objValidateEmail = new FuelAPIValidateEmail();
                isvalidate = objValidateEmail.ValidateEmail(ValidateETEmail.Value);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return isvalidate;
        }

    }
}