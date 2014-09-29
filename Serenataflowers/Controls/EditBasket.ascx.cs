using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using System.Web.UI.HtmlControls;
namespace Serenataflowers.Controls
{
    public partial class EditBasket : System.Web.UI.UserControl
    {
        #region Variables


        CommonFunctions objCommondetails;
        string orderID;
        int productId;
        public Boolean intSpotProductStatus = false;
        public String stringSpotVasevalue = string.Empty;
        #region Declared/Initilised BAL object for New Order Schema
        OrderInfo objOrderInfoNew;
        #endregion
        #endregion
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.upBasket.UpdateMode; }
            set { this.upBasket.UpdateMode = value; }
        }

        public void Update()
        {
            if (ViewState["OrderID"] != null)
            {
               
                BindBasket(Convert.ToString(ViewState["OrderID"]));
                BindDeliveryDetails(Convert.ToString(ViewState["OrderID"]));
            }
            this.upBasket.Update();
        }
        public event EventHandler ButtonClick;
        protected void Page_Load(object sender, EventArgs e)
        {
            UpsellsBAL objUpsellsBAL = new UpsellsBAL();
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    
                    if (!IsPostBack)
                    {
                        SaveAndContinue.Attributes.Add("data-theme", "d");
                        SaveAndContinue.Attributes.Add("data-role", "button");
                        SaveAndContinue.Attributes.Add("data-icon", "arrow-r");
                        SaveAndContinue.Attributes.Add("data-iconpos", "right");

                        Cancel.Attributes.Add("data-theme", "d");
                        Cancel.Attributes.Add("data-role", "button");
                        Cancel.Attributes.Add("data-icon", "arrow-l");
                        Cancel.Attributes.Add("data-iconpos", "left");

                        Save.Attributes.Add("data-theme", "d");
                        Save.Attributes.Add("data-role", "button");
                        Save.Attributes.Add("data-icon", "arrow-r");
                        Save.Attributes.Add("data-iconpos", "right");                        
                        orderID = Common.GetOrderIdFromQueryString();
                        EditBasketheader.InnerHtml = "Your Basket - " + orderID;
                        ViewState["OrderID"] = orderID;
                        BindBasket(orderID);
                        BindDeliveryDetails(orderID);
                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindBasket(string orderId)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            try
            {
                lstProductItems = objOrderdetails.GetBasketContents(orderId);
                hdnTotal.Value = Convert.ToString(objOrderdetails.GetTotalQty(orderId));
                DataTable dtProducts = objOrderdetails.GetMainProducts(orderId);
                foreach (ProductInfo objProduct in lstProductItems)
                {
                    foreach (DataRow row in dtProducts.Rows)
                    {
                        if (Convert.ToString(objProduct.productid) == row["productID"].ToString().Trim())
                            objProduct.isMainProduct = true;
                    }
                    //lstProductItems.Add(objProduct);
                }
                rptViewBasket.DataSource = lstProductItems;
                rptViewBasket.DataBind();


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void rptViewBasket_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                Button btnDelete = e.Item.FindControl("Delete") as Button;
                if (btnDelete != null)
                {
                    btnDelete.Attributes.Add("data-role", "custom");
                    btnDelete.Attributes.Add("data-icon", "delete");
                    btnDelete.Attributes.Add("data-iconpos", "notext");
                }
                //}
            }
        }
        private void BindDeliveryDetails(string orderId)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            Common objCommon = new Common();

            try
            {
                objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(orderId);

                if (objDeliveryTimeInfo != null)
                {
                    lblDeliveryType.Text = objDeliveryTimeInfo.OptionName;

                    if (objDeliveryTimeInfo.deliveryPrice == 0.00)
                        lblDeliveryPrice.Text = "FREE";
                    else
                        lblDeliveryPrice.Text = String.Format("£{0}", objDeliveryTimeInfo.deliveryPrice);

                    lblTotal.Text = String.Format("£{0:0.00}", objDeliveryTimeInfo.OrderTotal);


                }
                if (objDeliveryTimeInfo.voucherTitle != null)
                {
                    if (objDeliveryTimeInfo.voucherTitle.Trim().Length > 0)
                    {                       
                        trDiscount.Visible = true;

                        hlinkVoucherTitle.Text = objDeliveryTimeInfo.voucherTitle;
                        lblDiscountPrice.Text = string.Format("-£{0:0.00}", objDeliveryTimeInfo.discount);


                    }
                }


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void rptViewBasket_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DELETE":
                        OrderLinesInfo objOrderLines = new OrderLinesInfo();
                        string productId = Convert.ToString(e.CommandArgument);                       
                        orderID = Common.GetOrderIdFromQueryString();
                        OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
                        objOrderdetails.DeleteBasketItem(orderID, Convert.ToInt32(productId));
                        BindBasket(orderID);
                        BindDeliveryDetails(orderID);
                        if (ButtonClick != null)
                        {
                            //fires the event passing the same arguments of the button
                            //click event
                            ButtonClick(null, e);
                        }
                    break;
                case "Cancel":
                    
                    break;
                case "ADD":
                   
                    break;
            }
        }
        
        protected void SaveAndContinue_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
                List<ProductInfo> lstEditProductItems = new List<ProductInfo>();
                string productId = string.Empty;
                string orderId = string.Empty;
                orderId = Common.GetOrderIdFromQueryString();
                foreach (RepeaterItem rpbasket in rptViewBasket.Items)
                {
                    ProductInfo objProductInfo = new ProductInfo();
                    HiddenField hdnProductId = (HiddenField)rpbasket.FindControl("hdnProductId");
                    if (hdnProductId.Value != "")
                    {
                        objProductInfo.productid = Convert.ToInt32(hdnProductId.Value);
                    }
                    TextBox txtQuntity = (TextBox)rpbasket.FindControl("txtQuntity");
                    if (txtQuntity.Text == "")
                        objProductInfo.quantity = 0;
                    else
                        objProductInfo.quantity = Convert.ToInt16(txtQuntity.Text);
                    Label lblQtyPrice = (Label)rpbasket.FindControl("lblQtyPrice");
                    if (lblQtyPrice.Text.Replace("£", "") == "")
                        objProductInfo.price = 0.0;
                    else
                        objProductInfo.price = Convert.ToDouble(lblQtyPrice.Text.Replace("£", ""));

                    lstEditProductItems.Add(objProductInfo);
                }
                List<ProductInfo> lstOrgProductItems = objOrderdetails.GetBasketContents(orderId);
                if (lstOrgProductItems != null && lstEditProductItems != null)
                {
                    foreach (ProductInfo objOrgProductInfo in lstOrgProductItems.ToList())
                    {
                        int intTotal = 0;
                        foreach (ProductInfo objProductInfo in lstEditProductItems)
                        {
                            intTotal = intTotal + 1;
                            if ((objOrgProductInfo.productid == objProductInfo.productid) && (objOrgProductInfo.quantity == objProductInfo.quantity))
                            {
                                //nothing
                                break;
                            }
                            else if ((objOrgProductInfo.productid == objProductInfo.productid) && !(objOrgProductInfo.quantity == objProductInfo.quantity))
                            {
                                //update
                                objOrderdetails.UpdateBasketItem(orderId, objProductInfo.productid, objProductInfo.quantity);
                                break;
                            }
                        }
                    }
                }
                //this tests if the event has been subscribed by any method
                if (ButtonClick != null)
                {
                    //fires the event passing the same arguments of the button
                    //click event
                    ButtonClick(sender, e);
                }
                //HtmlAnchor objhtmlancher = ((HtmlAnchor)Parent.FindControl("basketCount"));
                //if (objhtmlancher != null)
                //{
                //    objhtmlancher.InnerText = Convert.ToString(objOrderdetails.GetTotalProductQty(orderId));
                //}

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
          
            orderID = Common.GetOrderIdFromQueryString();
            BindBasket(orderID);
            BindDeliveryDetails(orderID);
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
                
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQty = (TextBox)sender;
            RepeaterItem rptItem = (RepeaterItem)txtQty.NamingContainer;
            if (rptItem != null)
            {
                try
                {
                    orderID = Common.GetOrderIdFromQueryString();
                    BindBasket(orderID);
                    BindDeliveryDetails(orderID);
                }
                catch (Exception ex)
                {
                    SFMobileLog.Error(ex);
                }
            }
        }
    }
}