using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Serenata_Checkout.PaymentGateway;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using System.Web.UI.HtmlControls;

namespace Serenataflowers.Checkout
{
    public partial class m_expressCheckout : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["s"] != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();
                    Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "Paypal");
                    ProcessPayPal(OrderID);
                }
                else
                {
                    Response.Redirect("~/Default.aspx", true);
                }
            }
        }
        public void ProcessPayPal(string OrderID)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
        
           
            try
            {
                objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(OrderID);
                lstProductItems = objOrderdetails.GetBasketContents(OrderID);
                if (objDeliveryTimeInfo!=null)
                {
                    NVPAPICaller test = new NVPAPICaller();
                    string retMsg = "";
                    string token = "";
                    string totalAmount = Convert.ToString(objDeliveryTimeInfo.OrderTotal);
                    string sumTotal = String.Format("{0:0.00}", Convert.ToString(objDeliveryTimeInfo.OrderTotal));
                    string deliveryPrice = String.Format("{0:0.00}", objDeliveryTimeInfo.deliveryPrice);
                   

                    string amt = String.Format("{0:0.00}",totalAmount);

                    var kvpl = new List<KeyValuePair<string, string>>();

                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_CURRENCYCODE", "GBP"));
                    // kvpl.Add(new KeyValuePair<string, string>("ALLOWNOTE", "1"));
                    // kvpl.Add(new KeyValuePair<string, string>("NOSHIPPING", "1"));
                    //kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_TAXAMT", "5.00"));
                    //kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_SHIPPINGAMT", "1.00"));
                    //kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_HANDLINGAMT", "1.00"));
                    //kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_INSURANCEAMT", "1.00"));

                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_AMT", totalAmount));
                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_ITEMAMT", totalAmount));
                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_PAYMENTACTION", "Sale"));
                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_INVNUM", OrderID));
                    kvpl.Add(new KeyValuePair<string, string>("PAYMENTREQUEST_0_DESC", ItemName()));
                    //kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_DESCm", "Flowers"));
                   
                    //Class1 c1 = new Class1();
                    //DataSet ds = c1.get_basket_item(Session["login"].ToString());
                    //objuser.Mode = "UGB";
                    //objuser.Customer_id = Session["login"].ToString();
                    //DataTable dt = new DataTable();
                    //dt = objuser.BindDataID(ref errorMsg);
                    //if (dt.Rows.Count > 0)
                    //{
                    //for(int i=0;i<dt.Rows.Count;i++)
                    //{
                    //string item_name = "test";
                    // string qty = "1";
                    //double price = 27.25; ;
                    //string res = String.Format("{0:0.00}", price);
                    int i = 0;
                    //DataTable dtProducts = dsConfirmedOrders.Tables[0];
                    foreach (ProductInfo objProductInfo in lstProductItems)
                    {
                        // on all table's columns
                        string productTitle = Convert.ToString(objProductInfo.producttitle);
                        string quantity = Convert.ToString(objProductInfo.quantity);
                        string price = String.Format("{0:0.00}", objProductInfo.price);

                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_NAME" + i + "", productTitle));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_QTY" + i + "", quantity));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_AMT" + i + "", price));
                        i++;
                        
                    }
                    int j = i++;
                    //DataTable dtDelivery = dsConfirmedOrders.Tables[2];
                    //DataTable dtOrder = dsConfirmedOrders.Tables[1];

                    if (Convert.ToDouble(objDeliveryTimeInfo.discount) != 0.0)
                    {
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_NAME" + (j) + "", "Discount"));
                        //kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_QTY", objDeliveryTimeInfo.Deliverydate));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_AMT" + (j) + "", "-" + String.Format("{0:0.00}", Convert.ToString(objDeliveryTimeInfo.discount))));
                       ////// kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_DESC" + (j) + "", Convert.ToString(objDeliveryTimeInfo.voucherTitle)));

                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_NAME" + (j + 1) + "", Convert.ToString(objDeliveryTimeInfo.OptionName)));
                        //kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_QTY", objDeliveryTimeInfo.Deliverydate));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_AMT" + (j + 1) + "", Convert.ToString(objDeliveryTimeInfo.deliveryPrice)));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_DESC" + (j + 1) + "", "Delivery date: " + Convert.ToString(objDeliveryTimeInfo.Deliverydate)));
                    }
                    else
                    {
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_NAME" + (j) + "", Convert.ToString(objDeliveryTimeInfo.OptionName)));
                        //kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_QTY", objDeliveryTimeInfo.Deliverydate));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_AMT" + (j) + "", deliveryPrice));
                        kvpl.Add(new KeyValuePair<string, string>("L_PAYMENTREQUEST_0_DESC" + (j) + "", "Delivery date: " + Convert.ToString(objDeliveryTimeInfo.Deliverydate)));
                    }
                   
                    kvpl.Add(new KeyValuePair<string, string>("brandname", ConfigurationManager.AppSettings["PaypalBrandName"]));
                    kvpl.Add(new KeyValuePair<string, string>("LOGOIMG", LogoPath()));
               
                    bool ret = test.ShortcutExpressCheckout(amt, ref token, ref retMsg, kvpl, Request.QueryString["s"],GetSiteName());


                    // bool ret = test.ShortcutExpressCheckout(amt, ref token, ref retMsg);
                    if (ret)
                    {
                        HttpContext.Current.Session["token"] = token;
                        Response.Redirect(retMsg);

                    }
                    else
                    {
                        Response.Redirect("../ErrorPage.aspx?er=" + retMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }

        public string LogoPath()
        {
            CommonFunctions  objCommondetails = new CommonFunctions();
            string logopath = string.Empty;
            try
            {
                int siteId = objCommondetails.GetSiteId();
                switch (siteId)
                {

                    case 1:
                        logopath = ConfigurationManager.AppSettings["FlowersLogo"];
                        break;
                    case 6:
                        logopath = ConfigurationManager.AppSettings["HampersLogo"];
                        break;
                    case 3:
                        logopath = ConfigurationManager.AppSettings["ChocolatesLogo"];
                        break;
                    case 13:
                        logopath = ConfigurationManager.AppSettings["PlantsLogo"];
                        break;
                    case 4:
                        logopath = ConfigurationManager.AppSettings["WinesLogo"];
                        break;
                }
                
            }
            catch (Exception ex)
            { 
                        
            }
            return logopath;
        }
        public string GetSiteName()
        {
            string domain = string.Empty;
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
        public string ItemName()
        {
            CommonFunctions objCommondetails = new CommonFunctions();
            string ItemName = string.Empty;
            try
            {
                int siteId = objCommondetails.GetSiteId();
                switch (siteId)
                {

                    case 1:
                        ItemName = "Flowers";
                        break;
                    case 6:
                        ItemName = "Hampers";
                        break;
                    case 3:
                        ItemName = "Chocolates";
                        break;
                    case 13:
                        ItemName = "Plants";
                        break;
                    case 4:
                        ItemName = "Wines";
                        break;
                }

            }
            catch (Exception ex)
            {

            }
            return ItemName;
        }
    }
}