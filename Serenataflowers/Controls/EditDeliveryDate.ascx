<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDeliveryDate.ascx.cs" Inherits="Serenataflowers.Controls.EditDeliveryDate" %>
<script type="text/javascript">
    function CloseDialog() {
            $('.ui-dialog').dialog('close');
            // return true;


        }

    
    
    </script>

<div data-role="page" data-theme="a" id="DeliveryDate" data-title="Edit delivery date">


	<div data-role="footer" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:center;">Edit delivery date</h3>
	</div><!-- /header -->

	<div data-role="content" data-theme="a">	



    <span id="spanCutOffMsg" runat="server" style="color: red;font-weight: bold; display: block;margin-bottom:5px;" ></span>

		     <asp:UpdatePanel ID="updDelivery" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
		<label for="">Delivery date</label>
			 <asp:DropDownList ID="ddlDeliveryDate" runat="server" OnSelectedIndexChanged="ddlDeliveryDate_SelectedIndexChanged"
                      AutoPostBack="True">
                 </asp:DropDownList>
		
		<fieldset data-role="controlgroup" data-mini="true">
			<legend>Delivery time:</legend>
            <div style="margin-left:-40px">	
			<asp:RadioButtonList ID="rbtnLstDeliveryOptions" runat="server"  RepeatLayout="OrderedList" 
                      RepeatColumns="1">
                 </asp:RadioButtonList>
                   </div>
		</fieldset>
		
		<div style="clear:both;"></div>
		




		 </ContentTemplate>
     </asp:UpdatePanel>		
		<%--<asp:Button ID="btnTest2" runat="server" OnClientClick="return CloseDialog();"  Text="Test2" />--%>
        		<div align="center" style="margin:5px 0 5px 0;">
         <asp:Button ID="SaveChanges" runat="server" OnClientClick="CloseDialog();" Text="Save changes" OnClick="SaveChanges_Click" />

         <asp:Button ID="btnCancel" runat="server" OnClientClick="return CloseDialog();"  Text="Cancel" OnClick="btnCancel_Click" />
			
           <%-- <asp:Button ID="btnTest1" runat="server" OnClientClick="return CloseDialog();"  Text="Test1" />--%>

		</div>
	</div><!-- /content -->
	
	<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div><!-- /footer -->
</div>