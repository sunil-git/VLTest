using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using log4net;
using log4net.Config;
using System.Web.UI.HtmlControls;
using Serenata_Checkout.ChilkatComponent;
using Serenata_Checkout.Logic;
using Serenata_Checkout.Dto;
using Serenata_Checkout.Bal;

namespace Serenata_Checkout.Ui
{
    public partial class TestMail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = new Serenata_Checkout.Logic.Common().GetServerDateTime().ToString();
        }
    }
}