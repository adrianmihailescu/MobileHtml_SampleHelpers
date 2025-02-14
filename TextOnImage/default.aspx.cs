using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using tools;
using SpeechLib;
using System.Threading;

namespace TextOnImage
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public class _default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnAddTextOverImage;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.UserControl ucDiskBrowser1;
		protected System.Web.UI.WebControls.Image imgTextOverImage;
		protected System.Web.UI.WebControls.Button btnAddTextLettersOverImage;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.RadioButtonList rblSimpleText;
		protected System.Web.UI.WebControls.RadioButtonList rblImageText;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.Button btnSwirlImage;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList2;
		protected System.Web.UI.WebControls.Button btnScrollTextLettersOverImage;
		protected System.Web.UI.WebControls.Button btnSpeakText;
		protected System.Web.UI.WebControls.DropDownList ddlSpeakTextOptions;
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.Label lblSelectImage;
	
		#region display methods
		/// <summary>
		/// draws a text with image letters on an image
		/// </summary>
		/// <param name="strImagePath">the image to draw text on</param>
		/// <param name="strNameToDraw">the text to be written on image</param>
		/// <param name="strLetterType">the type of text to be written on image</param>
		private void ScrollStringWithImageLetters(string strBackgroundImagePath, string strNameToDraw, string strLetterType)
		{
			try
			{
				if (strBackgroundImagePath == String.Empty)
					tools.tools.ShowWarningMessage(((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))), "Please select an image !");
				else
				{
					// for each iletter in text
					try
					{						
						string strImageLettersString = String.Empty;
						
						// for every leter in the text, add the corresponding letter on the background image
						for (int tempLetterCounter = 0; tempLetterCounter < strNameToDraw.Length; tempLetterCounter++)
						{
							if (strNameToDraw[tempLetterCounter] != ' ')
								strImageLettersString += "<img src=\"" + "images/letters/" + strLetterType + "/" + strNameToDraw[tempLetterCounter] + ".ico" + "\" />";
						}

						string strResponseString = String.Empty;
						
						Bitmap bmpImage = new Bitmap(strBackgroundImagePath);
						int width = bmpImage.Width, height = bmpImage.Height;
						strResponseString += "<div id=\"divMain\" style=\"background-image:url(" + strBackgroundImagePath + ");height:" + height + "px;width:" + width + "px;\">";
						strResponseString += "<marquee dir=\"ltr\">" + strImageLettersString + "</marquee>";
						strResponseString += "</div>";

						Response.Write(strResponseString);
					}
				
					catch (Exception ex)
					{
					}
				}
			}
			
			catch (Exception ex)
			{
				Response.Write("<img src=\"images/error.jpg\" />An error has occured. Click <a href=\"default.aspx\">here</a> to go to the default page.");
			}
		}

		/// <summary>
		/// draws a text with image letters on an image
		/// </summary>
		/// <param name="strImagePath">the image to draw text on</param>
		/// <param name="strNameToDraw">the text to be written on image</param>
		/// <param name="strLetterType">the type of text to be written on image</param>
		private void DrawStringWithImageLetters(string strImagePath, string strNameToDraw, string strLetterType)
		{
			try
			{
				if (strImagePath == String.Empty)
					tools.tools.ShowWarningMessage(((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))), "Please select an image !");
				else
				{
					Bitmap bitMapImage = new Bitmap(strImagePath);
					Graphics graphicImage = Graphics.FromImage(bitMapImage);

					//Smooth graphics is nice.
					graphicImage.SmoothingMode = SmoothingMode.AntiAlias;		

					int temporaryTotalWidth = 0;
					// for each iletter in text
					try
					{
						for (int tempLetterCounter = 0; tempLetterCounter < strNameToDraw.Length; tempLetterCounter++)
						{
							// we don't have space as a letter
							if (strNameToDraw[tempLetterCounter] != ' ')
							{
								Bitmap bitMapLetterImage = new Bitmap(Server.MapPath("~/images/letters/" + strLetterType + "/" + strNameToDraw[tempLetterCounter].ToString() + ".ico"));
								// count total width of the string built so far
								temporaryTotalWidth += bitMapLetterImage.Width;
								// add each letter to the image
								graphicImage.DrawImage(bitMapLetterImage, temporaryTotalWidth, 0);
							}
						}
					}
				
					catch (Exception ex)
					{
					}

					//Set the content type
					Response.ContentType="image/jpeg";

					//Save the new image to the response output stream.
					bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg);

					//Clean house.
					graphicImage.Dispose();
					bitMapImage.Dispose();
				}
			}
			
			catch (Exception ex)
			{
				Response.Write("<img src=\"images/error.jpg\" />An error has occured. Click <a href=\"default.aspx\">here</a> to go to the default page.");
			}
		}

		/// <summary>
		/// draws a text on an image
		/// </summary>
		/// <param name="strImagePath"></param>
		/// <param name="strTextToAdd"></param>
		private void DrawImageWithText(string strImagePath, string strTextToAdd)
		{
			try
			{
				if (strImagePath == String.Empty)
					tools.tools.ShowWarningMessage(((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))), "Please select an image !");
				else
				{
					Bitmap bitMapImage = new Bitmap(strImagePath);
					Graphics graphicImage = Graphics.FromImage(bitMapImage);

					//Smooth graphics is nice.
					graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
						
					// set properties from one type of image
					string strImageTypeToDisplay = "color";
					int displayCoordinateX = Convert.ToInt32(tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "X"));
					int displayCoordinateY = Convert.ToInt32(tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "Y"));
					string displayFontType = tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "FontType");
					string displayFontColor = tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "FontColor");
					string displayFontStyleToUse = tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "FontStyle");
					FontStyle displayFontStyle = tools.tools.GetTextStyleFromXmlValue(displayFontStyleToUse);
					int displayFontSize = Convert.ToInt32(tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "FontSize"));
					int displayAngle = Convert.ToInt32(tools.tools.GetTextDisplayInstructionsOnImage(strImageTypeToDisplay, "Angle"));
				
					//Write text on the image
					graphicImage.RotateTransform(displayAngle);
					graphicImage.DrawString(strTextToAdd, new Font(displayFontType, displayFontSize, displayFontStyle), new SolidBrush(Color.FromName(displayFontColor)), new Point(displayCoordinateX, displayCoordinateY));

					//Set the content type
					Response.ContentType="image/jpeg";

					//Save the new image to the response output stream.
					bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg);

					//Clean house.
					graphicImage.Dispose();
					bitMapImage.Dispose();
				}

				
			}
			
			catch (Exception ex)
			{
				Response.Write("<img src=\"images/error.jpg\" />An error has occured. Click <a href=\"default.aspx\">here</a> to go to the default page.");
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetPanelVisibility();
		}
		
		/// <summary>
		/// sets visibility for the page's items
		/// </summary>
		private void SetPanelVisibility()
		{
			rblImageText.Visible = ((rblSimpleText.SelectedIndex == 1) || (rblSimpleText.SelectedIndex == 3));
			btnAddTextOverImage.Visible = (rblSimpleText.SelectedIndex == 0);
			btnAddTextLettersOverImage.Visible = (rblSimpleText.SelectedIndex == 1);
			btnSwirlImage.Visible = (rblSimpleText.SelectedIndex == 2);
			btnScrollTextLettersOverImage.Visible = (rblSimpleText.SelectedIndex == 3);
			btnSpeakText.Visible = (rblSimpleText.SelectedIndex == 4);

			ddlSpeakTextOptions.Visible = (rblSimpleText.SelectedIndex == 4);
		}

		#endregion display methods

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
			this.rblSimpleText.SelectedIndexChanged += new System.EventHandler(this.rblSimpleText_SelectedIndexChanged);
			this.btnAddTextOverImage.Click += new System.EventHandler(this.btnAddTextOverImage_Click);
			this.btnAddTextLettersOverImage.Click += new System.EventHandler(this.btnAddTextLettersOverImage_Click);
			this.btnSwirlImage.Click += new System.EventHandler(this.btnSwirlImage_Click);
			this.btnScrollTextLettersOverImage.Click += new System.EventHandler(this.btnScrollTextLettersOverImage_Click);
			this.btnSpeakText.Click += new System.EventHandler(this.btnSpeakText_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region event handlers		
		private void btnAddTextOverImage_Click(object sender, System.EventArgs e)
		{
			string strPathToImageFile = ((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))).Text;
			string strTextToAddOnImage = TextBox1.Text;
			// draw text on image
			DrawImageWithText(strPathToImageFile, strTextToAddOnImage);
		}
		
		private void btnAddTextLettersOverImage_Click(object sender, System.EventArgs e)
		{
			string strPathToImageFile = ((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))).Text;
			string strTextToAddOnImage = TextBox1.Text;
			// draw text on image
			DrawStringWithImageLetters(strPathToImageFile, strTextToAddOnImage, rblImageText.SelectedValue);
		}
		
		private void rblSimpleText_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetPanelVisibility();
		}

		private void btnSwirlImage_Click(object sender, System.EventArgs e)
		{
			tools.tools.GenerateSwirlImageFromText(200, 150, TextBox1.Text, Color.Blue, Color.White).Save(Response.OutputStream, ImageFormat.Jpeg);
		}

		private void btnScrollTextLettersOverImage_Click(object sender, System.EventArgs e)
		{
			string strPathToImageFile = ((Label)(ucDiskBrowser1.FindControl("lblFoundFile"))).Text;
			string strTextToAddOnImage = TextBox1.Text;
			string strLetterType = rblImageText.SelectedValue;

			ScrollStringWithImageLetters(strPathToImageFile, strTextToAddOnImage, strLetterType);
		}

		/// <summary>
		/// speaks the text typed in the textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSpeakText_Click(object sender, System.EventArgs e)
		{
			// some required members 
			SpVoice speech = new SpVoice();
			int speechRate = 2; // Ranges from -10 to 10 
			int volume = 100;

			if (ddlSpeakTextOptions.SelectedValue == "play")
			{
				// play the file
				lblResult.Text = "Playing file...";

				speech.Rate = speechRate;
				speech.Volume = volume;
				speech.Speak(TextBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
				lblResult.Text = String.Empty;
			}
			
			else if (ddlSpeakTextOptions.SelectedValue == "save")
			{
				// save the file to disk
				lblResult.Text = String.Empty;
				string strSavePathFile = @"d:\full access\1.wav";

				SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
				SpFileStream SpFileStream = new SpFileStream();
				SpFileStream.Open(strSavePathFile, SpFileMode, false);
				speech.AudioOutputStream = SpFileStream;
				speech.Rate = speechRate;
				speech.Volume = volume;
				speech.Speak(TextBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
				speech.WaitUntilDone(Timeout.Infinite);
				SpFileStream.Close();

				lblResult.Text = "<img src=\"images/information.jpg\" /> The wav file has been written to disk: " + strSavePathFile;
			}
		}
		#endregion event handlers

	}
}
