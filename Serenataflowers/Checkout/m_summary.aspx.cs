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
using SFMobile.BAL.Checkout;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Data;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using SerenataOrderSchemaBAL;
namespace Serenataflowers.Checkout
{
   public partial class m_summary : AjaxLoader // System.Web.UI.Page
   {
      #region Variables
      ProductsLogic objProductsLogic;
      OrdersLogic objOrderLogic;
      CheckoutLogic objCheckoutLogic;
      CommonFunctions objCommondetails ;
      string orderId;
      int productId;
      #region Declared/Initilised BAL object for New Order Schema
      SerenataCheckoutLogic objOrderSchemaSummary = new SerenataCheckoutLogic();
      OrderDTO objOrderInfoNew;
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
          if (!string.IsNullOrEmpty(Request.QueryString["ProdId"]) && !string.IsNullOrEmpty(Request.QueryString["s"]))
          {

              if (!string.IsNullOrEmpty(Request.QueryString["multiFP"]))
              {
                  if (Convert.ToInt32(Request.QueryString["multiFP"]) == 0)
                  {
                      spnMultiFP.Visible = true;
                  }
                  else
                  {
                      spnMultiFP.Visible = false;
                  }
              }
              else
              {
                  spnMultiFP.Visible = false;
              }

              //Page.Title = CommonFunctions.PageTitle() + " - Checkout - Basket.";
              ltTitle.Text = "\n<title>" + CommonFunctions.PageTitle() + " - Checkout - Basket." + "</title>\n";
              if (!IsPostBack)
              {
                  objCommondetails = new CommonFunctions();

                  orderId = objCommondetails.Decrypt(Request.QueryString["s"],"testpage");
                  CreateMetaTags(orderId);
                  productId = Convert.ToInt32(Request.QueryString["ProdId"]);// Convert.ToInt32(Session["ProductId"]);
                  BindOrdersList(orderId);
                  BindSpecialExtras(orderId, productId);
              }
             
          }
          else
          {
              Response.Redirect("../Default.aspx");
          }
          //CommonFunctions.AddFloodLightTags(this.Page);
      }
       /// <summary>
       /// This "more" link button event will fired if user clicks to see more than 12 products .
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
      protected void lnkMore_Click(object sender, EventArgs e)
      {
         try
         {
               objCommondetails = new CommonFunctions();
               orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
               List<CartInfo> lstExtras = GetSpecialExtras(orderId, Convert.ToInt32(Request.QueryString["ProdId"]));// SerenataflowersSessions.LstExtras; //GetSpecialExtras// (List<CartInfo>)Session["lstExtras"];
               int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
               int dlExtrasCount = dlExtras.Items.Count;
               int displayCount = lstExtras.Count > (dlExtrasCount + pageSize) ? dlExtrasCount + pageSize : lstExtras.Count;
               dlExtras.DataSource = lstExtras.Take(displayCount);
               dlExtras.DataBind();

               lnkMore.Visible = lstExtras.Count > displayCount ? true : false;
               lstExtras = null;
            
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
      }
      /// <summary>
    /// This Dropdown event will fired when user selct and deliverydates and based
    /// on delivery dates delivery time will displays.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
      protected void ddlDeliveryDates_SelectedIndexChanged(object sender, EventArgs e)
      {
          objCommondetails = new CommonFunctions();
          try
          {
              if (ViewState["MaxPriceProductId"] != null)
              {
                  string nameOfDay =objCommondetails.ConvertDateFormateToNameOfDay(ddlDeliveryDates.SelectedValue);
                  int productId = Convert.ToInt32(ViewState["MaxPriceProductId"]);//SerenataflowersSessions.MaxPriceProductId Convert.ToInt32("ViewState["MaxPriceProductId"]);
                  BindDeliveryOptionsBasedOnDate(nameOfDay, productId, ddlDeliveryDates.SelectedValue);
                  rbtnLstDeliveryOptions.SelectedIndex = 0;
                  spnMultiFP.Visible = false;
              }
          }
          catch (Exception ex)
          {
              SFMobileLog.Error(ex);
          }
      }

      /// <summary>
      /// Handles the ItemCommand event of the repeater control.
      /// </summary>
      /// <param name="source">The source of the event.</param>
      /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
      protected void txtQty_TextChanged(object sender, EventArgs e)
      {
         TextBox txtQty = (TextBox)sender;
         RepeaterItem rptItem = (RepeaterItem)txtQty.NamingContainer;
         if (rptItem != null)
         {
            try
            {
                objCommondetails = new CommonFunctions();
                orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
               int index = rptItem.ItemIndex;              
               List<CartInfo> lstCartInfo = CartInfoDetails(orderId); //SerenataflowersSessions.CartInfo;// (List<CartInfo>)Session["cartInfo"];
               HiddenField hdnRptProdId = (HiddenField)rptOrders.Items[index].FindControl("hdnRptProdId");
               CartInfo item = lstCartInfo.Where(p => p.ProductId == Convert.ToInt32(hdnRptProdId.Value)).SingleOrDefault();
               item.Quantity = Convert.ToInt32(txtQty.Text);
               item.TotalPrice = Convert.ToSingle(item.Quantity * item.Price);
                //Added below condition block  by Srinivas on 27Feb2013 for Qunatity issue resolve 
               if (item.Quantity > 0)
               {
                   item.ActionType = "U";
               }
               else
               {
                   item.ActionType = "D";
               }
               UpdateCartItems(lstCartInfo);// added when session is removed
               BindCartItems(lstCartInfo);
               lstCartInfo = null;
               spnMultiFP.Visible = false;
               UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
               SFMobileLog.Error(ex);
            }
         }
      }

      /// <summary>
      /// Handles the ItemCommand event of the rptOrders control.
      /// </summary>
      /// <param name="source">The source of the event.</param>
      /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
      protected void rptOrders_ItemCommand(object sender, RepeaterCommandEventArgs e)
      {
         if (e.CommandName == "Delete")
         {
            try
            {
               
                  objCommondetails = new CommonFunctions();
                  orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
                  List<CartInfo> lstCartInfo = CartInfoDetails(orderId); //SerenataflowersSessions.CartInfo;// (List<CartInfo>)Session["cartInfo"];
                  CartInfo item = lstCartInfo.Where(p => p.ProductId == Convert.ToInt32(e.CommandArgument)).SingleOrDefault();
                  item.ActionType = "D";
                  UpdateCartItems(lstCartInfo);// added when session is removed
                  BindCartItems(lstCartInfo);
                  lstCartInfo = null;
                  spnMultiFP.Visible = false;
                  UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
               SFMobileLog.Error(ex);
            }
         }
      }

      /// <summary>
      /// Handles the Checkbox changed event of the datalist control.
      /// </summary>
      /// <param name="sender">The Source of the event</param>
      /// <param name="e"></param>
      protected void chkAddExtra_CheckedChanged(object sender, EventArgs e)
      {
         CheckBox chk = (CheckBox)sender;
         DataListItem item = (DataListItem)chk.NamingContainer;
         //string orderId = Session["OrderId"] != null ? Convert.ToString(Session["OrderId"]) : Request.Cookies["OrderId"].Value;
         //string orderId = SerenataflowersSessions.OrderId != null ? SerenataflowersSessions.OrderId : Request.Cookies["OrderId"].Value;
         objCommondetails = new CommonFunctions();
         string orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
         if (item != null)//Session["cartInfo"]
         {
            try
            {
               int index = item.ItemIndex;
               HiddenField hdnSpProdId = (HiddenField)dlExtras.Items[index].FindControl("hdnSpProdId");
               Image imgSpProdImg = (Image)dlExtras.Items[index].FindControl("imgSpProdImg");
               Label lblExtraPrice = (Label)dlExtras.Items[index].FindControl("lblExtraPrice");

               List<CartInfo> lstCartInfo = CartInfoDetails(orderId); //SerenataflowersSessions.CartInfo;// (List<CartInfo>)Session["cartInfo"];
               CartInfo cartItem = lstCartInfo.Where(p => p.ProductId == Convert.ToInt32(hdnSpProdId.Value)).SingleOrDefault();

               if (cartItem != null)
               {
                  cartItem.Quantity = cartItem.Quantity + 1;
                  cartItem.TotalPrice = Convert.ToSingle(cartItem.Quantity * cartItem.Price);
                  cartItem.ActionType = "U";
               }
               else
               {
                  CartInfo Ci = new CartInfo();
                  Ci.OrderId = orderId;
                  Ci.ProductId = Convert.ToInt32(hdnSpProdId.Value);
                  Ci.DeliveryDate = DateTime.ParseExact(ddlDeliveryDates.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(ddlDeliveryDates.SelectedValue);

                  Ci.Quantity = 1;
                  Ci.ImageSmallLow = imgSpProdImg.ImageUrl.Replace("big_high", "small_low");
                  Ci.ProductTitle = imgSpProdImg.AlternateText;
                  float price = Convert.ToSingle(lblExtraPrice.Text.Replace("£", ""));
                  Ci.Price = price;
                  Ci.TotalPrice = Convert.ToSingle(Ci.Quantity * price);
                  Ci.ActionType = "I";
                  lstCartInfo.Add(Ci);
               }
               UpdateCartItems(lstCartInfo);// added when session is removed
               BindCartItems(lstCartInfo);
               UpdatePanel2.Update();


               lstCartInfo = null;
               chk.Checked = false;
               spnMultiFP.Visible = false;
            }
            catch (Exception ex)
            {
               SFMobileLog.Error(ex);
            }

         }
      }

      /// <summary>
      /// Handles the ButtonClick event for Saving the cartIems.
      /// </summary>
      /// <param name="sender">The Source of the event</param>
      /// <param name="e"></param>
      protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
      {
         try
         {
            SaveCartItems();
            string upsellfilename = GetUpselFilename(Convert.ToInt32(rbtnLstDeliveryOptions.SelectedValue));
            if (Request.QueryString["isupsell"] != null)
            {
                if (Request.QueryString["isupsell"] == "false")
                {
                    if (!string.IsNullOrEmpty(upsellfilename))
                    {
                        Response.Redirect("m_upsells.aspx?ProdId=" + Request.QueryString["ProdId"] + "&pid=" + upsellfilename + "&s=" + Request.QueryString["s"], false);
                    }
                }
                else
                {
                    //Response.Redirect("m_step1.aspx?ProdId=" + Request.QueryString["ProdId"] + "&isupsell=" + Request.QueryString.Get("isupsell") + "&s=" + Request.QueryString["s"], false);
                    Response.Redirect("CustomerDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&isupsell=" + Request.QueryString.Get("isupsell") + "&s=" + Request.QueryString["s"], false);
                }
            }
            else
            {
               
                if (!string.IsNullOrEmpty(upsellfilename))
                {
                    Response.Redirect("m_upsells.aspx?ProdId=" + Request.QueryString["ProdId"] + "&pid=" + upsellfilename + "&s=" + Request.QueryString["s"], false);
                }
                else
                {
                   // Response.Redirect("m_step1.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"], false);
                    Response.Redirect("CustomerDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"], false);
                }
            }
            
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
      }    

      /// <summary>
      /// Handles the ButtonClick event for Saving the cartIems and navigates to default.aspx .
      /// </summary>
      /// <param name="sender">The Source of the event</param>
      /// <param name="e"></param>
      protected void lnkContinueShopping_Click(object sender, EventArgs e)
      {
         try
         {
            SaveCartItems();
            //Response.Redirect("~/default.aspx", false);
            var url = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/default.aspx?ProdId=" + Convert.ToInt32(Request.QueryString["ProdId"]) + "&s=" + Request.QueryString["s"]));
            Response.Redirect(url, false); 
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
      }
      #endregion

      #region Page Methods

      /// <summary>
      /// Binding Delivery Dates based on Product id to DropdownList
      /// </summary>
      private void BindDeliveryDates(string selectedValue, int productId)
      {
          objCommondetails = new CommonFunctions();
          objProductsLogic = new ProductsLogic();
         try
         {
            
            DataTable dtProducts = objProductsLogic.GetDeliveryDatesByProductId(productId);
            if (dtProducts != null)
            {
               ddlDeliveryDates.DataSource = dtProducts;
               ddlDeliveryDates.DataTextField = "DeliveryDate";
               ddlDeliveryDates.DataValueField = "DateValue";
               ddlDeliveryDates.DataBind();              

               string nameOfDay =objCommondetails.ConvertDateFormateToNameOfDay(selectedValue);
               BindDeliveryOptionsBasedOnDate(nameOfDay, productId, selectedValue);
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
            if (productId > 0)
            {
                DataTable dt = objProductsLogic.GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);
               if (dt != null)
               {
                  ViewState["DeliveryOptions"] = dt;
                  rbtnLstDeliveryOptions.DataSource = dt;
                  rbtnLstDeliveryOptions.DataTextField = "OptionName";
                  rbtnLstDeliveryOptions.DataValueField = "Id";
                  rbtnLstDeliveryOptions.DataBind();

               }
            }
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
      }
      
      /// <summary>
      /// Bind all Ordered items based on OrderID
      /// </summary>
      private void BindOrdersList(string orderId)
      {
         objOrderLogic = new OrdersLogic();
         DataSet dsOrders = new DataSet();
         List<CartInfo> lstCartInfo = new List<CartInfo>();
         try
         {
            dsOrders = objOrderLogic.GetOrderDetailsById(orderId);
            if (dsOrders != null && dsOrders.Tables[0].Rows.Count > 0)
            {
               foreach (DataRow dr in dsOrders.Tables[0].Rows)
               {
                  CartInfo Ci = new CartInfo();
                  Ci.OrderId = orderId;
                  Ci.ProductId = Convert.ToInt32(dr["ProductId"]);
                  Ci.DeliveryDate = Convert.ToDateTime(dr["deliveryDate"]);
                  Ci.DeliveryOptionId = Convert.ToInt32(dr["delOptionId"]);
                  Ci.Quantity = Convert.ToInt32(dr["Quantity"]);
                  Ci.ImageSmallLow = Convert.ToString(dr["Img1_small_low"]).Replace("http", "https");
                  Ci.ProductTitle = Convert.ToString(dr["ProductTitle"]);
                  Ci.OrginalPrice = Convert.ToSingle(dr["Price"]);
                  Ci.OfferPrice = Convert.ToSingle(dr["Offer"]);
                  float price = Ci.OfferPrice > 0.00f ? Ci.OfferPrice : Ci.OrginalPrice;
                  Ci.Price = price;
                  Ci.TotalPrice = Convert.ToSingle(Ci.Quantity * price);
                  lstCartInfo.Add(Ci);
               }               

               //For binding Deliverydate and DeliveryOption defaultvalues based on maxprice product
               CartInfo cartInfo = lstCartInfo.Where(p => p.Price == lstCartInfo.Max(mp=>mp.Price)).FirstOrDefault();
               string deliveryDate = Convert.ToDateTime(cartInfo.DeliveryDate).ToString("dd/MM/yyyy");
               string deliveryOptionId = Convert.ToString(cartInfo.DeliveryOptionId);
               ViewState["MaxPriceProductId"] = cartInfo.ProductId;
               BindDeliveryDates(deliveryDate, cartInfo.ProductId);
               ddlDeliveryDates.SelectedValue = deliveryDate;
               if (rbtnLstDeliveryOptions.Items.FindByValue(deliveryOptionId) != null)
               {
                   rbtnLstDeliveryOptions.Items.FindByValue(deliveryOptionId).Selected = true;
               }
               else
               {
                   rbtnLstDeliveryOptions.SelectedIndex = 0;
               }
            
               
            }
            BindCartItems(lstCartInfo);
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
         finally
         {
            dsOrders.Dispose();
            // List<CartInfo> object will be cleaned up next time the GC runs
            lstCartInfo = null;
         }

      }

      /// <summary>
      /// Binding cartitems
      /// </summary>
      /// <param name="lstCartInfo"></param>
      private void BindCartItems(List<CartInfo> lstCartInfo)
      {
         // Retrieve cart details from DB after modifing the basket.         
         //lstCartInfo = CartInfoDetails(orderId); 
         rptOrders.DataSource = lstCartInfo.Where(c => c.ActionType != "D");
         rptOrders.DataBind();
         imgBtnSave.Enabled = lstCartInfo.Where(c => c.ActionType != "D").Count() > 0 ? true : false;
         if (imgBtnSave.Enabled)
         {
            imgBtnSave.ImageUrl = "https://checkout.serenataflowers.com/images/savecheckout.gif";
         }
         else
         {
            imgBtnSave.ImageUrl = "https://checkout.serenataflowers.com/images/savecheckout_grey.gif";
         }
         //Refresh the update panel for imgBtnSave url changes
         UpdatePanel4.Update();
         lstCartInfo = null;
      }
      /// <summary>
      /// Bind the extra products
      /// </summary>
      private void BindSpecialExtras(string orderId,int productId)
      {
         objCheckoutLogic = new CheckoutLogic();
         DataSet dsExtra = new DataSet();
         try
         {
            dsExtra = objCheckoutLogic.GetExtrasByProdId(orderId,productId);
            if (dsExtra != null && dsExtra.Tables[0].Rows.Count > 0)
            {
               h2AddExtra.Visible = true;
               //dlExtras.DataSource = dsExtra;
               List<CartInfo> lstExtras = BindSpecialExtrasList(dsExtra.Tables[0]);
               //SerenataflowersSessions.LstExtras = lstExtras;//Session["lstExtras"]

               int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
               dlExtras.DataSource = lstExtras.Take(pageSize);
               dlExtras.DataBind();
               lnkMore.Visible = lstExtras.Count > pageSize ? true : false;
               lstExtras = null;
            }
            else
            {
               h2AddExtra.Visible = false;
               lnkMore.Visible = false;
            }
         }
         catch (Exception ex)
         {

            SFMobileLog.Error(ex);
         }
         finally
         {
            
            dsExtra.Dispose();
         }


      }

      private List<CartInfo> BindSpecialExtrasList(DataTable dtExtras)
      {
         List<CartInfo> lstExtras = new List<CartInfo>();
         try
         {
            foreach (DataRow dr in dtExtras.Rows)
            {
               CartInfo Ci = new CartInfo();
               Ci.ProductId = Convert.ToInt32(dr["ProductId"]);
               Ci.ImageSmallLow = Convert.ToString(dr["Img1_big_high"]).Replace("http", "https");
               Ci.ProductTitle = Convert.ToString(dr["ProductTitle"]);
               Ci.OrginalPrice = Convert.ToSingle(dr["Price"]);
               Ci.Price = Convert.ToSingle(dr["Price"]); ;
               lstExtras.Add(Ci);
            }
            
         }
         catch (Exception)
         {
            
            throw;
         }
         return lstExtras;
        
      }
      /// <summary>
      /// Save the CartItems in to DB
      /// </summary>
      private void SaveCartItems()
      {
         try
         {
            //List<CartInfo> lstCartInfo = SerenataflowersSessions.CartInfo;// (List<CartInfo>)Session["cartInfo"];
             objCommondetails = new CommonFunctions();
             string orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
            List<CartInfo> lstCartInfo = CartInfoDetails(orderId);
            DateTime deliveryDate = DateTime.ParseExact(ddlDeliveryDates.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(ddlDeliveryDates.SelectedValue);
            int deliveryOptionId = Convert.ToInt32(rbtnLstDeliveryOptions.SelectedValue);
            string deliveryOptionName = rbtnLstDeliveryOptions.SelectedItem.Text;
            objCheckoutLogic = new CheckoutLogic();
            foreach (CartInfo Ci in lstCartInfo)
            {
               Ci.DeliveryDate = deliveryDate;
               Ci.DeliveryOptionId = deliveryOptionId;
               Ci.DeliveryOptionName = deliveryOptionName;
               Ci.ActionType = "U";
               int result = objCheckoutLogic.InsertCartItemDetails(Ci);
            }
            #region Saving delivery dates and time for new order Schema
            // updating delivery details    
            objOrderInfoNew = new OrderDTO();        
            objOrderInfoNew.OrderId = orderId;
            objOrderInfoNew.DeliveryOptionId = deliveryOptionId;
            objOrderInfoNew.DeliveryDate = deliveryDate;
            OrderDTO tempDelInfo = objOrderSchemaSummary.GetDeliveryDetailsByDelOptionID(objOrderInfoNew);
            objOrderInfoNew.DeliveryPartnerID = tempDelInfo.DeliveryPartnerID;
            objOrderInfoNew.OptionalCost = tempDelInfo.OptionalCost;
            objOrderInfoNew.OptionalName = tempDelInfo.OptionalName;
            OrderSchemaUpdateDeliveryDateTime(objOrderInfoNew);
            #endregion

            lstCartInfo = null;

            //for generating the Order.xml file
            //objOrderLogic = new OrdersLogic();
            //string orderId = Session["OrderId"] != null ? Convert.ToString(Session["OrderId"]) : Request.Cookies["OrderId"].Value;
            //DataSet dsTransactionDetails = objOrderLogic.GetTransactionDetailsById(orderId);
            //string strXmlOrders = objOrderLogic.GenerateOrdersXml(dsTransactionDetails);


            ////string orderXmlEncrptValue = aesLibrary.EncryptData(strXmlOrders, "testpage", "", aesLibrary.BlockSize.Block256, aesLibrary.KeySize.Key256, aesLibrary.EncryptionMode.ModeECB, true);
            ////Session["orderXml"] = strXmlOrders;
            //XmlDocument xmlOrder = new XmlDocument();
            //string filePath = ConfigurationManager.AppSettings["encryptionPath"];
            //string orderXmlFileName = GenerateFileName("order");

            //if (Directory.Exists(filePath))
            //{
            //   xmlOrder.LoadXml(strXmlOrders);
            //   xmlOrder.Save(filePath + orderXmlFileName);
            //}
            //else
            //{
            //   Directory.CreateDirectory(filePath);
            //   xmlOrder.LoadXml(strXmlOrders);
            //   xmlOrder.Save(filePath + orderXmlFileName);
            //}

            ////Store the xmlFileName to Session
            //Session["orderXmlFileName"] = orderXmlFileName;
         }
         catch (Exception ex)
         {
            SFMobileLog.Error(ex);
         }
      }

      /// <summary>
      /// Get the unique filename for store the xml files on the webserver
      /// </summary>
      /// <param name="context"></param>
      /// <returns></returns>
      //private static string GenerateFileName(string context)
      //{
      //   return context + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString() + ".xml";
      //}

      #endregion

      #region PION Meta Tag
      private void CreateMetaTags(string orderId)
      {
          try
            {
                string strBasketTotal = string.Empty;
                string strDeliveryCharge = string.Empty;
                string strOrderTotal = string.Empty;
                string strDiscountTotal = string.Empty;
                string strVATTotal = string.Empty;
                string strVistorId = string.Empty;

                OrdersLogic objOrderLogic = new OrdersLogic();
                DataTable dtOrder = objOrderLogic.GetOrderDetails(orderId);

                if (dtOrder.Rows.Count > 0)
                {
                    strOrderTotal = Convert.ToString(Math.Round(Convert.ToDouble(dtOrder.Rows[0]["Total"]), 2));
                    strDiscountTotal = Convert.ToString(Math.Round(Convert.ToDouble(dtOrder.Rows[0]["Discount"]), 2));
                    strDeliveryCharge = Convert.ToString(Math.Round(Convert.ToDouble(dtOrder.Rows[0]["OptionalCost"]), 2));
                    double accExcVAT = Math.Round(Convert.ToDouble(dtOrder.Rows[0]["totalExcVAT"]), 2);
                    double basketTotal = Math.Round(accExcVAT - Math.Round(Convert.ToDouble(strDiscountTotal), 2) - Math.Round(Convert.ToDouble(strDeliveryCharge), 2), 2);
                    strBasketTotal = Convert.ToString(basketTotal);
                    strVATTotal = Convert.ToString(Math.Round(Convert.ToDouble(strOrderTotal) - accExcVAT, 2));

                }

                if (strDiscountTotal == "0")
                    strDiscountTotal = "0.00";
                if (strDeliveryCharge == "0")
                    strDeliveryCharge = "0.00";

          HtmlHead head = (HtmlHead)Page.Header;

          HtmlMeta hmdomain = new HtmlMeta();
          hmdomain.Name = "serenata.domain";
          hmdomain.Content = "serenataflowers.com";

          HtmlMeta hmpageName = new HtmlMeta();
          hmpageName.Name = "serenata.pageName";
          hmpageName.Content = "Checkout:Summary";

          HtmlMeta hmchannel = new HtmlMeta();
          hmchannel.Name = "serenata.channel";
          hmchannel.Content = "checkout";

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

          HtmlMeta hmpurchaseID = new HtmlMeta();
          hmpurchaseID.Name = "serenata.purchaseID";
          hmpurchaseID.Content = orderId;

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

          if (Request.Cookies["cVisitorID"] != null)
          {
              strVistorId = Request.Cookies["cVisitorID"].Value;
          }

          HtmlMeta hmVisitorID = new HtmlMeta();
          hmVisitorID.Name = "serenata.VisitorID";
          hmVisitorID.Content = strVistorId;

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
          head.Controls.Add(hmpurchaseID);
          head.Controls.Add(hmtransactionID);

          head.Controls.Add(hmOrderId);
          head.Controls.Add(hmBasketTotal);
          head.Controls.Add(hmDeliveryCharges);
          head.Controls.Add(hmOrderTotal);
          head.Controls.Add(hmDiscount);
          head.Controls.Add(hmVAT);
          head.Controls.Add(hmOrderId);
          head.Controls.Add(hmBasketTotal);
          head.Controls.Add(hmDeliveryCharges);
          head.Controls.Add(hmOrderTotal);
          head.Controls.Add(hmDiscount);
          head.Controls.Add(hmVAT);
          head.Controls.Add(hmVisitorID);
            }
          catch (Exception exc)
          {
              SFMobileLog.Error(exc);
          }


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

      private List<CartInfo> CartInfoDetails(string orderId)
      {
         objOrderLogic = new OrdersLogic();
         DataSet dsOrders = new DataSet();
         List<CartInfo> lstCartInfo = new List<CartInfo>();
        
            dsOrders = objOrderLogic.GetOrderDetailsById(orderId);
            if (dsOrders != null && dsOrders.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOrders.Tables[0].Rows)
                {
                    CartInfo Ci = new CartInfo();
                    Ci.OrderId = orderId;
                    Ci.ProductId = Convert.ToInt32(dr["ProductId"]);
                    Ci.DeliveryDate = Convert.ToDateTime(dr["deliveryDate"]);
                    Ci.DeliveryOptionId = Convert.ToInt32(dr["delOptionId"]);
                    Ci.Quantity = Convert.ToInt32(dr["Quantity"]);
                    Ci.ImageSmallLow = Convert.ToString(dr["Img1_small_low"]);
                    Ci.ProductTitle = Convert.ToString(dr["ProductTitle"]);
                    Ci.OrginalPrice = Convert.ToSingle(dr["Price"]);
                    Ci.OfferPrice = Convert.ToSingle(dr["Offer"]);
                    float price = Ci.OfferPrice > 0.00f ? Ci.OfferPrice : Ci.OrginalPrice;
                    Ci.Price = price;
                    Ci.TotalPrice = Convert.ToSingle(Ci.Quantity * price);
                    lstCartInfo.Add(Ci);
                }
            }
            return lstCartInfo;
            
      }//
      private void UpdateCartItems(List<CartInfo> lstCartInfo)
      {
         
          try
          {
             
              DateTime deliveryDate = DateTime.ParseExact(ddlDeliveryDates.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(ddlDeliveryDates.SelectedValue);
              int deliveryOptionId = Convert.ToInt32(rbtnLstDeliveryOptions.SelectedValue);
              string deliveryOptionName = rbtnLstDeliveryOptions.SelectedItem.Text;
              objCheckoutLogic = new CheckoutLogic();
              foreach (CartInfo Ci in lstCartInfo)
              {
                  Ci.DeliveryDate = deliveryDate;
                  Ci.DeliveryOptionId = deliveryOptionId;
                  Ci.DeliveryOptionName = deliveryOptionName;
                  int result = objCheckoutLogic.InsertCartItemDetails(Ci);
                  #region Saving quantity,add extra and Delete product from basket for new order Schema
                  // updating delivery details   
                  objOrderInfoNew = new OrderDTO();    
                  objOrderInfoNew.OrderId = Ci.OrderId;
                  objOrderInfoNew.ProductId = Ci.ProductId;
                  objOrderInfoNew.Quantity = Ci.Quantity;
                  OrderDTO tempPriceInfo = new OrderDTO();
                  tempPriceInfo = objOrderSchemaSummary.GetProductPriceDetails(objOrderInfoNew);
                  objOrderInfoNew.Price = tempPriceInfo.Price;
                  objOrderInfoNew.ProdVATRate = tempPriceInfo.ProdVATRate;
                  objOrderInfoNew.PartnerID = tempPriceInfo.PartnerID;
                  objOrderInfoNew.UpsaleCount = objOrderSchemaSummary.GetUpsaleCount(Ci.OrderId);
                  if (Ci.ActionType == "D")
                  {
                      OrderSchemaDeleteProductFromBasket(objOrderInfoNew);
                  }
                  else if (Ci.ActionType == "I")
                  {
                      OrderSchemaAddExtra(objOrderInfoNew);
                     
                  }
                  else {
                      OrderSchemaUpdateQuantity(objOrderInfoNew);
                  }
                 
                  #endregion

              }
                    
              
          }
          catch (Exception ex)
          {
              SFMobileLog.Error(ex);
          }
      }
      private List<CartInfo> GetSpecialExtras(string orderId, int productId)
      {
          objCheckoutLogic = new CheckoutLogic();
          DataSet dsExtra = new DataSet();
          List<CartInfo> lstExtras = new List<CartInfo>();
          try
          {
              dsExtra = objCheckoutLogic.GetExtrasByProdId(orderId, productId);
              if (dsExtra != null && dsExtra.Tables[0].Rows.Count > 0)
              {
                lstExtras = BindSpecialExtrasList(dsExtra.Tables[0]);
                  
              }
              
          }
          catch (Exception ex)
          {

              SFMobileLog.Error(ex);
          }
          return lstExtras;
      }
      private string GetUpselFilename(int delOptionId)
      {
          string upsell = String.Empty;
          try
          {
              objProductsLogic = new ProductsLogic();
              DataTable dtdelPartDelOpt = objProductsLogic.GetPartnerIdDelPartnerIdbyOptionId(delOptionId);
              if (dtdelPartDelOpt != null)
              {
                  string filename = "upsells-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]) + "-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["DeliveryPartnerID"]);
                  string upsellsProductsDirectory = System.Configuration.ConfigurationManager.AppSettings["UpsellsProductXML"].ToString();
                  string upsellsProductsXMLPath = Path.GetFullPath(upsellsProductsDirectory + "\\" + filename + ".xml");
                  if (Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]).Trim() == System.Configuration.ConfigurationManager.AppSettings["FlowerVisionDelPartnerId"].ToString().Trim())
                  {
                      if (File.Exists(upsellsProductsXMLPath))
                      {
                          upsell = Convert.ToString(dtdelPartDelOpt.Rows[0]["FulfillPartnerId"]) + "-" + Convert.ToString(dtdelPartDelOpt.Rows[0]["DeliveryPartnerID"]);
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              SFMobileLog.Error(ex);
          }
          return upsell;
      }
      private void OrderSchemaUpdateDeliveryDateTime(OrderDTO objOrderInfoNew)
      {
          objOrderSchemaSummary.ScheduleDispatch(objOrderInfoNew);
      
      }
      private void OrderSchemaUpdateQuantity(OrderDTO objOrderInfoNew)
      {
          objOrderSchemaSummary.UpdateProductQuantity(objOrderInfoNew);

      }
      private void OrderSchemaDeleteProductFromBasket(OrderDTO objOrderInfoNew)
      {
          objOrderSchemaSummary.DeleteProductFromBasket(objOrderInfoNew);

      }
      private void OrderSchemaAddExtra(OrderDTO objOrderInfoNew)
      {
           objOrderSchemaSummary.AddProductLine(objOrderInfoNew);
      }
   }
}