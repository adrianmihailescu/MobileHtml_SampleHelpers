<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FTP Transfer :: help</title>
    <link rel="shortcut icon" href="favico.ico" type="image/x-icon"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:header ID="header1" runat="server" />
        <asp:Label ID="lblHelp" runat="server" Text="help page"></asp:Label>
    </div>
    </form>
</body>
</html>
