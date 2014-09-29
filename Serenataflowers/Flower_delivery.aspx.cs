using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace Serenataflowers
{
    public partial class Flower_delivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltTitle.Text = "\n<title>" + CommonFunctions.PageTitle() + " - Delivery Information " + "</title>\n";
            CreateMetaTags();
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
            hmpageName.Content = "flowers_delivered";

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "Delivery";

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
    }
}