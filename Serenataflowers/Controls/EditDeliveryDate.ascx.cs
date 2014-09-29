using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using SFMobile.Exceptions;
using System.Globalization;
namespace Serenataflowers.Controls
{
    public partial class EditDeliveryDate : System.Web.UI.UserControl
    {
        string orderID;
        public UpdatePanelUpdateMode UpdateMode
        {
            get { return this.updDelivery.UpdateMode; }
            set { this.updDelivery.UpdateMode = value; }
        }

        public void Update()
        {
            int iProductId = (ViewState["productId"] != null ? Convert.ToInt32(ViewState["productId"]) : 0);
            BindDeliveryDates(iProductId);
            this.updDelivery.Update();
        }
        public event EventHandler ButtonClick;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            OrderDetailsBAL objOrderDetails = new OrderDetailsBAL();

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    SaveChanges.Attributes.Add("data-theme", "a");
                    SaveChanges.Attributes.Add("data-inline", "true");
                    SaveChanges.Attributes.Add("data-mini", "true");
                    SaveChanges.Attributes.Add("data-role", "button");
                    SaveChanges.Attributes.Add("data-icon", "edit");
                    SaveChanges.Attributes.Add("data-iconpos", "left");

                    btnCancel.Attributes.Add("data-theme", "a");
                    btnCancel.Attributes.Add("data-inline", "true");
                    btnCancel.Attributes.Add("data-mini", "true");
                    btnCancel.Attributes.Add("data-role", "button");
                    btnCancel.Attributes.Add("data-icon", "delete");
                    btnCancel.Attributes.Add("data-iconpos", "left");

                    if (!IsPostBack)
                    {
                        DispatchesBAL objDelivery = new DispatchesBAL();
                        orderID = Common.GetOrderIdFromQueryString();
                        string strRtn = objDelivery.GetmainProductByOrderId(orderID);
                        if (strRtn != null)
                        {
                            ViewState["productId"] = strRtn;
                            BindDeliveryDates(Convert.ToInt32(strRtn));
                            ddlDeliveryDate_SelectedIndexChanged(null, null);  
                        }
                      
                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }

                string strOrderId = Common.GetOrderIdFromQueryString();
                string strMessage = new OrderDetailsBAL().CheckDeliveryCutoffByOrderID(strOrderId);
                if (String.IsNullOrEmpty(strMessage) == false)
                {
                    //RemoveCookie("OrderId", Response, Request);
                    //Response.Redirect("http://" + Common.getRootUrl() + "/", false);
                    btnCancel.Visible = false;


                }
                else
                {
                    btnCancel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindDeliveryDates(int productId)
        {
            DispatchesBAL objDelivery = new DispatchesBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            Common objCommon = new Common();
            try
            {
                   List<DeliveryTimeInfo> lstDeliveryDateItems = objDelivery.GetDeliveryDatesByProductId(productId);

                    if (lstDeliveryDateItems != null)
                    {
                        ddlDeliveryDate.DataSource = lstDeliveryDateItems;
                        ddlDeliveryDate.DataTextField = "DeliveryDate";
                        ddlDeliveryDate.DataValueField = "DateValue";
                        ddlDeliveryDate.DataBind();
                        orderID = Common.GetOrderIdFromQueryString();
                        objDeliveryTimeInfo = objOrderdetails.GetDeliveryDetails(orderID);
                      
                        string strValue = objDeliveryTimeInfo.DateValue;
                        string strOptionName = Convert.ToString(objDeliveryTimeInfo.OptionName);
                        ViewState["strOptionName"] = strOptionName;


                        //ListItem selectedListItem = ddlDeliveryDate.Items.FindByText(strValue);
                        ListItem selectedListItem = ddlDeliveryDate.Items.FindByValue(strValue);

                        if (selectedListItem != null)
                        {
                            ddlDeliveryDate.Items.FindByValue(strValue).Selected = true;

                        }
                        else
                        {
                            ddlDeliveryDate.SelectedIndex = 0;
                        }

                        string nameOfDay = objCommon.ConvertDateFormateToNameOfDay(ddlDeliveryDate.SelectedValue);
                        BindDeliveryOptionsBasedOnDate(nameOfDay, productId, ddlDeliveryDate.SelectedValue, strOptionName);
                    }
                             
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        protected void ddlDeliveryDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Common objCommon = new Common();
                if (objCommon != null)
                {
                    if (ViewState["productId"] != null)
                    {
                        int productId = Convert.ToInt32(ViewState["productId"]);
                        string nameOfDay = objCommon.ConvertDateFormateToNameOfDay(ddlDeliveryDate.SelectedValue);
                        Label lblDeliveryType = new Label();
                        Page pg = (Page)this.Parent.NamingContainer;
                        UserControl ucBasket = (UserControl)pg.FindControl("ModifyBasket");
                         lblDeliveryType = (Label)ucBasket.FindControl("lblDeliveryType");
                       
                        
                        BindDeliveryOptionsBasedOnDate(nameOfDay, productId, ddlDeliveryDate.SelectedValue, lblDeliveryType.Text);
                        updDelivery.Update();
                    }
                }

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        private void BindDeliveryOptionsBasedOnDate(string day, int productId, string selectedDate, string optionalName)
        {
            try
            {
                DispatchesBAL objDelivery = new DispatchesBAL();

                if (objDelivery != null)
                {
                    List<DeliveryTimeInfo> lstDeliveryDataOptionsItems = objDelivery.GetDeliveryOptionsByDeliveryDate(productId, day, selectedDate);
                 
                    if (lstDeliveryDataOptionsItems != null)
                    {
                       
                            rbtnLstDeliveryOptions.DataSource = lstDeliveryDataOptionsItems;
                            rbtnLstDeliveryOptions.DataTextField = "OptionName";
                            rbtnLstDeliveryOptions.DataValueField = "Id";
                            rbtnLstDeliveryOptions.DataBind();
                            rbtnLstDeliveryOptions.Visible = true;

                            if (!string.IsNullOrEmpty(optionalName))
                            {
                                rbtnLstDeliveryOptions.SelectedIndex = 0;
                                foreach (DeliveryTimeInfo objTimeInfo in lstDeliveryDataOptionsItems)
                                {
                                    if (objTimeInfo.OptionName.ToLower().Contains(optionalName.ToLower()))
                                    {

                                        ListItem selectedListItem = rbtnLstDeliveryOptions.Items.FindByText(objTimeInfo.OptionName);

                                        if (selectedListItem != null)
                                        {
                                            rbtnLstDeliveryOptions.Items.FindByText(objTimeInfo.OptionName).Selected = true;
                                            break;

                                        }

                                    }
                                }

                            }
                            else
                            {
                                rbtnLstDeliveryOptions.SelectedIndex = 0;
                            }
                        
                        
                    }
                    else
                    {
                        rbtnLstDeliveryOptions.DataSource = null;
                        rbtnLstDeliveryOptions.DataBind();
                        rbtnLstDeliveryOptions.Visible = false;

                    }


                }
                //StrDelOptinId = rbtnLstDeliveryOptions.SelectedValue;
                updDelivery.Update();

            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }
        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            DispatchesInfo objDispatches = new DispatchesInfo();
            OrderDetailsBAL objOrderdetails = new OrderDetailsBAL();
            DeliveryTimeInfo objDeliveryTimeInfo = new DeliveryTimeInfo();

            DispatchesBAL objDelivery = new DispatchesBAL();
            try
            {       
                        string orderId = Common.GetOrderIdFromQueryString();
                        objDispatches.OrderID = orderId;
                       // string eventArguments = Convert.ToString(Request["__EVENTARGUMENT"]);
                        //string[] delOptionsArray = eventArguments.Split('@');
                        //objDispatches.DelOptionID = Convert.ToInt32(delOptionsArray[1].TrimEnd(','));
                        //objDispatches.DeliveryDate = DateTime.ParseExact(delOptionsArray[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        objDispatches.DelOptionID = Convert.ToInt32(rbtnLstDeliveryOptions.SelectedValue);
                        objDispatches.DeliveryDate = DateTime.ParseExact(ddlDeliveryDate.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DispatchesInfo tmpDispatches = new DispatchesInfo();
                        tmpDispatches = objDelivery.GetDeliveryDetailsByDelOptionID(objDispatches.DelOptionID);
                        objDispatches.CarrierID = tmpDispatches.CarrierID;
                        objDispatches.DeliveryTime = tmpDispatches.DeliveryTime;
                        objDispatches.DeliveryPrice = tmpDispatches.DeliveryPrice;
                        objDelivery.ScheduleDispatch(objDispatches);                      
                        updDelivery.Update();

                        if (ButtonClick != null)
                        {
                            //fires the event passing the same arguments of the button
                            //click event
                            ButtonClick(sender, e);
                        }

                        Response.Redirect(Request.RawUrl);

                       // ScriptManager.RegisterStartupScript(SaveChanges, SaveChanges.GetType(), "ScrollPage", "CloseDialog();", true);
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }

        }

        public static void RemoveCookie(string key, HttpResponse response, HttpRequest request)
        {
            //check that the request object is valid
            if (request == null) return;
            //check that the response object is valid
            if (response == null) return;
            //check key is passed in
            if (string.IsNullOrEmpty(key)) return;

            //check if the cookie exists
            if (request.Cookies[key] != null)
            {
                //create a new cookie to replace the current cookie
                HttpCookie newCookie = new HttpCookie(key);
                //set the new cookie to expire 1 day ago
                newCookie.Expires = DateTime.Now.AddDays(-1d);
                //update the cookies collection on the response
                response.Cookies.Add(newCookie);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //string strOrderId = Common.GetOrderIdFromQueryString();
                //string strMessage = new OrderDetailsBAL().CheckDeliveryCutoffByOrderID(strOrderId);
                //if (String.IsNullOrEmpty(strMessage) == false)
                //{
                //    RemoveCookie("OrderId", Response, Request);
                //    Response.Redirect("http://" + Common.getRootUrl() + "/", false);
                //}
            }
            catch (Exception ex)
            {
                SFMobileLog.Error(ex);
            }
        }
        
    }
}