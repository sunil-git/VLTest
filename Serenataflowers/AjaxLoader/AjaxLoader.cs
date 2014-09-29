using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Serenataflowers
{
    public class AjaxLoader : Page
    {
        protected override void OnLoad(EventArgs e)
        {
           if (!IsPostBack)
           {
              //Include CSS File
              //Page.Header.Controls.Add(new LiteralControl("<link href='Styles/m_ajaxload.css' rel='stylesheet' type='text/css' />"));


              //Include JS file on the page
              ClientScript.RegisterClientScriptInclude("ajaxload", ResolveUrl("~/Scripts/ajaxload.js"));

              //Writing declaration script 
              String script = "var prm = Sys.WebForms.PageRequestManager.getInstance();";
              script += "prm.add_initializeRequest(InitializeRequest);";
              script += "prm.add_endRequest(EndRequest);";
              ClientScript.RegisterStartupScript(typeof(string), "body", script, true);
              //if (!Request.AppRelativeCurrentExecutionFilePath.Contains("step1.aspx"))
              //{
              //   ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ShowModelPopUp", "InitializeRequest('', '');", true);
              //}
              base.OnLoad(e);
           }
        }

       

    }
}
