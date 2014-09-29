#region About the Class
/// <summary>
/// Author: Valuelabs
/// Last Modified Date Time: 12/18/2012 2:44:14 PM
/// Class Name: SerenataCheckoutLogic
/// Description: This class contains the business logic to manipulate the data for new order schema related information by using below methods.
/// <summary>
#endregion

#region Import Section
using System;
using System.Linq;
using System.Text;
using SerenataOrderSchema;
using SerenataOrderSchema.Checkout;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections.Generic;
using SFMobile.DTO;
#endregion

namespace SerenataOrderSchemaBAL
{
    public class SerenataCheckoutLogic
    {
        SerenataCheckout objCheckoutData = new SerenataCheckout();

        #region 1. To create an order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: CreateOrder()
        /// StoredProcedure Name: SFsp_CreateOrder
        /// Description: This method contains logic to execute stored procedure "SFsp_CreateOrder" to save/update the order details into [Orders] table.
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>string</returns>
        public string CreateOrder(OrderDTO objOrder)
        {
            return objCheckoutData.CreateOrder(objOrder);
        }
        #endregion

        #region 2. To add product details into the order.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: AddProductLine()
        /// StoredProcedure Name: Sfsp_AddProductLine
        /// Description: This method contains logic to  execute stored procedure "Sfsp_AddProductLine" to save/update the Order details into [OrderLines] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void AddProductLine(OrderDTO objOrder)
        {
            objCheckoutData.AddProductLine(objOrder);
        }
        #endregion

        #region 3. To schedule the dispatch.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ScheduleDispatch()
        /// StoredProcedure Name: Sfsp_RescheduleDispatch
        /// Description: This method contains logic to execute stored procedure "Sfsp_RescheduleDispatch" to save/update the dispatch details into [Dispatches] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void ScheduleDispatch(OrderDTO objOrder)
        {
            objCheckoutData.ScheduleDispatch(objOrder);
        }
        #endregion

        #region 4. To Update Product Quantity
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ScheduleDispatch()
        /// StoredProcedure Name: Sfsp_RescheduleDispatch
        /// Description: This method contains logic to execute stored procedure "Sfsp_RescheduleDispatch" to save/update the dispatch details into [Dispatches] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void UpdateProductQuantity(OrderDTO objOrderNew)
        {
            objCheckoutData.UpdateProductQuantity(objOrderNew);
        }
        #endregion

        #region 5. To Delete product from basket
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ScheduleDispatch()
        /// StoredProcedure Name: Sfsp_RescheduleDispatch
        /// Description: This method contains logic to execute stored procedure "Sfsp_RescheduleDispatch" to save/update the dispatch details into [Dispatches] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void DeleteProductFromBasket(OrderDTO objOrderNew)
        {
            objCheckoutData.DeleteProductFromBasket(objOrderNew);
        }
        #endregion

        #region 6. To Edit Dispatches Address
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: EditDispatchAddress()
        /// StoredProcedure Name: Sfsp_EditDispatchAddress
        /// Description: This method contains logic to execute stored procedure "Sfsp_EditDispatchAddress" to save/update the dispatch adddress details into [DeliveryAddress] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void EditDispatchAddress(CustomerInfo objCustomerInfoNew)
        {
            objCheckoutData.EditDispatchAddress(objCustomerInfoNew);
        }
        #endregion

        #region 7. To Edit Delivery Instructions
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: EditDeliveryInstructions()
        /// StoredProcedure Name: SFsp_EditDeliveryInstructions
        /// Description: This method contains logic to execute stored procedure "SFsp_EditDeliveryInstructions" to save/update the Delivery instructions details into [DeliveryAddress] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void EditDeliveryInstructions(CustomerInfo objCustomerInfoNew)
        {
            objCheckoutData.EditDeliveryInstructions(objCustomerInfoNew);
        }
        #endregion

        #region 8. To Edit Gift Message
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: EditCardMessage()
        /// StoredProcedure Name: SFsp_EditCardMessage
        /// Description: This method contains logic to execute stored procedure "SFsp_EditCardMessage" to save/update the gift message details into [DeliveryAddress] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public void EditCardMessage(CustomerInfo objCustomerInfoNew)
        {
            objCheckoutData.EditCardMessage(objCustomerInfoNew);
        }
        #endregion
        
        #region 9. To update customer or billing details
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : EditCustomerDetails()
        /// StoredProcedure Name:SFsp_EditCustomer
        /// Description: This method Contains logic to  execute stored procedure "SFsp_EditCustomer" to save/update the billing details into [Customers].
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <returns>int</returns>
        /// <remarks></remarks>
        public void EditCustomerDetails(CustomerInfo objCustomerInfo)
        {
            objCheckoutData.EditCustomerDetails(objCustomerInfo);
        }
        #endregion

        #region 10. To update VoucherCode
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : UpdateVoucherCode()
        /// StoredProcedure Name:SFsp_EditVoucherCode
        /// Description: This method Contains logic to  execute stored procedure "SFsp_EditVoucherCode" to save/update the valid voucher code into [Orders].
        /// </summary>
        /// <param name="objCustomerInfo">The obj customer info.</param>
        /// <remarks></remarks>
        public void EditVoucherCode(OrderDTO objOrderInfo)
        {
            objCheckoutData.EditVoucherCode(objOrderInfo);
        }
        #endregion

        #region 11. To update the encrypted order id
        /// <summary>
        /// Author:Valuelabs
        /// Method Name: UpdateEncryptedOrderId()
        /// StoredProcedure Name: SFsp_UpdateOrderWithEncryptedOrderID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_UpdateOrderWithEncryptedOrderID" to update the encrypted order id into [Orders].
        /// </summary>
        /// <param name="objCustomerInfo">The obj of order info.</param>
        /// <remarks></remarks>
        public void UpdateEncryptedOrderId(OrderDTO objOrderInfo)
        {
            objCheckoutData.UpdateEncryptedOrderId(objOrderInfo);
        }
        #endregion

        #region 12. Get customer id against an order id
        /// <summary>
        /// Get customer id against an order id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public int GetCustomerIdByOrderId(string OrderId)
        {
            return objCheckoutData.GetCustomerIdByOrderId(OrderId);
        }
        #endregion

        #region 13. Get customer id against an order id
        /// <summary>
        /// Get browser country name against an user ip address.
        /// </summary>
        /// <param name="UserIp"></param>
        /// <returns>string</returns>
        public string GetBrowserCountry(string UserIp)
        {
            return objCheckoutData.GetBrowserCountry(UserIp);
        }
        #endregion

        #region 14. To Accept the address for verified or not
        /// <summary>
        /// Get browser country name against an user ip address.
        /// </summary>
        /// <param name="UserIp"></param>
        /// <returns>string</returns>
        public void AcceptAddress(CustomerInfo objCustomerInfoNew)
        {
            objCheckoutData.AcceptAddress(objCustomerInfoNew);
        }
        #endregion

        #region 15. Validate voucher code
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ValidateVoucherCode()
        /// StoredProcedure Name: SFsp_ValidateVoucher
        /// Description: This method contains logic to execute stored procedure "SFsp_ValidateVoucher" to validate voucher code and update voucher id,discount and total  in [Orders] table.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns></returns>
        public OrderDTO ValidateVoucherCode(OrderDTO objOrderInfo)
        {
            return objCheckoutData.ValidateVoucherCode(objOrderInfo);
        }
        #endregion

        #region 16. Update Order Complete
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOrderComplete()
        /// StoredProcedure Name: SFsp_UpdateOrderComplete
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOrderComplete" to update order complete status in [Orders] table.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns>void</returns>
        public void UpdateOrderComplete(string OrderId)
        {
            objCheckoutData.UpdateOrderComplete(OrderId);
        }
        #endregion

        #region 17. Update Order Status for Trial Pay
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOrderStatusForTrialPay()
        /// StoredProcedure Name: SFsp_UpdateOrderStatusForTrailPay
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOrderStatusForTrailPay" to update order status for trail pay voucher title in [Orders] table.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>void</returns>
        public void UpdateOrderStatusForTrialPay(string OrderId)
        {
            objCheckoutData.UpdateOrderStatusForTrialPay(OrderId);
        }
        #endregion

        #region 18. To get product price details.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetProductPriceDetails()
        /// StoredProcedure Name: SFsp_GetProductPriceDetails
        /// Description: This method contains logic to  execute stored procedure "SFsp_GetProductPriceDetails" to get product price details from [T_ProductData] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public OrderDTO GetProductPriceDetails(OrderDTO objOrder)
        {
            return objCheckoutData.GetProductPriceDetails(objOrder);
        }
        #endregion

        #region 19. To get delivery details by using delivery option ID.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetDeliveryDetailsByDelOptionID()
        /// StoredProcedure Name: SFsp_GetDeliveryDetailsByDelOptionID
        /// Description: This method contains logic to execute stored procedure "SFsp_GetDeliveryDetailsByDelOptionID" to get delivery details from [T_Delivery_Option] table.
        /// </summary>
        /// <param name="objOrder"></param>
        public OrderDTO GetDeliveryDetailsByDelOptionID(OrderDTO objOrder)
        {
            return objCheckoutData.GetDeliveryDetailsByDelOptionID(objOrder);
        }
        #endregion

        #region 20. To get delivery details by using delivery option ID.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: SFsp_GetUpsaleCountInBasket()
        /// StoredProcedure Name: SFsp_GetUpsaleCountInBasket
        /// Description: This method contains logic to execute stored procedure "SFsp_GetUpsaleCountInBasket" to get upsale count.
        /// </summary>
        /// <param name="objOrder"></param>
        public int GetUpsaleCount(string orderid)
        {
            return objCheckoutData.GetUpsaleCount(orderid);
        }
        #endregion

        #region 21. To get VoucherID by using VoucherCode.
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: GetVoucherIDByVoucherCode()
        /// StoredProcedure Name: SFsp_GetVoucherIDByVoucherCode
        /// Description: This method contains logic to execute stored procedure "SFsp_GetVoucherIDByVoucherCode" to get VoucherID by using VoucherCode.
        /// </summary>
        /// <param name="voucherCode"></param>
        /// <returns>int</returns>
        public int GetVoucherIDByVoucherCode(string voucherCode)
        {
            return objCheckoutData.GetVoucherIDByVoucherCode(voucherCode);
        }
        #endregion

        #region 22. Update Occasion and funeral time for a given order
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: UpdateOccasionAndFuneralTime()
        /// StoredProcedure Name: SFsp_UpdateOccasionAndFuneralTime
        /// Description: This method contains logic to execute stored procedure "SFsp_UpdateOccasionAndFuneralTime" to update Occasion and funeral time for a given order in [Dispatches] and [Orders] table.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>void</returns>
        public void UpdateOccasionAndFuneralTime(string OrderId)
        {
            objCheckoutData.UpdateOccasionAndFuneralTime(OrderId);
        }
        #endregion

        #region 23. Validate voucher code for existing order schema transactions
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ValidateVoucherCodeForExistingOrder()
        /// StoredProcedure Name: SFsp_ValidateVoucher
        /// Description: This method contains logic to execute stored procedure "SFsp_ValidateVoucher" to validate voucher code and update voucher id,discount and total  in [T_Orders] table.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns>int</returns>
        public int ValidateVoucherCodeForExistingOrder(OrderDTO objOrderInfo)
        {
            return objCheckoutData.ValidateVoucherCodeForExistingOrder(objOrderInfo);
        }
        #endregion
    }
}
