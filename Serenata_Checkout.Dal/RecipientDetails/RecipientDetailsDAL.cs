using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Serenata_Checkout.Log;
using Serenata_Checkout.Dto;
using System.Configuration;

using Serenata_Checkout.Dal.Common;

namespace Serenata_Checkout.Dal
{
    public class RecipientDetailsDAL
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

        public List<RecipientInfo> GetCustomerAddressBook(int CustomerID)
        {
            List<RecipientInfo> objAddressList = new List<RecipientInfo>();
            RecipientInfo objAddressInfo = new RecipientInfo();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@CustomerID", CustomerID);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCustomerAddressBook", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    objAddressList.Add(FillRecipientInfo(reader));
                }

                return objAddressList;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objAddressList;
        }

        private RecipientInfo FillRecipientInfo(IDataRecord myDataRecord)
        {
            RecipientInfo objRecipientInfo = new RecipientInfo();
            try
            {

                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DeliveryAddressID")))
                {
                    objRecipientInfo.DeliveryAddressID= myDataRecord.GetInt32(myDataRecord.GetOrdinal("DeliveryAddressID"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Title")))
                {
                    objRecipientInfo.Title = myDataRecord.GetString(myDataRecord.GetOrdinal("Title"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FirstName")))
                {
                    objRecipientInfo.FirstName = myDataRecord.GetString(myDataRecord.GetOrdinal("FirstName"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LastName")))
                {
                    objRecipientInfo.LastName = myDataRecord.GetString(myDataRecord.GetOrdinal("LastName"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address1")))
                {
                    objRecipientInfo.HouseNo = myDataRecord.GetString(myDataRecord.GetOrdinal("Address1"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address2")))
                {
                    objRecipientInfo.Street = myDataRecord.GetString(myDataRecord.GetOrdinal("Address2"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address3")))
                {
                    objRecipientInfo.District = myDataRecord.GetString(myDataRecord.GetOrdinal("Address3"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Organisation")))
                {
                    objRecipientInfo.Organisation = myDataRecord.GetString(myDataRecord.GetOrdinal("Organisation"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Mobile")))
                {
                    objRecipientInfo.RecipientMobile = myDataRecord.GetString(myDataRecord.GetOrdinal("Mobile"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PostCode")))
                {
                    objRecipientInfo.PostCode = myDataRecord.GetString(myDataRecord.GetOrdinal("PostCode"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("County")))
                {
                    objRecipientInfo.County = myDataRecord.GetString(myDataRecord.GetOrdinal("County"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CountryID")))
                {
                    objRecipientInfo.CountryID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("CountryID"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Town")))
                {
                    objRecipientInfo.Town = myDataRecord.GetString(myDataRecord.GetOrdinal("Town"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Addr_Verified")))
                {
                    objRecipientInfo.AddressVerified = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Addr_Verified"));
                }
                return objRecipientInfo;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return objRecipientInfo;

        }




        public RecipientInfo GetDeliveryDetails(string strOrderId)
        {
            RecipientInfo objRecipient = new RecipientInfo(); 
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryDetails", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    objRecipient = FillDeliveryDetails(reader);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objRecipient;
        }

        private RecipientInfo FillDeliveryDetails(IDataRecord myDataRecord)
        {
            RecipientInfo objRecipientInfo = new RecipientInfo();
            try
            {
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("orderDate")))
                {
                    objRecipientInfo.OrderDate = Convert.ToDateTime(myDataRecord["orderDate"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Title")))
                {
                    objRecipientInfo.Title = Convert.ToString(myDataRecord["Title"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FirstName")))
                {
                    objRecipientInfo.FirstName = Convert.ToString(myDataRecord["FirstName"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LastName")))
                {
                    objRecipientInfo.LastName = Convert.ToString(myDataRecord["LastName"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address1")))
                {
                    objRecipientInfo.HouseNo = Convert.ToString(myDataRecord["Address1"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address2")))
                {
                    objRecipientInfo.Street = Convert.ToString(myDataRecord["Address2"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address3")))
                {
                    objRecipientInfo.District = Convert.ToString(myDataRecord["Address3"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Organisation")))
                {
                    objRecipientInfo.Organisation = Convert.ToString(myDataRecord["Organisation"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Mobile")))
                {
                    objRecipientInfo.RecipientMobile = Convert.ToString(myDataRecord["Mobile"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PostCode")))
                {
                    objRecipientInfo.PostCode = Convert.ToString(myDataRecord["PostCode"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("County")))
                {
                    objRecipientInfo.County = Convert.ToString(myDataRecord["County"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CountryID")))
                {
                    objRecipientInfo.CountryID = Convert.ToInt32(myDataRecord["CountryID"]);
                    CommonDal obj = new CommonDal();
                    objRecipientInfo.CountryName = obj.GetCountryNameByCountryCode(objRecipientInfo.CountryID);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Town")))
                {
                    objRecipientInfo.Town = Convert.ToString(myDataRecord["Town"]);
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Addr_Verified")))
                {
                    objRecipientInfo.AddressVerified = Convert.ToInt32(myDataRecord["Addr_Verified"]);
                }

                return objRecipientInfo;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return objRecipientInfo;

        }


        public void EditDeliveryAddress(RecipientInfo objRecInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objRecInfo.OrderId);
                dbManager.AddParameter("@Title", objRecInfo.Title);
                dbManager.AddParameter("@FirstName", objRecInfo.FirstName);
                dbManager.AddParameter("@LastName", objRecInfo.LastName);
                dbManager.AddParameter("@Organisation", objRecInfo.Organisation);
                dbManager.AddParameter("@Address1", objRecInfo.HouseNo);
                dbManager.AddParameter("@Address2", objRecInfo.Street);
                dbManager.AddParameter("@Address3", objRecInfo.District);
                dbManager.AddParameter("@Town", objRecInfo.Town);
                dbManager.AddParameter("@Mobile", objRecInfo.RecipientMobile);
                dbManager.AddParameter("@Postcode", objRecInfo.PostCode);
                dbManager.AddParameter("@County", objRecInfo.County);
                dbManager.AddParameter("@CountryID", objRecInfo.CountryID);
                dbManager.AddParameter("@Addr_Verified", objRecInfo.AddressVerified);
                dbManager.ExecuteScalar("SFsp_EditDispatchAddress", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }

        public int GetCustomerIdByOrderId(string OrderId)
        {
            int CustomerId = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                CustomerId = (int)dbManager.ExecuteScalar("SFsp_GetCustomerIDByOrderID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return CustomerId;
        }

        public DataSet GetOccasionByOrderID(string OrderId)
        {
            DataSet dsOccasions = new DataSet();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dsOccasions = dbManager.ExecuteDataSet("SFSP_GetOccasionByOrderID", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return dsOccasions;

        }

        //public List<OccassionCard> GetCardProducts(string orderID, int noOfRows, int languageId, int productsetId, ref int refSelectedProductId)
        //{
        //    List<OccassionCard> lstOccassionCards = new List<OccassionCard>();
        //    try
        //    {
        //        dbManager.OpenConnection(connectionString);
        //        dbManager.AddParameter("@NumRows", noOfRows);
        //        dbManager.AddParameter("@LanguageID", languageId);
        //        dbManager.AddParameter("@ProductSetId", productsetId);
        //        SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_RetrieveProductSetRS", CommandType.StoredProcedure);
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                OccassionCard obj = new OccassionCard();
        //                if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
        //                {
        //                    obj.ProductTitle = Convert.ToString(reader["ProductTitle"]);
        //                }
        //                if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
        //                {
        //                    obj.ProductId = Convert.ToInt32(reader["ProductId"]);
        //                }
        //                if (!reader.IsDBNull(reader.GetOrdinal("OnOffer")))
        //                {
        //                    int onOffer = Convert.ToInt32(reader["OnOffer"]);

        //                    if (onOffer == 0)
        //                    {
        //                        if (!reader.IsDBNull(reader.GetOrdinal("Price")))
        //                        {
        //                            obj.Price = Convert.ToDecimal(reader["Price"]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (!reader.IsDBNull(reader.GetOrdinal("Offer")))
        //                        {
        //                            obj.Price = Convert.ToDecimal(reader["Offer"]);
        //                        }
        //                    }

        //                    // if (obj.Price == 0.0M) obj.Price = -1.0M; // For identifying free product
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("Info2")))
        //                {
        //                    obj.Info2 = Convert.ToString(reader["Info2"]);
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("img1_big_high")))
        //                {
        //                    obj.Img1BigHigh = Convert.ToString(reader["img1_big_high"]);
        //                }
        //                if (!reader.IsDBNull(reader.GetOrdinal("img3_big_high")))
        //                {
        //                    obj.Img3BigHigh = Convert.ToString(reader["img3_big_high"]);
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("img1_small_high")))
        //                {
        //                    obj.Img1SmallHigh = Convert.ToString(reader["img1_small_high"]);
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("domainID")))
        //                {
        //                    obj.DomainID = Convert.ToInt32(reader["domainID"]);
        //                }

        //                //For setting NoCard Initial Value
        //                // obj.NoCard = 1;

        //                lstOccassionCards.Add(obj);
        //            }


        //            //To check any message card is already added to current Order
        //            //dbManager.ClearParameters();
        //            //if (dbManager.Connection.State == ConnectionState.Open) dbManager.Connection.Close();

        //            refSelectedProductId = GetSelectedMessageCardIDByOrderID(orderID);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.Error(ex);
        //    }
        //    finally
        //    {
        //        dbManager.CloseConnection();
        //    }
        //    return lstOccassionCards;
        //}

        public List<OccassionCard> GetCardProducts(string orderID,int occasionID, ref int refSelectedProductId)
        {
            List<OccassionCard> lstOccassionCards = new List<OccassionCard>();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OccasionID", occasionID);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCartlistByOccasionID", CommandType.StoredProcedure);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        OccassionCard obj = new OccassionCard();
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductTitle")))
                        {
                            obj.ProductTitle = Convert.ToString(reader["ProductTitle"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductId")))
                        {
                            obj.ProductId = Convert.ToInt32(reader["ProductId"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                        {
                            obj.Price = Convert.ToDecimal(reader["Price"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Info2")))
                        {
                            obj.Info2 = Convert.ToString(reader["Info2"]);
                        }
                        //if (!reader.IsDBNull(reader.GetOrdinal("img1_big_high")))
                        //{
                        //    obj.Img1BigHigh = Convert.ToString(reader["img1_big_high"]);
                        //}
                        //if (!reader.IsDBNull(reader.GetOrdinal("img1_small_high")))
                        //{
                        //    obj.Img1SmallHigh = Convert.ToString(reader["img1_small_high"]);
                        //}
                        if (!reader.IsDBNull(reader.GetOrdinal("img1_big_high")))
                        {
                            obj.Img1BigHigh = Convert.ToString(reader["img1_big_high"]).Replace("http", "https"); 
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("img1_small_high")))
                        {
                            obj.Img1SmallHigh = Convert.ToString(reader["img1_small_high"]).Replace("http", "https"); 
                        }
                        lstOccassionCards.Add(obj);
                    }
                    refSelectedProductId = GetSelectedMessageCardIDByOrderID(orderID);
                 
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return lstOccassionCards;
        }

        public int GetSelectedMessageCardIDByOrderID(string orderID)
        {
            dbManager.OpenConnection(connectionString_New);
            dbManager.AddParameter("@OrderID", orderID);
            int refSelectedProductId = Convert.ToInt32(dbManager.ExecuteScalar("SFsp_CheckMessageCardByOrderID", CommandType.StoredProcedure));
            return refSelectedProductId;
        }




        /// <summary>
        /// Author: Valuelabs
        /// Method Name: AddMessageCard()
        /// StoredProcedure Name: SFsp_AddMessageCard
        /// Description: To add Message cards
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <param name="noCard"></param>
        public void AddMessageCard(string orderId, int productId,  decimal price, int noCard)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                dbManager.AddParameter("@MessageCardID", productId);
                dbManager.AddParameter("@DeleteFlag", noCard);
                dbManager.AddParameter("@Price", price);
                dbManager.ExecuteScalar("SFsp_AddMessageCard", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        

        public DataSet CheckNonDelPostCode(string Postcode, string OrderId)
        {
            DataSet NonDelPostCode = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(OrderId) && !string.IsNullOrEmpty(Postcode))
                {
                    dbManager.OpenConnection(connectionString_New);
                    dbManager.AddParameter("@PostCode", Postcode);
                    dbManager.AddParameter("@OrderID", OrderId);
                    NonDelPostCode = dbManager.ExecuteDataSet("SFsp_CheckNonDelPostCode", CommandType.StoredProcedure);
                }
                else {

                    NonDelPostCode = null;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return NonDelPostCode;

        }
        public RecipientInfo GetCustomerAddressBookbyAddressID(int DeliveryAddressID)
        {

            RecipientInfo objRecipientInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@DeliveryAddressID", DeliveryAddressID);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCustomerAddressBookByDelAddressID", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    objRecipientInfo = AddressInfo(reader);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return objRecipientInfo;
        }
        private RecipientInfo AddressInfo(IDataRecord myDataRecord)
        {
            RecipientInfo objRecipientInfo = new RecipientInfo();
            try
            {
                
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Title")))
                {
                    objRecipientInfo.Title = myDataRecord.GetString(myDataRecord.GetOrdinal("Title"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FirstName")))
                {
                    objRecipientInfo.FirstName = myDataRecord.GetString(myDataRecord.GetOrdinal("FirstName"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LastName")))
                {
                    objRecipientInfo.LastName = myDataRecord.GetString(myDataRecord.GetOrdinal("LastName"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address1")))
                {
                    objRecipientInfo.HouseNo = myDataRecord.GetString(myDataRecord.GetOrdinal("Address1"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address2")))
                {
                    objRecipientInfo.Street = myDataRecord.GetString(myDataRecord.GetOrdinal("Address2"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Address3")))
                {
                    objRecipientInfo.District = myDataRecord.GetString(myDataRecord.GetOrdinal("Address3"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Organisation")))
                {
                    objRecipientInfo.Organisation = myDataRecord.GetString(myDataRecord.GetOrdinal("Organisation"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Mobile")))
                {
                    objRecipientInfo.RecipientMobile = myDataRecord.GetString(myDataRecord.GetOrdinal("Mobile"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PostCode")))
                {
                    objRecipientInfo.PostCode = myDataRecord.GetString(myDataRecord.GetOrdinal("PostCode"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("County")))
                {
                    objRecipientInfo.County = myDataRecord.GetString(myDataRecord.GetOrdinal("County"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CountryID")))
                {
                    objRecipientInfo.CountryID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("CountryID"));
                    
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Town")))
                {
                    objRecipientInfo.Town = myDataRecord.GetString(myDataRecord.GetOrdinal("Town"));
                }
                if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Addr_Verified")))
                {
                    objRecipientInfo.AddressVerified = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Addr_Verified"));
                }

                return objRecipientInfo;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return objRecipientInfo;

        }

        public void EditDeliveryInstructions(string OrderId, string DeliveryInstructions)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@DeliveryInstructions", DeliveryInstructions);            
                dbManager.ExecuteScalar("SFsp_EditDeliveryInstructions", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        public void EditMessageCard(string OrderId, string Message)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@Message", Message);
                dbManager.ExecuteScalar("SFsp_EditCardMessage", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }

        public int GetCardMessageLength(string orderId)
        {
            int len = 0;

            try
            {

                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                len = (int)dbManager.ExecuteScalar("SFsp_GetCardMessageLengthByOrderID", CommandType.StoredProcedure);
                return len;
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return len;
        }

        public DataSet GetProductCountryByOrderId(string OrderId)
        {
            DataSet Country = new DataSet();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                Country = dbManager.ExecuteDataSet("SFsp_GetProductCountryByOrderID", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return Country;

        }

        public string GetCardMessage(string orderId)
        {
            string str = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCardMessage", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Message")))
                        {
                            str = Convert.ToString(reader["Message"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return str;
        }

        public string GetDeliveryInstructions(string orderId)
        {
            string str = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", orderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryInstructions", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryInstructions")))
                        {
                            str = Convert.ToString(reader["DeliveryInstructions"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return str;
        }
        public void UpdateOccasion(string strOrderid, int strOccasionId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderid);
                dbManager.AddParameter("@OccasionID", strOccasionId);
                dbManager.ExecuteScalar("SFsp_UpdateOccasionAndFuneralTime", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
    }
}
