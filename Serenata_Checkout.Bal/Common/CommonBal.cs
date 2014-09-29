using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Xml;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Dal;
using Serenata_Checkout.Log;
using Serenata_Checkout.Dal.Common;

namespace Serenata_Checkout.Bal.Common
{
    public class CommonBal
    {

        public string InsertContactTicket(string strOrderId, int intStatusId)
        {
            return new CommonDal().InsertContactTicket(strOrderId, intStatusId);
        }

        public string InsertContactInfo(ContactInfo objContactInfo)
        {
           return new CommonDal().InsertContactInfo(objContactInfo);
        }
        public int GetCountryIdCountryCode(string CountryCode)
        {
            return new CommonDal().GetCountryIdCountryCode(CountryCode);
        
        }

        public string GetCountryNameByCountryCode(int countryCode)
        {
            return new CommonDal().GetCountryNameByCountryCode(countryCode);
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
        /// <param name="strSiteId"></param>
        /// <returns>VoucherInfo</returns>
        public VoucherInfo ValidateVoucherCode(string strOrderId, string strVoucherCode, int intSiteId)
        {
            return new CommonDal().ValidateVoucherCode(strOrderId, strVoucherCode, intSiteId);
        }
        #endregion

        public string GetCountdownTimeByOrderID(string strOrderId, ref string strDeliveryDate)
        {
            return new CommonDal().GetCountdownTimeByOrderID(strOrderId, ref strDeliveryDate);
        }

        public string GetServerDateTime()
        {
            return new CommonDal().GetServerDateTime();
        }
    }
}

