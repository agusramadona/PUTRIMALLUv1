Partial Class MainForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblTimerDisplay = New System.Windows.Forms.Label()
        Me.txtCountdown = New System.Windows.Forms.TextBox()
        Me.lblCountdown = New System.Windows.Forms.Label()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.txtFolderPath = New System.Windows.Forms.TextBox()
        Me.lblFolderToCompress = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.cmbAction = New System.Windows.Forms.ComboBox()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.mainTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTimerDisplay
        '
        Me.lblTimerDisplay.BackColor = System.Drawing.Color.Black
        Me.lblTimerDisplay.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimerDisplay.ForeColor = System.Drawing.Color.Green
        Me.lblTimerDisplay.Location = New System.Drawing.Point(430, 10)
        Me.lblTimerDisplay.Name = "lblTimerDisplay"
        Me.lblTimerDisplay.Size = New System.Drawing.Size(140, 30)
        Me.lblTimerDisplay.TabIndex = 0
        Me.lblTimerDisplay.Text = "00:00:00"
        Me.lblTimerDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTimerDisplay.Visible = False
        '
        'txtCountdown
        '
        Me.txtCountdown.Location = New System.Drawing.Point(20, 45)
        Me.txtCountdown.Name = "txtCountdown"
        Me.txtCountdown.Size = New System.Drawing.Size(250, 20)
        Me.txtCountdown.TabIndex = 1
        '
        'lblCountdown
        '
        Me.lblCountdown.AutoSize = True
        Me.lblCountdown.Location = New System.Drawing.Point(20, 20)
        Me.lblCountdown.Name = "lblCountdown"
        Me.lblCountdown.Size = New System.Drawing.Size(187, 13)
        Me.lblCountdown.TabIndex = 2
        Me.lblCountdown.Text = "Countdown Timer (e.g., 30s, 10m, 1h):"
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Location = New System.Drawing.Point(480, 108)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(80, 23)
        Me.btnBrowseFolder.TabIndex = 4
        Me.btnBrowseFolder.Text = "Browse..."
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'txtFolderPath
        '
        Me.txtFolderPath.Location = New System.Drawing.Point(20, 110)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.ReadOnly = True
        Me.txtFolderPath.Size = New System.Drawing.Size(450, 20)
        Me.txtFolderPath.TabIndex = 3
        '
        'lblFolderToCompress
        '
        Me.lblFolderToCompress.AutoSize = True
        Me.lblFolderToCompress.Location = New System.Drawing.Point(20, 85)
        Me.lblFolderToCompress.Name = "lblFolderToCompress"
        Me.lblFolderToCompress.Size = New System.Drawing.Size(100, 13)
        Me.lblFolderToCompress.TabIndex = 5
        Me.lblFolderToCompress.Text = "Folder to Compress:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(20, 175)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(250, 20)
        Me.txtPassword.TabIndex = 5
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(20, 150)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(136, 13)
        Me.lblPassword.TabIndex = 7
        Me.lblPassword.Text = "Compressed File Password:"
        '
        'cmbAction
        '
        Me.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAction.FormattingEnabled = True
        Me.cmbAction.Items.AddRange(New Object() {"Shutdown", "Restart"})
        Me.cmbAction.Location = New System.Drawing.Point(20, 240)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(250, 21)
        Me.cmbAction.TabIndex = 6
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Location = New System.Drawing.Point(20, 215)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(110, 13)
        Me.lblAction.TabIndex = 9
        Me.lblAction.Text = "Action on Completion:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(200, 320)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 30)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(300, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 30)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'mainTimer
        '
        Me.mainTimer.Interval = 1000
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.ImageLocation = "C:\Users\agus\Documents\TEKRA\PUTRIMALLU\logo.png"
        Me.PictureBox1.Location = New System.Drawing.Point(411, 137)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(149, 168)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblAction)
        Me.Controls.Add(Me.cmbAction)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblFolderToCompress)
        Me.Controls.Add(Me.txtFolderPath)
        Me.Controls.Add(Me.btnBrowseFolder)
        Me.Controls.Add(Me.lblCountdown)
        Me.Controls.Add(Me.txtCountdown)
        Me.Controls.Add(Me.lblTimerDisplay)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "PutriMallu App"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Friend WithEvents lblTimerDisplay As System.Windows.Forms.Label
    Friend WithEvents txtCountdown As System.Windows.Forms.TextBox
    Friend WithEvents lblCountdown As System.Windows.Forms.Label
    Friend WithEvents btnBrowseFolder As System.Windows.Forms.Button
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents lblFolderToCompress As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents cmbAction As System.Windows.Forms.ComboBox
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents mainTimer As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As PictureBox
End Class