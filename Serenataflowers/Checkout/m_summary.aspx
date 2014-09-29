<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_summary.aspx.cs" Inherits="Serenataflowers.Checkout.m_summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
   <title id="Title1"  runat="server" visible="false"></title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport" />
    <link type="text/css" rel="stylesheet" href="../styles/m_style.css" />
   
    <style type="text/css" >
      
    </style>
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
 <script type="text/javascript">
     function chgQty(txtControlObj) {
         var qunatity = document.getElementById(txtControlObj.id).value;
         var priceId = txtControlObj.id.replace("txtQty", "lblPrice");
         var totalPriceId = txtControlObj.id.replace("txtQty", "lblTotal");
         var price = document.getElementById(priceId).innerHTML.replace("£", "");
         var totalPrice = formatCurrency(qunatity) * formatCurrency(price);
         document.getElementById(totalPriceId).innerHTML = "£" + formatCurrency(totalPrice);
     }
     function formatCurrency(num) {
         num = isNaN(num) || num === '' || num === null ? 0.00 : num;
         return parseFloat(num).toFixed(2);
     }
     function formatquntity(obj) {

         var p = obj;
         var pp = '';
         obj = obj.value;

         if (obj.length > 0) {
             pp = '';
             for (c = 0; c < obj.length; c++) {
                 if (isNaN(obj.charAt(c)) || obj.charAt(c) == ' ') {
                     //Assigned Default Value
                     pp = "1";
                     continue;
                 }
                 else {
                     pp = pp + obj.charAt(c);
                 }
             }
             p.value = pp;
         }
     }
    </script>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page_wrapper">
        <div id="content_normal">
            <!--Header DIV Start-->
            <div id="header">
                <uc1:Header ID="Header1" runat="server" EnableUrl="false" />
            </div>
            <!--Header DIV End-->
            <div class="clearleft">
            </div>
            <div class="divrow dottedline">
                <div style="width: 100%;">
                    <h1 style="font-size: 18px;">
                        Please confirm your delivery date and product selection</h1>
                </div>
            </div>
            <div class="divrow dottedline">
                <div style="width: 50%">
                    <h2 style="font-size: 14px;">
                        <b>Delivery date:</b></h2>
                    Select the date of delivery:<br />
                    <asp:DropDownList ID="ddlDeliveryDates" runat="server" AutoPostBack="true" CssClass="lfloat "
                        OnSelectedIndexChanged="ddlDeliveryDates_SelectedIndexChanged" />
                </div>
                <div style="width: 50%">
                    <br />
                    <h2 style="font-size: 14px;">
                        <b>Choose a time:</b></h2>
                    <div id="delOptionSection" style="width: 100%; !important">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" style="width:100%">
                            <ContentTemplate>
                                <asp:RadioButtonList CssClass="lfloat" ID="rbtnLstDeliveryOptions" runat="server"
                                    RepeatColumns="1" RepeatLayout="Flow">
                                </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDeliveryDates" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div style="clear: left;height:20px;">
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                            <HeaderTemplate>
                                <table id="tb" width="100%" class="m" cellspacing="1">
                                    <tr>
                                        <th class="top">
                                            Qty
                                        </th>
                                        <th class="top">
                                            Img
                                        </th>
                                        <th class="top">
                                            Product name
                                        </th>
                                        <th class="top">
                                            Price
                                        </th>
                                        <th class="top">
                                            Total
                                        </th>
                                        <th class="top">
                                            Delete
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtQty" Width="20px" Height="15px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'
                                            AutoPostBack="true" onblur="chgQty(this)" OnTextChanged="txtQty_TextChanged" onkeyup="formatquntity(this)" MaxLength="3"></asp:TextBox>
                                        <asp:HiddenField ID="hdnRptProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId")%>' />
                                    </td>
                                    <td>
                                        <asp:Image  Width="30" ID="imgProduct" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageSmallLow") %>'
                                            runat="server" />
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "ProductTitle") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:£0.00}")%>'
                                            runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" Text=' <%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:£0.00}")%>'
                                            runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgDelete" CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ProductId")%>'
                                            CommandName="Delete" runat="server" ImageUrl="https://checkout.serenataflowers.com/images/bin.gif" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </Table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ContentTemplate>                    
                </asp:UpdatePanel>
            </div>
            <div class="clear:left">
            </div>
            <div>
            <span id="spnMultiFP"  runat="server" style="color:red;padding:2px;font-size:smaller; border:1px solid red;text-align:center;display:block;margin-top:3px;">Sorry, You can't mix these products!</span>
            </div>
            <div>
             <br />
             <h2 style="font-size: 18px;" runat="server" id="h2AddExtra">
                                        3. Add some special extras</h2>
                                       
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <br />
                            <asp:DataList ID="dlExtras" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"   CssClass="ProductLists" >
                               
                                <ItemTemplate>
                                    <div onmouseout="this.className='upsell'" onmouseover="this.className='upsellonmouse'"
                                        class="upsell">
                                        
                                            <asp:Image ID="imgSpProdImg" runat="server" Width="65px" BorderWidth="0" AlternateText='<%# DataBinder.Eval(Container.DataItem, "ProductTitle") %>'
                                                ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageSmallLow") %>' CssClass="prodImage" />
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="upsellbottom">
                                            <span style="font: normal 11px tahoma, verdana, sans-serif;">
                                                <asp:Label ID="lblExtraPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price", "{0:£0.00}")%>'></asp:Label>
                                            </span>
                                            <asp:CheckBox ID="chkAddExtra" AutoPostBack="true" OnCheckedChanged="chkAddExtra_CheckedChanged"
                                                runat="server" />
                                            <asp:HiddenField ID="hdnSpProdId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductId") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                                
                            </asp:DataList>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="morediv">
                 
                    <asp:LinkButton ID="lnkMore" runat="server" onclick="lnkMore_Click" CssClass="nextbtn">More..</asp:LinkButton>
                </div>
            <div class="clearleft">
            </div>
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="divrow dottedlinetopbtm">
                        <asp:ImageButton runat="server" ID="imgBtnSave" ImageUrl="https://checkout.serenataflowers.com/images/savecheckout.gif"
                            Width="196" Height="46" OnClick="imgBtnSave_Click" />
                    </div>
           </ContentTemplate>
            </asp:UpdatePanel>
            <div class="clearleft">
            </div>
              <!--Footer DIV Start-->
            <div id="Div1">
                <p class="lfloat fott">
                      <asp:LinkButton ID="lnkContinueShopping" CssClass="btn" runat="server" 
                    onclick="lnkContinueShopping_Click" ><span>« continue shopping</span></asp:LinkButton>
          
                </p>
            </div>
            <div class="clearleft"></div>
            <br />
           
            <div id="FooterDiv">
                  <uc2:Footer ID="Footer1" runat="server" />
            </div>
            <!--Footer DIV End-->
        </div>
    </div>
    </form>
</body>
</html>
