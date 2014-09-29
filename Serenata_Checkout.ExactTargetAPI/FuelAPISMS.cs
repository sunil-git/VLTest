using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Serenata_Checkout.Log;
using System.Configuration;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
namespace Serenata_Checkout.ExactTargetAPI
{
    public class FuelAPISMS
    {
        public string ProcessFualApiForSms(string strMobNumber, string strMessageText)
        {
            string strResponse = string.Empty;
            try
            {
                string strMobileNumberToSend = string.Empty;
                string strPurifiedMob = string.Empty;

                if (ConfigurationManager.AppSettings["EnableTestMobile"] == "true")
                {
                    strPurifiedMob = CleanseMobileNumber(ConfigurationManager.AppSettings["TestMobNo"]);
                }
                else
                {
                    strPurifiedMob = CleanseMobileNumber(strMobNumber);
                }

                if (strPurifiedMob.Length == 10)
                {
                    string strCountryCode = ConfigurationManager.AppSettings["SmsCountryCode"];
                    string strAuthResourceUrl = ConfigurationManager.AppSettings["AuthResourceUrl"];
                    string strMsgContactSendResourceUrl = ConfigurationManager.AppSettings["MsgContactSendResourceUrl"];
                    string strMessageId = ConfigurationManager.AppSettings["MessageId"];

                    strMobileNumberToSend = strCountryCode + strPurifiedMob;
                    string strAccessToken = GetAccessToken(strAuthResourceUrl);
                    strResponse = SendSmsToMobile(strMsgContactSendResourceUrl, strMessageId, strAccessToken, strMobileNumberToSend, strMessageText);
                    if (strResponse.Contains("tokenId"))
                    {
                        strResponse = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strResponse;
        }

        private string RemoveNonNumerics(string phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(phone, "");
        }

        public string CleanseMobileNumber(string strMobileNumber)
        {
            string strPurifiedMob = string.Empty;
            try
            {
                // remove all non-numeric characters
                strMobileNumber = RemoveNonNumerics(strMobileNumber);
                // Get last 10 characters from the string.
                strPurifiedMob = strMobileNumber.Substring(strMobileNumber.Length - Math.Min(10, strMobileNumber.Length));
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strPurifiedMob;
        }

        private string getTokenStringFromResponse(string strResponse)
        {
            string strTokenString = String.Empty;
            try
            {
                string[] arrResponse = strResponse.Split(new char[] { ',' });
                int intColonIndex = arrResponse[0].IndexOf(":");
                string strAccessToken = arrResponse[0].Substring(intColonIndex + 1);
                strTokenString = strAccessToken.Replace("\"", "");
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strTokenString;
        }

        private string GetAccessToken(string strUrl)
        {
            string strToken = string.Empty;
            try
            {
                // Create POST data and convert it to a byte array.
                string strClientId = ConfigurationManager.AppSettings["ClientId"];
                string strClientSecret = ConfigurationManager.AppSettings["ClientSecret"];

                string postData = "{";
                postData += "\"clientId\": \"" + strClientId + "\",";
                postData += "\"clientSecret\": \"" + strClientSecret + "\"";
                postData += "}";

                string strResponse = postRequestData(strUrl, postData);
                strToken = getTokenStringFromResponse(strResponse);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strToken;
        }

        private string SendSmsToMobile(string strUrl, string strMessageId, string strAccessToken, string strMobNumber, string MessageText)
        {
            string strResponse = string.Empty;
            try
            {
                // Construct resource url by appending message id ann access token.
                strUrl = strUrl.Replace("{messageId}", strMessageId);
                strUrl += strAccessToken;

                // Create POST data and convert it to a byte array.
                string strMessageText = MessageText; //"Dear Ann, we wish to inform you that we've received your order, Q4094081, for delivery on 30/03/2013";//ConfigurationManager.AppSettings["MessageText"];
                string strSubscribe = ConfigurationManager.AppSettings["Subscribe"];
                string strResubscribe = ConfigurationManager.AppSettings["Resubscribe"];
                string strOverride = ConfigurationManager.AppSettings["Override"];
                string strKeyword = ConfigurationManager.AppSettings["Keyword"];

                string postData = "{";
                postData += "\"mobileNumbers\": [\"" + strMobNumber + "\"],";
                postData += "\"Subscribe\": \"" + strSubscribe + "\",";
                postData += "\"Resubscribe\": \"" + strResubscribe + "\",";
                if (strSubscribe == "true")
                {
                    postData += "\"keyword\": \"" + strKeyword + "\",";
                }
                postData += "\"Override\": \"" + strOverride + "\",";
                postData += "\"messageText\": \"" + strMessageText + "\"";
                postData += "}";
                strResponse = postRequestData(strUrl, postData);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strResponse;
        }

        private string postRequestData(string strUrl, string strPostData)
        {
            string strResponse = string.Empty;
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(strUrl);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(strPostData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                // Set the ContentLength property of the WebRequest.
                //request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                string outpou = ByteArrayToString(byteArray);
                WebResponse response = request.GetResponse();
                // Get the status.
                string strStatus = ((HttpWebResponse)response).StatusDescription;
                if (strStatus == "OK" || strStatus == "Accepted")
                {
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    strResponse = reader.ReadToEnd();
                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                }
                response.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                strResponse = ex.Message;
            }
            return strResponse;
        }

        public string ByteArrayToString(byte[] input)
        {
            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(input);
            return str;
        }
    }
}
