using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Log;
namespace Serenata_Checkout.Dal.Common
{
    public class CommonDal
    {
        DBManager dbManager = new DBManager();

        private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
        private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;

        #region 1. Get browser country
        /// <summary>
        /// Get browser country name against an user ip address.
        /// </summary>
        /// <param name="UserIp"></param>
        /// <returns>string</returns>
        public string GetBrowserCountry(string UserIp)
        {
            string BrowserCountry = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@IPAddress", UserIp);
                BrowserCountry = (string)dbManager.ExecuteScalar("SFsp_GetCountryFromIPAddress", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return BrowserCountry;
        }
        #endregion

        public int GetCountryIdCountryCode(string CountryCode)
        {
            int BrowserCountry = 0;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@ProductCountry", CountryCode);
                BrowserCountry = (int)dbManager.ExecuteScalar("SFsp_GetCountryIdByCountryCode", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            finally
            {
                dbManager.CloseConnection();
            }
            return BrowserCountry;
        }

        public string GetCountryNameByFulFilmentPatnerID(int PatnerId)
        {
            string strCountryName = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@FulfillmentPartnerID ", PatnerId);
                strCountryName = (string)dbManager.ExecuteScalar("SFsp_GetCountryNameByFulfillmentPartner", CommandType.StoredProcedure);
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
            return strCountryName;
        }

        public string GetCountryNameByCountryCode(int countryCode)
        {
            try
            {
                string countryname = string.Empty;
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@CountryID", countryCode);
                countryname = (String)dbManager.ExecuteScalar("SFsp_GetCountryNameByCountryID", CommandType.StoredProcedure);
                return countryname;
            }
            catch
            {
                throw;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }

        public string InsertContactTicket(string strOrderId, int intStatusId)
        {
            try
            {
                string Result = "";
                dbManager.OpenConnection(connectionString);
                dbManager.AddParameter("@OrderID", strOrderId);
                dbManager.AddParameter("@StatusID", intStatusId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_InsertContactTicket", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("IdTicket")))
                        {
                            Result = Convert.ToInt32(reader["IdTicket"]).ToString();
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("New_GUID")))
                        {
                            Result += "|" + Convert.ToString(reader["New_GUID"]);
                        }
                    }
                }
                return Result;
            }
            catch
            {
                throw;
            }
            finally
            {
                dbManager.ClearParameters();
                dbManager.CloseConnection();
            }
        }

        public string InsertContactInfo(ContactInfo objContactInfo)
        {
            string strGUIid = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@MessageFromName", objContactInfo.MessageFromName);
                dbManager.AddParameter("@MessageFromEmail", objContactInfo.MessageFromEmail);
                dbManager.AddParameter("@MessageFromPhone", objContactInfo.MessageFromPhone);

                dbManager.AddParameter("@OrderID", objContactInfo.OrderID);
                dbManager.AddParameter("@SubjectStr", objContactInfo.SubjectStr);
                dbManager.AddParameter("@EncryptedMessage", objContactInfo.EncryptedMessage);
                dbManager.AddParameter("@ReasonID", objContactInfo.ReasonID);
                dbManager.AddParameter("@SourceID", objContactInfo.SourceID);

                dbManager.AddParameter("@EncryptionType", objContactInfo.EncryptionType);

                dbManager.AddParameter("@IPAddress", objContactInfo.IPAddress);
                dbManager.AddParameter("@Source ", "MobileContactUS");

           

                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_InsertContactInfo_New", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {

                        if (!reader.IsDBNull(reader.GetOrdinal("GUID")))
                        {
                            strGUIid = Convert.ToString(reader["GUID"]);
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
            return strGUIid;
        }

        #region Validate voucher code
        /// <summary>
        /// Author: Valuelabs
        /// Method Name: ValidateVoucherCode()
        /// StoredProcedure Name: SFsp_ValidateVoucher
        /// Description: This method contains logic to execute stored procedure "SFsp_ValidateVoucher" to validate voucher code and update voucher id,discount and total  in [Orders] table.
        /// </summary>
        /// <param name="strOrderId"></param>
        /// <param name="strVoucherCode"></param>
        /// <returns>VoucherInfo</returns>
        public VoucherInfo ValidateVoucherCode(string strOrderId, string strVoucherCode, int intSiteId)
        {
            VoucherInfo objV = new VoucherInfo();
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderId", strOrderId);
                dbManager.AddParameter("@VoucherCode", strVoucherCode);
                dbManager.AddParameter("@SiteID", intSiteId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_ValidateVoucher_NewCheckout", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Result")))
                        {
                            objV.Status = Convert.ToInt32(reader["Result"]);
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
            return objV;
        }
        #endregion

        public string GetCountdownTimeByOrderID(string strOrderId, ref string strDeliveryDate)
        {
            string strEndTime = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                dbManager.AddParameter("@OrderId", strOrderId);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetCountdownTimeByOrderID", CommandType.StoredProcedure);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("EndTime")))
                        {
                            strEndTime = Convert.ToString(reader["EndTime"]);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryDate")))
                        {
                            strDeliveryDate = Convert.ToString(reader["DeliveryDate"]);
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
            return strEndTime;
        }
        public string GetServerDateTime()
        {
            string strCurTime = string.Empty;
            try
            {
                dbManager.OpenConnection(connectionString_New);
                SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("select getdate() as GetDate", CommandType.Text);

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("GetDate")))
                        {
                            strCurTime = Convert.ToString(reader["GetDate"]);
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
            return strCurTime;
        }

    }
}
