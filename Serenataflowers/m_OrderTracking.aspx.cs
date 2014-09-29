using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serenata_Checkout.Bal;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Logic;
using Serenata_Checkout.ChilkatComponent;
using SerenataOrderSchemaBAL;
using SFMobile.Exceptions;
namespace Serenataflowers
{
    public partial class m_OrderTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                divresult.Visible = false;
            
            
            }
        }

        protected void TrackOrder_Click(object sender, EventArgs e)
        {
            OrderDetailsBAL objOrderTrackingDetails = new OrderDetailsBAL();
            OrderTracking objOrderTracking = new OrderTracking();
            try
            {
                if (txtOrder.Value.Trim() != "")
                {
                    objOrderTracking = objOrderTrackingDetails.GetOrderTrackingInfo(txtOrder.Value);
                    if (objOrderTracking.OrderStatusName != null)
                    {
                        
                        switch (objOrderTracking.OrderStatusName)
                        {
                            case "Order Dispatched":
                                OrderStatus.InnerText = "our order is out for delivery";
                                break;
                            case "Confirmed Delivery":
                                OrderStatus.InnerText = "Your order has been delivered";
                                break;
                            case "Failed Delivery":
                                OrderStatus.InnerText = "Failed delivery";
                                break;
                            case "Order Accepted":
                                OrderStatus.InnerText = "We have received your order";
                                break;
                            case "Order Received":
                                OrderStatus.InnerText = "We have received your order";
                                break;
                            case "On hold":
                                OrderStatus.InnerText = "On hold, please contact us asap";
                                break;
                            case "Assigned to production":
                                OrderStatus.InnerText = "We are preparing your order";
                                break;
                            case "Accepted by courier (hub scan)":
                                OrderStatus.InnerText = "Your order is out for delivery";
                                break;
                            case "Arrived at local depot":
                                OrderStatus.InnerText = "Your order is out for delivery";
                                break;
                            case "Needs review":
                                OrderStatus.InnerText = "Needs review, please contact us asap";
                                break;
                            case "Order streamed to partner (awaiting acceptance)":
                                OrderStatus.InnerText = "Awaiting acceptance from florist";
                                break;
                            case "Order accepted by partner":
                                OrderStatus.InnerText = "Your flowers are being made up";
                                break;
                            case "Unfinished order":
                                OrderStatus.InnerText = "You have an unfinished order";
                                break;
                            default:
                                OrderStatus.InnerText = "We are unable to find your order";
                                break;
                        }
                       // DateTime dt;
                       // dt = DateTime.ParseExact(objOrderTracking.DeliveryDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                       //if( objOrderTracking.OrderStatusName== "Assigned to production"  &&  dt==DateTime.Now)
                       // {
                       //  OrderStatus.InnerText = "Your order is out for delivery..";
                       // }

                       divresult.Visible = true;
                    }
                    else {
                        divresult.Visible = true;
                        OrderStatus.InnerText = "We are unable to find your order";
                    }
                }
                else {

                    divresult.Visible = true;
                    OrderStatus.InnerText = "We are unable to find your order";
                
                }
            }
            catch (Exception ex)
            {

                SFMobileLog.Error(ex);
            
            }
        }
    }
}