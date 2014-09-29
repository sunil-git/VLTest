/// <summary>
/// CartInfoClass for Serena Product
/// Date: 07/30/2011 11:25:14 AM
/// Author: Auto Generated.
/// <summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFMobile.DTO
{
    /// <summary>
    /// This class is used to transfer the data from UI layer to BAl layer
    /// </summary>
   public class CartInfo
   {
      #region Private variables
      private string actionType;
      private string orderId;
      private int productId;
      private DateTime deliveryDate;
      private string deliveryTime;
      private int quantity;
      private string imageSmallLow;
      private string productTitle;
      private float price;
      private float orginalPrice;
      private float offerPrice;
      private float totalPrice;
      private int deliveryOptionId;
      private string deliveryOptionName;


      #endregion

      #region Property  ActionType
      public string ActionType
      {
         get { return actionType; }
         set { actionType = value; }
      }
      #endregion

      #region Property ProductId
      public int ProductId
      {
         get { return productId; }
         set { productId = value; }
      }
      #endregion

      #region Property OrderId
      public string OrderId
      {
         get { return orderId; }
         set { orderId = value; }
      }
      #endregion

      #region Property DeliveryDate
      public DateTime DeliveryDate
      {
         get { return deliveryDate; }
         set { deliveryDate = value; }
      }
      #endregion

      #region Property  DeliveryTime
      public string DeliveryTime
      {
         get { return deliveryTime; }
         set { deliveryTime = value; }
      }
      #endregion

      #region Property Quantity
      public int Quantity
      {
         get { return quantity; }
         set { quantity = value; }
      }
      #endregion

      #region Property ImageSmallLow
      public string ImageSmallLow
      {
         get { return imageSmallLow; }
         set { imageSmallLow = value; }
      }
      #endregion

      #region Property  ProductTitle
      public string ProductTitle
      {
         get { return productTitle; }
         set { productTitle = value; }
      }
      #endregion

      #region Property  Price
      public float Price
      {
         get { return price; }
         set { price = value; }
      }
      #endregion

      #region Property  OfferPrice
      public float OfferPrice
      {
         get { return offerPrice; }
         set { offerPrice = value; }
      }
      #endregion

      #region Property  OrginalPrice
      public float OrginalPrice
      {
         get { return orginalPrice; }
         set { orginalPrice = value; }
      }
      #endregion

      #region Property  TotalPrice
      public float TotalPrice
      {
         get { return totalPrice; }
         set { totalPrice = value; }
      }
      #endregion



      #region Property DeliveryOptionId
      public int DeliveryOptionId
      {
         get { return deliveryOptionId; }
         set { deliveryOptionId = value; }
      }
      #endregion

      #region Property DeliveryOptionName
      public string DeliveryOptionName
      {
         get { return deliveryOptionName; }
         set { deliveryOptionName = value; }
      }
      #endregion

      /// <summary>
      /// Default Consructor for CartInfo
      /// </summary>
      public CartInfo()
      { }
   }
}
