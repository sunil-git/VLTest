using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
namespace Serenata_Checkout.Dal
{
    public class PaymentDetailsDAL
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;
        public int MakePaymentOrRefund(PaymentInfo objPaymentInfo)
        {
            int response = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objPaymentInfo.OrderID);
                dbManager.AddParameter("@PaymentTypeID", objPaymentInfo.PaymentTypeID);
                dbManager.AddParameter("@PaymentGatewayID", objPaymentInfo.PaymentGatewayID);
                dbManager.AddParameter("@PaymentStatus", objPaymentInfo.PaymentStatus);
                dbManager.AddParameter("@Amount", objPaymentInfo.Amount);
                dbManager.AddParameter("@IDCardType", objPaymentInfo.IDCardType);
                dbManager.AddParameter("@TransID", objPaymentInfo.TransID);
                dbManager.AddParameter("@IDTransType", objPaymentInfo.IDTransType);
                dbManager.AddParameter("@TotalAmount", objPaymentInfo.TotalAmount);
                dbManager.AddParameter("@Comment", objPaymentInfo.Comment);
                dbManager.AddParameter("@CV2Result", objPaymentInfo.CV2Result);
                dbManager.AddParameter("@AddressResult", objPaymentInfo.AddressResult);
                dbManager.AddParameter("@PostcodeResult", objPaymentInfo.PostcodeResult);
                dbManager.AddParameter("@CardScheme", objPaymentInfo.CardScheme);
                dbManager.AddParameter("@CardCountry", objPaymentInfo.CardCountry);
                dbManager.AddParameter("@ChasePaymentType", objPaymentInfo.ChasePaymentType);                
                dbManager.ExecuteScalar("SFsp_MakePaymentOrRefund", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                response = -1;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();

            }
            return response;
        }
        public void UpdateOrderStatus(PaymentInfo objPaymentInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objPaymentInfo.OrderID);
                dbManager.AddParameter("@OrderStatusID", objPaymentInfo.PaymentStatus);
                dbManager.AddParameter("@ByWhom", "Customer");
                dbManager.ExecuteScalar("SFsp_ChangeOrderStatus", CommandType.StoredProcedure);
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
        public void UpdateDHHPResponse(DHHPInfo objDHPPInfo)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objDHPPInfo.OrderID);
                dbManager.AddParameter("@TransactionSecret", objDHPPInfo.TrasactionSecrete);
                dbManager.AddParameter("@DHPPToken", objDHPPInfo.Token);
                dbManager.AddParameter("@UUID", objDHPPInfo.UUID);                
                dbManager.ExecuteScalar("SFsp_UpdateDHPPResponse", CommandType.StoredProcedure);
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

        public DHHPInfo GetDHHPResponse(string strOrderId)
        {
            DHHPInfo objDHHPInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDHPPResponse", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objDHHPInfo = new DHHPInfo();

                        if (!reader.IsDBNull(reader.GetOrdinal("TransactionSecret")))
                        {
                            objDHHPInfo.TrasactionSecrete = Convert.ToString(reader["TransactionSecret"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("DHPPToken")))
                        {
                            objDHHPInfo.Token = Convert.ToString(reader["DHPPToken"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Total")))
                        {
                            objDHHPInfo.Amount = Convert.ToDouble(reader["Total"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("UUID")))
                        {
                            objDHHPInfo.UUID = Convert.ToString(reader["UUID"]);
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
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return objDHHPInfo;
        }

        public int GetCardTypeByCardName(string CardName)
        {
            int CardType = 0;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@CardName", CardName);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCardTypeByCardName", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        CardType = Convert.ToInt16(reader["idCardType"]);
                    }
                }

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
            return CardType;
        }

        public PaymentInfo GetPaymentDetailsByPaymentStatus(string strOrderId,int paymentStatus,string chasePaymentType)
        {
            PaymentInfo objPaymentInfo = null;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@PaymentStatus", paymentStatus);
                dbManager.AddParameter("@ChasePaymentType", chasePaymentType);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetPaymentDataByPaymentStatus", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        objPaymentInfo = new PaymentInfo();

                        if (!reader.IsDBNull(reader.GetOrdinal("TransID")))
                        {
                            objPaymentInfo.TransID = Convert.ToString(reader["TransID"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Amount")))
                        {
                            objPaymentInfo.Amount = Convert.ToDouble(reader["Amount"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("TotalAmount")))
                        {
                            objPaymentInfo.TotalAmount = Convert.ToDouble(reader["TotalAmount"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("OrderID")))
                        {
                            objPaymentInfo.OrderID = Convert.ToString(reader["OrderID"]);
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
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
            return objPaymentInfo;
        }

        public string GetCardCountry(string ISOCountry)
        {
            string countryname = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@ISOCountryCode", ISOCountry);

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCountryNameByISOCountryCode", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        countryname = Convert.ToString(reader["Country"]);
                    }
                }

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
            return countryname;
        }

        public int InsertChaseDetails(string OrderId, string UUID)
        {
            int response = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@UUID", UUID);
                dbManager.ExecuteScalar("SFsp_InsertChaseUUID", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
                response = -1;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();

            }
            return response;
        }

        public void UpdateOrderDate(string OrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.ExecuteScalar("SFsp_UpdateOrderDate", CommandType.StoredProcedure);
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

        public int CheckDuplicatePayment(PaymentInfo objPaymentInfo)
        {
            int IsDuplicate = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", objPaymentInfo.OrderID);
                dbManager.AddParameter("@TransID", objPaymentInfo.TransID);
                dbManager.AddParameter("@TotalAmount", objPaymentInfo.TotalAmount);
                IsDuplicate = (int)dbManager.ExecuteScalar("SFsp_CheckDuplicatePayment", CommandType.StoredProcedure);


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
            return IsDuplicate;
        }

        public void UpdateIsRedirectURL(string OrderId,string IsRedirect)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@IsRedirectURL", IsRedirect);
                dbManager.ExecuteScalar("SFsp_UpdateIsRedirectURL", CommandType.StoredProcedure);
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
        public int IsPaymentExists(string OrderID, double Totalamount)
        {
            int IsDuplicate = 0;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderID);
                dbManager.AddParameter("@TotalAmount", Totalamount);
                IsDuplicate = (int)dbManager.ExecuteScalar("SFsp_IsPaymentExists", CommandType.StoredProcedure);


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
            return IsDuplicate;
        }
        public void UpdateGetStatusTime(string OrderId, string UUID)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@UUID", UUID);
                dbManager.ExecuteScalar("SFsp_UpdateGetStatusTime", CommandType.StoredProcedure);
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
        public void InsertChaseActions(string OrderId, string Action, string Response, string UUID, string TrasactionSecret)
        {
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderId);
                dbManager.AddParameter("@Action", Action);
                dbManager.AddParameter("@Response", Response);
                dbManager.AddParameter("@UUID", UUID);
                dbManager.AddParameter("@TransactionSecret", TrasactionSecret);
                dbManager.ExecuteScalar("SFsp_InsertChaseAction", CommandType.StoredProcedure);
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
        public string CheckOrderPaymentStatus(string OrderID)
        {
            string strMsg = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderID", OrderID);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_CheckPaymentstatusForOrder", CommandType.StoredProcedure);
                if (reader.Read())
                {
                    strMsg = Convert.ToString(reader["PaymentStatus"]);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }

            return strMsg;
        }
    }
}
