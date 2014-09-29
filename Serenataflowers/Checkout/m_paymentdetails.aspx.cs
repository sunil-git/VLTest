using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.PaymentGateway;
using System.Xml;
using System.Data;
using System.Configuration;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using System.IO;
using System.Web.UI.HtmlControls;

namespace Serenataflowers.Checkout
{
    public partial class m_paymentdetails : System.Web.UI.Page
    {
        ChasePaymentDHPP objchase = new ChasePaymentDHPP();
        string token, paypalURL, dopayurl;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public string DoPayURL
        {
            get { return dopayurl; }
            set { dopayurl = value; }
        }
        public string PaypalURL
        {
            get { return paypalURL; }
            set { paypalURL = value; }
        }

        protected void SaveDates_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            //DisplayBasketCount.Update();
            ModifyBasket.Update();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument doc, responseDoc;
            PaymentDetailsBAL objPaymentDetails = new PaymentDetailsBAL();
            DHHPInfo UpdateDHHPDetails;
            CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
            CustomerInfo objCustomerInfo = new CustomerInfo();

            if (!IsPostBack)
            {
                if (Request.QueryString["s"] != null)
                {
                    try
                    {


                        string OrderID = Common.GetOrderIdFromQueryString(); //"224079266"; //
                        Check48hrPostCode();

                        double ordTotal = new OrderDetailsBAL().GetOrderTotalByOrderID(OrderID);

                        if (ordTotal == 0)
                        {
                            string nextPageUrl = "m_confirmation.aspx?s=" + Request.QueryString["s"];
                            Response.Redirect(nextPageUrl, false);
                        }

                        SetProgressBarLinks();

                        objCustomerInfo = objCustomerDetails.GetBillingDetails(OrderID);

                        //PaypalURL = "http://localhost/SerenataCheckout/Checkout/m_expressCheckout.aspx?s" + Request.QueryString["s"];
                        PaypalURL = "https://" + GetSiteName() + ConfigurationManager.AppSettings["PaypalExpressCheckoutURL"] + Request.QueryString["s"];

                        DHHPInfo DHHPInfoDetails = new DHHPInfo();
                        DHHPInfoDetails = GetDHPPDetails(OrderID);
                        objPaymentDetails = new PaymentDetailsBAL();
                        isAmount.InnerText = Convert.ToString(DHHPInfoDetails.Amount);
                        int paymentIsxits = objPaymentDetails.IsPaymentExists(OrderID, DHHPInfoDetails.Amount);
                        if (paymentIsxits != 0)
                        {
                            divPaymentExit.Attributes.Add("display", "block");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "dsd", "showIsError();", true);
                            DoPayURL = "";
                        }
                        else
                        {
                            divPaymentExit.Attributes.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "dsd", "HideIsError();", true);

                            string TS = Convert.ToString(Guid.NewGuid());
                            doc = objchase.ChasePaymentXMLRequest(TS, DHHPInfoDetails, objCustomerInfo);

                            objPaymentDetails.InsertChaseActions(OrderID, "DoPrepareRequest", doc.InnerXml, DHHPInfoDetails.UUID, TS);

                            responseDoc = objchase.DoPrepare(TS, doc);
                            Token = objchase.GetToken(responseDoc);
                            objPaymentDetails.InsertChaseActions(OrderID, "DoPrepareResponse", Token, DHHPInfoDetails.UUID, TS);
                            if (Token != "")
                            {
                                divErrorDHPP.Attributes.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "afsdflertHi", "HideError();", true);
                                DoPayURL = ConfigurationManager.AppSettings["DoPayRequestURL"] + Token;
                                objPaymentDetails.InsertChaseActions(OrderID, "DoPayRequest", DoPayURL, DHHPInfoDetails.UUID, TS);
                                objPaymentDetails.InsertChaseActions(OrderID, "RedirectURL", "No", DHHPInfoDetails.UUID, TS);
                                UpdateDHHPDetails = new DHHPInfo();
                                UpdateDHHPDetails.Token = Token;
                                UpdateDHHPDetails.OrderID = OrderID;
                                UpdateDHHPDetails.TrasactionSecrete = TS;
                                UpdateDHHPDetails.UUID = DHHPInfoDetails.UUID;
                                objPaymentDetails.UpdateDHHPResponse(UpdateDHHPDetails);
                                // objPaymentDetails.UpdateIsRedirectURL(OrderID, "No");
                                //objPaymentDetails.InsertChaseDetails(OrderID, DHHPInfoDetails.UUID);
                            }
                            else
                            {
                                divErrorDHPP.Attributes.Add("display", "block");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "afsdflertHi", "showError();", true);
                                DoPayURL = "";
                            }
                        }

                        Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, "Step3");
                    }
                    catch (Exception ex)
                    {
                        SFMobileLog.Error(ex);
                    }

                    //OrbitalGatewayMarkForCapture objmar = new OrbitalGatewayMarkForCapture();
                    //objmar.ChasePaymentMarkForCapture();

                    //OrbitalGatewayForRefund objRefund = new OrbitalGatewayForRefund();
                    //objRefund.ChasePaymentRefund();

                    //OrbitalGatewayReversal objReversal = new OrbitalGatewayReversal();
                    //objReversal.ChasePaymentCancelation();
                }
            }
            new Common().CheckCutOffTime(MasterBody, Common.GetOrderIdFromQueryString());
        }

        public DHHPInfo GetDHPPDetails(string OrderID)
        {
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            DHHPInfo objDHHPInfo = new DHHPInfo();
            try
            {
                objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(OrderID);
                objDHHPInfo.Amount = Convert.ToDouble(String.Format("{0:0.00}", objDeliveryTimeInfo.OrderTotal));
                objDHHPInfo.Currency = "GBP";
                objDHHPInfo.UUID = Convert.ToString(GenerateId());
                objDHHPInfo.OrderID = OrderID;
                objDHHPInfo.ProductDescription = GetProductDescriptor();
                objDHHPInfo.MerchantEmail = ConfigurationManager.AppSettings["MerchantEmail"];
                objDHHPInfo.EncryptedOrderID = Request.QueryString["s"];

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objDHHPInfo;
        }

        private void SetProgressBarLinks()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                ancArrowCust.HRef = "m_customerdetails.aspx?s=" + Request.QueryString["s"];
                ancArrowRec.HRef = "m_recipientdetails.aspx?s=" + Request.QueryString["s"];
            }
        }

        private string GetProductDescriptor()
        {
            string ProductDescriptor = "SERENATAFLOWERS.CO";

            try
            {
                List<ProductTypeInfo> lstProductTypeInfo = new List<ProductTypeInfo>();
                Common objCommondetails = new Common();
                string xPathProductType = Path.GetFullPath(System.Configuration.ConfigurationManager.AppSettings["ProductTypeCategoryXML"].ToString());
                lstProductTypeInfo = objCommondetails.GetProductType(xPathProductType);
                if (lstProductTypeInfo != null)
                {
                    string url = Request.Url.ToString();
                    Uri baseUri = new Uri(url);
                    string domain = baseUri.Host;
                    foreach (ProductTypeInfo objdomain in lstProductTypeInfo)
                    {
                        if (objdomain.domainName.ToLower() == domain.ToLower())
                        {
                            switch (objdomain.ProductTypeId.ToString())
                            {
                                case "1":
                                    ProductDescriptor = "SERENATAFLOWERS.CO";
                                    break;
                                case "3":
                                    ProductDescriptor = "SERENATACHOCOLATES";
                                    break;
                                case "4":
                                    ProductDescriptor = "SERENATAHAMPERS.CO";
                                    break;
                                case "5":
                                    ProductDescriptor = "SERENATAWINES.COM";
                                    break;

                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            }
            return ProductDescriptor;
        }
        private long GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 8);
        }

        public string GetSiteName()
        {
            string domain = string.Empty;
            try
            {
                string url = HttpContext.Current.Request.Url.ToString();
                Uri baseUri = new Uri(url);
                domain = baseUri.Host;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return domain;
        }

        private bool Check48hrPostCode()
        {
            bool bResult = true;
            RecipientDetailsBAL objRecipientDetails = new RecipientDetailsBAL();
            RecipientInfo objRecInfo = new RecipientInfo();
            try
            {
                string OrderID = Common.GetOrderIdFromQueryString();
                objRecInfo = objRecipientDetails.GetDeliveryDetails(OrderID);
                DataSet dsPostCodeDesc = objRecipientDetails.CheckNonDelPostCode(objRecInfo.PostCode, OrderID);
                if (dsPostCodeDesc != null && dsPostCodeDesc.Tables.Count > 0)
                {
                    if (Convert.ToString(dsPostCodeDesc.Tables[0].Rows[0]["ValidPC"]) != "1")
                    {
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

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return bResult;
        }
    }

}