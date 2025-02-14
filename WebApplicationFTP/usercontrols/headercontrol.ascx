<%@ Control Language="C#" AutoEventWireup="true" CodeFile="headercontrol.ascx.cs" Inherits="headercontrol" %>
<%@ Register TagPrefix="uc" TagName="footer" Src="~/usercontrols/footercontrol.ascx" %>

<table style="width: 100%">
    <tr>
        <td align="center">
            <asp:Label ID="lblHeaderTitle" runat="server" Text="Web application FTP transfer" Font-Bold="true"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <uc:footer ID="footer1" runat="server" />
        </td>
    </tr>
</table>
<hr />