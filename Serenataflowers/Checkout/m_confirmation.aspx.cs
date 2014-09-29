using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using SFMobile.Exceptions;
using Serenata_Checkout.Logic;
using Serenata_Checkout.Bal.Confirmation;
using System.Data;
using Serenata_Checkout.ChilkatComponent;
using System.Globalization;
using System.Configuration;
using Serenata_Checkout.PaymentGateway;
using System.Web.UI.HtmlControls;

namespace Serenataflowers.Checkout
{
    public partial class m_confirmation : System.Web.UI.Page
    {
        double Total = 0.00;
        RecipientDetailsBAL objRecipientDetails;
        CustomerDetailsBAL objCustomerDetails;
        string OrderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((!IsPostBack))
                {

                    string orderId = string.Empty;
                    if (Request.QueryString["s"] != null)
                    {
                        string strOrderId = Common.GetOrderIdFromQueryString();
                        OrderID = strOrderId;
                        lblOrderId.Text = strOrderId;
                        //BindRecipPopUp(strOrderId);
                        //BindCustPopUp(strOrderId);
                        BindBasket(strOrderId);
                        BindConfirmationSection(strOrderId);
                        DisplayOrderConfirmationDetails(strOrderId);
                        ChaseStatus.Text = "";
                        UpdateOrderComplete(strOrderId);
                        DeleteOrderIdCookies();
                        Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "Confirmation");
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        //private void DeleteOrderIdCookies()
        //{
        //    Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetNoStore();
        //    Common.RemoveCookie("Order_Id", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);
        //}

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
                Common.RemoveCookie("selectedDelIns", System.Web.HttpContext.Current.Response, System.Web.HttpContext.Current.Request);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private void UpdateOrderComplete(string strOrderId)
        {
            ConfirmationBAL objConfirmBal = new ConfirmationBAL();
            objConfirmBal.UpdateOrderComplete(strOrderId);
            objConfirmBal.UpdateProductPaymentStatus(strOrderId);
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
                if (!string.IsNullOrEmpty(txtRecName.Text))
                {
                    string name = txtRecName.Text;
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

                //objRecDtl.Organisation = recpientOrganization.Text;
                objRecDtl.HouseNo = txtAddress1.Text;
                objRecDtl.Street = txtAddress2.Text;
                objRecDtl.District = txtAddress3.Text;
                objRecDtl.Town = txtCity.Text;
                objRecDtl.PostCode = txtPostCode.Text;
                objRecDtl.CountryID = Convert.ToInt32(drpRecCountry.SelectedItem.Value);
                objRecipientDetails = new RecipientDetailsBAL();
                if (CheckDBPostCode(OrderID, UseThisAddress) == true)
                {
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);
                    //divRecfinal.Style.Add("display", "block");
                    //divRecfinalEditor.Style.Add("display", "block");
                    //divContinuePayment.Style.Add("display", "block");
                    //ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                    ScriptManager.RegisterStartupScript(UseThisAddress, UseThisAddress.GetType(), "alertHi", "Closesuggesteddelivery();", true);
                    //Response.Redirect("m_confirmation.aspx?s=" + Request.QueryString["s"], false);
                    RefreshParentRecDeliverySection(OrderID);
                    UpdatePanel2.Update();
                    UpdatePanel1.Update();
                }
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
                            //recpientOrganization.Text = orgName;
                            txtAddress1.Text = house;
                            txtAddress2.Text = street;
                            txtAddress3.Text = district;
                            txtCity.Text = city;
                            txtPostCode.Text = postcode;
                            //GetAllCountries();
                            drpRecCountry.SelectedIndex = drpRecCountry.Items.IndexOf(drpRecCountry.Items.FindByValue(country));
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

        /// <summary>
        /// Yes Button of Not Found Address Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                //Response.Redirect("m_confirmation.aspx?s=" + Request.QueryString["s"], false);
                UpdateRecipient();
                UpdatePanel2.Update();
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
                    // string eventArguments = Convert.ToString(Request["__EVENTARGUMENT"]);
                    //string[] delOptionsArray = eventArguments.Split('@');
                    //objDispatches.DelOptionID = Convert.ToInt32(delOptionsArray[1].TrimEnd(','));
                    //objDispatches.DeliveryDate = DateTime.ParseExact(delOptionsArray[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objDispatches.DelOptionID = Convert.ToInt32(rbtnLstsuggestedDeliveryOptions.SelectedValue);
                    objDispatches.DeliveryDate = DateTime.ParseExact(ddlSugestedDeliveryDate.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DispatchesInfo tmpDispatches = new DispatchesInfo();
                    tmpDispatches = objDelivery.GetDeliveryDetailsByDelOptionID(objDispatches.DelOptionID);
                    objDispatches.CarrierID = tmpDispatches.CarrierID;
                    objDispatches.DeliveryTime = tmpDispatches.DeliveryTime;
                    objDispatches.DeliveryPrice = tmpDispatches.DeliveryPrice;
                    objDelivery.ScheduleDispatch(objDispatches);
                    updSuggestetdate.Update();




                }

                ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                //Response.Redirect("confirmation.aspx?s=" + Request.QueryString["s"], false);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        protected void CancelOrder_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(CancelOrder, CancelOrder.GetType(), "afsdflertHi", "Closesuggesteddelivery();", false);
            Response.Redirect(ConfigurationManager.AppSettings["RootURL"], false);
        }
        #endregion

        /// <summary>
        /// Binding the Confirmation Section
        /// </summary>
        /// <param name="strOrderId"></param>
        private void BindConfirmationSection(string strOrderId)
        {
            ConfirmationBAL objConfirmBal = new ConfirmationBAL();
            ConfirmDetailsInfo objConfirmInfo = null;
            objConfirmInfo = objConfirmBal.GetConfirmationPageDetails(strOrderId);
            txtMessage.Value = objConfirmInfo.Message;
            lblGiftMessage.Text = objConfirmInfo.Message;
            lblDeliveryInstruction.Text = objConfirmInfo.DeliveryInstructions;
            if (objConfirmInfo.DeliveryInstructions.Contains("Leave with neighbour"))
            {
                divHouseNo.Style["display"] = "block";
                drpDelIns.SelectedValue = "Leave with neighbour";
                txthouseNumber.Text = objConfirmInfo.DeliveryInstructions.Replace("Leave with neighbour", "").ToString();
            }
            else
                drpDelIns.SelectedValue = objConfirmInfo.DeliveryInstructions;
        }
        /// <summary>
        /// Binding the Customer Address PopUP
        /// </summary>
        /// <param name="strOrderId"></param>
        private void BindCustPopUp(string strOrderId)
        {
            objCustomerDetails = new CustomerDetailsBAL();
            CustomerInfo objCustomerInfo = new CustomerInfo();
            try
            {

                objCustomerInfo = objCustomerDetails.GetBillingDetails(strOrderId);
                if (objCustomerInfo != null)
                {
                    if (!string.IsNullOrEmpty(objCustomerInfo.FirstName))
                    {
                        txtCustName.Text = objCustomerInfo.FirstName + " " + objCustomerInfo.LastName;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.HouseNo))
                    {
                        TxtCustAddr1.Text = objCustomerInfo.HouseNo;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Street))
                    {
                        TxtCustAddr2.Text = objCustomerInfo.Street;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.District))
                    {
                        TxtCustAddr3.Text = objCustomerInfo.District;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Town))
                    {
                        TxtCustTown.Text = objCustomerInfo.Town;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.PostCode))
                    {
                        TxtCustPostCode.Text = objCustomerInfo.PostCode;
                    }
                    Common.FillAllCountries(ddlCustCountry);
                    ddlCustCountry.SelectedValue = objCustomerInfo.CountryID.ToString();
                    if (!string.IsNullOrEmpty(objCustomerInfo.UKMobile))
                    {
                        TxtCustMobile.Text = objCustomerInfo.UKMobile;
                    }
                }


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Message PopUp
        /// </summary>
        private void BindMessagePopUp(string strOdrerID)
        {
            objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                objRecInfo = objRecipientDetails.GetDeliveryDetails(strOdrerID);
                txtMessage.Value = objRecInfo.Message;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Binding the recipient popup
        /// </summary>
        private void BindRecipPopUp(string strOdrerID)
        {
            objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {

                objRecInfo = objRecipientDetails.GetDeliveryDetails(strOdrerID);
                if (objRecInfo != null)
                {
                    if (!string.IsNullOrEmpty(objRecInfo.FirstName))
                    {
                        txtRecName.Text = objRecInfo.FirstName + " " + objRecInfo.LastName;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.HouseNo))
                    {
                        txtAddress1.Text = objRecInfo.HouseNo;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Street))
                    {
                        txtAddress2.Text = objRecInfo.Street;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.District))
                    {
                        txtAddress3.Text = objRecInfo.District;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Town))
                    {
                        txtCity.Text = objRecInfo.Town;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.PostCode))
                    {
                        txtPostCode.Text = objRecInfo.PostCode;
                    }
                    Common.FillAllCountries(drpRecCountry);
                    drpRecCountry.SelectedValue = objRecInfo.CountryID.ToString();
                    //if (!string.IsNullOrEmpty(objRecInfo.RecipientMobile))
                    //{
                    //    txtRecPhoneNumber.Text = objRecInfo.RecipientMobile;
                    //}
                }


            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// Bind basket contents in repeater control
        /// </summary>
        /// <param name="orderId"></param>
        private void BindBasket(string orderId)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            try
            {
                lstProductItems = objOrderdetails.GetBasketContents(orderId);
                rptViewBasket.DataSource = lstProductItems;
                rptViewBasket.DataBind();
                BindDeliveryDetails(orderId);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// Display Delivery details.
        /// </summary>
        /// <param name="orderId"></param>
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

                    //if (objDeliveryTimeInfo.OptionName != null)
                    //{
                    //    //if (objDeliveryTimeInfo.OptionName.ToLower().Trim() == "standard")
                    //    //    hLinkDelivery.Visible = true;
                    //    //else
                    //    //    hLinkDelivery.Visible = false;

                    //}
                    //else
                    //    hLinkDelivery.Visible = false;

                    if (objDeliveryTimeInfo.deliveryPrice == 0.00)
                        lblDeliveryPrice.Text = "FREE";
                    else
                        lblDeliveryPrice.Text = String.Format("£{0}", objDeliveryTimeInfo.deliveryPrice);

                    //lblDeldate.Text = objDeliveryTimeInfo.Deliverydate;


                    lblTotal.Text = String.Format("£{0}", objDeliveryTimeInfo.OrderTotal);
                    //lblTotalWithVat.Text = String.Format("£{0}", objDeliveryTimeInfo.OrderTotal);

                    if (objDeliveryTimeInfo.voucherTitle != null)
                    {
                        if (objDeliveryTimeInfo.voucherTitle.Trim().Length > 0)
                        {
                            //trPromoVoucher.Style.Add("display", "none");
                            //trDiscount.Style.Add("display", "block");

                            hlinkVoucherTitle.Text = objDeliveryTimeInfo.voucherTitle;
                            lblDiscountPrice.Text = string.Format("-£{0}", objDeliveryTimeInfo.discount);
                        }
                        else
                        {
                            trDiscount.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        trDiscount.Style.Add("display", "none");
                    }
                }


            }

            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void SaveDates_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
        protected void Voucher_Click(object sender, EventArgs e)
        {
            ModifyBasket.Update();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            DisplayBasketCount.Update();
        }
        /// <summary>
        /// Calculation of Total Column in the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptViewBasket_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label Qty = e.Item.FindControl("lblQty") as Label;
                Label Price = e.Item.FindControl("lblPrice") as Label;
                if (Qty != null)
                    Qty.Text = Qty.Text + " x";
                //if (Price != null)
                //{
                //    Total += Convert.ToDouble(Price.Text.Remove(0, 1)) * Convert.ToInt32(Qty.Text.Replace("X", ""));
                //}
            }
            //if (e.Item.ItemType == ListItemType.Footer)
            //{

                //Label lblDeliveryType = e.Item.FindControl("lblDeliveryType") as Label;
                //Label lblDeliveryPrice = e.Item.FindControl("lblDeliveryPrice") as Label;
                //var DeliveryInfo = new string[2];
                //DeliveryInfo = BindDeliveryTimeInfo(OrderID);
                //if (lblDeliveryType != null)
                //    lblDeliveryType.Text = DeliveryInfo[0];
                //if (lblDeliveryPrice != null)
                //{
                //    lblDeliveryPrice.Text = DeliveryInfo[1];
                //    if (DeliveryInfo[1].ToLower() != "free")
                //        Total += Convert.ToDouble(DeliveryInfo[1].Remove(0, 1));
                //}


                //OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
                //DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
                //Common objCommon = new Common();

                //objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(Common.GetOrderIdFromQueryString());

                //if (objDeliveryTimeInfo != null)
                //{
                //    //lblDeliveryType.Text = objDeliveryTimeInfo.OptionName;

                    //if (objDeliveryTimeInfo.OptionName != null)
                    //{
                    //    if (objDeliveryTimeInfo.OptionName.ToLower().Trim() == "standard")
                    //        hLinkDelivery.Visible = true;
                    //    else
                    //        hLinkDelivery.Visible = false;

                    //}
                    //else
                    //    hLinkDelivery.Visible = false;

                    //if (objDeliveryTimeInfo.deliveryPrice == 0.00)
                    //    lblDeliveryPrice.Text = "FREE";
                    //else
                    //    lblDeliveryPrice.Text = String.Format("£{0}", objDeliveryTimeInfo.deliveryPrice);

                    //lblDeldate.Text = objDeliveryTimeInfo.Deliverydate;


                    //Label lTotal = e.Item.FindControl("lblTotal") as Label;
                    //if (lTotal != null)
                    //    lTotal.Text = "£" + Total.ToString();

                    ////lblTotal.Text = String.Format("£{0}", objDeliveryTimeInfo.TotalExVat);
                    ////lblTotalWithVat.Text = String.Format("£{0}", objDeliveryTimeInfo.OrderTotal);
                    //lTotal.Text = String.Format("£{0}", objDeliveryTimeInfo.OrderTotal);

                    //if (objDeliveryTimeInfo.voucherTitle.Trim().Length > 0)
                    //{
                    //    //trPromoVoucher.Style.Add("display", "none");
                    //    TableRow trDiscount = e.Item.FindControl("trDiscount") as TableRow;
                    //    trDiscount.Style.Add("display", "block");
                    //    trDiscount.Visible = true;

                    //    Label hlinkVoucherTitle = e.Item.FindControl("hlinkVoucherTitle") as Label;
                    //    hlinkVoucherTitle.Text = objDeliveryTimeInfo.voucherTitle;

                    //    Label lblDiscountPrice = e.Item.FindControl("lblDiscountPrice") as Label;
                    //    lblDiscountPrice.Text = string.Format("-£{0}", objDeliveryTimeInfo.discount);
                    //}
                //}










                //Label lTotal = e.Item.FindControl("lblTotal") as Label;
                //if (lTotal != null)
                //    lTotal.Text = "£" + Total.ToString();
            //}
        }
        /// <summary>
        /// Populate the Confimation Section
        /// </summary>
        /// <param name="strOrderId"></param>
        private void DisplayOrderConfirmationDetails(string strOrderId)
        {
            try
            {
                ConfirmationBAL objConfirmBal = new ConfirmationBAL();
                ConfirmDetailsInfo objConfirmInfo = null;
                objConfirmInfo = objConfirmBal.GetConfirmationPageDetails(strOrderId);
                if (objConfirmInfo != null)
                {
                    lblGiftMessage.Text = objConfirmInfo.Message;
                    // lblPayment.Text = objConfirmInfo.Payment;
                    //lblOrderDate.Text = string.Format("{0:dd MMMM yyyy}", objConfirmInfo.OrderDate);
                    lblDeliveryDate.Text = string.Format("{0:dd MMMM yyyy}", objConfirmInfo.Deliverydate);
                    string strDelInstruction = objConfirmInfo.DeliveryInstructions;
                    lblDeliveryInstruction.Text = strDelInstruction;
                    if (!string.IsNullOrEmpty(strDelInstruction))
                        strDelInstruction = strDelInstruction.Trim();
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        /// <summary>
        /// Update the Recipient's Address Section
        /// </summary>
        /// <param name="strOrderId"></param>
        private void RefreshParentRecDeliverySection(string strOrderId)
        {
            try
            {
                RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
                RecipientInfo objRecInfo = new RecipientInfo();

                Label lblName = (Label)ucDeliveryDetails.FindControl("lblName");
                Label lblOrganization = (Label)ucDeliveryDetails.FindControl("lblOrganization");
                Label lblHouseNumber = (Label)ucDeliveryDetails.FindControl("lblHouseNumber");
                Label lblStreet = (Label)ucDeliveryDetails.FindControl("lblStreet");
                Label lblDistrict = (Label)ucDeliveryDetails.FindControl("lblDistrict");
                Label lblPostCode = (Label)ucDeliveryDetails.FindControl("lblPostCode");
                Label lblTown = (Label)ucDeliveryDetails.FindControl("lblTown");
                Label lblCountry = (Label)ucDeliveryDetails.FindControl("lblCountry");
                Label lblMobile = (Label)ucDeliveryDetails.FindControl("lblMobile");

                int iCountSpaces = 0;
                objRecInfo = objRecipientDetails.GetDeliveryDetails(strOrderId);
                if (objRecInfo != null)
                {
                    if (!string.IsNullOrEmpty(objRecInfo.FirstName))
                    {
                        lblName.Visible = true;
                        lblName.Text = objRecInfo.FirstName + " " + objRecInfo.LastName + "<br />";
                        txtRecName.Text = objRecInfo.FirstName + " " + objRecInfo.LastName;
                    }
                    else
                    {
                        lblName.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Organisation))
                    {
                        lblOrganization.Visible = true;
                        lblOrganization.Text = objRecInfo.Organisation + "<br />";
                    }
                    else
                    {
                        lblOrganization.Visible = false;
                        iCountSpaces++;
                    }

                    if (!string.IsNullOrEmpty(objRecInfo.HouseNo))
                    {
                        lblHouseNumber.Visible = true;
                        lblHouseNumber.Text = objRecInfo.HouseNo + "<br />";
                        txtAddress1.Text = objRecInfo.HouseNo;
                    }
                    else
                    {
                        lblHouseNumber.Visible = false;
                        iCountSpaces++;
                    }

                    if (!string.IsNullOrEmpty(objRecInfo.Street))
                    {
                        lblStreet.Visible = true;
                        lblStreet.Text = objRecInfo.Street + "<br />";
                        txtAddress2.Text = objRecInfo.Street;
                    }
                    else
                    {
                        lblStreet.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.District))
                    {
                        lblDistrict.Visible = true;
                        lblDistrict.Text = objRecInfo.District + "<br />";
                        txtAddress3.Text = objRecInfo.District;
                    }
                    else
                    {
                        lblDistrict.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.Town))
                    {
                        lblTown.Visible = true;
                        lblTown.Text = objRecInfo.Town + "<br />";
                        txtCity.Text = objRecInfo.Town;
                    }
                    else
                    {
                        lblTown.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.PostCode))
                    {
                        lblPostCode.Visible = true;
                        lblPostCode.Text = objRecInfo.PostCode + "<br />";
                        txtPostCode.Text = objRecInfo.PostCode;
                    }
                    else
                    {
                        lblPostCode.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.CountryName))
                    {
                        lblCountry.Visible = true;
                        lblCountry.Text = objRecInfo.CountryName + "<br />";
                    }
                    else
                    {
                        lblCountry.Visible = false;
                        iCountSpaces++;
                    }
                    if (lblCountry.Visible == false)
                    {
                        lblCountry.Visible = true;
                        lblCountry.Text = "";
                    }
                    if (!string.IsNullOrEmpty(objRecInfo.RecipientMobile))
                    {
                        lblMobile.Visible = true;
                        lblMobile.Text = objRecInfo.RecipientMobile + "<br />";
                    }
                    else
                    {
                        iCountSpaces++;
                        lblMobile.Visible = false;
                    }
                    if (lblMobile.Visible == false)
                    {
                        lblMobile.Visible = true;
                        lblMobile.Text = "";
                    }
                    for (int i = 0; i < iCountSpaces; i++)
                    {
                        lblMobile.Text += "<br />";
                    }
                }
                UpdatePanel uPanelDelivery = (UpdatePanel)ucDeliveryDetails.FindControl("updDeliveryInfoPanel");
                uPanelDelivery.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Update the Customer Address Section
        /// </summary>
        /// <param name="strOrderId"></param>
        private void RefreshParentCustDeliverySection(string strOrderId)
        {
            try
            {
                CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                CustomerInfo objCustInfo = new CustomerInfo();

                Label lblName = (Label)ucCustomerDetails.FindControl("lblName");
                // Label lblOrganization = (Label)ucDeliveryDetails.FindControl("lblOrganization");
                Label lblHouseNumber = (Label)ucCustomerDetails.FindControl("lblHouseNumber");
                Label lblStreet = (Label)ucCustomerDetails.FindControl("lblStreet");
                Label lblDistrict = (Label)ucCustomerDetails.FindControl("lblDistrict");
                Label lblPostCode = (Label)ucCustomerDetails.FindControl("lblPostCode");
                Label lblTown = (Label)ucCustomerDetails.FindControl("lblTown");
                Label lblCountry = (Label)ucCustomerDetails.FindControl("lblCountry");
                Label lblMobile = (Label)ucCustomerDetails.FindControl("lblMobile");

                int iCountSpaces = 0;
                objCustInfo = objCustomerDetails.GetBillingDetails(strOrderId);

                if (objCustInfo != null)
                {
                    if (!string.IsNullOrEmpty(objCustInfo.FirstName))
                    {
                        lblName.Visible = true;
                        lblName.Text = objCustInfo.FirstName + " " + objCustInfo.LastName + "<br />";
                    }
                    else
                    {
                        lblName.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.HouseNo))
                    {
                        lblHouseNumber.Visible = true;
                        lblHouseNumber.Text = objCustInfo.HouseNo + "<br />";
                    }
                    else
                    {
                        lblHouseNumber.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.Street))
                    {
                        lblStreet.Visible = true;
                        lblStreet.Text = objCustInfo.Street + "<br />";
                    }
                    else
                    {
                        lblStreet.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.District))
                    {
                        lblDistrict.Visible = true;
                        lblDistrict.Text = objCustInfo.District + "<br />";
                    }
                    else
                    {
                        lblDistrict.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.Town))
                    {
                        lblTown.Visible = true;
                        lblTown.Text = objCustInfo.Town + "<br />";
                    }
                    else
                    {
                        lblTown.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.PostCode))
                    {
                        lblPostCode.Visible = true;
                        lblPostCode.Text = objCustInfo.PostCode + "<br />";
                    }
                    else
                    {
                        lblPostCode.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.Country))
                    {
                        lblCountry.Visible = true;
                        lblCountry.Text = objCustInfo.Country + "<br />";
                    }
                    else
                    {
                        lblCountry.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustInfo.UKMobile))
                    {
                        lblMobile.Visible = true;
                        lblMobile.Text = objCustInfo.UKMobile + "<br />";
                    }
                    else
                    {
                        lblMobile.Visible = false;
                        iCountSpaces++;
                    }
                    if (lblMobile.Visible == false)
                    {
                        lblMobile.Visible = true;
                        lblMobile.Text = "";
                    }
                    for (int i = 0; i < iCountSpaces; i++)
                    {
                        lblMobile.Text += "<br />";
                    }
                }
                UpdatePanel uPanelDelivery = (UpdatePanel)ucCustomerDetails.FindControl("CustInfoUpdatePanel");
                uPanelDelivery.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Button Click to Update the Recipient Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSaveRecManuallyAddress_Click(object sender, EventArgs e)
        {
            //RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            DataSet dsPostCodeDesc = new DataSet();
            RecipientInfo objRecInfo = new RecipientInfo();
            string name = txtRecName.Text.Trim();
            string firstName = string.Empty;
            string lastName = string.Empty;
            try
            {
                RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
                Encryption objEncryption = new Encryption();
                OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                //if (drpDelIns.SelectedItem.Text != "Please select an option")
                //{
                //    string instruct = string.Empty;
                //    if (drpDelIns.SelectedItem.Text == "Leave with neighbour")
                //    {
                //        instruct = drpDelIns.SelectedItem.Text + " " + txthouseNumber.Text.Trim();
                //    }
                //    else
                //    {
                //        instruct = drpDelIns.SelectedItem.Text;
                //    }

                //    objRecipientDetails.EditDeliveryInstructions(OrderID, instruct);
                //}

                System.Text.StringBuilder custAddress = new System.Text.StringBuilder();
                if (!string.IsNullOrEmpty(txtPostCode.Text))
                {

                    if (!string.IsNullOrEmpty(txtRecName.Text))
                    {


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

                    objRecInfo.HouseNo = txtAddress1.Text;
                    objRecInfo.Street = txtAddress2.Text;
                    objRecInfo.District = txtAddress3.Text;
                    objRecInfo.Town = txtCity.Text;
                    objRecInfo.PostCode = txtPostCode.Text;
                    objRecInfo.AddressVerified = 0;
                    objRecInfo.CountryID = Convert.ToInt32(drpRecCountry.SelectedItem.Value);
                    //objRecipientDetails.EditDeliveryAddress(objRecInfo);

                    if (!string.IsNullOrEmpty(txtAddress1.Text))
                    {
                        custAddress.Append(txtAddress1.Text + ",");
                    }
                    if (!string.IsNullOrEmpty(txtAddress2.Text))
                    {
                        custAddress.Append(txtAddress2.Text + ",");
                    }
                    if (!string.IsNullOrEmpty(txtAddress3.Text))
                    {
                        custAddress.Append(txtAddress3.Text + ",");
                    }
                    if (!string.IsNullOrEmpty(txtCity.Text))
                    {
                        custAddress.Append(txtCity.Text + ",");
                    }
                    if (!string.IsNullOrEmpty(txtPostCode.Text))
                    {
                        custAddress.Append(txtPostCode.Text);
                    }

                }
                else
                {
                    objRecInfo = objRecipientDetails.GetDeliveryDetails(OrderID);
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

                if (objRecInfo.CountryID == 215)
                {
                    if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "1")
                    {

                        ScriptManager.RegisterClientScriptBlock(lnkSaveRecManuallyAddress, lnkSaveRecManuallyAddress.GetType(), "ajax123", "popupSuggestedAddress();", true);
                    }
                    else if (hdnAddressVerify.Value == "0" && hdnHasApiAddress.Value == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(lnkSaveRecManuallyAddress, lnkSaveRecManuallyAddress.GetType(), "ajax123", "addressnotfound();", true);

                    }
                    else
                    {
                        UpdateRecipient();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "closePopup();", true);
                        UpdatePanel2.Update();
                        //Response.Redirect("m_paymentDetails.aspx?s=" + Request.QueryString["s"], false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(lnkSaveRecManuallyAddress, lnkSaveRecManuallyAddress.GetType(), "alertHi", "Closesuggesteddelivery();", false);
                    UpdatePanel2.Update();
                    //Response.Redirect("m_paymentDetails.aspx?s=" + Request.QueryString["s"], false);
                }






            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private void UpdateRecipient()
        {
            objRecipientDetails = new RecipientDetailsBAL();
            string name = txtRecName.Text.Trim();
            string firstName = string.Empty;
            string lastName = string.Empty;
            string strOrderId = Common.GetOrderIdFromQueryString();
            RecipientInfo objRecDtl = new RecipientInfo();
            objRecDtl.OrderId = strOrderId;

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
            objRecDtl.HouseNo = txtAddress1.Text.Trim();
            objRecDtl.Street = txtAddress2.Text.Trim();
            objRecDtl.District = txtAddress3.Text.Trim();
            objRecDtl.Town = txtCity.Text.Trim();
            objRecDtl.PostCode = txtPostCode.Text.Trim();
            objRecDtl.CountryID = Convert.ToInt32(drpRecCountry.SelectedItem.Value);
            //RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            objRecipientDetails.EditDeliveryAddress(objRecDtl);
            RefreshParentRecDeliverySection(strOrderId);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "closePopup();", true);
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
                    if (!string.IsNullOrEmpty(txtRecName.Text))
                    {
                        string name = txtRecName.Text;
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

                    //objRecDtl.Organisation = recpientOrganization.Text;
                    objRecDtl.HouseNo = txtAddress1.Text;
                    objRecDtl.Street = txtAddress2.Text;
                    objRecDtl.District = txtAddress3.Text;
                    objRecDtl.Town = txtCity.Text;
                    objRecDtl.PostCode = txtPostCode.Text;
                    objRecDtl.CountryID = Convert.ToInt32(drpRecCountry.SelectedItem.Value);
                    objRecDtl.AddressVerified = 1;
                    hdnAddressVerify.Value = "1";
                    objRecipientDetails = new RecipientDetailsBAL();
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);

                    ScriptManager.RegisterStartupScript(UseTheAddressIEntered, UseTheAddressIEntered.GetType(), "alertHi", "Closesuggesteddelivery();", true);
                    //Response.Redirect("m_confirmation.aspx?s=" + Request.QueryString["s"], false);
                    RefreshParentRecDeliverySection(OrderID);
                    UpdatePanel1.Update();
                    UpdatePanel2.Update();
                }
                else if (CheckDBPostCode(OrderID, UseTheAddressIEntered) == true)
                {
                    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                    CustomerInfo objCustomerInfo = new CustomerInfo();
                    Encryption objEncryptions = new Encryption();
                    OrderID = objEncryptions.GetAesDecryptionString(Request.QueryString["s"]);
                    objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);
                    RecipientInfo objRecDtl = new RecipientInfo();
                    objRecDtl.OrderId = OrderID;
                    objRecDtl.FirstName = objCustomerInfo.FirstName;
                    objRecDtl.LastName = objCustomerInfo.LastName;
                    objRecDtl.Organisation = objCustomerInfo.Organisation;
                    objRecDtl.HouseNo = objCustomerInfo.HouseNo;
                    objRecDtl.Street = objCustomerInfo.Street;
                    objRecDtl.District = objCustomerInfo.District;
                    objRecDtl.Town = objCustomerInfo.Town;
                    objRecDtl.PostCode = objCustomerInfo.PostCode;
                    objRecDtl.CountryID = objCustomerInfo.CountryID;
                    objRecDtl.RecipientMobile = objCustomerInfo.UKMobile;
                    objRecipientDetails = new RecipientDetailsBAL();
                    objRecipientDetails.EditDeliveryAddress(objRecDtl);


                    //divRecfinal.Style.Add("display", "block");
                    //divRecfinalEditor.Style.Add("display", "block");
                    //divContinuePayment.Style.Add("display", "block");
                    //divrecInitial.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(ChangeDate, ChangeDate.GetType(), "alertHi", "Closesuggesteddelivery();", true);
                    //Response.Redirect("m_confirmation.aspx?s=" + Request.QueryString["s"], false);
                    RefreshParentRecDeliverySection(OrderID);
                    UpdatePanel1.Update();
                    UpdatePanel2.Update();

                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        private bool CheckDBPostCode(string OrderID, Button objButton)
        {
            bool validPost = true;
            DataSet dsPostCodeDesc = new DataSet();
            try
            {
                if (txtPostCode.Text != "")
                {
                    dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(txtPostCode.Text, OrderID);
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
                        if (txtPostCode.Text != "")
                        {
                            lblpostcodeInfo.Text = message + " {" + txtPostCode.Text + "}";
                        }
                        else
                        {
                            lblpostcodeInfo.Text = message + " {" + hdnPCAPostCode.Value + "}";
                        }
                        if (NextDeliveryDate != null && NextDeliveryDate != "")
                        {
                            hdnNextDelDate.Value = NextDeliveryDate;
                            ScriptManager.RegisterClientScriptBlock(objButton, objButton.GetType(), "Reveal", "revealSuggestDates('" + NextDeliveryDate + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(objButton, objButton.GetType(), "Reveal", "revealPostcodeMessage('" + lblpostcodeInfo.Text + "');", true);
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

        /// <summary>
        /// Update the Message card 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMessageBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
                RecipientInfo objRecDtl = new RecipientInfo();
                RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
                objRecipientDetails.EditMessageCard(strOrderId, txtMessage.Value);
                lblGiftMessage.Text = txtMessage.Value;
                BindConfirmationSection(strOrderId);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "closePopup();", true);
                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }


        /// <summary>
        /// To Update Delivery instruction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnDeliveryInst_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
                RecipientInfo objRecDtl = new RecipientInfo();
                RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();



                if (drpDelIns.SelectedItem.Text != "Please select an option")
                {
                    string instruct = string.Empty;
                    if (drpDelIns.SelectedItem.Text == "Leave with neighbour")
                    {
                        instruct = drpDelIns.SelectedItem.Text + " " + txthouseNumber.Text.Trim();
                    }
                    else
                    {
                        instruct = drpDelIns.SelectedItem.Text;
                    }

                    objRecipientDetails.EditDeliveryInstructions(strOrderId, instruct);
                    lblDeliveryInstruction.Text = instruct;
                    if (instruct.Contains("Leave with neighbour"))
                        divHouseNo.Style["display"] = "block";
                    else
                    {
                        divHouseNo.Style["display"] = "none";
                        txthouseNumber.Text = string.Empty;
                    }
                }
                BindConfirmationSection(strOrderId);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test123", "closePopup();", true);
                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Button Click to Update the Customer Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSaveCustManuallyAddress_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
                CustomerInfo objCustDtl = new CustomerInfo();
                //objCustDtl.CustomerId= strOrderId;
                string name = txtCustName.Text.Trim();
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
                objCustDtl.FirstName = firstName;
                objCustDtl.LastName = lastName;
                objCustDtl.HouseNo = TxtCustAddr1.Text.Trim();
                objCustDtl.Street = TxtCustAddr2.Text.Trim();
                objCustDtl.District = TxtCustAddr3.Text.Trim();
                objCustDtl.Town = TxtCustTown.Text.Trim();
                objCustDtl.PostCode = TxtCustPostCode.Text.Trim();
                objCustDtl.CountryID = Convert.ToInt32(drpRecCountry.SelectedItem.Value);
                CustomerDetailsBAL objCustDetails = new CustomerDetailsBAL();
                objCustDtl.CustomerId = objCustDetails.GetCustomerIdByOrderId(strOrderId);
                objCustDetails.EditCustomerDetails(objCustDtl);
                RefreshParentCustDeliverySection(strOrderId);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "testCustomerPopUp", "closePopup();", true);
                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        /// <summary>
        /// Returns Delivery Time Details for an Order
        /// </summary>
        /// <param name="orderId"></param>
        private string[] BindDeliveryTimeInfo(string orderId)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            Common objCommon = new Common();
            var resultsToReturn = new string[2];
            try
            {
                objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(orderId);

                if (objDeliveryTimeInfo != null)
                {
                    resultsToReturn[0] = objDeliveryTimeInfo.OptionName;
                    if (objDeliveryTimeInfo.deliveryPrice == 0.00)
                        resultsToReturn[1] = "FREE";
                    else
                        resultsToReturn[1] = String.Format("£{0}", objDeliveryTimeInfo.deliveryPrice);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return resultsToReturn;
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

        //******************************************************************//
        #region TestChasePayment Refund and Reversal
        protected void Refund_Click(object sender, EventArgs e)
        {
            PaymentDetailsBAL objPaymentDetails = new PaymentDetailsBAL();
            PaymentInfo objPaymentInfo = new PaymentInfo();
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
                OrbitalGatewayForRefund objRefund = new OrbitalGatewayForRefund();
                objPaymentInfo = objPaymentDetails.GetPaymentDetailsByPaymentStatus(strOrderId, 1, "DB");
                 string status=objRefund.ChasePaymentRefund(objPaymentInfo);
                 if (status == "1")
                 {
                     ChaseStatus.Text = "Refunded successfull  as per spec as Approval Status=1";
                 }
                 else
                 {
                     ChaseStatus.Text = status + "unsuccessfull";
                 }

               
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        protected void Cancellation_Click(object sender, EventArgs e)
        {
            PaymentDetailsBAL objPaymentDetails = new PaymentDetailsBAL();
            PaymentInfo objPaymentInfo = new PaymentInfo();
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
               
                objPaymentInfo = objPaymentDetails.GetPaymentDetailsByPaymentStatus(strOrderId, 1, "DB");
                OrbitalGatewayReversal objReversal = new OrbitalGatewayReversal();
                string status=objReversal.ChasePaymentCancelation(objPaymentInfo);
                if (status == "1")
                {
                    ChaseStatus.Text = " Reversal successfull as per spec as Approval Status=1";
                }
                else
                {
                    ChaseStatus.Text = status + "unsuccessfull";
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }

        #endregion
        //******************************************************************//
    }
}