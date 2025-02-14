using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpeechLib;
using System.Threading;

public partial class _Default : System.Web.UI.Page 
{
    // some required members 
    SpVoice speech = new SpVoice();
    int speechRate = 2; // Ranges from -10 to 10 
    int volume = 100; // Range from 0 to 100.

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

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
