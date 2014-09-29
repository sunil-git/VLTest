<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ETEmailVerfiy.ascx.cs"
    Inherits="Serenataflowers.Controls.ETEmailVerify" %>
   
<div data-role="page" data-theme="a" id="EmailVerify" data-title="Email Verfication">
 <script >

     function closeETverifyPopup() {
         $('.ui-dialog').dialog('close');
     }
</script>
    <div data-role="header" data-theme="a" style="text-align: left;" >   
        <h3 style="text-align: center;">
            Email Verification</h3>
    </div>
    <div data-role="content" data-theme="a">
    				  <asp:UpdatePanel ID="udpEmailVerification" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
  <div>
                
                            <asp:Label ID="Label2" ForeColor="green" runat="server" Text="Please enter valid email address."
                                Style="color: green; font-weight: bold; display: block;"></asp:Label>
                       <textarea rows="1"  ID="ValidateETEmail" ClientIDMode="Static" runat="server" placeholder="e.g. mark@mail.com"></textarea>
                     <span class="errortext"  id="ErrorSignIn"  style="display:none" runat="server">Invalid Login</span>
                <asp:Button ID="btnEmailVerify" runat="server" data-theme="d" Text="Verify" OnClick="btnEmailVerify_Click"/>
                   </div>   
                </ContentTemplate>
           <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnEmailVerify" EventName="Click" />
           </Triggers>
  </asp:UpdatePanel>	
           
    </div>
    <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
    </div>
</div>
