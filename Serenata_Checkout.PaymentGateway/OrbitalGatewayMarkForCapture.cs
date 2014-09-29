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
    public class OrbitalGatewayMarkForCapture
    {


        public void ChasePaymentMarkForCapture()
        {
            
            PaymentechGatewayPortTypeClient objClient = new PaymentechGatewayPortTypeClient();
            MFCElement objMFCRequest = new MFCElement();
            objMFCRequest.orbitalConnectionUsername = ConfigurationManager.AppSettings["OrbitalUserName"];
            objMFCRequest.orbitalConnectionPassword = ConfigurationManager.AppSettings["OrbitalPassword"];
            objMFCRequest.orderID = "224079266";
            objMFCRequest.merchantID = "732214";
            objMFCRequest.bin = "000001";
            objMFCRequest.txRefNum = "5284ED050D1DF2D49B64BFB10C8B838F668F5318";
            objMFCRequest.terminalID = "001";
            objMFCRequest.version = "2.7";
            objMFCRequest.amount = "5.99";

            MFCResponseElement objMFCRequest1 = new MFCResponseElement();
            objMFCRequest1 = objClient.MFC(objMFCRequest);
                 
        }
    }
}
