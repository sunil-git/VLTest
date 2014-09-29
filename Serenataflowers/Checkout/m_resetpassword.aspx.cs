using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Bal;
namespace Serenataflowers.Checkout
{
    public partial class m_resetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private string GetOrderIdFromQueryString()
        {
            string strCustId = string.Empty;
            try
            {
                Encryption objEncryption = new Encryption();
                strCustId = objEncryption.GetAesDecryptionString(Common.GetQueryStringValue("cid"));
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return strCustId;
        }
        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string strCustomerId = GetOrderIdFromQueryString();
                if (!string.IsNullOrEmpty(strCustomerId))
                {
                    CustomerDetailsBAL objCustomerDetails = new CustomerDetailsBAL();
                    Common objCommon = new Common();
                    LoginInfo objLoginInfo = new LoginInfo();
                    objLoginInfo.EncryptedPassword = objCommon.CalculateMD5Hash(NewPassword.Text.Trim());
                    int iCustId = Convert.ToInt32(strCustomerId);
                    objCustomerDetails.UpdateCustomerPassword(objLoginInfo, iCustId);
                    ScriptManager.RegisterClientScriptBlock(ResetPassword, ResetPassword.GetType(), "test123", "UpdatedPassword();", true);
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
    }
}