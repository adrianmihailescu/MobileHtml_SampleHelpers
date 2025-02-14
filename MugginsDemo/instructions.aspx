<%@ Page language="c#" Codebehind="instructions.aspx.cs" AutoEventWireup="false" Inherits="MugginsDemo.instructions" %>
<%@ Import namespace="MugginsDemo.tools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Muggins ::
			<% = tools.GetXmlValue("Setup1InstructionsText", Request.QueryString["lang"])%>
		</title>
		<LINK id="link1" href="css/style.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="tblShowInstructionsImage" runat="server">
				<tr>
					<td><asp:image id="imgLogo" ImageUrl="images/logo.gif" Runat="server"></asp:image></td>
				</tr>
			</table>
			<TABLE class="setup1TableNoBorder" runat="server">
				<tr>
					<td>
						<asp:Image ID="imgShowInstructions" Runat="server" ImageUrl="images/MIXED.gif"></asp:Image>
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblMessageTip" Runat="server"></asp:label></td>
				</tr>
			</TABLE>
			<table id="tblBackLink" runat="server">
				<tr>
					<td>
						<asp:HyperLink ID="lnkBackArrow" Runat="server" CssClass="setup1Link">
							<asp:Image ID="imgBackArrow" Runat="server" ImageUrl="images/back_arrow.JPG"></asp:Image>
						</asp:HyperLink>
						<asp:HyperLink ID="lnkBack" Runat="server" CssClass="setup1Link"></asp:HyperLink>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
