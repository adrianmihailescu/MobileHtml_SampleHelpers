using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MugginsDemo
{
	/// <summary>
	/// Summary description for testpage.
	/// </summary>
	public class testpage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnRetrieve;
		protected System.Web.UI.WebControls.Button btnCreate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			HttpCookie myCookie = new HttpCookie("cakes");
			if (Request.Cookies[myCookie.Name] == null)
			{
				myCookie.Value = "testing";
				Response.Write(myCookie.Value);
			}
			else
			{
				Response.Write(myCookie.Value);
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
			this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnRetrieve_Click(object sender, System.EventArgs e)
		{

		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{

		}
	}
}
