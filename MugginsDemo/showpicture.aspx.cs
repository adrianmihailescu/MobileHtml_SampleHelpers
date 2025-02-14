using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;

namespace MugginsDemo
{
	public class Catalog : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string bgfile = Request.QueryString["bgfile"];
			string size = Request.QueryString["size"].ToUpper();
			string sex = Request.QueryString["sex"].ToUpper();
			string skin = Request.QueryString["skin"].ToUpper();
			string hairType = Request.QueryString["hair"].ToUpper();
			string character = Request.QueryString["character"].ToUpper();
			string eyesColor = Request.QueryString["eyes"].ToUpper();
			string hairColor = Request.QueryString["hairColor"].ToUpper();
			string accesories = (Request.QueryString["acc"] != null && Request.QueryString["acc"] != "") ? Request.QueryString["acc"].ToUpper() : "";
			string shoes = Request.QueryString["shoes"].ToUpper();
			string clothes = Request.QueryString["clothes"].ToUpper();
			string trousers = Request.QueryString["trousers"].ToUpper();
			
			string[] spl = size.Split('X');
			int width = Convert.ToInt32(spl[0]);
			int height = Convert.ToInt32(spl[1]);

			if (bgfile != null && bgfile != "")
			{
				Response.ContentType = "image/jpeg";
				Response.Clear();

				// Buffer response so that page is sent
				// after processing is complete.
				Response.BufferOutput = true;

				Bitmap objImage, bitMapImage; 
				Graphics graphicImage;

				bitMapImage = new Bitmap(width, height);
				graphicImage = Graphics.FromImage(bitMapImage); 
					
				string url = String.Format("http://content.k-mobile.com/V2/DATA/IMG/{0}/{1}/DAT/JPGX/{1}.JPGX_{2}x{3}x24.jpg", bgfile.Substring(0, 1), bgfile, width, height);
				WebRequest webrq = WebRequest.Create(url);
				objImage = (Bitmap)Bitmap.FromStream(webrq.GetResponse().GetResponseStream());
				graphicImage.DrawImage(objImage, 0, 0);
						
				// Apply smooth graphics to the image
				graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

				// the dressing structure is different from boy to girl
				if (sex == "BOY")
				{
					#region BOY
					string sunglasses = String.Empty;
					string sleeves = "SLEEVES1";

					if (character == "COOL" || character == "FLIRT"  || character == "HAPPY")
						sleeves = "SLEEVES2";

					// draw body
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{0}.png", sex, size, skin)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}

					// brows
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/BROWS/{4}.png", sex, size, skin, character, hairColor)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}

					// draw shoes
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/SHOES/{2}.png", sex, size, shoes)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
			
					// draw trousers
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/TROUSERS/{2}.png", sex, size, trousers)));
						graphicImage.DrawImage(objImage, 0, 0);			
					}
					catch{}
		
					// draw hair
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/HAIR/{2}/{3}.png", sex, size, hairType, hairColor)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
					
					// draw coat
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/{2}/BODY.png", sex, size, clothes)));
						graphicImage.DrawImage(objImage, 0, 0);
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/{2}/{3}.png", sex, size, clothes, sleeves)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
			
					// draw face
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/FACE.png", sex, size, skin, character)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
				

					// draw accessories			
					try
					{
						string[] acc = accesories.Split(',');
						for (int i = 0; i< acc.Length; i++)
						{
							if ((acc[i] != null) && (acc[i].IndexOf("GLASSES") < 0))
							{
								Trace.Warn(acc[i]);
								string strBuildTempAccessories = "~/images/{0}/{1}/ACCESSORIES/{2}.png";
								if (acc[i] == "CAP")
								{
									// draw cap. The cap is different for different characters
									strBuildTempAccessories = "~/images/{0}/{1}/HAIR/{2}/CAP.png";

									objImage = new Bitmap(Server.MapPath(String.Format(strBuildTempAccessories, sex, size, hairType)));
									graphicImage.DrawImage(objImage, 0, 0);
								}
								else if (acc[i] == "WATCH")
								{
									// draw watch. The watch is different for different characters
									switch (character)
									{
										case "FURIOUS": 
										case "FLIRT":
										case "COOL":
										case "HAPPY":
											strBuildTempAccessories = "~/images/{0}/{1}/ACCESSORIES/WATCHMOOD.png";
											break;
										default:
											strBuildTempAccessories = "~/images/{0}/{1}/ACCESSORIES/WATCH.png";
											break;
									}
									objImage = new Bitmap(Server.MapPath(String.Format(strBuildTempAccessories, sex, size, acc[i])));
									graphicImage.DrawImage(objImage, 0, 0);
								}
								else
								{
									objImage = new Bitmap(Server.MapPath(String.Format(strBuildTempAccessories, sex, size, acc[i])));
									graphicImage.DrawImage(objImage, 0, 0);						
								}
							}
						}
					}
					catch{}

					try
					{
						if (Request.QueryString["glasses"].ToUpper() == "NOGLASSES")
						{
							// eyes
							objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/EYES.png", sex, size, skin, character)));
							graphicImage.DrawImage(objImage, 0, 0);
					
							objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/EYES/{4}.png", sex, size, skin, character, eyesColor)));
							graphicImage.DrawImage(objImage, 0, 0);			
						}
				
						else
						{
							// if he has glasses, draw them
							sunglasses = Request.QueryString["glasses"].ToUpper();

							objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/ACCESSORIES/{2}.png", sex, size, sunglasses)));
							graphicImage.DrawImage(objImage, 0, 0);
						}
					}
					catch{}

					// draw hands
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/HANDS.png", sex, size, skin, character)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
					#endregion
				}
		
					// if it's a girl
				else
				{
					#region GIRL
					// draw back hair
					try
					{
						if (character == "CURLY" || character == "FLAT" || character == "FRINGE")
						{
							objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/HAIR/{2}/BACK/{3}.png", sex, size, hairType, hairColor)));
							graphicImage.DrawImage(objImage, 0, 0);
						}
					}
					catch{}				
					// draw body
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/{0}.png", sex, size, skin, character)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}				
					// draw shoes
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/SHOES/{2}.png", sex, size, shoes)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}			
					// draw lower body
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/LOWER BODY/{2}.png", sex, size, trousers)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}			
					// draw upper body
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/CLOTHES/UPPER BODY/{2}.png", sex, size, clothes)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
					// draw front hair
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/HAIR/{2}/FRONT/{3}.png", sex, size, hairType, hairColor)));
						graphicImage.DrawImage(objImage, 0, 0);				
					}
					catch{}
					// eyes
					try
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/EYES/{4}.png", sex, size, skin, character, eyesColor)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					catch{}
					// draw arms
					if (character != "STANDARD")
					{
						objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/SKIN/{2}/{3}/ARM.png", sex, size, skin, character)));
						graphicImage.DrawImage(objImage, 0, 0);
					}
					// sunglasses

					try
					{
						string sunglasses = Request.QueryString["glasses"].ToUpper();
						if (sunglasses != "NOGLASSES")
						{
							objImage = new Bitmap(Server.MapPath(String.Format("~/images/{0}/{1}/ACCESSORIES/{2}/{3}.png", sex, size, character, sunglasses)));
							graphicImage.DrawImage(objImage, 0, 0);
						}
					}
					catch{}

					// draw accessories
					string[] acc = accesories.Split(',');
					for (int i = 0; i< acc.Length; i++)
					{
						if (acc[i] != "CAP")
						{
							string strBuildTempAccessories = "~/images/{0}/{1}/ACCESSORIES/{2}/{3}.png";
							try
							{
								objImage = new Bitmap(Server.MapPath(String.Format(strBuildTempAccessories, sex, size, character, acc[i])));
								graphicImage.DrawImage(objImage, 0, 0);		
							}
							catch{}
						}
						else
						{
							string strBuildTempAccessories = "~/images/{0}/{1}/HAIR/{2}/PINK_HAT.png";
							try
							{
								objImage = new Bitmap(Server.MapPath(String.Format(strBuildTempAccessories, sex, size, hairType)));
								graphicImage.DrawImage(objImage, 0, 0);		
							}
							catch{}
						}
					}
					#endregion
				}

				bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg);
				graphicImage.Dispose(); bitMapImage.Dispose();
				Response.Flush();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
