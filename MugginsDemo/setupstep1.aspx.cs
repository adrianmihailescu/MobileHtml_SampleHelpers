using System;
using MugginsDemo.tools;

namespace MugginsDemo
{
	public class setupstep1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlSex, ddlSkin, ddlEyes, ddlSize;
		protected System.Web.UI.WebControls.Button Button1, btnPreviousStep;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Size;
		protected System.Web.UI.WebControls.Label lblSex;
		protected System.Web.UI.WebControls.Label lblSkin;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Image imgLogo;
		protected System.Web.UI.WebControls.Image imgInformationSign;
		protected System.Web.UI.WebControls.HyperLink lnkShowInformations;
	
		public string bgfile;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			bgfile = (Request.QueryString["bgfile"] != null && Request.QueryString["bgfile"] != "") ? Request.QueryString["bgfile"] : "KM_FONDBLANC_C85";
//			string ua = this.Request.UserAgent.ToUpper();
//			if (ua.IndexOf("FIREFOX") >= 0)
//			{
//				Button1.Enabled = false;
//				Button1.Text = "Please, do not use firefox.";
//			}
		}

		#region Form generated code

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
			this.btnPreviousStep.Click += new System.EventHandler(this.btnPreviousStep_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			SetPageItemsText();
		}
		#endregion

		#region xml methods		
		/// <summary>
		/// sets text on labels according to the selected language
		/// </summary>
		private void SetPageItemsText()
		{
			System.Web.UI.WebControls.ListItem liSkinListItem;
			System.Web.UI.WebControls.ListItem liEyesListItem;
			System.Web.UI.WebControls.ListItem liBoyListItem;
			System.Web.UI.WebControls.ListItem liGirlListItem;

			// set labels text according to the selected language
			Label1.Text = tools.tools.GetXmlValue("Setup1CharacterDetails", Request.QueryString["lang"]);
			Size.Text = tools.tools.GetXmlValue("Setup1CharacterSize", Request.QueryString["lang"]);
			lblSex.Text = tools.tools.GetXmlValue("Setup1CharacterSex", Request.QueryString["lang"]);
			lblSkin.Text = tools.tools.GetXmlValue("Setup1CharacterSkin", Request.QueryString["lang"]);
			Label2.Text = tools.tools.GetXmlValue("Setup1CharacterEyesColor", Request.QueryString["lang"]);
			btnPreviousStep.Text = tools.tools.GetXmlValue("Setup1BackToPreviousStep", Request.QueryString["lang"]);
			Button1.Text = tools.tools.GetXmlValue("Setup1SubmitChoice", Request.QueryString["lang"]);
			lnkShowInformations.Text = tools.tools.GetXmlValue("Setup1Instructions", Request.QueryString["lang"]);

			lnkShowInformations.NavigateUrl = "instructions.aspx?lang=" + Request.QueryString["lang"];
			
			// add boy / girl values to dropdownlist
			liBoyListItem = new System.Web.UI.WebControls.ListItem();
			liBoyListItem.Text = tools.tools.GetXmlValue("Setup1Boy", Request.QueryString["lang"]);
			liBoyListItem.Value = "boy";
			ddlSex.Items.Add(liBoyListItem);
			
			liGirlListItem = new System.Web.UI.WebControls.ListItem();
			liGirlListItem.Text = tools.tools.GetXmlValue("Setup1Girl", Request.QueryString["lang"]);
			liGirlListItem.Value = "girl";
			ddlSex.Items.Add(liGirlListItem);
			
			// add black, mulato, white skin to dropdownlist
			liSkinListItem = new System.Web.UI.WebControls.ListItem();
			liSkinListItem.Text = tools.tools.GetXmlValue("Setup1SkinBlack", Request.QueryString["lang"]);
			liSkinListItem.Value = "black";
			ddlSkin.Items.Add(liSkinListItem);
			
			liSkinListItem = new System.Web.UI.WebControls.ListItem();
			liSkinListItem.Text = tools.tools.GetXmlValue("Setup1SkinMulato", Request.QueryString["lang"]);
			liSkinListItem.Value = "mulato";
			ddlSkin.Items.Add(liSkinListItem);

			liSkinListItem = new System.Web.UI.WebControls.ListItem();
			liSkinListItem.Text = tools.tools.GetXmlValue("Setup1SkinWhite", Request.QueryString["lang"]);
			liSkinListItem.Value = "white";
			ddlSkin.Items.Add(liSkinListItem);

			// add eyes color to dropdownlist
			liEyesListItem = new System.Web.UI.WebControls.ListItem();
			liEyesListItem.Text = tools.tools.GetXmlValue("Setup1EyeBlue", Request.QueryString["lang"]);
			liEyesListItem.Value = "blue";
			ddlEyes.Items.Add(liEyesListItem);

			liEyesListItem = new System.Web.UI.WebControls.ListItem();
			liEyesListItem.Text = tools.tools.GetXmlValue("Setup1EyeGreen", Request.QueryString["lang"]);
			liEyesListItem.Value = "green";
			ddlEyes.Items.Add(liEyesListItem);

			liEyesListItem = new System.Web.UI.WebControls.ListItem();
			liEyesListItem.Text = tools.tools.GetXmlValue("Setup1EyeLightBrown", Request.QueryString["lang"]);
			liEyesListItem.Value = "lbrown";
			ddlEyes.Items.Add(liEyesListItem);

			liEyesListItem = new System.Web.UI.WebControls.ListItem();
			liEyesListItem.Text = tools.tools.GetXmlValue("Setup1EyeDarkBrown", Request.QueryString["lang"]);
			liEyesListItem.Value = "brown";
			ddlEyes.Items.Add(liEyesListItem);
		}
		#endregion xml methods

		#region event handlers
		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strBuildBody = String.Empty;
            
			strBuildBody += "size=" + ddlSize.SelectedValue;
			strBuildBody += "&sex=" + ddlSex.SelectedValue;
			strBuildBody += "&skin=" + ddlSkin.SelectedValue;
			
			strBuildBody += "&eyes=" + ddlEyes.SelectedValue;
			strBuildBody += "&bgfile=" + bgfile;
			strBuildBody += "&lang=" + Request.QueryString["lang"];

			Response.Redirect("setupstep2.aspx" + "?" + strBuildBody);
			
			// Response.Write(tools.tools.GetXmlValue("SampleText", "spanish"));
		}

		private void btnPreviousStep_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("default.aspx");
		}
		#endregion event handlers
	}
}
