using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using SFMobile.Exceptions;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using SFMobile.DTO;
using System.Security.Cryptography;
using System.IO;
using Frez.Crypt;
using System.Text.RegularExpressions;
using Chilkat;
namespace Serenataflowers
{
    public class CommonFunctions
    {
        /// <summary>
        /// Default Consructor for CommonFunctions
      /// </summary>
        public CommonFunctions()
        { }


        /// <summary>
        /// Get suggested address list based on the address that entered manually.
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        public DataSet CleanseAddress(string strAddr)
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
                SFMobileLog.Error(ex);
            }
            return dsAddress;
        }

        /// <summary>
        /// Get  address list based on PostCode value
        /// </summary>
        /// <param name="postCode"></param>
        /// <returns></returns>
        public DataSet GetAddressByPostCode(string postCode)
        {
            DataSet dsPostCodeAddress = new DataSet();
            try
            {
                string accountCode = ConfigurationManager.AppSettings["PostCodeAccount"];
                string licenceCode = ConfigurationManager.AppSettings["PostCodeLicence"];

                postCode = TidyValue(postCode);


                string _strUrl;

                _strUrl = "https://services.postcodeanywhere.co.uk/recordset.aspx?";

                _strUrl += "&account_code=" + System.Web.HttpUtility.UrlEncode(accountCode);

                _strUrl += "&license_code=" + System.Web.HttpUtility.UrlEncode(licenceCode);

                _strUrl += "&action=lookup";

                _strUrl += "&type=by_freetext";

                _strUrl += "&freetext=" + System.Web.HttpUtility.UrlEncode(postCode);
                dsPostCodeAddress.ReadXml(_strUrl);
                return dsPostCodeAddress;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return dsPostCodeAddress;
        }

        /// <summary>
        /// removes from ' and & symbols from input string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string TidyValue(string value)
        {
            return value.Replace("'", "").Replace("&", " ");
        }

        /// <summary>
        /// Get address list based on organisation and city/town
        /// </summary>
        /// <param name="organisation"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public DataSet GetAddressByOrganisationAndCity(string organisation, string city)
        {
            DataSet dsPostCodeAddress = new DataSet();
            try
            {
                string accountCode = ConfigurationManager.AppSettings["PostCodeAccount"];
                string licenceCode = ConfigurationManager.AppSettings["PostCodeLicence"];

                organisation = TidyValue(organisation);
                city = TidyValue(city);

                string _strUrl;

                _strUrl = "https://services.postcodeanywhere.co.uk/recordset.aspx?";

                _strUrl += "&account_code=" + System.Web.HttpUtility.UrlEncode(accountCode);

                _strUrl += "&license_code=" + System.Web.HttpUtility.UrlEncode(licenceCode);

                _strUrl += "&action=lookup";

                _strUrl += "&type=by_organisation";

                _strUrl += "&organisation=" + System.Web.HttpUtility.UrlEncode(organisation);
                _strUrl += "&town=" + System.Web.HttpUtility.UrlEncode(city);


                dsPostCodeAddress.ReadXml(_strUrl);
                return dsPostCodeAddress;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return dsPostCodeAddress;
        }

        /// <summary>
        /// Get Address fields based on AddressID
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public DataSet GetAddressFieldsBasedOnAddressID(string addressID)
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
                SFMobileLog.Error(ex);
            }

            return dsAddress;

        }

        /// <summary>
        /// check required columns exists or not
        /// </summary>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public bool CheckColumnsExistsInDataSet(DataSet dsResult)
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
                SFMobileLog.Error(ex);
            }
            return flag;
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
                SFMobileLog.Error(ex);
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
                string strHostName = Dns.GetHostName();
                System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();

                // Get server related information.
                IPHostEntry heserver = Dns.GetHostEntry(strHostName);

                ipAddress = Convert.ToString(heserver.AddressList[0]);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return ipAddress;
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
                SFMobileLog.Error(ex);
            }
           

            return lstProductType;
        
        
        }
        public List<CategoryInfo> GetCategoriesByProductType(string xmlProductTypefilepath,string CategoryId)
        {
            List<CategoryInfo> lstCategoryInfo = new List<CategoryInfo>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlProductTypefilepath);
                XmlNodeList ProductTypesList = doc.SelectNodes("/ProductTypes/ProductType[@id='" + CategoryId + "']");

                foreach (XmlNode node in ProductTypesList)
                {
                   
                    XmlElement categoryInfoElement = (XmlElement)node;
                    XmlNodeList categoryList = categoryInfoElement.GetElementsByTagName("category");
                    foreach (XmlNode categorynode in categoryList)
                    {
                        CategoryInfo objCategoryInfo = new CategoryInfo();
                        XmlElement categoryName = (XmlElement)categorynode;
                        objCategoryInfo.CategoryId = categoryName.Attributes["id"].Value;
                        objCategoryInfo.CategoryName = categoryName.InnerText;
                        lstCategoryInfo.Add(objCategoryInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


            return lstCategoryInfo;


        }

        
        public  string Encrypt(string PlainText, string Password)
        {
       
            try
            {
                return Frez.Crypt.Rijndael.EncryptData(PlainText, Password, "", Frez.Crypt.Rijndael.BlockSize.Block256, Frez.Crypt.Rijndael.KeySize.Key256, Frez.Crypt.Rijndael.EncryptionMode.ModeCBC, true);

             
            }
            catch (Exception a)
            {
                throw a;
            }
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">Text to be decrypted</param>
        /// <param name="Password">Password to decrypt with</param>
        /// <param name="Salt">Salt to decrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public string Decrypt(string PlainText, string Password)
        {
            try
            {
                return Frez.Crypt.Rijndael.DecryptData(PlainText, Password, "", Frez.Crypt.Rijndael.BlockSize.Block256, Frez.Crypt.Rijndael.KeySize.Key256, Frez.Crypt.Rijndael.EncryptionMode.ModeCBC, true);
            }
            catch (Exception a)
            {
                throw a;
            }
        }

        #region Add DoubleClick Floodlight Tag

        /// <summary>
        /// AddFloodLightTags
        /// </summary>
        /// <param name="page"></param>
        /// <param name="strCat"></param>
        //public static void AddFloodLightTags(System.Web.UI.Page page,string strCat="")
        //{
        //    try
        //    {
        //        string url = HttpContext.Current.Request.Url.ToString();
        //        Uri baseUri = new Uri(url);
        //        string domain = baseUri.Host;
        //        string strPageName = Path.GetFileName(HttpContext.Current.Request.Path);
        //        AddFloodLightTag(System.Configuration.ConfigurationManager.AppSettings["FloodlightXMLfile"],
        //                domain, strPageName, strCat, page);
        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }
        //}
        #endregion


        /// <summary>
        /// Add Double click FloodLight tag after body tag.
        /// </summary>
        /// <param name="strFloodLightTagFilepath"></param>
        /// <param name="strSiteName"></param>
        /// <param name="strPageName"></param>
        /// <param name="strCatName"></param>
        /// <param name="page"></param>
        private static void AddFloodLightTag(string strFloodLightTagFilepath, string strSiteName, string strPageName, string strCatName, System.Web.UI.Page page)
        {
            string cat = "";
            string src = "";
            string activityName = "";
            string type = "";
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(strFloodLightTagFilepath);
                XmlNodeList WebsiteList = doc.SelectNodes("floodlight/website");
                foreach (XmlNode node in WebsiteList)
                {
                    XmlElement pageInfoElement = (XmlElement)node;
                    if (pageInfoElement.Attributes["domain"].Value == strSiteName)
                    {
                        XmlNodeList pageList = pageInfoElement.GetElementsByTagName("page");
                        foreach (XmlNode pageNode in pageList)
                        {
                            XmlElement pageElement = (XmlElement)pageNode;
                            if (strPageName.ToLower() == pageElement.Attributes["name"].Value.ToLower() && strCatName.ToLower() == pageElement.Attributes["category"].Value.ToLower())
                            {
                                activityName = pageElement.GetElementsByTagName("activityName")[0].InnerText;
                                src = pageElement.GetElementsByTagName("src")[0].InnerText;
                                type = pageElement.GetElementsByTagName("type")[0].InnerText;
                                cat = pageElement.GetElementsByTagName("cat")[0].InnerText;
                                break;
                            }
                        }
                    }
                }
                string codeJavaScript = string.Format("<!-- \n Start of DoubleClick Floodlight Tag: Please do not remove\n Activity name of this tag: {0}\n URL of the webpage where the tag is expected to be placed: {1} \n This tag must be placed between the <body> and </body> tags, as close as possible to the opening tag.\n Creation Date:{2:d} \n-->", activityName, HttpContext.Current.Request.Url.ToString(), System.DateTime.Now.Date);
                codeJavaScript += "<script type='text/javascript'>";
                codeJavaScript += "var axel = Math.random()+ '';";
                codeJavaScript += "var a = axel * 10000000000000;";
                if(strCatName == "Bestseller")
                    codeJavaScript += string.Format("document.write(\"<iframe src='http://fls.doubleclick.net/activityi;src={0};type={1};cat={2};u5=[CustomerID];ord=1;num=' + a + '?' width='1' height='1' frameborder='0' style='display:none'></iframe>\");", src, type, cat);
                else
                    codeJavaScript += string.Format("document.write(\"<iframe src='http://fls.doubleclick.net/activityi;src={0};type={1};cat={2};ord=1;num=' + a + '?' width='1' height='1' frameborder='0' style='display:none'></iframe>\");", src, type, cat);
                codeJavaScript += "</script>";
                codeJavaScript += "<noscript>";
                if (strCatName == "Bestseller")
                    codeJavaScript += string.Format("<iframe src='http://fls.doubleclick.net/activityi;src={0};type={1};cat={2};u5=[CustomerID];ord=1;num=1?' width='1' height='1' frameborder='0' style='display:none'></iframe>", src, type, cat);
                else
                    codeJavaScript += string.Format("<iframe src='http://fls.doubleclick.net/activityi;src={0};type={1};cat={2};ord=1;num=1?' width='1' height='1' frameborder='0' style='display:none'></iframe>", src, type, cat);
                codeJavaScript += "</noscript>";
                codeJavaScript += "<!-- \n End of DoubleClick Floodlight Tag: Please do not remove \n-->";
                page.RegisterClientScriptBlock("OnLoadEvent", codeJavaScript);
            }
            catch (Exception e) 
            { 
                throw e; 
            }
        }

        public static void AddFloodLightTag_confirm(string strFloodLightTagFilepath, string strSiteName, string strPageName, string strCatName, System.Web.UI.Page page, string orderId, string qty, string revenue, string customerid, string pricebucket, string fulfilmentPartner)
        {

            string cat = "";
            string src = "";
            string activityName = "";
            string type = "";
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(strFloodLightTagFilepath);
                XmlNodeList WebsiteList = doc.SelectNodes("floodlight/website");
                foreach (XmlNode node in WebsiteList)
                {
                    XmlElement pageInfoElement = (XmlElement)node;
                    if (pageInfoElement.Attributes["domain"].Value == strSiteName)
                    {
                        XmlNodeList pageList = pageInfoElement.GetElementsByTagName("page");
                        foreach (XmlNode pageNode in pageList)
                        {
                            XmlElement pageElement = (XmlElement)pageNode;
                            if (strPageName.ToLower() == pageElement.Attributes["name"].Value.ToLower() && strCatName.ToLower() == pageElement.Attributes["category"].Value.ToLower())
                            {
                                activityName = pageElement.GetElementsByTagName("activityName")[0].InnerText;
                                src = pageElement.GetElementsByTagName("src")[0].InnerText;
                                type = pageElement.GetElementsByTagName("type")[0].InnerText;
                                cat = pageElement.GetElementsByTagName("cat")[0].InnerText;
                                break;
                            }
                        }
                    }
                }
                string codeJavaScript = string.Format("<!-- \n Start of DoubleClick Floodlight Tag: Please do not remove\n Activity name of this tag: {0}\n URL of the webpage where the tag is expected to be placed: {1} \n This tag must be placed between the <body> and </body> tags, as close as possible to the opening tag.\n Creation Date: {2:d} \n-->", activityName, HttpContext.Current.Request.Url.ToString(), System.DateTime.Now.Date);
                codeJavaScript += string.Format("<iframe src='https://fls.doubleclick.net/activityi;src={0};type={1};cat={2};qty={3};cost={4};u5={5};u4={6};u3={7};ord={8}?' width='1' height='1' frameborder='0' style='display:none'></iframe>", src, type, cat, qty,revenue,customerid,pricebucket,fulfilmentPartner,orderId);
               
               
                codeJavaScript += "<!-- \n End of DoubleClick Floodlight Tag: Please do not remove \n-->";
                page.RegisterClientScriptBlock("OnLoadEvent", codeJavaScript);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MetaDataInfo GetMetaData(string metadatafilepath)
        {
            MetaDataInfo objMetaDataInfo = new MetaDataInfo();
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
                            objMetaDataInfo.SiteId = MetaDataInfoElement.Attributes["siteId"].Value;
                            objMetaDataInfo.Title = MetaDataInfoElement.GetElementsByTagName("title")[0].InnerText;
                            objMetaDataInfo.MetaKey = MetaDataInfoElement.GetElementsByTagName("metakeywords")[0].InnerText;
                            objMetaDataInfo.MetaDesc = MetaDataInfoElement.GetElementsByTagName("metadescription")[0].InnerText;
                            break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


            return objMetaDataInfo;


        }
        public int GetSiteId()
        {
            string metadatafilepath = System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"];
            int siteId =1;
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
                            siteId =Convert.ToInt32(MetaDataInfoElement.Attributes["siteId"].Value);
                             break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return siteId;
         

        }
        public string GetSiteName()
        {
            string domain=string.Empty;
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                domain = baseUri.Host;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return domain;
        }
        public static string PageTitle()
        {
            string subdomain = string.Empty;
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                string host = baseUri.Host;
                char[] separators = new char[] { '.' };
                string[] domains = host.Split(separators);
                if (domains.Length > 2)
                {
                    string domainsurl = domains[1];
                    if (domainsurl.Length > 8)
                    {
                        string split = domainsurl.Substring(8, domainsurl.Length - 8);
                        string domiansplit = domainsurl.Substring(0, 8);
                        string domainupper = domiansplit.First().ToString().ToUpper() + String.Join("", domiansplit.Skip(1));
                        string splitUpp = split.First().ToString().ToUpper() + String.Join("", split.Skip(1));
                        subdomain = domainupper + " " + splitUpp;
                    }

                    
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            return subdomain;
        }
        public static string GetEncryptedMsg(string str_UnLockCode, string messageTobeEncrypted, string encryptpassword, string cryptAlgorithm, string cipherMode, int encryptkeylength, string encoding, string encodingmode)
        {
            string encText = string.Empty; 
            bool bool_Success = false;
            Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            try
            {
                bool_Success = crypt.UnlockComponent(str_UnLockCode);
                if (!bool_Success)
                {
                    SFMobileLog.Error(crypt.LastErrorText);
                }
                else
                {

                    crypt.CryptAlgorithm = cryptAlgorithm;
                    crypt.CipherMode = cipherMode;
                    crypt.KeyLength = encryptkeylength;

                    ////  Generate a binary secret key from a password string
                    ////  of any length.  For 128-bit encryption, GenEncodedSecretKey
                    ////  generates the MD5 hash of the password and returns it
                    ////  in the encoded form requested.  The 2nd param can be
                    ////  "hex", "base64", "url", "quoted-printable", etc.
                    string hexKey;
                    hexKey = crypt.GenEncodedSecretKey(encryptpassword, encoding);
                    crypt.SetEncodedKey(hexKey, encoding);

                    crypt.EncodingMode = encodingmode;
                    encText = crypt.EncryptStringENC(messageTobeEncrypted);
                }


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return encText;
        }

        public static string GetDecryptedMsg(string str_UnLockCode, string messageTobeDecrypted, string encryptpassword, string cryptAlgorithm, string cipherMode, int encryptkeylength, string encoding, string encodingmode)
        {
            string decText = string.Empty;
            bool bool_Success = false;
            Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            try
            {
                bool_Success = crypt.UnlockComponent(str_UnLockCode);
                if (!bool_Success)
                {
                    SFMobileLog.Error(crypt.LastErrorText);
                }
                else
                {

                    crypt.CryptAlgorithm = cryptAlgorithm;
                    crypt.CipherMode = cipherMode;
                    crypt.KeyLength = encryptkeylength;

                    ////  Generate a binary secret key from a password string
                    ////  of any length.  For 128-bit encryption, GenEncodedSecretKey
                    ////  generates the MD5 hash of the password and returns it
                    ////  in the encoded form requested.  The 2nd param can be
                    ////  "hex", "base64", "url", "quoted-printable", etc.
                    string hexKey;
                    
                    hexKey = crypt.GenEncodedSecretKey(encryptpassword, encoding);
                    crypt.SetEncodedKey(hexKey, encoding);
                    
                    crypt.EncodingMode = encodingmode;
                    decText = crypt.DecryptStringENC(messageTobeDecrypted);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return decText;
        }
        
    }

}