using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using SFMobile.BAL.SiteData;
using SFMobile.DTO;
using SFMobile.Exceptions;

namespace Serenataflowers
{
    public partial class amazonLocal5 : System.Web.UI.Page
    {
        CountriesLogic objCountries;
        CategoriesLogic objCategories;
        CommonFunctions objCommondetails;
        public static int productSetId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //CategoryId:9999  for best sellers 
            FillBanner(9999);
        }

        private void FillBanner(int categoryId)
        {
            objCategories = new CategoriesLogic();
            DataSet dscategoryBanner = new DataSet();
            CommonFunctions objcommon = new CommonFunctions();
            MetaDataInfo objMetaData = new MetaDataInfo();
            ViewState["CatMetaKeywords"] = null;
            ViewState["CatMetaDesc"] = null;
            try
            {
                objMetaData = objcommon.GetMetaData(System.Configuration.ConfigurationManager.AppSettings["MetaDataXML"]);
                if (objMetaData != null)
                {

                    dscategoryBanner = objCategories.GetBannerDetailsByCategorySiteId(categoryId, Convert.ToInt32(objMetaData.SiteId));
                }
                else
                {
                    dscategoryBanner = objCategories.GetBannerDetailsByCategoryId(categoryId);
                }

                if (dscategoryBanner.Tables[0].Rows.Count > 0)
                {
                    lblContentTitle.Text = dscategoryBanner.Tables[0].Rows[0]["BannerTitle"].ToString();
                    lblcontentDesc.Text = dscategoryBanner.Tables[0].Rows[0]["BannerText"].ToString();
                    spntxt.InnerText = lblcontentDesc.Text;
                    lblcontentDesc.Text = lblcontentDesc.Text.Substring(0, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisplayProductDescLength"])) + "...<a href='javascript:showCatDescMore()'>more</a>";
                    if (dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString() != "")
                    {
                        ltTitle.Text = "\n<title>" + dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString() + "</title>\n";
                        //Page.Title = dscategoryBanner.Tables[0].Rows[0]["MetaTitle"].ToString();
                    }
                    else
                    {
                        ltTitle.Text = "\n<title>" + objMetaData.Title + "</title>\n";
                        //Page.Title = objMetaData.Title;
                    }
                    ViewState["CatMetaKeywords"] = dscategoryBanner.Tables[0].Rows[0]["MetaKeywords"].ToString();
                    ViewState["CatMetaDesc"] = dscategoryBanner.Tables[0].Rows[0]["MetaDesc"].ToString();
                    banner.Src = "";
                    banner.Visible = false;
                }
                else
                {
                    ltTitle.Text = "\n<title>" + "" + "</title>\n";
                    if (categoryId == 9999 || categoryId == 8205 || categoryId == 2249 || categoryId == 2249)
                    {
                        //Page.Title = objMetaData.Title;
                        ltTitle.Text = "\n<title>" + objMetaData.Title + "</title>\n";
                        ViewState["CatMetaKeywords"] = objMetaData.MetaKey;
                        ViewState["CatMetaDesc"] = objMetaData.MetaDesc;
                    }
                    lblContentTitle.Text = "";
                    lblcontentDesc.Text = "";
                    string xbannerPath = System.Configuration.ConfigurationManager.AppSettings["Banner"].ToString();
                    if (File.Exists(xbannerPath))
                    {
                        XmlDocument bannerdoc = new XmlDocument();
                        bannerdoc.Load(xbannerPath);
                        XmlNode bannernode = bannerdoc.SelectSingleNode("banners/category[@Id='" + categoryId + "']");
                        if (bannernode != null)
                        {
                            if (bannernode.InnerText != "")
                            {
                                ancBanner.HRef = bannernode.Attributes["linkUrl"].Value;
                                banner.Src = bannernode.InnerText;
                                banner.Visible = true;
                            }
                        }
                        else
                        {
                            banner.Src = "";
                            banner.Visible = false;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        
        private void CreateMetaTags()
        {


            HtmlHead head = (HtmlHead)Page.Header;

            HtmlMeta hmdomain = new HtmlMeta();
            hmdomain.Name = "serenata.domain";
            hmdomain.Content = "serenataflowers.com";

            HtmlMeta hmpageName = new HtmlMeta();
            hmpageName.Name = "serenata.pageName";
            hmpageName.Content = "Home_Page";

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "home";

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
    }
}