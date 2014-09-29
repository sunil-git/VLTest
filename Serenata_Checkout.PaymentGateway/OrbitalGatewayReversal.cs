using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Data;
using System.Configuration;
using SFMobile.Exceptions;
using Serenata_Checkout.Dto;
using System.ServiceModel;
using System.Globalization;
using Serenata_Checkout.PaymentGateway.PaymentechGatewayServiceReference;
namespace Serenata_Checkout.PaymentGateway
{
    public class OrbitalGatewayReversal
    {

        public string  ChasePaymentCancelation(PaymentInfo objPaymentInfo)
        {
            string status = string.Empty;
            try
            {
                PaymentechGatewayPortTypeClient objClient = new PaymentechGatewayPortTypeClient();
                ReversalElement objNewOrderReversalElement = new ReversalElement();
                objNewOrderReversalElement.orbitalConnectionUsername = ConfigurationManager.AppSettings["OrbitalUserName"];
                objNewOrderReversalElement.orbitalConnectionPassword = ConfigurationManager.AppSettings["OrbitalPassword"];
                objNewOrderReversalElement.version = "2.7";
                objNewOrderReversalElement.bin = ConfigurationManager.AppSettings["Bin"];
                objNewOrderReversalElement.merchantID = ConfigurationManager.AppSettings["MerchantID"];
                objNewOrderReversalElement.terminalID = ConfigurationManager.AppSettings["TerminalID"];
                objNewOrderReversalElement.orderID = objPaymentInfo.OrderID;
                objNewOrderReversalElement.txRefNum = objPaymentInfo.TransID;
                ReversalResponseElement objReversalResponseElement = new ReversalResponseElement();
                objReversalResponseElement = objClient.Reversal(objNewOrderReversalElement);
                status = objReversalResponseElement.approvalStatus;
                return status;
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                status = ex.Message;
                return status;
            }
            
        }
    }
}
