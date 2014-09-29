/// <summary>
/// <summary>
/// Author:Valuelabs
/// Date: 07/26/2011 12:34:14 PM
/// Class Name:CheckoutLogic
/// Description:This class contains the bisiness logic manipulate the
/// data for checkout related information by using below methods.
/// <summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFMobile.DTO;
using SFMobile.DAL;
using SFMobile.DAL.Checkout;
using System.Data;

namespace SFMobile.BAL.Checkout
{
    public partial class CheckoutLogic
    {
        CheckoutData objCheckoutData = new CheckoutData();        

        /// <summary>
        /// Default CheckoutLogic Constructor
        /// </summary>
        public CheckoutLogic()
        {
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertUpdateRecipientDetails()
        /// StoredProcedure Name:SFsp_InsertRecipientDetails
        /// Description: This method Contains logic to insert/update the recipient’s details in to database.
        /// </summary>
        /// <param name="objCustomerInfo"></param>
        /// <returns>int</returns>
        public int InsertUpdateRecipientDetails(CustomerInfo objCustomerInfo)
        {
            return objCheckoutData.InsertUpdateRecipientDetails(objCustomerInfo);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertCartItemDetails()
        /// StoredProcedure Name:SFsp_InsertIntoProductBasket
        /// Description: This method Contains logic to insert/update the cart details in to database.
        /// </summary>
        /// <param name="objCartInfo"></param>
        /// <returns>int</returns>
       public int InsertCartItemDetails(CartInfo objCartInfo)
        {
           return objCheckoutData.InsertCartItemDetails(objCartInfo);
        }

        /// <summary>
       /// Author :Valuelabs
       /// Method Name : GetBillingDetails()
       /// StoredProcedure Name:SFsp_GetSameAsDeliveryDetails
       /// Description: This method Contains logic to get the billing information  from database.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public CustomerInfo GetBillingDetails(string orderId)
        {

            return objCheckoutData.GetBillingDetails(orderId);
        
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : RetainedBillingDetails()
        /// StoredProcedure Name:SFsp_GetBillingDetails
        /// Description: This method Contains logic to get the billing information  from database.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public CustomerInfo RetainedBillingDetails(string orderId)
        {

            return objCheckoutData.RetainedBillingDetails(orderId);

        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetExtrasByProdId()
        /// StoredProcedure Name:Sfsp_GetGiftProducts
        /// Description: This method Contains logic to get the gift products from database.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetExtrasByProdId(string orderId,int productId)
        {
            return objCheckoutData.GetExtrasByProdId(orderId,productId);
        }

         /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertUpdateBillingDetails()
        /// StoredProcedure Name:SFsp_InsertBillingDetails
        /// Description: This method Contains logic to insert/update the billing details in to database.
        /// </summary>
        /// <param name="objCustomerInfo"></param>
        /// <returns>int</returns>
        public int InsertUpdateBillingDetails(CustomerInfo objCustomerInfo)
        {
            return objCheckoutData.InsertUpdateBillingDetails(objCustomerInfo);
        }
        public void UpdateEncryptedOrderId(string orderId, string encryptedOrderId)
        {
             objCheckoutData.UpdateEncryptedOrderId(orderId, encryptedOrderId);
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : PostcodeValid()
        /// StoredProcedure Name:SFsp_CheckValidPostCode
        /// Description: This method Contains logic to get the gift products from database.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns>DataSet</returns>
        public DataSet PostcodeValid(string postCode, string productId)
        {
            return objCheckoutData.PostcodeValid(postCode, productId);
        }
    }
}
