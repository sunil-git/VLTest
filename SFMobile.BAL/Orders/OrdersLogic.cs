/// <summary>
/// Author:Valuelabs
/// Date: 07/22/2011 11:25:14 AM
/// Class Name:OrdersLogic
/// Description:This class contains the business logic to  to manipulate the
/// data for order related information by using below methods.
/// <summary>

using System;
using System.Linq;
using System.Text;
using SFMobile.DAL;
using SFMobile.DTO;
using System.Data;
using System.Xml;
using SFMobile.DAL.Orders;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SFMobile.BAL.Orders
{
    public partial class OrdersLogic
    {
        OrdersData objOrderData = new OrdersData();


        /// <summary>
        /// Default Costructor for OrderLogic
        /// </summary>
        public OrdersLogic() { }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertAmendOrders()
        /// StoredProcedure Name:SFsp_InsertProductDetails
        /// Description: This method Contains business logic to insert/update the Order’s details in to database.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        /// <returns>string</returns>
        public string InsertAmendOrders(OrderDTO objOrderInfo)
        {
            return objOrderData.InsertAmendOrders(objOrderInfo);
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : UpdateOrderDetails()
        /// StoredProcedure Name:SFsp_InsertIntoProductBasket
        /// Description: This method Contains business logic to update the Order’s details in to database.
        /// </summary>
        /// <param name="objOrderInfo"></param>
        public void UpdateOrderDetails(OrderDTO objOrderInfo)
        {
            objOrderData.UpdateOrderDetails(objOrderInfo);
        }

       /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetOrderDetailsById()
        /// StoredProcedure Name:SFsp_GetOrderDetailsByOrderId
        /// Description: This method Contains logic to get the Order details  based on productid from database.
       /// </summary>
       /// <param name="orderId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetOrderDetailsById(string orderId)
        {
            return objOrderData.GetOrderDetailsById(orderId);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetConfirmationPriceByOrderID()
        /// StoredProcedure Name:SFsp_GetPriceDetails
        /// Description: This method Contains logic to get the price details  based on orderid from database.
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns>DataSet</returns>
        public DataSet GetConfirmationPriceByOrderID(string orderId)
        {
            return objOrderData.GetConfirmationPriceByOrderID(orderId);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetConfirmationDetailsByOrderID()
        /// StoredProcedure Name:SFsp_GetPriceDetails
        /// Description: This method Contains logic to get the all details for a order based on orderid from database.
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataSet GetConfirmationDetailsByOrderID(string orderId)
        {
            return objOrderData.GetConfirmationDetailsByOrderID(orderId);
        }
        public DataSet GetFloodtagDetailsByOrderID(string encryptionorderId)
        {
            return objOrderData.GetFloodtagDetailsByOrderID(encryptionorderId);
        }
        public DataSet GetFloodtagDetailsByOrderIDTemp(string orderId)
        {
            return objOrderData.GetFloodtagDetailsByOrderIDTemp(orderId);
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetTransactionDetailsById()
        /// StoredProcedure Name:SFsp_GetTransactionDetails
        /// Description: This method Contains logic to  get the Transaction details  based on orderid from database.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns>DataSet</returns>
        public DataSet GetTransactionDetailsById(string orderId)
        {
            return objOrderData.GetTransactionDetailsById(orderId);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GenerateOrdersXml()
        /// StoredProcedure Name:NA
        /// Description: This method Contains logic to generate xml string from dataset.
        /// </summary>
        /// <param name="dsResults"></param>
        /// <returns>string</returns>
        public string GenerateOrdersXml(DataSet dsResults)
        {
            XmlDocument xmlOrders = new XmlDocument();

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);

            writer.WriteStartDocument();
            writer.WriteComment("information regarding an order");
            writer.WriteComment("details of the order transaction");
            writer.WriteStartElement("request");
            writer.WriteElementString("requestType", "placeOrder");

            writer.WriteStartElement("transaction");

            //Transaction Details
            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                //Transaction Starts

                writer.WriteStartElement("orderRef");
                writer.WriteString(Convert.ToString(dr["orderRef"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("fulfillmentPartnerID");
                writer.WriteString(Convert.ToString(dr["fulfillmentPartnerID"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("customerID");
                writer.WriteString(Convert.ToString(dr["customerID"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("orderDate");
                writer.WriteString(Convert.ToDateTime(dr["orderDate"]).ToString("dd/MM/yyyy HH:mm:ss"));
                writer.WriteFullEndElement();


                writer.WriteStartElement("totalPrice");
                writer.WriteString(Convert.ToString(dr["totalPrice"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("totalPriceExchange");
                writer.WriteString(Convert.ToString(dr["totalPriceExchange"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("discount");
                writer.WriteString(Convert.ToString(dr["discount"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("discountExchange");
                writer.WriteString(Convert.ToString(dr["discountExchange"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("discountName");
                writer.WriteString(Convert.ToString(dr["discountName"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("currency");
                writer.WriteCData("&pound;" + Convert.ToString(dr["currency"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("currencyID");
                writer.WriteString(Convert.ToString(dr["currencyID"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("email");
                writer.WriteString(Convert.ToString(dr["email"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("telephone");
                writer.WriteString(Convert.ToString(dr["telephone"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("mobile");
                writer.WriteAttributeString("", "smsAlerts", null, Convert.ToString(dr["smsAlerts"]));
                writer.WriteValue(Convert.ToString(dr["mobile"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("fax");
                writer.WriteString(Convert.ToString(dr["fax"]));
                writer.WriteFullEndElement();
            }
            if (dsResults.Tables[1].Rows.Count > 0)
            {
                //Address
                DataRow dr = dsResults.Tables[1].Rows[0];
                writer.WriteStartElement("address");
                writer.WriteAttributeString("", "type", null, "invoice");
                writer.WriteAttributeString("", "paf", null, "1");

                writer.WriteStartElement("title");
                writer.WriteCData(Convert.ToString(dr["title"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("firstname");
                writer.WriteCData(Convert.ToString(dr["firstname"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("lastname");
                writer.WriteCData(Convert.ToString(dr["lastname"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("organisation");
                writer.WriteCData(Convert.ToString(dr["organisation"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address1");
                writer.WriteCData(Convert.ToString(dr["address1"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address2");
                writer.WriteCData(Convert.ToString(dr["address2"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address3");
                writer.WriteCData(Convert.ToString(dr["address3"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("town");
                writer.WriteCData(Convert.ToString(dr["town"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("county");
                writer.WriteCData(Convert.ToString(dr["county"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("postcode");
                writer.WriteString(Convert.ToString(dr["postcode"]));
                writer.WriteFullEndElement();

                writer.WriteFullEndElement();
            }
            if (dsResults.Tables[2].Rows.Count > 0)
            {
                //Address
                DataRow dr = dsResults.Tables[2].Rows[0];
                writer.WriteStartElement("address");
                writer.WriteAttributeString("", "type", null, "delivery");
                writer.WriteAttributeString("", "paf", null, "1");

                writer.WriteStartElement("title");
                writer.WriteCData(Convert.ToString(dr["title"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("firstname");
                writer.WriteCData(Convert.ToString(dr["firstname"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("lastname");
                writer.WriteCData(Convert.ToString(dr["lastname"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("organisation");
                writer.WriteCData(Convert.ToString(dr["organisation"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address1");
                writer.WriteCData(Convert.ToString(dr["address1"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address2");
                writer.WriteCData(Convert.ToString(dr["address2"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("address3");
                writer.WriteCData(Convert.ToString(dr["address3"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("town");
                writer.WriteCData(Convert.ToString(dr["town"]));
                writer.WriteFullEndElement();

                writer.WriteStartElement("county");
                writer.WriteCData(Convert.ToString(dr["county"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("postcode");
                writer.WriteString(Convert.ToString(dr["postcode"]));
                writer.WriteFullEndElement();

                writer.WriteFullEndElement();
            }

            //Delivery Info
            if (dsResults.Tables[3].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[3].Rows[0];
                writer.WriteStartElement("deliveryInfo");

                writer.WriteStartElement("deliveryDate");
                writer.WriteString(Convert.ToDateTime(dr["deliveryDate"]).ToString("dd/MM/yyyy"));
                writer.WriteFullEndElement();

                writer.WriteStartElement("deliveryOption");
                writer.WriteAttributeString("", "id", null, Convert.ToString(dr["deliveryOptionId"]));
                writer.WriteString(Convert.ToString(dr["deliveryOption"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("deliveryPrice");
                writer.WriteString(Convert.ToString(dr["deliveryPrice"]));
                writer.WriteFullEndElement();


                writer.WriteStartElement("delDays");
                writer.WriteString(Convert.ToString(dr["delDays"]));
                writer.WriteFullEndElement();

                writer.WriteFullEndElement();
            }

            //Delivery Info
            if (dsResults.Tables[4].Rows.Count > 0)
            {
                writer.WriteStartElement("basket");

                foreach (DataRow dr in dsResults.Tables[4].Rows)
                {
                    writer.WriteStartElement("product");

                    writer.WriteStartElement("id");
                    writer.WriteString(Convert.ToString(dr["id"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("itemID");
                    writer.WriteString(Convert.ToString(dr["itemID"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("name");
                    writer.WriteCData(Convert.ToString(dr["name"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("quantity");
                    writer.WriteString(Convert.ToString(dr["quantity"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("itemPrice");
                    writer.WriteString(Convert.ToString(dr["itemPrice"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("linePrice");
                    writer.WriteString(Convert.ToString(dr["linePrice"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("itemPriceExchange");
                    writer.WriteString(Convert.ToString(dr["itemPriceExchange"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("linePriceExchange");
                    writer.WriteString(Convert.ToString(dr["linePriceExchange"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("smallURL");
                    writer.WriteString(Convert.ToString(dr["smallURL"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("bigURL");
                    writer.WriteString(Convert.ToString(dr["bigURL"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("delDate");
                    writer.WriteString(Convert.ToDateTime(dr["delDate"]).ToString("dd/MM/yyyy"));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("delOptionName");
                    writer.WriteString(Convert.ToString(dr["delOptionName"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("delOptionCost");
                    writer.WriteString(Convert.ToString(dr["delOptionCost"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("delOptionCostExchange");
                    writer.WriteString(Convert.ToString(dr["delOptionCostExchange"]));
                    writer.WriteFullEndElement();

                    writer.WriteStartElement("fpID");
                    writer.WriteString(Convert.ToString(dr["fpID"]));
                    writer.WriteFullEndElement();

                    //Closing the product Element
                    writer.WriteFullEndElement();
                }

                //Closing the basket Element
                writer.WriteFullEndElement();
            }

            //Closing the transaction Element
            writer.WriteEndElement();

            //Closing the request Element
            writer.WriteEndElement();
            writer.Flush();


            string strXmlOrders = sb.ToString().Replace("utf-16", "ISO-8859-1").Replace("utf-8", "ISO-8859-1");

            return strXmlOrders;
        }
        public int GetQuantityByOrderIdProductId(string orderId, int productId)
        {
            return objOrderData.GetQuantityByOrderIdProductId(orderId, productId);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : GetContactUsDetailsByGuid()
        /// StoredProcedure Name:SFsp_GetContactUsDetailsByGUID
        /// Description: This method Contains logic to  execute stored procedure "SFsp_GetContactUsDetailsByGUID"
        /// to get the Email communication details  based on guid from database.
        /// </summary>
        /// <param name="strGuid">strGuid</param>
        /// <returns>DataSet</returns>
        public DataSet GetContactUsDetailsByGuid(string strGuid)
        {
            return objOrderData.GetContactUsDetailsByGuid(strGuid);
        }

        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertContactPageInfo()
        /// StoredProcedure Name:SFsp_InsertContactInfo
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertContactInfo"
        /// to add new message details in T_ContactUs table.
        /// </summary>
        /// <param name="objTicketInfo"></param>
        /// <returns>int</returns>
        public int InsertContactPageInfo(EmailTicketInfo objTicketInfo)
        {
            return objOrderData.InsertContactPageInfo(objTicketInfo);
        }
        /// <summary>
        /// Author :Valuelabs
        /// Method Name : InsertTicketInfo()
        /// StoredProcedure Name:SFsp_InsertContactTicket
        /// Description: This method Contains logic to  execute stored procedure "SFsp_InsertContactTicket"
        /// to add new message details in T_ContactUs table.
        /// </summary>
        /// <param name="objTicketInfo"></param>
        /// <returns>int</returns>
        public int InsertTicketInfo(string orderId,int statusId)
        {
            return objOrderData.InsertTicketInfo(orderId,statusId);
        }
        public int GetQuantityByOrderId(string orderId)
        {
            return objOrderData.GetQuantityByOrderId(orderId);
        }

        public int GetCardMessageLength(string orderId)
        {
            return objOrderData.GetCardMessageLength(orderId);
        }

        public OrderDTO CheckMultiFPProducts(OrderDTO objOrder)
        {
            return objOrderData.CheckMultiFPProducts(objOrder);
        }
      
        public DataSet GetReOrderVoucherDetails(string orderId, int voucherId)
        {
            return objOrderData.GetReOrderVoucherDetails(orderId, voucherId);
        }

        public DataTable GetOrderDetails(string orderId)
        {
            return objOrderData.GetOrderDetails(orderId);
        }
        public string GetGUIId()
        {
            return objOrderData.GetGUIId();
        }
    }
}
