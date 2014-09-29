using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
using System.Data;
namespace Serenata_Checkout.Bal
{
    public class PaymentDetailsBAL
    {
        PaymentDetailsDAL objPaymentDetailsDAL = new PaymentDetailsDAL();
        public int MakePaymentOrRefund(PaymentInfo objPaymentInfo)
        {
            return new PaymentDetailsDAL().MakePaymentOrRefund(objPaymentInfo);
        }
        public void UpdateOrderStatus(PaymentInfo objPaymentInfo)
        {
            new PaymentDetailsDAL().UpdateOrderStatus(objPaymentInfo);
        }
        public void UpdateDHHPResponse(DHHPInfo objDHPPInfo)
        {
            new PaymentDetailsDAL().UpdateDHHPResponse(objDHPPInfo);
        }
        public DHHPInfo GetDHHPResponse(string strOrderId)
        {
            return new PaymentDetailsDAL().GetDHHPResponse(strOrderId);
        }
        public int GetCardTypeByCardName(string CardName)
        {
            return new PaymentDetailsDAL().GetCardTypeByCardName(CardName);
        }
        public PaymentInfo GetPaymentDetailsByPaymentStatus(string strOrderId, int paymentStatus, string chasePaymentType)
        {
            return new PaymentDetailsDAL().GetPaymentDetailsByPaymentStatus(strOrderId, paymentStatus, chasePaymentType);
        }
        public string GetCardCountry(string ISOCountry)
        {
            return new PaymentDetailsDAL().GetCardCountry(ISOCountry);
        }
        public int InsertChaseDetails(string OrderId, string UUID)
        {
            return new PaymentDetailsDAL().InsertChaseDetails(OrderId, UUID);
        }
        public void UpdateOrderDate(string OrderId)        
        {

            new PaymentDetailsDAL().UpdateOrderDate(OrderId);
        }
        public int CheckDuplicatePayment(PaymentInfo objPaymentInfo)        {

            return new PaymentDetailsDAL().CheckDuplicatePayment(objPaymentInfo);
        }
        public void UpdateIsRedirectURL(string OrderId, string IsRedirect)
        {

            new PaymentDetailsDAL().UpdateIsRedirectURL(OrderId, IsRedirect);
        }
        public int IsPaymentExists(string OrderID, double Totalamount)
        {

            return new PaymentDetailsDAL().IsPaymentExists(OrderID, Totalamount);
        }
        public void UpdateGetStatusTime(string OrderId, string UUID)
        {

            new PaymentDetailsDAL().UpdateGetStatusTime(OrderId, UUID);
        }
        public void InsertChaseActions(string OrderId, string Action, string Response, string UUID, string TrasactionSecret)
        {
            new PaymentDetailsDAL().InsertChaseActions(OrderId, Action, Response, UUID, TrasactionSecret);
        }
        public string CheckOrderPaymentStatus(string OrderID)
        {
            return new PaymentDetailsDAL().CheckOrderPaymentStatus(OrderID);
        }
    }
}
