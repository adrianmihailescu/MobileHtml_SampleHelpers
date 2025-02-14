<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Text" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Page Language="VB" %>
<script language="VB" runat="server">

Sub Page_Load(sender As Object, e As EventArgs)

	If Page.IsPostBack Then
		
		Dim oBitmap As Bitmap = New Bitmap(468, 60)
		Dim oGraphic As Graphics = Graphics.FromImage(oBitmap)
		Dim oColor As System.Drawing.Color

		Dim sColor As String = Request("BackgroundColor")
		Dim sText As String = Request("Text")
		Dim sFont As String = Request("Font")

		Select Case sColor
		    Case "red"
		        oColor = Color.Red
		    Case "green"
		        oColor = Color.Green
		    Case "navy"
		        oColor = Color.Navy
		    Case "orange"
		        oColor = Color.Orange
		    Case Else
		        oColor = Color.Gray
		End Select
    
		Dim oBrush As New SolidBrush(oColor)
		Dim oBrushWrite As New SolidBrush(Color.White)

		oGraphic.FillRectangle(oBrush, 0, 0, 468, 60)
		oGraphic.TextRenderingHint = TextRenderingHint.AntiAlias
	
		Dim oFont As New Font(sFont, 13)
		Dim oPoint As New PointF(5F, 5F)

		oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint)


		'Response.ContentType = "image/jpeg"
		'oBitmap.Save (Response.OutputStream, ImageFormat.Jpeg)

		oBitmap.Save(Server.MapPath("gen_img.jpg"), ImageFormat.Jpeg)

		Response.Write("View the generated image <a target=""_blank"" href=""gen_img.jpg"">here</a>")

	End If
	
	

End Sub

</script>
<form runat="server" ID="Form1">
	<asp:TextBox runat="server" id="Text" />
	<br>
	<br>
	<asp:dropdownlist runat="server" id="BackgroundColor">
		<asp:ListItem Value="red">Red</asp:ListItem>
		<asp:ListItem Value="green">Green</asp:ListItem>
		<asp:ListItem Value="navy">Navy</asp:ListItem>
		<asp:ListItem Value="orange">Orange</asp:ListItem>
	</asp:dropdownlist>
	<asp:dropdownlist runat="server" id="Font">
		<asp:ListItem Value="Arial">Arial</asp:ListItem>
		<asp:ListItem Value="Verdana">Verdana</asp:ListItem>
		<asp:ListItem Value="Courier">Courier</asp:ListItem>
		<asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
	</asp:dropdownlist>
	<br>
	<br>
	<asp:Button runat="Server" id="SubmitButton" Text="Generate Image" />
</form>
