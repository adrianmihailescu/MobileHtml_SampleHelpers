<%@ Control Language="C#" AutoEventWireup="true" CodeFile="diskbrowser.ascx.cs" Inherits="usercontrols_diskbrowser" %>
    <asp:Panel ID="pnlGridHeader" runat="server">
	    <table style="border: 0; width: 100%">
	        <tr>
	            <td valign="middle">
	                <asp:Label ID="Label1" runat="server" Text="drive:"></asp:Label>
	            </td>
	            <td>	                
	                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
	            </td>
	            <td valign="middle">
	                <asp:Label ID="Label2" runat="server" Text="Select the file by clicking on its name"></asp:Label>
	            </td>
	        </tr>
	    </table> 
    </asp:Panel>    
    <asp:Label ID="lblCurrentDirectory" runat="server" style="font-size:xx-small; font-family:Arial;"></asp:Label>
    <asp:Panel ID="pnlGrid"  runat="server" style="border: solid 1px black; height: 150px; overflow: auto;">
	<asp:DataGrid  id="FileSystem" runat="server" AllowSorting="True"
				AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" style="width: auto; height: auto; font-size:xx-small; font-family:Arial;">
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
	    <asp:HyperLink ID="lnkBack" runat="server" Text="back" NavigateUrl="javascript:history.go(-1)" style="font-family: Arial; font-size: xx-small;"></asp:HyperLink> | 
	    <asp:HyperLink ID="lnkForward" runat="server" Text="forward" NavigateUrl="javascript:history.go(+1)" style="font-family: Arial; font-size: xx-small;"></asp:HyperLink><br />
        <asp:Label ID="lblFoundFile" runat="server" style="font-size:xx-small; font-family:Arial;"></asp:Label>
     </asp:Panel>
