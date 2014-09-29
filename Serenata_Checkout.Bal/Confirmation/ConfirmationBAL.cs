using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Xml;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Log;
using Serenata_Checkout.Dal.Confirmation;

namespace Serenata_Checkout.Bal.Confirmation
{
    public class ConfirmationBAL
    {
        public void UpdateOrderComplete(string strOrderId)
        {
            new ConfirmationDAL().UpdateOrderComplete(strOrderId);
        }

        public ConfirmDetailsInfo GetConfirmationPageDetails(string strOrderId)
        {
            return new ConfirmationDAL().GetConfirmationPageDetails(strOrderId);
        }

        public DiscountInfo InsertReOrderVoucher(string strOrderId, string strVoucherId)
        {
            return new ConfirmationDAL().InsertReOrderVoucher(strOrderId, strVoucherId);
        }

        public ProductInfo GetProductDetails(int iProductId)
        {
            return new ConfirmationDAL().GetProductDetails(iProductId);
        }

        public void UpdateProductPaymentStatus(string strOrderId)
        {
            new ConfirmationDAL().UpdateProductPaymentStatus(strOrderId);
        }

        public OrderTotalInfo GetOrderAndPaymentTotals(string strOrderId)
        {
            return new ConfirmationDAL().GetOrderAndPaymentTotals(strOrderId);
        }

        public List<OccassionCard> GetExtraProducts(string orderID)
        {
            return new ConfirmationDAL().GetExtraProducts(orderID);
        }

        public void UpdateOrderStatusForPaypal(string strOrderId)
        {
            new ConfirmationDAL().UpdateOrderStatusForPaypal(strOrderId);
        }

        public ConfirmDetailsInfo GetDataForMetaTagInConfirmation(string strOrderId)
        {
            return new ConfirmationDAL().GetDataForMetaTagInConfirmation(strOrderId);
        }
    }
}

