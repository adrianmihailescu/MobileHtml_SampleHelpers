<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>
<%@ Register TagPrefix="uc" TagName="colorpicker" Src="~/usercontrols/colorpicker.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>FTP Transfer :: admin</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc:header ID="header1" runat="server" />
        <asp:Panel ID="pnlShowExistingAlerts" runat="server">
            <table>
                <tr>
                    <td colspan="3" style="font-weight: bold; text-align: center">
                        <asp:Label ID="lblAdminShowExistingAlerts" Text="Existing alerts:" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="4" DataSourceID="XmlDataSource1"
                        ForeColor="#333333" GridLines="None" PageSize="5" AllowPaging="True">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                            <asp:BoundField DataField="color" HeaderText="color" SortExpression="color" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table border="0">
                                        <tr>
                                            <td bgcolor="<%#Eval("color")%>">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <asp:Panel ID="pnlAddNewAlert" runat="server" style="border: solid 1px black; width: 215px; background-color: LightGrey;">
            <table>
                <tr>
                    <td colspan="3" style="font-weight:bold; text-align: center">
                        <asp:Label ID="lblAlertDetails" runat="server" Text="New alert's details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAlertName" runat="server" Text="name:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbxAlertName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="valAlertName" runat="server" ControlToValidate="tbxAlertName" Text="*" ValidationGroup="addNewAlertToFile"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblColorPicker" runat="server" Text="color:"></asp:Label>
                    </td>
                    <td align="left">
                        <uc:colorpicker ID="ucColorPicker1" runat="server" />        
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnAddAlertToFile" runat="server" Text="Add alert to file" ValidationGroup="addNewAlertToFile" OnClick="btnAddAlertToFile_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                        <asp:Label ID="lblInsertAlertStatus" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        * here you can add a new type of alert if you can't find it
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/xml/alertconfig.xml" EnableCaching="false" XPath="/ConfigSite/Alerts/Alert">
        </asp:XmlDataSource>
</body>
</html>