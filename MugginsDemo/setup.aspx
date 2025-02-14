<%@ Page language="c#" Codebehind="setup.aspx.cs" AutoEventWireup="false" Inherits="MugginsDemo.setup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Muggins :: setup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1">
				<TR>
					<TD><asp:label id="Size" runat="server">Size:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlSize" runat="server">
							<asp:ListItem Value="266x340">266x340</asp:ListItem>
							<asp:ListItem Value="340x266">340x266</asp:ListItem>
							<asp:ListItem Value="340x340">340x340</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblSex" runat="server">Sex:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlSex" runat="server" AutoPostBack="True">
							<asp:ListItem Value="boy">boy</asp:ListItem>
							<asp:ListItem Value="girl">girl</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblAccessories" runat="server">Accessories:</asp:label></TD>
					<TD><asp:panel id="pnlBoyAccessories" Runat="server">
							<asp:checkboxlist id="cbkBoyAccessories" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
								<asp:ListItem Value="glasses">glasses</asp:ListItem>
								<asp:ListItem Value="keys">keys</asp:ListItem>
								<asp:ListItem Value="sunglasses">sunglasses</asp:ListItem>
							</asp:checkboxlist>
						</asp:panel>
						<asp:panel id="pnlGirlAccessories" Runat="server">
							<asp:DropDownList id="ddlGirlAccessories" Runat="server">
								<asp:ListItem Value="blackhandbag">blackhandbag</asp:ListItem>
								<asp:ListItem Value="handbag">handbag</asp:ListItem>
								<asp:ListItem Value="scarf">scarf</asp:ListItem>
							</asp:DropDownList>
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
							<asp:dropdownlist id="ddlGirlClothes" runat="server" AutoPostBack="True">
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
					<TD><asp:label id="lblSkin" runat="server">Skin:</asp:label></TD>
					<td><asp:dropdownlist id="ddlSkin" runat="server">
							<asp:ListItem Value="black">black</asp:ListItem>
							<asp:ListItem Value="mulato">mulato</asp:ListItem>
							<asp:ListItem Value="white">white</asp:ListItem>
						</asp:dropdownlist></td>
				</TR>
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
					<TD><asp:label id="lblCharacter" runat="server">Character:</asp:label></TD>
					<TD><asp:panel id="pnlBoyCharacter" Runat="server">
							<asp:dropdownlist id="ddlBoyCharacter" runat="server">
								<asp:ListItem Value="cool">cool</asp:ListItem>
								<asp:ListItem Value="flirt">flirt</asp:ListItem>
								<asp:ListItem Value="furious">furious</asp:ListItem>
								<asp:ListItem Value="happy">happy</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlCharacter" Runat="server">
							<asp:dropdownlist id="ddlGirlCharacter" runat="server">
								<asp:ListItem Value="angry">angry</asp:ListItem>
								<asp:ListItem Value="fun">fun</asp:ListItem>
								<asp:ListItem Value="happy">happy</asp:ListItem>
								<asp:ListItem Value="sexy">sexy</asp:ListItem>
								<asp:ListItem Value="standard">standard</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblEyes" runat="server">Eyes:</asp:label></TD>
					<TD><asp:panel id="pnlBoyEyes" Runat="server">
							<asp:dropdownlist id="ddlBoyEyes" runat="server">
								<asp:ListItem Value="blue">blue</asp:ListItem>
								<asp:ListItem Value="brown">brown</asp:ListItem>
								<asp:ListItem Value="green">green</asp:ListItem>
								<asp:ListItem Value="lbrown">lbrown</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlEyes" Runat="server">
							<asp:dropdownlist id="ddlGirlEyes" runat="server">
								<asp:ListItem Value="blue">blue</asp:ListItem>
								<asp:ListItem Value="brown">dbrown</asp:ListItem>
								<asp:ListItem Value="green">green</asp:ListItem>
								<asp:ListItem Value="lbrown">lbrown</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></TD>
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
				<TR>
					<TD><asp:label id="lblBrows" runat="server">Hair color:</asp:label></TD>
					<TD><asp:panel id="pnlBoyBrows" Runat="server">
							<asp:dropdownlist id="ddlBoyBrows" runat="server">
								<asp:ListItem Value="black">black</asp:ListItem>
								<asp:ListItem Value="blond">blond</asp:ListItem>
								<asp:ListItem Value="brown">brown</asp:ListItem>
								<asp:ListItem Value="lbrown">lbrown</asp:ListItem>
								<asp:ListItem Value="red">red</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel><asp:panel id="pnlGirlBrows" Runat="server">
							<asp:dropdownlist id="ddlGirlBrows" runat="server">
								<asp:ListItem Value="black">black</asp:ListItem>
								<asp:ListItem Value="blond">blond</asp:ListItem>
								<asp:ListItem Value="brown">brown</asp:ListItem>
								<asp:ListItem Value="red">lbrown</asp:ListItem>
								<asp:ListItem Value="Violet">red</asp:ListItem>
							</asp:dropdownlist>
						</asp:panel></TD>
				</TR>
			</TABLE>
			<asp:button id="Button1" runat="server" Text="Submit"></asp:button></form>
	</body>
</HTML>
