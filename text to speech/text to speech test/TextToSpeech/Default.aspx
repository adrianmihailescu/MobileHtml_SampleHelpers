<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0">
            <tr>
                <td>
                    <asp:TextBox ID="txtTextToSpeak" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqTextToSpeak" runat="server" ErrorMessage="*" ControlToValidate="txtTextToSpeak"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Selected="True">play voice</asp:ListItem>
                        <asp:ListItem>save voice to file</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnPlayOrSave" runat="server" Text="play" OnClick="btnPlayOrSave_Click" />            
                </td>
            </tr>
        </table>        
        </div>
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    </form>
</body>
</html>