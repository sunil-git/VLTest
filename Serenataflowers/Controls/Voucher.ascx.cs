using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Bal.Common;
using SFMobile.Exceptions;
namespace Serenataflowers.Controls
{
    public partial class Voucher : System.Web.UI.UserControl
    {
        string orderID;
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.upVoucher.UpdateMode; }
            set { this.upVoucher.UpdateMode = value; }
        }

        public void Update()
        {
            this.upVoucher.Update();
        }
        public event EventHandler ButtonClick;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    if (!IsPostBack)
                    {
                        orderID = Common.GetOrderIdFromQueryString();
                        Submit.Attributes.Add("data-theme", "a");
                        Submit.Attributes.Add("data-role", "button");
                        Submit.Attributes.Add("data-inline", "true");
                        Submit.Attributes.Add("data-mini", "true");
                        Submit.Attributes.Add("data-icon", "edit");
                        Submit.Attributes.Add("data-iconpos", "left");
                        ViewState["OrderID"] = orderID;
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderId = Common.GetOrderIdFromQueryString();
                CommonBal objCommonBal = new CommonBal();
                VoucherInfo objV = objCommonBal.ValidateVoucherCode(strOrderId, DiscountVoucher.Text.Trim(),new Common().GetSiteId());
                int intStatus = 0;
                string strMessage = string.Empty;
                intStatus = objV.Status;
                switch (new Common().ValidateVoucherCode(intStatus))
                {

                    case Common.VoucherMsg.Invalidvouchercode:
                        strMessage = "Voucher code is not valid.";
                        ErrorVoucher.Style["display"] = "block";
                        ErrorVoucher.Style["color"] = "Red";
                        ErrorVoucher.InnerText = "Error: " + strMessage;
                        ErrorVoucher.Style["font-weight"] = "normal";
                        ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "WarnUser();", true);
                        break;
                    case Common.VoucherMsg.Vouchercodenotexists:
                        strMessage = "Voucher code does not exists.";
                        ErrorVoucher.Style["display"] = "block";
                        ErrorVoucher.Style["color"] = "Red";
                        ErrorVoucher.InnerText = "Error: " + strMessage;
                        ErrorVoucher.Style["font-weight"] = "normal";
                        ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "WarnUser();", true);
                        break;
                    case Common.VoucherMsg.Voucherexpired:
                        strMessage = "Vouchercode is expired.";
                        ErrorVoucher.Style["display"] = "block";
                        ErrorVoucher.Style["color"] = "Red";
                        ErrorVoucher.InnerText = "Error: " + strMessage;
                        ErrorVoucher.Style["font-weight"] = "normal";
                        ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "WarnUser();", true);
                        break;
                    case Common.VoucherMsg.Voucheralreadyused:
                        strMessage = "Already a vouchercode is used for this order.";
                        ErrorVoucher.Style["display"] = "block";
                        ErrorVoucher.Style["color"] = "Red";
                        ErrorVoucher.Style["font-weight"] = "normal";
                        ErrorVoucher.InnerText = "Error: " + strMessage;
                        ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "WarnUser();", true);
                        break;
                    case Common.VoucherMsg.Success:
                        ErrorVoucher.Style["display"] = "block";
                        DiscountVoucher.Style["background"] = "";
                        DiscountVoucher.Style["border"] = "";
                        strMessage = "Vouchercode has been validated succesfully.";
                        ErrorVoucher.Style["color"] = "green";
                        ErrorVoucher.Style["font-weight"] = "bold";
                        ErrorVoucher.InnerText = "Success: " + strMessage;
                        ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "testff123", "Close();", true);
                        if (ButtonClick != null)
                        {
                            //fires the event passing the same arguments of the button
                            //click event
                            ButtonClick(sender, e);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            upVoucher.Update();
            //ScriptManager.RegisterClientScriptBlock(Submit, Submit.GetType(), "test123", "bindVoucher();", true);
        }



    
        

    }
}