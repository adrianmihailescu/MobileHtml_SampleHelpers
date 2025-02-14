<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtSpeach = New System.Windows.Forms.RichTextBox
        Me.cmbVoices = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSpeak = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadFromTextFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.flTxtFile = New System.Windows.Forms.OpenFileDialog
        Me.trVolume = New System.Windows.Forms.TrackBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSavetoFile = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.MenuStrip1.SuspendLayout()
        CType(Me.trVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSpeach
        '
        Me.txtSpeach.Location = New System.Drawing.Point(12, 112)
        Me.txtSpeach.Name = "txtSpeach"
        Me.txtSpeach.Size = New System.Drawing.Size(413, 164)
        Me.txtSpeach.TabIndex = 1
        Me.txtSpeach.Text = "Enter text here"
        '
        'cmbVoices
        '
        Me.cmbVoices.FormattingEnabled = True
        Me.cmbVoices.Location = New System.Drawing.Point(109, 26)
        Me.cmbVoices.Name = "cmbVoices"
        Me.cmbVoices.Size = New System.Drawing.Size(157, 21)
        Me.cmbVoices.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Select Voice Type"
        '
        'btnSpeak
        '
        Me.btnSpeak.Location = New System.Drawing.Point(12, 288)
        Me.btnSpeak.Name = "btnSpeak"
        Me.btnSpeak.Size = New System.Drawing.Size(75, 23)
        Me.btnSpeak.TabIndex = 0
        Me.btnSpeak.Text = "Speak"
        Me.btnSpeak.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(431, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadFromTextFileToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'LoadFromTextFileToolStripMenuItem
        '
        Me.LoadFromTextFileToolStripMenuItem.Name = "LoadFromTextFileToolStripMenuItem"
        Me.LoadFromTextFileToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.LoadFromTextFileToolStripMenuItem.Text = "&Load From Text File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'flTxtFile
        '
        Me.flTxtFile.DefaultExt = "txt"
        '
        'trVolume
        '
        Me.trVolume.Location = New System.Drawing.Point(109, 48)
        Me.trVolume.Maximum = 100
        Me.trVolume.Minimum = 10
        Me.trVolume.Name = "trVolume"
        Me.trVolume.Size = New System.Drawing.Size(157, 45)
        Me.trVolume.TabIndex = 5
        Me.trVolume.TickFrequency = 10
        Me.trVolume.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.trVolume.Value = 100
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Volume "
        '
        'btnSavetoFile
        '
        Me.btnSavetoFile.Location = New System.Drawing.Point(109, 288)
        Me.btnSavetoFile.Name = "btnSavetoFile"
        Me.btnSavetoFile.Size = New System.Drawing.Size(130, 23)
        Me.btnSavetoFile.TabIndex = 7
        Me.btnSavetoFile.Text = "Save to Audio File"
        Me.btnSavetoFile.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "MyVoice.wav"
        Me.SaveFileDialog1.Filter = "Wave (*.wav)|*.wav"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 314)
        Me.Controls.Add(Me.btnSavetoFile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.trVolume)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbVoices)
        Me.Controls.Add(Me.txtSpeach)
        Me.Controls.Add(Me.btnSpeak)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.trVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSpeach As System.Windows.Forms.RichTextBox
    Friend WithEvents cmbVoices As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSpeak As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadFromTextFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents flTxtFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents trVolume As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSavetoFile As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
