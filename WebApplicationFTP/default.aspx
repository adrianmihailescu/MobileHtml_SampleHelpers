<%@ Page language="c#" Inherits="WebApplicationFTP.WebForm1" CodeFile="default.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd html 4.0 Transitional//EN" >
<html>
	<head>
		<title>FTP Transfer</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	    <link rel="shortcut icon" href="favico.ico" type="image/x-icon"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			    <uc:header ID="header2" runat="server" />
			    <div style="width: 100%; vertical-align: middle;" align="center">
			        <table id="table1" cellSpacing="1" cellPadding="1" border="0" style="border: solid 1px black; background-color: LightGrey;">
				        <tr>
                            <td align="center" colspan="3">
						        <asp:Label id="lblLogin" runat="server" Font-Bold="True">Login</asp:Label>&nbsp;</td>
				        </tr>
				        <tr>
					        <td align="right">
						        <asp:Label id="lblServer" runat="server">Server:</asp:Label></td>
					        <td>
						        <asp:TextBox id="txtServer" runat="server">83.166.220.132</asp:TextBox></td>
					        <td>
						        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="please type server" ControlToValidate="txtServer">*</asp:RequiredFieldValidator></td>
				        </tr>
				        <tr>
					        <td align="right">
						        <asp:Label id="lblUser" runat="server">User:</asp:Label></td>
					        <td>
						        <asp:TextBox id="txtUser" runat="server" TextMode="SingleLine" Width="100%"></asp:TextBox></td>
				        </tr>
				        <tr>
					        <td align="right">
						        <asp:Label id="lblPassword" runat="server">Password:</asp:Label></td>
					        <td>
						        <asp:TextBox id="txtPassword" runat="server" TextMode="Password" Width="100%"></asp:TextBox></td>
				        </tr>
				        <tr>
					        <td align="right">
						        <asp:Label id="lblPort" runat="server">Port:</asp:Label></td>
					        <td colspan="2">
						        <asp:TextBox id="txtPort" runat="server" style="text-align: right;">21</asp:TextBox>
						    </td>
				        </tr>
				        <tr>
				            <td align="right" colspan="3">
				                <asp:Button id="btnLogin" runat="server" Text="login" onclick="btnLogin_Click"></asp:Button>
				            </td>
				        </tr>
				        </table>
				        <table style="background-color: White;">
				        <tr>
					        <td colspan="3">
						        <asp:Label id="lblStatus" runat="server"></asp:Label>
					        </td>
				        </tr>
			        </table>
			    </div>
		</form>
	</body>
</html>
