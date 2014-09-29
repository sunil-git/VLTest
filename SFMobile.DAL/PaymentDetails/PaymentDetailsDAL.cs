using System;
using System.Linq;
using System.Text;
using System.Data;
using SFMobile.DTO;
using SFMobile.Exceptions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
namespace SFMobile.DAL.PaymentDetails
{
  public  class PaymentDetailsDAL
    {
        DBManager dbManager = new DBManager();
        private string connectionString = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;  
     
        public int MakePaymentOrRefund(PaymentDTO objPaymentInfo)
        {
          int response = 0;
          try
          {
              dbManager.OpenConnection(connectionString);
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
              dbManager.ExecuteScalar("SFsp_MakePaymentOrRefund", CommandType.StoredProcedure);
          }
          catch (Exception ex)
          {
              SFMobileLog.Error(ex);
              response = -1;
          }
          finally
          {
              dbManager.CloseConnection();

          }
          return response;
      }

        public int AddMakePayment(PaymentDTO objPaymentInfo)
        {
            int response = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@cType", objPaymentInfo.IDCardType);
                dbManager.AddParameter("@oref", objPaymentInfo.OrderID);
                dbManager.AddParameter("@tAmount", objPaymentInfo.TotalAmount);
                dbManager.AddParameter("@stat", objPaymentInfo.PaymentStatus);
                dbManager.AddParameter("@errString", objPaymentInfo.Comment);
                dbManager.AddParameter("@transID", objPaymentInfo.TransID);
                dbManager.AddParameter("@idGateway", objPaymentInfo.PaymentGatewayID);
                dbManager.AddParameter("@TransType", objPaymentInfo.IDTransType);
                dbManager.AddParameter("@cardScheme", "");                
                dbManager.ExecuteScalar("SFsp_InsertPaymentDetails", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                response = -1;
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return response;
        }

        public int UpdatePaymentOrder(PaymentDTO objPaymentInfo)
        {
            int response = 0;
            try
            {
                dbManager.OpenConnection();
                dbManager.AddParameter("@OrderID", objPaymentInfo.OrderID);
                dbManager.AddParameter("@TAmount", objPaymentInfo.TotalAmount);
                dbManager.AddParameter("@PaymentTypeID", objPaymentInfo.PaymentTypeID);  
                dbManager.ExecuteScalar("SFsp_UpdateOrderPaymentDetails", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
                response = -1;
            }
            finally
            {
                dbManager.CloseConnection();

            }
            return response;
        }
        public void UpdateProductPaymentStatus(string strOrderId)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.ExecuteScalar("SFsp_UpdateProductPaymentStatus", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
        public void UpdateOrderStatus(string strOrderId,int orderStatus)
        {
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@OrderStatusID", strOrderId);
                dbManager.AddParameter("@ByWhom", "Customer");
                dbManager.ExecuteScalar("SFsp_ChangeOrderStatus", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }
    }
}
