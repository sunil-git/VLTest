using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.BAL.Checkout;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using SFMobile.BAL.Orders;
using SerenataOrderSchemaBAL;
namespace Serenataflowers.Checkout
{
    public partial class RecipientDetails : System.Web.UI.Page
    {
        #region Variables
        CommonFunctions objSearchAddress;
        CheckoutLogic objCheckout;
        OrdersLogic objOrder;
        string decryOrderId;
        string DecryptrdOrderId;
        string orderId;
        #region Declared/Initilised BAL object for New Order Schema
        SerenataCheckoutLogic objOrderSchemaRecipient = new SerenataCheckoutLogic();

        #endregion
        #endregion

        #region Page Event
        /// <summary>
        /// This Event is fired usually the most common used method on the server side 
        /// application code for an .aspx file. All code inside of this method is executed once at the beginning of 
        /// the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string.IsNullOrEmpty(Request.QueryString["s"]) == false) && (string.IsNullOrEmpty(Request.QueryString["t"]) == false))
            {

                objSearchAddress = new CommonFunctions();
                orderId = objSearchAddress.Decrypt(Request.QueryString["s"], "testpage");
                hdnconfilename.Value = "config_" + Request.QueryString["t"] + ".xml"; ;
                hdnDecrptedOrderId.Value = orderId;
                CreateMetaTags(orderId);
                if (!IsPostBack)
                {
                    //set Gift message maxlength based on fulfilment partner
                    objOrder = new OrdersLogic();
                    int messageLength = objOrder.GetCardMessageLength(orderId);
                    hdnMaxLength.Value = Convert.ToString(messageLength);
                    mycounter.InnerText = hdnMaxLength.Value + " characters left.";
                    var url = String.Format("https://{0}{1}", Request.ServerVariables["HTTP_HOST"], ResolveUrl("CustomerDetails.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"]));
                    ancStp.HRef = url;
                    txtCountry_field.Text = "United Kingdom";
                    // Page.Title = CommonFunctions.PageTitle() + " - Checkout - Recipent Details.";
                    ltTitle.Text = "\n<title>" + CommonFunctions.PageTitle() + " - Checkout - Recipent Details." + "</title>\n";
                    //Page.Title = " Flowers UK London | Flower Delivery UK | Florist | Flowers Online| Send Flowers to England Florists ";
                    hdnEncryptionUrl.Value = ConfigurationManager.AppSettings["encryptionUrlPath"];


                    RetainedData(orderId);
                    //if (SerenataflowersSessions.CountryName!=null && SerenataflowersSessions.CountryName != "United Kingdom")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);
                    //    divPrefer.Style.Add("display", "none");
                    //}
                    //else
                    //{
                    divPrefer.Style.Add("display", "block");
                    //}


                }

            }
            else
            {
                Response.Redirect("~/Default.aspx", true);
            }
            //CommonFunctions.AddFloodLightTags(this.Page);

        }

        string GetUserEnteredAddress()
        {
            string strAddress = string.Empty;
            try
            {
                string organisation = string.Empty;
                string houseno = string.Empty;
                string street = string.Empty;
                string town = string.Empty;
                string postcode = string.Empty;

                organisation = txtOrganisation_field.Text.Trim();
                houseno = txtAddressLine1.Text.Trim();
                street = txtStreet_field.Text.Trim();
                town = txtTownCity.Text.Trim();
                postcode = txtPostCode.Text.Trim();

                strAddress = organisation + "," + houseno + "," + street + "," + town + "," + postcode;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return strAddress;
        }

        bool IsCustomerAddressSameAsAPI()
        {
            bool boolResult = false;
            try
            {
                string custAddress = string.Empty;
                string organisation = string.Empty;
                string houseno = string.Empty;
                string street = string.Empty;
                string town = string.Empty;
                string postcode = string.Empty;

                organisation = txtOrganisation_field.Text.Trim();
                houseno = txtAddressLine1.Text.Trim();
                street = txtStreet_field.Text.Trim();
                town = txtTownCity.Text.Trim();
                postcode = txtPostCode.Text.Trim();
                custAddress = organisation + " " + houseno + " " + street + " " + town + " " + postcode.Trim();
                custAddress = custAddress.Replace(",", "");
                custAddress = custAddress.Replace("  ", " ");
                custAddress = custAddress.Trim();
                DataSet objDset = new CommonFunctions().CleanseAddress(GetUserEnteredAddress());
                if (objDset != null && objDset.Tables.Count > 4 && objDset.Tables[4] != null)
                {
                    DataTable dt = objDset.Tables[4];
                    if (dt.Rows[0]["description"].ToString().ToLower() == custAddress.ToLower())
                    {
                        addressVerify.Value = "1";
                        CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                        CustomerInfo objCustomerInfo = new CustomerInfo();
                        objCustomerInfo.CountryCode = txtCountry_field.Text;
                        objCustomerInfo.County = txtCounty_field.Text;
                        if (chkBxDelivery_instruction_check.Checked)
                            //objCustomerInfo.DeliveryIns = txtDelivery_instructions.InnerText;
                            objCustomerInfo.DeliveryIns = drpDelivery_Instructions.SelectedValue;
                        objCustomerInfo.District = txtDistrict_field.Text;
                        if (chkBxCard_message_check.Checked)
                            objCustomerInfo.GiftMessage = txtCard_message.InnerText;
                        objCustomerInfo.HouseNo = txtAddressLine1.Text;
                        objCustomerInfo.NoCardMessage = chkBxCard_message_check.Checked ? 1 : 0;
                        objCustomerInfo.Organisation = txtOrganisation_field.Text;
                        objCustomerInfo.PostCode = txtPostCode.Text;
                        objCustomerInfo.ReceipentTelPhNo = txtContact_field.Text;
                        objCustomerInfo.Street = txtStreet_field.Text;
                        objCustomerInfo.Town = txtTownCity.Text;
                        objCustomerInfo.OrderID = orderId;
                        objCustomerInfo.Addr_Verified = 1;

                        string name = txtFirstName.Text;
                        string firstName = string.Empty;
                        string lastName = string.Empty;


                        if (name.Contains(' '))
                        {
                            firstName = name.Substring(0, name.IndexOf(' '));
                            lastName = name.Substring(name.IndexOf(' ') + 1);
                        }
                        else
                            firstName = name;
                        objCustomerInfo.Name = firstName;
                        objCustomerInfo.LastName = lastName;

                        int result = objCheckOutLogic.InsertUpdateRecipientDetails(objCustomerInfo);
                        #region updated  Addr_Verified into DB
                        CustomerInfo objCustomerInfoNew = new CustomerInfo();
                        objCustomerInfoNew.OrderID = orderId;
                        objCustomerInfoNew.Addr_Verified = 1;
                        objOrderSchemaRecipient.AcceptAddress(objCustomerInfoNew);
                        #endregion
                        boolResult = true;
                    }
                    else
                    {
                        addressVerify.Value = "0";
                        boolResult = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return boolResult;
        }


        void FillSuggestedAddress()
        {
            DataSet objDset = null;
            if (lstSuggestedAdd.Items.Count > 0)
            {
                lstSuggestedAdd.Items.Clear();
            }
            objDset = new CommonFunctions().CleanseAddress(GetUserEnteredAddress());
            if (objDset != null && objDset.Tables.Count > 4 && objDset.Tables[4] != null)
            {
                lstSuggestedAdd.DataSource = objDset.Tables[4];
                lstSuggestedAdd.DataTextField = "description";
                lstSuggestedAdd.DataValueField = "id";
                lstSuggestedAdd.DataBind();

                lstSuggestedAdd.Items.Insert(0, new ListItem("--Please select one of the suggested addresses--", "0"));
                lstSuggestedAdd.SelectedIndex = 0;

            }
            if (lstSuggestedAdd.Items.Count > 0)
            {
                hasApiAddress.Value = "1";
            }
            else
            {
                hasApiAddress.Value = "0";
            }
        }

        protected void UseThisAddress_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);
                FillAddressFields(lstSuggestedAdd.SelectedValue);

                CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                CustomerInfo objCustomerInfo = new CustomerInfo();
                objCustomerInfo.CountryCode = txtCountry_field.Text;
                objCustomerInfo.County = txtCounty_field.Text;
                if (chkBxDelivery_instruction_check.Checked)
                    //objCustomerInfo.DeliveryIns = txtDelivery_instructions.InnerText;
                    objCustomerInfo.DeliveryIns = drpDelivery_Instructions.SelectedValue;
                objCustomerInfo.District = txtDistrict_field.Text;
                if (chkBxCard_message_check.Checked)
                    objCustomerInfo.GiftMessage = txtCard_message.InnerText;
                objCustomerInfo.HouseNo = txtAddressLine1.Text;
                objCustomerInfo.NoCardMessage = chkBxCard_message_check.Checked ? 1 : 0;
                objCustomerInfo.Organisation = txtOrganisation_field.Text;
                objCustomerInfo.PostCode = txtPostCode.Text;
                objCustomerInfo.ReceipentTelPhNo = txtContact_field.Text;
                objCustomerInfo.Street = txtStreet_field.Text;
                objCustomerInfo.Town = txtTownCity.Text;
                objCustomerInfo.OrderID = orderId;
                if (ViewState["addr_verified"] != null)
                {
                    objCustomerInfo.Addr_Verified = 1;
                }
                else
                {
                    objCustomerInfo.Addr_Verified = 0;
                }
                string name = txtFirstName.Text;
                string firstName = string.Empty;
                string lastName = string.Empty;


                if (name.Contains(' '))
                {
                    firstName = name.Substring(0, name.IndexOf(' '));
                    lastName = name.Substring(name.IndexOf(' ') + 1);
                }
                else
                    firstName = name;
                objCustomerInfo.Name = firstName;
                objCustomerInfo.LastName = lastName;

                result = objCheckOutLogic.InsertUpdateRecipientDetails(objCustomerInfo);
                #region updated  Addr_Verified into DB
                CustomerInfo objCustomerInfoNew = new CustomerInfo();
                if (ViewState["addr_verified"] != null)
                {
                    objCustomerInfoNew.Addr_Verified = 1;
                }
                else
                {
                    objCustomerInfoNew.Addr_Verified = 0;
                }
                objCustomerInfoNew.OrderID = orderId;
                objOrderSchemaRecipient.AcceptAddress(objCustomerInfoNew);
                #endregion
                GenerateaAndStoreOrderXml(objCustomerInfo.OrderID);

                ScriptManager.RegisterClientScriptBlock(UseThisAddress, UseThisAddress.GetType(), "postForm", "encrypt();", true);



            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }

        protected void UseTheAddressIEntered_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);

                CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                CustomerInfo objCustomerInfo = new CustomerInfo();
                objCustomerInfo.CountryCode = txtCountry_field.Text;
                objCustomerInfo.County = txtCounty_field.Text;
                if (chkBxDelivery_instruction_check.Checked)
                    //objCustomerInfo.DeliveryIns = txtDelivery_instructions.InnerText;
                    objCustomerInfo.DeliveryIns = drpDelivery_Instructions.SelectedValue;
                objCustomerInfo.District = txtDistrict_field.Text;
                if (chkBxCard_message_check.Checked)
                    objCustomerInfo.GiftMessage = txtCard_message.InnerText;
                objCustomerInfo.HouseNo = txtAddressLine1.Text;
                objCustomerInfo.NoCardMessage = chkBxCard_message_check.Checked ? 1 : 0;
                objCustomerInfo.Organisation = txtOrganisation_field.Text;
                objCustomerInfo.PostCode = txtPostCode.Text;
                objCustomerInfo.ReceipentTelPhNo = txtContact_field.Text;
                objCustomerInfo.Street = txtStreet_field.Text;
                objCustomerInfo.Town = txtTownCity.Text;
                objCustomerInfo.OrderID = orderId;
                objCustomerInfo.Addr_Verified = 1;

                string name = txtFirstName.Text;
                string firstName = string.Empty;
                string lastName = string.Empty;


                if (name.Contains(' '))
                {
                    firstName = name.Substring(0, name.IndexOf(' '));
                    lastName = name.Substring(name.IndexOf(' ') + 1);
                }
                else
                    firstName = name;
                objCustomerInfo.Name = firstName;
                objCustomerInfo.LastName = lastName;

                int result = objCheckOutLogic.InsertUpdateRecipientDetails(objCustomerInfo);
                #region updated  Addr_Verified into DB
                CustomerInfo objCustomerInfoNew = new CustomerInfo();
                objCustomerInfoNew.OrderID = orderId;
                objCustomerInfoNew.Addr_Verified = 1;
                objOrderSchemaRecipient.AcceptAddress(objCustomerInfoNew);
                #endregion
                ScriptManager.RegisterClientScriptBlock(UseTheAddressIEntered, UseTheAddressIEntered.GetType(), "postForm", "encrypt();", true);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }



        protected void ImgBtnYes_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);

                CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                CustomerInfo objCustomerInfo = new CustomerInfo();
                objCustomerInfo.CountryCode = txtCountry_field.Text;
                objCustomerInfo.County = txtCounty_field.Text;
                if (chkBxDelivery_instruction_check.Checked)
                    //objCustomerInfo.DeliveryIns = txtDelivery_instructions.InnerText;
                    objCustomerInfo.DeliveryIns = drpDelivery_Instructions.SelectedValue;
                objCustomerInfo.District = txtDistrict_field.Text;
                if (chkBxCard_message_check.Checked)
                    objCustomerInfo.GiftMessage = txtCard_message.InnerText;
                objCustomerInfo.HouseNo = txtAddressLine1.Text;
                objCustomerInfo.NoCardMessage = chkBxCard_message_check.Checked ? 1 : 0;
                objCustomerInfo.Organisation = txtOrganisation_field.Text;
                objCustomerInfo.PostCode = txtPostCode.Text;
                objCustomerInfo.ReceipentTelPhNo = txtContact_field.Text;
                objCustomerInfo.Street = txtStreet_field.Text;
                objCustomerInfo.Town = txtTownCity.Text;
                objCustomerInfo.OrderID = orderId;
                objCustomerInfo.Addr_Verified = 1;

                string name = txtFirstName.Text;
                string firstName = string.Empty;
                string lastName = string.Empty;


                if (name.Contains(' '))
                {
                    firstName = name.Substring(0, name.IndexOf(' '));
                    lastName = name.Substring(name.IndexOf(' ') + 1);
                }
                else
                    firstName = name;
                objCustomerInfo.Name = firstName;
                objCustomerInfo.LastName = lastName;

                int result = objCheckOutLogic.InsertUpdateRecipientDetails(objCustomerInfo);
                #region updated  Addr_Verified into DB
                CustomerInfo objCustomerInfoNew = new CustomerInfo();
                objCustomerInfoNew.OrderID = orderId;
                objCustomerInfoNew.Addr_Verified = 1;
                objOrderSchemaRecipient.AcceptAddress(objCustomerInfoNew);
                #endregion
                ScriptManager.RegisterClientScriptBlock(ImgBtnYes, ImgBtnYes.GetType(), "postForm", "encrypt();", true);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }


        /// <summary>
        /// This Event will fire when user want to searh address using PostCode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void imgBtnFind_residential_Click(object sender, EventArgs e)
        //{


        //    string errorMsg = string.Empty;
        //    bool flag;
        //    Button btn = (Button)sender;
        //    DataSet dsAddress = null;
        //    DataSet dsPostcode = null;
        //    divAddressFields.Style.Add("display", "none");
        //    objSearchAddress = new CommonFunctions();
        //    string ReaseanDesc = string.Empty;
        //    int validPc = 0;
        //    try
        //    {
        //        // code added for valid postcode
        //        CheckoutLogic objCheckoutLogic = new CheckoutLogic();


        //        if (btn.ID == "imgBtnFind_residential")
        //        {
        //            divBusinessLocator.Style.Add("display", "none");
        //            divPostCodes.Style.Add("display", "block");
        //            dsPostcode = objCheckoutLogic.PostcodeValid(txtfind_postcode.Text.Trim(), orderId);
        //            if (dsPostcode != null)
        //            {
        //                validPc = Convert.ToInt32(dsPostcode.Tables[0].Rows[0]["ValidPC"]);
        //                ReaseanDesc = Convert.ToString(dsPostcode.Tables[0].Rows[0]["ReasonDesc"]);
        //            }
        //            if (validPc == 1)
        //            {
        //                dsAddress = objSearchAddress.GetAddressByPostCode(txtfind_postcode.Text);
        //                if (dsAddress != null && dsAddress.Tables.Count > 4 && dsAddress.Tables[4] != null)
        //                {

        //                    flag = CheckValidPostCodeDataset(dsAddress, ref errorMsg);
        //                    if (flag == true)
        //                    {
        //                        ddlAddressPostCodes.Items.Clear();
        //                        ddlAddressPostCodes.DataSource = dsAddress.Tables[4];
        //                        ddlAddressPostCodes.DataTextField = "description";
        //                        ddlAddressPostCodes.DataValueField = "id";
        //                        ddlAddressPostCodes.DataBind();


        //                        divSelectAddressMsg.Style.Add("display", "block");
        //                        divPostCode_Address.Style.Add("display", "block");


        //                        ddlAddressPostCodes.Items.Insert(0, new ListItem("--Select your address--", "0"));
        //                        ddlAddressPostCodes.SelectedIndex = 0;

        //                    }
        //                    else
        //                    {

        //                        divPostCode_Address.Style.Add("display", "none");
        //                        divSelectAddressMsg.Style.Add("display", "none");

        //                    }


        //                }
        //                else
        //                {
        //                    divErrorMsg.Style.Add("display", "block");
        //                    spnErrorMsg.InnerText = "Sorry, we couldn't find any address to match the postcode entered. Please try again or enter the address manually.";
        //                    divSelectAddressMsg.Style.Add("display", "none");
        //                    divPostCode_Address.Style.Add("display", "none");

        //                }
        //            }
        //            else
        //            {
        //                //added code  for post code
        //                divErrorMsg.Style.Add("display", "block");
        //                divErrorMsg.Attributes.Add("class", "reqdottedline");
        //                spnErrorMsg.Style.Add("white-space", "normal");
        //                spnErrorMsg.Style.Add("color", "rgb(255, 255, 255)");
        //                spnErrorMsg.Style.Add("font-size", "11px");
        //                spnErrorMsg.InnerText = ReaseanDesc;
        //                divSelectAddressMsg.Style.Add("display", "none");
        //                divPostCode_Address.Style.Add("display", "none");
        //            }
        //        }
        //        else
        //        {
        //            divBusinessLocator.Style.Add("display", "block");
        //            divPostCodes.Style.Add("display", "none");
        //            dsPostcode = objCheckoutLogic.PostcodeValid(txtBusinessLocaterPostCode.Text.Trim(), orderId);
        //            if (dsPostcode != null)
        //            {
        //                validPc = Convert.ToInt32(dsPostcode.Tables[0].Rows[0]["ValidPC"]);
        //                ReaseanDesc = Convert.ToString(dsPostcode.Tables[0].Rows[0]["ReasonDesc"]);
        //            }
        //            if (validPc == 1)
        //            {
        //                dsAddress = objSearchAddress.GetAddressByOrganisationAndCity(txtBusinessLocaterOrg.Text, txtBusinessLocaterPostCode.Text);
        //                //dsAddress = GetAddressByOrganisationAndCity(txtBusinessLocaterOrg.Text, txtBusinessLocaterPostCode.Text);

        //                if (dsAddress != null && dsAddress.Tables.Count > 4 && dsAddress.Tables[4] != null)
        //                {
        //                    flag = CheckValidPostCodeDataset(dsAddress, ref errorMsg);
        //                    if (flag == true)
        //                    {
        //                        ddlAddressPostCodes.Items.Clear();
        //                        ddlAddressPostCodes.DataSource = dsAddress.Tables[4];
        //                        ddlAddressPostCodes.DataTextField = "description";
        //                        ddlAddressPostCodes.DataValueField = "id";
        //                        ddlAddressPostCodes.DataBind();
        //                        ddlAddressPostCodes.Style.Add("display", "block");

        //                        divPostCode_Address.Style.Add("display", "block");
        //                        divSelectAddressMsg.Style.Add("display", "block");

        //                        ddlAddressPostCodes.Items.Insert(0, new ListItem("---Select---", "0"));
        //                        ddlAddressPostCodes.SelectedIndex = 0;
        //                    }
        //                    else
        //                    {

        //                        divSelectAddressMsg.Style.Add("display", "none");
        //                        divPostCode_Address.Style.Add("display", "none");

        //                    }

        //                }
        //                else
        //                {
        //                    divErrorMsg.Style.Add("display", "block");
        //                    spnErrorMsg.InnerText = "Sorry, we couldn't find any address to match the postcode entered. Please try again or enter the address manually.";
        //                    divSelectAddressMsg.Style.Add("display", "none");
        //                    divPostCode_Address.Style.Add("display", "none");
        //                    divSelectAddressMsg.Style.Add("display", "none");
        //                }
        //            }
        //            else
        //            {
        //                //added code  for post code
        //                divErrorMsg.Style.Add("display", "block");
        //                divErrorMsg.Attributes.Add("class", "reqdottedline");
        //                spnErrorMsg.Style.Add("white-space", "normal");
        //                spnErrorMsg.Style.Add("color", "rgb(255, 255, 255)");
        //                spnErrorMsg.Style.Add("font-size", "11px");
        //                spnErrorMsg.InnerText = ReaseanDesc;
        //                divSelectAddressMsg.Style.Add("display", "none");
        //                divPostCode_Address.Style.Add("display", "none");
        //                divSelectAddressMsg.Style.Add("display", "none");
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }

        //}
        /// <summary>
        /// This dropdown selectedIndexChanged event will fire when user select address from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void ddlAddressPostCodes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "SetaddressVerifyVal('1');", true);

        //        divPostCode_Address.Style.Add("display", "none");
        //        divSelectAddressMsg.Style.Add("display", "none");


        //        divPostCodes.Style.Add("display", "none");
        //        divAddressFields.Style.Add("display", "block");
        //        divBusinessLocator.Style.Add("display", "none");
        //        FillAddressFields(ddlAddressPostCodes.SelectedValue);
        //        hdnSelectedAddressValue.Value = "";

        //    }
        //    catch (Exception ex)
        //    {
        //        SFMobileLog.Error(ex);
        //    }
        //}
        /// <summary>
        /// This Event will fire when user want to save the recipient details in to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void continue_bottom_Click(object sender, EventArgs e)
        {


            int result = 0;
            try
            {
                CheckoutLogic objCheckOutLogic = new CheckoutLogic();
                CustomerInfo objCustomerInfo = new CustomerInfo();
                objCustomerInfo.CountryCode = txtCountry_field.Text;
                objCustomerInfo.County = txtCounty_field.Text;
                if (chkBxDelivery_instruction_check.Checked)
                    //objCustomerInfo.DeliveryIns = txtDelivery_instructions.InnerText;
                    objCustomerInfo.DeliveryIns = drpDelivery_Instructions.SelectedValue;
                objCustomerInfo.District = txtDistrict_field.Text;
                if (chkBxCard_message_check.Checked)
                    objCustomerInfo.GiftMessage = txtCard_message.InnerText;
                objCustomerInfo.HouseNo = txtAddressLine1.Text;
                objCustomerInfo.NoCardMessage = chkBxCard_message_check.Checked ? 1 : 0;
                objCustomerInfo.Organisation = txtOrganisation_field.Text;
                objCustomerInfo.PostCode = txtPostCode.Text;
                objCustomerInfo.ReceipentTelPhNo = txtContact_field.Text;
                objCustomerInfo.Street = txtStreet_field.Text;
                objCustomerInfo.Town = txtTownCity.Text;
                objCustomerInfo.OrderID = orderId;
                if (ViewState["addr_verified"] != null)
                {
                    objCustomerInfo.Addr_Verified = 1;
                }
                else
                {
                    objCustomerInfo.Addr_Verified = 0;
                }
                string name = txtFirstName.Text;
                string firstName = string.Empty;
                string lastName = string.Empty;


                if (name.Contains(' '))
                {
                    firstName = name.Substring(0, name.IndexOf(' '));
                    lastName = name.Substring(name.IndexOf(' ') + 1);
                }
                else
                    firstName = name;
                objCustomerInfo.Name = firstName;
                objCustomerInfo.LastName = lastName;

                result = objCheckOutLogic.InsertUpdateRecipientDetails(objCustomerInfo);
                #region Edit Dispatch Address for New OrderSchema
                objCustomerInfo.CountryID = 215;
                objOrderSchemaRecipient.EditDispatchAddress(objCustomerInfo);
                objOrderSchemaRecipient.EditDeliveryInstructions(objCustomerInfo);
                objOrderSchemaRecipient.EditCardMessage(objCustomerInfo);
                // Updating Occasion ID and funeral time.
                objOrderSchemaRecipient.UpdateOccasionAndFuneralTime(orderId);
                #endregion
                GenerateaAndStoreOrderXml(objCustomerInfo.OrderID);

                if (addressVerify.Value == "0" && IsCustomerAddressSameAsAPI() == false)
                {
                    FillSuggestedAddress();
                    ScriptManager.RegisterStartupScript(continue_bottom, continue_bottom.GetType(), "HideModelPopUp", "hideModal('modalPage');", true);
                    ScriptManager.RegisterClientScriptBlock(continue_bottom, continue_bottom.GetType(), "postForm", "popupSuggestedAddress();", true);
                    //Response.Redirect("~/Checkout/m_CheckoutCancel.aspx?s=" + Request.QueryString["s"] + "&t=" + Request.QueryString["t"], false);


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(continue_bottom, continue_bottom.GetType(), "postForm", "encrypt();", true);
                    //Response.Redirect("~/Checkout/m_CheckoutCancel.aspx?s=" + Request.QueryString["s"] + "&t=" + Request.QueryString["t"], false);
                }

                //Post the Form to Payment page
                //ScriptManager.RegisterClientScriptBlock(continue_bottom, continue_bottom.GetType(), "postForm", "encrypt();", true);
                //GetConfigXML();
                //continue_bottom.PostBackUrl = "~/Checkout/m_step2.aspx?s=" + decryOrderId;
                // string filename = hdnfilename.Value.Replace("config_", "").Replace(".xml", "");

                // Response.Redirect("~/Checkout/m_step2.aspx?ProdId=" + Request.QueryString["ProdId"] + "&s=" + Request.QueryString["s"] + "&t=" + filename, false);


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


        }
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

                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);
                        SameAsDeliveryAddress(orderId);

                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "SetaddressVerifyVal('0');", true);

                        //divPostCodes.Style.Add("display", "none");
                        //divBusinessLocator.Style.Add("display", "none");
                        //DivErrorMsgFindPostcode.Style.Add("display", "none");
                        //div1.Style.Add("display", "none");
                        //div2.Style.Add("display", "none");
                        //div3.Style.Add("display", "block");
                        //UpdatePanel3.Update();
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx", false);
                    }

                }
                else
                {

                    txtFirstName.Text = "";
                    txtOrganisation_field.Text = "";
                    txtAddressLine1.Text = "";
                    txtStreet_field.Text = "";
                    txtDistrict_field.Text = "";
                    txtTownCity.Text = "";
                    txtPostCode.Text = "";
                    txtCounty_field.Text = "";
                    txtContact_field.Text = "";
                    //txtDelivery_instructions.InnerText = "";
                    drpDelivery_Instructions.SelectedValue = "Please select an option";
                    txtCard_message.InnerText = "";
                    //DivAddressResult.Style.Add("display", "none");
                    //DivAddressResultMsg.Style.Add("display", "none");
                    //DivErrorMsgFindPostcode.Style.Add("display", "none");
                    //div1.Style.Add("display", "block");
                    //div2.Style.Add("display", "none");
                    //div3.Style.Add("display", "none");
                    //if (SerenataflowersSessions.CountryName != "United Kingdom")
                    //{
                    //    div3.Style.Add("display", "block");
                    //    div1.Style.Add("display", "none");
                    //}
                    //UpdatePanel3.Update();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        #endregion

        #region Page methods
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

        private void SameAsDeliveryAddress(string orderId)
        {
            objCheckout = new CheckoutLogic();
            CustomerInfo objBillingInfo = new CustomerInfo();
            try
            {
                objBillingInfo = objCheckout.RetainedBillingDetails(orderId);
                if (objBillingInfo != null)
                {
                    //txtFirstName.Text = objBillingInfo.Name + objBillingInfo.LastName;
                    txtOrganisation_field.Text = objBillingInfo.Organisation;
                    txtAddressLine1.Text = objBillingInfo.HouseNo;
                    txtStreet_field.Text = objBillingInfo.Street;
                    txtDistrict_field.Text = objBillingInfo.District;
                    txtTownCity.Text = objBillingInfo.Town;
                    txtPostCode.Text = objBillingInfo.PostCode;
                    txtCounty_field.Text = objBillingInfo.County;
                    txtContact_field.Text = objBillingInfo.ReceipentTelPhNo;
                    //txtDelivery_instructions.InnerText = objBillingInfo.DeliveryIns;
                    drpDelivery_Instructions.SelectedValue = objBillingInfo.DeliveryIns;

                    txtCard_message.InnerText = objBillingInfo.GiftMessage;

                    //organisation_field.Text = objBillingInfo.Organisation;
                    //txtAddressline1.Text = objBillingInfo.HouseNo;
                    //street_field.Text = objBillingInfo.Street;
                    //district_field.Text = objBillingInfo.District;
                    //txtCitytown.Text = objBillingInfo.Town;
                    //txtPostcodefield.Text = objBillingInfo.PostCode;
                    //if (objBillingInfo.Country != "")
                    //{
                    //    invCountry.SelectedIndex = -1;
                    //    invCountry.Items.FindByText(objBillingInfo.Country).Selected = true;

                    //}
                }
                // sms_field.Checked = true;
                //promo_email_field.Checked = true;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Bind address field values based on Selected Address id
        /// </summary>
        /// <param name="selectedValue"></param>
        public void FillAddressFields(string selectedValue)
        {
            string errorMsg = string.Empty;
            bool flag;
            objSearchAddress = new CommonFunctions();
            try
            {
                //DataSet doc = GetAddressFieldsBasedOnAddressID(selectedValue);
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
                            txtOrganisation_field.Text = orgName;
                            txtAddressLine1.Text = house;
                            txtStreet_field.Text = street;
                            txtDistrict_field.Text = district;
                            txtTownCity.Text = city;
                            txtCounty_field.Text = county;
                            txtPostCode.Text = postcode;
                            txtCountry_field.Text = country;
                            txtCountry_field.ReadOnly = true;
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
        /// Check Postcode dataset is Valid or Not
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
                //flag = CheckColumnsExistsInDataSet(dsResult);
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
                    divErrorMsg.Style.Add("display", "block");
                    spnErrorMsg.InnerText = errorMsg;
                    // divSelectAddressMsg.Style.Add("display", "none");
                }
                else
                {
                    divErrorMsg.Style.Add("display", "none");
                    spnErrorMsg.InnerText = "";
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
        /// This method will get the encrypted OrderId from xmlHttpRequest Response.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        [WebMethod]
        public static string getEncryptedOrderId(string httpResponse)
        {
            string configXmlFileName = string.Empty;
            try
            {
                string encryptedValue = httpResponse.Replace("\r\n", "");

                //Read the Config.xml file from local disk
                XmlDocument xmlConfig = new XmlDocument(); ;
                string xmlfilePath = ConfigurationManager.AppSettings["ConfigXMLPath"];
                string domainName = GetDomainName();//added              
                xmlConfig.Load(xmlfilePath);
                string xmlConfigFile = xmlConfig.InnerXml.Replace("@OrderID@", encryptedValue);
                xmlConfigFile = xmlConfigFile.Replace("@DomainName", domainName);
                string filePath = ConfigurationManager.AppSettings["encryptionPath"];
                configXmlFileName = GenerateFileName("config");

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
        /// <summary>
        /// This method will get response from xmlhttpRequest and stored the the encrypted Order.xml value in to session
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        //[WebMethod(EnableSession = true)]
        //public static string getEncryptedOrder(string httpResponse)
        //{
        //   string encryptedOrderxml = string.Empty;
        //   try
        //   {
        //      string encryptedValue = httpResponse.Replace("\r\n", "");
        //      System.Web.HttpContext.Current.Session["orderXml"] = encryptedValue;
        //      encryptedOrderxml = encryptedValue;
        //   }
        //   catch (Exception ex)
        //   {
        //      SFMobileLog.Error(ex);
        //   }
        //   return encryptedOrderxml;
        //}
        /// <summary>
        /// This method will get response from xmlhttpRequest and stored the the encrypted Config.xml value in to session
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        [WebMethod]
        public static string getEncryptedConfig(string httpResponse)
        {
            string encryptedConfigxml = string.Empty;
            try
            {
                string encryptedValue = httpResponse.Replace("\r\n", "");
                //System.Web.HttpContext.Current.Session["configXml"] = encryptedValue;
                SerenataflowersSessions.ConfigXML = encryptedValue;
                encryptedConfigxml = encryptedValue;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return encryptedConfigxml;
        }
        /// <summary>
        /// This method will generate Unique File name
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
            CustomerInfo objCustomerInfo = new CustomerInfo();
            try
            {
                objCustomerInfo = objCheckOutLogic.GetBillingDetails(orderId);
                if (objCustomerInfo != null)
                {
                    txtFirstName.Text = objCustomerInfo.Name + objCustomerInfo.LastName;
                    txtOrganisation_field.Text = objCustomerInfo.Organisation;
                    txtAddressLine1.Text = objCustomerInfo.HouseNo;
                    txtStreet_field.Text = objCustomerInfo.Street;
                    txtDistrict_field.Text = objCustomerInfo.District;
                    txtTownCity.Text = objCustomerInfo.Town;
                    txtPostCode.Text = objCustomerInfo.PostCode;
                    txtCounty_field.Text = objCustomerInfo.County;
                    txtContact_field.Text = objCustomerInfo.ReceipentTelPhNo;
                    //txtDelivery_instructions.InnerText = objCustomerInfo.DeliveryIns;
                    drpDelivery_Instructions.SelectedValue = objCustomerInfo.DeliveryIns;

                    txtCard_message.InnerText = objCustomerInfo.GiftMessage;
                    if (objCustomerInfo.HouseNo != null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Show", "DisplayAddressFields();", true);
                        //divPrefer.Style.Add("display", "block");

                    }

                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }


        }


        [WebMethod]
        public static bool CheckValidPostcode(string postCode, string decOrderID)
        {
            bool IsValid = false;
            try
            {
                DataSet dsPostcode = null;
                int validPc = 0;
                // code added for valid postcode
                CheckoutLogic objCheckoutLogic = new CheckoutLogic();
                if (decOrderID.Length > 0)
                {
                    dsPostcode = objCheckoutLogic.PostcodeValid(postCode, decOrderID);
                    if (dsPostcode != null)
                    {
                        validPc = Convert.ToInt32(dsPostcode.Tables[0].Rows[0]["ValidPC"]);
                        IsValid = validPc == 1 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return IsValid;
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
                hmpageName.Content = "Checkout:Step2";

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
    }
}