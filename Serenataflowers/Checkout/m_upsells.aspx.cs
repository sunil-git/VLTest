using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;

namespace Serenataflowers.Checkout
{
    public partial class m_upsells :System.Web.UI.Page
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
        
        #region Page Load
        /// <summary>
        /// This Event is fired usually the most common used method on the server side 
        /// application code for an .aspx file. All code inside of this method is executed once at the beginning of 
        /// the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
             UpsellsBAL objUpsellsBAL = new UpsellsBAL();
             DispatchesBAL objDelivery = new DispatchesBAL();
             OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();

        

            try
            {

                

                
                    if (!IsPostBack)
                    {
                        //objCommondetails = new CommonFunctions();
                        //ClientScript.GetPostBackEventReference(btnHidden, string.Empty);
                        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                        {
                          //  new Common().RevealCutOffMsg(MasterBody, Common.GetOrderIdFromQueryString());


                            SaveAndCheckout.Attributes.Add("data-theme", "d");
                            SaveAndCheckout.Attributes.Add("data-icon", "arrow-r");
                            SaveAndCheckout.Attributes.Add("data-iconpos", "right");
                            SaveAndCheckout.Attributes.Add("data-iconpos", "right");


                            ContinueShopping.Attributes.Add("data-theme", "a");
                            ContinueShopping.Attributes.Add("data-icon", "arrow-l");
                            ContinueShopping.Attributes.Add("data-mini", "true");
                            ContinueShopping.Attributes.Add("data-iconpos", "left");

                            ltTitle.Text = "\n<title>" + CommonFunctions.PageTitle() + " - Checkout - Upsells Details." + "</title>\n";

                            orderID =  Common.GetOrderIdFromQueryString();
                            int productID = 0;
                            //string orderID = Common.GetOrderIdFromQueryString();
                            //basketCount.HRef = "m_basket.aspx?s=" + Request.QueryString["s"];
                            //EditBasket.Attributes.Add("src", "m_basket.aspx?s=" + Request.QueryString["s"]);
                            //int quantity=objOrderDetails.GetTotalProductQty(orderID);
                           // basketCount.InnerText =Convert.ToString(quantity);
                            string strProductId = string.Empty;
                            strProductId = objDelivery.GetmainProductByOrderId(orderID);

                            if (strProductId != null && strProductId != "0")
                            {
                                productID = Convert.ToInt32(strProductId);
                            }
                            else
                            {
                                //Response.Redirect("../Default.aspx");
                            }

                            int partnerId;
                            partnerId = objUpsellsBAL.GetDeliveryPartnerid(orderID);

                            ViewState["partnerId"] = partnerId;
                            ViewState["orderID"] = orderID;

                            

                     
                            BindUpsells(productID, partnerId); ;
                            Common.AddMetaTags(orderID, (HtmlHead)Page.Header, "Upsells");

                     
                    }
                    else
                    {
                       
                        Response.Redirect("../ErrorPage.aspx");
                    } 
                }
                    new Common().CheckCutOffTime(MasterBody, Common.GetOrderIdFromQueryString());              
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        #endregion
        protected void GorgeousVases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                  CheckBox chkBox = e.Item.FindControl("Selectvase") as CheckBox;
                    if (chkBox != null)
                    {
                        chkBox.InputAttributes.Add("class", "custom");
                        chkBox.InputAttributes.Add("data-theme", "c");
                        chkBox.InputAttributes.Add("data-mini", "true");
                    }
                //}
            }
        }
        protected void TeddyBears_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                CheckBox chkBox = e.Item.FindControl("SelectTeddyBears") as CheckBox;
                if (chkBox != null)
                {
                    chkBox.InputAttributes.Add("class", "custom");
                    chkBox.InputAttributes.Add("data-theme", "c");
                    chkBox.InputAttributes.Add("data-mini", "true");
                }
                //}
            }
        }
        protected void Chocolate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                CheckBox chkBox = e.Item.FindControl("SelectChocolate") as CheckBox;
                if (chkBox != null)
                {
                    chkBox.InputAttributes.Add("class", "custom");
                    chkBox.InputAttributes.Add("data-theme", "c");
                    chkBox.InputAttributes.Add("data-mini", "true");
                }
                //}
            }
        }
        protected void Balloons_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                CheckBox chkBox = e.Item.FindControl("SelectBalloons") as CheckBox;
                if (chkBox != null)
                {
                    chkBox.InputAttributes.Add("class", "custom");
                    chkBox.InputAttributes.Add("data-theme", "c");
                    chkBox.InputAttributes.Add("data-mini", "true");
                }
                //}
            }
        }
        protected void Wines_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                //{


                CheckBox chkBox = e.Item.FindControl("SelectWines") as CheckBox;
                if (chkBox != null)
                {
                    chkBox.InputAttributes.Add("class", "custom");
                    chkBox.InputAttributes.Add("data-theme", "c");
                    chkBox.InputAttributes.Add("data-mini", "true");
                }
                //}
            }
        }

        private void BindUpsells(int productid, int partnerId)
        {
            List<ProductInfo> objGorgeousVasesList = new List<ProductInfo>();
            List<ProductInfo> objTeddyBearsList = new List<ProductInfo>();
            List<ProductInfo> objChocolateCakeList = new List<ProductInfo>();
            List<ProductInfo> objBalloonsList = new List<ProductInfo>();
            List<ProductInfo> objWinesList = new List<ProductInfo>();
            List<ProductInfo> objTempVasesList = new List<ProductInfo>();
            UpsellsBAL objUpsellBAL = new UpsellsBAL();
            string strVasePionValues = string.Empty;
            string strTeddyPionValues = string.Empty;
            string strChocklatePionValues = string.Empty;
            string strBallonsPionValues = string.Empty;
            string strWinesPionValues = string.Empty;
            string strupsellsSpotlight = string.Empty;

            try
            {
                int intPartnerId = Convert.ToInt16(ViewState["partnerId"]);
                // for Gorgeous Vases
                if (Request.QueryString["vaseTest"] != "1")
                {
                    if (partnerId == 2)
                    {
                        objGorgeousVasesList = objUpsellBAL.GetUpsells(intPartnerId, productid, 3, 3, 3);
                    }
                    objTempVasesList = objUpsellBAL.GetUpsells(intPartnerId, productid, 8, 8, 8);
                    objGorgeousVasesList = objGorgeousVasesList.Concat(objTempVasesList).ToList();
                    intSpotProductStatus = true;
                    if(objGorgeousVasesList.Count>0)
                    {
                        strVasePionValues = GetPIONVariables(objGorgeousVasesList);
                        GorgeousVases.DataSource = objGorgeousVasesList.Take(4);
                        GorgeousVases.DataBind();
                    
                        ZoomGorgeousVase.DataSource = objGorgeousVasesList.Take(4);
                        ZoomGorgeousVase.DataBind();
                        divVases.Visible = true;
                    }
                }

                 //Teddy Bears
                objTeddyBearsList = objUpsellBAL.GetUpsells(intPartnerId, productid, 5, 5, 5);
                if (objTeddyBearsList.Count > 0)
                {
                    strTeddyPionValues = GetPIONVariables(objTeddyBearsList);
                    TeddyBears.DataSource = objTeddyBearsList.Take(4);
                    TeddyBears.DataBind();
                    ZoomTeddyBears.DataSource = objTeddyBearsList.Take(4);
                    ZoomTeddyBears.DataBind();
                    divTeddy.Visible = true;
                }

                ////Chocolate, Birthday/Romantic Cake
                objChocolateCakeList = objUpsellBAL.GetUpsells(intPartnerId, productid, 9, 9, 20);
                if (objChocolateCakeList.Count > 0)
                {
                    strChocklatePionValues = GetPIONVariables(objChocolateCakeList);
                    Chocolate.DataSource = objChocolateCakeList.Take(4);
                    Chocolate.DataBind();
                    ZoomChocolate.DataSource = objChocolateCakeList.Take(4);
                    ZoomChocolate.DataBind();
                    DivChocolate.Visible = true;
                }
                

                // Balloons
                // Why not surprise with an air filled foil balloon?
                if (partnerId == 2 || partnerId == 8)
                {   
                    objBalloonsList = objUpsellBAL.GetUpsells(intPartnerId, productid, 10, 10, 10);
                    if (objBalloonsList.Count > 0)
                    {
                        divBalloons.Visible = true;
                        strBallonsPionValues = GetPIONVariables(objBalloonsList);
                        Balloons.DataSource = objBalloonsList.Take(4);
                        Balloons.DataBind();
                        ZoomBalloons.DataSource = objBalloonsList.Take(4);
                        ZoomBalloons.DataBind();
                    }
                   
                }
                else
                {
                    divBalloons.Visible = false;
                }

                //if (partnerId == 8)
                //{
                //    //lblBallonsText.Text = "Why not surprise with a helium balloon?";
                //    //divballon.Visible = true;
                //    objBalloonsList = objUpsellBAL.GetUpsells(intPartnerId, productid, 10, 10, 10);
                       
                //        if (objBalloonsList.Count > 0)
                //        {
                //            divBalloons.Visible = true;                           
                //            strBallonsPionValues = GetPIONVariables(objBalloonsList);
                //            Balloons.DataSource = objBalloonsList.Take(4);
                //            Balloons.DataBind();
                //            ZoomBalloons.DataSource = objBalloonsList.Take(4);
                //            ZoomBalloons.DataBind();
                //        }
                //}
                //else
                //{
                //    divBalloons.Visible = false;
                //}
                if (partnerId == 8)
                {
                        objWinesList = objUpsellBAL.GetUpsells(intPartnerId, productid, 7, 7, 7);
                        if (objWinesList.Count > 0)
                        {
                            divWines.Visible = true;
                            strWinesPionValues = GetPIONVariables(objWinesList);
                            Wines.DataSource = objWinesList.Take(4);
                            Wines.DataBind();
                            ZoomWines.DataSource = objWinesList.Take(4);
                            ZoomWines.DataBind();
                        }
                }
                else
                {
                    divWines.Visible = false;
                }

                StringBuilder strPIONVariables = new StringBuilder();

                strPIONVariables.Append("<!-- PION VARIABLES\n\n");
                strPIONVariables.Append("serenata.upsellsVasesprodlist=");
                strPIONVariables.Append(Common.IsBlank(strVasePionValues) + "\n");

                strPIONVariables.Append("serenata.upsellsTeddyBearsprodlist=");
                strPIONVariables.Append(Common.IsBlank(strTeddyPionValues) + "\n");

                strPIONVariables.Append("serenata.upsellsBirthdayprodlist=");
                strPIONVariables.Append(Common.IsBlank(strChocklatePionValues) + "\n");

                strPIONVariables.Append("serenata.upsellsBalloonsprodlist=");
                strPIONVariables.Append(Common.IsBlank(strBallonsPionValues) + "\n");

                strPIONVariables.Append("serenata.upsellsWinesprodlist=");
                strPIONVariables.Append(Common.IsBlank(strWinesPionValues));


                ltrSpot.Text = Convert.ToString(strPIONVariables);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private string GetPIONVariables(List<ProductInfo> objProductInfoList)
        {
            string strRtn = string.Empty;
            UpsellsBAL objUpsellBAL = new UpsellsBAL();
            try
            {
                int rowSize = objProductInfoList.Count / 7;
                if (rowSize == 0)
                    rowSize = 1;
                int rowNo = 0;
                int colNo = 0;
                foreach (ProductInfo objProductInfo in objProductInfoList)
                {
                    if (objProductInfoList.Count <= 7)
                    {
                        rowNo = 1;
                        colNo = colNo + 1;
                    }
                    else if (colNo > 7)
                    {
                        rowNo = rowNo + 1;
                        colNo = 1;
                    }
                    else
                    {
                        colNo = colNo + 1;
                    }
                    int productId = objProductInfo.productid;
                    string productTitle = objProductInfo.producttitle;
                    if (!productTitle.Contains("giftbox"))
                    {
                        DispatchesBAL objDispatchBAL = new DispatchesBAL();
                        if (intSpotProductStatus == true)
                        {
                            GetSpotLightPION(productId);
                            intSpotProductStatus = false;
                        }
                        string strRtnPION = objUpsellBAL.GetPIONVariables(productId, rowNo, colNo);
                        if (strRtn != string.Empty)
                        {
                            strRtn = strRtn + "|" + strRtnPION;
                        }
                        else
                        {
                            strRtn = "\"[" + strRtnPION;
                        }
                    }
                }
                if (strRtn != string.Empty)
                {
                    strRtn = strRtn + "]" + "[Bestsellers|" + rowSize + "x" + objProductInfoList.Count + "]\"";
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return strRtn;
        }
        private void GetSpotLightPION(int ProductId)
        {
            string strRtn = string.Empty;
            UpsellsBAL objUpsellBAL = new UpsellsBAL();
            try
            {
                string strRtnPION = objUpsellBAL.GetPIONVariables(ProductId, 1, 1);
                strRtn = "\"[" + strRtnPION;
                stringSpotVasevalue = strRtn + "]" + "[Bestsellers|" + 1 + "X" + 1 + "]\"";
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void SaveAndCheckout_Click(object sender, EventArgs e)
        {
            OrderLinesInfo objOrderLines = new OrderLinesInfo();
            UpsellsBAL objUpsellsBAL = new UpsellsBAL();
            objCommondetails = new CommonFunctions();
            string DecryptedOrderId = string.Empty;
            try
            {
                DecryptedOrderId = Common.GetOrderIdFromQueryString();
                #region Save GorgeousVases
                foreach (RepeaterItem repeaterItem in GorgeousVases.Items)
                {
                    CheckBox chkBox = repeaterItem.FindControl("Selectvase") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            int index = repeaterItem.ItemIndex;
                            HiddenField hdnSpProdId = (HiddenField)GorgeousVases.Items[index].FindControl("hdnSpProdId");
                            HiddenField hdnprice = (HiddenField)GorgeousVases.Items[index].FindControl("hdnPrice");
                            objOrderLines.OrderID = DecryptedOrderId;
                            objOrderLines.ProductID = Convert.ToInt32(hdnSpProdId.Value);
                            objOrderLines.Price = Convert.ToDouble(hdnprice.Value);
                            objOrderLines.Description = "Upsells";
                            objUpsellsBAL.AddUpsellsProduct(objOrderLines);
                        }
                    }
                }
                #endregion
                #region Save TeddyBears
                foreach (RepeaterItem repeaterItem in TeddyBears.Items)
                {
                    CheckBox chkBox = repeaterItem.FindControl("SelectTeddyBears") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            int index = repeaterItem.ItemIndex;
                            HiddenField hdnSpProdId = (HiddenField)TeddyBears.Items[index].FindControl("hdnTeddyBearProdId");
                            HiddenField hdnprice = (HiddenField)TeddyBears.Items[index].FindControl("hdnTeddyBearPrice");
                            objOrderLines.OrderID = DecryptedOrderId;
                            objOrderLines.ProductID = Convert.ToInt32(hdnSpProdId.Value);
                            objOrderLines.Price = Convert.ToDouble(hdnprice.Value);
                            objOrderLines.Description = "Upsells";
                            objUpsellsBAL.AddUpsellsProduct(objOrderLines);
                        }
                    }
                }
                #endregion
                #region Save Chocolates
                foreach (RepeaterItem repeaterItem in Chocolate.Items)
                {
                    CheckBox chkBox = repeaterItem.FindControl("SelectChocolate") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            int index = repeaterItem.ItemIndex;
                            HiddenField hdnSpProdId = (HiddenField)Chocolate.Items[index].FindControl("hdnChocolateProdId");
                            HiddenField hdnprice = (HiddenField)Chocolate.Items[index].FindControl("hdnChocolatePrice");
                            objOrderLines.OrderID = DecryptedOrderId;
                            objOrderLines.ProductID = Convert.ToInt32(hdnSpProdId.Value);
                            objOrderLines.Price = Convert.ToDouble(hdnprice.Value);
                            objOrderLines.Description = "Upsells";
                            objUpsellsBAL.AddUpsellsProduct(objOrderLines);
                        }
                    }
                }
                #endregion
                #region Save Balloons
                foreach (RepeaterItem repeaterItem in Balloons.Items)
                {
                    CheckBox chkBox = repeaterItem.FindControl("SelectBalloons") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            int index = repeaterItem.ItemIndex;
                            HiddenField hdnSpProdId = (HiddenField)Balloons.Items[index].FindControl("hdnBalloonsProdId");
                            HiddenField hdnprice = (HiddenField)Balloons.Items[index].FindControl("hdnBalloonsPrice");
                            objOrderLines.OrderID = DecryptedOrderId;
                            objOrderLines.ProductID = Convert.ToInt32(hdnSpProdId.Value);
                            objOrderLines.Price = Convert.ToDouble(hdnprice.Value);
                            objOrderLines.Description = "Upsells";
                            objUpsellsBAL.AddUpsellsProduct(objOrderLines);
                        }
                    }
                }
                #endregion
                #region Save Wines
                foreach (RepeaterItem repeaterItem in Wines.Items)
                {
                    CheckBox chkBox = repeaterItem.FindControl("SelectWines") as CheckBox;
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            int index = repeaterItem.ItemIndex;
                            HiddenField hdnSpProdId = (HiddenField)Wines.Items[index].FindControl("hdnWinesProdId");
                            HiddenField hdnprice = (HiddenField)Wines.Items[index].FindControl("hdnWinesPrice");
                            objOrderLines.OrderID = DecryptedOrderId;
                            objOrderLines.ProductID = Convert.ToInt32(hdnSpProdId.Value);
                            objOrderLines.Price = Convert.ToDouble(hdnprice.Value);
                            objOrderLines.Description = "Upsells";
                            objUpsellsBAL.AddUpsellsProduct(objOrderLines);
                        }
                    }
                }
                #endregion
                if (Common.IsLoggedIn() != true)
                {
                    //Response.Redirect("m_Login.aspx?s=" + Request.QueryString["s"], true);
                    //Response.End();
                   // HttpContext.Current.Response.Redirect("m_Login.aspx?s=" + Request.QueryString["s"], false);
                    Response.Redirect("m_customerdetails.aspx?s=" + Request.QueryString["s"] + "&guest=yes", false);
                    //var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("~/Checkout/m_login.aspx?s=" + Request.QueryString["s"]));
                    //Response.Redirect(url);
                }
                else if (Common.IsLoggedIn() == true)
                {
                    Response.Redirect("m_RecipientDetails.aspx?s=" + Request.QueryString["s"], false);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            
            }
        }
        protected void ContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx?s=" + Request.QueryString["s"],false);
            
        }
        protected void btnHidden_Click(object sender, EventArgs e)
        {

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            DisplayBasketCount.Update();
            ModifyBasket.Update();
        }
        protected void SaveDates_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
          //  Response.Redirect(Request.RawUrl);
          //  Response.Redirect("~/checkout/m_upsells.aspx?s=" + Request.QueryString["s"], true);
        }
        protected void Voucher_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
        
      
    }
}