<%@ Import namespace="MugginsDemo.tools" %>
<%@ Page language="c#" Codebehind="setupstep2.aspx.cs" AutoEventWireup="false" Inherits="MugginsDemo.setupstep2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<% if (sex == "BOY") { %>
			Muggins :: <% = tools.GetXmlValue("Setup2Boys", Request.QueryString["lang"])%><% } else { %>
			Muggins :: <% = tools.GetXmlValue("Setup2Girls", Request.QueryString["lang"])%><%}%>
		</title>
		<LINK id="link1" href="css/style.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form runat="server">
			<table class="setup1Table">
				<tr>
					<td colSpan="3"><asp:image id="imgLogo" ImageUrl="images/logo.gif" Runat="server"></asp:image></td>
				</tr>
				<tr>
					<td colSpan="3"><asp:image id="imgInformationSign" ImageUrl="images/information.JPG" Runat="server"></asp:image><asp:label id="Label1" runat="server" CssClass="headerMessage">Character's details : dressing & accessories</asp:label></td>
				</tr>
				<tr>
					<td><asp:label id="lblCharacter" runat="server">Character:</asp:label></td>
					<td colSpan="2"><asp:dropdownlist id="ddlCharacter" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="lblHair" runat="server">Hair:</asp:label></td>
					<td><asp:dropdownlist id="ddlHair" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
					<td><asp:dropdownlist id="ddlHairColor" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="lblGlassesType" runat="server">Glasses:</asp:label><asp:radiobuttonlist id="rbtGlassesType" runat="server" CssClass="selectItemList"></asp:radiobuttonlist></td>
					<td colSpan="2"><asp:label id="lblAccessories" runat="server">Accessories:</asp:label><asp:checkboxlist id="cbkAccessories" runat="server" CssClass="selectItemList"></asp:checkboxlist></td>
				</tr>
				<tr>
					<td><asp:label id="lblClothes" runat="server">Clothes:</asp:label></td>
					<td colSpan="2"><asp:dropdownlist id="ddlClothes" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="lblTrousers" runat="server">Trousers:</asp:label></td>
					<td colSpan="2"><asp:dropdownlist id="ddlTrousers" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:label id="lblShoes" runat="server">Shoes:</asp:label></td>
					<td colSpan="2"><asp:dropdownlist id="ddlShoes" runat="server" CssClass="selectItemList"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td><asp:button id="btnPreviousStep" runat="server" Text="Back to previous step" CssClass="submitButton"></asp:button></td>
					<td colSpan="2"><asp:button id="Button1" runat="server" Text="Submit" CssClass="submitButton"></asp:button></td>
				</tr>
			</table>
		</form>
		</FORM>
	</body>
</HTML>
