using System;
using System.Web;
using System.Web.UI.WebControls;
using KMobile.Catalog.Presentation;
using KMobile.Catalog.Services;


namespace MugginsDemo
{
	public class test_page : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Label lblSelectLanguage;
		protected System.Web.UI.WebControls.Panel pnlSelectLanguage;
		protected RadioButtonList rblMugginPreviews, rblSelectLanguage;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		HttpCookie myCookie = new HttpCookie("MugginsLanguage");

		private void Page_Load(object sender, System.EventArgs e)
		{
			MobileCaps _mobile = (MobileCaps)Request.Browser;

			// parameters to query the webservice
			string strDisplayKey = "81DDEB08-0D40-4239-A52F-8EB76C17545D";
			int idContentSet = 6577;
			string strContentGroup = "IMG";
			string strContentType = "IMG_COLOR";
			string strMobileType = _mobile.MobileType;
            
			ContentSet cs = StaticCatalogService.GetContentsByContentSetExtended(strDisplayKey, idContentSet, strContentGroup, strContentType, strMobileType);

			// the radiobuttonlist to show the background previews
			rblMugginPreviews = new RadioButtonList();
			rblMugginPreviews.RepeatDirection = RepeatDirection.Horizontal;
			
			int maxNumberOfColumns = 0; int tempItemsWidth = 0;

			for (int tempCounter=0; tempCounter<cs.Count; tempCounter++)
			{	
				rblMugginPreviews.Items.Add(new ListItem("<img src=\"" + cs.ContentCollection[tempCounter].Preview.URL + "\">", cs.ContentCollection[tempCounter].ContentName));
				tempItemsWidth += 80;
				// add the preview's width

				if (tempItemsWidth < _mobile.ScreenPixelsWidth)
					maxNumberOfColumns++;
			}
			
			rblMugginPreviews.RepeatColumns = maxNumberOfColumns; // display previews on maxNumberOfColumns columns // 8
			rblMugginPreviews.CssClass = "radioMugginsPreviews";
			rblMugginPreviews.Items[0].Selected = true;

			// add the radiobuttonlist to the form			
			Button btnSubmitBackgroundChoice = new Button();
			btnSubmitBackgroundChoice.Text = "Submit"; btnSubmitBackgroundChoice.CssClass = "submitButton";
			btnSubmitBackgroundChoice.Click+=new EventHandler(btnSubmitBackgroundChoice_Click);

			// draw a table	
			Label lblStartTable = new Label(); lblStartTable.Text = "<table class=\"setupPageTable\"><tr><td>";
			
			// the first row is the "select background message"
			this.FindControl("Form1").Controls.Add(lblStartTable); // Form1
			
			// get the logo's width
			System.Drawing.Bitmap bmpLogo = new System.Drawing.Bitmap(Server.MapPath("~/images/logo.gif"));
			int logoBmpWidth = bmpLogo.Width;
			
			// if the logo's width is bigger than the screen's width
			// resize it
			if (logoBmpWidth > _mobile.ScreenPixelsWidth)
				logoBmpWidth = _mobile.ScreenPixelsWidth;

			Label lblShowLogo = new Label(); lblShowLogo.Text = "<img width=\"" + logoBmpWidth + "\" src=\"images/logo.gif\" />";
			this.FindControl("Form1").Controls.Add(lblShowLogo); // Form1

			
			//<-- select language panel
			Panel pnlSelectLanguage = new Panel(); Label lblSelectLanguage = new Label();
			pnlSelectLanguage.ID = "pnlSelectLanguage";

			lblSelectLanguage.Text = "Select language<br /><br />"; lblSelectLanguage.CssClass = "headerMessage";
			rblSelectLanguage = new RadioButtonList(); rblSelectLanguage.CssClass = "selectItemList";
			
			rblSelectLanguage.Items.Add(new ListItem("<img src=\"images/uk_flag.jpg\" /> english", "english"));
			rblSelectLanguage.Items.Add(new ListItem("<img src=\"images/spain_flag.jpg\" /> spanish", "spanish"));
			rblSelectLanguage.Items.Add(new ListItem("<img src=\"images/italy_flag.jpg\" /> italian", "italian"));
			rblSelectLanguage.Items[0].Selected = true;

			pnlSelectLanguage.Controls.Add(lblSelectLanguage);
			pnlSelectLanguage.Controls.Add(rblSelectLanguage);
			
			this.FindControl("Form1").Controls.Add(pnlSelectLanguage); // Form1
			
			// if the language cookie is not found
			if (Request.Cookies[myCookie.Name] == null)
			{
				// show the language panel
				pnlSelectLanguage.Visible = true;
				/*
				myCookie.Value = "rad";
				Response.Write(myCookie.Value);
				*/
			}

			else // get the language value from the cookie
			{
				pnlSelectLanguage.Visible = false;
				// get the language value from the panel
				/*
				Response.Write(myCookie.Value);
				*/
			}
			//--> select language panel

			Label lblBrTag = new Label(); lblBrTag.Text = "</td></tr><tr><td>";
			this.FindControl("Form1").Controls.Add(lblBrTag); // Form1
			
			Label lblSelectBackgroundMessage = new Label();	lblSelectBackgroundMessage.Text = "<br /><img src=\"images/information.jpg\"> Please select your background";
			lblSelectBackgroundMessage.CssClass = "headerMessageNoLine";
			this.FindControl("Form1").Controls.Add(lblSelectBackgroundMessage);

			// the second row is the background previews
			Label lblBreakTag = new Label(); lblBreakTag.Text = "</td></tr><tr><td>";
			this.FindControl("Form1").Controls.Add(lblBreakTag);
			this.FindControl("Form1").Controls.Add(rblMugginPreviews);
			Label lblBreakTagNew = new Label(); lblBreakTagNew.Text = "</td></tr><tr><td>";

			// the third row is the submit button
			this.FindControl("Form1").Controls.Add(lblBreakTagNew);
			this.FindControl("Form1").Controls.Add(btnSubmitBackgroundChoice);

			Label lblEndTable = new Label(); lblEndTable.Text = "</td></tr></table>";
			this.FindControl("Form1").Controls.Add(lblEndTable);
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// the handler for submit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSubmitBackgroundChoice_Click(object sender, EventArgs e)
		{
			myCookie.Value = "spanish";
			Response.Cookies.Add(myCookie);

			Response.Redirect("setupstep1.aspx?bgfile=" + rblMugginPreviews.SelectedItem.Value + "&lang=" + rblSelectLanguage.SelectedValue);
		}
	}
}
