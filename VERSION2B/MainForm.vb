Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.IO.Compression
Imports System.Diagnostics
Imports System.Threading
Imports System.Runtime.InteropServices ' Required for P/Invoke

Public Class MainForm
    Inherits Form

    ' Structure for GetLastInputInfo
    <StructLayout(LayoutKind.Sequential)>
    Private Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)>
        Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dwTime As UInteger
    End Structure

    ' P/Invoke declaration for GetLastInputInfo
    <DllImport("user32.dll")>
    Private Shared Function GetLastInputInfo(ByRef plii As LASTINPUTINFO) As Boolean
    End Function

    Private timeRemaining As TimeSpan ' For the 10-second action countdown
    Private originalFolderPath As String
    Private compressPassword As String
    Private selectedAction As String

    Private isMonitoringIdle As Boolean = False
    Private isActionCountdownActive As Boolean = False

    Private Const IDLE_THRESHOLD_SECONDS As Integer = 30
    Private Const ACTION_COUNTDOWN_SECONDS As Integer = 10

    Private idleTimer As New System.Windows.Forms.Timer() ' MODIFIED THIS LINE
    ' mainTimer is already declared in Designer.vb as System.Windows.Forms.Timer

    Public Sub New()
        InitializeComponent()
        CenterToScreen()
        If cmbAction.Items.Count > 0 AndAlso cmbAction.SelectedIndex = -1 Then
            cmbAction.SelectedIndex = 0
        End If

        idleTimer.Interval = 1000 ' Check every second
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblTimerDisplay.Location = New Point(Me.ClientSize.Width - 150 - 10, 10)
        Dim buttonY As Integer = Me.ClientSize.Height - btnOK.Height - 20
        Dim totalButtonWidth As Integer = btnOK.Width + btnCancel.Width + 20
        Dim startX As Integer = (Me.ClientSize.Width - totalButtonWidth) / 2
        btnOK.Location = New Point(startX, buttonY)
        btnCancel.Location = New Point(startX + btnOK.Width + 20, buttonY)

        AddHandler btnBrowseFolder.Click, AddressOf BtnBrowseFolder_Click
        AddHandler btnOK.Click, AddressOf BtnStart_Click ' Changed from BtnOK_Click
        AddHandler btnCancel.Click, AddressOf BtnStop_Click ' Changed from BtnCancel_Click
        AddHandler mainTimer.Tick, AddressOf ActionCountdown_Tick ' Renamed from MainTimer_Tick
        AddHandler idleTimer.Tick, AddressOf IdleTimer_Tick
    End Sub

    Private Function GetLastInputTime() As UInteger
        Dim lastInputInfo As New LASTINPUTINFO()
        lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo)
        If GetLastInputInfo(lastInputInfo) Then
            Return lastInputInfo.dwTime
        End If
        Return 0
    End Function

    Private Function GetIdleTimeSeconds() As UInteger
        Return CUInt((Environment.TickCount - GetLastInputTime()) / 1000)
    End Function

    Private Sub IdleTimer_Tick(sender As Object, e As EventArgs)
        If Not isMonitoringIdle OrElse isActionCountdownActive Then Return

        If GetIdleTimeSeconds() >= IDLE_THRESHOLD_SECONDS Then
            StartActionCountdown()
        End If
    End Sub

    Private Sub StartActionCountdown()
        isActionCountdownActive = True
        timeRemaining = TimeSpan.FromSeconds(ACTION_COUNTDOWN_SECONDS)
        lblTimerDisplay.Text = timeRemaining.ToString("hh\:mm\:ss")
        lblTimerDisplay.Visible = True
        mainTimer.Start()
        ' Controls are already disabled if monitoring was started
    End Sub

    Private Sub ActionCountdown_Tick(sender As Object, e As EventArgs)
        ' Check for activity to interrupt countdown
        If GetIdleTimeSeconds() < 1 Then ' Activity detected (margin of 1 second)
            StopActionCountdown("User activity detected. Countdown aborted.")
            Return
        End If

        If timeRemaining.TotalSeconds > 0 Then
            timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1))
            lblTimerDisplay.Text = timeRemaining.ToString("hh\:mm\:ss")
        Else
            mainTimer.Stop()
            lblTimerDisplay.Text = "00:00:00"
            PerformCompressionAndAction()
        End If
    End Sub

    Private Sub StopActionCountdown(Optional message As String = "")
        mainTimer.Stop()
        isActionCountdownActive = False
        lblTimerDisplay.Visible = False
        If Not String.IsNullOrEmpty(message) Then
            ' Optionally show a message or log, for now, just hide timer
            ' MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' If monitoring is still supposed to be active, idleTimer will continue
    End Sub

    Private Sub BtnBrowseFolder_Click(sender As Object, e As EventArgs)
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select a folder to compress"
            If fbd.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(fbd.SelectedPath) Then
                txtFolderPath.Text = fbd.SelectedPath
            End If
        End Using
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs)
        If isMonitoringIdle Then
            MessageBox.Show("Monitoring is already active.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        isMonitoringIdle = True
        SetControlsEnabled(False) ' Disable input fields while monitoring
        idleTimer.Start()
        btnOK.Enabled = False ' Disable Start button itself
        btnCancel.Enabled = True ' Enable Stop button
        lblTimerDisplay.Text = "Monitoring..."
        lblTimerDisplay.Visible = True ' Show monitoring status
    End Sub

    Private Sub BtnStop_Click(sender As Object, e As EventArgs)
        If Not isMonitoringIdle And Not isActionCountdownActive Then
            MessageBox.Show("Monitoring is not active.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        StopMonitoringAndCountdown()
    End Sub

    Private Sub StopMonitoringAndCountdown()
        idleTimer.Stop()
        mainTimer.Stop()
        isMonitoringIdle = False
        isActionCountdownActive = False
        lblTimerDisplay.Visible = False
        SetControlsEnabled(True) ' Re-enable input fields
        btnOK.Enabled = True ' Enable Start button
        btnCancel.Enabled = False ' Disable Stop button
        MessageBox.Show("Monitoring stopped.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Removed TryParseCountdown as it's no longer needed

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
                                                            ' Password note: Standard ZipFile doesn't support passwords.
                                                            ' You'd need a third-party library like DotNetZip or SharpCompress for this.
                                                            ' This part remains a placeholder for that functionality.
                                                            Me.Invoke(New Action(Sub()
                                                                                     MessageBox.Show("Compression complete. Password protection requires a third-party library and is not implemented in this version.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                                 End Sub))
                                                        End If

                                                        Directory.Delete(originalFolderPath, True)

                                                        Me.Invoke(New Action(Sub()
                                                                                 PerformSystemAction(selectedAction)
                                                                                 ResetProcessStateAfterAction()
                                                                             End Sub))

                                                    Catch ex As Exception
                                                        Me.Invoke(New Action(Sub()
                                                                                 MessageBox.Show($"Error during compression/deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                                 ResetProcessStateAfterAction()
                                                                             End Sub))
                                                    End Try
                                                End Sub)
            compressionThread.IsBackground = True
            compressionThread.Start()

        Catch ex As Exception
            MessageBox.Show($"Error preparing compression: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ResetProcessStateAfterAction()
        End Try
    End Sub

    Private Sub PerformSystemAction(action As String)
        Try
            Select Case action
                Case "Shutdown"
                    Process.Start("shutdown", "/s /f /t 0")
                Case "Restart"
                    Process.Start("shutdown", "/r /f /t 0")
            End Select
        Catch ex As Exception
            MessageBox.Show($"Error performing system action: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ' Application will close on shutdown/restart, so no further UI reset needed here for those actions.
    End Sub

    Private Sub SetControlsEnabled(enabled As Boolean)
        txtFolderPath.Enabled = enabled
        btnBrowseFolder.Enabled = enabled
        txtPassword.Enabled = enabled
        cmbAction.Enabled = enabled
        ' btnOK and btnCancel are handled separately in Start/Stop logic
    End Sub

    Private Sub ResetProcessStateAfterAction()
        ' Called after compression/action attempt (success or fail)
        isMonitoringIdle = False
        isActionCountdownActive = False
        Me.Invoke(New Action(Sub()
                                 lblTimerDisplay.Visible = False
                                 SetControlsEnabled(True)
                                 btnOK.Enabled = True
                                 btnCancel.Enabled = False ' Stop button should be disabled if not monitoring
                             End Sub))
    End Sub

    ' Ensure form closes cleanly and stops timers
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        idleTimer.Stop()
        mainTimer.Stop()
        MyBase.OnFormClosing(e)
    End Sub

End Class

Public Module Module1
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New MainForm())
    End Sub
End Module