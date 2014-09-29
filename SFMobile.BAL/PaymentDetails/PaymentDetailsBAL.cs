using System;
using System.Linq;
using System.Text;
using SFMobile.DAL;
using SFMobile.DTO;
using System.Data;
using System.Xml;
using SFMobile.DAL.PaymentDetails;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SFMobile.BAL.PaymentDetails
{
    public class PaymentDetailsBAL
    {
        PaymentDetailsDAL objpayment = new PaymentDetailsDAL();

        public int MakePaymentOrRefund(PaymentDTO objPaymentInfo)
        {
            return new PaymentDetailsDAL().MakePaymentOrRefund(objPaymentInfo);
        }
        public int AddMakePayment(PaymentDTO objPaymentInfo)
        {
            return new PaymentDetailsDAL().AddMakePayment(objPaymentInfo);
        }
        public int UpdatePaymentOrder(PaymentDTO objPaymentInfo)
        {
            return new PaymentDetailsDAL().UpdatePaymentOrder(objPaymentInfo);
        }
        public void UpdateProductPaymentStatus(string strOrderId)
        {
            new PaymentDetailsDAL().UpdateProductPaymentStatus(strOrderId);
        }
        public void UpdateOrderStatus(string strOrderId, int orderStatus)
        {
            new PaymentDetailsDAL().UpdateOrderStatus(strOrderId, orderStatus);
        }
    }
}
