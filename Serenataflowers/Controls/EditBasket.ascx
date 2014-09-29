<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditBasket.ascx.cs" Inherits="Serenataflowers.Controls.EditBasket" EnableViewState="true" %>

  <script type="text/javascript">
      function BaseURL1() {
          var totalQt = document.getElementById("ModifyBasket_hdnTotal").value
          var displaySave = document.getElementById("ModifyBasket_hdnDisplaySave").value;
          //alert('totalQt' + totalQt);
          //alert('displaySave' + displaySave);
          if (totalQt <= 0 && displaySave == "false") {

              document.getElementById("RowOption").style.display = "table-row"
              document.getElementById("RowOption1").style.display = "table-row"
              document.getElementById("RowWarn").style.display = "table-row";
              document.getElementById("RowSave").style.display = "none";
              document.getElementById("RowWarnQty").style.display = "none";
              document.getElementById("ModifyBasket_hdnDisplaySave").value = "true";
              //$('.ui-dialog').dialog('close');
              return false;
          }
          else {
              document.getElementById("ModifyBasket_hdnDisplaySave").value = "false";
              document.getElementById("RowWarnQty").style.display = "none";
              document.getElementById('ModifyBasket_EditBasketheader').innerHTML = "Your Basket";
              $('.ui-dialog').dialog('close');
              return true;
          }
          
      }
      function chgQtyOndelete(txtControlObj) {

          var priceId = txtControlObj.id.replace("Delete", "hdnPrice");
          var qunatitytxt = txtControlObj.id.replace("Delete", "txtQuntity");
          var totalPriceId = txtControlObj.id.replace("Delete", "lblQtyPrice");
          var productLabel = txtControlObj.id.replace("Delete", "hdnIsMainProduct");
          var IsMainProduct = document.getElementById(productLabel).value;         
          var hdnActQty = txtControlObj.id.replace("Delete", "hdnActQty");
          var ActQty = document.getElementById(hdnActQty).value;
          var totalQt = document.getElementById("ModifyBasket_hdnTotal").value;
          var price = document.getElementById(priceId).value;
          var totalPrice = formatCurrency(qunatity) * formatCurrency(price);
          var qunatity = document.getElementById(qunatitytxt).value;
          if (qunatity.length == 0) {
              document.getElementById(qunatitytxt).value = 0;
              qunatity = 0;
          }

          document.getElementById(totalPriceId).innerHTML = "£" + formatCurrency(totalPrice);
          if (IsMainProduct == 'True' && (parseInt(totalQt) - parseInt(qunatity)) <= 0) {
            
              document.getElementById("ModifyBasket_hdnIsLastProduct").value = totalQt;
             
              totalQt = parseInt(totalQt) - parseInt(qunatity);

              document.getElementById("RowOption").style.display = "table-row"
              document.getElementById("RowOption1").style.display = "table-row"
              document.getElementById("RowWarn").style.display = "table-row";
              document.getElementById("RowSave").style.display = "none";
              document.getElementById("ModifyBasket_hdnDisplaySave").value = "true";

              document.getElementById("ModifyBasket_hdnTotal").value = totalQt;
              document.getElementById(qunatitytxt).value = 0;
              document.getElementById(hdnActQty).value = qunatity;
              document.getElementById("ModifyBasket_hdnTotal").value = 0;
              document.getElementById("ModifyBasket_hdnDisplaySave").value = "true";

              return false;
          }
          else if (IsMainProduct == 'True' && (parseInt(totalQt) - parseInt(qunatity)) < parseInt(totalQt)) {
              document.getElementById("ModifyBasket_hdnIsLastProduct").value = 0;
               
              totalQt = parseInt(totalQt) - parseInt(qunatity);

              document.getElementById("RowOption").style.display = "none"
              document.getElementById("RowOption1").style.display = "none"
              document.getElementById("RowWarn").style.display = "none";
              document.getElementById("RowSave").style.display = "table-row";
              document.getElementById("ModifyBasket_hdnTotal").value = totalQt;
              document.getElementById(hdnActQty).value = qunatity;
              document.getElementById("ModifyBasket_hdnDisplaySave").value = "false";
              return true;
          }
          else if (IsMainProduct == 'False') {
        
              if (document.getElementById("ModifyBasket_hdnIsLastProduct").value > 0) {
                
                  document.getElementById("ModifyBasket_hdnTotal").value = document.getElementById("ModifyBasket_hdnIsLastProduct").value;

              }
              document.getElementById("RowOption").style.display = "none"
              document.getElementById("RowOption1").style.display = "none"
              document.getElementById("RowWarn").style.display = "none";
              document.getElementById("RowWarnQty").style.display = "none";
              document.getElementById("RowSave").style.display = "table-row";
              if (totalQt <= 0) {
                  document.getElementById("ModifyBasket_hdnDisplaySave").value = "false";
              }
             
              return true;
          }
      }
      function chgQty(txtControlObj) {
          var qunatity = document.getElementById(txtControlObj.id).value;

          if (qunatity.length == 0) {

              document.getElementById(txtControlObj.id).value = 0;
              qunatity = 0;

          }
          qunatity = parseInt(qunatity);

          var productLabel = txtControlObj.id.replace("txtQuntity", "hdnIsMainProduct");
          var IsMainProduct = document.getElementById(productLabel).value;

          if (IsMainProduct ==  "True") {
              if (qunatity <= 1) {
                  document.getElementById("RowWarnQty").style.display = "none";
              }
              else {
                  document.getElementById("RowWarnQty").style.display = "block";
                  document.getElementById("RowWarnQty").style.display = "table-row";
              }
          }

          var priceId = txtControlObj.id.replace("txtQuntity", "hdnPrice");
          var totalPriceId = txtControlObj.id.replace("txtQuntity", "lblQtyPrice");
          var price = document.getElementById(priceId).value;
          document.getElementById('ModifyBasket_hdnActPrice').value = document.getElementById(totalPriceId).innerHTML.replace("£", "");

          var totalPrice = formatCurrency(qunatity) * formatCurrency(price);
          var prevtotal = document.getElementById('ModifyBasket_lblTotal').innerHTML.replace("£", "");


          document.getElementById(totalPriceId).innerHTML = "£" + formatCurrency(totalPrice);
          var ActPrice = document.getElementById('ModifyBasket_hdnActPrice').value

          //alert("total" + formatCurrency(totalPrice));
          //alert("pre"+prevtotal);
          //alert("p" + ActPrice);
          var deliveyPrice = 0;
          var grandtotal = 0;
          var grandtotal1 = 0;
          // alert("DeliveryText" + document.getElementById('ModifyBasket_lblDeliveryPrice').innerHTML);
          if (document.getElementById('ModifyBasket_lblDeliveryPrice').innerHTML != "FREE") {
              deliveyPrice = document.getElementById('ModifyBasket_lblDeliveryPrice').innerHTML.replace("£", "");
              grandtotal = formatCurrency(parseFloat(prevtotal) - formatCurrency(parseFloat(deliveyPrice)) + parseFloat(totalPrice) - parseFloat(ActPrice));
              grandtotal1 = parseFloat(grandtotal) + parseFloat(deliveyPrice)
          }
          else {
              grandtotal = formatCurrency(parseFloat(prevtotal) + parseFloat(totalPrice) - parseFloat(ActPrice));
              grandtotal1 = parseFloat(grandtotal) + parseFloat(deliveyPrice)
          }

          //alert(grandtotal1);
          document.getElementById('ModifyBasket_lblTotal').innerHTML = "£" + grandtotal1;


//          var productLabel = txtControlObj.id.replace("txtQuntity", "hdnIsMainProduct");
//          var IsMainProduct = document.getElementById(productLabel).value;
          //var buttonCancel = txtControlObj.id.replace("txtQuntity", "btnCancel");
          var hdnActQty = txtControlObj.id.replace("txtQuntity", "hdnActQty");
          var ActQty = document.getElementById(hdnActQty).value;
          //alert("ss" + ActQty);
          //alert("try" + qunatity);
          //alert("IsMainProduct" + IsMainProduct);
          //alert("totalQt" + totalQt); 
          var totalQt = document.getElementById("ModifyBasket_hdnTotal").value;
          if (IsMainProduct == 'True' && parseInt(totalQt) == 0 && parseInt(qunatity) > 0) {
              totalQt = parseInt(totalQt) + parseInt(qunatity);

          }
          else if (IsMainProduct == 'True' && parseInt(ActQty) > parseInt(qunatity)) {

              totalQt = parseInt(totalQt) - (parseInt(ActQty) - parseInt(qunatity));
              //alert("t2:" + totalQt);
          }
          else if (IsMainProduct == 'True' && parseInt(qunatity) > parseInt(ActQty)) {
              totalQt = parseInt(totalQt) + (parseInt(qunatity) - parseInt(ActQty));
              //alert("t3:" + totalQt);
          }

          //alert(document.getElementById("ModifyBasket_hdnTotal").value);
          document.getElementById("ModifyBasket_hdnTotal").value = totalQt;
          document.getElementById(hdnActQty).value = qunatity;




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

      function closeCurrentPopup() {
          $('.ui-dialog').dialog('close');
      }

    </script>
  <div data-role="page" data-theme="a" id="EditBasket" data-title="Basket" >
	         <div data-role="header" data-theme="a" style="text-align: left;" onclick="closeCurrentPopup();">
                    <h3 style="text-align: left;" id="EditBasketheader" runat="server">
                        Your Basket</h3>
                </div>
	        <div data-role="content">
               
            <asp:UpdatePanel ID="upBasket" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <table>
                <tbody>
                    <asp:Repeater ID="rptViewBasket" runat="server" OnItemCommand="rptViewBasket_ItemCommand"
                        OnItemDataBound="rptViewBasket_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td align="center" width="15%">
                                    <asp:TextBox ID="txtQuntity" Width="20" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>'
                                        onblur="javascript:formatquntity(this)"  MaxLength="3"  onkeyup="chgQty(this)"></asp:TextBox>
                                </td>
                                
                                <td align="left" width="45%">
                                    <%# DataBinder.Eval(Container.DataItem, "producttitle") %>
                                </td>
                                <td align="center" width="10%">
                                <b>
                                <asp:Label ID="lblQtyPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,  "qtyPrice", "{0:£,0.00}") %>'></asp:Label>
                                  
                                </td>
                                <td align="center" width="10%">
                                    <asp:Button ID="Delete" runat="server"  OnClientClick="return chgQtyOndelete(this)" CommandName="DELETE" CommandArgument='<%# Eval("productid") %>' />
                                    <asp:HiddenField ID="hdnPrice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,  "price") %>' />
                                    <asp:HiddenField ID="hdnProductId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,  "productid") %>' />
                                    <asp:HiddenField ID="hdnIsMainProduct" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,  "isMainProduct") %>' />
                                    <asp:HiddenField ID="hdnActQty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,  "quantity") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                         
                    </asp:Repeater>
                    <tr>
                        <td align="center" width="15%">
                            1x
                        </td>
                        
                        <td align="left" width="45%">
                            <asp:Label ID="lblDeliveryType" runat="server" ></asp:Label>
                        </td>
                        <td align="center" width="10%">
                            <asp:Label ID="lblDeliveryPrice" runat="server" ></asp:Label>
                        </td>
                        <td align="center" width="10%">
                        </td>
                    </tr>
                    <tr id="trDiscount" runat="server" visible="false">
                    <td align="center" width="15%">
                        <b>Discount:</b>
                    </td>
                    <td width="45%">
                    <asp:Label ID="hlinkVoucherTitle" runat="server" />
                        </td>
                    <td align="center" width="10%">
                       <b>
                            <asp:Label ID="lblDiscountPrice" runat="server"></asp:Label></b> 
                    </td>
                    <td align="center" width="10%">
                        
                    </td>
                </tr>
                    <tr>
                        <td width="15%">
                        </td>
                       
                        <td width="45%">
                        </td>
                        <td width="10%">
                            <b>TOTAL:</b>
                        </td>
                        <td width="10%">
                            <b>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                     <tr id="RowSave">
                        <td width="100%" colspan="4" align="center">
                      <asp:Button ID="SaveAndContinue" runat="server"  OnClientClick="return BaseURL1();" Text="Save and Continue" OnClick="SaveAndContinue_Click" />
                           
                        </td>
                        
                    </tr>
                    <tr style="display:none" Id="RowOption">
                        <td width="100%" colspan="4" align="center">

                            <asp:Button ID="Save" runat="server" Text="Save and Continue" OnClick="Save_Click" />
                        </td>
                        
                        
                    </tr>
                    <tr style="display:none" Id="RowOption1">
                       
                       <td width="100%" colspan="4" align="center">
                         <asp:Button ID="Cancel" runat="server"  Text="Cancel" OnClick="Cancel_Click" />
                          
                        </td>
                        
                    </tr>
                     <tr id="RowWarn" style="display:none">
                        <td width="100%"  colspan="4" align="center">
                            <asp:Label ID="lblErrorInfo"  ForeColor="Red" runat="server" Text="By deleting this product you remove your only main item and will be redirected back to the site. Do you still want to continue?" Style="color: red;font-weight:bold;"></asp:Label>
                        </td>

                </tr>
                                     <tr id="RowWarnQty" style="display:none">
                        <td width="100%"  colspan="4" align="center" style="color:Red">
                             <asp:Label ID="Label1"  ForeColor="Red" runat="server" Text="You have multiple products in your basket. To rectify, simple change the quantity in the 'Qty' box above" Style="color: red;font-weight:bold;"></asp:Label>
                        </td>

                </tr>
                </tbody>
            </table>
                            
            </ContentTemplate>
           
    </asp:UpdatePanel>
        <asp:HiddenField ID="hdnTotal" runat="server" />
        <asp:HiddenField ID="hdnActPrice" runat="server" />
           <asp:HiddenField ID="hdnIsLastProduct" runat="server" value="0"/>
         <asp:HiddenField ID="hdnDisplaySave" Value="false" runat="server" />
          
           
    </div>
     <div data-role="footer" style="padding: 15px 0 15px 0; text-align: center;">
      </div>
</div>