<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Voucher.ascx.cs" Inherits="Serenataflowers.Controls.Voucher" %>

    <script type="text/javascript">
function ValidateVoucherCode() {
    var VoucherVal = document.getElementById("VoucherCode_DiscountVoucher").value;
    if (VoucherVal == '') {
        document.getElementById("VoucherCode_ErrorVoucher").style.display = 'block';
        document.getElementById("VoucherCode_ErrorVoucher").innerText = "Please enter a valid voucher code.";
        document.getElementById("VoucherCode_DiscountVoucher").style.background = '#f9c2c2';
        document.getElementById("VoucherCode_DiscountVoucher").style.border = '1px solid red';
        
        return false;
    }
    else {
        document.getElementById("VoucherCode_ErrorVoucher").style.display = 'none';
        return true;
    }

}
function WarnUser() {
    document.getElementById("VoucherCode_DiscountVoucher").style.background = '#f9c2c2';
    document.getElementById("VoucherCode_DiscountVoucher").style.border = '1px solid red';
    document.getElementById("VoucherCode_ErrorVoucher").style.display = 'block';
}
function Close() {
    //$('.ui-dialog').dialog('close');
    showEditBasket();
}
   </script>
   <style type="text/css">
.errorVoucher 
{
    background:#f9c2c2;
    border:1px solid red;
}
   
</style>
<div data-role="page" data-theme="a" id="Voucher" data-title="Voucher code">

<!-- header -->
	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Voucher code</h3>
	</div>
<!-- header -->
<!-- /content -->

	<div data-role="content" data-theme="a" id="divVocher">
       <asp:UpdatePanel runat="server" ID="upVoucher" UpdateMode="Conditional">
    <ContentTemplate>
		<label for="name">Voucher code:</label>
     
		 <asp:TextBox ID="DiscountVoucher" runat="server" ></asp:TextBox>
              
		<span class="errortext" id="ErrorVoucher" style="display:none" runat="server">Please enter a valid voucher
                    code.</span>

		<div style="clear:both;"></div>
		
		<div align="center" style="margin:5px 0 5px 0;">
         <asp:Button ID="Submit" runat="server"   OnClientClick="javascript:return ValidateVoucherCode();" Text="Submit" OnClick="Submit_Click" />
			</ContentTemplate>
</asp:UpdatePanel>
		</div>
           
				
			
	</div>

	<!-- content -->

	<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
       <!-- footer -->
</div>
