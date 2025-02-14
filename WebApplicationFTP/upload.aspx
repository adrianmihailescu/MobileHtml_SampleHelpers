<%@ Page language="c#" Inherits="WebApplicationFTP.upload" CodeFile="upload.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="upload" Src="~/usercontrols/fileuploadcontrol.ascx" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>FTP Transfer :: upload</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="shortcut icon" href="favico.ico" type="image/x-icon"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc:header ID="header1" runat="server" />
			<uc:upload ID="uploadControl1" runat="server" />
		</form>
	</body>
</html>