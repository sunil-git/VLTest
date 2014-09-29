using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serenataflowers
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["er"] != null)
            {
                if (Request.QueryString["Desc2"] != null)
                {
                    errMessage.InnerText = Request.QueryString["Desc2"].ToString();
                }
                else if (Request.QueryString["Desc1"] != null)
                {
                    errMessage.InnerText = Request.QueryString["Desc1"].ToString();
                }
            }
        }
        

        protected void Revist_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
    }
}