<%@ Control Language="C#" AutoEventWireup="true" CodeFile="colorpicker.ascx.cs" Inherits="usercontrols_colorpicker" %>
<table>
    <tr>
        <td>
            <asp:DropDownList ID ="ddlMultiColor" runat="server" AutoPostBack="false">    
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblAddNewAlertStatus" runat="server"></asp:Label>
        </td>
    </tr>
</table>