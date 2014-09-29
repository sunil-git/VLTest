using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Bal.Common;
using SFMobile.Exceptions;
using Serenata_Checkout.ExactTargetAPI;
using System.Web.Services;

namespace Serenataflowers.Checkout
{
    public partial class m_customerdetails : System.Web.UI.Page
    {
        string IsPCAClicked = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                CustomerEmail.Focus();
                CustomerEmail.Attributes.Add("type", "email");
                ManualEditEmail.Attributes.Add("type", "email");

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                    {
                        string OrderID = Common.GetOrderIdFromQueryString();
                        SaveRecipient.Attributes.Add("data-theme", "d");
                        SaveRecipient.Attributes.Add("data-icon", "arrow-r");
                        SaveRecipient.Attributes.Add("data-iconpos", "right");

                        ContinueRecipient.Attributes.Add("data-theme", "d");
                        ContinueRecipient.Attributes.Add("data-icon", "arrow-r");
                        ContinueRecipient.Attributes.Add("data-iconpos", "right");

                        SavePCADetails.Attributes.Add("data-theme", "d");
                        SavePCADetails.Attributes.Add("data-icon", "arrow-r");
                        SavePCADetails.Attributes.Add("data-iconpos", "right");

                        Back.Attributes.Add("data-theme", "a");
                        Back.Attributes.Add("data-icon", "arrow-l");
                        Back.Attributes.Add("data-iconpos", "left");
                        Back.Attributes.Add("data-mini", "true");

                        ScreenBack.Attributes.Add("data-theme", "a");
                        ScreenBack.Attributes.Add("data-icon", "arrow-l");
                        ScreenBack.Attributes.Add("data-iconpos", "left");
                        ScreenBack.Attributes.Add("data-mini", "true");
                        DisplayScreen();
                        Common.GetAllCountries(CustomerCountry);
                        CheckGuest();
                        CustomerCountry_SelectedIndexChanged(null, null);
                      
                        #region Social Login
                        if (!string.IsNullOrEmpty(Common.GetQueryStringValue("sn")))
                        {
                            

                            string strSn = Common.GetQueryStringValue("sn");
                            string decStrSocialdata = new Encryption().GetAesDecryptionString(strSn);
                            string[] arrCustData = decStrSocialdata.Split(new char[] { '|' });
                            if (arrCustData.Length >= 7)
                            {


                                DisplayCustomerAddress.InnerHtml = arrCustData[0];
                                DisplayCustomerFullname.InnerHtml = arrCustData[1] + " " + arrCustData[6];

                               
                                ManualEditCustomerHouseNumber.Value = arrCustData[2];
                                ManualEditCustomerCity.Value = arrCustData[3];


                                if (!string.IsNullOrEmpty(arrCustData[4]))
                                {
                                    CustomerDetailsBAL objCustomerDetails=new CustomerDetailsBAL();
                                    CustomerCountry.SelectedIndex = CustomerCountry.Items.IndexOf(CustomerCountry.Items.FindByValue(arrCustData[4]));
                                    MobileNumber.Value = arrCustData[5];
                                    string CountryDialingCode = objCustomerDetails.GetCountryDialingCodeByCuontyID(Convert.ToInt32(CustomerCountry.SelectedValue));
                                    CountryCode.Text = CountryDialingCode;
                                }
                                
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "clickEnterAddressManually();", true);
                            }
                        }
                        else
                        {
                           
                            DisplayCustomerDetails(OrderID);
                        }
                        #endregion

                        Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "Step1");

                    }
                    else
                    {
                        Response.Redirect("../ErrorPage.aspx");
                    }


                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

            new Common().CheckCutOffTime(MasterBody, Common.GetOrderIdFromQueryString());

        }
        protected void SaveRecipient_Click(object sender, EventArgs e)
        {
            try
            {
                string strIsValid = ValidateEmail(CustomerEmail.Value);
                if (strIsValid == "false")
                {

                    ScriptManager.RegisterClientScriptBlock(SaveRecipient, SaveRecipient.GetType(), "Add4421ressDiv", "EmailVerifyPopup('" + CustomerEmail.Value + "');", true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(CustomerVoucherCode.Value.Trim()))
                    {
                        if (ValidateVoucherCode(CustomerVoucherCode.Value.Trim()) == true)
                        {
                            SaveCustomerDetails();
                        }
                    }
                    else
                    {
                        errorCustomerVoucherCode.InnerHtml = " ";
                        errorCustomerVoucherCode.Style.Add("display", "none");
                        SaveCustomerDetails();
                    }
                    bindAddress();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
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
        private bool ValidateVoucherCode(string voucher)
        {
            bool isValidate = false;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    Encryption objEncryption = new Encryption();
                    string strOrderId = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                    CommonBal objCommonBal = new CommonBal();
                    VoucherInfo objV = objCommonBal.ValidateVoucherCode(strOrderId, voucher, new Common().GetSiteId());
                    int intStatus = 0;
                    string strMessage = string.Empty;
                    intStatus = objV.Status;

                    switch (new Common().ValidateVoucherCode(intStatus))
                    {
                        case Common.VoucherMsg.Invalidvouchercode:
                            strMessage = "Voucher code is not valid.";
                            errorCustomerVoucherCode.InnerHtml = "Error: " + strMessage;
                            errorCustomerVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Vouchercodenotexists:
                            strMessage = "Voucher code does not exists.";
                            errorCustomerVoucherCode.InnerHtml = "Error: " + strMessage;
                            errorCustomerVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Voucherexpired:
                            strMessage = "Vouchercode is expired.";
                            errorCustomerVoucherCode.InnerHtml = "Error: " + strMessage;
                            errorCustomerVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Voucheralreadyused:
                            strMessage = "Vouchercode is expired.";
                            errorCustomerVoucherCode.InnerHtml = "Error: " + strMessage;
                            errorCustomerVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;

                        case Common.VoucherMsg.Success:
                            errorCustomerVoucherCode.Style.Add("display", "none");
                            ModifyBasket.Update();
                            isValidate = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                isValidate = false;
            }
            return isValidate;
        }
        private bool ValidateDiscountVoucherCode(string voucher)
        {
            bool isValidate = false;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    Encryption objEncryption = new Encryption();
                    string strOrderId = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                    CommonBal objCommonBal = new CommonBal();
                    VoucherInfo objV = objCommonBal.ValidateVoucherCode(strOrderId, voucher, new Common().GetSiteId());
                    int intStatus = 0;
                    string strMessage = string.Empty;
                    intStatus = objV.Status;
                    
                    switch (new Common().ValidateVoucherCode(intStatus))
                    {
                        case Common.VoucherMsg.Invalidvouchercode:
                            strMessage = "Voucher code is not valid.";
                            ErrorVoucherCode.InnerHtml = "Error: " + strMessage;
                            ErrorVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Vouchercodenotexists:
                            strMessage = "Voucher code does not exists.";
                            ErrorVoucherCode.InnerHtml = "Error: " + strMessage;
                            ErrorVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Voucherexpired:
                            strMessage = "Vouchercode is expired.";
                            ErrorVoucherCode.InnerHtml = "Error: " + strMessage;
                            ErrorVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Voucheralreadyused:
                            strMessage = "Vouchercode is expired.";
                            ErrorVoucherCode.InnerHtml = "Error: " + strMessage;
                            ErrorVoucherCode.Style.Add("display", "block");
                            isValidate = false;
                            break;
                        case Common.VoucherMsg.Success:
                            ErrorVoucherCode.Style.Add("display", "none");
                            ModifyBasket.Update();
                            isValidate = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                isValidate = false;
            }
            return isValidate;
        }
        private void bindAddress()
        {

            CustomerFullName.InnerHtml = CustomerFirstName.Value + "" + CustomerLastName.Value;
            CustomerEmailAddress.InnerHtml = CustomerEmail.Value;
            CustomerPCAAddress.InnerHtml = hdnPCAAddress.Value;
            CustomerPCACity.InnerHtml = hdnPCACity.Value;
            CustomerPCAPostCode.InnerHtml = hdnPCAPostCode.Value;
            CustomerPCACountry.InnerHtml = hdnPCACountry.Value;
        }
        private void CheckGuest()
        {
            if (Request.QueryString["guest"] != null)
            {
                if (Convert.ToString(Request.QueryString["guest"]) == "yes")
                {
                    divPassword.Style.Add("display", "none");

                }
                else
                {
                    divPassword.Style.Add("display", "block");

                }
            }
            else
            {
                divPassword.Style.Add("display", "block");

            }
        }
        private void SaveCustomerDetails()
        {
            int CustomerId = 0;
            CustomerInfo objCustomerInfo = new CustomerInfo();
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            Encryption objEncryption = new Encryption();
            try
            {
                if (Common.GetQueryStringValue("s") != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();

                    CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                    objCustomerInfo.CustomerId = CustomerId;

                    objCustomerInfo.OrderID = OrderID;
                    objCustomerInfo.Email = CustomerEmail.Value.Trim();
                    objCustomerInfo.FirstName = CustomerFirstName.Value.Trim();
                    objCustomerInfo.LastName = CustomerLastName.Value.Trim();
                    objCustomerInfo.HouseNo = hdnPCAHouseNumber.Value;
                    objCustomerInfo.Street = hdnPCAStreet.Value;
                    objCustomerInfo.District = hdnPCADistrict.Value;
                    objCustomerInfo.Town = hdnPCACity.Value;
                    objCustomerInfo.PostCode = hdnPCAPostCode.Value;
                    CommonBal objBAl = new CommonBal();
                    int country = objBAl.GetCountryIdCountryCode(hdnPCACountry.Value);
                    objCustomerInfo.CountryID = country;
                    if (ReceiveOffersByEmail.Checked == true)
                        objCustomerInfo.EmailNewsletter = 1;
                    else
                        objCustomerInfo.EmailNewsletter = 0;
                    if (ReceiveOffersBySMS.Checked == true)
                        objCustomerInfo.SMSNotification = 1;
                    else
                        objCustomerInfo.SMSNotification = 0;
                    if (textareaCustomerPassword.Value.Trim() != "" && textareaCustomerConfirmPassword.Value.Trim() != "")
                    {
                        if (textareaCustomerPassword.Value.Trim() == textareaCustomerConfirmPassword.Value.Trim())
                        {
                            Common objCommon = new Common();
                            objCustomerInfo.EncryptedPassword = objCommon.CalculateMD5Hash(textareaCustomerPassword.Value.Trim());
                        }
                    }
                    objCustomerInfo.UKMobile = "(" + CustomerCountryCode.Text.Trim() + ")" + CustomerMobileNumber.Value.Trim();
                    objCustomerDetails.EditCustomerDetails(objCustomerInfo);
                    if (Request.QueryString["guest"] != null)
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"] + "&guest=yes", false);
                    }
                    else
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void SavedCustomerManually()
        {
            int CustomerId = 0;
            CustomerInfo objCustomerInfo = new CustomerInfo();
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            Encryption objEncryption = new Encryption();
            try
            {
                if (Common.GetQueryStringValue("s") != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();

                    CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                    objCustomerInfo.CustomerId = CustomerId;
                    objCustomerInfo.OrderID = OrderID;
                    if (hdnEnterManualyEdit.Value != "true")
                    {

                        objCustomerInfo.Email = CustomerEmail.Value.Trim();
                        objCustomerInfo.FirstName = CustomerFirstName.Value.Trim();
                        objCustomerInfo.LastName = CustomerLastName.Value.Trim();
                        objCustomerInfo.HouseNo = ManualEditCustomerHouseNumber.Value.Trim();
                        objCustomerInfo.Street = ManualEditCustomerStreet.Value.Trim();
                        objCustomerInfo.District = ManualEditCustomerDistrict.Value.Trim();
                        objCustomerInfo.Town = ManualEditCustomerCity.Value.Trim();
                        objCustomerInfo.PostCode = ManualEditCustomerPostCode.Value.Trim();
                    }
                    else
                    {
                        objCustomerInfo.Email = ManualEditEmail.Value.Trim();
                        objCustomerInfo.FirstName = ManualEditFirstName.Value.Trim();
                        objCustomerInfo.LastName = ManualEditLastName.Value.Trim();
                        objCustomerInfo.HouseNo = ManualEditCustomerHouseNumber.Value.Trim();
                        objCustomerInfo.Street = ManualEditCustomerStreet.Value.Trim();
                        objCustomerInfo.District = ManualEditCustomerDistrict.Value.Trim();
                        objCustomerInfo.Town = ManualEditCustomerCity.Value.Trim();
                        objCustomerInfo.PostCode = ManualEditCustomerPostCode.Value.Trim();
                    }
                    objCustomerInfo.CountryID = Convert.ToInt32(CustomerCountry.SelectedItem.Value);
                    if (IsReceiveOffersByEmail.Checked == true)
                        objCustomerInfo.EmailNewsletter = 1;
                    else
                        objCustomerInfo.EmailNewsletter = 0;
                    if (IsReceiveOffersBySMS.Checked == true)
                        objCustomerInfo.SMSNotification = 1;
                    else
                        objCustomerInfo.SMSNotification = 0;
                    if (ManualEditCustomerPassword.Value.Trim() != "")
                    {
                        //if (CustomerPassword.Value.Trim() == CustomerConfirmPassword.Value.Trim())
                        //{
                        Common objCommon = new Common();
                        objCustomerInfo.EncryptedPassword = objCommon.CalculateMD5Hash(ManualEditCustomerPassword.Value.Trim());
                        //}
                    }
                    objCustomerInfo.UKMobile = CountryCode.Text.Trim() + MobileNumber.Value.Trim();
                    objCustomerDetails.EditCustomerDetails(objCustomerInfo);
                    if (Request.QueryString["guest"] != null)
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"] + "&guest=yes", false);
                    }
                    else
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void SavedPCACustomer()
        {
            int CustomerId = 0;
            CustomerInfo objCustomerInfo = new CustomerInfo();
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            Encryption objEncryption = new Encryption();
            try
            {
                if (Common.GetQueryStringValue("s") != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();

                    CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                    objCustomerInfo.CustomerId = CustomerId;
                    objCustomerInfo.OrderID = OrderID;

                    objCustomerInfo.Email = PCAEditEmail.Value.Trim();
                    objCustomerInfo.FirstName = PCAEditFirstName.Value.Trim();
                    objCustomerInfo.LastName = PCAEditLastName.Value.Trim();
                    objCustomerInfo.HouseNo = PCAEditCustomerHourseNumber.Value.Trim();
                    objCustomerInfo.Street = PCAEditCustomerStreet.Value.Trim();
                    objCustomerInfo.District = PCAEditCustomerDistrict.Value.Trim();
                    objCustomerInfo.Town = PCAEditCustomerCity.Value.Trim();
                    objCustomerInfo.PostCode = PCAEditCustomerPostCode.Value.Trim();

                    objCustomerInfo.CountryID = Convert.ToInt32(CustomerCountry.SelectedItem.Value);
                    if (IsReceiveOffersByEmail.Checked == true)
                        objCustomerInfo.EmailNewsletter = 1;
                    else
                        objCustomerInfo.EmailNewsletter = 0;
                    if (IsReceiveOffersBySMS.Checked == true)
                        objCustomerInfo.SMSNotification = 1;
                    else
                        objCustomerInfo.SMSNotification = 0;
                    if (ManualEditCustomerPassword.Value.Trim() != "")
                    {
                        //if (CustomerPassword.Value.Trim() == CustomerConfirmPassword.Value.Trim())
                        //{
                        Common objCommon = new Common();
                        objCustomerInfo.EncryptedPassword = objCommon.CalculateMD5Hash(ManualEditCustomerPassword.Value.Trim());
                        //}
                    }
                    objCustomerInfo.UKMobile = "(" + CountryCode.Text.Trim() + ")" + MobileNumber.Value.Trim();
                    objCustomerDetails.EditCustomerDetails(objCustomerInfo);
                    if (Request.QueryString["guest"] != null)
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"] + "&guest=yes", false);
                    }
                    else
                    {
                        Response.Redirect("m_recipientDetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void ContinueRecipient_Click(object sender, EventArgs e)
        {
            try
            {

                string strIsValid = ValidateEmail(PCAEditEmail.Value);
                if (strIsValid == "false")
                {

                    ScriptManager.RegisterClientScriptBlock(ContinueRecipient, ContinueRecipient.GetType(), "Add4421ressDiv", "EmailVerifyPopup('" + PCAEditEmail.Value + "');", true);
                }
                else
                {




                    if (!string.IsNullOrEmpty(DiscountVoucherCode.Value.Trim()))
                    {
                        if (ValidateDiscountVoucherCode(DiscountVoucherCode.Value.Trim()) == true)
                        {
                            SavedCustomerManually();
                        }
                        else
                        {


                            if (hdnEnterAddManually.Value == "true" && hdnEnterManualyEdit.Value != "true")
                            {
                                ScriptManager.RegisterClientScriptBlock(ContinueRecipient, ContinueRecipient.GetType(), "Add4421ressDiv", "clickEnterAddressManually();", true);
                            }
                            else if (hdnEnterAddManually.Value == "true" && hdnEnterManualyEdit.Value == "true")
                            {
                                ScriptManager.RegisterClientScriptBlock(ContinueRecipient, ContinueRecipient.GetType(), "Addr34231essDiv", "clickEnterAddressManually();", true);
                                ScriptManager.RegisterClientScriptBlock(ContinueRecipient, ContinueRecipient.GetType(), "Addr423essDiv", "clickManualEdit();", true);
                            }
                            ScriptManager.RegisterClientScriptBlock(ContinueRecipient, ContinueRecipient.GetType(), "Add4421ressDiv", "GetDiscountFocus();", true);
                        }
                    }
                    else
                    {
                        ErrorVoucherCode.InnerHtml = " ";
                        ErrorVoucherCode.Style.Add("display", "none");
                        SavedCustomerManually();
                    }
                }
                //bindAddress();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
  
        protected void SavePCADetails_Click(object sender, EventArgs e)
        {
            try
            {
                string strIsValid = ValidateEmail(PCAEditEmail.Value);
                if (strIsValid == "false")
                {

                    ScriptManager.RegisterClientScriptBlock(SavePCADetails, SavePCADetails.GetType(), "Add4421ressDiv", "EmailVerifyPopup('" + PCAEditEmail.Value + "');", true);
                }
                else
                {


                    if (!string.IsNullOrEmpty(DiscountVoucherCode.Value.Trim()))
                    {
                        if (ValidateDiscountVoucherCode(DiscountVoucherCode.Value.Trim()) == true)
                        {
                            SavedPCACustomer();
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(SavePCADetails, SavePCADetails.GetType(), "34213", "clickPCAEdit();", true);
                            ScriptManager.RegisterClientScriptBlock(SavePCADetails, SavePCADetails.GetType(), "Add4421ressDiv", "GetDiscountFocus();", true);
                        }
                    }
                    else
                    {
                        ErrorVoucherCode.InnerHtml = " ";
                        ErrorVoucherCode.Style.Add("display", "none");
                        SavedPCACustomer();
                    }
                    //bindAddress();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                //Response.Redirect("m_login.aspx?s=" + Request.QueryString["s"], false);
                Response.Redirect("m_upsells.aspx?s=" + Request.QueryString["s"], false);
            }
        }

        protected void CustomerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            try
            {
                string CountryDialingCode = objCustomerDetails.GetCountryDialingCodeByCuontyID(Convert.ToInt32(CustomerCountry.SelectedValue));
                CountryCode.Text = CountryDialingCode;
                if (hdnPCAEdit.Value == "true")
                {
                    ScriptManager.RegisterClientScriptBlock(CustomerCountry, CustomerCountry.GetType(), "34213", "clickPCAEdit();", true);
                }
                else
                {
                    if (hdnEnterAddManually.Value == "true" && hdnEnterManualyEdit.Value != "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(CustomerCountry, CustomerCountry.GetType(), "Add4421ressDiv", "clickEnterAddressManually();", true);
                    }
                    else if (hdnEnterAddManually.Value == "true" && hdnEnterManualyEdit.Value == "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(CustomerCountry, CustomerCountry.GetType(), "Addr34231essDiv", "clickEnterAddressManually();", true);
                        ScriptManager.RegisterClientScriptBlock(CustomerCountry, CustomerCountry.GetType(), "Addr423essDiv", "clickManualEdit();", true);
                    }
                }

                //clickEnterAddressManually
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        public void DisplayCustomerDetails(string OrderId)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            CustomerInfo objCustomerInfo = new CustomerInfo();
            Encryption objEncrypt = new Encryption();
            try
            {
                objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderId);
                if (string.IsNullOrEmpty(objCustomerInfo.UKMobile) && !string.IsNullOrEmpty(objCustomerInfo.Email))
                {
                    content2.Style.Add("display", "block");
                    content3.Style.Add("display", "none");
                    content1.Style.Add("display", "none");
                    CustomerPCACountry.InnerHtml = objCustomerInfo.Country;
                    hdnPCACountry.Value = objCustomerInfo.Country;
                    CustomerPCACity.InnerText = objCustomerInfo.Town;
                    hdnPCACity.Value = objCustomerInfo.Town;
                    hdnPCAHouseNumber.Value = objCustomerInfo.HouseNo;
                    hdnPCAStreet.Value = objCustomerInfo.Street;
                    hdnPCADistrict.Value = objCustomerInfo.District;
                    CustomerPCAPostCode.InnerHtml = objCustomerInfo.PostCode;
                    hdnPCAPostCode.Value = objCustomerInfo.PostCode;
                    CustomerFullName.InnerHtml = objCustomerInfo.FirstName + " " + objCustomerInfo.LastName;
                    CustomerEmailAddress.InnerHtml = objCustomerInfo.Email;
                    hdnCustomerFirstname.Value = objCustomerInfo.FirstName;
                    hdnCustomerLastName.Value = objCustomerInfo.LastName;


                    System.Text.StringBuilder custAddress = new System.Text.StringBuilder();

                    if (!string.IsNullOrEmpty(objCustomerInfo.Organisation))
                    {
                        custAddress.Append(objCustomerInfo.Organisation + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.HouseNo))
                    {
                        custAddress.Append(objCustomerInfo.HouseNo + ",");

                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.District))
                    {
                        custAddress.Append(objCustomerInfo.District + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Street))
                    {
                        custAddress.Append(objCustomerInfo.Street + ",");
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Town))
                    {
                        custAddress.Append(objCustomerInfo.Town);
                    }
                    CustomerPCAAddress.InnerHtml = custAddress.ToString();
                    if (objCustomerInfo.Country != "")
                    {
                        CustomerCountry.SelectedIndex = -1;
                        CustomerCountry.Items.FindByText(objCustomerInfo.Country).Selected = true;
                        CustomerCountry_SelectedIndexChanged(null, null);
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "34trt213", "displaySecondScreen();", true);
                }
                else if (objCustomerInfo.Email != null && objCustomerInfo.Email != "")
                {

                    divPCAEdit.Style.Add("display", "block");
                    PCASave.Style.Add("display", "block");
                    content1.Style.Add("display", "none");
                    content2.Style.Add("display", "none");
                    content3.Style.Add("display", "block");

                    PCAEditEmail.Value = objCustomerInfo.Email;
                    PCAEditFirstName.Value = objCustomerInfo.FirstName;
                    PCAEditLastName.Value = objCustomerInfo.LastName;
                    PCAEditOrganization.Value = objCustomerInfo.Organisation;
                    PCAEditCustomerHourseNumber.Value = objCustomerInfo.HouseNo;
                    PCAEditCustomerStreet.Value = objCustomerInfo.Street;
                    PCAEditCustomerDistrict.Value = objCustomerInfo.District;
                    PCAEditCustomerCity.Value = objCustomerInfo.Town;
                    PCAEditCustomerPostCode.Value = objCustomerInfo.PostCode;
                    if (objCustomerInfo.Country != "")
                    {
                        CustomerCountry.SelectedIndex = -1;
                        CustomerCountry.Items.FindByText(objCustomerInfo.Country).Selected = true;
                        CustomerCountry_SelectedIndexChanged(null, null);
                    }
                    if (objCustomerInfo.EmailNewsletter == 1)
                    {
                        IsReceiveOffersByEmail.Checked = true;
                    }
                    else
                    {
                        IsReceiveOffersByEmail.Checked = false;
                    }
                    if (objCustomerInfo.SMSNotification == 1)
                    {
                        IsReceiveOffersBySMS.Checked = true;
                    }
                    else
                    {
                        IsReceiveOffersBySMS.Checked = false;
                    }
                    string dialingCode = CustomerCountryCode.Text.Trim();
                    MobileNumber.Value = objCustomerInfo.UKMobile.Remove(0, dialingCode.Length);
                    SetNewMobileFields(objCustomerInfo.UKMobile);

                }
                else
                {

                    content2.Style.Add("display", "block");
                    content2.Style.Add("display", "none");
                    content3.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }


        private void SetNewMobileFields(string PhoneNumber)
        {
            try
            {
                if (PhoneNumber.Contains("(") || PhoneNumber.Contains(")"))
                {
                    MobileNumber.InnerText = PhoneNumber.Split(')')[1].ToString();
                    CountryCode.Text = PhoneNumber.Split(')')[0].ToString().Replace("(", "");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void CapturePCAData_Click(object sender, EventArgs e)
        {
            int CustomerId = 0;
            CustomerInfo objCustomerInfo = new CustomerInfo();
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            Encryption objEncryption = new Encryption();
            try
            {
                if (Common.GetQueryStringValue("s") != null)
                {
                    string OrderID = Common.GetOrderIdFromQueryString();

                    CustomerId = objCustomerDetails.GetCustomerIdByOrderId(OrderID);
                    objCustomerInfo.CustomerId = CustomerId;

                    objCustomerInfo.OrderID = OrderID;
                    objCustomerInfo.Email = CustomerEmail.Value.Trim();
                    objCustomerInfo.FirstName = CustomerFirstName.Value.Trim();
                    objCustomerInfo.LastName = CustomerLastName.Value.Trim();
                    objCustomerInfo.HouseNo = hdnPCAHouseNumber.Value;
                    objCustomerInfo.Street = hdnPCAStreet.Value;
                    objCustomerInfo.District = hdnPCADistrict.Value;
                    objCustomerInfo.Town = hdnPCACity.Value;
                    objCustomerInfo.PostCode = hdnPCAPostCode.Value;
                    objCustomerInfo.Organisation = hdnPCAOrganisation.Value;
                    CommonBal objBAl = new CommonBal();
                    int country = objBAl.GetCountryIdCountryCode(hdnPCACountry.Value);
                    objCustomerInfo.CountryID = country;
                    if (ReceiveOffersByEmail.Checked == true)
                        objCustomerInfo.EmailNewsletter = 1;
                    else
                        objCustomerInfo.EmailNewsletter = 0;
                    if (ReceiveOffersBySMS.Checked == true)
                        objCustomerInfo.SMSNotification = 1;
                    else
                        objCustomerInfo.SMSNotification = 0;
                    if (textareaCustomerPassword.Value.Trim() != "" && textareaCustomerConfirmPassword.Value.Trim() != "")
                    {
                        if (textareaCustomerPassword.Value.Trim() == textareaCustomerConfirmPassword.Value.Trim())
                        {
                            Common objCommon = new Common();
                            objCustomerInfo.EncryptedPassword = objCommon.CalculateMD5Hash(textareaCustomerPassword.Value.Trim());
                        }
                    }
                    objCustomerInfo.UKMobile = CustomerCountryCode.Text.Trim() + CustomerMobileNumber.Value.Trim();

                    CustomerFullName.InnerText = objCustomerInfo.FirstName + " " + objCustomerInfo.LastName;
                    CustomerEmailAddress.InnerText = objCustomerInfo.Email;
                    CustomerPCAAddress.InnerText = hdnPCAAddress.Value;
                    CustomerPCACity.InnerText = hdnPCACity.Value;
                    CustomerPCAPostCode.InnerText = hdnPCAPostCode.Value;
                    CustomerPCACountry.InnerText = hdnPCACountry.Value;

                    string CountryDialingCode = objCustomerDetails.GetCountryDialingCodeByCuontyID(country);
                    CustomerCountryCode.Text = CountryDialingCode.Trim();


                    objCustomerDetails.EditCustomerDetails(objCustomerInfo);
                    content2.Style.Add("display", "block");
                    content3.Style.Add("display", "none");
                    content1.Style.Add("display", "none");

                    ScriptManager.RegisterClientScriptBlock(CapturePCAData, CapturePCAData.GetType(), "34trt213", "displaySecondScreen();", true);

                    //updContent2.Update();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        //protected void updContent2_Load(object sender, EventArgs e)
        //{
        //    content2.Style.Add("display", "block");
        //    content3.Style.Add("display", "none");
        //    content1.Style.Add("display", "none");
        //}
        public void DisplayScreen()
        {


        }
        public static void RemoveCookie(string key, HttpResponse response, HttpRequest request)
        {
            //check that the request object is valid
            if (request == null) return;
            //check that the response object is valid
            if (response == null) return;
            //check key is passed in
            if (string.IsNullOrEmpty(key)) return;

            //check if the cookie exists
            if (request.Cookies[key] != null)
            {
                //create a new cookie to replace the current cookie
                HttpCookie newCookie = new HttpCookie(key);
                //set the new cookie to expire 1 day ago
                newCookie.Expires = DateTime.Now.AddDays(-1d);
                //update the cookies collection on the response
                response.Cookies.Add(newCookie);
            }
        }

        private int DbLookUpForEmail()
        {
            int CustId = 0;
            try
            {
                CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                CustId = objCustomerDetails.CheckCustomer(CustomerEmail.Value);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);                
            }
            return CustId;
        }

        /// <summary>
        /// Prompts already registered email id for login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomerEmail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int CustId = DbLookUpForEmail();
                if (CustId > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(CustomerEmail, CustomerEmail.GetType(), "test123", "PromptLoginPopup();", true);
                }
                else
                {
                    string strIsValid = ValidateEmail(CustomerEmail.Value);
                    if (strIsValid == "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(CustomerEmail, CustomerEmail.GetType(), "CustomerEmailpass", "PassedETEmail();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(CustomerEmail, CustomerEmail.GetType(), "CustomerEmail", "failedETEmail();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);                   
            }
        }

        public string ValidateEmail(string strEmail)
        {
            string isvalidate = String.Empty;
            try
            {
                FuelAPIValidateEmail objValidateEmail = new FuelAPIValidateEmail();
                isvalidate = objValidateEmail.ValidateEmail(strEmail);

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return isvalidate;
        }

        [WebMethod]
        public static string GetCurrentDate(string name)
        {
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            int CustId = objCustomerDetails.CheckCustomer(name);
            if (CustId > 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "PromptLoginPopup();", true);
            }
            return CustId.ToString();
        }
    }

}