<%@ Register TagPrefix="uc" TagName="diskbrowser" Src="~/usercontrols/diskbrowser.ascx" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fileuploadcontrol.ascx.cs" Inherits="fileuploadcontrol"%>

<head id="Head1" runat="server">
    <script language = "javascript">
        function showPleaseWait()
        {
            document.getElementById('PleaseWait').style.display = 'block';
        }
    </script>
</head>
    
<asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/xml/alertconfig.xml" EnableCaching="false" XPath="/ConfigSite/Alerts/Alert"></asp:XmlDataSource>
    <!-- start please wait message -->
    <div class="helptext" id="PleaseWait" style="display: none; text-align:right; color:White; vertical-align:top;">
        <table id="myTable" style="background-color: LightGrey; width: 100%;">
        <tr>
            <td align="center">
                <b><font color="black">Please wait while the files are being uploaded...</font></b><br />
            </td>
        </tr>
        </table>
    </div>
    <!-- stop please wait message -->
<asp:Label id="lblUploadStatus" runat="server"></asp:Label>
<table style="width: 100%; height: 88%">
    <tr>
        <td>
<table style="background-color: LightGrey; border: solid 1px black; width: 400px">
    <tr>
        <td style="text-align: center; font-weight: bold;">
            <asp:Label ID="txtBrowseFile" runat="server">Select the alert's files:</asp:Label>
        </td>
    </tr>
     <tr>
        <td>
            <uc:diskbrowser ID="diskBrowser1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold; text-align: center;">
            <asp:Label ID="Label2" runat="server" Text="Alert's details:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDescription" runat="server">Description:</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="txtDescriptionValidator" runat="server" ControlToValidate="txtDescription" Text="*" ValidationGroup="formValidation"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblAlertType" Text="Type:" runat="server"></asp:Label><br />
        </td>
    </tr>
    <tr>
        <td valign="middle">        
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="name" DataValueField="value" AutoPostBack="false" OnDataBound="DropDownList1_DataBound">
            </asp:DropDownList>
            <asp:Label ID="lblAddAlertTypeNotFound" runat="server" Text="If you can't find it in the list "></asp:Label>
            <asp:HyperLink ID="lnkAddAlertType" runat="server" NavigateUrl="~/admin.aspx" Text="add a new alert type"></asp:HyperLink>            
        </td>
    </tr>
     <tr>
        <td>
            <asp:Label ID="Label1" Text="Date:" runat="server"></asp:Label><br />
            <asp:Calendar id="Calendar1" runat="server" CellPadding="4" 
                  BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" 
                  Height="180px" ForeColor="Black" DayNameFormat="FirstLetter" 
                  Width="200px" BackColor="White" NextMonthText='<img border="0" src="images/next_arrow.jpg">' PrevMonthText='<img border="0" src="images/prev_arrow.jpg">' OnDayRender="Calendar1_DayRender">
                  <TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
                  <SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
                  <NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
                  <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
                  <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666">
                  </SelectedDayStyle>
                  <TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999">
                  </TitleStyle>
                  <WeekendDayStyle BackColor="LightSteelBlue"></WeekendDayStyle>
                  <OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
            </asp:Calendar>  
<!-- start legend -->
<asp:Label ID="lblShowLegend" runat="server"></asp:Label>
<!-- end legend -->          
        </td>
     </tr>
      <tr>
        <td style="text-align: center;">
            <asp:Label ID="txtSelectFile" runat="server" style="font-weight:bold;">Add the selected files to list:</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ListBox ID="ListBox1" runat="server" style="overflow: auto; font-size: xx-small; width: 100%"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="AddFile" Text="Add" runat="server" OnClick="AddFile_Click"  ValidationGroup="formValidation" />
            <asp:Button id="RemvFile" runat="server" Text="Remove" OnClick="RemvFile_Click" />
        </td>
    </tr>
    <tr>
        <td align="right">
            <hr />
            <asp:Button ID="Upload" Text="Upload files" runat="server" UseSubmitBehavior="true" OnClick="Upload_Click" onmouseup="showPleaseWait()"/>
        </td>
    </tr>
</table>
</td>
        <td>
            <asp:Panel ID="pnlImagePreviews" runat="server" Height="100%" ScrollBars="Auto">
            </asp:Panel>
        </td>
    </tr>
</table>