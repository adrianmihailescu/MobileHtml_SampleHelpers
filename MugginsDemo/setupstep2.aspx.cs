using System;
using System.Web.UI.WebControls;

namespace MugginsDemo
{
	public class setupstep2 : System.Web.UI.Page
	{
		protected DropDownList ddlCharacter, ddlHair, ddlHairColor, ddlShoes, ddlClothes, ddlTrousers;
		protected Button Button1, btnPreviousStep;
		protected CheckBoxList cbkAccessories;
		protected RadioButtonList rbtGlassesType;
		protected Panel pnlAccesoriesBoys;
	
		public string bgfile, sex;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblCharacter;
		protected System.Web.UI.WebControls.Label lblHair;
		protected System.Web.UI.WebControls.Label lblGlassesType;
		protected System.Web.UI.WebControls.Label lblAccessories;
		protected System.Web.UI.WebControls.Label lblClothes;
		protected System.Web.UI.WebControls.Label lblTrousers;
		protected System.Web.UI.WebControls.Image imgInformationSign;
		protected System.Web.UI.WebControls.Image imgLogo;
		protected System.Web.UI.WebControls.Label lblShoes;

		private void Page_Load(object sender, System.EventArgs e) 
		{
			sex = Request.QueryString["sex"].ToUpper();

			if (sex == "BOY")
			{
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterCool", Request.QueryString["lang"]), "cool"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterFlirt", Request.QueryString["lang"]), "flirt"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterFurious", Request.QueryString["lang"]), "furious"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterHappyB", Request.QueryString["lang"]), "happy"));

				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeDread", Request.QueryString["lang"]), "dread"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeLong", Request.QueryString["lang"]), "long"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypePosh", Request.QueryString["lang"]), "posh"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeShort", Request.QueryString["lang"]), "short"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeSoldier", Request.QueryString["lang"]), "soldier"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeSpiky", Request.QueryString["lang"]), "spiky"));

				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorBlack", Request.QueryString["lang"]), "black"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorBlond", Request.QueryString["lang"]), "blond"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorDarkBrown", Request.QueryString["lang"]), "brown"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorLightBrown", Request.QueryString["lang"]), "lbrown"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorRed", Request.QueryString["lang"]), "red"));
				
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2Glasses", Request.QueryString["lang"]), "glasses"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2SunGlasses", Request.QueryString["lang"]), "sunglasses"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GlassesSport", Request.QueryString["lang"]), "glassessport"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2NoGlasses", Request.QueryString["lang"]), "noglasses"));
				rbtGlassesType.Items[3].Selected = true;

				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2AccessoryKeys", Request.QueryString["lang"]), "keys"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2AccessoryEarPiece", Request.QueryString["lang"]), "earpiece"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2AccessoryLaptopBag", Request.QueryString["lang"]), "laptopbag"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2AccessoryShoulderBag", Request.QueryString["lang"]), "shoulderbag"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2AccessoryCap", Request.QueryString["lang"]), "cap"));

				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesCoat", Request.QueryString["lang"]), "coat"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesShirt", Request.QueryString["lang"]), "shirt"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesSweatShirt", Request.QueryString["lang"]), "sweatshirt"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesTShirt", Request.QueryString["lang"]), "tshirt"));

				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2TrousersArmy", Request.QueryString["lang"]), "army"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2TrousersBrown", Request.QueryString["lang"]), "brown"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2TrousersJeans", Request.QueryString["lang"]), "jeans"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2TrousersPleated", Request.QueryString["lang"]), "pleated"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2TrousersSport", Request.QueryString["lang"]), "sport"));

				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ShoesAllStars", Request.QueryString["lang"]), "black"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ShoesClassical", Request.QueryString["lang"]), "brown"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ShoesCamper", Request.QueryString["lang"]), "lbrown"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ShoesSport", Request.QueryString["lang"]), "whiteblue"));
			}
			else
			{
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterAngry", Request.QueryString["lang"]), "angry"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterFun", Request.QueryString["lang"]), "fun"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterHappyG", Request.QueryString["lang"]), "happy"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterSexy", Request.QueryString["lang"]), "sexy"));
				ddlCharacter.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2CharacterStandard", Request.QueryString["lang"]), "standard"));

				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeCurly", Request.QueryString["lang"]), "curly"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeFlat", Request.QueryString["lang"]), "flat"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeFringe", Request.QueryString["lang"]), "fringe"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeMedium", Request.QueryString["lang"]), "medium"));
				ddlHair.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairTypeShort", Request.QueryString["lang"]), "short"));
				
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorBlack", Request.QueryString["lang"]), "black"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorBlond", Request.QueryString["lang"]), "blond"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorDarkBrown", Request.QueryString["lang"]), "brown"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorViolet", Request.QueryString["lang"]), "violet"));
				ddlHairColor.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2HairColorRed", Request.QueryString["lang"]), "red"));
				
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlGlassesBlue", Request.QueryString["lang"]), "sun_glasses_blue"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlGlassesDark", Request.QueryString["lang"]), "sun_glasses_dark"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlGlassesOrange", Request.QueryString["lang"]), "sun_glasses_orange"));
				rbtGlassesType.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlGlassesNo", Request.QueryString["lang"]), "noglasses"));
				rbtGlassesType.Items[3].Selected = true;

				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesBangle", Request.QueryString["lang"]), "bangle"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesBeachBag", Request.QueryString["lang"]), "beach_bag"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesCap", Request.QueryString["lang"]), "cap"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesHairPin", Request.QueryString["lang"]), "hairpin"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesPinkScarf", Request.QueryString["lang"]), "pink_scarf"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesHeadBand", Request.QueryString["lang"]), "head_band"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesNeckCross", Request.QueryString["lang"]), "neck_cross"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesNeckHeart", Request.QueryString["lang"]), "neck_heart"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesPalestinaScarf", Request.QueryString["lang"]), "palestina_scarf"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesHandBag", Request.QueryString["lang"]), "handbag"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesPurse", Request.QueryString["lang"]), "purse"));
				cbkAccessories.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlAccessoriesShoulderBagBlue", Request.QueryString["lang"]), "shoulder_bag_blue"));
				
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesEveningGown", Request.QueryString["lang"]), "evening_gown"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesTankTop", Request.QueryString["lang"]), "tanktop"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesTop", Request.QueryString["lang"]), "top"));
				ddlClothes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2ClothesWaistCoat", Request.QueryString["lang"]), "waistcoat"));

				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlTrousersBellJeans", Request.QueryString["lang"]), "bell_jeans"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlTrousersLeggins", Request.QueryString["lang"]), "leggins"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlTrousersMiniLeggins", Request.QueryString["lang"]), "mini_leggins"));
				ddlTrousers.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlTrousersSkinnyJeans", Request.QueryString["lang"]), "skinny_jeans"));
			
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlShoesBallerina", Request.QueryString["lang"]), "ballerina"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlShoesBeachSlippers", Request.QueryString["lang"]), "beach_slippers"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlShoesBooties", Request.QueryString["lang"]), "booties"));
				ddlShoes.Items.Add(new ListItem(tools.tools.GetXmlValue("Setup2GirlShoesHighHeels", Request.QueryString["lang"]), "high_heels"));
			}
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
			// set labels text according to the selected language
			lblCharacter.Text = tools.tools.GetXmlValue("Setup2Character", Request.QueryString["lang"]);
			lblHair.Text = tools.tools.GetXmlValue("Setup2Hair", Request.QueryString["lang"]);
			lblGlassesType.Text = tools.tools.GetXmlValue("Setup2Glasses", Request.QueryString["lang"]);
			lblAccessories.Text = tools.tools.GetXmlValue("Setup2Accessories", Request.QueryString["lang"]);
			lblClothes.Text = tools.tools.GetXmlValue("Setup2Clothes", Request.QueryString["lang"]);
			lblTrousers.Text = tools.tools.GetXmlValue("Setup2Trousers", Request.QueryString["lang"]);
			lblShoes.Text = tools.tools.GetXmlValue("Setup2Shoes", Request.QueryString["lang"]);
			Label1.Text = tools.tools.GetXmlValue("Setup2CharacterDetails", Request.QueryString["lang"]);
			btnPreviousStep.Text = tools.tools.GetXmlValue("Setup2BackToPreviousStep", Request.QueryString["lang"]);
			Button1.Text = tools.tools.GetXmlValue("Setup2SubmitChoice", Request.QueryString["lang"]);
			//
		}
		#endregion xml methods
		
		#region event handlers
		private void Button1_Click(object sender, System.EventArgs e)
		{	
			// build the querystring with the parameters
			string strBuildBody = String.Empty;
			strBuildBody += Request.QueryString.ToString() + "&hair=" + ddlHair.SelectedValue + "&hairColor=" + ddlHairColor.SelectedValue + "&character=" + ddlCharacter.SelectedValue;

			for (int accessoryCounter = 0; accessoryCounter < cbkAccessories.Items.Count; accessoryCounter++)
			{
				if (cbkAccessories.Items[accessoryCounter].Selected)
					strBuildBody += "&acc=" + cbkAccessories.Items[accessoryCounter].Value;
					//strBuildBody += "&acc" + accessoryCounter.ToString() + "=" + cbkAccessories.Items[accessoryCounter].Text;
			}
			
			strBuildBody+= "&glasses=" + rbtGlassesType.SelectedValue;
			strBuildBody += "&shoes="  + ddlShoes.SelectedValue;
			strBuildBody += "&clothes=" + ddlClothes.SelectedValue;
			strBuildBody += "&trousers=" + ddlTrousers.SelectedValue;

			Response.Redirect("showpicture.aspx" + "?" + strBuildBody);
		}
		
		private void btnPreviousStep_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("setupstep1.aspx?lang=" + Request.QueryString["lang"]);
		}
		#endregion event handlers
	}
}
