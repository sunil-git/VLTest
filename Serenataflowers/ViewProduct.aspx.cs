
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.BAL;
using SFMobile.BAL.SiteData;
using SFMobile.BAL.Products;
using SFMobile.BAL.Orders;
using SFMobile.Exceptions;
using System.Data;
using System.Xml;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Web.UI.HtmlControls;
using SFMobile.DTO;
#region Imported Namespaces for New Order Schema

using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using Serenata_Checkout.ChilkatComponent;
using SerenataOrderSchemaBAL;
#endregion

namespace Serenataflowers
{
   public partial class ViewProduct : AjaxLoader //System.Web.UI.Page
    {
        #region Variables
            ProductsLogic objProductsLogic;
            OrdersLogic objOrderLogic;
           // OrderInfo objOrderInfo;
            CommonFunctions objCommondetails;
            string encryptedOrderId, decryptedOrderId;
          
            #region Declared/Initilised BAL object for New Order Schema
            OrderInfo objOrderInfo = new OrderInfo();
            Common objCommon = new Common();
            DispatchesInfo objDispatchDetails = new DispatchesInfo();
            OrderLinesInfo objOrderLineInfo = new OrderLinesInfo();
            #endregion
        #endregion

        #region Page Events
        /// <summary>
        /// This Event is fired usually the most common used method on the server side 
        /// application code for an .aspx file. All code inside of this method is executed once at the beginning of 
        /// the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ProdId"]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        //if (!string.IsNullOrEmpty(Request.QueryString["s"]) && !string.IsNullOrEmpty(Request.QueryString["ProdId"]))
                        //{
                        //    objCommondetails = new CommonFunctions();

                        //    string orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
                        //    objProductsLogic = new ProductsLogic();
                        //    int ProdId = objProductsLogic.GetProductIdByOrderId(orderId);
                        //    dvcheckout.Visible = true;
                        //    var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/CustomerDetails.aspx?ProdId=" + ProdId + "&s=" + Request.QueryString["s"]));

                        //    ancsavebtn.HRef = url;
                        //}
                        //else
                        //{
                        //    dvcheckout.Visible = false;
                        //}
                        int productId = Convert.ToInt32(Request.QueryString["ProdId"]);
                        GetProductDetails();
                        BindUpgradeProducts(productId);
                        BindDeliveryDates(productId);
                        BindReviewData(productId);
                    }
                    catch(Exception ex)
                    {
                        SFMobileLog.Error(ex);
                        Response.Redirect("ErrorPage.aspx?error="+ex);
                    
                    }
                   
                  }
            }
            else
                Response.Redirect("ErrorPage.aspx");

            //CommonFunctions.AddFloodLightTags(this.Page);


        }
        /// <summary>
        /// This SelectedIndexChanged Evet will fire when user select any date from dropdown .
        /// Bases on this selected value changes Choose Option will display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDeliveryDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            objCommondetails = new CommonFunctions();
            try
            {                
                string nameOfDay = objCommondetails.ConvertDateFormateToNameOfDay(ddlDeliveryDates.SelectedValue);
                int productId = Convert.ToInt32(Request.QueryString["ProdId"]);
                BindDeliveryOptionsBasedOnDate(nameOfDay, productId, ddlDeliveryDates.SelectedValue);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
           
        }
        /// <summary>
        /// This Event will fire when user Save the Product details for Order and New order Id will be created/Ameded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnOrdernow_Click(object sender, ImageClickEventArgs e)
        {
            string enableSSL;
            OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();
            try
            {
                if (Convert.ToInt32(Request.QueryString["ProdId"]) > 0 && !string.IsNullOrEmpty(ddlDeliveryDates.SelectedValue))
                {
                    DataTable dt = (DataTable)ViewState["DeliveryOptions"];
                    string delStartTime = string.Empty;
                    string delEndTime = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToString(dr["Id"]) == rbtnLstDeliveryOptions.SelectedValue)
                        {
                            float f = float.Parse(Convert.ToString(dr["OptionPrice"]));
                            hdnOptionalPrice.Value = Convert.ToString(f);
                            break;
                        }
                    }
                    
                    string orderId = string.Empty;



                    string delim = "/";
                    char[] delimiter = delim.ToCharArray();
                    string[] split = ddlDeliveryDates.SelectedValue.Split(delimiter);
                    DateTime deliverDate = new DateTime(Convert.ToInt32(split[2]), Convert.ToInt32(split[1]), Convert.ToInt32(split[0]));

                    #region Create Order
                    OrderDTO objOrderDTO = new OrderDTO();
                     OrderDTO objOrderDTOnew = new OrderDTO();
                     objOrderDTO.ProductId = Convert.ToInt32(rbtnupgrade.SelectedValue);
                    SerenataCheckoutLogic objNewOrder = new SerenataCheckoutLogic();
                    objOrderDTOnew = objNewOrder.GetProductPriceDetails(objOrderDTO);
                    objOrderInfo.Prefix = "22";
                    objOrderInfo.IPAddress = findip();// objCommon.GetServerIp();
                    objOrderInfo.CustomerID = null;
                    objOrderInfo.CookiesID = 0;
                    if(!string.IsNullOrEmpty(Session.SessionID))
                    {
                        objOrderInfo.SessionID = Session.SessionID;
                    }
                    else{
                        objOrderInfo.SessionID = null;
                    }
                    objOrderInfo.CurrencyID = 1;
                    objOrderInfo.SiteID = objCommon.GetSiteId();
                    objOrderInfo.BrowserIP = objCommon.GetUserIp();
                    objOrderInfo.BrowserCountry = objCommon.GetBrowserCountry(objCommon.GetUserIp());                   
                    objOrderInfo.CountryID = 215;
                    objOrderInfo.FulfilmentPartnerId = objOrderDTOnew.PartnerID;
                    objOrderInfo.OrderStatusID = 30;
                    objOrderInfo.ChannelID = 2;

                    objDispatchDetails.DelOptionID = Convert.ToInt32(rbtnLstDeliveryOptions.SelectedValue);
                    objDispatchDetails.DeliveryPrice = float.Parse(hdnOptionalPrice.Value);
                    //DateTime dt;
                    //dt = DateTime.ParseExact(deldate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDispatchDetails.DeliveryDate = deliverDate;
                    objDispatchDetails.CarrierID = null;
                    objDispatchDetails.DeliveryTime = null;

                    objOrderLineInfo.ProductID = Convert.ToInt32(rbtnupgrade.SelectedValue);                  
                    objOrderLineInfo.Quantity = 1;
                    if (!string.IsNullOrEmpty(hdnbrowserID.Value))
                    {
                        objOrderInfo.BrowserID = hdnbrowserID.Value;
                    }
                    else {
                        objOrderInfo.BrowserID = "";
                    }
                    if (!string.IsNullOrEmpty(hdndeviceID.Value))
                    {
                        objOrderInfo.DeviceID = hdndeviceID.Value;
                    }
                    else
                    {
                        objOrderInfo.DeviceID = "";
                    }
                    if (!string.IsNullOrEmpty(hdnaltVisitorID.Value))
                    {
                        objOrderInfo.AltVisitorID = hdnaltVisitorID.Value;
                    }
                    else
                    {
                        objOrderInfo.AltVisitorID = "";
                    }
                    if (Request.Cookies["Order_Id"] == null && string.IsNullOrEmpty(Request.QueryString["s"]))
                    {

                        string idOrder = objOrderDetails.CreateOrder(objOrderInfo);
                        objDispatchDetails.OrderID = idOrder;
                        HttpCookie cooki = new HttpCookie("Order_Id", idOrder);
                        //cooki.Domain = objCommon.GetSiteName();
                        Response.Cookies.Add(cooki);

                        Encryption objEncryption = new Encryption();
                        if (objEncryption.CheckLicense(ConfigurationManager.AppSettings["CryptUnlockCode"]) == true)
                        {
                            objOrderInfo.EncryptedOrderID = objEncryption.GetAesEncryptionString(idOrder);
                            objOrderInfo.OrderID = idOrder;
                            objOrderDetails.UpdateEncryptedOrderId(objOrderInfo);
                        }
                        objDispatchDetails.OrderID = idOrder;
                        
                        objOrderDetails.ScheduleDispatch(objDispatchDetails.OrderID, objDispatchDetails);
                    }
                    else if (string.IsNullOrEmpty(Request.QueryString["s"]) && Request.Cookies["Order_Id"] != null)
                    {
                        objDispatchDetails.OrderID = Request.Cookies["Order_Id"].Value;
                        objOrderInfo.OrderID = Request.Cookies["Order_Id"].Value;
                        Encryption objEncryption = new Encryption();
                        objOrderInfo.EncryptedOrderID = objEncryption.GetAesEncryptionString(objOrderInfo.OrderID);
                        objOrderDetails.ScheduleDispatch(objDispatchDetails.OrderID, objDispatchDetails);
                    }
                    else if (Request.Cookies["Order_Id"] == null && !string.IsNullOrEmpty(Request.QueryString["s"]))
                    {
                        objDispatchDetails.OrderID = Common.GetOrderIdFromQueryString();
                        objOrderInfo.OrderID = Common.GetOrderIdFromQueryString();
                        Encryption objEncryption = new Encryption();
                        objOrderInfo.EncryptedOrderID = objEncryption.GetAesEncryptionString(objOrderInfo.OrderID);
                        objOrderDetails.ScheduleDispatch(objDispatchDetails.OrderID, objDispatchDetails);
                    }
                    else if (Request.Cookies["Order_Id"] != null && !string.IsNullOrEmpty(Request.QueryString["s"]))
                    {
                        objDispatchDetails.OrderID = Request.Cookies["Order_Id"].Value;
                        objOrderInfo.OrderID = Request.Cookies["Order_Id"].Value;
                        Encryption objEncryption = new Encryption();
                        objOrderInfo.EncryptedOrderID = objEncryption.GetAesEncryptionString(objOrderInfo.OrderID);
                        objOrderDetails.ScheduleDispatch(objDispatchDetails.OrderID, objDispatchDetails);
                    }
                    int response = objOrderDetails.AddProductToBasket(objDispatchDetails, objOrderLineInfo, false);
                    if (response == 0)
                    {
                        ViewState["multiFP"] = "true";
                    }
                    UpsellsBAL objupsell = new UpsellsBAL();
                    int upsellcount = objupsell.GetUpsaleCount(objOrderInfo.OrderID);

                    if (Common.IsLoggedIn() != true && objOrderInfo.FulfilmentPartnerId == 608 && upsellcount == 0)
                    {
                        if (ViewState["multiFP"] != null && Convert.ToString(ViewState["multiFP"]) == "true")
                        {
                            //Response.Redirect("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true", false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true"));
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            //Response.Redirect("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID, false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID));
                            Response.Redirect(url, false);
                        }
                    }
                    else if (Common.IsLoggedIn() == true && objOrderInfo.FulfilmentPartnerId == 608 && upsellcount == 0)
                    {

                        if (ViewState["multiFP"] != null && Convert.ToString(ViewState["multiFP"]) == "true")
                        {
                            //Response.Redirect("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true", false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true"));
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            //Response.Redirect("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID, false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_upsells.aspx?s=" + objOrderInfo.EncryptedOrderID));
                            Response.Redirect(url, false);
                        }
                    }
                    else if (Common.IsLoggedIn() == true && objOrderInfo.FulfilmentPartnerId == 608 && upsellcount > 0)
                    {

                        if (ViewState["multiFP"] != null && Convert.ToString(ViewState["multiFP"]) == "true")
                        {
                            //Response.Redirect("~/Checkout/m_RecipientDetails.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true", false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_RecipientDetails.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true"));
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            //Response.Redirect("~/Checkout/m_RecipientDetails.aspx?s=" + objOrderInfo.EncryptedOrderID, false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_RecipientDetails.aspx?s=" + objOrderInfo.EncryptedOrderID));
                            Response.Redirect(url, false);
                        }
                    }
                    else
                    {
                        if (ViewState["multiFP"] != null && Convert.ToString(ViewState["multiFP"]) == "true")
                        {
                            //Response.Redirect("~/Checkout/m_login.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true", false);
                            //var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_login.aspx?s=" + objOrderInfo.EncryptedOrderID + "&multiFP=true"));
                            //Response.Redirect(url, false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_customerdetails.aspx?s=" + objOrderInfo.EncryptedOrderID  +"&guest=yes"));
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            //Response.Redirect("~/Checkout/m_login.aspx?s=" + objOrderInfo.EncryptedOrderID, false);
                            var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_customerdetails.aspx?s=" + objOrderInfo.EncryptedOrderID + "&guest=yes"));
                            Response.Redirect(url, false);
                        }
                    }
                    #endregion
                }
                else {
                    //Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
               // Response.Redirect("Default.aspx");
            }
        }

       
        #endregion

        #region Page Methods
        /// <summary>
        /// This method will display the product details.  
        /// </summary>
        private void GetProductDetails()
        {
            try
            {
                objProductsLogic = new ProductsLogic();
                ProductDTO objProduct = null;
                int productId = Convert.ToInt32(Request.QueryString["ProdId"]);
                objProduct = objProductsLogic.GetProductDetailById(productId);
                if (objProduct != null)
                {
                    lblOfferPrice.Text = "£" + String.Format("{0:0.##}", objProduct.ProductOfferPrice);
                    lblOfferPriceAtFooter.Text = lblOfferPrice.Text;
                    lblOldPrice.Text = "£" + String.Format("{0:0.##}", objProduct.ProductOldPrice);
                    lblOldPriceAtFooter.Text = lblOldPrice.Text;
                    lblProductDesciption.Text = objProduct.ProductDesc;
                    lblProductTitle.Text = objProduct.ProductName;
                    //this.Page.Title = lblProductTitle.Text;
                    ltTitle.Text = "\n<title>" + lblProductTitle.Text + "</title>\n";
                    lblProductTitleAtFooter.Text = lblProductTitle.Text;
                    lblBottomProductInfo.Text = objProduct.ProductInfo1;
                    imgLargeProduct.ImageUrl = objProduct.ImagePath;
                    if (objProduct.ProductOfferPrice <= 0)
                    {
                        spanOldPriceFooter.Attributes.Add("class", "normalPrice");
                        spanOldPriceHeader.Attributes.Add("class", "normalPrice");
                        lblOfferPrice.Text = string.Empty;
                        lblOfferPriceAtFooter.Text = string.Empty;
                    }
                }
                // Added Pion Tag
                CreateMetaTags(objProduct);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            
        }
        /// <summary>
        /// Binding Delivery Dates based on Product id to DropdownList
        /// </summary>
        private void BindDeliveryDates(int productId)
        {
            objCommondetails = new CommonFunctions();
            objProductsLogic = new ProductsLogic();
            try
            {
                
                //int productId = Session["ProductId"]!=null ? Convert.ToInt32(Session["ProductId"]): Convert.ToInt32(Request.QueryString["ProdId"]);
                DataTable dtDeliveryDate = objProductsLogic.GetDeliveryDatesByProductId(productId);
                if (dtDeliveryDate != null)
                {
                    ddlDeliveryDates.DataSource = dtDeliveryDate;
                    ddlDeliveryDates.DataTextField = "DeliveryDate";
                    ddlDeliveryDates.DataValueField = "DateValue";
                    ddlDeliveryDates.Style.Add("font", " 1em/1.4 courier new,Arial,Geneva,Helvetica,Sans-Serif");
                    ddlDeliveryDates.Style.Add("padding-bottom", "1px");
                    ddlDeliveryDates.Style.Add("padding-top", "1px");
                    ddlDeliveryDates.DataBind();

                    ddlDeliveryDates.SelectedIndex = 0;//Session["DeliveryDateIndex"] != null?Convert.ToInt32(Session["DeliveryDateIndex"]):  0;
                    string nameOfDay =objCommondetails.ConvertDateFormateToNameOfDay(ddlDeliveryDates.SelectedValue);
                    BindDeliveryOptionsBasedOnDate(nameOfDay, productId, ddlDeliveryDates.SelectedValue);
                }
            }
            catch (Exception ex) 
            {
                SFMobileLog.Error(ex);
            }
            
        }
        /// <summary>
        /// Binding Delivery Options based on Product Id and Day to RadionButton List
        /// </summary>
        /// <param name="day"></param>
        private void BindDeliveryOptionsBasedOnDate(string day, int productId,string selectedDate)
        {
           try
           {
              objProductsLogic = new ProductsLogic();
              //int productId = Session["ProductId"] != null ? Convert.ToInt32(Session["ProductId"]) : Convert.ToInt32(Request.QueryString["ProdId"]);
              DataTable dtDeliveryOptions = objProductsLogic.GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);
              if (dtDeliveryOptions != null)
              {
                 ViewState["DeliveryOptions"] = dtDeliveryOptions;
                 rbtnLstDeliveryOptions.DataSource = dtDeliveryOptions;
                 rbtnLstDeliveryOptions.DataTextField = "OptionName";
                 rbtnLstDeliveryOptions.DataValueField = "Id";
                 rbtnLstDeliveryOptions.DataBind();
                 
                 UpdatePanel1.Update();
                 rbtnLstDeliveryOptions.SelectedIndex = 0;
              }
           }
           catch (Exception ex)
           {
              SFMobileLog.Error(ex);
           }
            
        }        
        
       
        private void BindReviewData(int ProductId)
        {
            DataTable objDtReviewData = new DataTable();
            objProductsLogic = new ProductsLogic();
            try {

                objDtReviewData = objProductsLogic.GetReviewDataByProductId(ProductId);
                if (objDtReviewData != null)
                {
                    if (Convert.ToString(objDtReviewData.Rows[0]["TotalReviews"]) != "0")
                    {
                        divOverlay.Visible = true;
                        lblReviewDesc.Text = "(" + Convert.ToString(objDtReviewData.Rows[0]["StarPercentage"]) + "%)" + " based on " +Convert.ToString(objDtReviewData.Rows[0]["TotalReviews"])+ " Customer reviews";
                        lblRating.Text = Convert.ToString(objDtReviewData.Rows[0]["StarRating"]);
                        lblof.Text = "of";
                        lblRatingCount.Text = "5";
                        string StarRatingRounded = Convert.ToDouble(objDtReviewData.Rows[0]["StarRatingRounded"]).ToString("0.0", CultureInfo.InvariantCulture);
                        imgStarRating.ImageUrl = "http://images.serenataflowers.com/star." + StarRatingRounded + ".png";
                    }
                    else
                    {
                        divOverlay.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }
        private void BindUpgradeProducts(int ProductId)
        {
            try
            {
                objProductsLogic = new ProductsLogic();
                //int productId = Session["ProductId"] != null ? Convert.ToInt32(Session["ProductId"]) : Convert.ToInt32(Request.QueryString["ProdId"]);
                DataTable dtupgradeProducts = objProductsLogic.GetUpgradeProductsByProductId(ProductId);
                if (dtupgradeProducts != null)
                {

                    rbtnupgrade.DataSource = dtupgradeProducts;
                    rbtnupgrade.DataTextField = "UpgradeProductStr";
                    rbtnupgrade.DataValueField = "UpgradeProductId";
                    rbtnupgrade.DataBind();
                    rbtnupgrade.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        
        #endregion

        #region PION Meta Tag
       /// <summary>
       /// This method will create a PION tag for product page.
       /// </summary>
       /// <param name="objProduct"></param>
        private void CreateMetaTags(ProductDTO objProduct)
        {


            HtmlHead head = (HtmlHead)Page.Header;

            HtmlMeta hmdomain = new HtmlMeta();
            hmdomain.Name = "serenata.domain";
            hmdomain.Content = "serenataflowers.com";

            HtmlMeta hmpageName = new HtmlMeta();
            hmpageName.Name = "serenata.pageName";
            hmpageName.Content = "Product:"+Convert.ToString(Request.QueryString["ProdId"]);

            HtmlMeta hmchannel = new HtmlMeta();
            hmchannel.Name = "serenata.channel";
            hmchannel.Content = "product";

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
            hmnumSessionVariables.Content = Convert.ToString(HttpContext.Current.Session.Count);

            HtmlMeta hmproductID = new HtmlMeta();
            hmproductID.Name = "serenata.productID";
            hmproductID.Content = Convert.ToString(Request.QueryString["ProdId"]);

            HtmlMeta hmproductValue = new HtmlMeta();
            hmproductValue.Name = "serenata.productValue";
            if (objProduct.ProductOfferPrice <= 0)
            {
                hmproductValue.Content = String.Format("{0:0.##}", objProduct.ProductOldPrice);
            }
            else
            {
                hmproductValue.Content = String.Format("{0:0.##}", objProduct.ProductOfferPrice);
            }

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
            head.Controls.Add(hmproductID);
            head.Controls.Add(hmproductValue);



        }
       /// <summary>
       /// This method will return the day of the week as interger value
       /// </summary>
       /// <returns></returns>
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
        #region commneted method
        // DONT ALLOW MULTI FP	
        //private int checkMultiFP()
        //{
        //    string DorderId = string.Empty;
        //    int MultiVal = 1;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(Request.QueryString["ProdId"]))
        //        {
        //            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
        //            {
        //                objCommondetails = new CommonFunctions();
        //                DorderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
        //                encryptedOrderId = Request.QueryString["s"];
        //            }
        //            if (Request.Cookies["Order_Id"] != null)
        //            {
        //                DorderId = Request.Cookies["Order_Id"].Value;
        //                encryptedOrderId = objCommondetails.Encrypt(Request.Cookies["Order_Id"].Value, "testpage");
        //            }
        //            if (!string.IsNullOrEmpty(DorderId))
        //            {
        //                // Calling SP to check Multi FB.
        //                objOrderLogic = new OrdersLogic();
        //                OrderInfo objOrdInfo = new OrderInfo();
        //                objOrdInfo.OrderId = DorderId;
        //                objOrdInfo.ProductId = Convert.ToInt32(Request.QueryString["ProdId"]);
        //                objOrdInfo = objOrderLogic.CheckMultiFPProducts(objOrdInfo);
        //                MultiVal = objOrdInfo.MultiFPTrue;
        //                if (MultiVal == 0)
        //                {
        //                    Response.Redirect("~/Checkout/m_summary.aspx?ProdId=" + objOrdInfo.ProductId + "&s=" + encryptedOrderId + "&multiFP=0", false);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }
        //    return MultiVal;
        //}
        //private int GetProductQuantity()
        //{
        //    int qty = 0;
        //    string DorderId = string.Empty;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(Request.QueryString["ProdId"]))
        //        {

        //            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
        //            {
        //                objCommondetails = new CommonFunctions();
        //                DorderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
        //            }
        //            if (Request.Cookies["Order_Id"] != null)
        //            {
        //                DorderId = Request.Cookies["Order_Id"].Value;
        //            }
        //            if (!string.IsNullOrEmpty(DorderId))
        //            {
        //                objOrderLogic = new OrdersLogic();
        //                qty = objOrderLogic.GetQuantityByOrderIdProductId(DorderId, Convert.ToInt32(rbtnupgrade.SelectedValue));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);

        //    }
        //    return qty;
        //}
        //private string GetUpselFilename(int delOptionId)
        //{
        //    string upsell = String.Empty;
        //    try
        //    {
        //        objProductsLogic = new ProductsLogic();
        //        DataTable dtdelPartDelOpt = objProductsLogic.GetPartnerIdDelPartnerIdbyOptionId(delOptionId);
        //        if (dtdelPartDelOpt != null)
        //        {
        //            string filename = "upsells-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]) + "-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["DeliveryPartnerID"]);
        //            string upsellsProductsDirectory = System.Configuration.ConfigurationManager.AppSettings["UpsellsProductXML"].ToString();
        //            string upsellsProductsXMLPath = Path.GetFullPath(upsellsProductsDirectory + "\\" + filename + ".xml");
        //            if (Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]).Trim() == System.Configuration.ConfigurationManager.AppSettings["FlowerVisionDelPartnerId"].ToString().Trim())
        //            {
        //                if (File.Exists(upsellsProductsXMLPath))
        //                {
        //                    upsell = Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]) + "-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["DeliveryPartnerID"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }
        //    return upsell;
        //}
        #endregion
        public string findip()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }    
    }
}