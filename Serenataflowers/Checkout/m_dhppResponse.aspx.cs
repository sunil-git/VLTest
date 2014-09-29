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
using SFMobile.DTO;
using System.IO;
using Serenata_Checkout.Bal.Confirmation;
using System.Web.UI.HtmlControls;
namespace Serenataflowers.Checkout
{
    public partial class m_dhppResponse : System.Web.UI.Page
    {
        XmlDocument doc, responseDoc;
        ChasePaymentDHPP objchase = new ChasePaymentDHPP();
        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentDetailsBAL objPaymentDetails = new PaymentDetailsBAL();
            string metaPageTitle = string.Empty;
            if (!IsPostBack)
            {
                if (Request.QueryString["s"] != null)
                {
                    try
                    {
                        string OrderID = Common.GetOrderIdFromQueryString();

                        //objPaymentDetails.UpdateIsRedirectURL(OrderID, "Yes");

                        DHHPInfo DHHPInfoDetails = new DHHPInfo();
                        DHHPInfoDetails = objPaymentDetails.GetDHHPResponse(OrderID);

                       // objPaymentDetails.UpdateGetStatusTime(OrderID, DHHPInfoDetails.UUID);

                        objPaymentDetails.InsertChaseActions(OrderID, "RedirectURL", "Yes", DHHPInfoDetails.UUID, DHHPInfoDetails.TrasactionSecrete);

                        int delay = Convert.ToInt32(ConfigurationManager.AppSettings["GetStatusDelayInMillisecond"]);
                        System.Threading.Thread.Sleep(delay);
                        string GetStatusURL = ConfigurationManager.AppSettings["GetChasePaymentStatusURL"] + DHHPInfoDetails.TrasactionSecrete + "&token=" + DHHPInfoDetails.Token;

                        objPaymentDetails.InsertChaseActions(OrderID, "GetStatusRequest", GetStatusURL, DHHPInfoDetails.UUID, DHHPInfoDetails.TrasactionSecrete);

                        responseDoc = objchase.GetStatus(DHHPInfoDetails.TrasactionSecrete, DHHPInfoDetails.Token);
                        objPaymentDetails.InsertChaseActions(OrderID, "GetStatusResponse", responseDoc.InnerXml, DHHPInfoDetails.UUID, DHHPInfoDetails.TrasactionSecrete);

                       
                        //string checkRes = GetRetrieveConstantValueFromXml("Return", responseDoc);
                        //if (checkRes == "Prepared" || checkRes == "Waiting for shopper")
                        //{
                        //    responseDoc = objchase.GetStatus(DHHPInfoDetails.TrasactionSecrete, DHHPInfoDetails.Token);
                        //}
                        //*******************
                        if (ConfigurationManager.AppSettings["WriteGetStatusXml"] == "True")
                        {
                            string time = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                            string filePath = ConfigurationManager.AppSettings["GetStausXMLPath"];
                            if (Directory.Exists(filePath))
                            {

                                responseDoc.Save(filePath + "\\" + OrderID + "_" + time + "_" + "Response.xml");
                            }
                            else
                            {
                                Directory.CreateDirectory(filePath);
                                responseDoc.Save(filePath + "\\" + OrderID + "_" + time + "_" + "Response.xml");
                            }
                        }
                        //*******************
                        string Return = GetRetrieveConstantValueFromXml("Return", responseDoc);                        
                        string code = RetrieveCodeAttributeFromXml(responseDoc);
                        List<DHPPResponseCodeInfo> onjListResponse = new List<DHPPResponseCodeInfo>();
                        onjListResponse = Common.ParseDHHPResponseDetails(ConfigurationManager.AppSettings["DHHPResponseXMLPath"]);

                        DHPPResponseCodeInfo objDHPPResponseCodeInfo = onjListResponse.Where(t => t.ResponseCode == code).FirstOrDefault();
                        if (objDHPPResponseCodeInfo != null)
                        {
                            if (Return == "Transaction succeeded" && objDHPPResponseCodeInfo.Status == "Approved")
                            {
                                PaymentInfo objPaymentInfo = new PaymentInfo();
                                PaymentDetailsBAL objPayment = new PaymentDetailsBAL();
                                CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                                objPaymentInfo = GetStatusDetails(responseDoc, Convert.ToString(DHHPInfoDetails.Amount));
                                string ProfileID = GetRetrieveConstantValueFromXml("ConnectorTxID3", responseDoc);
                                int isDuplicate = objPayment.CheckDuplicatePayment(objPaymentInfo);
                                if (isDuplicate != 1)
                                {
                                    if (!string.IsNullOrEmpty(ProfileID))
                                    {
                                        objCustomerDetails.UpdateCustomerProfileID(OrderID, ProfileID);
                                    }
                                    int res = objPayment.MakePaymentOrRefund(objPaymentInfo);
                                  
                                    UpdateOrderComplete(OrderID);
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "afsdflertHi", "HideError();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "<script type='text/javascript'>window.parent.location.href = 'm_confirmation.aspx?s=" + Request.QueryString["s"] + "'; </script>", false);
                                metaPageTitle = "PaymentSuccess";
                            }
                            else
                            {
                                SFMobileLog.Info(objDHPPResponseCodeInfo.CustomMessage);
                                ErrorChase.InnerHtml = objDHPPResponseCodeInfo.CustomMessage;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "afsdflertHi", "displayError();", true);




                                PaymentInfo objPaymentInfo = new PaymentInfo();
                                PaymentDetailsBAL objPayment = new PaymentDetailsBAL();
                                objPaymentInfo = GetStatusDetails(responseDoc, Convert.ToString(DHHPInfoDetails.Amount));
                                objPaymentInfo.Comment = objDHPPResponseCodeInfo.Message;
                                objPaymentInfo.PaymentTypeID = 16;
                                objPaymentInfo.PaymentStatus = 0;
                                int res = objPayment.MakePaymentOrRefund(objPaymentInfo);
                                metaPageTitle = "PaymentFail-" + objDHPPResponseCodeInfo.CustomMessage;
                            }
                        }
                        else
                        {
                            SFMobileLog.Info(Return);
                            ErrorChase.InnerHtml = Return;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "afsdflertHi", "displayError();", true);




                            PaymentInfo objPaymentInfo = new PaymentInfo();
                            PaymentDetailsBAL objPayment = new PaymentDetailsBAL();
                            objPaymentInfo = GetStatusDetails(responseDoc, Convert.ToString(DHHPInfoDetails.Amount));
                            objPaymentInfo.Comment = Return;
                            objPaymentInfo.PaymentTypeID = 16;
                            objPaymentInfo.PaymentStatus = 0;
                            int res = objPayment.MakePaymentOrRefund(objPaymentInfo);
                            metaPageTitle = "PaymentFail-" + Return;
                        }

                        Common.AddMetaTags(OrderID, (HtmlHead)Page.Header, metaPageTitle);
                    }
                    catch (Exception ex)
                    {
                        SFMobileLog.Error(ex);
                    }

                }
                else
                {
                    SFMobileLog.Error("Query string is null");
                }
            }
        }

      

        public  string GetRetrieveConstantValueFromXml(string str_nodeName, XmlDocument xmlDoc)
        {
            string str_nodeValue = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc = xmlDoc;               
                if (doc.GetElementsByTagName(str_nodeName).Count > 0)
                {
                    str_nodeValue = doc.GetElementsByTagName(str_nodeName)[0].InnerText;

                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return str_nodeValue.Trim();
        }
        public string RetrieveCodeAttributeFromXml(XmlDocument xmlDoc)
        {
            string str_nodeValue = string.Empty;
            try
            {
                XmlNode xmlNodeElement = xmlDoc.SelectSingleNode("Response/Transaction/Processing/Return");
                if (xmlNodeElement != null)
                {
                    str_nodeValue=(xmlNodeElement.Attributes["code"] == null) ? "" : xmlNodeElement.Attributes["code"].Value;
                }
                
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return str_nodeValue.Trim();
        }
        public PaymentInfo GetStatusDetails(XmlDocument xmlDoc,string amount)
        {
            PaymentDetailsBAL objPaymentDetails = new PaymentDetailsBAL();
            PaymentInfo objPaymentInfo = new PaymentInfo();
            try
            {
                string TrasactionID = GetRetrieveConstantValueFromXml("ConnectorTxID1", xmlDoc);
                string[] arrTrasactionID = TrasactionID.Split(new char[] { ';' });
                if (arrTrasactionID.Length > 1)
                {
                    objPaymentInfo.TransID = arrTrasactionID[0];
                }
                else {
                    objPaymentInfo.TransID = "";
                }
                string Cardname = GetRetrieveConstantValueFromXml("Brand", xmlDoc);
                string isocountry = GetRetrieveConstantValueFromXml("Country", xmlDoc);
                objPaymentInfo.OrderID = GetRetrieveConstantValueFromXml("OrderID", xmlDoc);
                objPaymentInfo.PaymentTypeID = 12;
                objPaymentInfo.PaymentGatewayID = 5;
                objPaymentInfo.IDCardType = objPaymentDetails.GetCardTypeByCardName(Cardname);
                objPaymentInfo.IDTransType = 1;
                objPaymentInfo.PaymentStatus = 1;
                objPaymentInfo.Amount = Convert.ToDouble(amount);
                objPaymentInfo.TotalAmount = Convert.ToDouble(amount);
                objPaymentInfo.CardScheme = Cardname;
                objPaymentInfo.CardCountry = objPaymentDetails.GetCardCountry(isocountry);
                objPaymentInfo.ChasePaymentType = ConfigurationManager.AppSettings["PaymentType"];
                objPaymentInfo.Comment = "Chase payment accepted";
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objPaymentInfo;
        }
        private void UpdateOrderComplete(string strOrderId)
        {
            ConfirmationBAL objConfirmBal = new ConfirmationBAL();
               PaymentDetailsBAL objPayment = new PaymentDetailsBAL();
            objConfirmBal.UpdateOrderComplete(strOrderId);
            objConfirmBal.UpdateProductPaymentStatus(strOrderId);
            objConfirmBal.UpdateOrderStatusForPaypal(strOrderId);
            objPayment.UpdateOrderDate(strOrderId);

        }
        protected void btnBacktoPayment_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "<script type='text/javascript'>window.parent.location.href = 'm_paymentdetails.aspx?s=" + Request.QueryString["s"] + "'; </script>", false);
            //Response.Redirect("m_paymentdetails.aspx?s=" + Request.QueryString["s"], false);
        }
    }
}