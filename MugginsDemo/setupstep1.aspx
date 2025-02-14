<%@ Page language="c#" Codebehind="setupstep1.aspx.cs" AutoEventWireup="false" Inherits="MugginsDemo.setupstep1" %>
<%@ Import namespace="MugginsDemo.tools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Muggins ::
			<% = tools.GetXmlValue("Setup1CharacterType", Request.QueryString["lang"])%>
		</title>
		<link id="link1" rel="stylesheet" href="css/style.css" type="text/css">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<!--
			<% = tools.GetXmlValue("SampleText", Request.QueryString["lang"])%>
			-->
			<TABLE id="Table1" class="setup1Table">
				<tr>
					<td colspan="2">
						<asp:Image ID="imgLogo" Runat="server" ImageUrl="images/logo.gif"></asp:Image>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Image ID="imgInformationSign" Runat="server" ImageUrl="images/information.JPG"></asp:Image>
						<asp:label id="Label1" runat="server" CssClass="headerMessage">Character's details : type</asp:label>
					</td>
				</tr>
				<TR>
					<TD><asp:label id="Size" runat="server">Size:</asp:label></TD>
					<TD><asp:dropdownlist id="ddlSize" runat="server" CssClass="selectItemList">
							<asp:ListItem Value="266x340">266x340</asp:ListItem>
							<asp:ListItem Value="340x266">340x266</asp:ListItem>
							<asp:ListItem Value="340x340">340x340</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblSex" runat="server">Sex:</asp:label></TD>
					<TD>
						<asp:dropdownlist id="ddlSex" runat="server" CssClass="selectItemList"></asp:dropdownlist>
					</TD>
				</TR>
				<TR>
					<TD><asp:label id="lblSkin" runat="server">Skin:</asp:label></TD>
					<td><asp:dropdownlist id="ddlSkin" runat="server" CssClass="selectItemList"></asp:dropdownlist>
					</td>
				</TR>
				<tr>
					<TD><asp:label id="Label2" runat="server">Eyes color:</asp:label></TD>
					<td>
						<asp:dropdownlist id="ddlEyes" runat="server" CssClass="selectItemList"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td>
						<asp:button id="btnPreviousStep" runat="server" Text="Back to previous step" CssClass="submitButton"></asp:button>
					</td>
					<td>
						<asp:button id="Button1" runat="server" Text="Submit" CssClass="submitButton"></asp:button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:HyperLink ID="lnkShowInformations" Runat="server" CssClass="setup1Link"></asp:HyperLink>
					</td>
				</tr>
			</TABLE>
		</form>
		</FORM>
	</body>
</HTML>
