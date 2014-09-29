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
   public class DispatchesDAL
    {
       DBManager dbManager = new DBManager();

       private static string connectionString = ConfigurationManager.ConnectionStrings["SerenaConnectionString"].ConnectionString;
       private static string connectionString_New = ConfigurationManager.ConnectionStrings["neworderschema_connectionString"].ConnectionString;
       /// <summary>
       /// Author: Valuelabs
       /// Method Name: GetDeliveryDatesByProductId()
       /// StoredProcedure Name: SFSP_GetDeliveryDate
       /// Description: To get DeliveryDates for a perticuler product
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
       public List<DeliveryTimeInfo> GetDeliveryDatesByProductId(int productId)
       {
           List<DeliveryTimeInfo> lstDeliveryItems = new List<DeliveryTimeInfo>();
           try
           {
               dbManager.OpenConnection(connectionString);
               dbManager.AddParameter("@ProductId", productId);
               SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFSP_GetDeliveryDate", CommandType.StoredProcedure);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        DeliveryTimeInfo objDeliveryInfo = new DeliveryTimeInfo();

                        if (!reader.IsDBNull(reader.GetOrdinal("DateValue")))
                        {
                            objDeliveryInfo.DateValue = Convert.ToString(reader["DateValue"]);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("Deliverydate")))
                        {
                            objDeliveryInfo.Deliverydate = Convert.ToString(reader["Deliverydate"]);
                        }
                        lstDeliveryItems.Add(objDeliveryInfo);

                    }
                }

             
           }
           catch (Exception ex)
           {
               throw;
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
           return lstDeliveryItems;
       }

       /// <summary>
       /// Author: Valuelabs
       /// Method Name: GetDeliveryOptionsByDeliveryDate()
       /// StoredProcedure Name: SFsp_GetDeliveryOptions
       /// Description:Get Delivery options by delivery date
       /// </summary>
       /// <param name="productId"></param>
       /// <param name="day"></param>
       /// <param name="selectedDate"></param>
       /// <returns></returns>
       public List<DeliveryTimeInfo> GetDeliveryOptionsByDeliveryDate(int productId, string day, string selectedDate)
       {
           List<DeliveryTimeInfo> lstDeliveryItems = new List<DeliveryTimeInfo>();
           try
           {
               dbManager.OpenConnection(connectionString);
               dbManager.AddParameter("@ProductId", productId);
               dbManager.AddParameter("@Day", day);
               dbManager.AddParameter("@SelectedDeliveryDate", selectedDate);
               SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryOptions", CommandType.StoredProcedure);
             

               if (reader != null)
               {
                   while (reader.Read())
                   {
                       DeliveryTimeInfo objDeliveryInfo = new DeliveryTimeInfo();

                       if (!reader.IsDBNull(reader.GetOrdinal("id")))
                       {
                           objDeliveryInfo.id = Convert.ToInt32(reader["id"]);
                       }
                       if (!reader.IsDBNull(reader.GetOrdinal("OptionName")))
                       {
                           objDeliveryInfo.OptionName = Convert.ToString(reader["OptionName"]);
                       }
                       if (!reader.IsDBNull(reader.GetOrdinal("OptionPrice")))
                       {
                           objDeliveryInfo.deliveryPrice = Convert.ToDouble(reader["OptionPrice"]);
                       }
                       if (!reader.IsDBNull(reader.GetOrdinal("DeliveryPartnerID")))
                       {
                           objDeliveryInfo.DeliveryPartnerID = Convert.ToInt32(reader["DeliveryPartnerID"]);
                       }
                       lstDeliveryItems.Add(objDeliveryInfo);

                   }
               }

             
           }
           catch (Exception ex)
           {
               throw;
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
           return lstDeliveryItems;
       }
       /// <summary>
       /// Author: Valuelabs
       /// Method Name: ScheduleDispatch()
       /// StoredProcedure Name: Sfsp_RescheduleDispatch
       /// Description:this method used to add dispatch details
       /// </summary>
       /// <param name="objDispatches"></param>
       public void ScheduleDispatch(DispatchesInfo objDispatches)
       {
           try
           {
               dbManager.OpenConnection(connectionString_New);
               dbManager.AddParameter("@OrderID", objDispatches.OrderID);
               dbManager.AddParameter("@DeliveryDate", objDispatches.DeliveryDate);
               dbManager.AddParameter("@DelOptionID", objDispatches.DelOptionID);
               dbManager.AddParameter("@CarrierID", objDispatches.CarrierID);
               dbManager.AddParameter("@DeliveryTime", objDispatches.DeliveryTime);
               dbManager.AddParameter("@DeliveryPrice", objDispatches.DeliveryPrice);
               dbManager.ExecuteScalar("Sfsp_RescheduleDispatch", CommandType.StoredProcedure);
           }
           catch (Exception ex)
           {
               throw;
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
       }


      /// <summary>
       /// Author: Valuelabs
       /// Method Name: GetmainProductByOrderId()
       /// StoredProcedure Name: SFsp_GetCoreProductIDSortByPrice
       /// Description:To get main product id by orderId
      /// </summary>
      /// <param name="strOrderId"></param>
      /// <returns></returns>
       public int GetmainProductByOrderId(string strOrderId)
       {
           int intRtn = 0;
           try
           {
               dbManager.OpenConnection(connectionString_New);
               dbManager.AddParameter("@OrderID", strOrderId);
               intRtn = (Int32)dbManager.ExecuteScalar("SFsp_GetCoreProductIDSortByPrice", CommandType.StoredProcedure);
           }
           catch(Exception ex)
           {
               ErrorLog.Error(ex);
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
           return intRtn;
       }
        /// <summary>
       /// Author: Valuelabs
       /// Method Name: GetDeliveryDetailsByDelOptionID()
       /// StoredProcedure Name: SFsp_GetDeliveryDetailsByDelOptionID
       /// Description:To get DeliveryOption details 
       /// </summary>
       /// <param name="objDispatches"></param>
       /// <returns></returns>
       public DispatchesInfo GetDeliveryDetailsByDelOptionID(int delOptionId)
       {
           DispatchesInfo objDispatches = new DispatchesInfo();
           try
           {
              
               dbManager.OpenConnection(connectionString);
               dbManager.AddParameter("@DelOptionID", delOptionId);
               SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryDetailsByDelOptionID", CommandType.StoredProcedure);
               if (reader.Read())
               {

                   if (!reader.IsDBNull(reader.GetOrdinal("DeliveryPartnerID")))
                   {
                       objDispatches.CarrierID = Convert.ToInt32(reader["DeliveryPartnerID"]);
                   }
                   if (!reader.IsDBNull(reader.GetOrdinal("OptionName")))
                   {
                       objDispatches.DeliveryTime = Convert.ToString(reader["OptionName"]);
                   }
                   if (!reader.IsDBNull(reader.GetOrdinal("OptionPrice")))
                   {
                       objDispatches.DeliveryPrice = Convert.ToSingle(reader["OptionPrice"].ToString());
                   }
               }
             
           }
           catch (Exception ex)
           {
               throw;
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
           return objDispatches;
       }
       /// <summary>
       /// Author: Valuelabs
       /// Method Name: CheckCashedProduct()
       /// StoredProcedure Name: SFsp_CheckCachedProduct
       /// Description:To get Product cashed or not
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
       public int CheckCashedProduct(int productId)
       {
           int intRtn = 0;
           try
           {
               
               dbManager.OpenConnection(connectionString);
               dbManager.AddParameter("@productId", productId);
                intRtn = (Int32)dbManager.ExecuteScalar("SFsp_CheckCachedProduct", CommandType.StoredProcedure);
              
               
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
           return intRtn;
       }
       public List<DeliveryTimeInfo> GetSuggetedDates(int productId, DateTime suggestedDate)
       {
           List<DeliveryTimeInfo> lstDeliveryItems = new List<DeliveryTimeInfo>();
           try
           {
               dbManager.OpenConnection(connectionString);
               dbManager.AddParameter("@ProductId", productId);
               dbManager.AddParameter("@SpecifiedDelDate", suggestedDate);
               SqlDataReader reader = (SqlDataReader)dbManager.ExecuteReader("SFsp_GetDeliveryDatesFromSpecificDate", CommandType.StoredProcedure);

               if (reader != null)
               {
                   while (reader.Read())
                   {
                       DeliveryTimeInfo objDeliveryInfo = new DeliveryTimeInfo();

                       if (!reader.IsDBNull(reader.GetOrdinal("DateValue")))
                       {
                           objDeliveryInfo.DateValue = Convert.ToString(reader["DateValue"]);
                       }
                       if (!reader.IsDBNull(reader.GetOrdinal("Deliverydate")))
                       {
                           objDeliveryInfo.Deliverydate = Convert.ToString(reader["Deliverydate"]);
                       }
                       lstDeliveryItems.Add(objDeliveryInfo);

                   }
               }


           }
           catch (Exception ex)
           {
               throw;
           }
           finally
           {
               dbManager.ClearParameters();
               dbManager.CloseConnection();
           }
           return lstDeliveryItems;
       }
      
    }
}
