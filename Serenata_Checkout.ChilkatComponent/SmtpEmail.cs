using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
namespace Serenata_Checkout.ChilkatComponent
{
    public  class SmtpEmail
    {
        Chilkat.MailMan objMailman;

        public SmtpEmail()
        {
            objMailman = new Chilkat.MailMan();
        }

        public bool SendMailWithoutSMTP(EmailInfo objEmailInfo, string str_LicenseKey)
        {
          
            //  Any string argument automatically begins the 30-day trial.
            bool bool_Success;
            try
            {
                bool_Success = objMailman.UnlockComponent(str_LicenseKey);
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }
                //  Do a DNS MX lookup for the recipient's mail server.
                string smtpHostname;
                smtpHostname = objMailman.MxLookup(objEmailInfo.To);
                if (smtpHostname == null)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }

                //  Set the SMTP server.
                objMailman.SmtpHost = smtpHostname;

                //  Create a new email object
                Chilkat.Email objEmail = new Chilkat.Email();
                objEmail.Subject = objEmailInfo.Subject;
                objEmail.SetHtmlBody(objEmailInfo.HTMLBody);
                objEmail.From = objEmailInfo.From;
                objEmail.AddTo(objEmailInfo.FriendlyName, objEmailInfo.To);
                bool_Success = objMailman.SendEmail(objEmail);
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }
                bool_Success = objMailman.CloseSmtpConnection();
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return true;
        }
        public bool SendSMTPMail(EmailInfo objResMailInfo, SmtpServerInfo objSmtpServer, string str_LicenseKey)
        {
            bool bool_Success;
            try
            {
                bool_Success = objMailman.UnlockComponent(str_LicenseKey);
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }
                //  Set the SMTP server.
                objMailman.SmtpHost = objSmtpServer.SmtpHost;
                //  Set the SMTP login/password (if required)
                objMailman.SmtpUsername = objSmtpServer.SmtpUsername;
                objMailman.SmtpPassword = objSmtpServer.SmtpPassword;

                objMailman.SmtpSsl = true;
                objMailman.SmtpPort = 587;
                objMailman.StartTLS = true;

                //  Create a new email object
                Chilkat.Email objEmail = new Chilkat.Email();
                objEmail.Subject = objResMailInfo.Subject;
                objEmail.Body = objResMailInfo.HTMLBody;
                objEmail.From = objResMailInfo.From;
                objEmail.AddTo(objResMailInfo.FriendlyName, objResMailInfo.To);
                bool_Success = objMailman.SendEmail(objEmail);
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(objMailman.LastErrorText));
                    return false;
                }
                bool_Success = objMailman.CloseSmtpConnection();
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception("Connection to SMTP server not closed cleanly."));
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return true;
        }
    }
}
