using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SFMobile.BAL.SiteData;
using SFMobile.BAL.Checkout;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Configuration;
using SFMobile.BAL.Orders;
using System.Xml;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using SFMobile.BAL.Products;

#region Imported Namespaces for New Order Schema
using SerenataOrderSchemaBAL;
#endregion

namespace Serenataflowers.Checkout
{
    public partial class CustomerDetails : AjaxLoader
    {
        #region variables
        CountriesLogic objCountries;
        CheckoutLogic objCheckout;
        CustomerInfo objCustomerInfo;
        OrdersLogic objOrder;
        CommonFunctions objSearchAddress;
        string orderId;      
        string decryOrderId;
        string DecryptrdOrderId;

        ProductsLogic objProductsLogic;
        OrdersLogic objOrderLogic;
        CheckoutLogic objCheckoutLogic;
        CommonFunctions objCommondetails;    
        int productId;

        #region Declared/Initilised BAL object for New Order Schema
        SerenataCheckoutLogic objNewOrder = new SerenataCheckoutLogic();
        CustomerInfo objCustomerInfoNew;
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
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                DecryptrdOrderId = Convert.ToString(Request.QueryString["s"]);
                objSearchAddress = new CommonFunctions();
                decryOrderId = objSearchAddress.Decrypt(DecryptrdOrderId, "testpage");
                hdnOrderId.Value = decryOrderId; 
            }
            if ((string.IsNullOrEmpty(Request.QueryString["s"]) == false))
            {
                objSearchAddress = new CommonFunctions();
                orderId = objSearchAddress.Decrypt(Request.QueryString["s"], "testpage");              
                CreateMetaTags(orderId);
                if (!IsPostBack)
                {
                    var url = String.Format("http://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("../default.aspx?ProdId=" + Convert.ToInt32(Request.QueryString["ProdId"]) + "&s=" + DecryptrdOrderId));
                    ancStp.HRef = url;
                    GetOrderID();
                    GetAllContries();
                    //Page.Title = CommonFunctions.PageTitle() + " - Checkout - Billing Details.";
                    ltTitle.Text = "\n<title>" + CommonFunctions.PageTitle() + " - Checkout - Billing Details." + "</title>\n";
                    //Page.Title = " Flowers UK London | Flower Delivery UK | Florist | Flowers Online| Send Flowers to England Florists ";
                    hdnEncryptionUrl.Value = ConfigurationManager.AppSettings["encryptionUrlPath"];
                    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ShowModelPopUp", "revealModal('modalPage');", true);
                    hdnBoolConfigXml.Value = SerenataflowersSessions.ConfigXML != null ? "true" : "false";
                    RetainedData(orderId);
                   
                }
                // added for quantity verification
                objCommondetails = new CommonFunctions();
                orderId = objCommondetails.Decrypt(Request.QueryString["s"], "testpage");
                BindOrdersList(orderId);
            }
            else
            {
                Response.Redirect("~/Default.aspx", true);
            }
            //CommonFunctions.AddFloodLightTags(this.Page);
        }
        /// <summary>
        ///This Event will fire when user want to searh address using PostCode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void find_residential_Click(object sender, EventArgs e)
        //{
        //    string errorMsg = string.Empty;
        //    bool flag;
        //    DataSet dsAddress = null;
        //    objSearchAddress = new CommonFunctions();
        //    div1.Style.Add("display", "block");
        //    div2.Style.Add("display", "none");
        //    div3.Style.Add("display", "none");
        //    try
        //    {
        //        //dsAddress = objSearchAddress.GetAddressByPostCode(find_postcode.Text);
        //        dsAddress = objSearchAddress.GetAddressByPostCode("SE1 0hs");
        //        if (dsAddress != null && dsAddress.Tables.Count > 4 && dsAddress.Tables[4] != null)
        //        {

        //            flag = CheckValidPostCodeDataset(dsAddress, ref errorMsg);
        //            ErrorMsgFindPostcode.Style.Add("display", "block");
        //            if (flag == true)
        //            {
        //                ddlAddress.DataSource = dsAddress.Tables[4];
        //                ddlAddress.DataTextField = "description";
        //                ddlAddress.DataValueField = "id";
        //                ddlAddress.DataBind();

        //                ddlAddress.Items.Insert(0, new ListItem("---Select your address---", "0"));
        //                ddlAddress.SelectedIndex = 0;

        //                DivAddressResult.Style.Add("display", "block");
        //                DivAddressResultMsg.Style.Add("display", "block");
        //                AddressResultMsg.InnerText = "Please select an address from the list above";
        //            }
        //            else
        //            {
        //                DivAddressResult.Style.Add("display", "none");
        //                DivAddressResultMsg.Style.Add("display", "none");
        //                ErrorMsgFindPostcode.InnerText = "";
        //            }


        //        }
        //        else
        //        {
        //            DivErrorMsgFindPostcode.Style.Add("display", "block");
        //            ErrorMsgFindPostcode.InnerText = "Sorry, we couldn't find any address to match the postcode entered. Please try again or enter the address manually.";
        //            DivAddressResult.Style.Add("display", "none");
        //            DivAddressResultMsg.Style.Add("display", "none");
        //            updatePanel.Update();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);

        //    }
        //}
        /// <summary>
        /// This Event will fire when user want to searh address using PostCode and Business organization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void find_business_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            bool flag;
            DataSet dsAddress = null;
            objSearchAddress = new CommonFunctions();
            div1.Style.Add("display", "none");
            div2.Style.Add("display", "block");
            div3.Style.Add("display", "none");

            try
            {
                dsAddress = objSearchAddress.GetAddressByOrganisationAndCity(find_business_name.Text, find_town.Text);
                if (dsAddress != null && dsAddress.Tables.Count > 4 && dsAddress.Tables[4] != null)
                {
                    flag = CheckValidPostCodeDataset(dsAddress, ref errorMsg);
                    if (flag == true)
                    {
                        ddlAddress.DataSource = dsAddress.Tables[4];
                        ddlAddress.DataTextField = "description";
                        ddlAddress.DataValueField = "id";
                        ddlAddress.DataBind();

                        ddlAddress.Items.Insert(0, new ListItem("---Select---", "0"));
                        ddlAddress.SelectedIndex = 0;

                        DivAddressResult.Style.Add("display", "block");
                        DivAddressResultMsg.Style.Add("display", "block");
                        AddressResultMsg.InnerText = "Please select an address from the list above";
                    }
                    else
                    {
                        DivAddressResult.Style.Add("display", "none");
                        DivAddressResultMsg.Style.Add("display", "none");


                    }


                }
                else
                {

                    DivErrorMsgFindPostcode.Style.Add("display", "block");
                    ErrorMsgFindPostcode.InnerText = "Sorry, we couldn't find any address to match the Business/Organisation or City entered. Please try again or enter the address manually.";
                    DivAddressResult.Style.Add("display", "none");
                    DivAddressResultMsg.Style.Add("display", "none");
                    updatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);

            }
        }
        /// <summary>
        /// This checkbox CheckedChanged Event will fire when user want to select same address for billing details
        /// and display all the recipient information in respective text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void same_as_delivery_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (same_as_delivery.Checked == true)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                    {
                        objSearchAddress = new CommonFunctions();
                        orderId = objSearchAddress.Decrypt(Request.QueryString["s"], "testpage");
                        SameAsDeliveryAddress(orderId);
                        DivAddressResult.Style.Add("display", "none");
                        DivAddressResultMsg.Style.Add("display", "none");
                        DivErrorMsgFindPostcode.Style.Add("display", "none");
                        div1.Style.Add("display", "none");
                        div2.Style.Add("display", "none");
                        div3.Style.Add("display", "block");
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx", false);
                    }

                }
                else
                {

                    organisation_field.Text = "";
                    txtAddressline1.Text = "";
                    street_field.Text = "";
                    district_field.Text = "";
                    txtCitytown.Text = "";
                    txtPostcodefield.Text = "";
                    DivAddressResult.Style.Add("display", "none");
                    DivAddressResultMsg.Style.Add("display", "none");
                    DivErrorMsgFindPostcode.Style.Add("display", "none");
                    div1.Style.Add("display", "block");
                    div2.Style.Add("display", "none");
                    div3.Style.Add("display", "none");
                    //if (SerenataflowersSessions.CountryName != "United Kingdom")
                    //{
                    //    div3.Style.Add("display", "block");
                    //    div1.Style.Add("display", "none");
                    //}
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// This dropdown selectedIndexChanged event will fire when user select address from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddress.SelectedValue != "0")
                {
                    if (!string.IsNullOrEmpty(hdnSelectedAddressValue.Value))
                    {
                        ddlAddress.SelectedValue = hdnSelectedAddressValue.Value;
                    }
                    div1.Style.Add("display", "block");
                    div2.Style.Add("display", "none");
                    div3.Style.Add("display", "block");
                    DivAddressResult.Style.Add("display", "block");
                    DivAddressResultMsg.Style.Add("display", "block");
                    updatePanel.Update();
                    FillAddressFields(ddlAddress.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// This Event will fire when user click on 'Continue my Payment Details button" to save billing
        /// information in to DB and Post the Form to payment.asp page uisng Javascript.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            //config.Value = hdnEncryptedConfig.Value;
            if (!string.IsNullOrEmpty(Request.QueryString["s"]) )
            {
                objSearchAddress = new CommonFunctions();
                orderId = objSearchAddress.Decrypt(Request.QueryString["s"], "testpage");
            }

            if (!string.IsNullOrEmpty(orderId))
            {
                #region Initilised object for New Order Schema
                objCustomerInfoNew = new CustomerInfo();
                #endregion
                objCustomerInfo = new CustomerInfo();
                objCheckout = new CheckoutLogic();
                int response = 0;
                try
                {
                    objCustomerInfo.Name = txtFirstName.Text;
                    objCustomerInfo.LastName = txtLastName.Text;
                    objCustomerInfo.Email = txtEmail.Text;
                    objCustomerInfo.Country = invCountry.SelectedItem.Text;
                    objCustomerInfo.CountryCode = "215";
                    //objCustomerInfo.OrderID = Session["OrderId"] != null ? Convert.ToString(Session["OrderId"]) : Request.Cookies["OrderId"].Value;
                    objCustomerInfo.OrderID = orderId;// SerenataflowersSessions.OrderId != null ? SerenataflowersSessions.OrderId : Request.Cookies["OrderId"].Value;
                    objCustomerInfo.Organisation = organisation_field.Text;
                    objCustomerInfo.PostCode = txtPostcodefield.Text;
                    objCustomerInfo.ReceipentTelPhNo = txtContacttel.Text;
                    objCustomerInfo.UKMobile = mobile_field.Text;
                    objCustomerInfo.VoucherCode = voucher_code.Text;
                    objCustomerInfo.Town = txtCitytown.Text;
                    objCustomerInfo.Street = street_field.Text;
                    objCustomerInfo.District = district_field.Text;
                    objCustomerInfo.SMSNotification = sms_field.Checked ? 1 : 0;
                    objCustomerInfo.EmailNewsletter = promo_email_field.Checked ? 1 : 0;
                    objCustomerInfo.HouseNo = txtAddressline1.Text;
                    objCustomerInfo.SameAsDelivery = same_as_delivery.Checked ? 1 : 0;
                    objCheckout.InsertUpdateBillingDetails(objCustomerInfo);

                    #region Updating customer details for New Order Schema
                    // Updating customer details.
                        objCustomerInfoNew.CustomerId = 0;
                        objCustomerInfoNew.OrderID = orderId;
                        objCustomerInfoNew.FirstName = txtFirstName.Text;
                        objCustomerInfoNew.LastName = txtLastName.Text;
                        objCustomerInfoNew.Email = txtEmail.Text;
                        objCustomerInfoNew.CountryID = 215;
                        objCustomerInfoNew.Organisation = organisation_field.Text;
                        objCustomerInfoNew.PostCode = txtPostcodefield.Text;
                        objCustomerInfoNew.UKMobile = mobile_field.Text;
                        objCustomerInfoNew.VoucherCode = voucher_code.Text;
                        objCustomerInfoNew.Town = txtCitytown.Text;
                        objCustomerInfoNew.Street = street_field.Text;
                        objCustomerInfoNew.District = district_field.Text;
                        objCustomerInfoNew.SMSNotification = sms_field.Checked ? 1 : 0;
                        objCustomerInfoNew.EmailNewsletter = promo_email_field.Checked ? 1 : 0;
                        objCustomerInfoNew.HouseNo = txtAddressline1.Text;
                        objNewOrder.EditCustomerDetails(objCustomerInfoNew);
                    // Validating voucher code
                        if (voucher_code.Text.Length != 0)
                        {
                            OrderDTO objOrderInfoNew = new OrderDTO();
                            objOrderInfoNew.OrderId = orderId;
                            objOrderInfoNew.VoucherCode = voucher_code.Text;
                            objOrderInfoNew.SiteId = objSearchAddress.GetSiteId();
                            objNewOrder.ValidateVoucherCode(objOrderInfoNew);
                        }
                    #endregion

                        if (voucher_code.Text.Length != 0)
                        {
                            OrderDTO objOrderInfoNew = new OrderDTO();
                            objOrderInfoNew.OrderId = orderId;
                            objOrderInfoNew.VoucherCode = voucher_code.Text;
                            objOrderInfoNew.SiteId = objSearchAddress.GetSiteId();
                            response = objNewOrder.ValidateVoucherCodeForExistingOrder(objOrderInfoNew);
                            switch (ValidateVoucherCode(response))
                            {

                                case Voucher.Invalidvouchercode:
                                    spnVoucher.InnerText = "Voucher code is not valid.";
                                    div3.Style.Add("display", "block");
                                    updatePanel.Update();
                                    break;
                                case Voucher.Vouchercodenotexists:
                                    spnVoucher.InnerText = "Voucher code does not exists.";
                                    div3.Style.Add("display", "block");
                                    updatePanel.Update();
                                    break;
                                case Voucher.Success:
                                    spnVoucher.InnerText = " ";
                                    div3.Style.Add("display", "block");
                                    updatePanel.Update();

                                    int qty = 0;
                                    objOrderLogic = new OrdersLogic();
                                    qty = objOrderLogic.GetQuantityByOrderId(orderId);
                                    if (qty > 1)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(btnContinue, btnContinue.GetType(), "postForm", "ShowModalPopup('dvPopup');", true);
                                    }
                                    else
                                    {
                                        string filename = hdnfilename.Value.Replace("config_", "").Replace(".xml", "");
                                        Response.Redirect("~/Checkout/RecipientDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"] + "&t=" + filename, false);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            int qty = 0;
                            objOrderLogic = new OrdersLogic();
                            qty = objOrderLogic.GetQuantityByOrderId(orderId);
                            div3.Style.Add("display", "block");
                            updatePanel.Update();
                            if (qty > 1)
                            {
                                ScriptManager.RegisterClientScriptBlock(btnContinue, btnContinue.GetType(), "postForm", "ShowModalPopup('dvPopup');", true);
                            }
                            else
                            {
                                string filename = hdnfilename.Value.Replace("config_", "").Replace(".xml", "");
                                Response.Redirect("~/Checkout/RecipientDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"] + "&t=" + filename, false);
                            }
                        }
                }
                catch (Exception ex)
                {
                    SFMobileLog.Error(ex);
                    Response.Redirect("~/Default.aspx", true);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", true);
            }

        }
        #endregion

        #region Page Method
        private void GetOrderID()
        {
            spanOrderId.InnerText = "(" + hdnOrderId.Value + ")";
            if (Request.QueryString["isupsell"] != null)
            {
                ancOrderId.HRef += "?ProdId=" + Request.QueryString["ProdId"] + "&isupsell=" + Request.QueryString.Get("isupsell") + "&s=" + Convert.ToString(Request.QueryString["s"]);
            }
            else
            {
                ancOrderId.HRef += "?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Convert.ToString(Request.QueryString["s"]);
            }
        }  
        /// <summary>
        /// Method to Get all Countries in Alphabetical order
        /// </summary>
        private void GetAllContries()
        {
            objCountries = new CountriesLogic();
            DataSet dsCountry = new DataSet();
            try
            {
                dsCountry = objCountries.GetCountryById(1);
                if (dsCountry != null && dsCountry.Tables.Count > 0)
                {
                    invCountry.DataSource = dsCountry;
                    invCountry.DataTextField = "CountryName";
                    invCountry.DataValueField = "Id";
                    invCountry.DataBind();
                    invCountry.SelectedIndex = -1;
                    string country = "United Kingdom";
                    if (!string.IsNullOrEmpty(country))
                    {

                        invCountry.Items.Remove(invCountry.Items.FindByText(country));
                        invCountry.SelectedItem.Text = "United Kingdom";// SerenataflowersSessions.CountryName;
                        invCountry.SelectedItem.Value = "215";

                    }
                    else
                        invCountry.SelectedIndex = 0;
                }
                invCountry.Enabled = false;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// This methods contains the logic to get the the Address details basesd on order Id
        /// </summary>
        /// <param name="orderId"></param>
        private void SameAsDeliveryAddress(string orderId)
        {
            objCheckout = new CheckoutLogic();
            CustomerInfo objBillingInfo = new CustomerInfo();
            try
            {
                objBillingInfo = objCheckout.GetBillingDetails(orderId);
                if (objBillingInfo != null)
                {
                    organisation_field.Text = objBillingInfo.Organisation;
                    txtAddressline1.Text = objBillingInfo.HouseNo;
                    street_field.Text = objBillingInfo.Street;
                    district_field.Text = objBillingInfo.District;
                    txtCitytown.Text = objBillingInfo.Town;
                    txtPostcodefield.Text = objBillingInfo.PostCode;
                    if (objBillingInfo.Country != "")
                    {
                        invCountry.SelectedIndex = -1;
                        invCountry.Items.FindByText(objBillingInfo.Country).Selected = true;

                    }
                }
                sms_field.Checked = true;
                promo_email_field.Checked = true;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// This method will Bind address field values based on Selected Address id
        /// </summary>
        /// <param name="selectedValue"></param>
        private void FillAddressFields(string selectedValue)
        {
            objSearchAddress = new CommonFunctions();
            string errorMsg = string.Empty;
            bool flag;
            try
            {

                DataSet doc = objSearchAddress.GetAddressFieldsBasedOnAddressID(selectedValue);
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
                            string country = "United Kingdom"; //SerenataflowersSessions.CountryName != null ? SerenataflowersSessions.CountryName : "";

                            organisation_field.Text = orgName;
                            txtAddressline1.Text = house;
                            street_field.Text = street;
                            district_field.Text = district;
                            txtCitytown.Text = city;
                            txtPostcodefield.Text = postcode;
                            invCountry.SelectedIndex = -1;
                            invCountry.Items.FindByText(country).Selected = true;
                            sms_field.Checked = true;
                            promo_email_field.Checked = true;

                        }
                        updatePanel.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        /// <summary>
        /// This method will Check Postcode dataset is Valid or Not
        /// </summary>
        /// <param name="dsResult"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool CheckValidPostCodeDataset(DataSet dsResult, ref string errorMsg)
        {
            bool flag = false;
            objSearchAddress = new CommonFunctions();
            try
            {
                flag = objSearchAddress.CheckColumnsExistsInDataSet(dsResult);
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
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ErrorMsgFindPostcode.InnerText = errorMsg;
                    DivErrorMsgFindPostcode.Style.Add("display", "block");
                }
                else
                {

                    spnErrorMsg.InnerText = "";
                    DivErrorMsgFindPostcode.Style.Add("display", "none");
                    ErrorMsgFindPostcode.InnerText = "";
                }
                return flag;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return flag;
        }

        /// <summary>
        ///Enums for voucher code are used for validate voucer code while update billing details
        /// </summary>       
        enum Voucher { Vouchercodenotexists, Invalidvouchercode, Success }
        private Voucher ValidateVoucherCode(int response)
        {
            if (response == -3)
            {
                return Voucher.Vouchercodenotexists;
            }
            else if (response == -2)
            {
                return Voucher.Invalidvouchercode;
            }
            return Voucher.Success;
        }

        private void GenerateaAndStoreOrderXml(string orderId)
        {
            try
            {

                //for generating the Order.xml file
                objOrder = new OrdersLogic();
                DataSet dsTransactionDetails = objOrder.GetTransactionDetailsById(orderId);
                string strXmlOrders = objOrder.GenerateOrdersXml(dsTransactionDetails);

                //Order.xml encryption
                //string orderXmlEncrptValue = aesLibrary.EncryptData(strXmlOrders, "testpage", "", aesLibrary.BlockSize.Block256, aesLibrary.KeySize.Key256, aesLibrary.EncryptionMode.ModeECB, true);
                //Session["orderXml"] = strXmlOrders;
                XmlDocument xmlOrder = new XmlDocument();
                string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                string orderXmlFileName = GenerateFileName("order");

                if (Directory.Exists(filePath))
                {
                    xmlOrder.LoadXml(strXmlOrders);
                    xmlOrder.Save(filePath + "\\" + orderXmlFileName);
                }
                else
                {
                    Directory.CreateDirectory(filePath);
                    xmlOrder.LoadXml(strXmlOrders);
                    xmlOrder.Save(filePath + "\\" + orderXmlFileName);
                }

                //Store the xmlFileName to Session
                hdnOrderXmlFileName.Value = orderXmlFileName;
                updOrder.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// This method will create a unique file name.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string GenerateFileName(string context)
        {
            return context + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString() + ".xml";
        }
        /// <summary>
        /// This method will retained the value  for recipient's details
        /// </summary>
        /// <param name="orderId"></param>
        private void RetainedData(string orderId)
        {
            CheckoutLogic objCheckOutLogic = new CheckoutLogic();
            CustomerInfo objBillingInfo = new CustomerInfo();
            try
            {
                objBillingInfo = objCheckOutLogic.RetainedBillingDetails(orderId);
                if (objBillingInfo != null)
                {
                    txtFirstName.Text = objBillingInfo.Name;
                    txtLastName.Text = objBillingInfo.LastName;
                    txtEmail.Text = objBillingInfo.Email;
                    organisation_field.Text = objBillingInfo.Organisation;
                    txtAddressline1.Text = objBillingInfo.HouseNo;
                    street_field.Text = objBillingInfo.Street;
                    district_field.Text = objBillingInfo.District;
                    txtCitytown.Text = objBillingInfo.Town;
                    txtPostcodefield.Text = objBillingInfo.PostCode;
                    txtContacttel.Text = objBillingInfo.ReceipentTelPhNo;
                    mobile_field.Text = objBillingInfo.UKMobile;
                    voucher_code.Text = objBillingInfo.VoucherCode;
                    if (objBillingInfo.SMSNotification == 1)
                    {
                        sms_field.Checked = true;
                    }
                    else
                    {
                        sms_field.Checked = false;
                    }
                    if (objBillingInfo.EmailNewsletter == 1)
                    {
                        promo_email_field.Checked = true;
                    }
                    else
                    {
                        promo_email_field.Checked = false;
                    }


                    if (objBillingInfo.Country != null && objBillingInfo.Country != "")
                    {
                        invCountry.SelectedIndex = -1;
                        invCountry.Items.FindByText(objBillingInfo.Country).Selected = true;

                    }
                    if (objBillingInfo.Name != null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "RetainedBillingInfo();", true);
                        //divPrefer.Style.Add("display", "block");

                    }

                }
                else
                {
                    sms_field.Checked = true;
                    promo_email_field.Checked = true;
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


        }
        [WebMethod]
        public static string getEncryptedOrderId(string httpResponse)
        {
            string configXmlFileName = string.Empty;
            try
            {
                string encryptedValue = httpResponse.Replace("\r\n", "");
                string enOrderId = string.Empty; ;
                //Read the Config.xml file from local disk
                XmlDocument xmlConfig = new XmlDocument(); ;
                string xmlfilePath = ConfigurationManager.AppSettings["ConfigXMLPath"];
                string domainName = GetDomainName();//added  
                configXmlFileName = GenerateFileName("config");
                string filename = configXmlFileName.Replace("config_", "").Replace(".xml", "");
                xmlConfig.Load(xmlfilePath);
                System.Collections.Specialized.NameValueCollection collections = GetQueryStringCollection(HttpContext.Current.Request.UrlReferrer.Query);
                if (collections != null && collections.Count > 0)
                {
                     enOrderId = HttpContext.Current.Server.UrlDecode(collections["s"]);
                }
                string xmlConfigFile = xmlConfig.InnerXml.Replace("@OrderID@", encryptedValue).Replace("@orderid@", enOrderId).Replace("@filename@", filename);
                xmlConfigFile = xmlConfigFile.Replace("@DomainName", domainName);
                string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                //configXmlFileName = GenerateFileName("config");

                if (Directory.Exists(filePath))
                {
                    xmlConfig.LoadXml(xmlConfigFile);
                    xmlConfig.Save(filePath + "\\" + configXmlFileName);
                }
                else
                {
                    Directory.CreateDirectory(filePath);
                    xmlConfig.LoadXml(xmlConfigFile);
                    xmlConfig.Save(filePath + "\\" + configXmlFileName);
                }
                //string s = System.Web.HttpContext.Current.Request.Cookies["OrderId"].Value;
                System.Collections.Specialized.NameValueCollection collection = GetQueryStringCollection(HttpContext.Current.Request.UrlReferrer.Query);
                if (collection != null && collection.Count > 0)
                {
                    string DecryptrdOrderId = HttpContext.Current.Server.UrlDecode(collection["s"]);
                    CommonFunctions objSearchAddress = new CommonFunctions();
                    string decryOrderId = objSearchAddress.Decrypt(DecryptrdOrderId, "testpage");

                    CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                    objCheckOutLogic.UpdateEncryptedOrderId(decryOrderId, encryptedValue);
                    #region Updating encrypted order id for New Order Schema
                        SerenataCheckoutLogic objNewOrder = new SerenataCheckoutLogic();
                        OrderDTO objOrderInfoNew = new OrderDTO();
                        objOrderInfoNew.OrderId = decryOrderId;
                        objOrderInfoNew.EncryptedOrderID = encryptedValue;
                        objNewOrder.UpdateEncryptedOrderId(objOrderInfoNew);
                    #endregion
                }


                //Store the xmlFileName to Session
                //System.Web.HttpContext.Current.Session["configXmlFileName"] = configXmlFileName;

                //xmlConfig.LoadXml(xmlConfigFile);

                //System.Web.HttpContext.Current.Session["encryptedOrderId"] = encryptedValue;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return configXmlFileName;
        }
        public static System.Collections.Specialized.NameValueCollection GetQueryStringCollection(string url)
        {
            string keyValue = string.Empty;
            System.Collections.Specialized.NameValueCollection collection = new System.Collections.Specialized.NameValueCollection();
            string[] querystrings = url.Split('&');
            if (querystrings != null && querystrings.Count() > 0)
            {
                for (int i = 0; i < querystrings.Count(); i++)
                {
                    string[] pair = querystrings[i].Split('=');
                    collection.Add(pair[0].Trim('?'), pair[1]);
                }
            }
            return collection;
        } 
        public static string GetDomainName()
        {
            string domain = string.Empty;
            string url;
            try
            {
                url = System.Web.HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                domain = baseUri.Host;

            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            }
            return domain;
        }
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
                hmpageName.Content = "Checkout:Step1";

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

        #region Code for Quantity Verification Popup

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
                    if (txtQty.Text == "" || txtQty.Text == "0")
                    {
                        item.ActionType = "D";
                    }
                    else
                    {
                        item.Quantity = Convert.ToInt32(txtQty.Text);
                        item.TotalPrice = Convert.ToSingle(item.Quantity * item.Price);
                        item.ActionType = "U";
                    }
                   
                    UpdateCartItems(lstCartInfo);// added when session is removed
                    BindCartItems(lstCartInfo);
                    lstCartInfo = null;
                    HtmlControl span = (HtmlControl)rptOrders.Controls[rptOrders.Controls.Count - 1].Controls[0].FindControl("divwarn");
                    span.Style.Add("display", "none");
                }
                catch (Exception ex)
                {
                    SFMobileLog.Error(ex);
                }
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
            rptOrders.DataSource = lstCartInfo.Where(c => c.ActionType != "D");
            rptOrders.DataBind();
            lstCartInfo = null;
        }


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
        }

        private void UpdateCartItems(List<CartInfo> lstCartInfo)
        {
            try
            {
                objCheckoutLogic = new CheckoutLogic();
                foreach (CartInfo Ci in lstCartInfo)
                {
                    int result = objCheckoutLogic.InsertCartItemDetails(Ci);
                    #region Saving quantity,add extra and delete product from basket for new order Schema
                        // updating delivery details   
                        OrderDTO objOrderInfoNew = new OrderDTO();
                        objOrderInfoNew = new OrderDTO();
                        objOrderInfoNew.OrderId = Ci.OrderId;
                        objOrderInfoNew.ProductId = Ci.ProductId;
                        objOrderInfoNew.Quantity = Ci.Quantity;
                        // Below changes made on 29th Jan 2012 to get product price and upsale count.
                        OrderDTO tempPriceInfo = objNewOrder.GetProductPriceDetails(objOrderInfoNew);
                        objOrderInfoNew.Price = tempPriceInfo.Price;
                        objOrderInfoNew.ProdVATRate = tempPriceInfo.ProdVATRate;
                        objOrderInfoNew.PartnerID = tempPriceInfo.PartnerID;

                        objOrderInfoNew.UpsaleCount = objNewOrder.GetUpsaleCount(Ci.OrderId);

                        if (Ci.ActionType == "D")
                        {
                            objNewOrder.DeleteProductFromBasket(objOrderInfoNew);
                        }
                        else if (Ci.ActionType == "I")
                        {
                            objNewOrder.AddProductLine(objOrderInfoNew);
                        }
                        else
                        {
                            objNewOrder.UpdateProductQuantity(objOrderInfoNew);
                        }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void continue_recipient_Click(object sender, EventArgs e)
        {
            string filename = hdnfilename.Value.Replace("config_", "").Replace(".xml", "");
            //Response.Redirect("~/Checkout/m_step2.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"] + "&t=" + filename, false);
            Response.Redirect("~/Checkout/RecipientDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"] + "&t=" + filename, false);
        }
        #endregion
    }
}