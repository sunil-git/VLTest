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
    public class RecipientDetailsBAL
    {
        RecipientDetailsDAL objRecipientDetailsDAL = new RecipientDetailsDAL();

        public List<AddressInfo> GetCustomerAddressBook(int CustomerID)
        {
            List<AddressInfo> objAddressListInfo = new List<AddressInfo>();
            List<RecipientInfo> objRecipientList = new List<RecipientInfo>();
            try
            {
                objRecipientList = objRecipientDetailsDAL.GetCustomerAddressBook(CustomerID);
                if (objRecipientList != null)
                {
                    foreach (RecipientInfo RecipientInfo in objRecipientList)
                    {
                        AddressInfo objAddressInfo = new AddressInfo();
                        System.Text.StringBuilder FullName = new System.Text.StringBuilder();
                        if(!string.IsNullOrEmpty(RecipientInfo.Title))
                        {
                            FullName.Append(RecipientInfo.Title);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.FirstName))
                        {
                            FullName.Append(" " + RecipientInfo.FirstName);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.LastName))
                        {
                            FullName.Append(" " + RecipientInfo.LastName);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.HouseNo))
                        {
                            FullName.Append(" : " + RecipientInfo.HouseNo);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.Street))
                        {
                            FullName.Append(" " + RecipientInfo.Street);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.PostCode))
                        {
                            FullName.Append(" " + RecipientInfo.PostCode);
                        }
                        if (!string.IsNullOrEmpty(RecipientInfo.Town))
                        {
                            FullName.Append(" " + RecipientInfo.Town);
                        }
                        //string FulName = RecipientInfo.Title + " " + RecipientInfo.FirstName + " " + RecipientInfo.LastName + " " + ":" + RecipientInfo.HouseNo + " " + RecipientInfo.Street + " " + RecipientInfo.PostCode + " " + RecipientInfo.Town;
                        objAddressInfo.FullName = FullName.ToString();
                        objAddressInfo.AddressID = RecipientInfo.DeliveryAddressID;
                        objAddressListInfo.Add(objAddressInfo);
                        
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLog.Error(ex); 
            }


            return objAddressListInfo;
        
        }


        public RecipientInfo GetDeliveryDetails(string strOrderId)
        {
            return new RecipientDetailsDAL().GetDeliveryDetails(strOrderId);
        }

        public void EditDeliveryAddress(RecipientInfo objRecInfo)
        {
            new RecipientDetailsDAL().EditDeliveryAddress(objRecInfo);
        }
        public int GetCustomerIdByOrderId(string OrderId)
        {
            return new RecipientDetailsDAL().GetCustomerIdByOrderId(OrderId);
        }
        public DataSet GetOccasionByOrderID(string OrderId)
        {
            return new RecipientDetailsDAL().GetOccasionByOrderID(OrderId);
        }


        public List<OccassionCard> GetCardProducts(string orderID, int occasionID, ref int refSelectedProductId)
        {
            return new RecipientDetailsDAL().GetCardProducts(orderID, occasionID, ref refSelectedProductId);
        }

        public void AddMessageCard(string orderId, int productId, decimal price, int noCard)
        {
            new RecipientDetailsDAL().AddMessageCard(orderId, productId, price, noCard);
        }

        public DataSet CheckNonDelPostCode(string Postcode, string OrderId)
        {
            return new RecipientDetailsDAL().CheckNonDelPostCode(Postcode,OrderId);
        }
        public RecipientInfo GetCustomerAddressBookbyAddressID(int DeliveryAddressID)
        {

            return new RecipientDetailsDAL().GetCustomerAddressBookbyAddressID(DeliveryAddressID);
        }
        public void EditMessageCard(string OrderId, string Message)
        {
            new RecipientDetailsDAL().EditMessageCard(OrderId, Message);
        }
        public void EditDeliveryInstructions(string OrderId, string DeliveryInstructions)
        {
            new RecipientDetailsDAL().EditDeliveryInstructions(OrderId, DeliveryInstructions);
        }
        public int GetCardMessageLength(string orderId)
        {
            return new RecipientDetailsDAL().GetCardMessageLength(orderId);
        }

        public int GetSelectedMessageCardIDByOrderID(string orderID)
        {
            return new RecipientDetailsDAL().GetSelectedMessageCardIDByOrderID(orderID);
        }
        public DataSet GetProductCountryByOrderId(string orderID)
        {
            return new RecipientDetailsDAL().GetProductCountryByOrderId(orderID);
        }
        public string GetCardMessage(string orderId)
        {
            return new RecipientDetailsDAL().GetCardMessage(orderId);
        }
        public string GetDeliveryInstructions(string orderId)
        {
            return new RecipientDetailsDAL().GetDeliveryInstructions(orderId);
        }
        public void UpdateOccasion(string strOrderid, int strOccasionId)
        {
            new RecipientDetailsDAL().UpdateOccasion(strOrderid, strOccasionId);
        }
    }
}
