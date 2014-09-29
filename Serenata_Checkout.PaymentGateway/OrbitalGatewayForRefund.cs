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
using System.Configuration;
using System.Globalization;
using Serenata_Checkout.PaymentGateway.PaymentechGatewayServiceReference;
namespace Serenata_Checkout.PaymentGateway
{
    public class OrbitalGatewayForRefund
    {
        public string  ChasePaymentRefund(PaymentInfo objPaymentInfo)
        {
            string status = string.Empty;
            try
            {
                PaymentechGatewayPortTypeClient objClient = new PaymentechGatewayPortTypeClient();
                NewOrderRequestElement objNewOrderRequestElement = new NewOrderRequestElement();
                objNewOrderRequestElement.orbitalConnectionUsername = ConfigurationManager.AppSettings["OrbitalUserName"];
                objNewOrderRequestElement.orbitalConnectionPassword = ConfigurationManager.AppSettings["OrbitalPassword"];
                objNewOrderRequestElement.version = "2.7";
                objNewOrderRequestElement.industryType = ConfigurationManager.AppSettings["IndustryType"];
                objNewOrderRequestElement.transType = ConfigurationManager.AppSettings["TransType"];
                objNewOrderRequestElement.bin = ConfigurationManager.AppSettings["Bin"];
                objNewOrderRequestElement.merchantID = ConfigurationManager.AppSettings["MerchantID"];
                objNewOrderRequestElement.terminalID = ConfigurationManager.AppSettings["TerminalID"];

                objNewOrderRequestElement.orderID = objPaymentInfo.OrderID;
                objNewOrderRequestElement.txRefNum = objPaymentInfo.TransID;               
               

                objNewOrderRequestElement.amount = Convert.ToString((objPaymentInfo.TotalAmount * 100));

                NewOrderResponseElement objNewOrderResponseElement = new NewOrderResponseElement();
                objNewOrderResponseElement = objClient.NewOrder(objNewOrderRequestElement);

                status = objNewOrderResponseElement.approvalStatus;
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
