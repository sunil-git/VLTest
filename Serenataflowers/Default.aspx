<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Serenataflowers.Default" EnableEventValidation="false"%>
<%--<%@ OutputCache Duration="1000" VaryByControl="ProductList" Location="Client" VaryByParam="none" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<asp:Literal ID="ltTitle"  runat="server"></asp:Literal>
<title id="hometitle" runat="server" visible="false"></title>
<link href="Styles/m_style.css" rel="Stylesheet" type="text/css" />
<meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">   
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
 <script language="javascript" type="text/javascript">

     function __doPostBack(eventTarget, eventArgument) {
         if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
             theForm.__EVENTTARGET.value = eventTarget;
             theForm.__EVENTARGUMENT.value = eventArgument;
             theForm.submit();
         }
     }
     function showCatDescMore() {
         document.getElementById('spntxt').style.display = 'block';
         document.getElementById('lblcontentDesc').textContent = document.getElementById('spntxt').textContent;
         document.getElementById('spntxt').style.display = 'none';
     }
     function postthis(obj) {
         if (obj.id == "drpCountry") {
             if (document.getElementById(obj.id).length > 1) {
                 revealModal();
                 __doPostBack('<%=drpCountry.UniqueID %>', '');
                 return true;
             }
             else {
                 return false;
             }
         }
         else {
             if (document.getElementById(obj.id).length > 1) {
                 revealModal();
                 __doPostBack('<%=drpCategory.UniqueID %>', '');
                 return true;
             }
             else {
                 return false;
             }
         }
     }
     function revealModal() {
         //debugger;
         document.getElementById('modalPage').style.display = "block";
     }

     function hideModal() {
         //debugger;
         document.getElementById('modalPage').style.display = "none";
     }
</script> 
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
     <div id="modalPage" class="ModalProgressContainer" style="height:50000px">
        <div class="ModalProgressContent">
        <img src="images/ajax-loader.gif" alt="" />
        <div>Loading....</div>
        </div>
    </div>
            <div id="page_wrapper">
                <!--CONTENET DIV Start-->
                <div id="content_normal">
                    <!--Header DIV Start-->
                    <div id="header">
                        <uc1:Header ID="Header1" runat="server" EnableUrl="true" />
                    </div>
                    <!--Header DIV End-->
                    <!--Banner DIV Start-->
                    <div class="banner">
                        <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
             <ContentTemplate>--%>
                        <h1>
                            <asp:Label ID="lblContentTitle" runat="server" Text=""></asp:Label></h1>
                        <p>
                            <asp:Label ID="lblcontentDesc" runat="server" Text=""></asp:Label>
                            <span id="spntxt" runat="server" style="display:none"></span>
                            </p><a href="#" runat="server" id="ancBanner">
                        <img alt="" border="0" runat="server" id="banner" visible="false"></a>
                        <%--</ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />  
                  <asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" /> 
                </Triggers>
                </asp:UpdatePanel>--%>
                    </div>
                    <!--Banner DIV End-->
                    <!--Category DIV Start-->
                    <div class="fleft categorydiv">
                        <h4 style="margin: 0 0 12px;">
                            Categories:</h4>
                        <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
               <ContentTemplate>--%>
                        <asp:DropDownList ID="drpCategory" runat="server" CssClass="dropdown1" AutoPostBack="True"
                            onchange="postthis(this)" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--</ContentTemplate>
                <Triggers >
                <asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" />
                </Triggers>
               
                 </asp:UpdatePanel>--%>
                    </div>
                    <!--Category DIV End-->
                    <!--Country DIV Start-->
                    <div class="fleft">
                        <h4 style="margin: 0 0 12px;">
                            Product Type:</h4>
                        <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>
                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="dropdown1" AutoPostBack="True"
                           onchange="postthis(this)" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%-- </ContentTemplate>
                <Triggers >
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
                </Triggers>
               
                 </asp:UpdatePanel>--%>
                    </div>
                    <!--Country DIV End-->
                    <div class="clear">
                    </div>
                    <!--Content Div-->
                    <div id="DivPList" runat="server">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
               <ContentTemplate>
                        <asp:DataList ID="ProductList" runat="server" RepeatDirection="Horizontal" CssClass="ProductLists"
                            RepeatLayout="Flow" OnItemDataBound="ProductList_ItemDataBound">
                            <ItemTemplate>
                                <div runat="server" id="divImage" class="prodimg">
                                    <div class="prodoverlay_wrapper">
                                        <a href='<%# Eval("ProductId", "ViewProduct.aspx?ProdId={0}") %>' id="ancOrdernow" runat="server">
                                            <img class="prodImage" src='<%# DataBinder.Eval(Container.DataItem, "ImagePath")%>'
                                                alt='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' width="135" border="0"
                                                height="158">
                                        </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                    <div class="prodimgbottom">
                                        <h5>
                                            <a href='<%# Eval("ProductId", "ViewProduct.aspx?ProdId={0}") %>' id="ancname" runat="server">
                                                <%# DataBinder.Eval(Container.DataItem, "ProductName")%></a></h5>
                                        <span class="oldprice" id="oldprice" runat="server">£<%# String.Format("{0:0.##}",DataBinder.Eval(Container.DataItem, "ProductOldPrice"))%></span><span
                                            class="offerprice" id="offerprice" runat="server">&nbsp;£<%# String.Format("{0:0.##}",DataBinder.Eval(Container.DataItem, "ProductOfferPrice"))%></span></div>
                                    <div class="prodbuttons probtnmar">
                                        <div class="lfloat">
                                            <a class="btnorange"  title='<%# Eval("ProductName", "{0} - Order ") %>'  id="ancbtn" runat="server"
                                                href='<%# Eval("ProductId", "ViewProduct.aspx?ProdId={0}") %>'><span>Order now »</span></a>
                                               
                                        </div>
                                    </div>
                                      <div style="height:33px;">
                                     <div class="ratting" runat="server" id="divStar">
                                    <div id="half" style="float:left;">
                                    <img border="0" src='<%# DataBinder.Eval(Container.DataItem, "StarRatingImageURL")%>'>
                                    </div>
                                    <div id="divratVal" runat="server" style="float:left;margin-top:2px;">
                                    <span style="float:left;font:normal 11px tahoma, sans-serif;color:#666;margin:1px 0 0 8px;"><b style="color:#000;"><%# DataBinder.Eval(Container.DataItem, "StarRating")%></b>/5</span>
                                    </div>
                                     </div>
                                     <div id="divcust" runat="server" align="left" style="width:150px;margin-left:18px;margin-top:-5px;font :normal 10px tahoma, sans-serif;">
                                     <a href='<%# Eval("ProductId", "ViewProduct.aspx?ProdId={0}") %>'>based on <%# DataBinder.Eval(Container.DataItem, "TotalReviews")%> reviews</a>
                                    </div>
                                    </div>
                                     <div id="divTotalReview" runat="server" visible="false">
                                     <%# DataBinder.Eval(Container.DataItem, "TotalReviews")%>
                                     </div>
                                </div>

                            </ItemTemplate>
                        </asp:DataList>
                       
                         </ContentTemplate>
                <Triggers >
              <%--  <asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" />               
                   <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />  --%>
                <asp:AsyncPostBackTrigger ControlID="lnkMore" EventName="Click" />                              
                </Triggers>               
                 </asp:UpdatePanel>
                    </div>
                    <!--Content Div-->
                   
                    <div class="morediv">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <ContentTemplate>  
                        <asp:LinkButton ID="lnkMore" runat="server" OnClick="lnkMore_Click" CssClass="nextbtn">more products...</asp:LinkButton>
                        </ContentTemplate>
                <Triggers>
               <%-- <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" /> --%>
              <asp:AsyncPostBackTrigger ControlID="lnkMore" EventName="Click" />
              
                </Triggers>
                </asp:UpdatePanel>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="prodbuttons probtnmar" runat="server" id="dvcheckout" style="display:none">
                       <div class="lfloat">
                         <a class="btnorange"   id="ancsavebtn" runat="server" href="#"><span>checkout »</span></a>
                      </div>
                   </div>
                  <div class="clear">
                    </div>
                      <!--Footer DIV Start-->
                    <div id="FooterDiv">
                        <uc2:Footer ID="Footer1" runat="server" />
                    </div>
                    <!--Footer DIV End-->
                </div>
                <!--CONTENET DIV End-->
            </div>
         <asp:UpdatePanel ID="PIONPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>                  
                <asp:Literal ID="pion" runat="server" ></asp:Literal>                               
        </ContentTemplate>            
                <Triggers>              
              <asp:AsyncPostBackTrigger ControlID="lnkMore" EventName="Click" />              
                </Triggers>
                </asp:UpdatePanel>   
    </form>
    
  
</body>
</html>
