<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="true" Inherits="TextOnImage._default" %>
<%@ Register TagPrefix="uc1" TagName="diskbrowser" Src="usercontrols/diskbrowser/diskbrowser.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>default</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblSelectImage" Runat="server">Select an image:</asp:label>
			<P><uc1:diskbrowser id="ucDiskBrowser1" runat="server"></uc1:diskbrowser></P>
			<P><asp:label id="Label1" runat="server"> Text to use:</asp:label><asp:textbox id="TextBox1" runat="server"></asp:textbox></P>
			<asp:label id="lblResult" runat="server"></asp:label>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD vAlign="top"><asp:radiobuttonlist id="rblSimpleText" runat="server" AutoPostBack="True">
							<asp:ListItem Value="simple" Selected="True">simple</asp:ListItem>
							<asp:ListItem Value="image">image</asp:ListItem>
							<asp:ListItem Value="swirl">swirl</asp:ListItem>
							<asp:ListItem Value="scroll">scroll</asp:ListItem>
							<asp:ListItem Value="speak text">speak text</asp:ListItem>
						</asp:radiobuttonlist></TD>
					<TD vAlign="top"><asp:radiobuttonlist id="rblImageText" runat="server">
							<asp:ListItem Value="blue" Selected="True">blue <img src="images/letters/blue/a.ico"><img src="images/letters/blue/b.ico"><img src="images/letters/blue/c.ico"></asp:ListItem>
							<asp:ListItem Value="fancy">fancy <img src="images/letters/fancy/a.ico"><img src="images/letters/fancy/b.ico"><img src="images/letters/fancy/c.ico"></asp:ListItem>
						</asp:radiobuttonlist><asp:dropdownlist id="ddlSpeakTextOptions" runat="server">
							<asp:ListItem Value="play">play file</asp:ListItem>
							<asp:ListItem Value="save">save file</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
			</TABLE>
			<asp:button id="btnAddTextOverImage" runat="server" Text="Draw text over image"></asp:button><asp:button id="btnAddTextLettersOverImage" runat="server" Text="Draw text with fancy letters"></asp:button><asp:button id="btnSwirlImage" runat="server" Text="Draw swirl image"></asp:button><asp:button id="btnScrollTextLettersOverImage" runat="server" Text="Scroll image with fancy letters"></asp:button><asp:button id="btnSpeakText" runat="server" Text="Speak text"></asp:button></form>
	</body>
</HTML>
