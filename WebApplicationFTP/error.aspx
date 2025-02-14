<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FTP Transfer :: error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:header ID="header1" runat="server" />
        <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
