<%@ Page language="c#" Inherits="WebApplicationFTP.download" CodeFile="download.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="header" Src="~/usercontrols/headercontrol.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>FTP Transfer :: download</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="shortcut icon" href="favico.ico" type="image/x-icon"/>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc:header ID="header1" runat="server" />
            &nbsp;&nbsp;&nbsp;
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/xml/alertconfig.xml"  EnableCaching="false" XPath="/ConfigSite/Alerts/Alert"></asp:XmlDataSource>
            <table width="100%" height="88%">
                <tr>
                    <td>
            <table style="border: solid 1px black; background-color: lightgrey;">
                <tr>
                    <td valign="top" align="right">
                        <asp:Label ID="lblCalendar" runat="server" Text="Date:"></asp:Label>
                    </td>
                    <td valign="top">
           <asp:Calendar id="Calendar1" runat="server" CellPadding="4" 
                  BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" 
                  Height="180px" ForeColor="Black" DayNameFormat="FirstLetter" 
                  Width="200px" BackColor="White" OnSelectionChanged="Calendar1_SelectionChanged" NextMonthText='<img border="0" src="images/next_arrow.jpg">' PrevMonthText='<img border="0" src="images/prev_arrow.jpg">'>
                  <TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
                  <SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
                  <NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
                  <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC">
                  </DayHeaderStyle>
                  <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666">
                  </SelectedDayStyle>
                  <TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999">
                  </TitleStyle>
                  <WeekendDayStyle BackColor="LightSteelBlue"></WeekendDayStyle>
                  <OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
            </asp:Calendar>            
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
<!-- start legend -->
<asp:Label ID="lblShowLegend" runat="server"></asp:Label>
<!-- end legend --> 
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">
                        <asp:Label ID="lblAlertType" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAlertType" runat="server" Width="100%" DataSourceID="XmlDataSource1" DataTextField="name" DataValueField="value" OnDataBound="ddlAlertType_DataBound">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlSearchStatus" runat="server" style="width: 100%">
                            <asp:Label ID="lblSearchStatus" runat="server"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <br />            
                    <td>
                        <asp:Panel ID="pnlImagePreviews" runat="server" style="height: 100%; overflow: auto">
                        </asp:Panel>
                    </td>
                </tr>
            </table>
		</form>
	</body>
</html>
