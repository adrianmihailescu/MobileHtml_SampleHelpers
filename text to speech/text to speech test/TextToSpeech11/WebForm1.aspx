<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="TextToSpeech11.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
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
			<DIV></DIV>
		</form>
	</body>
</HTML>
