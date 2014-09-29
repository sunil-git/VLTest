<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="International.aspx.cs" Inherits="Serenataflowers.International" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <%@ Register Src="Controls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
 <%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flowers UK London | Flower Delivery UK | Florist | Flowers Online| Send Flowers to England Florists</title>
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" name="viewport">
     <link href="~/Styles/m_style.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="//nexus.ensighten.com/serenata/Bootstrap.js"></script>
<body>
    <form id="form1" runat="server">
   <div id="page_wrapper">
    <div id="content_normal">
    <!--Header DIV Start-->
    <div id="header">
        <uc1:Header ID="Header1" runat="server" EnableUrl="true"/>
    </div>
     <!--Header DIV End-->
     <!-----------------------Content Details----------------------->
     <div class="clearleft"></div>
     <asp:Literal ID="LtrlContries" runat="server" ></asp:Literal>
    <%--  <div class="step" runat="server" Id="A">
				<a name="A"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>A</b></h2>							

				<a href="Default.aspx?countryid=60" title="Florist in Algeria"> Algeria</a>
				&nbsp; <a href="Default.aspx?countryid=10" title="Florist in Angola"> Angola</a>
				&nbsp; <a href="Default.aspx?countryid=6" title="Florist in Anguilla"> Anguilla</a>
				&nbsp; <a href="Default.aspx?countryid=5" title="Florist in Antigua"> Antigua</a>
				&nbsp; <a href="Default.aspx?countryid=12" title="Florist in Argentina"> Argentina</a>	
				&nbsp; <a href="Default.aspx?countryid=8" title="Florist in Armenia"> Armenia</a>
				&nbsp; <a href="Default.aspx?countryid=16" title="Florist in Aruba"> Aruba</a>	
				&nbsp; <a href="Default.aspx?countryid=15" title="Florist in Australia"> Australia</a>
				&nbsp; <a href="Default.aspx?countryid=14" title="Florist in Austria"> Austria</a>
				&nbsp; <a href="Default.aspx?countryid=18" title="Florist in Azerbaijan"> Azerbaijan</a>
				&nbsp; <a href="Default.aspx?countryid=234" title="Florist in Azores"> Azores</a>
				

				<div style="clear:left;"></div><br>
			</div>
        <div class="step" runat="server" Id="B">
				<a name="B"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>B</b></h2>
							
				<a href="Default.aspx?countryid=32" title="Florist in Bahama Islands"> Bahama&nbsp;Islands</a> 
				&nbsp; <a href="Default.aspx?countryid=25" title="Florist in Bahrain"> Bahrain</a>
				&nbsp; <a href="Default.aspx?countryid=20" title="Florist in Barbados"> Barbados</a>
				&nbsp; <a href="Default.aspx?countryid=36" title="Florist in Belarus"> Belarus</a> 
				&nbsp; <a href="Default.aspx?countryid=22" title="Florist in Belgium"> Belgium</a>
				&nbsp; <a href="Default.aspx?countryid=37" title="Florist in Belize"> Belize</a>
				&nbsp; <a href="Default.aspx?countryid=27" title="Florist in Benin"> Benin</a>
				&nbsp; <a href="Default.aspx?countryid=28" title="Florist in Bermuda"> Bermuda</a>
				&nbsp; <a href="Default.aspx?countryid=30" title="Florist in Bolivia"> Bolivia</a>
				&nbsp; <a href="Default.aspx?countryid=236" title="Florist in Bosnia-Herzegovina"> Bosnia-Herzegovina</a>
				&nbsp; <a href="Default.aspx?countryid=35" title="Florist in Botswana"> Botswana</a>
				&nbsp; <a href="Default.aspx?countryid=31" title="Florist in Brazil"> Brazil</a>															
				&nbsp; <a href="Default.aspx?countryid=24" title="Florist in Bulgaria"> Bulgaria</a>

				<div style="clear:left;"></div><br>
			</div> 
		<div class="step" runat="server" Id="C">
				<a name="C"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>C</b></h2> 	

				<a href="Default.aspx?countryid=110" title="Florist in Cambodia"> Cambodia</a>
				&nbsp; <a href="Default.aspx?countryid=38" title="Florist in Canada"> Canada</a> 
				&nbsp; <a href="Default.aspx?countryid=116" title="Florist in Cayman Islands"> Cayman&nbsp;Islands</a>
				&nbsp; <a href="Default.aspx?countryid=238" title="Florist in Channel Islands"> Channel&nbsp;Islands</a>
				&nbsp; <a href="Default.aspx?countryid=44" title="Florist in Chile"> Chile</a>
				&nbsp; <a href="Default.aspx?countryid=46" title="Florist in China"> China</a>
				&nbsp; <a href="Default.aspx?countryid=47" title="Florist in Colombia"> Colombia</a>
				&nbsp; <a href="Default.aspx?countryid=43" title="Florist in Cook Islands"> Cook&nbsp;Islands</a>
				&nbsp; <a href="Default.aspx?countryid=48" title="Florist in Costa Rica"> Costa&nbsp;Rica</a> 
				&nbsp; <a href="Default.aspx?countryid=92" title="Florist in Croatia"> Croatia</a>
				&nbsp; <a href="Default.aspx?countryid=50" title="Florist in Cuba"> Cuba</a>
				&nbsp; <a href="Default.aspx?countryid=239" title="Florist in Curacao"> Curacao</a>
				&nbsp; <a href="Default.aspx?countryid=53" title="Florist in Cyprus"> Cyprus</a>
				&nbsp; <a href="Default.aspx?countryid=54" title="Florist in Czech Republic"> Czech&nbsp;Republic</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="D">
				<a name="D"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>D</b></h2> 

				<a href="Default.aspx?countryid=57" title="Florist in Denmark"> Denmark</a> 
				&nbsp; <a href="Default.aspx?countryid=58" title="Florist in Dominica"> Dominica</a>
				&nbsp; <a href="Default.aspx?countryid=59" title="Florist in Dominican Republic"> Dominican&nbsp;Republic</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="E">
				<a name="E"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>E</b></h2> 

				<a href="Default.aspx?countryid=61" title="Florist in Ecuador"> Ecuador</a> 
				&nbsp; <a href="Default.aspx?countryid=63" title="Florist in Egypt"> Egypt</a> 
				&nbsp; <a href="Default.aspx?countryid=196" title="Florist in El Salvador"> El&nbsp;Salvador</a>
				&nbsp; <a href="Default.aspx?countryid=65" title="Florist in Eritrea"> Eritrea</a>
				&nbsp; <a href="Default.aspx?countryid=62" title="Florist in Estonia"> Estonia</a>
				&nbsp; <a href="Default.aspx?countryid=67" title="Florist in Ethiopia"> Ethiopia</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="F">
				<a name="F"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>F</b></h2> 

				<a href="Default.aspx?countryid=72" title="Florist in Faroe Islands"> Faroe&nbsp;Islands</a>
				&nbsp; <a href="Default.aspx?countryid=69" title="Florist in Fiji Islandss"> Fiji&nbsp;Islands</a>
				&nbsp; <a href="Default.aspx?countryid=68" title="Florist in Finland"> Finland</a>
				&nbsp; <a href="Default.aspx?countryid=73" title="Florist in France"> France</a>
				&nbsp; <a href="Default.aspx?countryid=78" title="Florist in French Guiana"> French&nbsp;Guiana</a>
				&nbsp; <a href="Default.aspx?countryid=165" title="Florist in French Polynesia"> French&nbsp;Polynesia</a>	

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="G">
				<a name="G"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>G</b></h2> 

				 <a href="Default.aspx?countryid=74" title="Florist in Gabon"> Gabon</a>
				 &nbsp; <a href="Default.aspx?countryid=77" title="Florist in Georgia"> Georgia</a>
				 &nbsp; <a href="Default.aspx?countryid=55" title="Florist in Germany"> Germany</a>
				 &nbsp; <a href="Default.aspx?countryid=80" title="Florist in Gibraltar"> Gibraltar</a> 
				 &nbsp; <a href="Default.aspx?countryid=86" title="Florist in Greece"> Greece</a>
				 &nbsp; <a href="Default.aspx?countryid=84" title="Florist in Guadeloupe"> Guadeloupe</a>
				 &nbsp; <a href="Default.aspx?countryid=88" title="Florist in Guam , M.I."> Guam&nbsp;,&nbsp;M.I.</a>
				 &nbsp; <a href="Default.aspx?countryid=87" title="Florist in Guatemala"> Guatemala</a>
				 &nbsp; <a href="Default.aspx?countryid=89" title="Florist in Guyana"> Guyana</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="H">
				<a name="H"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>H</b></h2> 

				 <a href="Default.aspx?countryid=93" title="Florist in Haiti"> Haiti</a>	
				 &nbsp; <a href="Default.aspx?countryid=91" title="Florist in Honduras"> Honduras</a>															 
				 &nbsp; <a href="Default.aspx?countryid=90" title="Florist in Hong Kong"> Hong&nbsp;Kong</a>
				 &nbsp; <a href="Default.aspx?countryid=94" title="Florist in Hungary"> Hungary</a> 	

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="I">
				<a name="I"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>I</b></h2> 

				 <a href="Default.aspx?countryid=102" title="Florist in Iceland"> Iceland</a> 
				 &nbsp; <a href="Default.aspx?countryid=99" title="Florist in India"> India</a>
				 &nbsp; <a href="Default.aspx?countryid=95" title="Florist in Indonesia"> Indonesia</a>
				 &nbsp; <a href="Default.aspx?countryid=96" title="Florist in Ireland"> Ireland</a>
				 &nbsp; <a href="Default.aspx?countryid=97" title="Florist in Israel"> Israel</a> 
				 &nbsp; <a href="Default.aspx?countryid=103" title="Florist in Italy"> Italy</a> 
				 &nbsp; <a href="Default.aspx?countryid=232" title="Florist in Ivory Coast"> Ivory&nbsp;Coast</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="J">
				<a name="J"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>J</b></h2> 

				<a href="Default.aspx?countryid=105" title="Florist in Jamaica"> Jamaica</a>
				&nbsp; <a href="Default.aspx?countryid=107" title="Florist in Japan"> Japan</a>
				&nbsp; <a href="Default.aspx?countryid=106" title="Florist in Jordan"> Jordan</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="K">
				<a name="K"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>K</b></h2> 
							
				<a href="Default.aspx?countryid=117" title="Florist in Kazakhstan"> Kazakhstan</a>
				&nbsp; <a href="Default.aspx?countryid=108" title="Florist in Kenya"> Kenya</a>
				&nbsp; <a href="Default.aspx?countryid=115" title="Florist in Kuwait"> Kuwait</a>
				&nbsp; <a href="Default.aspx?countryid=109" title="Florist in Kyrgyzstan"> Kyrgyzstan</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="L">
				<a name="L"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>L</b></h2> 

				<a href="Default.aspx?countryid=127" title="Florist in Latvia"> Latvia</a>
				&nbsp; <a href="Default.aspx?countryid=119" title="Florist in Lebanon"> Lebanon</a>
				&nbsp; <a href="Default.aspx?countryid=121" title="Florist in Liechtenstein"> Liechtenstein</a> 
				&nbsp; <a href="Default.aspx?countryid=125" title="Florist in Lithuania"> Lithuania</a> 
				&nbsp; <a href="Default.aspx?countryid=243" title="Florist in Luxemburg"> Luxemburg</a> 

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="M">
				<a name="M"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>M</b></h2> 

				<a href="Default.aspx?countryid=134" title="Florist in Macedonia"> Macedonia</a> 
				&nbsp; <a href="Default.aspx?countryid=244" title="Florist in Madeira"> Madeira</a>
				&nbsp; <a href="Default.aspx?countryid=145" title="Florist in Malawi"> Malawi</a>
				&nbsp; <a href="Default.aspx?countryid=147" title="Florist in Malaysia"> Malaysia</a>
				&nbsp; <a href="Default.aspx?countryid=142" title="Florist in Malta"> Malta</a>
				&nbsp; <a href="Default.aspx?countryid=139" title="Florist in Martinique"> Martinique</a>
				&nbsp; <a href="Default.aspx?countryid=143" title="Florist in Mauritius"> Mauritius</a>
				&nbsp; <a href="Default.aspx?countryid=146" title="Florist in Mexico"> Mexico</a> 
				&nbsp; <a href="Default.aspx?countryid=131" title="Florist in Moldova"> Moldova</a> 
				&nbsp; <a href="Default.aspx?countryid=130" title="Florist in Monaco"> Monaco</a>
				&nbsp; <a href="Default.aspx?countryid=252" title="Florist in Montenegro"> Montenegro</a>
				&nbsp; <a href="Default.aspx?countryid=141" title="Florist in Montserrat"> Montserrat</a>
				&nbsp; <a href="Default.aspx?countryid=129" title="Florist in Morocco"> Morocco</a> 
				&nbsp; <a href="Default.aspx?countryid=148" title="Florist in Mozambique"> Mozambique</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="N">
				<a name="N"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>N</b></h2> 

				&nbsp; <a href="Default.aspx?countryid=149" title="Florist in Namibia"> Namibia</a>
				&nbsp; <a href="Default.aspx?countryid=155" title="Florist in Netherlands"> Netherlands</a>
				&nbsp; <a href="Default.aspx?countryid=150" title="Florist in New Caledoni"> New&nbsp;Caledonia</a>
				&nbsp; <a href="Default.aspx?countryid=161" title="Florist in New Zealand"> New&nbsp;Zealand</a>
				&nbsp; <a href="Default.aspx?countryid=154" title="Florist in Nicaragua"> Nicaragua</a>
				&nbsp; <a href="Default.aspx?countryid=245" title="Florist in Northern Cyprus"> Northern&nbsp;Cyprus</a>
				&nbsp; <a href="Default.aspx?countryid=156" title="Florist in Norway"> Norway</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="O">
				<a name="O"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>O</b></h2> 
				<a href="Default.aspx?countryid=162" title="Florist in Oman">Oman</a>&nbsp;

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="P">
				<a name="P"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>P</b></h2> 

				 &nbsp; <a href="Default.aspx?countryid=168" title="Florist in Pakistan"> Pakistan</a> 
				 &nbsp; <a href="Default.aspx?countryid=163" title="Florist in Panama"> Panama</a> 
				 &nbsp; <a href="Default.aspx?countryid=175" title="Florist in Paraguay"> Paraguay</a>
				 &nbsp; <a href="Default.aspx?countryid=164" title="Florist in Peru"> Peru</a>
				 &nbsp; <a href="Default.aspx?countryid=167" title="Florist in Philippines"> Philippines</a> 
				 &nbsp; <a href="Default.aspx?countryid=169" title="Florist in Poland"> Poland</a>
				 &nbsp; <a href="Default.aspx?countryid=173" title="Florist in Portugal"> Portugal</a> 
				 &nbsp; <a href="Default.aspx?countryid=171" title="Florist in Puerto Rico"> Puerto&nbsp;Rico</a>	

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="Q">
				<a name="Q"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>Q</b></h2> 
				<a href="Default.aspx?countryid=176" title="Florist in Qatar">Qatar</a>&nbsp;	

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="R">
				<a name="R"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>R</b></h2> 

				 <a href="Default.aspx?countryid=177" title="Florist in Reunion Island"> Reunion&nbsp;Island</a>
				 &nbsp; <a href="Default.aspx?countryid=178" title="Florist in Romania"> Romania</a>
				 &nbsp; <a href="Default.aspx?countryid=246" title="Florist in Russia"> Russia</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="S">
				<a name="S"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>S</b></h2>

				 <a href="Default.aspx?countryid=120" title="Florist in Saint Lucia"> Saint&nbsp;Lucia</a>
				 &nbsp; <a href="Default.aspx?countryid=247" title="Florist in Saint Pierre Et Miquelon"> Saint&nbsp;Pierre&nbsp;Et&nbsp;Miquelon</a>
				 &nbsp; <a href="Default.aspx?countryid=254" title="Florist in Samoa"> Samoa</a>															 
				 &nbsp; <a href="Default.aspx?countryid=192" title="Florist in San Marino"> San&nbsp;Marino</a>
				 &nbsp; <a href="Default.aspx?countryid=182" title="Florist in Saudi Arabia"> Saudi&nbsp;Arabia</a>
				 &nbsp; <a href="Default.aspx?countryid=193" title="Florist in Senegal"> Senegal</a>
				 &nbsp; <a href="Default.aspx?countryid=179" title="Florist in Serbia"> Serbia</a>
				 &nbsp; <a href="Default.aspx?countryid=248" title="Florist in Seychelles Islands"> Seychelles&nbsp;Islands</a>
				 &nbsp; <a href="Default.aspx?countryid=187" title="Florist in Singapore"> Singapore</a>
				 &nbsp; <a href="Default.aspx?countryid=249" title="Florist in Slovakia"> Slovakia</a> 
				 &nbsp; <a href="Default.aspx?countryid=253" title="Florist in Slovenia"> Slovenia</a>
				 &nbsp; <a href="Default.aspx?countryid=229" title="Florist in South&nbsp;Africa"> South&nbsp;Africa</a> 
				 &nbsp; <a href="Default.aspx?countryid=66" title="Florist in Spain"> Spain</a>
				 &nbsp; <a href="Default.aspx?countryid=122" title="Florist in Sri Lanka"> Sri&nbsp;Lanka</a>
				 &nbsp; <a href="Default.aspx?countryid=195" title="Florist in Suriname"> Suriname</a>
				 &nbsp; <a href="Default.aspx?countryid=198" title="Florist in Sweden"> Swaziland</a>
				 &nbsp; <a href="Default.aspx?countryid=186" title="Florist in Sweden"> Sweden</a>
				 &nbsp; <a href="Default.aspx?countryid=40" title="Florist in Switzerland"> Switzerland</a>
				 &nbsp; <a href="Default.aspx?countryid=197" title="Florist in Syria"> Syria</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="T">
				<a name="T"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>T</b></h2> 

				 <a href="Default.aspx?countryid=211" title="Florist in Taiwan"> Taiwan</a> 
				 &nbsp; <a href="Default.aspx?countryid=202" title="Florist in Tajikistan"> Tajikistan</a>
				 &nbsp; <a href="Default.aspx?countryid=201" title="Florist in Thailand"> Thailand</a>
				 &nbsp; <a href="Default.aspx?countryid=206" title="Florist in Tonga"> Tonga</a>				
				 &nbsp; <a href="Default.aspx?countryid=205" title="Florist in Tunisia"> Tunisia</a>
				 &nbsp; <a href="Default.aspx?countryid=208" title="Florist in Turkey"> Turkey</a>
				 &nbsp; <a href="Default.aspx?countryid=204" title="Florist in Turkmenistan"> Turkmenistan</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="U">
				<a name="U"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>U</b></h2>
							
				 <a href="Default.aspx?countryid=213" title="Florist in Ukraine"> Ukraine</a>
				 &nbsp; <a href="Default.aspx?countryid=3" title="Florist in United Arab Emirates"> United&nbsp;Arab&nbsp;Emirates</a>
				 &nbsp; <a href="Default.aspx?countryid=215" title="Florist in United Kingdom"> United&nbsp;Kingdom</a>
				 &nbsp; <a href="Default.aspx?countryid=216" title="Florist in US"> United States</a> 
				 &nbsp; <a href="Default.aspx?countryid=217" title="Florist in Uruguay"> Uruguay</a> 
				 &nbsp; <a href="Default.aspx?countryid=218" title="Florist in Uzbekistan"> Uzbekistan</a>

				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="V">
				<a name="V"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>V</b></h2> 

				 &nbsp; <a href="Default.aspx?countryid=219" title="Florist in Vatican City"> Vatican&nbsp;City</a>
				 &nbsp; <a href="Default.aspx?countryid=220" title="Florist in Venezuela"> Venezuela</a>
				 &nbsp; <a href="Default.aspx?countryid=223" title="Florist in Vietnam"> Vietnam</a>
				 &nbsp; <a href="Default.aspx?countryid=222" title="Florist in Virgin Islands"> Virgin&nbsp;Islands</a>


				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="W">
				<a name="W"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>W</b></h2> 

				<a href="Default.aspx?countryid=251" title="Florist in West Indies"> West&nbsp;Indies</a>


				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="Y">
				<a name="Y"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>Y</b></h2> 

				<a href="Default.aspx?countryid=226" title="Florist in Yemen"> Yemen</a>
				&nbsp; <a href="Default.aspx?countryid=228" title="Florist in Yugoslavia"> Yugoslavia</a>


				<div style="clear:left;"></div><br>
			</div>

			<div class="step" runat="server" Id="Z">
				<a name="Z"></a><h2 style="font-size:28px;color:orange;margin:0;"><b>Z</b></h2> 

				<a href="Default.aspx?countryid=230" title="Florist in Zambia"> Zambia</a>
				&nbsp; <a href="Default.aspx?countryid=231" title="Florist in Zimbabwe"> Zimbabwe</a>

				<div style="clear:left;"></div><br>
			</div>	--%>
 <!-----------------------Content Details----------------------->
      <!--Footer DIV Start-->
     <div id="FooterDiv" >
    <uc2:Footer ID="Footer1" runat="server" />
    </div>
    <!--Footer DIV End-->
     </div>
    </div>
    </form>
</body>
</html>
