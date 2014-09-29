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
using System.Data;
using Serenata_Checkout.ChilkatComponent;
using SFMobile.Exceptions;

namespace Serenataflowers.Controls
{
    public partial class ViewCustomerDetails : System.Web.UI.UserControl
    {
        string OrderID;
        CustomerDetailsBAL objCustomerDetails;
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.CustInfoUpdatePanel.UpdateMode; }
            set { this.CustInfoUpdatePanel.UpdateMode = value; }
        }

        public void Update()
        {
            if (ViewState["OrderID"] != null)
            {
                GetCustomerDetails(Convert.ToString(ViewState["OrderID"]));
            }
            this.CustInfoUpdatePanel.Update();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["s"] != null)
                {
                    Encryption objEncryption = new Encryption();

                    OrderID = objEncryption.GetAesDecryptionString(Request.QueryString["s"]);
                    ViewState["OrderID"] = OrderID;
                    GetCustomerDetails(OrderID);
                }
                else
                {
                    Response.Redirect(".");
                }
            }
        }

        public void GetCustomerDetails(string orderId)
        {
            objCustomerDetails = new CustomerDetailsBAL();
            CustomerInfo objCustomerInfo = new CustomerInfo();
            try
            {
                int iCountSpaces = 0;
                objCustomerInfo = objCustomerDetails.GetBillingDetails(orderId);
                if (objCustomerInfo != null)
                {
                    if (!string.IsNullOrEmpty(objCustomerInfo.FirstName))
                    {
                        lblName.Visible = true;
                        lblName.Text = objCustomerInfo.FirstName + " " + objCustomerInfo.LastName + "<br />";
                    }
                    else
                    {
                        lblName.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Organisation))
                    {
                        lblOrganization.Visible = true;
                        lblOrganization.Text = objCustomerInfo.Organisation + "<br />";
                    }
                    else
                    {
                        lblOrganization.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.HouseNo))
                    {
                        lblHouseNumber.Visible = true;
                        lblHouseNumber.Text = objCustomerInfo.HouseNo + "<br />";
                    }
                    else
                    {
                        lblHouseNumber.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Street))
                    {
                        lblStreet.Visible = true;
                        lblStreet.Text = objCustomerInfo.Street + "<br />";
                    }
                    else
                    {
                        lblStreet.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.District))
                    {
                        lblDistrict.Visible = true;
                        lblDistrict.Text = objCustomerInfo.District + "<br />";
                    }
                    else
                    {
                        lblDistrict.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.PostCode))
                    {
                        lblPostCode.Visible = true;
                        lblPostCode.Text = objCustomerInfo.PostCode + "<br />";
                    }
                    else
                    {
                        lblPostCode.Visible = false;
                        iCountSpaces++;
                    }
                    if (objCustomerInfo.Town != "")
                    {
                        lblTown.Visible = true;
                        lblTown.Text = objCustomerInfo.Town + "<br />";
                    }
                    else
                    {
                        lblTown.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.Country))
                    {
                        lblCountry.Visible = true;
                        lblCountry.Text = objCustomerInfo.Country + "<br />";
                    }
                    else
                    {
                        lblCountry.Visible = false;
                        iCountSpaces++;
                    }
                    if (!string.IsNullOrEmpty(objCustomerInfo.UKMobile))
                    {
                        lblMobile.Visible = true;
                        lblMobile.Text = objCustomerInfo.UKMobile + "<br />";
                    }
                    else
                    {
                        iCountSpaces++;
                        lblMobile.Visible = false;
                    }
                    if (lblHiddenText.Value == "Customer Details:")
                    {
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
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
    }
}