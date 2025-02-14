using System;

namespace MugginsDemo
{
	public class setupstep3 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.CheckBoxList cbkBoyAccessories;
		protected System.Web.UI.WebControls.Panel pnlBoyAccessories;
		protected System.Web.UI.WebControls.Panel pnlGirlAccessories;
		protected System.Web.UI.WebControls.DropDownList ddlBoyClothes;
		protected System.Web.UI.WebControls.Panel pnlBoyClothes;
		protected System.Web.UI.WebControls.DropDownList ddlGirlClothes;
		protected System.Web.UI.WebControls.Panel pnlGirlClothes;
		protected System.Web.UI.WebControls.RadioButtonList rblGirlBlouseTypes;
		protected System.Web.UI.WebControls.Panel pnlGirlBlouseTypes;
		protected System.Web.UI.WebControls.Label lblTrousers;
		protected System.Web.UI.WebControls.DropDownList ddlBoyTrousers;
		protected System.Web.UI.WebControls.Panel pnlBoyTrousers;
		protected System.Web.UI.WebControls.DropDownList ddlGirlTrousers;
		protected System.Web.UI.WebControls.Panel pnlGirlTrousers;
		protected System.Web.UI.WebControls.DropDownList ddlBoyShoes;
		protected System.Web.UI.WebControls.Panel pnlBoyShoes;
		protected System.Web.UI.WebControls.DropDownList ddlGirlShoes;
		protected System.Web.UI.WebControls.Panel pnlGirlShoes;
		protected System.Web.UI.WebControls.RadioButtonList rblBoyGlassesType;
		protected System.Web.UI.WebControls.Panel pnlBoyGlasses;
		protected System.Web.UI.WebControls.Button btnPreviousStep;
		protected System.Web.UI.WebControls.DropDownList ddlBoyHair;
		protected System.Web.UI.WebControls.Panel pnlBoyHair;
		protected System.Web.UI.WebControls.DropDownList ddlGirlHair;
		protected System.Web.UI.WebControls.Panel pnlGirlHair;
		protected System.Web.UI.WebControls.CheckBoxList cbkGirlAccessoriesNoScarf;
		protected System.Web.UI.WebControls.CheckBoxList cbkGirlAccessories;
		protected System.Web.UI.WebControls.Panel pnlGirlAccessoriesNoScarf;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblHair;
		protected System.Web.UI.WebControls.Label lblAccessories;
		protected System.Web.UI.WebControls.Label lblGlassesType;
		protected System.Web.UI.WebControls.Label lblClothes;
		protected System.Web.UI.WebControls.Label lblShoes;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.btnPreviousStep.Click += new System.EventHandler(this.btnPreviousStep_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.setupstep2_Init);

		}
		#endregion

		/// <summary>
		/// sets visibility for panels according to the figure's sex
		/// </summary>
		private void SetPanelsVisibility()
		{
			if (Request.QueryString["sex"] != null)
			{
				pnlBoyHair.Visible = (Request.QueryString["sex"].ToLower() == "boy");
				pnlGirlHair.Visible = (Request.QueryString["sex"].ToLower() == "girl");

				pnlBoyGlasses.Visible = (Request.QueryString["sex"].ToLower() == "boy");

				pnlBoyAccessories.Visible = (Request.QueryString["sex"].ToLower() == "boy");
				pnlGirlAccessories.Visible = ((Request.QueryString["sex"].ToLower() == "girl") && (Request.QueryString["character"].ToLower() != "angry") && (Request.QueryString["character"].ToLower() != "standard"));
			
				// if the girl is "angry" or "standard", she hasn't scarf as an accessory
				pnlGirlAccessoriesNoScarf.Visible = ((Request.QueryString["sex"].ToLower() == "girl") && ((Request.QueryString["character"].ToLower() == "angry") || (Request.QueryString["character"].ToLower() == "standard")));

				pnlBoyClothes.Visible = (Request.QueryString["sex"].ToLower() == "boy");
				pnlGirlClothes.Visible = (Request.QueryString["sex"].ToLower() == "girl");

				pnlBoyShoes.Visible = (Request.QueryString["sex"].ToLower() == "boy");
				pnlGirlShoes.Visible = (Request.QueryString["sex"].ToLower() == "girl");
			
				pnlBoyTrousers.Visible = (Request.QueryString["sex"].ToLower() == "boy");
				pnlGirlTrousers.Visible = (Request.QueryString["sex"].ToLower() == "girl");

				pnlGirlBlouseTypes.Visible = ((ddlGirlClothes.SelectedValue.ToLower() == "blouse") && (Request.QueryString["sex"].ToLower() == "girl"));
			}
		}

		#region event handlers
		private void Button1_Click(object sender, System.EventArgs e)
		{
			// build the querystring qith the parameters
			string strPreviousQueryString = Request.QueryString.ToString(), strBuildBody = String.Empty;
            
			// add the previous querystring to the current query string;
			strBuildBody += strPreviousQueryString;
			
			// hair type
			if ((Request.QueryString["sex"] != null) && Request.QueryString["sex"].ToLower() == "boy")
			{
				strBuildBody += "&hair=" + ddlBoyHair.SelectedValue;
			}
			
			else if ((Request.QueryString["sex"] != null) && Request.QueryString["sex"].ToLower() == "girl")
			{
				strBuildBody += "&hair=" + ddlGirlHair.SelectedValue;
			}

			// accessories
			if ((Request.QueryString["sex"] != null) && Request.QueryString["sex"].ToLower() == "boy")
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
				// strBuildBody += "&acc=" + cbkGirlAccessories.SelectedValue;
				for (int accessoryCounter = 0; accessoryCounter < cbkGirlAccessories.Items.Count; accessoryCounter++)
				{
					if (cbkGirlAccessories.Items[accessoryCounter].Selected)
						strBuildBody += "&acc" + accessoryCounter.ToString() + "=" + cbkGirlAccessories.Items[accessoryCounter].Text;
				}
			}
			
			// add glasses type
			strBuildBody+= "&glasses=" + rblBoyGlassesType.SelectedValue;

			strBuildBody += "&shoes="  + (pnlBoyShoes.Visible ? ddlBoyShoes.SelectedValue : ddlGirlShoes.SelectedValue);
			strBuildBody += "&clothes=" + (pnlBoyClothes.Visible ? ddlBoyClothes.SelectedValue : ddlGirlClothes.SelectedValue);
			
			// if it's a girl and the selected coat is a blouse
			if ((Request.QueryString["sex"] != null) && (Request.QueryString["sex"].ToLower() == "girl") && (ddlGirlClothes.SelectedValue.ToLower() == "blouse"))
				strBuildBody += "&blouse_type=" + rblGirlBlouseTypes.SelectedValue;
			
			strBuildBody += "&trousers=" + (pnlBoyTrousers.Visible ? ddlBoyTrousers.SelectedValue : ddlGirlTrousers.SelectedValue);

			Response.Redirect("showpicture.aspx" + "?" + strBuildBody);
		}

		/// <summary>
		/// handles page init event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setupstep2_Init(object sender, EventArgs e)
		{
			SetPanelsVisibility();
		}
		#endregion event handlers

		private void btnPreviousStep_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("setupstep2.aspx?bgfile=" + Request.QueryString["bgfile"]);
		}
	}
}
