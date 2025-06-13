Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.IO.Compression
Imports System.Diagnostics
Imports System.Threading

Public Class MainForm
    Inherits Form

    ' Control declarations are now in MainForm.Designer.vb

    Private timeRemaining As TimeSpan
    Private originalFolderPath As String
    Private compressPassword As String
    Private selectedAction As String
    Private isProcessing As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CenterToScreen()
        ' Me.Text and Me.Size are set in Designer.vb
        ' Me.FormBorderStyle and Me.MaximizeBox are set in Designer.vb

        ' Ensure default ComboBox selection if items exist
        If cmbAction.Items.Count > 0 AndAlso cmbAction.SelectedIndex = -1 Then
            cmbAction.SelectedIndex = 0
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Adjust positions that depend on ClientSize
        lblTimerDisplay.Location = New Point(Me.ClientSize.Width - 150 - 10, 10) ' 10px padding from right

        ' Center OK and Cancel buttons
        Dim buttonY As Integer = Me.ClientSize.Height - btnOK.Height - 20 ' 20px padding from bottom
        Dim totalButtonWidth As Integer = btnOK.Width + btnCancel.Width + 20 ' 20px spacing
        Dim startX As Integer = (Me.ClientSize.Width - totalButtonWidth) / 2

        btnOK.Location = New Point(startX, buttonY)
        btnCancel.Location = New Point(startX + btnOK.Width + 20, buttonY)

        ' Add handlers that were previously in the old InitializeComponent
        AddHandler btnBrowseFolder.Click, AddressOf BtnBrowseFolder_Click
        AddHandler btnOK.Click, AddressOf BtnOK_Click
        AddHandler btnCancel.Click, AddressOf BtnCancel_Click
        AddHandler mainTimer.Tick, AddressOf MainTimer_Tick
    End Sub

    ' InitializeComponent method is now in MainForm.Designer.vb

    Private Sub BtnBrowseFolder_Click(sender As Object, e As EventArgs)
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select a folder to compress"
            If fbd.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(fbd.SelectedPath) Then
                txtFolderPath.Text = fbd.SelectedPath
            End If
        End Using
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs)
        If isProcessing Then
            ResetProcess()
        End If

        If String.IsNullOrWhiteSpace(txtCountdown.Text) Then
            MessageBox.Show("Please enter a countdown time.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCountdown.Focus()
            Return
        End If

        If Not TryParseCountdown(txtCountdown.Text, timeRemaining) Then
            MessageBox.Show("Invalid countdown format. Use number followed by s, m, or h (e.g., 30s, 10m, 1h).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCountdown.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtFolderPath.Text) Then
            MessageBox.Show("Please select a folder to compress.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowseFolder.Focus()
            Return
        End If

        If Not Directory.Exists(txtFolderPath.Text) Then
            MessageBox.Show("The selected folder does not exist.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnBrowseFolder.Focus()
            Return
        End If

        originalFolderPath = txtFolderPath.Text
        compressPassword = txtPassword.Text
        selectedAction = cmbAction.SelectedItem.ToString()

        isProcessing = True
        lblTimerDisplay.Text = timeRemaining.ToString("hh\:mm\:ss")
        lblTimerDisplay.Visible = True
        SetControlsEnabled(False)
        mainTimer.Start()
    End Sub

    Private Function TryParseCountdown(input As String, ByRef result As TimeSpan) As Boolean
        input = input.Trim().ToLower()
        If String.IsNullOrEmpty(input) OrElse input.Length < 2 Then Return False

        Dim unit As Char = input.Last()
        Dim valueString As String = input.Substring(0, input.Length - 1)
        Dim numericValue As Integer

        If Not Integer.TryParse(valueString, numericValue) OrElse numericValue <= 0 Then Return False

        Select Case unit
            Case "s"c
                result = TimeSpan.FromSeconds(numericValue)
                Return True
            Case "m"c
                result = TimeSpan.FromMinutes(numericValue)
                Return True
            Case "h"c
                result = TimeSpan.FromHours(numericValue)
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Sub MainTimer_Tick(sender As Object, e As EventArgs)
        If timeRemaining.TotalSeconds > 0 Then
            timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1))
            lblTimerDisplay.Text = timeRemaining.ToString("hh\:mm\:ss")
        Else
            mainTimer.Stop()
            lblTimerDisplay.Text = "00:00:00"
            PerformCompressionAndAction()
        End If
    End Sub

    Private Sub PerformCompressionAndAction()
        Try
            Dim parentDir As String = Path.GetDirectoryName(originalFolderPath)
            Dim folderName As String = Path.GetFileName(originalFolderPath)
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim zipFileName As String = $"{folderName}_{timestamp}.zip"
            Dim zipFilePath As String = Path.Combine(parentDir, zipFileName)

            If File.Exists(zipFilePath) Then
                zipFilePath = Path.Combine(parentDir, $"{folderName}_{timestamp}_{Guid.NewGuid().ToString().Substring(0, 4)}.zip")
            End If

            Dim compressionThread As New Thread(Sub()
                                                    Try
                                                        Using archive As ZipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create)
                                                            For Each currentFilePath As String In Directory.GetFiles(originalFolderPath, "*.*", SearchOption.AllDirectories)
                                                                Dim entryName As String = currentFilePath.Substring(originalFolderPath.Length + 1)
                                                                Dim entry As ZipArchiveEntry = archive.CreateEntry(entryName)
                                                                Using entryStream As Stream = entry.Open()
                                                                    Using fileStream As FileStream = File.OpenRead(currentFilePath)
                                                                        fileStream.CopyTo(entryStream)
                                                                    End Using
                                                                End Using
                                                            Next
                                                        End Using

                                                        If Not String.IsNullOrWhiteSpace(compressPassword) Then
                                                            ' Password note
                                                        End If

                                                        Directory.Delete(originalFolderPath, True)

                                                        Me.Invoke(New Action(Sub()
                                                                                 PerformSystemAction(selectedAction)
                                                                                 ResetProcess()
                                                                             End Sub))

                                                    Catch ex As Exception
                                                        Me.Invoke(New Action(Sub()
                                                                                 MessageBox.Show($"Error during compression/deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                 ResetProcess()
                                                                             End Sub))
                                                    End Try
                                                End Sub)
            compressionThread.IsBackground = True
            compressionThread.Start()

        Catch ex As Exception
            MessageBox.Show($"Error preparing compression: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ResetProcess()
        End Try
    End Sub

    Private Sub PerformSystemAction(action As String)
        Try
            Select Case action
                Case "Shutdown"
                    Process.Start("shutdown", "/s /f /t 0") ' Added /f
                Case "Restart"
                    Process.Start("shutdown", "/r /f /t 0") ' Added /f
            End Select
        Catch ex As Exception
            MessageBox.Show($"Error performing system action: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs)
        If isProcessing Then
            ResetProcess()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ResetProcess()
        mainTimer.Stop()
        isProcessing = False
        lblTimerDisplay.Visible = False
        lblTimerDisplay.Text = "00:00:00"
        txtCountdown.Clear()
        txtFolderPath.Clear()
        txtPassword.Clear()
        If cmbAction.Items.Count > 0 Then cmbAction.SelectedIndex = 0
        SetControlsEnabled(True)
        txtCountdown.Focus()
    End Sub

    Private Sub SetControlsEnabled(enabled As Boolean)
        txtCountdown.Enabled = enabled
        btnBrowseFolder.Enabled = enabled
        txtFolderPath.Enabled = enabled
        txtPassword.Enabled = enabled
        cmbAction.Enabled = enabled
        btnOK.Text = If(enabled, "OK", "Restart Process")
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If isProcessing Then
            Dim result As DialogResult = MessageBox.Show("A process is currently running. Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then
                e.Cancel = True
            Else
                mainTimer.Stop()
            End If
        End If
        MyBase.OnFormClosing(e)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class

Public Module Module1
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MainForm())
    End Sub
End Module