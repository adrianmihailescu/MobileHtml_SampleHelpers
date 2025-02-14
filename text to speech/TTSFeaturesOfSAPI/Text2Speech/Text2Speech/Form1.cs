using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SpeechLib;
using System.Threading;

namespace Text2Speech
{
    public partial class Form1 : Form
    {
         // some required members 
          SpVoice speech = new SpVoice();
          int speechRate = 2; // Ranges from -10 to 10 
          int volume = 30; // Range from 0 to 100.
          bool paused = false;


        public Form1()
        {
            InitializeComponent();
            foreach (ISpeechObjectToken Token in speech.GetVoices(string.Empty, string.Empty))
            {
                // Populate the ComboBox Entries ..
                cmbVoices.Items.Add(Token.GetDescription(49));
            }

            cmbVoices.SelectedIndex = 0;    // Select the first Index of the comboBox 
            tbarRate.Value = speechRate;
            trbVolume.Value = volume;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             // load the combo for list of Voices available.
                     
           
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            if (paused)
            {
                speech.Resume();
                paused = false;
            }
            else
            {
                speech.Rate = speechRate;
                speech.Volume = volume;
                speech.Speak(tbspeech.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
           

        }
        ////
               
        private void btnToWAV_Click(object sender, EventArgs e)
        {
            try
            {
               

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "All files (*.*)|*.*|wav files (*.wav)|*.wav";
                sfd.Title = "Save to a wave file";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
                    SpFileStream SpFileStream = new SpFileStream();
                    SpFileStream.Open(sfd.FileName, SpFileMode, false);
                    speech.AudioOutputStream = SpFileStream;
                    speech.Rate = speechRate;
                    speech.Volume = volume;
                    speech.Speak(tbspeech.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                    speech.WaitUntilDone(Timeout.Infinite);
                    SpFileStream.Close();
                }
            }
            catch
            {
                MessageBox.Show("There is some error in converting to Wav file.");
            }
        }

        private void cmbVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            speech.Voice = speech.GetVoices(string.Empty, string.Empty).Item(cmbVoices.SelectedIndex); 
        }

        private void tbarRate_Scroll(object sender, EventArgs e)
        {
            speechRate = tbarRate.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            speech.Pause();
            paused = true;
        }

       
        private void trbVolume_Scroll(object sender, EventArgs e)
        {
            volume = trbVolume.Value;
        }

    }
}