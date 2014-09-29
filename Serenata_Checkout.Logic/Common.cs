using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenata_Checkout.Dto;
using System.Xml;
using System.IO;
using System.Data;
using System.Web;
using System.Net;
using Serenata_Checkout.Log;
using Serenata_Checkout.Bal.Confirmation;
using Serenata_Checkout.Dal;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.UI.WebControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.ChilkatComponent;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Serenata_Checkout.Bal.Common;

namespace Serenata_Checkout.Logic
{
    public class Common
    {
        Dal.Common.CommonDal objCommonDal = new Dal.Common.CommonDal();

        /// <summary>
        /// Get query string value.
        /// </summary>
        /// <param name="strQueryStringName">Name of query string.</param>
        /// <returns>String</returns>
        public static string GetQueryStringValue(string strQueryStringName)
        {
            string strVal = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Request.QueryString[strQueryStringName] != null)
                {
                    if (System.Web.HttpContext.Current.Request.QueryString[strQueryStringName] != "")
                    {
                        strVal = System.Web.HttpContext.Current.Request.QueryString[strQueryStringName];
                        strVal = strVal.Replace("'", "’");
                        strVal = strVal.Replace("\"", "");
                        strVal = strVal.Replace(")", "");
                        strVal = strVal.Replace("(", "");
                        strVal = strVal.Replace(";", "");
                        strVal = strVal.Replace("|", "");
                        strVal = strVal.Replace("<", "");
                        strVal = strVal.Replace(">", "");
                        strVal = strVal.Replace("select", "");
                        strVal = strVal.Replace(";", "");
                        strVal = strVal.Replace("--", "");
                        strVal = strVal.Replace("insert", "");
                        strVal = strVal.Replace("delete", "");
                        strVal = strVal.Replace("xp_", "");
                        strVal = strVal.Replace("script", "");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strVal;
	    }
        
        /// <summary>
        /// Convert Date Formate value to Name of the Day
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string ConvertDateFormateToNameOfDay(string strDate)
        {

            string delim = "/";
            char[] delimiter = delim.ToCharArray();
            string[] split = strDate.Split(delimiter);
            DateTime date1 = new DateTime(Convert.ToInt32(split[2]), Convert.ToInt32(split[1]), Convert.ToInt32(split[0]));
            string day = date1.ToString("dddd");
            return day;

        }

        /// <summary>
        /// this method will return deliverydates for a product from XML.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<DeliveryTimeInfo> BindDeliveryDatesByXML(string filename)
        {
            List<DeliveryTimeInfo> objDeliveryInfoList = new List<DeliveryTimeInfo>();
            DataSet ds = new DataSet();
            try
            {
                             
                ds.ReadXml(filename);
               if (ds.Tables != null && ds.Tables.Count > 0)
               {
                   if (ds.Tables.Contains("dayNode"))
                   {
                       DataTable dtDates = ds.Tables["dayNode"];

                       foreach (DataRow row in dtDates.Rows)
                       {
                            DeliveryTimeInfo objDeliveryInfo = new DeliveryTimeInfo();
                            objDeliveryInfo.Deliverydate = Convert.ToString(row["date"]).Trim();
                            objDeliveryInfo.DateValue = Convert.ToString(row["displayDate"]).Trim() + " " + Convert.ToString(row["optionPriceStr"]).Trim();
                            objDeliveryInfoList.Add(objDeliveryInfo);

                       }
                   }

               }
               
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return objDeliveryInfoList;

        }
        /// <summary>
        /// This method will return delivery options for a data from XML
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<DeliveryTimeInfo> BindDeliveryOptionsByXML(string filename)
        {

            List<DeliveryTimeInfo> objDeliveryOptionsList = new List<DeliveryTimeInfo>();
            DataSet ds = new DataSet();
            try
            {
            
                ds.ReadXml(filename);
                if (ds.Tables != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables.Contains("deliveryOption"))
                    {
                        DataTable dtOptions = ds.Tables["deliveryOption"];

                        foreach (DataRow row in dtOptions.Rows)
                        {
                            DeliveryTimeInfo objDeliveryInfo = new DeliveryTimeInfo();
                            objDeliveryInfo.DateValue = Convert.ToString(row["optionValue"]).Trim();
                            objDeliveryInfo.OptionName = Convert.ToString(row["optionValueStr"]).Trim() ;
                            objDeliveryOptionsList.Add(objDeliveryInfo);

                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

            return objDeliveryOptionsList;
        }

        /// <summary>
        /// Get UserIp
        /// </summary>
        /// <returns></returns>
        public string GetUserIp()
        {
            string ip = string.Empty;
            try
            {

                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                ip = "202.89.105.100";
                if (string.IsNullOrEmpty(ip))
                {
                    return ip;
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
        /// <summary>
        /// Get the Server IP  Address from HostName
        /// </summary>
        /// <returns></returns>
        public string GetServerIp()
        {
            string ipAddress = string.Empty;
            //ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            try
            {
                //string strHostName = Dns.GetHostName();
                //System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();

                //// Get server related information.
                //IPHostEntry heserver = Dns.GetHostEntry(strHostName);

                //ipAddress = Convert.ToString(heserver.AddressList[0]);

                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress1 = ipHostInfo.AddressList[0];

               

                return ipAddress1.ToString();

            }
            catch (Exception ex)
            {
                throw;
            }

            return ipAddress;
        }
        #region Get browser country
        /// <summary>
        /// Get browser country name against an user ip address.
        /// </summary>
        /// <param name="UserIp"></param>
        /// <returns>string</returns>
        public string GetBrowserCountry(string UserIp)
        {
            return objCommonDal.GetBrowserCountry(UserIp);
        }
        #endregion
        public int GetCountryIdCountryCode(string CountryCode)
        {
            return objCommonDal.GetCountryIdCountryCode(CountryCode);
        }
        public string GetCountryNameByFulFilmentPatnerID(int PatnerId)
        {
            return objCommonDal.GetCountryNameByFulFilmentPatnerID(PatnerId);
        }

        public string GetSiteName()
        {
            string domain = string.Empty;
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                domain = baseUri.Host;
                //HttpContext.Current.Response.Write("Domain - " + domain);
                //HttpContext.Current.Response.End();

                if (domain.Trim().StartsWith("."))
                {
                    domain = domain.Remove(0);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return domain;
        }

        public int GetSiteId()
        {
            string metadatafilepath = System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"];
            int siteId = 1;
            string sitename = GetSiteName();
            try
            {
                XmlDocument doc = new XmlDocument();
                if (File.Exists(metadatafilepath))
                {
                    doc.Load(metadatafilepath);
                    XmlNodeList MetadataList = doc.SelectNodes("/metadata/sitename");
                    foreach (XmlNode node in MetadataList)
                    {
                        XmlElement MetaDataInfoElement = (XmlElement)node;
                        if (MetaDataInfoElement.Attributes["name"].Value.ToLower() == sitename.ToLower())
                        {
                            siteId = Convert.ToInt32(MetaDataInfoElement.Attributes["siteId"].Value);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return siteId;
        }

        public enum VoucherMsg { Vouchercodenotexists, Invalidvouchercode, Voucherexpired, Voucheralreadyused, Success }
        public VoucherMsg ValidateVoucherCode(int response)
        {
            if (response == -3)
            {
                return VoucherMsg.Vouchercodenotexists;
            }
            else if (response == -2)
            {
                return VoucherMsg.Invalidvouchercode;
            }
            else if (response == -4)
            {
                return VoucherMsg.Voucherexpired;
            }
            else if (response == -5)
            {
                return VoucherMsg.Voucheralreadyused;
            }
            return VoucherMsg.Success;
        }



        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public  string GenerateRandomPassword(int passwordLength, int type)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyz";
            const string allowedCharsWithCaps = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string allowedCharsWithCapsAndNumbers = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string allowedCharsWithCapsAndNumbersAndSymbols = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";

            char[] chars = new char[passwordLength];
            Random rd = new Random();
            string passwordCombinations;

            switch (type)
            {
                case 1:
                    passwordCombinations = allowedChars;
                    break;
                case 2:
                    passwordCombinations = allowedCharsWithCaps;
                    break;
                case 3:
                    passwordCombinations = allowedCharsWithCapsAndNumbers;
                    break;
                case 4:
                    passwordCombinations = allowedCharsWithCapsAndNumbersAndSymbols;
                    break;
                default:
                    passwordCombinations = allowedChars;
                    break;
            }

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = passwordCombinations[rd.Next(0, passwordCombinations.Length - 1)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Get suggested address list based on the address that entered manually.
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        public static DataSet CleanseAddress(string strAddr)
        {
            DataSet dsAddress = new DataSet();
            string strUrl = string.Empty;
            try
            {
                string accountCode = ConfigurationManager.AppSettings["PostCodeAccount"];
                string licenceCode = ConfigurationManager.AppSettings["PostCodeLicence"];
                strUrl = "https://services.postcodeanywhere.co.uk/recordset.aspx?";
                strUrl = strUrl + "account_code=" + System.Web.HttpUtility.UrlEncode(accountCode);
                strUrl = strUrl + "&license_code=" + System.Web.HttpUtility.UrlEncode(licenceCode);
                strUrl = strUrl + "&action=cleanse";
                strUrl = strUrl + "&type=by_streetfiltered";
                strUrl = strUrl + "&address=" + System.Web.HttpUtility.UrlEncode(strAddr);
                dsAddress.ReadXml(strUrl);
                return dsAddress;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return dsAddress;
        }

        /// <summary>
        /// Get Address fields based on AddressID.
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public static DataSet GetAddressFieldsBasedOnAddressID(string addressID)
        {
            DataSet dsAddress = new DataSet();
            try
            {
                string accountCode = ConfigurationManager.AppSettings["PostCodeAccount"];
                string licenceCode = ConfigurationManager.AppSettings["PostCodeLicence"];
                string _strUrl;
                _strUrl = "https://services.postcodeanywhere.co.uk/recordset.aspx?";
                _strUrl += "&account_code=" + System.Web.HttpUtility.UrlEncode(accountCode);
                _strUrl += "&license_code=" + System.Web.HttpUtility.UrlEncode(licenceCode);
                _strUrl += "&action=fetch";
                _strUrl += "&style=raw";
                _strUrl += "&id=" + System.Web.HttpUtility.UrlEncode(addressID);
                dsAddress.ReadXml(_strUrl);
                return dsAddress;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return dsAddress;
        }

        /// <summary>
        /// check required columns exists or not
        /// </summary>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public static bool CheckColumnsExistsInDataSet(DataSet dsResult)
        {
            bool flag = false;
            try
            {
                foreach (DataTable dt in dsResult.Tables)
                {
                    if (dt.TableName == "row")
                    {
                        flag = dt.Columns.Contains("id");
                        if (flag == true)
                        {
                            flag = dt.Columns.Contains("description");
                            if (flag == false)
                                flag = dt.Columns.Contains("organisation_name");
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                return flag;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return flag;
        }

        /// <summary>
        /// Fill countries into dropdown list.
        /// </summary>
        /// <param name="drpCountry"></param>
        public static void FillAllCountries(DropDownList drpCountry)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            DataSet dsCountry = new DataSet();
            try
            {
                dsCountry = objCustomerDetails.GetCountryById(1);
                if (dsCountry != null && dsCountry.Tables.Count > 0)
                {
                    drpCountry.DataSource = dsCountry;
                    drpCountry.DataTextField = "CountryName";
                    drpCountry.DataValueField = "Id";
                    drpCountry.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
        }

        /// <summary>
        /// It returns decrypted Order ID from query string.
        /// </summary>
        /// <returns>string</returns>
        public static string GetOrderIdFromQueryString()
        {
            string strOrderId = string.Empty;
            try
            {
                Encryption objEncryption = new Encryption();
                strOrderId = objEncryption.GetAesDecryptionString(GetQueryStringValue("s"));
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strOrderId;
        }


        /// <summary>
        /// Returns configuration app key value.
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfigKeyValue(string strKey)
        {
            string str = string.Empty;
            try
            {
                str = ConfigurationManager.AppSettings[strKey];
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return str;
        }

        /// <summary>
        /// It checks whether user has logged in or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsLoggedIn()
        {
            bool login = false;
            try
            {
                if (HttpContext.Current.Request.Cookies["CustomerID"] != null)
                {
                    login = true;
                }
                else
                {
                    login = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return login;
        }

        public static void RemoveCookie(string key, HttpResponse response, HttpRequest request)
        {
            //check that the request object is valid
            if (request == null) return;
            //check that the response object is valid
            if (response == null) return;
            //check key is passed in
            if (string.IsNullOrEmpty(key)) return;
            //check if the cookie exists
            if (request.Cookies[key] != null)
            {
                //create a new cookie to replace the current cookie
                HttpCookie newCookie = new HttpCookie(key);
                //set the new cookie to expire 1 day ago
                newCookie.Expires = DateTime.Now.AddDays(-1d);
                //update the cookies collection on the response
                response.Cookies.Add(newCookie);
            }
        }

        /// <summary>
        /// Checks blank string, if so it returns pair of blank double quotes.
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public static string IsBlank(string strVal)
        {
            string str = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(strVal))
                {
                    str = "\"\"";
                }
                else
                {
                    str = strVal;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return str;
        }

        /// <summary>
        /// Returns enumerated week day value.
        /// </summary>
        /// <returns></returns>
        public static string DayOfWeek()
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

        /// <summary>
        /// Fill countries into dropdown list.
        /// </summary>
        /// <param name="drpCountry"></param>
        public static void GetAllCountries(DropDownList drpCountry)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            DataSet dsCountry = new DataSet();
            try
            {
                dsCountry = objCustomerDetails.GetCountryById(1);
                if (dsCountry != null && dsCountry.Tables.Count > 0)
                {
                    drpCountry.DataSource = dsCountry;
                    drpCountry.DataTextField = "CountryName";
                    drpCountry.DataValueField = "Id";
                    drpCountry.DataBind();
                    drpCountry.SelectedIndex = -1;
                    string country = "United Kingdom";
                    if (!string.IsNullOrEmpty(country))
                    {
                        drpCountry.Items.Remove(drpCountry.Items.FindByText(country));
                        drpCountry.SelectedItem.Text = "United Kingdom";
                        drpCountry.SelectedItem.Value = "215";
                    }
                    else
                    {
                        drpCountry.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
        }

        /// <summary>
        /// Remove "undefined" message from hidden field.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetJSHiddenValue(string str)
        {
            string strVal = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    if (str == "undefined")
                    {
                        strVal = string.Empty;
                    }
                    else
                    {
                        strVal = str;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strVal.Trim();
        }
        public List<ProductTypeInfo> GetProductType(string xmlProductTypefilepath)
        {
            List<ProductTypeInfo> lstProductType = new List<ProductTypeInfo>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlProductTypefilepath);
                XmlNodeList ProductTypList = doc.GetElementsByTagName("ProductType");
                foreach (XmlNode node in ProductTypList)
                {
                    ProductTypeInfo objProductTypeInfo = new ProductTypeInfo();
                    XmlElement ProductTypeInfoElement = (XmlElement)node;
                    if (ProductTypeInfoElement.Attributes["id"] != null && ProductTypeInfoElement.GetElementsByTagName("name")[0] != null && ProductTypeInfoElement.GetElementsByTagName("domain")[0] != null)
                    {
                        objProductTypeInfo.ProductTypeId = Convert.ToInt32(ProductTypeInfoElement.Attributes["id"].Value);
                        objProductTypeInfo.ProductType = ProductTypeInfoElement.GetElementsByTagName("name")[0].InnerText;
                        objProductTypeInfo.domainName = ProductTypeInfoElement.GetElementsByTagName("domain")[0].InnerText;
                    }

                    lstProductType.Add(objProductTypeInfo);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }


            return lstProductType;


        }

        public static List<DHPPResponseCodeInfo> ParseDHHPResponseDetails(string xmlDHHPresponsefilepath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlDHHPresponsefilepath);
            XmlNodeList xmlNodeListTrackingCodes = xmlDocument.SelectNodes("ResponseCodes/ResponseCode");
            XmlNode xmlNodeElement = null;
            List<DHPPResponseCodeInfo> objListDHPPResponseCodeInfo = new List<DHPPResponseCodeInfo>();
            foreach (XmlNode xmlNodeTrackingCode in xmlNodeListTrackingCodes)
            {
                DHPPResponseCodeInfo objDHPPResponseCodeInfo = new DHPPResponseCodeInfo();
                objDHPPResponseCodeInfo.ResponseCode = (xmlNodeTrackingCode.Attributes["Code"] == null) ? "" : xmlNodeTrackingCode.Attributes["Code"].Value;

                xmlNodeElement = xmlNodeTrackingCode.SelectSingleNode("./Message");
                if (xmlNodeElement != null)
                    objDHPPResponseCodeInfo.Message = xmlNodeElement.InnerText.Trim();

                xmlNodeElement = xmlNodeTrackingCode.SelectSingleNode("./Status");
                if (xmlNodeElement != null)
                    objDHPPResponseCodeInfo.Status = xmlNodeElement.InnerText.Trim();

                xmlNodeElement = xmlNodeTrackingCode.SelectSingleNode("./CustomMessage");
                if (xmlNodeElement != null)
                    objDHPPResponseCodeInfo.CustomMessage = xmlNodeElement.InnerText.Trim();

                objListDHPPResponseCodeInfo.Add(objDHPPResponseCodeInfo);
            }



            return objListDHPPResponseCodeInfo;

        }

        #region PION Meta Tag
        public static void AddMetaTags(string orderId, HtmlHead head, string pageName)
        {
            try
            {
                string strBasketTotal = string.Empty;
                string strDeliveryCharge = string.Empty;
                string strOrderTotal = string.Empty;
                string strDiscountTotal = string.Empty;
                string strVATTotal = string.Empty;
                string strVistorId = string.Empty;

                OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
                DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
                objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(orderId);
                if (objDeliveryTimeInfo != null)
                {
                    strOrderTotal = String.Format("{0:0.00}", objDeliveryTimeInfo.OrderTotal);
                    strDiscountTotal = String.Format("{0:0.00}", objDeliveryTimeInfo.discount);
                    strDeliveryCharge = String.Format("{0:0.00}", objDeliveryTimeInfo.deliveryPrice);
                    double accExcVAT = Convert.ToDouble(objDeliveryTimeInfo.TotalExVat);
                    double basketTotal = Math.Round(accExcVAT - Math.Round(Convert.ToDouble(strDiscountTotal), 2) - Math.Round(Convert.ToDouble(strDeliveryCharge), 2), 2);
                    strBasketTotal = Convert.ToString(basketTotal);
                    strVATTotal = String.Format("{0:0.00}", Convert.ToDouble(strOrderTotal) - accExcVAT);
                }
                
                HtmlMeta hmpageName = new HtmlMeta();
                hmpageName.Name = "serenata.pageName";
                hmpageName.Content = "Checkout:" + pageName;

                HtmlMeta hmchannel = new HtmlMeta();
                hmchannel.Name = "serenata.channel";
                hmchannel.Content = "checkout";

                HtmlMeta hmsessionID = new HtmlMeta();
                hmsessionID.Name = "serenata.sessionID";
                hmsessionID.Content = HttpContext.Current.Session.SessionID;

                HtmlMeta hmdayOfWeek = new HtmlMeta();
                hmdayOfWeek.Name = "serenata.dayOfWeek";
                hmdayOfWeek.Content = DayOfWeek();

                HtmlMeta hmhourOfDay = new HtmlMeta();
                hmhourOfDay.Name = "serenata.hourOfDay";
                hmhourOfDay.Content = DateTime.Now.Hour.ToString(new CultureInfo("de-DE"));


                HtmlMeta hmcountry = new HtmlMeta();
                hmcountry.Name = "serenata.country";
                hmcountry.Content = "United Kingdom";

                HtmlMeta hmcurrencyID = new HtmlMeta();
                hmcurrencyID.Name = "serenata.currencyID";
                hmcurrencyID.Content = "1";

                HtmlMeta hmserverIP = new HtmlMeta();
                hmserverIP.Name = "serenata.serverIP";
                hmserverIP.Content = new Common().GetServerIp();

                HtmlMeta hmbrowserIP = new HtmlMeta();
                hmbrowserIP.Name = "serenata.browserIP";
                hmbrowserIP.Content = new Common().GetUserIp();

                HtmlMeta hmdate = new HtmlMeta();
                hmdate.Name = "serenata.date";
                hmdate.Content = DateTime.Now.Date.ToString("dd/MM/yyyy");

                HtmlMeta hmnumSessionVariables = new HtmlMeta();
                hmnumSessionVariables.Name = "serenata.numSessionVariables";
                hmnumSessionVariables.Content = Convert.ToString(HttpContext.Current.Session.Count);

                HtmlMeta hmpurchaseId = new HtmlMeta();
                hmpurchaseId.Name = "serenata.purchaseID";
                hmpurchaseId.Content = orderId;

                HtmlMeta hmdomain = new HtmlMeta();
                hmdomain.Name = "serenata.transactionID";
                hmdomain.Content = orderId;

                HtmlMeta hmtransactionID = new HtmlMeta();
                hmtransactionID.Name = "serenata.transactionID";
                hmtransactionID.Content = orderId;

                HtmlMeta hmOrderId = new HtmlMeta();
                hmOrderId.Name = "serenata.orderID";
                hmOrderId.Content = orderId;

                HtmlMeta hmBasketTotal = new HtmlMeta();
                hmBasketTotal.Name = "serenata.BasketTotal";
                hmBasketTotal.Content = strBasketTotal;

                HtmlMeta hmDeliveryCharges = new HtmlMeta();
                hmDeliveryCharges.Name = "serenata.DeliveryCharge";
                hmDeliveryCharges.Content = strDeliveryCharge;

                HtmlMeta hmOrderTotal = new HtmlMeta();
                hmOrderTotal.Name = "serenata.OrderTotal";
                hmOrderTotal.Content = strOrderTotal;

                HtmlMeta hmDiscount = new HtmlMeta();
                hmDiscount.Name = "serenata.DiscountTotal";
                hmDiscount.Content = strDiscountTotal;

                HtmlMeta hmVAT = new HtmlMeta();
                hmVAT.Name = "serenata.VATTotal";
                hmVAT.Content = strVATTotal;

                if (HttpContext.Current.Request.Cookies["cVisitorID"] != null)
                {
                    strVistorId = HttpContext.Current.Request.Cookies["cVisitorID"].Value;
                }

                HtmlMeta hmVisitorID = new HtmlMeta();
                hmVisitorID.Name = "serenata.VisitorID";
                hmVisitorID.Content = strVistorId;

                HtmlMeta hmPageViewID = new HtmlMeta();
                hmPageViewID.Name = "serenata.PageViewID";
                hmPageViewID.Content = Get_PageViewID();

                // Page meta details
                head.Controls.Add(new LiteralControl(Environment.NewLine));
                head.Controls.Add(new LiteralControl(Environment.NewLine));
                head.Controls.Add(new LiteralControl("<!-- start of Pion tracking -->"));
                head.Controls.Add(new LiteralControl(Environment.NewLine));

                AddTagIntoHeader(head, hmpageName);
                AddTagIntoHeader(head, hmchannel);
                AddTagIntoHeader(head, hmsessionID);
                AddTagIntoHeader(head, hmdayOfWeek);
                AddTagIntoHeader(head, hmhourOfDay);
                AddTagIntoHeader(head, hmcountry);
                AddTagIntoHeader(head, hmcurrencyID);
                AddTagIntoHeader(head, hmserverIP);
                AddTagIntoHeader(head, hmbrowserIP);
                AddTagIntoHeader(head, hmdate);
                AddTagIntoHeader(head, hmnumSessionVariables);
                AddTagIntoHeader(head, hmpurchaseId);
                AddTagIntoHeader(head, hmdomain);

                //Order meta details 
                AddTagIntoHeader(head, hmOrderId);
                AddTagIntoHeader(head, hmBasketTotal);
                AddTagIntoHeader(head, hmDeliveryCharges);
                AddTagIntoHeader(head, hmOrderTotal);
                AddTagIntoHeader(head, hmDiscount);
                AddTagIntoHeader(head, hmVAT);
                AddTagIntoHeader(head, hmVisitorID);
                AddTagIntoHeader(head, hmPageViewID);
                if (pageName == "Confirmation")
                {
                    AddExtraTagsForConfirmation(ref head, orderId);
                }

                head.Controls.Add(new LiteralControl(Environment.NewLine));
                head.Controls.Add(new LiteralControl(Environment.NewLine));
                head.Controls.Add(new LiteralControl("<!-- end of Pion tracking -->"));
                head.Controls.Add(new LiteralControl(Environment.NewLine));
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
        }

        static void AddExtraTagsForConfirmation(ref HtmlHead head, string orderId)
        {
            ConfirmDetailsInfo objConfirmInfo = null; 
            ConfirmationBAL objConfirmBal = new ConfirmationBAL();
            objConfirmInfo = objConfirmBal.GetDataForMetaTagInConfirmation(orderId);


            HtmlMeta hmCustomerId = new HtmlMeta();
            hmCustomerId.Name = "serenata.CustomerID";
            hmCustomerId.Content = objConfirmInfo.CustomerId;

            HtmlMeta hmPaymentType = new HtmlMeta();
            hmPaymentType.Name = "serenata.PaymentType";
            hmPaymentType.Content = objConfirmInfo.Payment;

            HtmlMeta hmCurrency = new HtmlMeta();
            hmCurrency.Name = "serenata.Currency";
            hmCurrency.Content = "GBP";

            AddTagIntoHeader(head, hmCustomerId);
            AddTagIntoHeader(head, hmPaymentType);
            AddTagIntoHeader(head, hmCurrency);
        }

        static void AddTagIntoHeader(HtmlHead header, HtmlMeta metaTag)
        {
            try
            {
                header.Controls.Add(new LiteralControl(Environment.NewLine));
                header.Controls.Add(new LiteralControl(Environment.NewLine));
                header.Controls.Add(metaTag);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex.Message);
            }
        }
        #endregion


        

        public static string GetSocializeKey(string siteName)
        {
            const string SERENATAFLOWERSKEY = "3_3u5vepEjyEAd6l9pFV4XDhYKEjHfAJjeSv587qo0Lk4-XhRRoEAnrwrY54ZU4nk8";
            const string SERENATACHOCOLATESKEY = "3_i706lOL8z4HgvhLg-mbH0Qz3YPG00qXE4pVtdf4Ng5ow2qSNcCd3DIxSyLtm_RUD";
            const string SERENATAHAMPERSKEY = "3_KM4RrdFC0LFpVHioLxjhlA6cmVYU0chyzs6ejBuyVUEm6Jm602xZER2_OyoIzMGR";
            const string SERENATAWINESKEY = "3_3fvgsfn2rzPmtlLfX4gr2F0H66DQpvn_-lpvceCktg-makYN_PV9_iVK9CWbubQs";

            string key = string.Empty;
            try
            {
                switch (siteName)
                {
                    case "serenataflowers.com":
                        key = SERENATAFLOWERSKEY;
                        break;
                    case "serenatachocolates.com":
                        key = SERENATACHOCOLATESKEY;
                        break;
                    case "serenatahampers.com":
                        key = SERENATAHAMPERSKEY;
                        break;
                    case "serenatawines.com":
                        key = SERENATAWINESKEY;
                        break;
                }
                return key;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //the below function added on 4th March 2014
        public static string getRootUrl()
        {
            string _domainName = string.Empty;
            try
            {
                _domainName = new Common().GetSiteName().ToLower().Replace("m.", "www.").Replace("desktop.serenataflowers.com", "94.199.191.227").Replace("awswww.", "aws.");
            }
            catch (Exception)
            {
                throw;
            }
            return _domainName;
        }

        #region Delivery Cutoff

        public void RevealCutOffMsg(HtmlGenericControl MasterBody, string strOrderId)
        {
            try
            {
                string strMessage = new OrderDetailsBAL().CheckDeliveryCutoffByOrderID(strOrderId);
                if (!String.IsNullOrEmpty(strMessage))
                {
                    MasterBody.Attributes.Add("onload", "revealDeliveryCutoffMsg('" + strMessage + "');");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
        }

        public void CheckCutOffTime(HtmlGenericControl MasterBody, string strOrderId)
        {
            try
            {
                string strDeliveryDate = "";
                string strEndTime = new CommonBal().GetCountdownTimeByOrderID(strOrderId, ref strDeliveryDate);
                if (!String.IsNullOrEmpty(strEndTime))
                {
                    DateTime Stime = Convert.ToDateTime(strEndTime);
                    TimeSpan freTime = Stime.Subtract(GetServerDateTime());
                    if (freTime.Seconds >= 0)
                    {
                        Int64 t_cutoff = (freTime.Hours * 60 * 60) + (freTime.Minutes * 60) + (freTime.Seconds);
                        MasterBody.Attributes.Add("onload", "showcookieParam(" + t_cutoff + "," + freTime.Hours + "," + freTime.Minutes + "," + freTime.Seconds + ", '" + strDeliveryDate + "');");
                    }
                    else
                    {
                        RevealCutOffMsg(MasterBody, strOrderId);
                    }
                }
                else
                {
                    RevealCutOffMsg(MasterBody, strOrderId);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex.Message);
            }
        }


        public DateTime GetServerDateTime()
        {
            DateTime dt = new DateTime();
            try
            {
                dt = Convert.ToDateTime(new CommonBal().GetServerDateTime());
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return dt;
        }

        #endregion

        static string Get_PageViewID()
        {
            string PVID = string.Empty;


            string currentURL = Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri);
            string requestDateTime = Convert.ToString(DateTime.Now);

            string clientIP = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                clientIP = HttpContext.Current.Request.UserHostAddress;
            }
            string serverIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]);
            PVID = currentURL + "|" + requestDateTime + "|" + clientIP + "|" + serverIP;
            Common cmnObject = new Common();
            PVID = cmnObject.CalculateMD5Hash(PVID);

            return PVID;
        }
    }
}
