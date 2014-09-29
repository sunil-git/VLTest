using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using log4net;
using log4net.Config;
using SFMobile.Exceptions;
using System.Web.UI.HtmlControls;
using SFMobile.BAL.Orders;
using SFMobile.DTO;
using Chilkat;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Serenata_Checkout.ExactTargetAPI;
using Serenata_Checkout.Logic;
using Serenata_Checkout.Bal.Common;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Bal;


namespace Serenataflowers
{
    public partial class Contact : AjaxLoader //System.Web.UI.Page
    {

        string strEmailMessage, strTicket;
        protected void Page_Load(object sender, EventArgs e)
        {
            ltTitle.Text = "\n<title>" + " Contact us - " + CommonFunctions.PageTitle() + "</title>\n";
            CreateMetaTags();        
            
        }      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool success;
                const int STATUS_ID = 1;
                const string ENCRYPTION_TYPE = "Chilkat";
                const int REASON_ID = 1;
                const int SOURCE_ID = 3;
                CommonBal objCommonBal = new CommonBal();
                MetaDataInfo objMetaData = new MetaDataInfo();
                CommonFunctions objcommon = new CommonFunctions();
                objMetaData = objcommon.GetMetaData(System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"]);
                string strOrderId = txtOrdernumber.Text.Trim();
               // string Result = objCommonBal.InsertContactTicket(strOrderId, STATUS_ID);
                ContactInfo objCont = new ContactInfo();
                objCont.MessageFromName = txtName.Text.Trim();
                objCont.MessageFromEmail = txtEmail.Text.Trim();
                objCont.MessageTo = ConfigurationManager.AppSettings["MessageTo"];
                objCont.OrderID = strOrderId;
                objCont.SubjectStr = drpQuery.SelectedItem.ToString();
                objCont.MessageFromPhone = txtPhone.Text;
                objCont.IPAddress = GetIPAddress();
                string strMessage = txtMessage.Text;
                string strPassword = "";
                CustomerDetailsBAL custBal = new CustomerDetailsBAL();
                //Getting CustomerID

                if (string.IsNullOrEmpty(custBal.CheckOrderExist(strOrderId.Trim()))) //Invalid Order
                {
                    strOrderId = custBal.GetLatestOrderIDByEmail(txtEmail.Text.Trim());
                }
                else if (strOrderId == "")
                {
                    strOrderId = custBal.GetLatestOrderIDByEmail(txtEmail.Text.Trim());
                }
                if (strOrderId == "")
                    strOrderId = "00000000";


                if (strOrderId.Trim() != "")
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
               // objCont.IdTicket = intTicketId;
                string newGuid = objCommonBal.InsertContactInfo(objCont);
               // int intTicketId = Convert.ToInt32(Result.Split('|')[0]);
               // string New_GUID = Convert.ToString(Result.Split('|')[1]);
                strTicket = "http://" + Common.getRootUrl() + "/contactpage_latest.asp?ticketid=" + newGuid;
                strEmailMessage = strEmailMessage + "Dear " + txtName.Text + "<br /><br />";
                strEmailMessage = strEmailMessage + "Thank you for your recent correspondence. It has been forwarded onto our customer services team who will respond shortly if required.<br /><br />";
                strEmailMessage = strEmailMessage + "Please do not reply to this email as it has been sent from an unmanned email account.<br /><br />";
                strEmailMessage = strEmailMessage + "Should you wish to contact us again regarding this issue, please<a href='" + strTicket + "'> click here </a><br />";
                strEmailMessage = strEmailMessage.Replace("[TICKETURL]", strTicket);
              

                ExactTargetEmail ETobj = new ExactTargetEmail();
                success = ETobj.SendEmailfromContactUSUsingExactTarget(txtEmail.Text, txtEmail.Text, txtOrdernumber.Text, txtName.Text, strEmailMessage, 1);
                if (success)
                {
                    lblMessage.Text = "<b>Thank you!<br/>We will try to come back to you as soon as possible.</b>";
                    lblpara.Visible = true;
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtPhone.Text = "";
                    txtOrdernumber.Text = "";
                    txtMessage.Text = "";
                    drpQuery.SelectedIndex = -1;
                    UpdatePanel1.Update();
                }
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                
            }

            //MetaDataInfo objMetaData = new MetaDataInfo();
            //CommonFunctions objcommon = new CommonFunctions();
            //objMetaData = objcommon.GetMetaData(System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"]);
            //#region email
            //bool success;
            //const int STATUS_ID = 1;
            ////String strMessage = "";
            ////string strmailTo = System.Configuration.ConfigurationManager.AppSettings["ContactUsEmail"].ToString(); //"IS5862_" + drpQuery.SelectedValue.ToString() + "@is.instantservice.com";
            ////strMessage = "Message from " + txtName.Text.Trim() + "<br/>Phone number: " + txtPhone.Text.Trim() + "<br />" + "Order number: " + txtOrdernumber.Text.Trim() + "<br />" + "Message: " + txtMessage.Text.Trim();
            ////if (txtEmail.Text.Trim() != "")
            ////{
                
            ////    success = SendMail(txtEmail.Text.Trim(), strmailTo, "Contact us message", strMessage);
            ////}
            ////else
            ////{
            ////    success = false;
            ////}
            ////if(success==true)
            ////{
            ////    lblMessage.Text = "<b>Thank you! One of our staff will come back to you as quick as possible. We aim to reply to your correspondence within a maximum 1 working day of receipt</b>";
                
            ////}
            ////else
            ////{
            ////    lblMessage.Text = "<b>No valid email is entered!</b>";
            ////}
            //string encryptpassword=string.Empty;
            //if (txtOrdernumber.Text.Trim() != "")
            //{
            //    encryptpassword = Microsoft.VisualBasic.Strings.Left(Microsoft.VisualBasic.Strings.StrReverse(txtOrdernumber.Text.Trim()), txtOrdernumber.Text.Trim().Length - 1);
            //}
            //else
            //{
            //    encryptpassword = System.Configuration.ConfigurationManager.AppSettings["EncryptionPassword"].ToString();
            //}
            //string cryptAlgorithm = System.Configuration.ConfigurationManager.AppSettings["CryptAlgorithm"].ToString();
            //string cipherMode = System.Configuration.ConfigurationManager.AppSettings["CipherMode"].ToString();
            //int encryptkeylength =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["KeyLength"].ToString());
            //string encoding = System.Configuration.ConfigurationManager.AppSettings["Encoding"].ToString();
            //string encodingmode = System.Configuration.ConfigurationManager.AppSettings["EncodingMode"].ToString();
            //string ImapUnlockCode = System.Configuration.ConfigurationManager.AppSettings["CryptUnlockCode"].ToString();
            //string EncryptedMessage=CommonFunctions.GetEncryptedMsg(ImapUnlockCode,txtMessage.Text.Trim(),
            //    encryptpassword,
            //    cryptAlgorithm,
            //    cipherMode,
            //    encryptkeylength,
            //    encoding,
            //    encodingmode);
            ////The below lines(87-97) are added on 5th March 2014 for ET implementation
            //CommonBal objCommonBal = new CommonBal();
            //string strOrderId = txtOrdernumber.Text.Trim();
            //string Result = objCommonBal.InsertContactTicket(strOrderId, STATUS_ID);
            //int intTicketId = Convert.ToInt32(Result.Split('|')[0]);
            //string New_GUID = Convert.ToString(Result.Split('|')[1]);
            //strTicket = "http://" + Common.getRootUrl() + "/contactpage.asp?ticketid=" + New_GUID;
            //strEmailMessage = strEmailMessage + "Dear " + txtName.Text + "<br /><br />";
            //strEmailMessage = strEmailMessage + "Thank you for your recent correspondence. It has been forwarded onto our customer services team who will respond shortly if required.<br /><br />";
            //strEmailMessage = strEmailMessage + "Please do not reply to this email as it has been sent from an unmanned email account.<br /><br />";
            //strEmailMessage = strEmailMessage + "Should you wish to contact us again regarding this issue, please visit [TICKETURL].<br /><br />";
            //strEmailMessage = strEmailMessage.Replace("[TICKETURL]", strTicket);
            //EmailTicketInfo objTicketinfo = new EmailTicketInfo();
            //objTicketinfo.MessageFromName = txtName.Text.Trim();
            //objTicketinfo.MessageFromEmail = txtEmail.Text.Trim();
            //objTicketinfo.MessageTo = System.Configuration.ConfigurationManager.AppSettings["ContactUsEmail"].ToString();
            //objTicketinfo.OrderID = txtOrdernumber.Text.Trim();
            //objTicketinfo.SubjectStr = drpQuery.SelectedItem.ToString();
            //objTicketinfo.EncryptedMessage = EncryptedMessage;
            //objTicketinfo.ReasonID = 1;
            //objTicketinfo.SourceID = 1;
            //objTicketinfo.EncryptionType = System.Configuration.ConfigurationManager.AppSettings["EncryptType"].ToString();
            //objTicketinfo.IdTicket = intTicketId;
            //OrdersLogic objOrder = new OrdersLogic();
            //int id=objOrder.InsertContactPageInfo(objTicketinfo);
          
            //if(id>0)
            //{
            //    //The below lines are added to test ET 
            //    ExactTargetEmail ETobj = new ExactTargetEmail();
            //    success = ETobj.SendEmailfromContactUSUsingExactTarget(txtEmail.Text, txtEmail.Text, txtOrdernumber.Text, txtName.Text, strEmailMessage, Convert.ToInt32(objMetaData.SiteId));
            //    if (success)
            //    {
            //        lblMessage.Text = "<b>Thank you!<br/>We will try to come back to you as soon as possible.</b>";
            //        lblpara.Visible = true;
            //        txtName.Text = "";
            //        txtEmail.Text = "";
            //        txtPhone.Text = "";
            //        txtOrdernumber.Text = "";
            //        txtMessage.Text = "";
            //        drpQuery.SelectedIndex = -1;
            //        UpdatePanel1.Update();
            //    }
            //}
            //#endregion
                
        }
        private int GetTicketId()
        {
            int ticketid = 0;
            try{

                OrdersLogic objOrder = new OrdersLogic();
                ticketid = objOrder.InsertTicketInfo(txtOrdernumber.Text.Trim(), 1);

            }
            catch(Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return ticketid;
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
        /// <summary>
        /// This method contains the logic to send mail to support team for any query.
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="ToAddress"></param>
        /// <param name="Subject"></param>
        /// <param name="EmailBody"></param>
        /// <returns></returns>
        public bool SendMail(string fromAddress,string ToAddress,string Subject,string EmailBody)
        {
            try
            {



                MailMessage smail = new MailMessage();
                //Set the body of the mail to HTML
                smail.IsBodyHtml = true;

                smail.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString());//From_Email 


                //if (!string.IsNullOrEmpty(ToAddress))
                //{
                //    ToAddress = ToAddress.Substring(0, ToAddress.Length - 1);
                //    string[] toAddress = ToAddress.Split(';');
                //    foreach (string address in toAddress)
                //    {
                //        smail.To.Add(address);
                //    }
                //}
                smail.To.Add(ToAddress);
                smail.Subject = Subject;
                smail.Body = EmailBody;
                smail.ReplyTo = new MailAddress(fromAddress);
              
                SmtpClient client = new SmtpClient();

                client.Host = System.Configuration.ConfigurationManager.AppSettings["MailServer"].ToString();
                client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPServerPort"].ToString());
                String UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
                String Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
                //Credentials for SMTP Server
                //client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);

                client.Send(smail);
               
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                
                return false;
            }
            return true;
        }
        #region PION Meta Tag
        private void CreateMetaTags()
        {


            HtmlHead head = (HtmlHead)Page.Header;

            HtmlMeta hmdomain = new HtmlMeta();
            hmdomain.Name = "serenata.domain";
            hmdomain.Content = "serenataflowers.com";

            HtmlMeta hmpageName = new HtmlMeta();
            hmpageName.Name = "serenata.pageName";
            hmpageName.Content = "CS:ContactUs";

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "ContactUs";

            HtmlMeta hmsessionID = new HtmlMeta();
            hmsessionID.Name = "serenata.sessionID";
            hmsessionID.Content = Session.SessionID;

            HtmlMeta hmdayOfWeek = new HtmlMeta();
            hmdayOfWeek.Name = "serenata.dayOfWeek";
            hmdayOfWeek.Content = DayOfWeek();

            HtmlMeta hmhourOfDay = new HtmlMeta();
            hmhourOfDay.Name = "serenata.hourOfDay";
            hmhourOfDay.Content = DateTime.Now.Hour.ToString();

            HtmlMeta hmcountry = new HtmlMeta();
            hmcountry.Name = "serenata.country";
            hmcountry.Content = "United Kingdom";

            HtmlMeta hmcurrencyID = new HtmlMeta();
            hmcurrencyID.Name = "serenata.currencyID";
            hmcurrencyID.Content = "1";

            CommonFunctions objCommondetails = new CommonFunctions();

            HtmlMeta hmserverIP = new HtmlMeta();
            hmserverIP.Name = "serenata.serverIP";
            hmserverIP.Content = objCommondetails.GetServerIp();

            HtmlMeta hmbrowserIP = new HtmlMeta();
            hmbrowserIP.Name = "serenata.browserIP";
            hmbrowserIP.Content = objCommondetails.GetUserIp();

            HtmlMeta hmdate = new HtmlMeta();
            hmdate.Name = "serenata.date";
            hmdate.Content = DateTime.Now.Date.ToString("dd/MM/yyyy");

            HtmlMeta hmdatetime = new HtmlMeta();
            hmdatetime.Name = "serenata.datetime";
            hmdatetime.Content = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            HtmlMeta hmnumSessionVariables = new HtmlMeta();
            hmnumSessionVariables.Name = "serenata.numSessionVariables";
            hmnumSessionVariables.Content = "0";

            head.Controls.Add(hmdomain);
            head.Controls.Add(hmpageName);
            head.Controls.Add(hmchannel);
            head.Controls.Add(hmsessionID);
            head.Controls.Add(hmdayOfWeek);
            head.Controls.Add(hmhourOfDay);
            head.Controls.Add(hmcountry);
            head.Controls.Add(hmcurrencyID);
            head.Controls.Add(hmserverIP);
            head.Controls.Add(hmbrowserIP);
            head.Controls.Add(hmdate);
            head.Controls.Add(hmdatetime);
            head.Controls.Add(hmdatetime);
            head.Controls.Add(hmnumSessionVariables);



        }
        private string DayOfWeek()
        {
            string DayofWeek;

            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    DayofWeek = "1";
                    break;
                case "Tuesday":
                    DayofWeek = "2";
                    break;
                case "Wednesday":
                    DayofWeek = "3";
                    break;
                case "Thursday":
                    DayofWeek = "4";
                    break;
                case "Friday":
                    DayofWeek = "5";
                    break;
                case "Saturday":
                    DayofWeek = "6";
                    break;
                case "Sunday":
                    DayofWeek = "7";
                    break;
                default:
                    DayofWeek = "1";
                    break;
            }
            return DayofWeek;
        }
        #endregion
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