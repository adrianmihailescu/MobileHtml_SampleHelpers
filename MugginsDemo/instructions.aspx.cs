using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MugginsDemo.tools;
using KMobile.Catalog.Services;

namespace MugginsDemo
{
	/// <summary>
	/// Summary description for instructions.
	/// </summary>
	public class instructions : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblMessageTip;
		protected System.Web.UI.WebControls.HyperLink lnkBack;
		protected System.Web.UI.WebControls.Image imgLogo;
		protected System.Web.UI.WebControls.Label lblImageTip;
		protected System.Web.UI.WebControls.Image imgBackArrow;
		protected System.Web.UI.HtmlControls.HtmlTable tblBackLink, tblShowInstructionsImage;
		protected System.Web.UI.WebControls.HyperLink lnkBackArrow;
		protected System.Web.UI.WebControls.Image imgShowInstructions;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region xml methods		
		/// <summary>
		/// sets text on labels according to the selected language
		/// </summary>
		private void SetPageItemsText()
		{
			MobileCaps _mobile = (MobileCaps)Request.Browser;

			// set labels text according to the selected language
			lblMessageTip.Text = tools.tools.GetXmlValue("InstructionsText", Request.QueryString["lang"]);
			lnkBack.Text = tools.tools.GetXmlValue("Setup1Back", Request.QueryString["lang"]);
			lnkBack.NavigateUrl = "setupstep1.aspx?lang=" + Request.QueryString["lang"];
			lnkBackArrow.NavigateUrl = "setupstep1.aspx?lang=" + Request.QueryString["lang"];

			// resize image automatically to the screen
			imgShowInstructions.Width = (int)(0.5 * _mobile.ScreenPixelsWidth);
			imgShowInstructions.Height = (int)(0.5 * _mobile.ScreenPixelsHeight);

			// resize image automatically to the screen
			tblShowInstructionsImage.Width = Convert.ToString(0.5 * _mobile.ScreenPixelsWidth);		
		}
		#endregion xml methods

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
			this.Load += new System.EventHandler(this.Page_Load);
            SetPageItemsText();
		}
		#endregion
	}
}
