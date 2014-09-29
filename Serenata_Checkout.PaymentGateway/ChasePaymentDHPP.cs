using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Data;
using System.Configuration;
using SFMobile.Exceptions;
using Serenata_Checkout.Dto;
using System.Web;
namespace Serenata_Checkout.PaymentGateway
{
    public class ChasePaymentDHPP
    {
        public XmlDocument DoPrepare(string trasactionSecret, XmlDocument xmlDoc)
        {
            //string host = "https://test.ppipe.net/hpp/auth/DoPrepare";
            //string host = "https://test.ppipe.net/hpp/gate/DoPrepare";
            string host = ConfigurationManager.AppSettings["DoPrepareAuthRequestURL"];

        
         
            HttpWebRequest httpWebRequest = null;
            string BasicAuthentication;
            XmlDocument responseDocument = new XmlDocument();
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(host);
               
            }
            catch (UriFormatException ex)
            {
                SFMobileLog.Error(ex);
            }
            httpWebRequest.Method = "POST";

            string basicUserName = ConfigurationManager.AppSettings["BasicAuthUserName"];
            string basicPassword = ConfigurationManager.AppSettings["BasicAuthPassword"];

            byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(basicUserName + ":" + basicPassword);
            Base64Encoder myEncoder = new Base64Encoder(data);
            StringBuilder sb = new StringBuilder();
            sb.Append(myEncoder.GetEncoded());
            BasicAuthentication = sb.ToString();
            httpWebRequest.Headers["Authorization"] = "Basic " + BasicAuthentication;

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";//"application/xml;charset=UTF-8";
            httpWebRequest.Accept = "application/x-www-form-urlencoded";//"application/xml";//

            string poststring = String.Format("transactionSecret={0}&requestXML={1}", trasactionSecret, xmlDoc.OuterXml);
            // Content length in bytes 
            UTF8Encoding encoding = new UTF8Encoding();
            //string xmlString = xmlDoc.OuterXml;
            //string parameter = "";

            //parameter += trasactionSecret; // Trasaction SECRET
            //parameter += xmlString;


            byte[] xmlBytes = encoding.GetBytes(poststring);
            httpWebRequest.ContentLength = xmlBytes.Length;

            Stream str = null;
            try
            {
                str = httpWebRequest.GetRequestStream();
                str.Write(xmlBytes, 0, xmlBytes.Length);
                str.Close();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
           
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
           
            
            if (httpWebResponse.StatusCode.ToString() == "OK")
            {
                string result = "";
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    try
                    {
                        result = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    catch (Exception ex)
                    {
                        SFMobileLog.Error(ex);
                    }
                } // We get the HTTP Headers to start with, so // knock them off before creating the XML Document 
                int startOfXML = result.IndexOf("<?xml");
                if (startOfXML != -1)
                {
                    string xmlResponse = result.Substring(startOfXML);
                    // Now load the string into an XmlDocument

                    responseDocument.LoadXml(xmlResponse);
                }
                return responseDocument;
            }
            else
            {
                responseDocument = null;
                return responseDocument;
            }
        }
        public XmlDocument DoPay(string Token)
        {
            //string host = "https://test.ppipe.net/hpp/auth/DoPrepare";
            //string host = "https://test.ppipe.net/hpp/gate/DoPrepare";
            string host = ConfigurationManager.AppSettings["DoPayRequestURL"];



            HttpWebRequest httpWebRequest = null;
            string BasicAuthentication;
            XmlDocument responseDocument = new XmlDocument();
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(host);

            }
            catch (UriFormatException ex)
            {
                SFMobileLog.Error(ex);
            }
            httpWebRequest.Method = "POST";

            string basicUserName = ConfigurationManager.AppSettings["BasicAuthUserName"];
            string basicPassword = ConfigurationManager.AppSettings["BasicAuthPassword"];

            byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(basicUserName + ":" + basicPassword);
            Base64Encoder myEncoder = new Base64Encoder(data);
            StringBuilder sb = new StringBuilder();
            sb.Append(myEncoder.GetEncoded());
            BasicAuthentication = sb.ToString();
            httpWebRequest.Headers["Authorization"] = "Basic " + BasicAuthentication;

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";//"application/xml;charset=UTF-8"
            httpWebRequest.Accept = "application/x-www-form-urlencoded";

            string poststring = ""; //String.Format("transactionSecret={0}&requestXML={1}", xmlDoc.OuterXml);
            // Content length in bytes 
            UTF8Encoding encoding = new UTF8Encoding();
            //string xmlString = xmlDoc.OuterXml;
            //string parameter = "";

            //parameter += trasactionSecret; // Trasaction SECRET
            //parameter += xmlString;


            byte[] xmlBytes = encoding.GetBytes(poststring);
            httpWebRequest.ContentLength = xmlBytes.Length;

            Stream str = null;
            try
            {
                str = httpWebRequest.GetRequestStream();
                str.Write(xmlBytes, 0, xmlBytes.Length);
                str.Close();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();


            if (httpWebResponse.StatusCode.ToString() == "OK")
            {
                string result = "";
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    try
                    {
                        result = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    catch (Exception ex)
                    {
                        SFMobileLog.Error(ex);
                    }
                } // We get the HTTP Headers to start with, so // knock them off before creating the XML Document 
                int startOfXML = result.IndexOf("<?xml");
                string xmlResponse = result.Substring(startOfXML);
                // Now load the string into an XmlDocument

                responseDocument.LoadXml(xmlResponse);
                return responseDocument;
            }
            else
            {
                responseDocument = null;
                return responseDocument;
            }
        }
        public XmlDocument GetStatus(string trasactionSecret,string Token)
        {
            string host = ConfigurationManager.AppSettings["GetChasePaymentStatusURL"] + trasactionSecret + "&token=" + Token;

            HttpWebRequest httpWebRequest = null;           
            XmlDocument responseDocument = new XmlDocument();
            string BasicAuthentication;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(host);

            }
            catch (UriFormatException ex)
            {
                SFMobileLog.Error(ex);
                responseDocument = null;
            }
            httpWebRequest.Method = "POST";

            string basicUserName = ConfigurationManager.AppSettings["BasicAuthUserName"];
            string basicPassword = ConfigurationManager.AppSettings["BasicAuthPassword"];

            byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(basicUserName + ":" + basicPassword);
            Base64Encoder myEncoder = new Base64Encoder(data);
            StringBuilder sb = new StringBuilder();
            sb.Append(myEncoder.GetEncoded());
            BasicAuthentication = sb.ToString();
            httpWebRequest.Headers["Authorization"] = "Basic " + BasicAuthentication;
         
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

           
                 
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string result = "";
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    try
                    {
                        result = streamReader.ReadToEnd();
                        streamReader.Close();
                        string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                        StreamWriter fp = new StreamWriter(filePath + "\\" + "getStatus.txt", true);
                        fp.Write(result + host);
                        fp.Close();
                    }
                    catch (Exception ex)
                    {
                        string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                        StreamWriter fp = new StreamWriter(filePath + "\\" + "getStatus.txt", true);
                        fp.Write(ex + host);
                        fp.Close();
                        responseDocument = null;
                    }
                }
                // result = result.Replace("+", "").Replace("&", "");
                int startOfXML = result.IndexOf("<?xml");
                string xmlResponse = result.Substring(startOfXML);
                // Now load the string into an XmlDocument

                responseDocument.LoadXml(xmlResponse);
                return responseDocument;
            }
            catch (Exception ex)           
            {
                string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                StreamWriter fp = new StreamWriter(filePath + "\\" + "getStatus.txt", true);
                fp.Write(ex);
                fp.Close();
                responseDocument = null;
            }

            return responseDocument;
        }

        public XmlDocument ChasePaymentXMLRequest(string trasactionSecret,DHHPInfo objDHHPDetails,CustomerInfo objCustomerInfo)
        {
            XmlDocument doc = new XmlDocument();
            try
            {

                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                // Define the root (top-level) 
                XmlElement DoPrpepareRequest = doc.CreateElement("Request");
                DoPrpepareRequest.SetAttribute("version", "1.0");
                doc.AppendChild(DoPrpepareRequest); // Add some new container elements 


                // Add a "leaf" element with value and single attribute 
                XmlElement Frontend = doc.CreateElement("Frontend"); 
                DoPrpepareRequest.AppendChild(Frontend);

                XmlElement Form = doc.CreateElement("Form");
                Frontend.AppendChild(Form);

                XmlElement ElementAddressHeader = doc.CreateElement("Element");
                ElementAddressHeader.SetAttribute("forClass", "custom_paymentHeader");
                ElementAddressHeader.SetAttribute("label", "Enter your card details");
                Form.AppendChild(ElementAddressHeader);

                //XmlElement ElementpaymentHeader = doc.CreateElement("Customer");
                //ElementpaymentHeader.SetAttribute("mutable", "true");
                //ElementpaymentHeader.SetAttribute("shippingActive", "true");
                //ElementpaymentHeader.SetAttribute("shippingAsBillingDefault", "true");
                //Frontend.AppendChild(ElementpaymentHeader);


                //XmlElement ElementbillingAddressCompany = doc.CreateElement("Address");               
                //ElementpaymentHeader.AppendChild(ElementbillingAddressCompany);

                //XmlElement ElementbillingAddressStreet = doc.CreateElement("Zip");
                //ElementbillingAddressStreet.SetAttribute("mandatory", "true");
                //ElementbillingAddressCompany.AppendChild(ElementbillingAddressStreet);



                XmlElement ElementRedirectURL = doc.CreateElement("RedirectURL");
                string strdomain = GetDomainName();
                string dhhpResponseURL = string.Empty;
                if (strdomain != "")
                {
                    dhhpResponseURL = "https://" + strdomain + "/" + ConfigurationManager.AppSettings["ChaseRedirectURL"] + "?s=" + objDHHPDetails.EncryptedOrderID;
                }
                else
                {
                    dhhpResponseURL = ConfigurationManager.AppSettings["ChaseResponseURL"] + "?s=" + objDHHPDetails.EncryptedOrderID;
                }
                XmlText url = doc.CreateTextNode(dhhpResponseURL);
                ElementRedirectURL.AppendChild(url);
                Frontend.AppendChild(ElementRedirectURL);

                XmlElement ElementCSSPath = doc.CreateElement("CSSPath");
               //ElementCSSPath.SetAttribute("basedOn", "BASIC");
                XmlText csspath = doc.CreateTextNode(ConfigurationManager.AppSettings["DHHPCssPath"]);
                ElementCSSPath.AppendChild(csspath);
                Frontend.AppendChild(ElementCSSPath);

                //XmlElement ElementPaymentMethodsFronEnd = doc.CreateElement("PaymentMethods");
                //Frontend.AppendChild(ElementPaymentMethodsFronEnd);

                //XmlElement ElementOrderFronEnd = doc.CreateElement("Order");
                //XmlText OrderCards = doc.CreateTextNode("VCREDIT,MASTER");
                //ElementOrderFronEnd.AppendChild(OrderCards);               
                //ElementPaymentMethodsFronEnd.AppendChild(ElementOrderFronEnd);

                XmlElement ElementTransaction = doc.CreateElement("Transaction");
                ElementTransaction.SetAttribute("mode", ConfigurationManager.AppSettings["TransactionMode"]);
                DoPrpepareRequest.AppendChild(ElementTransaction);

                XmlElement ElementIdentification = doc.CreateElement("Identification");
                ElementTransaction.AppendChild(ElementIdentification);

                XmlElement ElementOrderID = doc.CreateElement("OrderID");
                XmlText OrderIDtext = doc.CreateTextNode(objDHHPDetails.OrderID);
                ElementOrderID.AppendChild(OrderIDtext);
                ElementIdentification.AppendChild(ElementOrderID);

                XmlElement ElementUUID = doc.CreateElement("UUID");
                XmlText UUIDtext = doc.CreateTextNode(objDHHPDetails.UUID);
                ElementUUID.AppendChild(UUIDtext);  
                ElementIdentification.AppendChild(ElementUUID);

              





                XmlElement ElementPaymentMethodsTransaction = doc.CreateElement("PaymentMethods");
                ElementTransaction.AppendChild(ElementPaymentMethodsTransaction);


                XmlElement ElementPaymentMethodTransaction = doc.CreateElement("PaymentMethod");
                ElementPaymentMethodTransaction.SetAttribute("subTypes", ConfigurationManager.AppSettings["PaymentCards"]);
                ElementPaymentMethodsTransaction.AppendChild(ElementPaymentMethodTransaction);

                XmlElement ElementMerchantAccountTransaction = doc.CreateElement("MerchantAccount");
                ElementMerchantAccountTransaction.SetAttribute("type", "CHASEPAYMENTECH");
                ElementPaymentMethodTransaction.AppendChild(ElementMerchantAccountTransaction);

                if (ConfigurationManager.AppSettings["EnableRetryLogic"] == "true")
                {

                    XmlElement ElementSecretTransaction = doc.CreateElement("Secret");
                    XmlText Secrettext = doc.CreateTextNode("true");
                    ElementSecretTransaction.AppendChild(Secrettext);
                    ElementMerchantAccountTransaction.AppendChild(ElementSecretTransaction);
                }


                XmlElement ElementMerchantID = doc.CreateElement("MerchantID");
                XmlText MerchantIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["MerchantID"]);
                ElementMerchantID.AppendChild(MerchantIDtext);
                ElementMerchantAccountTransaction.AppendChild(ElementMerchantID);

                XmlElement ElementTerminalID = doc.CreateElement("TerminalID");
                XmlText TerminalIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["TerminalID"]);
                ElementTerminalID.AppendChild(TerminalIDtext);
                ElementMerchantAccountTransaction.AppendChild(ElementTerminalID);

                XmlElement ElementMerchantName = doc.CreateElement("MerchantName");
                XmlText MerchantNametext = doc.CreateTextNode(ConfigurationManager.AppSettings["MerchantName"]);
                ElementMerchantName.AppendChild(MerchantNametext);
                ElementMerchantAccountTransaction.AppendChild(ElementMerchantName);

                XmlElement ElementUsername = doc.CreateElement("Username");
                XmlText Usernametext = doc.CreateTextNode(ConfigurationManager.AppSettings["DHPPUsername"]);
                ElementUsername.AppendChild(Usernametext);
                ElementMerchantAccountTransaction.AppendChild(ElementUsername);

                XmlElement ElementPassword = doc.CreateElement("Password");
                XmlText Passwordtext = doc.CreateTextNode(ConfigurationManager.AppSettings["DHPPPassword"]);
                ElementPassword.AppendChild(Passwordtext);
                ElementMerchantAccountTransaction.AppendChild(ElementPassword);

                XmlElement ElementTransactionCategory = doc.CreateElement("TransactionCategory");
                XmlText TransactionCategorytext = doc.CreateTextNode("ECOMMERCE");
                ElementTransactionCategory.AppendChild(TransactionCategorytext);
                ElementMerchantAccountTransaction.AppendChild(ElementTransactionCategory);

                XmlElement ElementRecurringCode = doc.CreateElement("RecurringCode");
                XmlText RecurringCodetext = doc.CreateTextNode("NONE");
                ElementRecurringCode.AppendChild(RecurringCodetext);
                ElementMerchantAccountTransaction.AppendChild(ElementRecurringCode);

                XmlElement ElementCVVValidation = doc.CreateElement("CVVValidation");
                ElementCVVValidation.SetAttribute("instruction", "IGNORE");
                ElementMerchantAccountTransaction.AppendChild(ElementCVVValidation);

                XmlElement ElementAVS = doc.CreateElement("AVS");
                ElementAVS.SetAttribute("instruction", "IGNORE");
                ElementMerchantAccountTransaction.AppendChild(ElementAVS);

                //******************3D Secure******************************//


                ////XmlElement ElementThreeDSecure = doc.CreateElement("ThreeDSecure");
                ////ElementMerchantAccountTransaction.AppendChild(ElementThreeDSecure);

                ////XmlElement ElementThreeDSystem = doc.CreateElement("ThreeDSystem");
                ////ElementThreeDSystem.SetAttribute("type", "VerifiedByVISA");
                ////ElementThreeDSecure.AppendChild(ElementThreeDSystem);

                ////XmlElement ElementAcquirerBIN = doc.CreateElement("AcquirerBIN");
                ////XmlText ElementAcquirerBINtext = doc.CreateTextNode(ConfigurationManager.AppSettings["VisaAcquirerBIN"]);
                ////ElementAcquirerBIN.AppendChild(ElementAcquirerBINtext);
                ////ElementThreeDSystem.AppendChild(ElementAcquirerBIN);

                ////XmlElement Element3DSecureMerchantID = doc.CreateElement("MerchantID");
                ////XmlText Element3DSecureMerchantIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["3DSecureMerchantID"]);
                ////Element3DSecureMerchantID.AppendChild(Element3DSecureMerchantIDtext);
                ////ElementThreeDSystem.AppendChild(Element3DSecureMerchantID);

                ////XmlElement Element3DSystem = doc.CreateElement("ThreeDSystem");
                ////Element3DSystem.SetAttribute("type", "MastercardSecureCode");
                ////ElementThreeDSecure.AppendChild(Element3DSystem);

                ////XmlElement Element3DAcquirerBIN = doc.CreateElement("AcquirerBIN");
                ////XmlText Element3DAcquirerBINtext = doc.CreateTextNode(ConfigurationManager.AppSettings["MasterCardAcquirerBIN"]);
                ////Element3DAcquirerBIN.AppendChild(Element3DAcquirerBINtext);
                ////Element3DSystem.AppendChild(Element3DAcquirerBIN);

                ////XmlElement Element3DMerchantID = doc.CreateElement("MerchantID");
                ////XmlText Element3DMerchantIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["3DSecureMerchantID"]);
                ////Element3DMerchantID.AppendChild(Element3DMerchantIDtext);
                ////Element3DSystem.AppendChild(Element3DMerchantID);

                ////XmlElement Element3DMerchantName = doc.CreateElement("MerchantName");
                ////XmlText Element3DMerchantNametext = doc.CreateTextNode(ConfigurationManager.AppSettings["MerchantName"]);
                ////Element3DMerchantName.AppendChild(Element3DMerchantNametext);
                ////ElementThreeDSecure.AppendChild(Element3DMerchantName);

                ////XmlElement Element3DMerchantUrl = doc.CreateElement("MerchantUrl");
                ////XmlText Element3DMerchantUrltext = doc.CreateTextNode(ConfigurationManager.AppSettings["ChaseRedirectURL"] + "?s=" + objDHHPDetails.EncryptedOrderID);
                ////Element3DMerchantUrl.AppendChild(Element3DMerchantUrltext);
                ////ElementThreeDSecure.AppendChild(Element3DMerchantUrl);

                ////XmlElement Element3DMerchantCountry = doc.CreateElement("MerchantCountry");
                ////XmlText Element3DMerchantCountrytext = doc.CreateTextNode("826");
                ////Element3DMerchantCountry.AppendChild(Element3DMerchantCountrytext);
                ////ElementThreeDSecure.AppendChild(Element3DMerchantCountry);

                //******************3D Secure******************************//



                XmlElement ElementPayment = doc.CreateElement("Payment");
                ElementPayment.SetAttribute("type", ConfigurationManager.AppSettings["PaymentType"]);
                ElementTransaction.AppendChild(ElementPayment);

                XmlElement ElementAmount = doc.CreateElement("Amount");
                XmlText Amounttext = doc.CreateTextNode(Convert.ToString(String.Format("{0:0.00}", objDHHPDetails.Amount)));
              
                //XmlText Amounttext = doc.CreateTextNode("00.232011");  
                ElementAmount.AppendChild(Amounttext);
                ElementPayment.AppendChild(ElementAmount);

                XmlElement ElementCurrency = doc.CreateElement("Currency");
                XmlText Currencytext = doc.CreateTextNode(objDHHPDetails.Currency);
                ElementCurrency.AppendChild(Currencytext);
                ElementPayment.AppendChild(ElementCurrency);

                //Customer profile

                XmlElement ElementCustomer = doc.CreateElement("Customer");
                ElementTransaction.AppendChild(ElementCustomer);

                XmlElement ElementName = doc.CreateElement("Name");              
                ElementCustomer.AppendChild(ElementName);

                XmlElement ElementFamily = doc.CreateElement("Given");
                XmlText Familytext = doc.CreateTextNode(objCustomerInfo.FirstName);
                ElementFamily.AppendChild(Familytext);
                ElementName.AppendChild(ElementFamily);

                XmlElement ElementGiven = doc.CreateElement("Family");
                XmlText Giventext = doc.CreateTextNode(objCustomerInfo.LastName);
                ElementGiven.AppendChild(Giventext);
                ElementName.AppendChild(ElementGiven);

                XmlElement ElementContact = doc.CreateElement("Contact");
                ElementCustomer.AppendChild(ElementContact);

                //XmlElement ElementIp = doc.CreateElement("Ip");
                //XmlText Iptext = doc.CreateTextNode(GetClientIp());
                //ElementIp.AppendChild(Iptext);
                //ElementContact.AppendChild(ElementIp);

                XmlElement ElementEmail = doc.CreateElement("Email");
                XmlText Emailtext = doc.CreateTextNode(objCustomerInfo.Email);
                ElementEmail.AppendChild(Emailtext);
                ElementContact.AppendChild(ElementEmail);

                XmlElement ElementMobile = doc.CreateElement("Mobile");
                XmlText Mobiletext = doc.CreateTextNode(objCustomerInfo.UKMobile);
                ElementMobile.AppendChild(Mobiletext);
                ElementContact.AppendChild(ElementMobile);

                XmlElement ElementAddress = doc.CreateElement("Address");
                ElementCustomer.AppendChild(ElementAddress);

                XmlElement ElementCountry = doc.CreateElement("Country");
                XmlText Countrytext = doc.CreateTextNode(objCustomerInfo.ISOCountry);
                ElementCountry.AppendChild(Countrytext);
                ElementAddress.AppendChild(ElementCountry);

                XmlElement ElementCity = doc.CreateElement("City");
                XmlText Citytext = doc.CreateTextNode(objCustomerInfo.Town);
                ElementCity.AppendChild(Citytext);
                ElementAddress.AppendChild(ElementCity);
            

                XmlElement ElementStreet = doc.CreateElement("Street");
                 string custstreet=string.Empty;
                 if (objCustomerInfo.Street != "")
                 {
                     custstreet = objCustomerInfo.HouseNo + " " + objCustomerInfo.Street;
                     custstreet = custstreet.Replace("&", ",");
                 }
                 else
                 {
                     custstreet = objCustomerInfo.HouseNo;
                     custstreet = custstreet.Replace("&", ",");
                 }
                 XmlText Streettext = doc.CreateTextNode(custstreet);
                //XmlText Streettext = doc.CreateTextNode("1 seren street");
                ElementStreet.AppendChild(Streettext);
                ElementAddress.AppendChild(ElementStreet);

                XmlElement ElementZip = doc.CreateElement("Zip");
                XmlText Ziptext = doc.CreateTextNode(objCustomerInfo.PostCode);
                ElementZip.AppendChild(Ziptext);
                ElementAddress.AppendChild(ElementZip);


                XmlElement ElementParameters = doc.CreateElement("Parameters");
                ElementTransaction.AppendChild(ElementParameters);

                XmlElement ElementParameter = doc.CreateElement("Parameter");
                ElementParameter.SetAttribute("name", "SDProductDescription");
                XmlText Parametertext = doc.CreateTextNode(objDHHPDetails.ProductDescription);
                ElementParameter.AppendChild(Parametertext);
                ElementParameters.AppendChild(ElementParameter);

                //XmlElement ElementParameter1 = doc.CreateElement("Parameter");
                //ElementParameter1.SetAttribute("name", "SDMerchantEmail");
                //XmlText Parameter1text = doc.CreateTextNode(objDHHPDetails.MerchantEmail);
                //ElementParameter1.AppendChild(Parameter1text);
                //ElementParameters.AppendChild(ElementParameter1);


                return doc;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return doc;
        }

        public string GetToken(XmlDocument xmlDoc)
        {
            string token = string.Empty;
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.LoadXml(xmlDoc.OuterXml); // suppose that myXmlString contains "<Names>...</Names>"

                XmlNodeList xnList = xml.SelectNodes("/Response/Frontend");
                foreach (XmlNode xn in xnList)
                {
                    token = xn["Token"].InnerText;
                    
                }

            }
            catch (Exception ex)
            { 
            
            
            }
            return token;
        }
        public string GetClientIp()
        {
            string ip = string.Empty;
            try
            {

                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    string[] ipArray = ip.Split(',');
                }
                return ip;
            }
            catch (Exception ex)
            {
                throw;
            }

            return ip;
        }

        public XmlDocument ChasePaymentTestXMLRequest(string trasactionSecret, DHHPInfo objDHHPDetails, CustomerInfo objCustomerInfo)
        {
            XmlDocument doc = new XmlDocument();
            try
            {

                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                // Define the root (top-level) 
                XmlElement DoPrpepareRequest = doc.CreateElement("Request");
                DoPrpepareRequest.SetAttribute("version", "1.0");
                doc.AppendChild(DoPrpepareRequest); // Add some new container elements 


                // Add a "leaf" element with value and single attribute 
                XmlElement Frontend = doc.CreateElement("Frontend");
                DoPrpepareRequest.AppendChild(Frontend);

                XmlElement Form = doc.CreateElement("Form");
                Frontend.AppendChild(Form);

                XmlElement ElementAddressHeader = doc.CreateElement("Element");
                ElementAddressHeader.SetAttribute("forClass", "custom_paymentHeader");
                ElementAddressHeader.SetAttribute("label", "Enter your card details");
                Form.AppendChild(ElementAddressHeader);

                //XmlElement ElementpaymentHeader = doc.CreateElement("Customer");
                //ElementpaymentHeader.SetAttribute("mutable", "true");
                //ElementpaymentHeader.SetAttribute("shippingActive", "true");
                //ElementpaymentHeader.SetAttribute("shippingAsBillingDefault", "true");
                //Frontend.AppendChild(ElementpaymentHeader);


                //XmlElement ElementbillingAddressCompany = doc.CreateElement("Address");               
                //ElementpaymentHeader.AppendChild(ElementbillingAddressCompany);

                //XmlElement ElementbillingAddressStreet = doc.CreateElement("Zip");
                //ElementbillingAddressStreet.SetAttribute("mandatory", "true");
                //ElementbillingAddressCompany.AppendChild(ElementbillingAddressStreet);



                XmlElement ElementRedirectURL = doc.CreateElement("RedirectURL");
                XmlText url = doc.CreateTextNode(ConfigurationManager.AppSettings["ChaseRedirectURL"] + "?s=" + objDHHPDetails.EncryptedOrderID);
                ElementRedirectURL.AppendChild(url);
                Frontend.AppendChild(ElementRedirectURL);

                XmlElement ElementCSSPath = doc.CreateElement("CSSPath");
                //ElementCSSPath.SetAttribute("basedOn", "BASIC");
                XmlText csspath = doc.CreateTextNode(ConfigurationManager.AppSettings["DHHPCssPath"]);
                ElementCSSPath.AppendChild(csspath);
                Frontend.AppendChild(ElementCSSPath);

                //XmlElement ElementPaymentMethodsFronEnd = doc.CreateElement("PaymentMethods");
                //Frontend.AppendChild(ElementPaymentMethodsFronEnd);

                //XmlElement ElementOrderFronEnd = doc.CreateElement("Order");
                //XmlText OrderCards = doc.CreateTextNode("VCREDIT,MASTER");
                //ElementOrderFronEnd.AppendChild(OrderCards);               
                //ElementPaymentMethodsFronEnd.AppendChild(ElementOrderFronEnd);

                XmlElement ElementTransaction = doc.CreateElement("Transaction");
                ElementTransaction.SetAttribute("mode", ConfigurationManager.AppSettings["TransactionMode"]);
                DoPrpepareRequest.AppendChild(ElementTransaction);

                XmlElement ElementIdentification = doc.CreateElement("Identification");
                ElementTransaction.AppendChild(ElementIdentification);

                XmlElement ElementOrderID = doc.CreateElement("OrderID");
                XmlText OrderIDtext = doc.CreateTextNode("787878");
                ElementOrderID.AppendChild(OrderIDtext);
                ElementIdentification.AppendChild(ElementOrderID);

                XmlElement ElementUUID = doc.CreateElement("UUID");
                XmlText UUIDtext = doc.CreateTextNode(objDHHPDetails.UUID);
                ElementUUID.AppendChild(UUIDtext);
                ElementIdentification.AppendChild(ElementUUID);







                XmlElement ElementPaymentMethodsTransaction = doc.CreateElement("PaymentMethods");
                ElementTransaction.AppendChild(ElementPaymentMethodsTransaction);


                XmlElement ElementPaymentMethodTransaction = doc.CreateElement("PaymentMethod");
                ElementPaymentMethodTransaction.SetAttribute("subTypes", ConfigurationManager.AppSettings["PaymentCards"]);
                ElementPaymentMethodsTransaction.AppendChild(ElementPaymentMethodTransaction);

                XmlElement ElementMerchantAccountTransaction = doc.CreateElement("MerchantAccount");
                ElementMerchantAccountTransaction.SetAttribute("type", "CHASEPAYMENTECH");
                ElementPaymentMethodTransaction.AppendChild(ElementMerchantAccountTransaction);

                XmlElement ElementMerchantID = doc.CreateElement("MerchantID");
                XmlText MerchantIDtext = doc.CreateTextNode("231947");
                ElementMerchantID.AppendChild(MerchantIDtext);
                ElementMerchantAccountTransaction.AppendChild(ElementMerchantID);

                XmlElement ElementTerminalID = doc.CreateElement("TerminalID");
                XmlText TerminalIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["TerminalID"]);
                ElementTerminalID.AppendChild(TerminalIDtext);
                ElementMerchantAccountTransaction.AppendChild(ElementTerminalID);

                XmlElement ElementMerchantName = doc.CreateElement("MerchantName");
                XmlText MerchantNametext = doc.CreateTextNode("testSerenata");
                ElementMerchantName.AppendChild(MerchantNametext);
                ElementMerchantAccountTransaction.AppendChild(ElementMerchantName);



                XmlElement ElementUsername = doc.CreateElement("Username");
                XmlText Usernametext = doc.CreateTextNode(ConfigurationManager.AppSettings["DHPPUsername"]);
                ElementUsername.AppendChild(Usernametext);
                ElementMerchantAccountTransaction.AppendChild(ElementUsername);

                XmlElement ElementPassword = doc.CreateElement("Password");
                XmlText Passwordtext = doc.CreateTextNode(ConfigurationManager.AppSettings["DHPPPassword"]);
                ElementPassword.AppendChild(Passwordtext);
                ElementMerchantAccountTransaction.AppendChild(ElementPassword);

                XmlElement ElementTransactionCategory = doc.CreateElement("TransactionCategory");
                XmlText TransactionCategorytext = doc.CreateTextNode("ECOMMERCE");
                ElementTransactionCategory.AppendChild(TransactionCategorytext);
                ElementMerchantAccountTransaction.AppendChild(ElementTransactionCategory);

                XmlElement ElementRecurringCode = doc.CreateElement("RecurringCode");
                XmlText RecurringCodetext = doc.CreateTextNode("NONE");
                ElementRecurringCode.AppendChild(RecurringCodetext);
                ElementMerchantAccountTransaction.AppendChild(ElementRecurringCode);

                XmlElement ElementCVVValidation = doc.CreateElement("CVVValidation");
                ElementCVVValidation.SetAttribute("instruction", "IGNORE");
                ElementMerchantAccountTransaction.AppendChild(ElementCVVValidation);

                XmlElement ElementAVS = doc.CreateElement("AVS");
                ElementAVS.SetAttribute("instruction", "IGNORE");
                ElementMerchantAccountTransaction.AppendChild(ElementAVS);

                //******************3D Secure******************************//


                XmlElement ElementThreeDSecure = doc.CreateElement("ThreeDSecure");
                ElementMerchantAccountTransaction.AppendChild(ElementThreeDSecure);

                XmlElement ElementThreeDSystem = doc.CreateElement("ThreeDSystem");
                ElementThreeDSystem.SetAttribute("type", "VerifiedByVISA");
                ElementThreeDSecure.AppendChild(ElementThreeDSystem);

                XmlElement ElementAcquirerBIN = doc.CreateElement("AcquirerBIN");
                XmlText ElementAcquirerBINtext = doc.CreateTextNode(ConfigurationManager.AppSettings["VisaAcquirerBIN"]);
                ElementAcquirerBIN.AppendChild(ElementAcquirerBINtext);
                ElementThreeDSystem.AppendChild(ElementAcquirerBIN);

                XmlElement Element3DSecureMerchantID = doc.CreateElement("MerchantID");
                XmlText Element3DSecureMerchantIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["MerchantID"]);
                Element3DSecureMerchantID.AppendChild(Element3DSecureMerchantIDtext);
                ElementThreeDSystem.AppendChild(Element3DSecureMerchantID);

                XmlElement Element3DSystem = doc.CreateElement("ThreeDSystem");
                Element3DSystem.SetAttribute("type", "MastercardSecureCode");
                ElementThreeDSecure.AppendChild(Element3DSystem);

                XmlElement Element3DAcquirerBIN = doc.CreateElement("AcquirerBIN");
                XmlText Element3DAcquirerBINtext = doc.CreateTextNode(ConfigurationManager.AppSettings["MasterCardAcquirerBIN"]);
                Element3DAcquirerBIN.AppendChild(Element3DAcquirerBINtext);
                Element3DSystem.AppendChild(Element3DAcquirerBIN);

                XmlElement Element3DMerchantID = doc.CreateElement("MerchantID");
                XmlText Element3DMerchantIDtext = doc.CreateTextNode(ConfigurationManager.AppSettings["3DSecureMerchantID"]);
                Element3DMerchantID.AppendChild(Element3DMerchantIDtext);
                Element3DSystem.AppendChild(Element3DMerchantID);

                XmlElement Element3DMerchantName = doc.CreateElement("MerchantName");
                XmlText Element3DMerchantNametext = doc.CreateTextNode("SCL");
                Element3DMerchantName.AppendChild(Element3DMerchantNametext);
                ElementThreeDSecure.AppendChild(Element3DMerchantName);

                XmlElement Element3DMerchantUrl = doc.CreateElement("MerchantUrl");
                XmlText Element3DMerchantUrltext = doc.CreateTextNode(ConfigurationManager.AppSettings["ChaseRedirectURL"] + "?s=" + objDHHPDetails.EncryptedOrderID);
                Element3DMerchantUrl.AppendChild(Element3DMerchantUrltext);
                ElementThreeDSecure.AppendChild(Element3DMerchantUrl);

                XmlElement Element3DMerchantCountry = doc.CreateElement("MerchantCountry");
                XmlText Element3DMerchantCountrytext = doc.CreateTextNode("826");
                Element3DMerchantCountry.AppendChild(Element3DMerchantCountrytext);
                ElementThreeDSecure.AppendChild(Element3DMerchantCountry);

                //******************3D Secure******************************//



                XmlElement ElementPayment = doc.CreateElement("Payment");
                ElementPayment.SetAttribute("type", "DB");
                ElementTransaction.AppendChild(ElementPayment);

                XmlElement ElementAmount = doc.CreateElement("Amount");
                XmlText Amounttext = doc.CreateTextNode(Convert.ToString(objDHHPDetails.Amount));
                ElementAmount.AppendChild(Amounttext);
                ElementPayment.AppendChild(ElementAmount);

                XmlElement ElementCurrency = doc.CreateElement("Currency");
                XmlText Currencytext = doc.CreateTextNode(objDHHPDetails.Currency);
                ElementCurrency.AppendChild(Currencytext);
                ElementPayment.AppendChild(ElementCurrency);

                //Customer profile

                XmlElement ElementCustomer = doc.CreateElement("Customer");
                ElementTransaction.AppendChild(ElementCustomer);

                XmlElement ElementName = doc.CreateElement("Name");
                ElementCustomer.AppendChild(ElementName);

                XmlElement ElementFamily = doc.CreateElement("Family");
                XmlText Familytext = doc.CreateTextNode("behera1");
                ElementFamily.AppendChild(Familytext);
                ElementName.AppendChild(ElementFamily);

                XmlElement ElementGiven = doc.CreateElement("Given");
                XmlText Giventext = doc.CreateTextNode("sunil1");
                ElementGiven.AppendChild(Giventext);
                ElementName.AppendChild(ElementGiven);

                XmlElement ElementContact = doc.CreateElement("Contact");
                ElementCustomer.AppendChild(ElementContact);

                //XmlElement ElementIp = doc.CreateElement("Ip");
                //XmlText Iptext = doc.CreateTextNode(GetClientIp());
                //ElementIp.AppendChild(Iptext);
                //ElementContact.AppendChild(ElementIp);

                XmlElement ElementEmail = doc.CreateElement("Email");
                XmlText Emailtext = doc.CreateTextNode(objCustomerInfo.Email);
                ElementEmail.AppendChild(Emailtext);
                ElementContact.AppendChild(ElementEmail);

                XmlElement ElementMobile = doc.CreateElement("Mobile");
                XmlText Mobiletext = doc.CreateTextNode(objCustomerInfo.UKMobile);
                ElementMobile.AppendChild(Mobiletext);
                ElementContact.AppendChild(ElementMobile);

                XmlElement ElementAddress = doc.CreateElement("Address");
                ElementCustomer.AppendChild(ElementAddress);

                XmlElement ElementCountry = doc.CreateElement("Country");
                XmlText Countrytext = doc.CreateTextNode("GB");
                ElementCountry.AppendChild(Countrytext);
                ElementAddress.AppendChild(ElementCountry);

                XmlElement ElementCity = doc.CreateElement("City");
                XmlText Citytext = doc.CreateTextNode("London1");
                ElementCity.AppendChild(Citytext);
                ElementAddress.AppendChild(ElementCity);


                XmlElement ElementStreet = doc.CreateElement("Street");
                //XmlText Streettext = doc.CreateTextNode(objCustomerInfo.Street);
                XmlText Streettext = doc.CreateTextNode("123 Test Street11");
                ElementStreet.AppendChild(Streettext);
                ElementAddress.AppendChild(ElementStreet);

                XmlElement ElementZip = doc.CreateElement("Zip");
                XmlText Ziptext = doc.CreateTextNode("EC1R 3BN");
                ElementZip.AppendChild(Ziptext);
                ElementAddress.AppendChild(ElementZip);


                XmlElement ElementParameters = doc.CreateElement("Parameters");
                ElementTransaction.AppendChild(ElementParameters);

                XmlElement ElementParameter = doc.CreateElement("Parameter");
                ElementParameter.SetAttribute("name", "SDProductDescription");
                XmlText Parametertext = doc.CreateTextNode("Flowers");
                ElementParameter.AppendChild(Parametertext);
                ElementParameters.AppendChild(ElementParameter);

                XmlElement ElementParameter1 = doc.CreateElement("Parameter");
                ElementParameter1.SetAttribute("name", "SDMerchantEmail");
                XmlText Parameter1text = doc.CreateTextNode("customerservices@serenata.co.uk");
                ElementParameter1.AppendChild(Parameter1text);
                ElementParameters.AppendChild(ElementParameter1);


                return doc;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return doc;
        }
        public string GetDomainName()
        {
            string strdomainname = "";
            try
            {
                string url = System.Web.HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                strdomainname = baseUri.Host;

            }
            catch (Exception ex)
            {
                throw;
            }
            return strdomainname;
        }
    }
  
}
