<%@ Control Language="c#" AutoEventWireup="false" Inherits="usercontrols_diskbrowser" CodeBehind="diskbrowser.ascx.cs" %>
<asp:Panel ID="pnlGridHeader" runat="server">
	<TABLE style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; WIDTH: 100%; BORDER-BOTTOM: 0px">
		<TR>
			<TD vAlign="middle">
				<asp:Label id="Label1" runat="server" Text="drive:"></asp:Label></TD>
			<TD>
				<asp:DropDownList id="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
					AutoPostBack="True"></asp:DropDownList></TD>
			<TD vAlign="middle">
				<asp:Label id="Label2" runat="server" Text="Select the file by clicking on its name"></asp:Label></TD>
		</TR>
	</TABLE>
</asp:Panel>
<asp:Label ID="lblCurrentDirectory" runat="server" style="FONT-SIZE:xx-small; FONT-FAMILY:Arial"></asp:Label>
<asp:Panel ID="pnlGrid" runat="server" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; OVERFLOW: auto; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
	Height="150">
	<asp:DataGrid id="FileSystem" style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" runat="server" GridLines="None"
		ForeColor="#333333" CellPadding="4" AutoGenerateColumns="False" AllowSorting="True">
		<Columns>
			<asp:BoundColumn DataField="Type" HeaderText="Type">
				<HeaderStyle Width="80px"></HeaderStyle>
			</asp:BoundColumn>
			<asp:TemplateColumn HeaderText="Name">
				<HeaderStyle Width="350px"></HeaderStyle>
				<ItemTemplate>
					<asp:LinkButton id="systemLink" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.FullName") %>' CommandName='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
						<%# DataBinder.Eval(Container, "DataItem.Name") %>
					</asp:LinkButton>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="CreationTime" HeaderText="Creation time">
				<HeaderStyle Width="150px"></HeaderStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="Size" HeaderText="Size">
				<HeaderStyle Width="150px"></HeaderStyle>
			</asp:BoundColumn>
		</Columns>
		<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
		<EditItemStyle BackColor="#2461BF" />
		<SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
		<AlternatingItemStyle BackColor="White" />
		<ItemStyle BackColor="#EFF3FB" />
		<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
	</asp:DataGrid>
</asp:Panel>
<asp:Panel ID="pnlGridFooter" runat="server">
<asp:HyperLink id="lnkBack" style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" runat="server" Text="back"
		NavigateUrl="javascript:history.go(-1)"></asp:HyperLink>| 
<asp:HyperLink id="lnkForward" style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" runat="server" Text="forward"
		NavigateUrl="javascript:history.go(+1)"></asp:HyperLink><BR>
<asp:Label id="lblFoundFile" style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" runat="server"></asp:Label>
     </asp:Panel>
