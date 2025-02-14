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
using SpeechLib;
using System.Threading;

namespace TextToSpeech11
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
	
		// some required members 
		SpVoice speech = new SpVoice();
		int speechRate = 2; // Ranges from -10 to 10 
		int volume = 100;
		protected System.Web.UI.WebControls.TextBox txtTextToSpeak;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqTextToSpeak;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Button btnPlayOrSave;
		protected System.Web.UI.WebControls.Label lblResult; // Range from 0 to 100.

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// plays the voice for text or saves it to disk
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPlayOrSave_Click(object sender, EventArgs e)
		{
			if (RadioButtonList1.SelectedIndex == 0)
			{
				// play the file
				lblResult.Text = "Playing file...";

				speech.Rate = speechRate;
				speech.Volume = volume;
				speech.Speak(txtTextToSpeak.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
				lblResult.Text = String.Empty;
			}
        
			else
			{
				// save the file to disk
				lblResult.Text = String.Empty;

				SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
				SpFileStream SpFileStream = new SpFileStream();
				SpFileStream.Open(@"d:\full access\1.wav", SpFileMode, false);
				speech.AudioOutputStream = SpFileStream;
				speech.Rate = speechRate;
				speech.Volume = volume;
				speech.Speak(txtTextToSpeak.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
				speech.WaitUntilDone(Timeout.Infinite);
				SpFileStream.Close();

				lblResult.Text = "The wav file has been written to disk.";
			}
		}
	}
}
