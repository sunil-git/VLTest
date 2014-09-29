using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using System.Data;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Bal.Common;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace Serenataflowers.Checkout
{
    public partial class m_recipientdetails : System.Web.UI.Page
    {
        RecipientDetailsBAL objRecipientDetails;
        CustomerDetailsBAL objCustomerDetails;
        string OrderID;
        public bool IsCardSectionVisible { get; private set; }
        public bool IsSelectFreeCard { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            

            if (!IsPostBack)
            {
                if (Request.QueryString["s"] != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();
                    
                    BindOccasion();
                   
                    //SameAsInvoiceAddress_CheckedChanged(null, null);
                    if (Common.IsLoggedIn() == true)
                    {
                        BindAddressBook(Convert.ToInt32(Request.Cookies["CustomerID"].Value));
                    }
                    else
                    {
                        divAddressBook.Style.Add("display", "none");
                        //BindRecipientAddressBook(OrderID);
                    }
                    Common.GetAllCountries(RecipientCountry);                  

                    DisplayDeliveryDetails(OrderID);
                    BindCardProducts();
                    //BindSuggestedDates(OrderID);
                    int messageLength = objRecipientDetails.GetCardMessageLength(OrderID);
                    hdnMaxLength.Value = Convert.ToString(messageLength);
                    mycounter.InnerText = hdnMaxLength.Value + " characters left.";
                    SetProgressBarLinks();
                    Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "Step2");

                    GetGiftMessage(OrderID);
                    GetDeliveryInstructions(OrderID);
                    DisplayHouseNo();
                }
              
            }
            new Common().CheckCutOffTime(MasterBody, Common.GetOrderIdFromQueryString());
        }

        private void GetGiftMessage(string orderId)
        {
            try
            {
                txtGiftMsg.InnerText = new RecipientDetailsBAL().GetCardMessage(orderId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetDeliveryInstructions(string orderId)
        {
            try
            {
                string strDelIns = new RecipientDetailsBAL().GetDeliveryInstructions(orderId);
                if (!string.IsNullOrEmpty(strDelIns))
                {
                    if (strDelIns.StartsWith("Leave with neighbour"))
                    {
                        drpDelIns.SelectedValue = "Leave with neighbour";
                        txthouseNumber.Value = strDelIns.Replace("Leave with neighbour", "").Trim();
                    }
                    else
                    {
                        drpDelIns.SelectedValue = strDelIns;
                    }
                    //chkDelIns.Checked = true;
                    //divDelIns.Style["display"] = "block";
                }
                else
                {
                    //chkDelIns.Checked = true;
                    //divDelIns.Style["display"] = "block";
                    drpDelIns.SelectedValue = "Please select an option";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DisplayHouseNo()
        {
            if (Request.Cookies["selectedDelIns"] != null)
            {
                SetSelectedIndex(ref drpDelIns, Convert.ToString(Request.Cookies["selectedDelIns"].Value));
            }
            else
            {
                this.drpDelIns.SelectedIndex = 0;
            }
            divHouseNo.Style["display"] = drpDelIns.SelectedValue.Equals("Leave with neighbour") ? "block" : "none";
        }

        private void SetProgressBarLinks()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                ancArrowCust.HRef = "m_customerdetails.aspx?s=" + Request.QueryString["s"];
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            DisplayBasketCount.Update();
            ModifyBasket.Update();
        }
        protected void SaveDates_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
        protected void Voucher_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }

        public void BindAddressBook(int CustomerID)
        {
            List<AddressInfo> objListAddress = new List<AddressInfo>();
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            try
            {
                objListAddress = objRecipientDetails.GetCustomerAddressBook(CustomerID);
                if (objListAddress != null && objListAddress.Count > 0)
                {
                    divAddressBook.Style.Add("display", "block");
                    drpAddressBook.DataSource = objListAddress;
                    drpAddressBook.DataTextField = "FullName";
                    drpAddressBook.DataValueField = "AddressID";
                    drpAddressBook.DataBind();

                    drpAddressBook.Items.Insert(0, new ListItem("--Choose from address book--", "0"));
                    drpAddressBook.SelectedIndex = 0;
                }
                else
                {
                    divAddressBook.Style.Add("display", "none");
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        public void BindRecipientAddressBook(string OrderId)
        {
            List<AddressInfo> objListAddress = new List<AddressInfo>();
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRec = new RecipientInfo();
            try
            {
                int custId = objRecipientDetails.GetCustomerIdByOrderId(OrderId);
                objListAddress = objRecipientDetails.GetCustomerAddressBook(custId);
                if (objListAddress != null && objListAddress.Count > 0)
                {
                    divAddressBook.Style.Add("display", "block");
                    drpAddressBook.DataSource = objListAddress;
                    drpAddressBook.DataTextField = "FullName";
                    drpAddressBook.DataValueField = "AddressID";
                    drpAddressBook.DataBind();

                    drpAddressBook.Items.Insert(0, new ListItem("--Please select an address from below--", "0"));


                    objRec = objRecipientDetails.GetDeliveryDetails(OrderId);
                    if (objRec != null)
                    {
                        System.Text.StringBuilder FullName = new System.Text.StringBuilder();
                        if (!string.IsNullOrEmpty(objRec.Title))
                        {
                            FullName.Append(objRec.Title);
                        }
                        if (!string.IsNullOrEmpty(objRec.FirstName))
                        {
                            FullName.Append(" " + objRec.FirstName);
                        }
                        if (!string.IsNullOrEmpty(objRec.LastName))
                        {
                            FullName.Append(" " + objRec.LastName);
                        }
                        if (!string.IsNullOrEmpty(objRec.HouseNo))
                        {
                            FullName.Append(" : " + objRec.HouseNo);
                        }
                        if (!string.IsNullOrEmpty(objRec.Street))
                        {
                            FullName.Append(" " + objRec.Street);
                        }
                        if (!string.IsNullOrEmpty(objRec.PostCode))
                        {
                            FullName.Append(" " + objRec.PostCode);
                        }
                        if (!string.IsNullOrEmpty(objRec.Town))
                        {
                            FullName.Append(" " + objRec.Town);
                        }
                        // CustomerAddressBook.SelectedIndex = CustomerAddressBook.SelectedIndex = CustomerAddressBook.Items.IndexOf(CustomerAddressBook.Items.FindByText(FullName.ToString()));
                        //drpAddressBook.SelectedIndex = drpAddressBook.Items.IndexOf(drpAddressBook.Items.FindByText(FullName.ToString()));
                        drpAddressBook.SelectedIndex = 0;
                    }
                }
                else
                {
                    divAddressBook.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void CapturePCAData_Click(object sender, EventArgs e)
        {
            //int CustomerId = 0;
            //CustomerInfo objCustomerInfo = new CustomerInfo();
            RecipientInfo objRecDtl = new RecipientInfo();
            //CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            objRecipientDetails = new RecipientDetailsBAL();
            Encryption objEncryption = new Encryption();
            string OrderID = Common.GetOrderIdFromQueryString();
            try
            {
                
                   
                    //CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                    //objCustomerInfo.CustomerId = CustomerId;

                    string name = txtRecipientName.Value.Trim();
                    string firstName = string.Empty;
                    string lastName = string.Empty;
                    if (name.Contains(' '))
                    {
                        firstName = name.Substring(0, name.IndexOf(' '));
                        lastName = name.Substring(name.IndexOf(' ') + 1);
                    }
                    else
                    {
                        firstName = name;
                    }
                    


                    objRecDtl.OrderId = OrderID;
                    objRecDtl.FirstName = firstName;
                    objRecDtl.LastName = lastName;
                    objRecDtl.HouseNo = hdnPCAHouseNumber.Value;
                    objRecDtl.Street = hdnPCAStreet.Value;
                    objRecDtl.District = hdnPCADistrict.Value;
                    objRecDtl.Town = hdnPCACity.Value;
                    objRecDtl.PostCode = hdnPCAPostCode.Value;
                    objRecDtl.Organisation = hdnPCAOrganisation.Value;
                    CommonBal objBAl = new CommonBal();
                    int country = objBAl.GetCountryIdCountryCode(hdnPCACountry.Value);
                    objRecDtl.CountryID = country;
                    objRecDtl.AddressVerified = 1;

                    recpientOrganization.Value = hdnPCAOrganisation.Value;
                    recipientAddress1.Value = hdnPCAHouseNumber.Value;
                    recipientAddress2.Value = hdnPCAStreet.Value;
                    recipientAddress3.Value = hdnPCADistrict.Value;
                    recipientTown.Value = hdnPCACity.Value;
                    recipientPostCode.Value = hdnPCAPostCode.Value;

                    objRecipientDetails.EditDeliveryAddress(objRecDtl);

                    spnOrganization.InnerHtml = hdnPCAOrganisation.Value;
                    spnHouseNumber.InnerHtml = hdnPCAHouseNumber.Value;
                    //spnStreet.InnerHtml = objRecInfo.Street;
                    spnDistrict.InnerHtml = hdnPCADistrict.Value;
               

                    CustomerPCACountry.InnerHtml = hdnPCACountry.Value;
                    CustomerPCACity.InnerHtml = hdnPCACity.Value;
                    CustomerPCAPostCode.InnerHtml = hdnPCAPostCode.Value;
                    CustomerPCAAddress.InnerHtml = hdnPCAAddress.Value;
                    CustomerFullName.InnerHtml = txtRecipientName.Value.Trim();
                    content2.Style.Add("display", "block");
                    content1.Style.Add("display", "none");

                    if (country == 215)
                    {
                        CheckDBPostCode(OrderID, CapturePCAData);
                    }
                    
                    
                    //ScriptManager.RegisterClientScriptBlock(CapturePCAData, CapturePCAData.GetType(), "34trt213", "displaySecondScreen();", true);
                    //Span1.InnerText = hdnPCAHouseNumber.Value;
                    //updContent2.Update();
                //}
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void SameAsInvoiceAddress_CheckedChanged(object sender, EventArgs e)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            CustomerInfo objCustomerInfo = new CustomerInfo();
            try
            {
                if (SameAsInvoiceAddress.Checked)
                {
                    Encryption objEncryption = new Encryption();
                    string OrderID = Common.GetOrderIdFromQueryString();

                    objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);
                    RecipientInfo objRecDtl = new RecipientInfo();
                    objRecDtl.OrderId = OrderID;

                    if (!string.IsNullOrEmpty(txtRecipientName.Value))
                    {

                        string name = txtRecipientName.Value;
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = name;
                        }
                        objRecDtl.FirstName = firstName;
                        objRecDtl.LastName = lastName;
                    }

                    objRecDtl.Organisation = objCustomerInfo.Organisation;
                    spnOrganization.InnerHtml = objCustomerInfo.Organisation;

                    objRecDtl.HouseNo = objCustomerInfo.HouseNo;
                    spnHouseNumber.InnerHtml = objCustomerInfo.HouseNo;
                   

                    objRecDtl.Street = objCustomerInfo.Street;
                    spnStreet.InnerHtml = objCustomerInfo.Street;

                    objRecDtl.District = objCustomerInfo.District;
                    spnDistrict.InnerHtml = objCustomerInfo.District;

                    objRecDtl.Town = objCustomerInfo.Town;
                    hdnPCACity.Value = objCustomerInfo.Town;

                    objRecDtl.PostCode = objCustomerInfo.PostCode;
                    hdnPCAPostCode.Value = objCustomerInfo.PostCode;

                    objRecDtl.CountryID = objCustomerInfo.CountryID;
                    objRecDtl.RecipientMobile = objCustomerInfo.UKMobile;
                    objRecipientDetails = new RecipientDetailsBAL();
                    ViewState["postcode"] = objCustomerInfo.PostCode;
                    objRecDtl.AddressVerified = 0;

                    recpientOrganization.Value = objRecDtl.Organisation;
                    recipientAddress1.Value = objRecDtl.HouseNo;
                    recipientAddress2.Value = objRecDtl.Street;
                    recipientAddress3.Value = objRecDtl.District;
                    recipientTown.Value = objRecDtl.Town ;
                    recipientPostCode.Value = objRecDtl.PostCode;

                    System.Text.StringBuilder custAddress = new System.Text.StringBuilder();

                    if (!string.IsNullOrEmpty(objCustomerInfo.Organisation))
                    {
                        custAddress.Append(objCustomerInfo.Organisation + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.HouseNo))
                    {
                        custAddress.Append(objCustomerInfo.HouseNo + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Street))
                    {
                        custAddress.Append(objCustomerInfo.Street + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.District))
                    {
                        custAddress.Append(objCustomerInfo.District + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Town))
                    {
                        custAddress.Append(objCustomerInfo.Town + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.PostCode))
                    {
                        custAddress.Append(objCustomerInfo.PostCode);
                    }

                    FillSuggestedAddress(custAddress.ToString());
                    string TheNewString = custAddress.ToString();
                    TheNewString = TheNewString.Substring(0, custAddress.Length - 1);                    
                   
                    CustomerFullName.InnerHtml = objRecDtl.FirstName + " " + objRecDtl.LastName;
                    CustomerPCAAddress.InnerText = TheNewString;
                    CustomerPCACity.InnerText = objRecDtl.Town;
                    CustomerPCAPostCode.InnerText = objRecDtl.PostCode;
                    CustomerPCACountry.InnerText = new CommonBal().GetCountryNameByCountryCode(objRecDtl.CountryID);
                                    

                    if (objRecDtl.CountryID == 215)
                    {
                        CheckDBPostCode(OrderID, SameAsInvoiceAddress);
                    }
                    else
                    {
                        objRecipientDetails.EditDeliveryAddress(objRecDtl);
                        content2.Style.Add("display", "block");
                        content1.Style.Add("display", "none");
                        ScriptManager.RegisterClientScriptBlock(SameAsInvoiceAddress, SameAsInvoiceAddress.GetType(), "34trt213", "displaySecondScreen();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        protected void ManualAddress_Click(object sender, EventArgs e)
        {
            hdnCustomerFirstname.Value = txtRecipientName.Value;
            RecipientName.Value = txtRecipientName.Value;
            content2.Style.Add("display", "block");
            content1.Style.Add("display", "none");
            content3.Style.Add("display", "block");
            divEditBox.Style.Add("display", "none");
            //ScriptManager.RegisterClientScriptBlock(ManualAddress, ManualAddress.GetType(), "34trt213", "displaySecondScreen();", true);
        }
        protected void drpAddressBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecipientDetailsBAL objAddressDetails = new RecipientDetailsBAL();
            RecipientInfo objAddressInfo = new RecipientInfo();
            try
            {

                string OrderID = Common.GetOrderIdFromQueryString();


                int AddrId = Convert.ToInt32(drpAddressBook.SelectedValue);
                objAddressInfo = objAddressDetails.GetCustomerAddressBookbyAddressID(AddrId);
                string name = string.Empty;
                if (objAddressInfo != null)
                {
                    //DataSet dscountry = new DataSet();
                    //dscountry = objRecipientDetails.GetProductCountryByOrderId(OrderID);
                    //if (dscountry != null && Convert.ToInt32(dscountry.Tables[0].Rows[0]["countryID"]) == objAddressInfo.CountryID)
                    //{

                    //if (CheckDBPostCode(OrderID, objAddressInfo.PostCode, drpAddressBook) == true)
                    //{
                    objAddressInfo.OrderId = OrderID;
                    objAddressInfo.AddressVerified = 1;
                    objAddressDetails.EditDeliveryAddress(objAddressInfo);

                    System.Text.StringBuilder custAddress = new System.Text.StringBuilder();

                    if (!string.IsNullOrEmpty(objAddressInfo.Organisation))
                    {
                        custAddress.Append(objAddressInfo.Organisation + ",");
                    }
                    if (!string.IsNullOrEmpty(objAddressInfo.HouseNo))
                    {
                        custAddress.Append(objAddressInfo.HouseNo + ",");
                    }
                    if (!string.IsNullOrEmpty(objAddressInfo.Street))
                    {
                        custAddress.Append(objAddressInfo.Street + ",");
                    }
                    if (!string.IsNullOrEmpty(objAddressInfo.District))
                    {
                        custAddress.Append(objAddressInfo.District + ",");
                    }
                    string TheNewString = custAddress.ToString();
                    TheNewString = TheNewString.Substring(0, custAddress.Length - 1);

                    CustomerFullName.InnerHtml = objAddressInfo.FirstName + " " + objAddressInfo.LastName;
                    
                    CustomerPCAAddress.InnerText = TheNewString;
                    CustomerPCACity.InnerText = objAddressInfo.Town;
                    CustomerPCAPostCode.InnerText = objAddressInfo.PostCode;

                    ViewState["Organization"] = objAddressInfo.Organisation;
                    ViewState["houseno"] = objAddressInfo.HouseNo;
                    ViewState["district"] = objAddressInfo.District;
                    ViewState["street"] = "";


                    CustomerPCACountry.InnerText = new CommonBal().GetCountryNameByCountryCode(objAddressInfo.CountryID);
                    content2.Style.Add("display", "block");
                    content1.Style.Add("display", "none");
                    ScriptManager.RegisterClientScriptBlock(SameAsInvoiceAddress, SameAsInvoiceAddress.GetType(), "34trt213", "displaySecondScreen();", true);
                  






                    //RefreshParentWindow(OrderID);
                    //BindRecipientAddressBook(OrderID);
                    //CustomerAddressBook.SelectedIndex = CustomerAddressBook.Items.IndexOf(CustomerAddressBook.Items.FindByValue(AddrId.ToString()));
                    //DisplayDeliveryDetails(OrderID);
                    //divRecfinal.Style.Add("display", "block");
                    //divRecfinalEditor.Style.Add("display", "block");
                    //divContinuePayment.Style.Add("display", "block");

                    //divrecInitial.Style.Add("display", "none");
                    //ScriptManager.RegisterClientScriptBlock(drpAddressBook, drpAddressBook.GetType(), "tesdsdt123", "DisplayFinalDiv();", true);
                    //}
                    //}
                    //else
                    //{
                    //    lblCountryProduct.Value = "you have added a product that can only be delivered in " + Convert.ToString(dscountry.Tables[0].Rows[0]["Country"]) + ". if you want a delivery for " + objAddressInfo.CountryName + "  please go to our internaitonal section here www.serenataflowers.com/international";
                    //    ScriptManager.RegisterClientScriptBlock(drpAddressBook, drpAddressBook.GetType(), "test21222443", "revealProdCountryMessage('" + lblCountryProduct.Value + "');", true);
                    //}
                }
                //DisplayStartOverButton();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void RecipientCountry_SelectedIndexChanged(object sender, EventArgs e)
        { 
        
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                Response.Redirect("m_customerdetails.aspx?s=" + Request.QueryString["s"], false);
            }
        }
        private void BindOccasion()
        {
            DataSet dsOccasion = new DataSet();
            objRecipientDetails = new RecipientDetailsBAL();
            string OrderID = Common.GetOrderIdFromQueryString();
            try
            {
                dsOccasion = objRecipientDetails.GetOccasionByOrderID(OrderID);

                if (dsOccasion != null && dsOccasion.Tables.Count > 0)
                {
                    DataTable dtOccassion = dsOccasion.Tables[0];
                    Add_Extra_Column_To_DataTable(ref dtOccassion, typeof(string), "DropdownValue");
                    Occasions.DataSource = dsOccasion;
                    Occasions.DataTextField = "Description";
                    Occasions.DataValueField = "DropdownValue";
                    Occasions.DataBind();

                    

                    
                    this.Occasions.Items.Insert(0, new ListItem("---Choose occasion---", "20|0"));

                    if (Request.Cookies["SelectedOccasion"] != null)
                    {
                        SetSelectedIndex(ref Occasions, Convert.ToString(Request.Cookies["SelectedOccasion"].Value));

                    }
                    else
                    {
                        this.Occasions.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        private void BindCardProducts()
        {
            try
            {

                objRecipientDetails = new RecipientDetailsBAL();
                int selectedProductId = 0;
                string orderId = Common.GetOrderIdFromQueryString();

                string[] arrKeys = Occasions.SelectedValue.Split('|');
                int intSelOccasionID = Convert.ToInt32(arrKeys[1]);

                List<OccassionCard> lstCardProducts = objRecipientDetails.GetCardProducts(orderId, intSelOccasionID, ref selectedProductId);
                if (selectedProductId == 0)
                {
                    selectedProductId = Convert.ToInt32(ConfigurationManager.AppSettings["FreeCardProductId"]);
                }
                else
                {
                    lstCardProducts.ForEach(p => p.NoCard = 0);
                }

                //Setting CssClass for all cards
               // lstCardProducts.ForEach(p => p.CssClass = "ca-item-main");

                if (IsSelectFreeCard) selectedProductId = Convert.ToInt32(ConfigurationManager.AppSettings["FreeCardProductId"]);

                OccassionCard objCard = lstCardProducts.Where(p => p.ProductId == selectedProductId).FirstOrDefault();

                //if (objCard != null)
                //{
                //    lstCardProducts.Remove(objCard);
                //    lstCardProducts.Insert(0, objCard);

                  objCard.IsCheckedString = "checked";
                //   // objCard.IsVisible = false;
                //    // IsVisible
                //    //To change the CssClass for selected product
                //    //objCard.CssClass = "ca-item-main selected";
                //}



                if (lstCardProducts != null && lstCardProducts.Count > 0)
                {
                    rptCards.DataSource = lstCardProducts;
                    rptCards.DataBind();


                    ZoomGorgeousVase.DataSource = lstCardProducts;
                    ZoomGorgeousVase.DataBind();

                    //string strPIONVariables = "<!-- PION VARIABLES\n\n";
                    //strPIONVariables += "serenata.upsellsProdlist=";
                    //strPIONVariables += Common.IsBlank(GetPIONVariables(lstCardProducts));
                    //Literal ltCtrl = (Literal)Master.FindControl("ltrSpot");
                    //ltCtrl.Value = strPIONVariables;

                    //pnlCardSection.Style.Add("display", "block");
                    //lblChooseCard.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindCardProducts(int selectProducId)
        {
            try
            {

                objRecipientDetails = new RecipientDetailsBAL();
                int selectedProductId = 0;
                string orderId = Common.GetOrderIdFromQueryString();

                string[] arrKeys = Occasions.SelectedValue.Split('|');
                int intSelOccasionID = Convert.ToInt32(arrKeys[1]);

                List<OccassionCard> lstCardProducts = objRecipientDetails.GetCardProducts(orderId, intSelOccasionID, ref selectedProductId);
                if (selectedProductId == 0)
                {
                    selectedProductId = Convert.ToInt32(ConfigurationManager.AppSettings["FreeCardProductId"]);
                }
                else
                {
                    lstCardProducts.ForEach(p => p.NoCard = 0);
                }
                selectProducId = 86557;
                //Setting CssClass for all cards
                // lstCardProducts.ForEach(p => p.CssClass = "ca-item-main");

                //if (IsSelectFreeCard) selectedProductId = Convert.ToInt32(ConfigurationManager.AppSettings["FreeCardProductId"]);

                OccassionCard objCard = lstCardProducts.Where(p => p.ProductId == selectProducId).FirstOrDefault();

                if (objCard != null)
                {
                    lstCardProducts.Remove(objCard);
                    lstCardProducts.Insert(0, objCard);

                    objCard.IsCheckedString = "checked";
                    // objCard.IsVisible = false;
                    // IsVisible
                    //To change the CssClass for selected product
                    //objCard.CssClass = "ca-item-main selected";
                }



                if (lstCardProducts != null && lstCardProducts.Count > 0)
                {
                    rptCards.DataSource = lstCardProducts;
                    rptCards.DataBind();

                    ZoomGorgeousVase.DataSource = lstCardProducts;
                    ZoomGorgeousVase.DataBind();

                    //string strPIONVariables = "<!-- PION VARIABLES\n\n";
                    //strPIONVariables += "serenata.upsellsProdlist=";
                    //strPIONVariables += Common.IsBlank(GetPIONVariables(lstCardProducts));
                    //Literal ltCtrl = (Literal)Master.FindControl("ltrSpot");
                    //ltCtrl.Value = strPIONVariables;

                    //pnlCardSection.Style.Add("display", "block");
                    //lblChooseCard.Visible = true;
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void Add_Extra_Column_To_DataTable(ref DataTable datatable, Type type, string ColumnName)
        {
            datatable.Columns.Add(ColumnName, type);

            foreach (DataRow dr in datatable.Rows)
                dr[ColumnName] = Convert.ToString(dr["relatedcard"]) + "|" + Convert.ToString(dr["idOccasion"]);
        }
        private void SetSelectedIndex(ref DropDownList ddl, string value)
        {
            ListItem li = ddl.Items.Cast<ListItem>().Where(x => x.Value.Contains(value)).LastOrDefault();
            ddl.SelectedIndex = ddl.Items.IndexOf(li);
        }
        protected void rptCards_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ADD")
            {

                try
                {
                    //string[] arrKeys = Convert.ToString(e.CommandArgument).Split('|');
                    //AddMessageCard(arrKeys);
                }
                catch (Exception ex)
                {
                   // ErrorLog.Error(ex);
                }

            }
        }
        protected void drpOccasions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string orderId = Common.GetOrderIdFromQueryString();
                if (Occasions.SelectedValue != "20|0")
                {
                    string[] arrKeys = Occasions.SelectedValue.Split('|');

                    int refSelectedProductId = 0; //GetSelectedMessageCardIDByOrderID();
                    int relatedCardId = Convert.ToInt32(arrKeys[0]);

                    int intSelOccasionID = Convert.ToInt32(arrKeys[1]);
                    objRecipientDetails = new RecipientDetailsBAL();
                    try
                    {
                        objRecipientDetails.UpdateOccasion(orderId, intSelOccasionID);
                    }
                    catch (Exception innEx)
                    {
                        SFMobileLog.Error(innEx);
                    }

                    
                    //Price is setting as zero for dropdown occassions.  Price will fetch from DB inside SP logic
                    arrKeys[1] = "0.00"; //Price

                    Array.Resize(ref arrKeys, arrKeys.Length + 1);
                    if (relatedCardId > 0 && refSelectedProductId == 0)
                    {
                        arrKeys[2] = "1";
                        //AddMessageCard(arrKeys);
                    }
                    else if (relatedCardId > 0 && refSelectedProductId > 0 && (relatedCardId != refSelectedProductId))
                    {
                        arrKeys[2] = "0";
                        //AddMessageCard(arrKeys);
                    }
                    Common objCommon = new Common();
                    HttpCookie cooki = new HttpCookie("SelectedOccasion", Occasions.SelectedValue);
                    //cooki.Domain = objCommon.GetSiteName();
                    Response.Cookies.Add(cooki);

                    IsSelectFreeCard = relatedCardId == 0 ? true : false;
                    hdnIsSelected.Value = IsSelectFreeCard.ToString();
                    //if (IsSelectFreeCard) BindCardProducts();
                     BindCardProducts(relatedCardId);
                     //Page.SetFocus(divMessage);
                    //BindOccasion();
                   // RefreshBaseketItems(orderId);
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private int GetSelectedMessageCardIDByOrderID()
        {
            RecipientDetailsBAL obj = new RecipientDetailsBAL();
            string orderId = Common.GetOrderIdFromQueryString();
            int refSelectedProductId = obj.GetSelectedMessageCardIDByOrderID(orderId);
            return refSelectedProductId;
        }
        private void AddMessageCard(string[] arrKeys)
        {
            string orderId = Common.GetOrderIdFromQueryString();
            if (!string.IsNullOrEmpty(orderId))
            {

                int productID = Convert.ToInt32(arrKeys[0]);
                decimal price = Convert.ToDecimal(arrKeys[1]);
                int noCard = Convert.ToInt32(arrKeys[2]);
                RecipientDetailsBAL objBal = new RecipientDetailsBAL();
                objBal.AddMessageCard(orderId, productID, price, noCard);
                BindCardProducts();
                //RefreshBaseketItems(orderId);
            }
        }
        protected void chkCardSelection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool IsShowCardSection = Convert.ToBoolean(hdnCardSelection.Value);
                if (!IsShowCardSection)
                {
                    int refSelectedProductId = GetSelectedMessageCardIDByOrderID();
                    if (refSelectedProductId > 0)
                    {
                        string[] arrKeys = new string[3];
                        arrKeys[0] = ConfigurationManager.AppSettings["FreeCardProductId"]; //Free CardID
                        arrKeys[1] = "0.00"; //PriceId
                        arrKeys[2] = "0"; //NoCard flag as zero to delete existing card id from order
                        AddMessageCard(arrKeys);
                        //Occasions.SelectedIndex = 0;
                        ChooseACard.Checked = true;
                        lblChooseCard.Style.Add("display", "none");
                        divMessage.Style.Add("display", "none");
                        //upsells.Style.Add("display", "none");
                        upsells.Visible = false;
                    }
                    else
                    {
                        ChooseACard.Checked = true;
                        //upsells.Style.Add("display", "none");
                        divMessage.Style.Add("display", "none");
                        lblChooseCard.Style.Add("display", "none");
                        upsells.Visible = false;
                    }
                }
                else
                {
                    ChooseACard.Checked = false;
                    lblChooseCard.Style.Add("display", "block");
                    divMessage.Style.Add("display", "block");
                   // upsells.Style.Add("display", "block");
                    upsells.Visible = true;
                }
                ModifyBasket.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        protected void lnkAddCardDummy_Click(object sender, EventArgs e)
        {
            try
            {

                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                string[] arrKeys = passedArgument.Split('|');
                AddMessageCard(arrKeys);
                ModifyBasket.Update();
                //Page.SetFocus(divMessage);
                ScriptManager.RegisterStartupScript(lnkAddCardDummy, lnkAddCardDummy.GetType(), "ScrollPage", "ResetScrollPosition();", true);
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        public void DisplayDeliveryDetails(string OrderId)
        {
            objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                objRecInfo = objRecipientDetails.GetDeliveryDetails(OrderId);
                string name = string.Empty;
                if (objRecInfo != null)
                {
                    if (objRecInfo.FirstName != null)
                    {
                        System.Text.StringBuilder recaddress = new System.Text.StringBuilder();

                        if (!string.IsNullOrEmpty(objRecInfo.Organisation))
                        {
                            recaddress.Append(objRecInfo.Organisation + ",");
                        }
                        if (!string.IsNullOrEmpty(objRecInfo.HouseNo))
                        {
                            recaddress.Append(objRecInfo.HouseNo + ",");
                        }
                        if (!string.IsNullOrEmpty(objRecInfo.Street))
                        {
                            recaddress.Append(objRecInfo.Street + ",");
                        }
                        if (!string.IsNullOrEmpty(objRecInfo.District))
                        {
                            recaddress.Append(objRecInfo.District + ",");
                        }


                        string TheNewString = recaddress.ToString();
                        if (recaddress.Length > 0)
                        {
                            TheNewString = TheNewString.Substring(0, recaddress.Length - 1);
                        }

                        CustomerFullName.InnerHtml = objRecInfo.FirstName + " " + objRecInfo.LastName;
                        CustomerPCAAddress.InnerText = TheNewString;
                        CustomerPCACity.InnerText = objRecInfo.Town;
                        CustomerPCAPostCode.InnerText = objRecInfo.PostCode;
                        CustomerPCACountry.InnerText = new CommonBal().GetCountryNameByCountryCode(objRecInfo.CountryID);
                        spnOrganization.InnerHtml = objRecInfo.Organisation;
                        spnHouseNumber.InnerHtml = objRecInfo.HouseNo;
                        spnStreet.InnerHtml = objRecInfo.Street;
                        spnDistrict.InnerHtml = objRecInfo.District;
                        hdnPCACity.Value = objRecInfo.Town;
                        hdnPCAPostCode.Value = objRecInfo.PostCode;

                        RecipientCountry.SelectedIndex = RecipientCountry.Items.IndexOf(RecipientCountry.Items.FindByValue(objRecInfo.CountryID.ToString()));
                        if (objRecInfo.CountryID == 215)
                        {
                            hdnInternational.Value = "no";
                        }
                        else
                        {
                            hdnInternational.Value = "yes";
                        }

                        content2.Style.Add("display", "block");
                        content1.Style.Add("display", "none");
                    }
                    else
                    {
                        content2.Style.Add("display", "none");
                        content1.Style.Add("display", "block");
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        #region popup suggested Dates
        protected void ChangeDate_Click(object sender, EventArgs e)
        {
            try
            {
                DispatchesInfo objDispatches = new DispatchesInfo();
                OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
                DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
                if (objDispatches != null)
                {
                    DispatchesBAL objDelivery = new DispatchesBAL();
                    string orderId = string.Empty;
                    orderId = Common.GetOrderIdFromQueryString();

                    objDispatches.OrderID = orderId;
                    objDispatches.DelOptionID = Convert.ToInt32(rbtnLstsuggestedDeliveryOptions.SelectedValue);
                    objDispatches.DeliveryDate = DateTime.ParseExact(ddlSugestedDeliveryDate.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DispatchesInfo tmpDispatches = new DispatchesInfo();
                    tmpDispatches = objDelivery.GetDeliveryDetailsByDelOptionID(objDispatches.DelOptionID);
                    objDispatches.CarrierID = tmpDispatches.CarrierID;
                    objDispatches.DeliveryTime = tmpDispatches.DeliveryTime;
                    objDispatches.DeliveryPrice = tmpDispatches.DeliveryPrice;
                    objDelivery.ScheduleDispatch(objDispatches);
                    updSuggestetdate.Update();

                    string recName = string.Empty;
                    if(!string.IsNullOrEmpty(txtRecipientName.Value))
                    {
                        recName = txtRecipientName.Value;
                    }
                    else{
                        recName = RecipientName.Value;
                    }
                    


                    RecipientInfo objRecDtl = new RecipientInfo();
                    objRecDtl.OrderId = orderId;


                    if (recName != "")
                    {
                        recName = recName.Trim();
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (recName.Contains(' '))
                        {
                            firstName = recName.Substring(0, recName.IndexOf(' '));
                            lastName = recName.Substring(recName.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = recName;
                        }
                        objRecDtl.FirstName = firstName;
                        objRecDtl.LastName = lastName;
                    }


                    if (SameAsInvoiceAddress.Checked)
                    {
                        CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                        CustomerInfo objCustomerInfo = new CustomerInfo();
                        objCustomerInfo = objCustomerDetails.GetBillingDetails(orderId);
                        objRecDtl.Organisation = objCustomerInfo.Organisation;
                        objRecDtl.HouseNo = objCustomerInfo.HouseNo;
                        objRecDtl.Street = objCustomerInfo.Street;
                        objRecDtl.District = objCustomerInfo.District;
                        objRecDtl.Town = objCustomerInfo.Town;
                        objRecDtl.PostCode = objCustomerInfo.PostCode;
                        objRecDtl.CountryID = objCustomerInfo.CountryID;
                        objRecDtl.RecipientMobile = objCustomerInfo.UKMobile;
                    }
                    else
                    {
                        objRecDtl.Organisation = recpientOrganization.Value;
                        objRecDtl.HouseNo = recipientAddress1.Value;
                        objRecDtl.Street = recipientAddress2.Value;
                        objRecDtl.District = recipientAddress3.Value;
                        objRecDtl.Town = recipientTown.Value;
                        objRecDtl.PostCode = recipientPostCode.Value;
                        objRecDtl.CountryID = Convert.ToInt32(RecipientCountry.SelectedItem.Value);
                    }

                    objRecipientDetails = new RecipientDetailsBAL();
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);


                    content2.Style.Add("display", "block");
                    content1.Style.Add("display", "none");
                    ScriptManager.RegisterClientScriptBlock(ChangeDate, ChangeDate.GetType(), "34trt213", "displaySecondScreen();", true);


                    ModifyDeliveryDate.Update();
                    ModifyBasket.Update();

                    DisplayDeliveryDetails(orderId);
                }

                //ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                //Response.Redirect("confirmation.aspx?s=" + Request.QueryString["s"], false);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private void DeleteOrderIdCookies()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            try
            {
                Common.RemoveCookie("Order_Id", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);
                Common.RemoveCookie("QunatityVerified", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);
                Common.RemoveCookie("SelectedOccasion", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);
                Common.RemoveCookie("CustomerID", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);

            }
            catch (Exception ex)
            {
               SFMobileLog.Error(ex);
            }
        }
        protected void CancelOrder_Click(object sender, EventArgs e)
        {

            DeleteOrderIdCookies();
            //ScriptManager.RegisterStartupScript(CancelOrder, CancelOrder.GetType(), "afsdflertHi", "Closesuggesteddelivery();", false);
            Response.Redirect(ConfigurationManager.AppSettings["RootURL"], false);

        }
        private void BindDeliveryDates(int productId)
        {
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            Common objCommon = new Common();


            try
            {


                List<DeliveryTimeInfo> lstDeliveryDateItems = new List<DeliveryTimeInfo>();
                lstDeliveryDateItems = objDelivery.GetDeliveryDatesByProductId(productId);
                if (lstDeliveryDateItems != null)
                {
                    ddlSugestedDeliveryDate.DataSource = lstDeliveryDateItems;
                    ddlSugestedDeliveryDate.DataTextField = "DeliveryDate";
                    ddlSugestedDeliveryDate.DataValueField = "DateValue";
                    ddlSugestedDeliveryDate.DataBind();
                }



            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        protected void ddlSugestedDeliveryDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            Common objCommon = new Common();
            Encryption objEncryption = new Encryption();
            int prodId = 0;
            try
            {
                string OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                string strRtn = objDelivery.GetmainProductByOrderId(OrderID);
                if (strRtn != null)
                {
                    prodId = Convert.ToInt32(strRtn);
                }
                string nameOfDay = objCommon.ConvertDateFormateToNameOfDay(ddlSugestedDeliveryDate.SelectedValue);
                BindDeliveryOptionsBasedOnDate(nameOfDay, prodId, ddlSugestedDeliveryDate.SelectedValue);
                updSuggestetdate.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindDeliveryOptionsBasedOnDate(string day, int productId, string selectedDate)
        {
            Common objCommon = new Common();
            List<DeliveryTimeInfo> lstDeliveryDataOptionsItems = new List<DeliveryTimeInfo>();
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();

            try
            {



                lstDeliveryDataOptionsItems = objDelivery.GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);
                if (lstDeliveryDataOptionsItems.Count >= 1)
                {
                    rbtnLstsuggestedDeliveryOptions.Items.Clear();
                    rbtnLstsuggestedDeliveryOptions.DataSource = lstDeliveryDataOptionsItems;
                    rbtnLstsuggestedDeliveryOptions.DataTextField = "OptionName";
                    rbtnLstsuggestedDeliveryOptions.DataValueField = "Id";
                    rbtnLstsuggestedDeliveryOptions.DataBind();
                    rbtnLstsuggestedDeliveryOptions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        private void BindSuggestedDates(string orderid)
        {
            string NextDeliveryDate = string.Empty;
            DispatchesBAL objDelivery = new DispatchesBAL();

            DataSet dsPostCodeDesc = new DataSet();
            RecipientInfo objRecInfo = new RecipientInfo();
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            try
            {
               
               
                   
                    int productID = 0;
                    string strRtn = objDelivery.GetmainProductByOrderId(orderid);
                    if (strRtn != null)
                    {
                        int productId = Convert.ToInt32(strRtn);

                        BindDeliveryDates(productId);

                    }
                    objRecInfo = objRecipientDetails.GetDeliveryDetails(orderid);
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(objRecInfo.PostCode, orderid);
                    if (dsPostCodeDesc != null && dsPostCodeDesc.Tables.Count > 0)
                    {
                        if (Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ValidPC"]) != "1")
                        {
                            //string message = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ReasonDesc"]);
                            NextDeliveryDate = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["NextDeliveryDate"]);
                            //string Delivery = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["Delivery"]);
                            if (NextDeliveryDate != null)
                            {
                                postcodeMessage.InnerText = "Unfortunately we cant delivery to {" + objRecInfo.PostCode + "} for your selected date. Please select another delivery date from the dropdown.";
                            }
                        }


                    }  

                    if (NextDeliveryDate != null)
                    {
                        ddlSugestedDeliveryDate.SelectedIndex = ddlSugestedDeliveryDate.Items.IndexOf(ddlSugestedDeliveryDate.Items.FindByValue(NextDeliveryDate));
                    }
                    ddlSugestedDeliveryDate_SelectedIndexChanged(null, null);
              
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindDeliveryDates(int productId, DateTime suggestedDates)
        {
            DispatchesBAL objDelivery = new DispatchesBAL();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            Common objCommon = new Common();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();

            try
            {


                List<DeliveryTimeInfo> lstDeliveryDateItems = new List<DeliveryTimeInfo>();
                lstDeliveryDateItems = objDelivery.GetSuggetedDates(productId, suggestedDates);
                if (lstDeliveryDateItems != null)
                {
                    ddlSugestedDeliveryDate.DataSource = lstDeliveryDateItems;
                    ddlSugestedDeliveryDate.DataTextField = "DeliveryDate";
                    ddlSugestedDeliveryDate.DataValueField = "DateValue";
                    ddlSugestedDeliveryDate.DataBind();
                }

                ddlSugestedDeliveryDate_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }  
        private void CheckDBPostCode(string OrderID, CheckBox objChkbox)
        {
            DataSet dsPostCodeDesc = new DataSet();
              DispatchesBAL objDelivery = new DispatchesBAL();
            try
            {
                if (recipientPostCode.Value != "")
                {
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(recipientPostCode.Value, OrderID);
                }
                else
                {
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(Convert.ToString(ViewState["postcode"]), OrderID);
                }
                if (dsPostCodeDesc != null && dsPostCodeDesc.Tables.Count > 0)
                {
                    if (Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ValidPC"]) != "1")
                    {
                        string message = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ReasonDesc"]);
                        string NextDeliveryDate = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["NextDeliveryDate"]);
                        string Delivery = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["Delivery"]);
                        if (recipientPostCode.Value != "")
                        {
                            lblpostcodeInfo.Text = message + " {" + recipientPostCode.Value + "}";
                        }
                        else
                        {
                            lblpostcodeInfo.Text = message + " {" + Convert.ToString(ViewState["postcode"]) + "}";
                        }
                        if (NextDeliveryDate != null && NextDeliveryDate != "")                        {
                            
                            hdnNextDelDate.Value = NextDeliveryDate;                           
                            string strRtn = objDelivery.GetmainProductByOrderId(OrderID);
                            DateTime dt;
                            dt = DateTime.ParseExact(NextDeliveryDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            BindDeliveryDates(Convert.ToInt32(strRtn), dt);
                            updSuggestetdate.Update();
                            ScriptManager.RegisterClientScriptBlock(objChkbox, objChkbox.GetType(), "test123", "revealSuggestDates('" + Convert.ToString(ViewState["postcode"]) + "');", true);
                            
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(objChkbox, objChkbox.GetType(), "test12443", "revealPostcodeMessage('" + lblpostcodeInfo.Text + "');", true);
                        }
                    }
                    else
                    {
                        CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                        CustomerInfo objCustomerInfo = new CustomerInfo();
                        objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);
                        RecipientInfo objRecDtl = new RecipientInfo();
                        objRecDtl.OrderId = OrderID;

                        //objRecDtl.FirstName = objCustomerInfo.FirstName;
                        //objRecDtl.LastName = objCustomerInfo.LastName;
                        if (!string.IsNullOrEmpty(txtRecipientName.Value))
                        {

                            string name = txtRecipientName.Value;
                            string firstName = string.Empty;
                            string lastName = string.Empty;
                            if (name.Contains(' '))
                            {
                                firstName = name.Substring(0, name.IndexOf(' '));
                                lastName = name.Substring(name.IndexOf(' ') + 1);
                            }
                            else
                            {
                                firstName = name;
                            }
                            objRecDtl.FirstName = firstName;
                            objRecDtl.LastName = lastName;
                        }

                        objRecDtl.Organisation = objCustomerInfo.Organisation;
                        hdnPCAOrganisation.Value = objCustomerInfo.Organisation;

                        objRecDtl.HouseNo = objCustomerInfo.HouseNo;
                        hdnPCAHouseNumber.Value = objCustomerInfo.HouseNo;

                        objRecDtl.Street = objCustomerInfo.Street;


                        objRecDtl.District = objCustomerInfo.District;
                        hdnPCADistrict.Value = objCustomerInfo.District;

                        objRecDtl.Town = objCustomerInfo.Town;
                        hdnPCACity.Value = objCustomerInfo.Town;

                        objRecDtl.PostCode = objCustomerInfo.PostCode;
                        hdnPCAPostCode.Value = objCustomerInfo.PostCode;


                        objRecDtl.CountryID = objCustomerInfo.CountryID;
                        objRecDtl.RecipientMobile = objCustomerInfo.UKMobile;
                        objRecipientDetails = new RecipientDetailsBAL();
                        objRecipientDetails.EditDeliveryAddress(objRecDtl);                        
                         content2.Style.Add("display", "block");
                         content1.Style.Add("display", "none");
                         ScriptManager.RegisterClientScriptBlock(SameAsInvoiceAddress, SameAsInvoiceAddress.GetType(), "34trt213", "displaySecondScreen();", true);
                        //ScriptManager.RegisterClientScriptBlock(SameAsInvoiceAddress, SameAsInvoiceAddress.GetType(), "tesft23", "DisplayFinalDiv();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private bool CheckDBPostCode(string OrderID, Button objButton)
        {
            objRecipientDetails = new RecipientDetailsBAL();
            bool validPost = true;
            DataSet dsPostCodeDesc = new DataSet();
            DispatchesBAL objDelivery = new DispatchesBAL();
            try
            {
                if (recipientPostCode.Value != "")
                {
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(recipientPostCode.Value, OrderID);
                }
                else
                {
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(hdnPCAPostCode.Value, OrderID);
                }
                if (dsPostCodeDesc != null && dsPostCodeDesc.Tables.Count > 0)
                {
                    if (Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ValidPC"]) != "1")
                    {
                        string message = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ReasonDesc"]);
                        string NextDeliveryDate = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["NextDeliveryDate"]);
                        string Delivery = Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["Delivery"]);
                        if (recipientPostCode.Value != "")
                        {
                            lblpostcodeInfo.Text = message + " {" + recipientPostCode.Value + "}";
                        }
                        else
                        {
                            lblpostcodeInfo.Text = message + " {" + hdnPCAPostCode.Value + "}";
                        }
                        if (NextDeliveryDate != null && NextDeliveryDate != "")
                        {
                            hdnNextDelDate.Value = NextDeliveryDate;
                            string strRtn = objDelivery.GetmainProductByOrderId(OrderID);
                            DateTime dt;
                            dt = DateTime.ParseExact(NextDeliveryDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            BindDeliveryDates(Convert.ToInt32(strRtn), dt);
                            updSuggestetdate.Update();
                            ScriptManager.RegisterClientScriptBlock(objButton, objButton.GetType(), "Reveal123", "revealSuggestDates('" + hdnPCAPostCode.Value + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(objButton, objButton.GetType(), "Reveal1234", "revealPostcodeMessage('" + lblpostcodeInfo.Text + "');", true);
                        }
                        validPost = false;
                    }
                    else
                    {
                        validPost = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return validPost;
        }
        #endregion
        protected void SaveRecipient_Click(object sender, EventArgs e)
        {
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            DataSet dsPostCodeDesc = new DataSet();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                Encryption objEncryption = new Encryption();
                OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                if (drpDelIns.SelectedItem.Value != "Please select an option")
                {
                    string instruct=string.Empty;
                    if (drpDelIns.SelectedItem.Value == "Leave with neighbour")
                    {
                        instruct = drpDelIns.SelectedItem.Value + " " + txthouseNumber.Value.Trim();
                    }
                    else
                    {
                        instruct = drpDelIns.SelectedItem.Value;
                    }
                 
                    objRecipientDetails.EditDeliveryInstructions(OrderID, instruct);
                }                

                objRecipientDetails.EditMessageCard(OrderID, txtGiftMsg.Value);

                System.Text.StringBuilder custAddress = new System.Text.StringBuilder();
                if (!string.IsNullOrEmpty(recipientPostCode.Value))
                {

                    if (!string.IsNullOrEmpty(RecipientName.Value))
                    {

                        string name = RecipientName.Value;
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = name;
                        }
                        objRecInfo.FirstName = firstName;
                        objRecInfo.LastName = lastName;
                    }
                    else if (!string.IsNullOrEmpty(CustomerFullName.InnerText))
                    {
                        string name = CustomerFullName.InnerText;
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = name;
                        }
                        objRecInfo.FirstName = firstName;
                        objRecInfo.LastName = lastName;

                    }                   
                    objRecInfo.OrderId = OrderID;
                    objRecInfo.Organisation = recpientOrganization.Value;
                    objRecInfo.HouseNo = recipientAddress1.Value;
                    objRecInfo.Street = recipientAddress2.Value;
                    objRecInfo.District = recipientAddress3.Value;
                    objRecInfo.Town = recipientTown.Value;
                    objRecInfo.PostCode = recipientPostCode.Value;
                    objRecInfo.AddressVerified = 0;
                    objRecInfo.CountryID = Convert.ToInt32(RecipientCountry.SelectedItem.Value);
                    //objRecipientDetails.EditDeliveryAddress(objRecInfo);
                    if (!string.IsNullOrEmpty(recpientOrganization.Value))
                    {
                        custAddress.Append(recpientOrganization.Value + ",");
                    }
                    if (!string.IsNullOrEmpty(recipientAddress1.Value))
                    {
                        custAddress.Append(recipientAddress1.Value + ",");
                    }
                    if (!string.IsNullOrEmpty(recipientAddress2.Value))
                    {
                        custAddress.Append(recipientAddress2.Value + ",");
                    }
                    if (!string.IsNullOrEmpty(recipientAddress3.Value))
                    {
                        custAddress.Append(recipientAddress3.Value + ",");
                    }
                    if (!string.IsNullOrEmpty(recipientTown.Value))
                    {
                        custAddress.Append(recipientTown.Value + ",");
                    }
                    if (!string.IsNullOrEmpty(recipientPostCode.Value))
                    {
                        custAddress.Append(recipientPostCode.Value);
                    }
                    
                }
                else 
                {
                    objRecInfo = objRecipientDetails.GetDeliveryDetails(OrderID);
                    objRecInfo.OrderId = OrderID;
                    if (!string.IsNullOrEmpty(objRecInfo.Organisation))
                    {
                        custAddress.Append(objRecInfo.Organisation + ",");
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.HouseNo))
                    {
                        custAddress.Append(objRecInfo.HouseNo + ",");
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Street))
                    {
                        custAddress.Append(objRecInfo.Street + ",");
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.District))
                    {
                        custAddress.Append(objRecInfo.District + ",");
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Town))
                    {
                        custAddress.Append(objRecInfo.Town + ",");
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.PostCode))
                    {
                        custAddress.Append(objRecInfo.PostCode);
                    }
                
                }

               
                FillSuggestedAddress(custAddress.ToString());

                #region Quantity Verification
                int qty = 0;
                OrderDetailsBAL objOrdBal = new OrderDetailsBAL();
                qty = objOrdBal.GetTotalQty(OrderID);
                if (qty > 1)
                {
                    if (Request.Cookies["QunatityVerified"] == null)
                    {
                        Common objCommon = new Common();
                        HttpCookie cooki = new HttpCookie("QunatityVerified", "yes");
                        //cooki.Domain = objCommon.GetSiteName();
                        Response.Cookies.Add(cooki);
                        ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "QuantityVerification", "popupQuantityVerification();", true);
                    }
                    else if (Convert.ToString(Request.Cookies["QunatityVerified"].Value) != "yes")
                    {
                        Common objCommon = new Common();
                        HttpCookie cooki = new HttpCookie("QunatityVerified", "yes");
                        cooki.Domain = objCommon.GetSiteName();
                        Response.Cookies.Add(cooki);
                        ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "QuantityVerification", "popupQuantityVerification();", true);
                    }
                    else
                    {
                        if (objRecInfo.CountryID == 215)
                        {
                            if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "1")
                            {
                                ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "ajax123", "popupSuggestedAddress();", true);
                            }
                            else if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "0")
                            {
                                ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "ajax123", "addressnotfound();", true);

                            }
                            else
                            {
                                objRecInfo.AddressVerified = 1;
                                hdnAddressVerify.Value = "1";
                               
                                objRecipientDetails.EditDeliveryAddress(objRecInfo);
                                if (CheckDBPostCode(OrderID, SaveRecipient) == true)
                                {
                                    RedirectToNextPage(OrderID);
                                }
                            }
                        }
                        else
                        {
                            objRecInfo.AddressVerified = 1;
                            hdnAddressVerify.Value = "1";
                            objRecipientDetails.EditDeliveryAddress(objRecInfo);
                            RedirectToNextPage(OrderID);
                        }
                    }
                }
                else
                {
                    if (objRecInfo.CountryID == 215)
                    {
                        if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "1")
                        {

                            ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "ajax123", "popupSuggestedAddress();", true);
                        }
                        else if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "ajax123", "addressnotfound();", true);

                        }
                        else
                        {
                            objRecInfo.AddressVerified = 1;
                            hdnAddressVerify.Value = "1";
                          
                            objRecipientDetails.EditDeliveryAddress(objRecInfo);
                            if (CheckDBPostCode(OrderID, SaveRecipient) == true)
                            {
                                RedirectToNextPage(OrderID);
                            }
                        }
                    }
                    else
                    {
                        objRecInfo.AddressVerified = 1;
                        hdnAddressVerify.Value = "1";
                        objRecipientDetails.EditDeliveryAddress(objRecInfo);
                        RedirectToNextPage(OrderID);
                    }
                }
                #endregion
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Fill suggested address into listbox.
        /// </summary>
        /// <param name="custAddress"></param>
        private void FillSuggestedAddress(string custAddress)
        {
            try
            {
                DataSet objDset = null;
                if (DrpSuggestedAddress.Items.Count > 0)
                {
                    DrpSuggestedAddress.Items.Clear();
                }
                objDset = Common.CleanseAddress(custAddress);
                if (objDset != null && objDset.Tables.Count > 4 && objDset.Tables[4] != null)
                {
                    if (objDset.Tables[4].Rows.Count > 0)
                    {
                        string strAddrOfCust = custAddress.Replace(",", " ").Replace("  ", " ").Trim();
                        string strAddrOfApi = Convert.ToString(objDset.Tables[4].Rows[0]["description"]);
                        if (strAddrOfCust.ToLower() == strAddrOfApi.ToLower())
                        {
                            hdnAddressVerify.Value = "1";
                        }
                        else
                        {
                            hdnAddressVerify.Value = "0";
                            DrpSuggestedAddress.DataSource = objDset.Tables[4];
                            DrpSuggestedAddress.DataTextField = "description";
                            DrpSuggestedAddress.DataValueField = "id";
                            DrpSuggestedAddress.DataBind();

                            
                    //         Occasions.DataSource =objDset.Tables[4];
                    //         Occasions.DataTextField = "description";
                    //Occasions.DataValueField = "id";
                    //Occasions.DataBind();

                            DrpSuggestedAddress.Items.Insert(0, new ListItem("--Please select one of the suggested addresses--", "0"));
                            DrpSuggestedAddress.SelectedIndex = 0;
                            hdnHasApiAddress.Value = "1";
                          
                        }
                    }
                    else
                    {
                        hdnHasApiAddress.Value = "0";
                        hdnAddressVerify.Value = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// "Use This Address" Button Click of Suggestion Address Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UseThisAddress_Click(object sender, EventArgs e)
        {
            try
            {
                Encryption objEncryption = new Encryption();
                OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                FillAddressFields(DrpSuggestedAddress.SelectedValue);
                RecipientInfo objRecDtl = new RecipientInfo();
                objRecDtl.OrderId = OrderID;
                if (ViewState["addr_verified"] != null)
                {
                    objRecDtl.AddressVerified = 1;
                    hdnAddressVerify.Value = "1";
                }
                else
                {
                    objRecDtl.AddressVerified = 0;
                    hdnAddressVerify.Value = "0";
                }

                if (!string.IsNullOrEmpty(txtRecipientName.Value))
                {

                    string name = txtRecipientName.Value;
                    string firstName = string.Empty;
                    string lastName = string.Empty;
                    if (name.Contains(' '))
                    {
                        firstName = name.Substring(0, name.IndexOf(' '));
                        lastName = name.Substring(name.IndexOf(' ') + 1);
                    }
                    else
                    {
                        firstName = name;
                    }
                    objRecDtl.FirstName = firstName;
                    objRecDtl.LastName = lastName;
                }

                objRecDtl.Organisation = recpientOrganization.Value;
                objRecDtl.HouseNo = recipientAddress1.Value;
                objRecDtl.Street = recipientAddress2.Value;
                objRecDtl.District = recipientAddress3.Value;
                objRecDtl.Town = recipientTown.Value;
                objRecDtl.PostCode = recipientPostCode.Value;
                objRecDtl.CountryID = Convert.ToInt32(RecipientCountry.SelectedItem.Value);
                objRecipientDetails = new RecipientDetailsBAL();
                if (CheckDBPostCode(OrderID, UseThisAddress) == true)
                {
                    objRecDtl.AddressVerified = 1;
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);
                   
                    
                    //divRecfinal.Style.Add("display", "block");
                    //divRecfinalEditor.Style.Add("display", "block");
                    //divContinuePayment.Style.Add("display", "block");

                    ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                    RedirectToNextPage(OrderID);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private void RedirectToNextPage(string orderId)
        {
            try
            {
                double ordTotal = new OrderDetailsBAL().GetOrderTotalByOrderID(orderId);
                string nextPageUrl = ordTotal == 0 ? "m_confirmation.aspx?s=" + Request.QueryString["s"] : "m_paymentdetails.aspx?s=" + Request.QueryString["s"];
                Response.Redirect(nextPageUrl, false);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }


        /// <summary>
        /// "Use The Address I Entered" Button Click of Suggestion Address Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UseTheAddressIEntered_Click(object sender, EventArgs e)
        {
            try
            {
                Encryption objEncryption = new Encryption();
                OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                if (CheckDBPostCode(OrderID, UseTheAddressIEntered) == true)
                {
                    RecipientInfo objRecDtl = new RecipientInfo();
                    objRecDtl.OrderId = OrderID;
                    if (!string.IsNullOrEmpty(txtRecipientName.Value))
                    {
                        string name = txtRecipientName.Value;
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = name;
                        }
                        objRecDtl.FirstName = firstName;
                        objRecDtl.LastName = lastName;
                    }

                    objRecDtl.Organisation = recpientOrganization.Value;
                    objRecDtl.HouseNo = recipientAddress1.Value;
                    objRecDtl.Street = recipientAddress2.Value;
                    objRecDtl.District = recipientAddress3.Value;
                    objRecDtl.Town = recipientTown.Value;
                    objRecDtl.PostCode = recipientPostCode.Value;
                    objRecDtl.CountryID = Convert.ToInt32(RecipientCountry.SelectedItem.Value);
                    objRecDtl.AddressVerified = 1;
                    hdnAddressVerify.Value = "1";
                    objRecipientDetails = new RecipientDetailsBAL();
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);
                    ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                    RedirectToNextPage(OrderID);
                }
                //else
                //{
                //    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                //    CustomerInfo objCustomerInfo = new CustomerInfo();
                //    Encryption objEncryptions = new Encryption();
                //    OrderID = objEncryptions.GetAesDecryptionString(Request.QueryString["s"]);
                //    objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);
                //    RecipientInfo objRecDtl = new RecipientInfo();
                //    objRecDtl.OrderId = OrderID;
                //    objRecDtl.FirstName = objCustomerInfo.FirstName;
                //    objRecDtl.LastName = objCustomerInfo.LastName;
                //    objRecDtl.Organisation = objCustomerInfo.Organisation;
                //    objRecDtl.HouseNo = objCustomerInfo.HouseNo;
                //    objRecDtl.Street = objCustomerInfo.Street;
                //    objRecDtl.District = objCustomerInfo.District;
                //    objRecDtl.Town = objCustomerInfo.Town;
                //    objRecDtl.PostCode = objCustomerInfo.PostCode;
                //    objRecDtl.CountryID = objCustomerInfo.CountryID;
                //    objRecDtl.RecipientMobile = objCustomerInfo.UKMobile;
                //    objRecipientDetails = new RecipientDetailsBAL();
                //    objRecDtl.AddressVerified = 1;
                //    objRecipientDetails.EditDeliveryAddress(objRecDtl);
                //    ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                //}
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// Yes Button of Not Found Address Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            DataSet dsPostCodeDesc = new DataSet();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                Encryption objEncryption = new Encryption();
                OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                if (drpDelIns.SelectedItem.Value != "Please select an option")
                {
                    string instruct = string.Empty;
                    if (drpDelIns.SelectedItem.Value == "Leave with neighbour")
                    {
                        instruct = drpDelIns.SelectedItem.Value + " " + txthouseNumber.Value.Trim();
                    }
                    else
                    {
                        instruct = drpDelIns.SelectedItem.Value;
                    }

                    objRecipientDetails.EditDeliveryInstructions(OrderID, instruct);
                }

                objRecipientDetails.EditMessageCard(OrderID, txtGiftMsg.Value);
                
                System.Text.StringBuilder custAddress = new System.Text.StringBuilder();
                if (!string.IsNullOrEmpty(recipientPostCode.Value))
                {

                    if (!string.IsNullOrEmpty(txtRecipientName.Value))
                    {

                        string name = txtRecipientName.Value;
                        string firstName = string.Empty;
                        string lastName = string.Empty;
                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                        {
                            firstName = name;
                        }
                        objRecInfo.FirstName = firstName;
                        objRecInfo.LastName = lastName;
                    }

                    objRecInfo.OrderId = OrderID;
                    objRecInfo.Organisation = recpientOrganization.Value;
                    objRecInfo.HouseNo = recipientAddress1.Value;
                    objRecInfo.Street = recipientAddress2.Value;
                    objRecInfo.District = recipientAddress3.Value;
                    objRecInfo.Town = recipientTown.Value;
                    objRecInfo.PostCode = recipientPostCode.Value;
                    objRecInfo.AddressVerified = 0;
                    objRecInfo.CountryID = Convert.ToInt32(RecipientCountry.SelectedItem.Value);
                    objRecipientDetails.EditDeliveryAddress(objRecInfo);
                }
                
                ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                RedirectToNextPage(OrderID);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Bind address field values based on selected address id
        /// </summary>
        /// <param name="selectedValue"></param>
        public void FillAddressFields(string selectedValue)
        {
            string errorMsg = string.Empty;
            bool flag;
            try
            {
                DataSet doc = Common.GetAddressFieldsBasedOnAddressID(selectedValue);
                if (doc != null && doc.Tables.Count > 4 && doc.Tables[4] != null)
                {
                    flag = CheckValidPostCodeDataset(doc, ref errorMsg);
                    if (flag == true)
                    {
                        DataTable dt = doc.Tables[4];
                        foreach (DataRow dr in dt.Rows)
                        {
                            string orgName = Convert.ToString(dr["organisation_name"]);
                            string house = Convert.ToString(dr["line1"]);
                            string street = Convert.ToString(dr["line2"]);
                            string district = Convert.ToString(dr["line3"]);
                            string city = Convert.ToString(dr["post_town"]);
                            string county = Convert.ToString(dr["county"]);
                            string postcode = Convert.ToString(dr["postcode"]);
                            string country = "215";
                            recpientOrganization.Value = orgName;
                            recipientAddress1.Value = house;
                            recipientAddress2.Value = street;
                            recipientAddress3.Value = district;
                            recipientTown.Value = city;
                            recipientPostCode.Value = postcode;
                            //GetAllCountries();
                            RecipientCountry.SelectedIndex = RecipientCountry.Items.IndexOf(RecipientCountry.Items.FindByValue(country));
                            ViewState["addr_verified"] = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Check postcode dataset is valid or not.
        /// </summary>
        /// <param name="dsResult"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool CheckValidPostCodeDataset(DataSet dsResult, ref string errorMsg)
        {
            bool flag = false;
            try
            {
                flag = Common.CheckColumnsExistsInDataSet(dsResult);
                if (flag == false)
                {
                    foreach (DataTable dt in dsResult.Tables)
                    {
                        if (dt.TableName == "row")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                errorMsg += Convert.ToString(dr["message"]) + Environment.NewLine;
                            }
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

        protected void EditAddress_Click(object sender, EventArgs e)
        {
            RecipientName.Value = CustomerFullName.InnerHtml;
            recpientOrganization.Value = Convert.ToString(ViewState["Organization"]);
            recipientAddress1.Value = Convert.ToString(ViewState["houseno"]);
            recipientAddress2.Value = Convert.ToString(ViewState["street"]);
            recipientAddress3.Value = Convert.ToString(ViewState["district"]);
           
            recipientTown.Value = CustomerPCACity.InnerText;
            recipientPostCode.Value = CustomerPCAPostCode.InnerText;
            

            content3.Style.Add("display", "block");
            content1.Style.Add("display", "none");
            content2.Style.Add("display", "block");
            divEditBox.Style.Add("display", "none");
        }
          
        

      
    }
    
}