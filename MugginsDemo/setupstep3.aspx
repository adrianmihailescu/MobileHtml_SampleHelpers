<%@ Page language="c#" Codebehind="setupstep3.aspx.cs" AutoEventWireup="false" Inherits="MugginsDemo.setupstep3" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Muggins :: dressing items</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="Label1" runat="server">Dressing & accessories:</asp:label>
			<TABLE id="Table1">
				<TR>
					<TD><asp:label id="lblHair" runat="server">Hair:</asp:label></TD>
					<td><asp:panel id="pnlBoyHair" Runat="server">
							<asp:dropdownlist id="ddlBoyHair" runat="server">
								<asp:ListItem Value="dread">dread</asp:ListItem>
								<asp:ListItem Value="long">long</asp:ListItem>
								<asp:ListItem Value="posh">posh</asp:ListItem>
								<asp:ListItem Value="short">short</asp:ListItem>
								<asp:ListItem Value="soldier">soldier</asp:ListItem>
								<asp:ListItem Value="spiky">spiky</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlHair" Runat="server">
							<asp:dropdownlist id="ddlGirlHair" runat="server">
								<asp:ListItem Value="curly">curly</asp:ListItem>
								<asp:ListItem Value="flat">flat</asp:ListItem>
								<asp:ListItem Value="fringe">fringe</asp:ListItem>
								<asp:ListItem Value="medium">medium</asp:ListItem>
								<asp:ListItem Value="short">short</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></td>
				</TR>
				<TR>
					<TD><asp:label id="lblAccessories" runat="server">Accessories:</asp:label></TD>
					<TD><asp:panel id="pnlBoyAccessories" Runat="server">
							<asp:Panel id="pnlBoyGlasses" Runat="server">
								<TABLE border="0">
									<TR>
										<TD>
											<asp:label id="lblGlassesType" runat="server">glasses:</asp:label>
											<asp:RadioButtonList id="rblBoyGlassesType" runat="server">
												<asp:ListItem Value="glasses" Selected="True">glasses</asp:ListItem>
												<asp:ListItem Value="sunglasses">sunglasses</asp:ListItem>
												<asp:ListItem Value="glassessport">glassessport</asp:ListItem>
												<asp:ListItem Value="noglasses">no glasses</asp:ListItem>
											</asp:RadioButtonList></TD>
									</TR>
								</TABLE>
							</asp:Panel>
							<asp:checkboxlist id="cbkBoyAccessories" runat="server" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid">
								<asp:ListItem Value="keys">keys</asp:ListItem>
								<asp:ListItem Value="earpiece">earpiece</asp:ListItem>
								<asp:ListItem Value="laptopbag">laptopbag</asp:ListItem>
								<asp:ListItem Value="shoulderbag">shoulderbag</asp:ListItem>
								<asp:ListItem Value="cap">cap</asp:ListItem>
							</asp:checkboxlist>
						</asp:panel>
						<asp:panel id="pnlGirlAccessories" Runat="server">
							<asp:CheckBoxList id="cbkGirlAccessories" Runat="server">
								<asp:ListItem Value="scarf">scarf</asp:ListItem>
								<asp:ListItem Value="blackhandbag">blackhandbag</asp:ListItem>
								<asp:ListItem Value="handbag">handbag</asp:ListItem>
								<asp:ListItem Value="neckcross">neckcross</asp:ListItem>
								<asp:ListItem Value="pinkscarf">pinkscarf</asp:ListItem>
								<asp:ListItem Value="neckheart">neckheart</asp:ListItem>
							</asp:CheckBoxList>
						</asp:panel>
						<asp:panel id="pnlGirlAccessoriesNoScarf" Runat="server">
							<asp:CheckBoxList id="cbkGirlAccessoriesNoScarf" Runat="server">
								<asp:ListItem Value="blackhandbag">blackhandbag</asp:ListItem>
								<asp:ListItem Value="handbag">handbag</asp:ListItem>
								<asp:ListItem Value="neckcross">neckcross</asp:ListItem>
								<asp:ListItem Value="pinkscarf">pinkscarf</asp:ListItem>
								<asp:ListItem Value="neckheart">neckheart</asp:ListItem>
							</asp:CheckBoxList>
						</asp:panel>
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblClothes" runat="server">Clothes:</asp:label></TD>
					<td><asp:panel id="pnlBoyClothes" Runat="server">
							<asp:dropdownlist id="ddlBoyClothes" runat="server">
								<asp:ListItem Value="coat">coat</asp:ListItem>
								<asp:ListItem Value="shirt">shirt</asp:ListItem>
								<asp:ListItem Value="sweatshirt">sweatshirt</asp:ListItem>
								<asp:ListItem Value="tshirt">tshirt</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlClothes" Runat="server">
							<asp:dropdownlist id="ddlGirlClothes" runat="server">
								<asp:ListItem Value="blouse">blouse</asp:ListItem>
								<asp:ListItem Value="coat">coat</asp:ListItem>
								<asp:ListItem Value="dress">dress</asp:ListItem>
								<asp:ListItem Value="shoes">shoes</asp:ListItem>
								<asp:ListItem Value="skirt">skirt</asp:ListItem>
								<asp:ListItem Value="trousers">trousers</asp:ListItem>
								<asp:ListItem Value="vest">vest</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlBlouseTypes" Runat="server">
							<asp:RadioButtonList id="rblGirlBlouseTypes" runat="server">
								<asp:ListItem Value="tanktop" Selected="True">tanktop</asp:ListItem>
								<asp:ListItem Value="top">top</asp:ListItem>
							</asp:RadioButtonList>
						</asp:panel></td>
				</TR>
				<TR>
					<TD><asp:label id="lblTrousers" runat="server">Trousers:</asp:label></TD>
					<td><asp:panel id="pnlBoyTrousers" Runat="server">
							<asp:dropdownlist id="ddlBoyTrousers" runat="server">
								<asp:ListItem Value="army">army</asp:ListItem>
								<asp:ListItem Value="brown">brown</asp:ListItem>
								<asp:ListItem Value="jeans">jeans</asp:ListItem>
								<asp:ListItem Value="pleated">pleated</asp:ListItem>
								<asp:ListItem Value="sport">sport</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlTrousers" Runat="server">
							<asp:dropdownlist id="ddlGirlTrousers" runat="server">
								<asp:ListItem Value="belljeans">belljeans</asp:ListItem>
								<asp:ListItem Value="leggins">leggins</asp:ListItem>
								<asp:ListItem Value="tightjeans">tightjeans</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></td>
				</TR>
				<TR>
					<TD><asp:label id="lblShoes" runat="server">Shoes:</asp:label></TD>
					<TD><asp:panel id="pnlBoyShoes" Runat="server">
							<asp:dropdownlist id="ddlBoyShoes" runat="server">
								<asp:ListItem Value="black">all stars</asp:ListItem>
								<asp:ListItem Value="brown">classical</asp:ListItem>
								<asp:ListItem Value="lbrown">camper</asp:ListItem>
								<asp:ListItem Value="whiteblue">sport</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlShoes" Runat="server">
							<asp:dropdownlist id="ddlGirlShoes" runat="server">
								<asp:ListItem Value="booties">booties</asp:ListItem>
								<asp:ListItem Value="dancers">dancers</asp:ListItem>
								<asp:ListItem Value="heels">heels</asp:ListItem>
								<asp:ListItem Value="slippers">slippers</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></TD>
				</TR>
			</TABLE>
			<asp:button id="btnPreviousStep" runat="server" Text="Back to previous step"></asp:button>|
			<asp:button id="Button1" runat="server" Text="Submit"></asp:button></form>
		</FORM>
	</body>
</HTML>
