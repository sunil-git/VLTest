using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Logic;
using Serenata_Checkout.Bal.Common;
using Serenata_Checkout.Dto;
using System.Configuration;
using SFMobile.Exceptions;
using Serenata_Checkout.Bal;
using Serenata_Checkout.ExactTargetAPI;
using SFMobile.DTO;
namespace Serenataflowers.Controls
{
    public partial class ContactUs : System.Web.UI.UserControl
    {
        string orderID,strEmailMessage, strTicket;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    if (!IsPostBack)
                    {
                        orderID = Common.GetOrderIdFromQueryString();
                        ContactUsOrderNumber.Value = orderID;


                        Serenata_Checkout.Dto.CustomerInfo objCustomerInfo = new Serenata_Checkout.Dto.CustomerInfo();
                        CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                        objCustomerInfo = objCustomerDetails.GetBillingDetails(orderID);
                        if (objCustomerInfo.Email != null && objCustomerInfo.Email != "")
                        {
                            ContactUsName.Value = objCustomerInfo.FirstName + " " + objCustomerInfo.LastName;
                            ContactUsEmail.Value = objCustomerInfo.Email;
                            ContactUsPhone.Value = objCustomerInfo.UKMobile;
                        }
                        SubmitRequest.Attributes.Add("data-theme", "a");
                        SubmitRequest.Attributes.Add("data-role", "button");
                        SubmitRequest.Attributes.Add("data-inline", "true");
                        SubmitRequest.Attributes.Add("data-mini", "true");
                        SubmitRequest.Attributes.Add("data-icon", "edit");
                        SubmitRequest.Attributes.Add("data-iconpos", "left");


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void SubmitRequest_Click(object sender, EventArgs e)
        {
            try
            {
                bool success;
                const int STATUS_ID = 1;
                const string ENCRYPTION_TYPE = "Chilkat";
                const int REASON_ID = 1;
                const int SOURCE_ID = 3;
                string strPassword = string.Empty;
                CommonBal objCommonBal = new CommonBal();
                MetaDataInfo objMetaData = new MetaDataInfo();
                CommonFunctions objcommon = new CommonFunctions();
                CustomerDetailsBAL custBal = new CustomerDetailsBAL();
                objMetaData = objcommon.GetMetaData(System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"]);
                string strOrderId = ContactUsOrderNumber.Value.Trim();
                ContactInfo objCont = new ContactInfo();
                objCont.MessageFromName = ContactUsName.Value;
                objCont.MessageFromEmail = ContactUsEmail.Value;
                objCont.MessageTo = ConfigurationManager.AppSettings["MessageTo"];
                objCont.OrderID = strOrderId;
                objCont.SubjectStr = ContactUsQueries.SelectedItem.Text;
                objCont.MessageFromPhone = ContactUsPhone.Value;
                objCont.IPAddress = GetIPAddress();
                string strMessage = ContactUsMessage.Value.Trim();

                if (string.IsNullOrEmpty(custBal.CheckOrderExist(strOrderId.Trim()))) //Invalid Order
                {
                    strOrderId = custBal.GetLatestOrderIDByEmail(ContactUsEmail.Value.Trim());
                }
                else if (strOrderId == "")
                {
                    strOrderId = custBal.GetLatestOrderIDByEmail(ContactUsEmail.Value.Trim());
                }
                if (strOrderId == "")
                    strOrderId = "00000000";
                //Getting CustomerID


                if (strOrderId.Length != 0)
                {

                     strPassword = ReverseString(strOrderId).Substring(0, strOrderId.Length - 1);
                }
                else
                {
                    strPassword = System.Configuration.ConfigurationManager.AppSettings["EncryptionPassword"].ToString();
                }
                Encryption objEncryption = new Encryption();
                string strEncryptMessage = objEncryption.GetAesEncryptionString(strMessage, strPassword);
                objCont.EncryptedMessage = strEncryptMessage;
                objCont.EncryptionType = ENCRYPTION_TYPE;
                objCont.ReasonID = REASON_ID;
                objCont.SourceID = SOURCE_ID;
                //objCont.IdTicket = intTicketId;
                string newGuid =  objCommonBal.InsertContactInfo(objCont);
               // string Result  = objCommonBal.InsertContactTicket(strOrderId, STATUS_ID);
               // int intTicketId =Convert.ToInt32( Result.Split('|')[0]);
               // string New_GUID=Convert.ToString( Result.Split('|')[1]);
                strTicket = "http://" + Common.getRootUrl() + "/contactpage_latest.asp?ticketid=" + newGuid;
                strEmailMessage = strEmailMessage + "Dear " + ContactUsName.Value + "<br /><br />";
                strEmailMessage = strEmailMessage + "Thank you for your recent correspondence. It has been forwarded onto our customer services team who will respond shortly if required.<br /><br />";
                strEmailMessage = strEmailMessage + "Please do not reply to this email as it has been sent from an unmanned email account.<br /><br />";
                strEmailMessage = strEmailMessage + "Should you wish to contact us again regarding this issue, please <a href='" + strTicket + "'> click here </a><br />";
                strEmailMessage = strEmailMessage.Replace("[TICKETURL]", strTicket);
                
                ExactTargetEmail ETobj = new ExactTargetEmail();
                success = ETobj.SendEmailfromContactUSUsingExactTarget(ContactUsEmail.Value, ContactUsEmail.Value, ContactUsOrderNumber.Value, ContactUsName.Value, strEmailMessage, Convert.ToInt32(objMetaData.SiteId));
                if (success)
                ScriptManager.RegisterClientScriptBlock(SubmitRequest, SubmitRequest.GetType(), "test123", "OpenSuccess();", true);
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            
            //ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "bindVoucher();", true);
        }
        public static string ReverseString(string s)
        {
            string str = string.Empty;
            try
            {
                char[] arr = s.ToCharArray();
                Array.Reverse(arr);
                str = new string(arr);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return str;
        }
        private string GetIPAddress()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
    }
}