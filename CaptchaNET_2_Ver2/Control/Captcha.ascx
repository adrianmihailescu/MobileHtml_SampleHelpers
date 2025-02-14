<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Captcha.ascx.cs" Inherits="CaptchaControl" %>
<table border="0" cellpadding="0" cellspacing="5" style="<% =style %>">
    <tr>
        <td style="font-size: 10pt; height: 16px">
            <asp:Label ID="lblCMessage" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; height: 16px">
            <asp:Image ID="imgCaptcha" runat="server" Height="92px" Width="305px" /></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; height: 16px">
            <asp:TextBox ID="txtCaptcha" runat="server"
                Width="298px"></asp:TextBox>
                </td>
    </tr>
    <tr>
        <td style="font-size: 10pt; height: 16px">
            <asp:Button ID="btnCSubmit" runat="server"
                Text="Submit" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
