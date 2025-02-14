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
using System.Drawing;
using System.Drawing.Imaging;
using Gif.Components;

namespace TextOnImage
{
	/// <summary>
	/// Summary description for testpage.
	/// </summary>
	public class testpage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblResult;
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				/* create Gif */
				// files path array
				String [] imageFilePaths = new String[]{"d:\\full access\\heroes2.jpg","d:\\full access\\pekes1.jpg","d:\\full access\\pekes2.jpg"}; 
				
				// output file path
				String outputFilePath = "d:\\full access\\test.gif";
				
				// start creating the animated gif
				AnimatedGifEncoder ef = new AnimatedGifEncoder();
				ef.Start( outputFilePath );
				ef.SetDelay(500);
				//-1:no repeat,0:always repeat
				ef.SetRepeat(0);
				for (int i = 0, count = imageFilePaths.Length; i < count; i++ ) 
				{
					ef.AddFrame( System.Drawing.Image.FromFile( imageFilePaths[i] ) );
				}
				ef.Finish();
				/* extract Gif */
				string outputPath = "c:\\";
				GifDecoder gifDecoder = new GifDecoder();
				gifDecoder.Read( "c:\\test.gif" );
				for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
				{
					System.Drawing.Image frame = gifDecoder.GetFrame( i );  // frame i
					frame.Save( outputPath + Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg );
				}

				lblResult.Text = "<img src=\"images/information.jpg\" /> The animated gif has been created";
			}

			catch (Exception ex)
			{
				lblResult.Text = "<img src=\"images/error.jpg\" /> An error has occured: " + ex.Message;;
			}
		}
	}
}
