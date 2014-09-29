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
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

namespace Serenataflowers.Controls
{
    public partial class PasswordReminder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string GenerateResetPwdLink(int iCustId)
        {
            string strLink = string.Empty;
            try
            {
                Encryption objEncryption = new Encryption();
                string strEncryptCid = objEncryption.GetAesEncryptionString(iCustId.ToString());
                strLink = ConfigurationManager.AppSettings["ForgetPwdLinkUrl"] + strEncryptCid;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return strLink;
        }

        public bool SendMail(string ToAddress, string Subject, string EmailBody)
        {
            try
            {
                MailMessage smail = new MailMessage();
                //Set the body of the mail to HTML
                smail.IsBodyHtml = true;
                smail.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString());//From_Email 
                //smail.From = new MailAddress(fromAddress);
                smail.To.Add(ToAddress);
                smail.Subject = Subject;
                smail.Body = EmailBody;

                SmtpClient client = new SmtpClient();
                client.Host = System.Configuration.ConfigurationManager.AppSettings["MailServer"].ToString();
                client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPServerPort"].ToString());
                String UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
                String Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
                //Credentials for SMTP Server
                client.EnableSsl = true;

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

        protected void GeneratePassword_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            bool Ismobile = false;
            bool IsValidEmail = false;
            bool IsMailSent = false;
            try
            {
                CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                string custmobile = CustomerSMSNumber.Value.Replace("+", "");
                Ismobile = isNumeric(custmobile.Trim(), System.Globalization.NumberStyles.Number);
                if (!string.IsNullOrEmpty(CustomerReminderPassword.Value))
                {
                    customerId = objCustomerDetails.CheckCustomer(CustomerReminderPassword.Value.Trim());
                    IsValidEmail = true;
                }
                else if (Ismobile == true)
                {
                    customerId = objCustomerDetails.CheckCustomer(custmobile.Trim(), true);
                }

                if (customerId > 0)
                {
                    PasswordReminderInfo objPasswordReminderInfo = new PasswordReminderInfo();
                    objPasswordReminderInfo = objCustomerDetails.GetCustomerDetailsByCustomerID(customerId);
                    if (objPasswordReminderInfo != null)
                    {
                        if (!string.IsNullOrEmpty(CustomerReminderPassword.Value) && IsValidEmail == true)
                        {
                            System.Text.StringBuilder htmlbody = new System.Text.StringBuilder();
                            htmlbody.Append("Dear " + objPasswordReminderInfo.Name + ",<br /><br />Please visit link below to create a new password<br /><br />");
                            htmlbody.Append("Reset Password Link: " + GenerateResetPwdLink(customerId) + " <br/><br/>");
                            htmlbody.Append("Please do not reply to this email as it has been sent from an unattended email account. If you wish to contact us, please use the contact form at http://www.serenataflowers.com/contactUs.asp.<br /><br />");
                            htmlbody.Append("Kind Regards,<br />");
                            htmlbody.Append("Serenata Flowers");

                            EmailInfo objEmail = new EmailInfo();
                            objEmail.From = ConfigurationManager.AppSettings["ForgetPasswordFromAddress"];
                            objEmail.HTMLBody = htmlbody.ToString();
                            objEmail.Subject = "Requested information";
                            objEmail.To = CustomerReminderPassword.Value.Trim();
                            SmtpEmail objSMTP = new SmtpEmail();
                            //send_email(ConfigurationManager.AppSettings["ForgetPasswordFromAddress"], CustomerReminderPassword.Value.Trim(), "Requested information", htmlbody.ToString());
                            //IsMailSent = objSMTP.SendMailWithoutSMTP(objEmail, ConfigurationManager.AppSettings["ImapUnlockCode"]);
                            //IsMailSent = SendMail(CustomerReminderPassword.Value.Trim(), "Requested information", htmlbody.ToString());

                            //IsMailSent = objSMTP.SendMailWithoutSMTP(objEmail, ConfigurationManager.AppSettings["ImapUnlockCode"]);
                            //IsMailSent = send_email(ConfigurationManager.AppSettings["ForgetPasswordFromAddress"], CustomerReminderPassword.Value.Trim(), "Requested information", htmlbody.ToString());

                            //SFMobileLog.Error(new Exception("Test --- " + IsMailSent));
                            //divForgetPassword.Style.Add("display", "none");
                            //h4Password.Style.Add("display", "block");

                            ExactTargetEmail objSendEtEmail = new ExactTargetEmail();
                            Common objCommon = new Common();
                            IsMailSent = objSendEtEmail.SendEmailUsingExactTarget(CustomerReminderPassword.Value.Trim(), objPasswordReminderInfo.Name, "", GenerateResetPwdLink(customerId), objCommon.GetSiteId());

                            if (IsMailSent)
                            {
                                lblErrorInfo.Text = "We have now sent you an email with a link where you can reset your password.";
                                ScriptManager.RegisterClientScriptBlock(GeneratePassword, GeneratePassword.GetType(), "test123", "SentEmail();", true);
                            }
                            //SFMobileLog.Error(new Exception("Test Complete --- " + IsMailSent));
                        }
                        if (!string.IsNullOrEmpty(CustomerSMSNumber.Value.Trim()) && Ismobile == true && IsMailSent == false)
                        {
                            FuelAPISMS objFual = new FuelAPISMS();
                            string strMessageText = ConfigurationManager.AppSettings["MessageText"];
                            strMessageText = strMessageText.Replace("[link]", GenerateResetPwdLink(customerId));
                            string response = objFual.ProcessFualApiForSms(objPasswordReminderInfo.Mobile, strMessageText);
                            if (response == "success")
                            {
                                //divForgetPassword.Style.Add("display", "none");
                                //h4Password.Style.Add("display", "block");
                                lblErrorInfo.Text = "We have now sent you a sms with a link where you can reset your password.";
                                ScriptManager.RegisterClientScriptBlock(GeneratePassword, GeneratePassword.GetType(), "test123", "SentSMS();", true);
                            }
                        }
                    }
                    else
                    {
                        //divForgetPassword.Style.Add("display", "block");
                        h4Password.Style.Add("display", "block");
                        h4Password.InnerText = "Sorry, we haven't been able to match your email/mobile number with a previous order.";
                    }
                }
                else
                {
                    //divForgetPassword.Style.Add("display", "block");
                    h4Password.Style.Add("display", "block");
                    h4Password.InnerText = "Sorry, we havent been able to match your email/mobile number with a previous order.";
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private bool send_email(string fromAddress, string ToAddress, string Subject, string EmailBody)
        {
            try
            {
                CDO.Message oMsg = new CDO.Message();
                CDO.IConfiguration iConfg;
                iConfg = oMsg.Configuration;
                ADODB.Fields oFields;
                oFields = iConfg.Fields;
                ADODB.Field oField = oFields["http://schemas.microsoft.com/cdo/configuration/sendusing"];
                oFields.Update();
                oMsg.Subject = Subject;
                oMsg.From = fromAddress;
                oMsg.To = ToAddress;
                oMsg.HTMLBody = EmailBody;
                oMsg.Send();
                return true;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex.Message);
                return false;
            }
        }

    }
}