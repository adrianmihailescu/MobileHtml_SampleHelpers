using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
// using System.Drawing;
using System.Web;
// using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
// using System.Web.UI.HtmlControls;

namespace WebApplicationFTP
{
	/// <summary>
	/// Summary description for home.
	/// </summary>
	public partial class home : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // if the user is not connected, redirect the browser to the login page
            if (!ftp.ftp_main.ftplib.IsConnected)
            {
                ftp.ftp_main.ftplib.ShowErrorMessage(lblWelcome, "Not connected to any server...Redirecting to the login page");
                Response.Redirect("default.aspx");
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
}
}