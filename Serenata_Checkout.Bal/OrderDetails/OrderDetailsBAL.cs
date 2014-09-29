#region About the Class
/// <summary>
/// Author: Valuelabs
/// Class Name: SerenataCheckoutLogic
/// Description: This class contains the business logic to manipulate the data for new order schema related information by using below methods.
/// <summary>
#endregion

#region Import Section
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
#endregion

namespace Serenata_Checkout.Bal
{
    public class OrderDetailsBAL
    {
        OrderDetailsDAL objOrderDetailsDal = new OrderDetailsDAL();
        #region 1. To create an order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CreateOrder
        /// Description: This method contains logic to execute stored procedure "SFsp_CreateOrder" to save/update the order details into [Orders] table.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public string CreateOrder(OrderInfo objOrder)
        {
            return objOrderDetailsDal.CreateOrder(objOrder);
        }
        #endregion
        #region 2. To StoreAffiliateTracking.
        /// <summary>
        /// StoreAffiliateTracking
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="awaid"></param>
        /// <param name="awgid"></param>
        /// <param name="awbid"></param>
        /// <param name="awpid"></param>
        /// <param name="awcr"></param>
        /// <param name="awcd"></param>
        public void StoreAffiliateTracking(string OrderID, string awaid, string awgid, string awbid, string awpid, string awcr, string awcd)
        {
            objOrderDetailsDal.StoreAffiliateTracking(OrderID, awaid, awgid, awbid, awpid, awcr, awcd);
        }
        #endregion
        #region 3. To ScheduleDispatch.
        /// <summary>
        /// ScheduleDispatch
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="objDispatchInfo"></param>
        public void ScheduleDispatch(string OrderID, DispatchesInfo objDispatchInfo)
        {
            objOrderDetailsDal.ScheduleDispatch(OrderID, objDispatchInfo);
        }
        #endregion
        #region 4. To update the encrypted order id
        /// <summary>
        /// Author:Valuelabs
        /// Method Name: UpdateEncryptedOrderId()
        /// StoredProcedure Name: SFsp_UpdateOrderWithEncryptedOrderID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_UpdateOrderWithEncryptedOrderID" to update the encrypted order id into [Orders].
        /// </summary>
        /// <param name="objCustomerInfo">The obj of order info.</param>
        /// <remarks></remarks>
        public void UpdateEncryptedOrderId(OrderInfo objOrderInfo)
        {
            objOrderDetailsDal.UpdateEncryptedOrderId(objOrderInfo);
        }
        #endregion

        public int AddProductToBasket(DispatchesInfo objDispatchesInfo, OrderLinesInfo objOrderLineInfo, bool addupsell)
        {
           return objOrderDetailsDal.AddProductToBasket(objDispatchesInfo, objOrderLineInfo, addupsell);
        
        }

        /// <summary>
        /// To get basket details
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<ProductInfo> GetBasketContents(string orderid)
        {
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            List<ProductInfo> tmplstProductItems = new List<ProductInfo>();
            try
            {
                tmplstProductItems = new OrderDetailsDAL().GetBasketContents(orderid);
                foreach (ProductInfo objProduct in tmplstProductItems)
                {
                    ProductInfo tmpProduct = new ProductInfo();
                    tmpProduct = objProduct;
                    tmpProduct.qtyPrice = objProduct.price * objProduct.quantity;
                    lstProductItems.Add(tmpProduct);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return lstProductItems;
        }

        /// <summary>
        /// To get delivery details
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DeliveryTimeInfo GetDeliveryDetails(string orderid)
        {
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            try
            {
                objDeliveryTimeInfo = new OrderDetailsDAL().GetDeliveryDetails(orderid);


            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

            return objDeliveryTimeInfo;
        }
        /// <summary>
        /// To get Basket details with priceXQuntity
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public List<ProductInfo> GetBasketContentsWithPrice(string orderid)
        {
            List<ProductInfo> lstProductItems = new List<ProductInfo>();
            List<ProductInfo> tmplstProductItems = new List<ProductInfo>();
            try
            {
                tmplstProductItems = new OrderDetailsDAL().GetBasketContents(orderid);
                foreach (ProductInfo objProduct in tmplstProductItems)
                {
                    ProductInfo tmpProduct = new ProductInfo();
                    tmpProduct = objProduct;
                    tmpProduct.qtyPrice = objProduct.price * objProduct.quantity;
                    lstProductItems.Add(tmpProduct);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

            return lstProductItems;
        }

        /// <summary>
        /// To delete Item from basket
        /// </summary>
        /// <param name="strOrderId"></param>
        /// <param name="intProductId"></param>
        public void DeleteBasketItem(string strOrderId, int intProductId)
        {
            try
            {
                new OrderDetailsDAL().DeleteBasketItem(strOrderId, intProductId);

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

        }

        /// <summary>
        /// To Update products in basket.
        /// </summary>
        /// <param name="strOrderId"></param>
        /// <param name="intProductId"></param>
        /// <param name="inQty"></param>
          public void UpdateBasketItem(string strOrderId, int intProductId, int inQty)
          {
            try
            {
                new OrderDetailsDAL().UpdateBasketItem(strOrderId, intProductId, inQty);

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

        }

          public DataTable GetMainProducts(string orderId)
          {
              DataTable dtproducts = new DataTable();
              try
              {
                  dtproducts = new OrderDetailsDAL().GetMainProducts(orderId);
              }
              catch (Exception ex)
              {
                  ErrorLog.Error(ex);
              }
              return dtproducts;
          }

          public int GetTotalQty(string orderId)
          {
              return new OrderDetailsDAL().GetTotalQty(orderId);
          }
          public DataTable GetOrderDetails(string orderId)
          {
              return new OrderDetailsDAL().GetOrderDetails(orderId);
          }
          public int GetTotalProductQty(string orderId)
          {
              return new OrderDetailsDAL().GetTotalProductQty(orderId);
          }

          public double GetOrderTotalByOrderID(string orderId)
          {
              return new OrderDetailsDAL().GetOrderTotalByOrderID(orderId);
          }
          public OrderTracking GetOrderTrackingInfo(string orderId)
          {
              OrderTracking objGetOrderTrackingInfo = new OrderTracking();
              try
              {
                  objGetOrderTrackingInfo = new OrderDetailsDAL().GetOrderTrackingInfo(orderId);


              }
              catch (Exception ex)
              {
                  ErrorLog.Error(ex);
              }

              return objGetOrderTrackingInfo;
          }

          public string CheckDeliveryCutoffByOrderID(string orderId)
          {
              return new OrderDetailsDAL().CheckDeliveryCutoffByOrderID(orderId);
          }
    }
}
