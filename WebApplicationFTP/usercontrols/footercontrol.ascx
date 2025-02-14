<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footercontrol.ascx.cs" Inherits="footercontrol" %>
<table border="0" width="100%">
    <tr>
        <td valign="bottom" id="lnkTdHome" runat="server">
            <asp:HyperLink ID="lnkHome" Text ="home" runat="server" NavigateUrl="~/home.aspx"></asp:HyperLink>
        </td>
        <td valign="bottom" id="lnkTdUpload" runat="server">
            <asp:HyperLink ID="lnkUpload" Text ="upload" runat="server" NavigateUrl="~/upload.aspx"></asp:HyperLink>
        </td>
        <td valign="bottom" id="lnkTdDownload" runat="server">
            <asp:HyperLink ID="lnkDownload" Text ="download" runat="server" NavigateUrl="~/download.aspx"></asp:HyperLink>
        </td>
        <td valign="bottom" id="lnkTdAdmin" runat="server">
            <asp:HyperLink ID="lnkAdmin" Text ="admin" runat="server" NavigateUrl="~/admin.aspx"></asp:HyperLink>
        </td>
        <td valign="bottom" id="lnkTdHelp" runat="server">
            <asp:HyperLink ID="lnkHelp" Text ="help" runat="server" NavigateUrl="~/help.aspx"></asp:HyperLink>
        </td>
        <td align="right">
            <asp:Label ID="lblConnectionStatus" runat="server"></asp:Label>
        </td>
        <td valign="bottom" id="lbtTdDisconnect" runat="server">
            <asp:LinkButton ID="lbtDisconnect" runat="server" Text="[disconnect]" OnClick="lbtDisconnect_Click"></asp:LinkButton>
        </td>
    </tr>
</table>