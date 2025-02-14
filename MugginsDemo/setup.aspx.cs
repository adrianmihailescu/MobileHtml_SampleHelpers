using System;

namespace MugginsDemo
{
	public class setup : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlSex;
		protected System.Web.UI.WebControls.Label lblSex;
		protected System.Web.UI.WebControls.Label lblAccessories;
		protected System.Web.UI.WebControls.Label lblSkin;
		protected System.Web.UI.WebControls.Label lblHair;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Size;
		protected System.Web.UI.WebControls.DropDownList ddlSize;
		protected System.Web.UI.WebControls.DropDownList ddlClothes;
		protected System.Web.UI.WebControls.DropDownList ddlSkin;
		protected System.Web.UI.WebControls.DropDownList ddlHair;
		protected System.Web.UI.WebControls.Label lblCharacter;
		protected System.Web.UI.WebControls.DropDownList ddlBoyCharacter, ddlGirlCharacter;
		protected System.Web.UI.WebControls.Panel pnlBoyCharacter, pnlGirlCharacter;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Label lblEyes;
		protected System.Web.UI.WebControls.Label lblBrows;
		protected System.Web.UI.WebControls.DropDownList ddlEyes;
		protected System.Web.UI.WebControls.DropDownList ddlBrows, ddlShoes, ddlTrousers;
		protected System.Web.UI.WebControls.Label lblTrousers;
		protected System.Web.UI.WebControls.Label lblShoes;
		protected System.Web.UI.WebControls.Label lblClothes;
		protected System.Web.UI.WebControls.CheckBoxList cbkBoyAccessories;
		protected System.Web.UI.WebControls.Panel pnlBoyAccessories;
		protected System.Web.UI.WebControls.DropDownList ddlGirlAccessories;
		protected System.Web.UI.WebControls.Panel pnlGirlAccessories;
		protected System.Web.UI.WebControls.DropDownList ddlBoyClothes;
		protected System.Web.UI.WebControls.Panel pnlBoyClothes;
		protected System.Web.UI.WebControls.DropDownList ddlGirlClothes;
		protected System.Web.UI.WebControls.Panel pnlGirlClothes;
		protected System.Web.UI.WebControls.DropDownList ddlBoyShoes;
		protected System.Web.UI.WebControls.DropDownList ddlGirlShoes;
		protected System.Web.UI.WebControls.Panel pnlBoyShoes;
		protected System.Web.UI.WebControls.Panel pnlGirlShoes;
		protected System.Web.UI.WebControls.DropDownList ddlBoyTrousers;
		protected System.Web.UI.WebControls.Panel pnlBoyTrousers;
		protected System.Web.UI.WebControls.DropDownList ddlGirlTrousers;
		protected System.Web.UI.WebControls.Panel pnlGirlTrousers;
		protected System.Web.UI.WebControls.Panel pnlBoyEyes;
		protected System.Web.UI.WebControls.Panel pnlGirlEyes;
		protected System.Web.UI.WebControls.DropDownList ddlBoyEyes;
		protected System.Web.UI.WebControls.DropDownList ddlGirlEyes;
		protected System.Web.UI.WebControls.Panel pnlBoyBrows;
		protected System.Web.UI.WebControls.Panel pnlGirlBrows;
		protected System.Web.UI.WebControls.DropDownList ddlBoyBrows;
		protected System.Web.UI.WebControls.DropDownList ddlGirlBrows;
		protected System.Web.UI.WebControls.Panel pnlBoyHair;
		protected System.Web.UI.WebControls.Panel pnlGirlHair;
		protected System.Web.UI.WebControls.DropDownList ddlBoyHair;
		protected System.Web.UI.WebControls.DropDownList ddlGirlHair;
		protected System.Web.UI.WebControls.Panel pnlGirlBlouseTypes;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList2;
		protected System.Web.UI.WebControls.RadioButtonList rblGirlBlouseTypes;

		public string bgfile;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			bgfile = Request.QueryString["background"];
			string ua = this.Request.UserAgent.ToUpper();
			if (ua.IndexOf("FIREFOX") >= 0)
			{
				Button1.Enabled = false;
				Button1.Text = "Please, do not use firefox.";
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
			this.ddlSex.SelectedIndexChanged += new System.EventHandler(this.ddlSex_SelectedIndexChanged);
			this.ddlGirlClothes.SelectedIndexChanged += new System.EventHandler(this.ddlGirlClothes_SelectedIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.setup_Init);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			// build the querystring qith the parameters
			string strBuildBody = String.Empty;
            
			strBuildBody += "size=" + ddlSize.SelectedValue;
			strBuildBody += "&sex=" + ddlSex.SelectedValue;
			
			// accessories
			if (ddlSex.SelectedValue.ToLower() == "boy")
			{
				for (int accessoryCounter = 0; accessoryCounter < cbkBoyAccessories.Items.Count; accessoryCounter++)
				{
					if (cbkBoyAccessories.Items[accessoryCounter].Selected)
						strBuildBody += "&acc" + accessoryCounter.ToString() + "=" + cbkBoyAccessories.Items[accessoryCounter].Text;
				}
			}
			
				// the girl has different accessories
			else 
			{
				strBuildBody += "&acc=" + ddlGirlAccessories.SelectedValue;
			}

			strBuildBody += "&skin=" + ddlSkin.SelectedValue;
			strBuildBody += "&hair=" + (pnlBoyHair.Visible ? ddlBoyHair.SelectedValue : ddlGirlHair.SelectedValue);
			strBuildBody += "&character=" + (pnlBoyCharacter.Visible ? ddlBoyCharacter.SelectedValue : ddlGirlCharacter.SelectedValue);
			strBuildBody += "&eyes=" + (pnlBoyEyes.Visible ? ddlBoyEyes.SelectedValue : ddlGirlEyes.SelectedValue);
			strBuildBody += "&brows=" + (pnlBoyBrows.Visible ? ddlBoyBrows.SelectedValue : ddlGirlBrows.SelectedValue);
			strBuildBody += "&shoes="  + (pnlBoyShoes.Visible ? ddlBoyShoes.SelectedValue : ddlGirlShoes.SelectedValue);
			strBuildBody += "&clothes=" + (pnlBoyClothes.Visible ? ddlBoyClothes.SelectedValue : ddlGirlClothes.SelectedValue);
			
			// if it's a girl and the selected coat is a blouse
			if ((ddlSex.SelectedValue.ToLower() == "girl") && (ddlGirlClothes.SelectedValue.ToLower() == "blouse"))
				strBuildBody += "&blouse_type=" + rblGirlBlouseTypes.SelectedValue;
			
			strBuildBody += "&trousers=" + (pnlBoyTrousers.Visible ? ddlBoyTrousers.SelectedValue : ddlGirlTrousers.SelectedValue);
			strBuildBody += "&bgfile=" + bgfile;

			Response.Redirect("showpicture.aspx" + "?" + strBuildBody);
		}

		/// <summary>
		/// sets visibility for panels according to the figure's sex
		/// </summary>
		private void SetPanelsVisibility()
		{
			// show boy or girl characters depending on sex
			pnlGirlCharacter.Visible = (ddlSex.SelectedValue.ToLower() == "girl");
			pnlBoyCharacter.Visible = (ddlSex.SelectedValue.ToLower() == "boy");

			pnlBoyAccessories.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlAccessories.Visible = (ddlSex.SelectedValue.ToLower() == "girl");

			pnlBoyClothes.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlClothes.Visible = (ddlSex.SelectedValue.ToLower() == "girl");

			pnlBoyShoes.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlShoes.Visible = (ddlSex.SelectedValue.ToLower() == "girl");
			
			pnlBoyTrousers.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlTrousers.Visible = (ddlSex.SelectedValue.ToLower() == "girl");
			
			pnlBoyEyes.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlEyes.Visible = (ddlSex.SelectedValue.ToLower() == "girl");		

			pnlBoyBrows.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlBrows.Visible = (ddlSex.SelectedValue.ToLower() == "girl");

			pnlBoyHair.Visible = (ddlSex.SelectedValue.ToLower() == "boy");
			pnlGirlHair.Visible = (ddlSex.SelectedValue.ToLower() == "girl");

			pnlGirlBlouseTypes.Visible = ((ddlGirlClothes.SelectedValue.ToLower() == "blouse") && (ddlSex.SelectedValue.ToLower() == "girl"));
		}

		/// <summary>
		/// handles page init event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setup_Init(object sender, EventArgs e)
		{
			SetPanelsVisibility();
		}

		/// <summary>
		/// handles the selection of boy and girl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ddlSex_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetPanelsVisibility();
		}

		/// <summary>
		/// chooses the type of blouse, when the coat is of type of blouse
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ddlGirlClothes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetPanelsVisibility();
		}
	}
}
