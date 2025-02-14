namespace Text2Speech
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbspeech = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSpeak = new System.Windows.Forms.Button();
            this.btnToWAV = new System.Windows.Forms.Button();
            this.cmbVoices = new System.Windows.Forms.ComboBox();
            this.tbarRate = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.trbVolume = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbarRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // tbspeech
            // 
            this.tbspeech.Location = new System.Drawing.Point(12, 43);
            this.tbspeech.Multiline = true;
            this.tbspeech.Name = "tbspeech";
            this.tbspeech.Size = new System.Drawing.Size(213, 104);
            this.tbspeech.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Enter the text below to speak! ";
            // 
            // btnSpeak
            // 
            this.btnSpeak.Location = new System.Drawing.Point(89, 163);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Size = new System.Drawing.Size(75, 23);
            this.btnSpeak.TabIndex = 2;
            this.btnSpeak.Text = "Speak";
            this.btnSpeak.UseVisualStyleBackColor = true;
            this.btnSpeak.Click += new System.EventHandler(this.btnSpeak_Click);
            // 
            // btnToWAV
            // 
            this.btnToWAV.Location = new System.Drawing.Point(8, 163);
            this.btnToWAV.Name = "btnToWAV";
            this.btnToWAV.Size = new System.Drawing.Size(75, 23);
            this.btnToWAV.TabIndex = 3;
            this.btnToWAV.Text = "To Wav";
            this.btnToWAV.UseVisualStyleBackColor = true;
            this.btnToWAV.Click += new System.EventHandler(this.btnToWAV_Click);
            // 
            // cmbVoices
            // 
            this.cmbVoices.FormattingEnabled = true;
            this.cmbVoices.Location = new System.Drawing.Point(304, 46);
            this.cmbVoices.Name = "cmbVoices";
            this.cmbVoices.Size = new System.Drawing.Size(121, 21);
            this.cmbVoices.TabIndex = 4;
            this.cmbVoices.SelectedIndexChanged += new System.EventHandler(this.cmbVoices_SelectedIndexChanged);
            // 
            // tbarRate
            // 
            this.tbarRate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbarRate.Location = new System.Drawing.Point(253, 43);
            this.tbarRate.Minimum = -10;
            this.tbarRate.Name = "tbarRate";
            this.tbarRate.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbarRate.Size = new System.Drawing.Size(45, 104);
            this.tbarRate.TabIndex = 5;
            this.tbarRate.Scroll += new System.EventHandler(this.tbarRate_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = " 10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "- 10";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(416, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(253, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rate:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(301, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Select Person:";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(170, 163);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // trbVolume
            // 
            this.trbVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trbVolume.LargeChange = 20;
            this.trbVolume.Location = new System.Drawing.Point(446, 43);
            this.trbVolume.Maximum = 100;
            this.trbVolume.Name = "trbVolume";
            this.trbVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbVolume.Size = new System.Drawing.Size(45, 104);
            this.trbVolume.SmallChange = 10;
            this.trbVolume.TabIndex = 12;
            this.trbVolume.TickFrequency = 5;
            this.trbVolume.Scroll += new System.EventHandler(this.trbVolume_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(469, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = " 100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(480, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(443, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Volume:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 200);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trbVolume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbarRate);
            this.Controls.Add(this.cmbVoices);
            this.Controls.Add(this.btnToWAV);
            this.Controls.Add(this.btnSpeak);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbspeech);
            this.Name = "Form1";
            this.Text = "MYRSH | Text To Speech and Text To Wav using SAPI";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbarRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbspeech;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSpeak;
        private System.Windows.Forms.Button btnToWAV;
        private System.Windows.Forms.ComboBox cmbVoices;
        private System.Windows.Forms.TrackBar tbarRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TrackBar trbVolume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

