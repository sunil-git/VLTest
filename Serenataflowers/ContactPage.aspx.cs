#region About Page
// Created Date: 18-July-2012
// Purpose: It shows the email communication details.
#endregion

#region Import Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;
using log4net;
using log4net.Config;
using SFMobile.Exceptions;
using System.Web.UI.HtmlControls;
using SFMobile.BAL.Orders;
using SFMobile.DTO;
#endregion

namespace Serenataflowers
{
    public partial class ContactPage : System.Web.UI.Page
    {

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ltTitle.Text = "\n<title>" + " Serenata Email Center - " + CommonFunctions.PageTitle() + "</title>\n";
            DataSet dsetMsg = new DataSet(); 
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ticketID"] != null)
                    {
                        BindMsgToDataList(Request.QueryString["ticketID"]);
                    }
                }
                divResponse.Visible = false;
                divReply.Visible = true;
                
          
                ClientScript.GetPostBackEventReference(this, string.Empty);
                string targetCtrl = Page.Request.Params.Get("__EVENTTARGET");
                if (targetCtrl != null && targetCtrl != string.Empty)
                {
                    if (targetCtrl == "ancSend")
                    {
                        OrdersLogic objOrder = new OrdersLogic();
                        dsetMsg = objOrder.GetContactUsDetailsByGuid(Request.QueryString["ticketID"]);
                        if (dsetMsg != null)
                        {
                            EmailTicketInfo objTicketDtl = GetEmailTicketObj(dsetMsg);
                            objOrder.InsertContactPageInfo(objTicketDtl);
                            divReply.Visible = false;
                            divResponse.Visible = true;
                            BindMsgToDataList(Request.QueryString["ticketID"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dsetMsg.Dispose();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// GetEmailTicketObj
        /// </summary>
        /// <param name="dsetMsg"></param>
        /// <returns></returns>
        private EmailTicketInfo GetEmailTicketObj(DataSet dsetMsg)
        {
            EmailTicketInfo objTicketDtl = new EmailTicketInfo();
            //Key to be added in web.config
            string encryptPwd = ConfigurationManager.AppSettings["EncryptionPassword"];
            try
            {
                DataRow dRow = dsetMsg.Tables[0].Rows[0];

                objTicketDtl.MessageFromName = Convert.ToString(dRow["MessageFromName"]);
                objTicketDtl.MessageFromEmail = Convert.ToString(dRow["MessageFromEmail"]);
                objTicketDtl.MessageTo = Convert.ToString(dRow["MessageTo"]);
                objTicketDtl.OrderID = Convert.ToString(dRow["OrderID"]);
                objTicketDtl.MessageDate = Convert.ToDateTime(dRow["MessageDate"]);
                objTicketDtl.SubjectStr = Convert.ToString(dRow["SubjectStr"]);
                objTicketDtl.ReasonID = Convert.ToInt32(dRow["ReasonID"]);
                objTicketDtl.SourceID = Convert.ToInt32(dRow["SourceID"]);
                objTicketDtl.IdTicket = Convert.ToInt32(dRow["IdTicket"]);

                objTicketDtl.EncryptionType = ConfigurationManager.AppSettings["EncryptType"];
                if (objTicketDtl.OrderID != "")
                {
                    encryptPwd = Microsoft.VisualBasic.Strings.Left(Microsoft.VisualBasic.Strings.StrReverse(objTicketDtl.OrderID), objTicketDtl.OrderID.Length - 1);
                }
                string cryptAlgorithm = ConfigurationManager.AppSettings["CryptAlgorithm"];
                string cipherMode = ConfigurationManager.AppSettings["CipherMode"];
                int encryptkeylength = Convert.ToInt32(ConfigurationManager.AppSettings["KeyLength"]);
                string encoding = ConfigurationManager.AppSettings["Encoding"];
                string encodingmode = ConfigurationManager.AppSettings["EncodingMode"];
                objTicketDtl.EncryptedMessage = CommonFunctions.GetEncryptedMsg(ConfigurationManager.AppSettings["CryptUnlockCode"], txtMessage.Text,
                    encryptPwd,
                    cryptAlgorithm,
                    cipherMode,
                    encryptkeylength,
                    encoding,
                    encodingmode);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            return objTicketDtl;
        }
      
        private void BindMsgToDataList(string str_Guid)
        {
            DataSet dsetMsg = null;
            try
            {
                OrdersLogic objOrder = new OrdersLogic();
                dsetMsg = objOrder.GetContactUsDetailsByGuid(str_Guid);
                if (dsetMsg != null)
                {
                    dlMsg.DataSource = dsetMsg.Tables[0];
                    dlMsg.DataBind();
                    string str_LastMsg = Convert.ToString(dsetMsg.Tables[0].Rows[0]["encryptedMessage"]);
                    string str_Name = "Hi " + Convert.ToString(dsetMsg.Tables[0].Rows[0]["MessageFromName"]) + "!";
                    DateTime str_MsgDate = Convert.ToDateTime(dsetMsg.Tables[0].Rows[0]["MessageDate"]);
                    string str_OrderId = Convert.ToString(dsetMsg.Tables[0].Rows[0]["OrderId"]);
                    // Code for decryption
                    string encryptPwd = ConfigurationManager.AppSettings["EncryptionPassword"];
                    if (str_OrderId != "")
                    {
                        encryptPwd = Microsoft.VisualBasic.Strings.Left(Microsoft.VisualBasic.Strings.StrReverse(str_OrderId), str_OrderId.Length - 1);
                    }
                    string cryptAlgorithm = ConfigurationManager.AppSettings["CryptAlgorithm"];
                    string cipherMode = ConfigurationManager.AppSettings["CipherMode"];
                    int encryptkeylength = Convert.ToInt32(ConfigurationManager.AppSettings["KeyLength"]);
                    string encoding = ConfigurationManager.AppSettings["Encoding"];
                    string encodingmode = ConfigurationManager.AppSettings["EncodingMode"];
                    lblLastMsg.Text = CommonFunctions.GetDecryptedMsg(ConfigurationManager.AppSettings["CryptUnlockCode"], str_LastMsg,
                        encryptPwd,
                        cryptAlgorithm,
                        cipherMode,
                        encryptkeylength,
                        encoding,
                        encodingmode);
                    lblName.Text = str_Name;
                    lblDateText.Text = "Last email sent by You | Date: " + str_MsgDate.ToString("mm/dd/yyyy HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
            finally
            {
                dsetMsg.Dispose();
            }
            
        }
        #endregion

        #region DataList ItemDataBound
        protected void dlMsg_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Label lblMsg = e.Item.FindControl("lblMsg") as Label;
                //Code for Decryption:Chilkat
                string encryptPwd = ConfigurationManager.AppSettings["EncryptionPassword"];
                string str_OrderId = ((HiddenField)e.Item.FindControl("hdnOrderId")).Value;
                if (str_OrderId != "")
                {
                    encryptPwd = Microsoft.VisualBasic.Strings.Left(Microsoft.VisualBasic.Strings.StrReverse(str_OrderId), str_OrderId.Length - 1);
                }
                string cryptAlgorithm = ConfigurationManager.AppSettings["CryptAlgorithm"];
                string cipherMode = ConfigurationManager.AppSettings["CipherMode"];
                int encryptkeylength = Convert.ToInt32(ConfigurationManager.AppSettings["KeyLength"]);
                string encoding = ConfigurationManager.AppSettings["Encoding"];
                string encodingmode = ConfigurationManager.AppSettings["EncodingMode"];
                lblMsg.Text = CommonFunctions.GetDecryptedMsg(ConfigurationManager.AppSettings["CryptUnlockCode"], lblMsg.Text,
                    encryptPwd,
                    cryptAlgorithm,
                    cipherMode,
                    encryptkeylength,
                    encoding,
                    encodingmode);
                Label lbl = e.Item.FindControl("dateMsg") as Label;
                DateTime dtMsgDate = Convert.ToDateTime(((HiddenField)e.Item.FindControl("hdnMsgDate")).Value);
                lbl.Text = "By:" + ((HiddenField)e.Item.FindControl("hdnFromName")).Value + " | " + "Date: " + dtMsgDate.ToString("mm/dd/yyyy HH:mm:ss") + " (" + ((HiddenField)e.Item.FindControl("hdnMessageId")).Value + ")";
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        #endregion
    }
}