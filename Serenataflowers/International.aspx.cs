using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SFMobile.BAL.SiteData;
using SFMobile.Exceptions;
using System.Text;
using System.Web.UI.HtmlControls;
namespace Serenataflowers
{
    public partial class International : System.Web.UI.Page
    {
       #region variables
       CountriesLogic objCountries;
       #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           
                GetAllContries();
                CreateMetaTags();
        }

        #region Page Methods
        /// <summary>
        /// Method to Get all Countries in Alphabetical order
        /// </summary>
        private void GetAllContries()
        {
           try
           {
              objCountries = new CountriesLogic();
              DataSet dsCountry = new DataSet();
              StringBuilder sbCountries = new StringBuilder();
              dsCountry = objCountries.GetCountryById(0);
              if (dsCountry.Tables.Count > 0)
              {
                 foreach (DataTable dtCountry in dsCountry.Tables)
                 {
                    DataRow rowHeader = dtCountry.Rows[0];
                    sbCountries.Append("<div class='step'>");
                    sbCountries.Append("<h2 style='font-size:28px;color:orange;margin:0;'><b>"+ rowHeader["FIRSTCHAR"].ToString() + "</b></h2>");
                    foreach (DataRow row in dtCountry.Rows)
                    {
                       sbCountries.Append(" <a href='Default.aspx?countryid=" + row["ID"].ToString() + "' title='Florist in " + row["COUNTRYNAME"].ToString() + "'>" + row["COUNTRYNAME"].ToString() + "</a>&nbsp;&nbsp;"); 
                    }
                    sbCountries.Append("<div style='clear:left;'></div><br>");
                    sbCountries.Append("</div>");
                 }
                 LtrlContries.Text = sbCountries.ToString();
              }
           }
           catch (Exception ex)
           {
              SFMobileLog.Error(ex);
           }
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
            hmpageName.Content = "Mobile:International";

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "International";

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
            hmcountry.Content = SerenataflowersSessions.CountryName;

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
            hmnumSessionVariables.Content = HttpContext.Current.Session.Count.ToString();

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
        #endregion
    }
   
}