<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.ascx.cs" Inherits="Serenataflowers.Controls.ContactUs" %>
    <script type="text/javascript">
        // Contact Us fields
        var ContactUsNameElementID = 'UserContactUs_ContactUsName';
        var ContactUsEmailElementID = 'UserContactUs_ContactUsEmail';
        var ContactUsQueriesElementID = 'ContactUsQueries';
function ValidateContactUsName(e) {
    var ContactName = document.getElementById(ContactUsNameElementID).value;
    if (ContactName == '') {
        document.getElementById(ContactUsNameElementID).style.background = '#f9c2c2';
        document.getElementById(ContactUsNameElementID).style.border = '1px solid red';
        document.getElementById('ErrorContactUsName').style.display = 'block';
    }
    else {
        document.getElementById(ContactUsNameElementID).style.background = '';
        document.getElementById(ContactUsNameElementID).style.border = '';
        document.getElementById('ErrorContactUsName').style.display = 'none';
    }
//    alert('name - ' + ContactName);
    return (ContactName);
}
function ValidateContactUsEmail(e) {
    var email = document.getElementById('UserContactUs_ContactUsEmail').value;

    if (email == '') {
        document.getElementById("UserContactUs_ContactUsEmail").style.background = '#f9c2c2';
        document.getElementById("UserContactUs_ContactUsEmail").style.border = '1px solid red';  
        document.getElementById('ErrorContactUsEmail').style.display = 'block';
    }
    else {

        if (validateEmailAddress(email) == false) {
            document.getElementById("UserContactUs_ContactUsEmail").style.background = '#f9c2c2';
            document.getElementById("UserContactUs_ContactUsEmail").style.border = '1px solid red';  
            document.getElementById('ErrorContactUsEmail').style.display = 'block';
            email = '';
        }
        else {
            document.getElementById("UserContactUs_ContactUsEmail").style.background = '';
            document.getElementById("UserContactUs_ContactUsEmail").style.border = '';  
            document.getElementById('ErrorContactUsEmail').style.display = 'none';
        }
    }
  //  alert('email - ' + email);
    return (email);
}
function ValidateContactUsQueries(e) {
    var ContactDropdown = '';
    if (CheckListSelected() == false) {
        
        document.getElementById('ErrorContactUsQueries').style.display = 'block';
        ContactDropdown = '';
    }
    else {
      
        document.getElementById('ErrorContactUsQueries').style.display = 'none';
        ContactDropdown = 'Value Exist';
    }
    //alert('dropdown - ' + ContactDropdown);
    return (ContactDropdown);
}
function ShowErrorContactUs() {
    var ContactName = document.getElementById('UserContactUs_ContactUsName').value;
    var ContactEmail = document.getElementById('UserContactUs_ContactUsEmail').value;

    var ElementId = '';
    if (ContactName == '') {
        document.getElementById('UserContactUs_ContactUsName').focus();
        document.getElementById("UserContactUs_ContactUsName").style.background = '#f9c2c2';
        document.getElementById("UserContactUs_ContactUsName").style.border = '1px solid red';
        document.getElementById('ErrorContactUsName').style.display = 'block';

    }
    else {
        document.getElementById("UserContactUs_ContactUsName").style.background = '';
        document.getElementById("UserContactUs_ContactUsName").style.border = '';
        document.getElementById('ErrorContactUsName').style.display = 'none';
     }
    if (ContactEmail == '') {
        document.getElementById('UserContactUs_ContactUsEmail').focus();
        document.getElementById("UserContactUs_ContactUsEmail").style.background = '#f9c2c2';
        document.getElementById("UserContactUs_ContactUsEmail").style.border = '1px solid red';  
        document.getElementById('ErrorContactUsEmail').style.display = 'block';
    } else {
        document.getElementById("UserContactUs_ContactUsEmail").style.background = '';
        document.getElementById("UserContactUs_ContactUsEmail").style.border = '';  
    document.getElementById('ErrorContactUsEmail').style.display = 'none'; }



    if (CheckListSelected() == false) {
        document.getElementById('ContactUsQueries').focus();
        document.getElementById('ErrorContactUsQueries').style.display = 'block';
    }
    else {
        document.getElementById('ErrorContactUsQueries').style.display = 'none';
    }
    

}
function ValidateContactUs() {
    var returnValue = false;
    returnValue = ((ValidateContactUsName(true) != '') && (ValidateContactUsEmail(true) != '') && (ValidateContactUsQueries(true) != '')) ? true : false;
    //alert('return  - ' + returnValue);
    if (returnValue == true) {
        $('.ui-dialog').dialog('close');
        return true;
    }
    else {
        ShowErrorContactUs();

        return false;

    }
    
}
function validateEmailAddress(addr) {

    if (addr == '') {
        return false;
    }
    if (addr == '') return true;
    var invalidChars = '\/\'\\ ";:?!()[]\{\}^|';
    for (i = 0; i < invalidChars.length; i++) {
        if (addr.indexOf(invalidChars.charAt(i), 0) > -1) {
            return false;
        }
    }
    for (i = 0; i < addr.length; i++) {
        if (addr.charCodeAt(i) > 127) {

            return false;
        }
    }

    var atPos = addr.indexOf('@', 0);
    if (atPos == -1) {

        return false;
    }
    if (atPos == 0) {

        return false;
    }
    if (addr.indexOf('@', atPos + 1) > -1) {

        return false;
    }
    if (addr.indexOf('.', atPos) == -1) {

        return false;
    }
    if (addr.indexOf('@.', 0) != -1) {

        return false;
    }
    if (addr.indexOf('.@', 0) != -1) {

        return false;
    }
    if (addr.indexOf('..', 0) != -1) {

        return false;
    }
    var suffix = addr.substring(addr.lastIndexOf('.') + 1);
    if (suffix.length != 2 && suffix != 'com' && suffix != 'net' && suffix != 'org' && suffix != 'edu' && suffix != 'int' && suffix != 'mil' && suffix != 'gov' & suffix != 'arpa' && suffix != 'biz' && suffix != 'aero' && suffix != 'name' && suffix != 'coop' && suffix != 'info' && suffix != 'pro' && suffix != 'museum') {

        return false;
    }
    return true;
}
function CheckListSelected() {
    var element = document.getElementById('ContactUsQueries');
    var addressID = element.selectedIndex;
    if (addressID >= 1) {
        return true;
    }
    else {

        return false;
    }
}
function OpenSuccess() {
   
    $.mobile.changePage('#ContactSuccess', { transition: 'pop', role: 'dialog' }); 

   
}
   </script>

   <style>
           textarea.textMsg
        {
            height:200px !important;            
        }
   
   
   </style>
<div data-role="page" data-theme="a" id="Contact" data-title="Contact us">
<!-- header -->
	<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Contact us</h3>
	</div>
<!-- header -->
<!-- content -->
	<div data-role="content" data-theme="a">
	  <asp:UpdatePanel ID="udpContactUs" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
		<label for="name"><span style="color:red;">*</span> Name:</label>
        <textarea rows="1" ID="ContactUsName" runat="server"  placeholder="e.g. John Smith"  onblur="ValidateContactUsName(true);"></textarea>
		 <span class="errortext" id="ErrorContactUsName" style="display:none" >Please enter Name.</span>
		
		<label for="email"><span style="color:red;">*</span> Email:</label>
        <textarea rows="1" ID="ContactUsEmail" runat="server"  placeholder="e.g. mark@mail.com"  onblur="ValidateContactUsEmail(true);"></textarea>
        <span style="display:none" class="errortext" id="ErrorContactUsEmail">Please enter Email.</span>
		
		<label for="phone">Phone:</label>
        <textarea rows="1" ID="ContactUsPhone" runat="server"  placeholder="0751223456"></textarea>
		<label for="orderno">Order Number:</label>
		<textarea rows="1" ID="ContactUsOrderNumber" runat="server"  placeholder="e.g. M123456"></textarea>	
		
		<label for="">Question relates to:</label>
			<asp:DropDownList runat="server" ID="ContactUsQueries" ClientIDMode="Static" style="padding:5px;">
                <asp:ListItem Value="-1" Selected="True" Text="Please choose a reason" />
                <asp:ListItem Value="1" Text="To complete the purchase" />
                <asp:ListItem Value="2" Text="To amend my order" />
                <asp:ListItem Value="3" Text="To track my order" />
                <asp:ListItem Value="4" Text="To raise an issue with delivery or timeliness of delivery" />
                <asp:ListItem Value="5" Text="To raise an issue about the product quality" />
                <asp:ListItem Value="6" Text="To find out about a product" />
                <asp:ListItem Value="12" Text="Did you receive my order?" />
                <asp:ListItem Value="10" Text="Wedding enquiry" />
                <asp:ListItem Value="11" Text="Funeral enquiry" />
                <asp:ListItem Value="13" Text="Sending flowers overseas" />
                <asp:ListItem Value="7" Text="Other enquiry" />
            </asp:DropDownList>
		 <span style="display:none" class="errortext" id="ErrorContactUsQueries">Please choose a reason.</span>
		<label for="message">Message:</label>		
        <textarea id="ContactUsMessage" class="textMsg" clientidmode="Static" runat="server"></textarea>
		
		
		<div style="clear:both;"></div>
		
		<div align="center" style="margin:5px 0 5px 0;">
         <asp:Button ID="SubmitRequest" runat="server" Text="Submit" OnClientClick="javascript:return ValidateContactUs();" OnClick="SubmitRequest_Click" />
			
		</div>

		   </ContentTemplate>
           <Triggers>
           <asp:AsyncPostBackTrigger ControlID="SubmitRequest" EventName="Click" />
           </Triggers>
  </asp:UpdatePanel>		
			
	</div>
	<!-- content -->
    <!-- footer -->
	<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
       <!-- footer -->
</div>
<div data-role="dialog" data-theme="a" id="ContactSuccess" data-title="Contact Message">
<div data-role="header" data-theme="a" style="text-align:left;">	
		<h3 style="text-align:left;">Thanks you</h3>
	</div>
                    <table id="tblMessage" runat="server" style="color: green; display: block;">
                        <tr>
                            <td>
                                    <asp:Label ID="lblErrorInfo"  ForeColor="green" runat="server" text="Thank you! One of our staff will come back to you as quick as possible. " Style="color: green;
                                        font-weight: bold; display: block;"></asp:Label>
                            </td>
                        </tr>
                    </table>
<div data-role="footer" style="padding:15px 0 15px 0;text-align:center;">		
		
	</div>
</div>