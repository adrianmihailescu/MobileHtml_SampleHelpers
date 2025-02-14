<%@ Page language="c#" Inherits="WebApplicationFTP.home" CodeFile="home.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server">
		<title>FTP Transfer :: home</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="shortcut icon" href="favico.ico" type="image/x-icon"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc:header ID="header1" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblWelcome" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        At this point, you can:
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/download.aspx">view</asp:HyperLink> the existing active alerts<br />
                        or you can <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/upload.aspx">create</asp:HyperLink> new alerts
                    </td>
                </tr>
            </table>
		</form>
	</body>
</html>