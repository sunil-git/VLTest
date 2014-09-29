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
using System.Web.UI.HtmlControls;
using Serenata_Checkout.Log;
using Serenata_Checkout.Dal;

namespace Serenata_Checkout.Logic
{
    public class MetaTag : System.Web.UI.Page
    {

        #region PION Meta Tag
        /// <summary>
        /// This method is used to createMataTags
        /// </summary>
        /// <param name="objMetaTag"></param>
        public void CreateMetaTags(MetaTagInfo objMetaTag )
        {

            try
            {

                HtmlHead head = (HtmlHead)Page.Header;

                HtmlMeta hmpageName = new HtmlMeta();
                hmpageName.Name = "serenata.pageName";
                hmpageName.Content = objMetaTag.hmpageName;

                HtmlMeta hmchannel = new HtmlMeta();
                hmchannel.Name = "serenata.channel";
                hmchannel.Content = objMetaTag.hmchannel;

                HtmlMeta hmsessionID = new HtmlMeta();
                hmsessionID.Name = "serenata.sessionID";
                hmsessionID.Content = objMetaTag.hmsessionID;

                HtmlMeta hmdayOfWeek = new HtmlMeta();
                hmdayOfWeek.Name = "serenata.dayOfWeek";
                hmdayOfWeek.Content = DayOfWeek();

                HtmlMeta hmhourOfDay = new HtmlMeta();
                hmhourOfDay.Name = "serenata.hourOfDay";
                hmhourOfDay.Content = DateTime.Now.Hour.ToString();

                HtmlMeta hmcountry = new HtmlMeta();
                hmcountry.Name = "serenata.country";
                hmcountry.Content = objMetaTag.hmcountry;

                HtmlMeta hmcurrencyID = new HtmlMeta();
                hmcurrencyID.Name = "serenata.currencyID";
                hmcurrencyID.Content = objMetaTag.hmcurrencyID;

                Common objCommondetails = new Common();
                HtmlMeta hmserverIP = new HtmlMeta();
                hmserverIP.Name = "serenata.serverIP";
                hmserverIP.Content = objCommondetails.GetServerIp();

                HtmlMeta hmbrowserIP = new HtmlMeta();
                hmbrowserIP.Name = "serenata.browserIP";
                hmbrowserIP.Content = objCommondetails.GetUserIp();

                HtmlMeta hmdate = new HtmlMeta();
                hmdate.Name = "serenata.date";
                hmdate.Content = DateTime.Now.Date.ToString("dd/MM/yyyy");

                HtmlMeta hmnumSessionVariables = new HtmlMeta();
                hmnumSessionVariables.Name = "serenata.numSessionVariables";
                hmnumSessionVariables.Content = objMetaTag.hmnumSessionVariables;

                HtmlMeta hmpurchaseID = new HtmlMeta();
                hmpurchaseID.Name = "serenata.purchaseID";
                hmpurchaseID.Content = objMetaTag.hmpurchaseID;

                HtmlMeta hmtransactionID = new HtmlMeta();
                hmtransactionID.Name = "serenata.transactionID";
                hmtransactionID.Content = objMetaTag.hmtransactionID;



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
                head.Controls.Add(hmnumSessionVariables);
                head.Controls.Add(hmpurchaseID);
                head.Controls.Add(hmtransactionID);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }


        }
        /// <summary>
        /// This method is used to getdatofweek
        /// </summary>
        /// <returns></returns>
        private string DayOfWeek()
        {
            string DayofWeek = string.Empty; ;
            try
            {
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
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return DayofWeek;
        }
        #endregion
    }
}
