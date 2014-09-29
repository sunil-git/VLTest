using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFMobile.Exceptions;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Bal;
using Serenata_Checkout.ChilkatComponent;

namespace Serenataflowers.Controls
{
    public partial class ViewRecipientDetails : System.Web.UI.UserControl
    {
        string OrderID;
        RecipientDetailsBAL objRecipientDetails;
        public string _ChildPageName = string.Empty;

        public string ChildPageName
        {
            get
            {
                return _ChildPageName;
            }
            set
            {
                _ChildPageName = value;
            }
        }
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.updDeliveryInfoPanel.UpdateMode; }
            set { this.updDeliveryInfoPanel.UpdateMode = value; }
        }

        public void Update()
        {
            if (ViewState["OrderID"] != null)
            {
                GetRecipientDetails(Convert.ToString(ViewState["OrderID"]));
            }
            this.updDeliveryInfoPanel.Update();
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
                    GetRecipientDetails(OrderID);
                }
                else
                {
                    Response.Redirect(".");
                }
            }
        }

        public void GetRecipientDetails(string orderId)
        {
            objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                int iCountSpaces = 0;
                objRecInfo = objRecipientDetails.GetDeliveryDetails(orderId);
                if (objRecInfo != null)
                {
                    if (!string.IsNullOrEmpty(objRecInfo.FirstName))
                    {
                        lblName.Visible = true;
                        lblName.Text = objRecInfo.FirstName + " " + objRecInfo.LastName + "<br />";
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
                    if (ChildPageName == "Confirmation")
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