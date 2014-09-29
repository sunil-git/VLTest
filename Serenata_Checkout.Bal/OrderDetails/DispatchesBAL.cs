using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Dto;

namespace Serenata_Checkout.Bal
{
   public class DispatchesBAL
    {
       /// <summary>
       /// To get Delivery Dates by productId
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
       public List<DeliveryTimeInfo> GetDeliveryDatesByProductId(int productId)
       {
           return new DispatchesDAL().GetDeliveryDatesByProductId(productId);          

       }
       /// <summary>
       /// To get delivery Options by delivery date.
       /// </summary>
       /// <param name="productId"></param>
       /// <param name="day"></param>
       /// <param name="selectedDate"></param>
       /// <returns></returns>
       public List<DeliveryTimeInfo> GetDeliveryOptionsByDeliveryDate(int productId, string day, string selectedDate)
       {
           return new DispatchesDAL().GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);

       }
       /// <summary>
       /// To Save dispatch details
       /// </summary>
       /// <param name="objDispatches"></param>
       public void ScheduleDispatch(DispatchesInfo objDispatches)
       {
           new DispatchesDAL().ScheduleDispatch(objDispatches);

       }
       /// <summary>
       /// To get ProductId by orderId
       /// </summary>
       /// <param name="strOrderId"></param>
       /// <returns></returns>
       public string GetmainProductByOrderId(string strOrderId)
       {
           string strRtn = string.Empty;
           try
           {
               int intRtn = new DispatchesDAL().GetmainProductByOrderId(strOrderId);
               strRtn = Convert.ToString(intRtn);
           }
           
           catch (Exception)
           {
               throw;
           }

           return strRtn;

       }
       /// <summary>
       /// To get Delivery details using DelOptionID
       /// </summary>
       /// <param name="objDispatches"></param>
       /// <returns></returns>
       public DispatchesInfo GetDeliveryDetailsByDelOptionID(int delOptionId)
       {
           return new DispatchesDAL().GetDeliveryDetailsByDelOptionID(delOptionId);
       }
       /// <summary>
       /// To get product cashed status
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
       public Boolean isProductCashed(int productId)
       {
           Boolean blnRtn;
           try
           {
               int intRtn = new DispatchesDAL().CheckCashedProduct(productId);              

               if (intRtn == 1)
                   blnRtn = true;
               else
                   blnRtn = false;
           }
           catch (Exception)
           {
               throw;
           }
           
           return blnRtn;

       }
       public List<DeliveryTimeInfo> GetSuggetedDates(int productId, DateTime suggestedDate)
       {
           return new DispatchesDAL().GetSuggetedDates(productId, suggestedDate);

       }
    }
}
