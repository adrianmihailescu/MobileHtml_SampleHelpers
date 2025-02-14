Public Class Form1

    
    Private Sub btnSpeak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpeak.Click
        Me.Cursor = Cursors.WaitCursor
        Dim oVoice As New SpeechLib.SpVoice
        Dim cpFileStream As New SpeechLib.SpFileStream
         
        oVoice.Voice = oVoice.GetVoices.Item(cmbVoices.SelectedIndex)
        oVoice.Volume = trVolume.Value
        oVoice.Speak(txtSpeach.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFDefault)
        oVoice = Nothing
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim x As New SpeechLib.SpVoice
        Dim arrVoices As SpeechLib.ISpeechObjectTokens = x.GetVoices
        Dim arrLst As New ArrayList
        For i As Integer = 0 To arrVoices.Count - 1
            arrLst.Add(arrVoices.Item(i).GetDescription)
        Next
        cmbVoices.DataSource = arrLst
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LoadFromTextFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadFromTextFileToolStripMenuItem.Click
        flTxtFile.Filter = "Text Files (*.txt)|*.txt"
        If flTxtFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Load File Content
            Dim strmRedr As New System.IO.StreamReader(flTxtFile.FileName)
            txtSpeach.Text = strmRedr.ReadToEnd
            strmRedr.Close()
        End If
    End Sub

    Private Sub btnSavetoFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavetoFile.Click
        Me.Cursor = Cursors.WaitCursor

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim oVoice As New SpeechLib.SpVoice
            Dim cpFileStream As New SpeechLib.SpFileStream
            cpFileStream.Open(SaveFileDialog1.FileName, SpeechLib.SpeechStreamFileMode.SSFMCreateForWrite, False)
            oVoice.AudioOutputStream = cpFileStream
            oVoice.Voice = oVoice.GetVoices.Item(cmbVoices.SelectedIndex)
            oVoice.Volume = trVolume.Value
            oVoice.Speak(txtSpeach.Text, SpeechLib.SpeechVoiceSpeakFlags.SVSFDefault)

            oVoice = Nothing
            cpFileStream.Close()
            cpFileStream = Nothing
        End If
        Me.Cursor = Cursors.Arrow
    End Sub
End Class
